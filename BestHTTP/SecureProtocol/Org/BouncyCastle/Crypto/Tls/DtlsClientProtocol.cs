﻿using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000409 RID: 1033
	public class DtlsClientProtocol : DtlsProtocol
	{
		// Token: 0x060029BC RID: 10684 RVA: 0x001119F4 File Offset: 0x0010FBF4
		public DtlsClientProtocol(SecureRandom secureRandom) : base(secureRandom)
		{
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x00111A00 File Offset: 0x0010FC00
		public virtual DtlsTransport Connect(TlsClient client, DatagramTransport transport)
		{
			if (client == null)
			{
				throw new ArgumentNullException("client");
			}
			if (transport == null)
			{
				throw new ArgumentNullException("transport");
			}
			SecurityParameters securityParameters = new SecurityParameters();
			securityParameters.entity = 1;
			DtlsClientProtocol.ClientHandshakeState clientHandshakeState = new DtlsClientProtocol.ClientHandshakeState();
			clientHandshakeState.client = client;
			clientHandshakeState.clientContext = new TlsClientContextImpl(this.mSecureRandom, securityParameters);
			securityParameters.clientRandom = TlsProtocol.CreateRandomBlock(client.ShouldUseGmtUnixTime(), clientHandshakeState.clientContext.NonceRandomGenerator);
			client.Init(clientHandshakeState.clientContext);
			DtlsRecordLayer recordLayer = new DtlsRecordLayer(transport, clientHandshakeState.clientContext, client, 22);
			TlsSession sessionToResume = clientHandshakeState.client.GetSessionToResume();
			if (sessionToResume != null && sessionToResume.IsResumable)
			{
				SessionParameters sessionParameters = sessionToResume.ExportSessionParameters();
				if (sessionParameters != null && sessionParameters.IsExtendedMasterSecret)
				{
					clientHandshakeState.tlsSession = sessionToResume;
					clientHandshakeState.sessionParameters = sessionParameters;
				}
			}
			DtlsTransport result;
			try
			{
				result = this.ClientHandshake(clientHandshakeState, recordLayer);
			}
			catch (TlsFatalAlert tlsFatalAlert)
			{
				this.AbortClientHandshake(clientHandshakeState, recordLayer, tlsFatalAlert.AlertDescription);
				throw tlsFatalAlert;
			}
			catch (IOException ex)
			{
				this.AbortClientHandshake(clientHandshakeState, recordLayer, 80);
				throw ex;
			}
			catch (Exception alertCause)
			{
				this.AbortClientHandshake(clientHandshakeState, recordLayer, 80);
				throw new TlsFatalAlert(80, alertCause);
			}
			finally
			{
				securityParameters.Clear();
			}
			return result;
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x00111B48 File Offset: 0x0010FD48
		internal virtual void AbortClientHandshake(DtlsClientProtocol.ClientHandshakeState state, DtlsRecordLayer recordLayer, byte alertDescription)
		{
			recordLayer.Fail(alertDescription);
			this.InvalidateSession(state);
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x00111B58 File Offset: 0x0010FD58
		internal virtual DtlsTransport ClientHandshake(DtlsClientProtocol.ClientHandshakeState state, DtlsRecordLayer recordLayer)
		{
			SecurityParameters securityParameters = state.clientContext.SecurityParameters;
			DtlsReliableHandshake dtlsReliableHandshake = new DtlsReliableHandshake(state.clientContext, recordLayer);
			byte[] array = this.GenerateClientHello(state, state.client);
			recordLayer.SetWriteVersion(ProtocolVersion.DTLSv10);
			dtlsReliableHandshake.SendMessage(1, array);
			DtlsReliableHandshake.Message message = dtlsReliableHandshake.ReceiveMessage();
			while (message.Type == 3)
			{
				ProtocolVersion readVersion = recordLayer.ReadVersion;
				ProtocolVersion clientVersion = state.clientContext.ClientVersion;
				if (!readVersion.IsEqualOrEarlierVersionOf(clientVersion))
				{
					throw new TlsFatalAlert(47);
				}
				recordLayer.ReadVersion = null;
				byte[] cookie = this.ProcessHelloVerifyRequest(state, message.Body);
				byte[] body = DtlsClientProtocol.PatchClientHelloWithCookie(array, cookie);
				dtlsReliableHandshake.ResetHandshakeMessagesDigest();
				dtlsReliableHandshake.SendMessage(1, body);
				message = dtlsReliableHandshake.ReceiveMessage();
			}
			if (message.Type != 2)
			{
				throw new TlsFatalAlert(10);
			}
			ProtocolVersion readVersion2 = recordLayer.ReadVersion;
			this.ReportServerVersion(state, readVersion2);
			recordLayer.SetWriteVersion(readVersion2);
			this.ProcessServerHello(state, message.Body);
			dtlsReliableHandshake.NotifyHelloComplete();
			DtlsProtocol.ApplyMaxFragmentLengthExtension(recordLayer, securityParameters.maxFragmentLength);
			if (state.resumedSession)
			{
				securityParameters.masterSecret = Arrays.Clone(state.sessionParameters.MasterSecret);
				recordLayer.InitPendingEpoch(state.client.GetCipher());
				byte[] expected_verify_data = TlsUtilities.CalculateVerifyData(state.clientContext, "server finished", TlsProtocol.GetCurrentPrfHash(state.clientContext, dtlsReliableHandshake.HandshakeHash, null));
				this.ProcessFinished(dtlsReliableHandshake.ReceiveMessageBody(20), expected_verify_data);
				byte[] body2 = TlsUtilities.CalculateVerifyData(state.clientContext, "client finished", TlsProtocol.GetCurrentPrfHash(state.clientContext, dtlsReliableHandshake.HandshakeHash, null));
				dtlsReliableHandshake.SendMessage(20, body2);
				dtlsReliableHandshake.Finish();
				state.clientContext.SetResumableSession(state.tlsSession);
				state.client.NotifyHandshakeComplete();
				return new DtlsTransport(recordLayer);
			}
			this.InvalidateSession(state);
			if (state.selectedSessionID.Length != 0)
			{
				state.tlsSession = new TlsSessionImpl(state.selectedSessionID, null);
			}
			message = dtlsReliableHandshake.ReceiveMessage();
			if (message.Type == 23)
			{
				this.ProcessServerSupplementalData(state, message.Body);
				message = dtlsReliableHandshake.ReceiveMessage();
			}
			else
			{
				state.client.ProcessServerSupplementalData(null);
			}
			state.keyExchange = state.client.GetKeyExchange();
			state.keyExchange.Init(state.clientContext);
			Certificate certificate = null;
			if (message.Type == 11)
			{
				certificate = this.ProcessServerCertificate(state, message.Body);
				message = dtlsReliableHandshake.ReceiveMessage();
			}
			else
			{
				state.keyExchange.SkipServerCredentials();
			}
			if (certificate == null || certificate.IsEmpty)
			{
				state.allowCertificateStatus = false;
			}
			if (message.Type == 22)
			{
				this.ProcessCertificateStatus(state, message.Body);
				message = dtlsReliableHandshake.ReceiveMessage();
			}
			if (message.Type == 12)
			{
				this.ProcessServerKeyExchange(state, message.Body);
				message = dtlsReliableHandshake.ReceiveMessage();
			}
			else
			{
				state.keyExchange.SkipServerKeyExchange();
			}
			if (message.Type == 13)
			{
				this.ProcessCertificateRequest(state, message.Body);
				TlsUtilities.TrackHashAlgorithms(dtlsReliableHandshake.HandshakeHash, state.certificateRequest.SupportedSignatureAlgorithms);
				message = dtlsReliableHandshake.ReceiveMessage();
			}
			if (message.Type != 14)
			{
				throw new TlsFatalAlert(10);
			}
			if (message.Body.Length != 0)
			{
				throw new TlsFatalAlert(50);
			}
			dtlsReliableHandshake.HandshakeHash.SealHashAlgorithms();
			IList clientSupplementalData = state.client.GetClientSupplementalData();
			if (clientSupplementalData != null)
			{
				byte[] body3 = DtlsProtocol.GenerateSupplementalData(clientSupplementalData);
				dtlsReliableHandshake.SendMessage(23, body3);
			}
			if (state.certificateRequest != null)
			{
				state.clientCredentials = state.authentication.GetClientCredentials(state.clientContext, state.certificateRequest);
				Certificate certificate2 = null;
				if (state.clientCredentials != null)
				{
					certificate2 = state.clientCredentials.Certificate;
				}
				if (certificate2 == null)
				{
					certificate2 = Certificate.EmptyChain;
				}
				byte[] body4 = DtlsProtocol.GenerateCertificate(certificate2);
				dtlsReliableHandshake.SendMessage(11, body4);
			}
			if (state.clientCredentials != null)
			{
				state.keyExchange.ProcessClientCredentials(state.clientCredentials);
			}
			else
			{
				state.keyExchange.SkipClientCredentials();
			}
			byte[] body5 = this.GenerateClientKeyExchange(state);
			dtlsReliableHandshake.SendMessage(16, body5);
			TlsHandshakeHash tlsHandshakeHash = dtlsReliableHandshake.PrepareToFinish();
			securityParameters.sessionHash = TlsProtocol.GetCurrentPrfHash(state.clientContext, tlsHandshakeHash, null);
			TlsProtocol.EstablishMasterSecret(state.clientContext, state.keyExchange);
			recordLayer.InitPendingEpoch(state.client.GetCipher());
			if (state.clientCredentials != null && state.clientCredentials is TlsSignerCredentials)
			{
				TlsSignerCredentials tlsSignerCredentials = (TlsSignerCredentials)state.clientCredentials;
				SignatureAndHashAlgorithm signatureAndHashAlgorithm = TlsUtilities.GetSignatureAndHashAlgorithm(state.clientContext, tlsSignerCredentials);
				byte[] hash;
				if (signatureAndHashAlgorithm == null)
				{
					hash = securityParameters.SessionHash;
				}
				else
				{
					hash = tlsHandshakeHash.GetFinalHash(signatureAndHashAlgorithm.Hash);
				}
				byte[] signature = tlsSignerCredentials.GenerateCertificateSignature(hash);
				DigitallySigned certificateVerify = new DigitallySigned(signatureAndHashAlgorithm, signature);
				byte[] body6 = this.GenerateCertificateVerify(state, certificateVerify);
				dtlsReliableHandshake.SendMessage(15, body6);
			}
			byte[] body7 = TlsUtilities.CalculateVerifyData(state.clientContext, "client finished", TlsProtocol.GetCurrentPrfHash(state.clientContext, dtlsReliableHandshake.HandshakeHash, null));
			dtlsReliableHandshake.SendMessage(20, body7);
			if (state.expectSessionTicket)
			{
				message = dtlsReliableHandshake.ReceiveMessage();
				if (message.Type != 4)
				{
					throw new TlsFatalAlert(10);
				}
				this.ProcessNewSessionTicket(state, message.Body);
			}
			byte[] expected_verify_data2 = TlsUtilities.CalculateVerifyData(state.clientContext, "server finished", TlsProtocol.GetCurrentPrfHash(state.clientContext, dtlsReliableHandshake.HandshakeHash, null));
			this.ProcessFinished(dtlsReliableHandshake.ReceiveMessageBody(20), expected_verify_data2);
			dtlsReliableHandshake.Finish();
			if (state.tlsSession != null)
			{
				state.sessionParameters = new SessionParameters.Builder().SetCipherSuite(securityParameters.CipherSuite).SetCompressionAlgorithm(securityParameters.CompressionAlgorithm).SetExtendedMasterSecret(securityParameters.IsExtendedMasterSecret).SetMasterSecret(securityParameters.MasterSecret).SetPeerCertificate(certificate).SetPskIdentity(securityParameters.PskIdentity).SetSrpIdentity(securityParameters.SrpIdentity).SetServerExtensions(state.serverExtensions).Build();
				state.tlsSession = TlsUtilities.ImportSession(state.tlsSession.SessionID, state.sessionParameters);
				state.clientContext.SetResumableSession(state.tlsSession);
			}
			state.client.NotifyHandshakeComplete();
			return new DtlsTransport(recordLayer);
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x00112130 File Offset: 0x00110330
		protected virtual byte[] GenerateCertificateVerify(DtlsClientProtocol.ClientHandshakeState state, DigitallySigned certificateVerify)
		{
			MemoryStream memoryStream = new MemoryStream();
			certificateVerify.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x00112150 File Offset: 0x00110350
		protected virtual byte[] GenerateClientHello(DtlsClientProtocol.ClientHandshakeState state, TlsClient client)
		{
			ProtocolVersion clientVersion = client.ClientVersion;
			if (!clientVersion.IsDtls)
			{
				throw new TlsFatalAlert(80);
			}
			TlsClientContextImpl clientContext = state.clientContext;
			clientContext.SetClientVersion(clientVersion);
			SecurityParameters securityParameters = clientContext.SecurityParameters;
			byte[] array = TlsUtilities.EmptyBytes;
			if (state.tlsSession != null)
			{
				array = state.tlsSession.SessionID;
				if (array == null || array.Length > 32)
				{
					array = TlsUtilities.EmptyBytes;
				}
			}
			bool isFallback = client.IsFallback;
			state.offeredCipherSuites = client.GetCipherSuites();
			if (array.Length != 0 && state.sessionParameters != null && (!state.sessionParameters.IsExtendedMasterSecret || !Arrays.Contains(state.offeredCipherSuites, state.sessionParameters.CipherSuite) || state.sessionParameters.CompressionAlgorithm != 0))
			{
				array = TlsUtilities.EmptyBytes;
			}
			state.clientExtensions = TlsExtensionsUtilities.EnsureExtensionsInitialised(client.GetClientExtensions());
			TlsExtensionsUtilities.AddExtendedMasterSecretExtension(state.clientExtensions);
			MemoryStream memoryStream = new MemoryStream();
			TlsUtilities.WriteVersion(clientVersion, memoryStream);
			memoryStream.Write(securityParameters.ClientRandom, 0, securityParameters.ClientRandom.Length);
			TlsUtilities.WriteOpaque8(array, memoryStream);
			TlsUtilities.WriteOpaque8(TlsUtilities.EmptyBytes, memoryStream);
			byte[] extensionData = TlsUtilities.GetExtensionData(state.clientExtensions, 65281);
			bool flag = extensionData == null;
			bool flag2 = !Arrays.Contains(state.offeredCipherSuites, 255);
			if (flag && flag2)
			{
				state.offeredCipherSuites = Arrays.Append(state.offeredCipherSuites, 255);
			}
			if (isFallback && !Arrays.Contains(state.offeredCipherSuites, 22016))
			{
				state.offeredCipherSuites = Arrays.Append(state.offeredCipherSuites, 22016);
			}
			TlsUtilities.WriteUint16ArrayWithUint16Length(state.offeredCipherSuites, memoryStream);
			TlsUtilities.WriteUint8ArrayWithUint8Length(new byte[1], memoryStream);
			TlsProtocol.WriteExtensions(memoryStream, state.clientExtensions);
			return memoryStream.ToArray();
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x001122F4 File Offset: 0x001104F4
		protected virtual byte[] GenerateClientKeyExchange(DtlsClientProtocol.ClientHandshakeState state)
		{
			MemoryStream memoryStream = new MemoryStream();
			state.keyExchange.GenerateClientKeyExchange(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x00112319 File Offset: 0x00110519
		protected virtual void InvalidateSession(DtlsClientProtocol.ClientHandshakeState state)
		{
			if (state.sessionParameters != null)
			{
				state.sessionParameters.Clear();
				state.sessionParameters = null;
			}
			if (state.tlsSession != null)
			{
				state.tlsSession.Invalidate();
				state.tlsSession = null;
			}
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x00112350 File Offset: 0x00110550
		protected virtual void ProcessCertificateRequest(DtlsClientProtocol.ClientHandshakeState state, byte[] body)
		{
			if (state.authentication == null)
			{
				throw new TlsFatalAlert(40);
			}
			MemoryStream memoryStream = new MemoryStream(body, false);
			state.certificateRequest = CertificateRequest.Parse(state.clientContext, memoryStream);
			TlsProtocol.AssertEmpty(memoryStream);
			state.keyExchange.ValidateCertificateRequest(state.certificateRequest);
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x001123A0 File Offset: 0x001105A0
		protected virtual void ProcessCertificateStatus(DtlsClientProtocol.ClientHandshakeState state, byte[] body)
		{
			if (!state.allowCertificateStatus)
			{
				throw new TlsFatalAlert(10);
			}
			MemoryStream memoryStream = new MemoryStream(body, false);
			state.certificateStatus = CertificateStatus.Parse(memoryStream);
			TlsProtocol.AssertEmpty(memoryStream);
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x001123D8 File Offset: 0x001105D8
		protected virtual byte[] ProcessHelloVerifyRequest(DtlsClientProtocol.ClientHandshakeState state, byte[] body)
		{
			MemoryStream memoryStream = new MemoryStream(body, false);
			ProtocolVersion protocolVersion = TlsUtilities.ReadVersion(memoryStream);
			byte[] array = TlsUtilities.ReadOpaque8(memoryStream);
			TlsProtocol.AssertEmpty(memoryStream);
			if (!protocolVersion.IsEqualOrEarlierVersionOf(state.clientContext.ClientVersion))
			{
				throw new TlsFatalAlert(47);
			}
			if (!ProtocolVersion.DTLSv12.IsEqualOrEarlierVersionOf(protocolVersion) && array.Length > 32)
			{
				throw new TlsFatalAlert(47);
			}
			return array;
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x00112438 File Offset: 0x00110638
		protected virtual void ProcessNewSessionTicket(DtlsClientProtocol.ClientHandshakeState state, byte[] body)
		{
			MemoryStream memoryStream = new MemoryStream(body, false);
			NewSessionTicket newSessionTicket = NewSessionTicket.Parse(memoryStream);
			TlsProtocol.AssertEmpty(memoryStream);
			state.client.NotifyNewSessionTicket(newSessionTicket);
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x00112464 File Offset: 0x00110664
		protected virtual Certificate ProcessServerCertificate(DtlsClientProtocol.ClientHandshakeState state, byte[] body)
		{
			MemoryStream memoryStream = new MemoryStream(body, false);
			Certificate certificate = Certificate.Parse(memoryStream);
			TlsProtocol.AssertEmpty(memoryStream);
			state.keyExchange.ProcessServerCertificate(certificate);
			state.authentication = state.client.GetAuthentication();
			state.authentication.NotifyServerCertificate(certificate);
			return certificate;
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x001124B0 File Offset: 0x001106B0
		protected virtual void ProcessServerHello(DtlsClientProtocol.ClientHandshakeState state, byte[] body)
		{
			SecurityParameters securityParameters = state.clientContext.SecurityParameters;
			MemoryStream input = new MemoryStream(body, false);
			ProtocolVersion server_version = TlsUtilities.ReadVersion(input);
			this.ReportServerVersion(state, server_version);
			securityParameters.serverRandom = TlsUtilities.ReadFully(32, input);
			state.selectedSessionID = TlsUtilities.ReadOpaque8(input);
			if (state.selectedSessionID.Length > 32)
			{
				throw new TlsFatalAlert(47);
			}
			state.client.NotifySessionID(state.selectedSessionID);
			state.resumedSession = (state.selectedSessionID.Length != 0 && state.tlsSession != null && Arrays.AreEqual(state.selectedSessionID, state.tlsSession.SessionID));
			int num = TlsUtilities.ReadUint16(input);
			if (!Arrays.Contains(state.offeredCipherSuites, num) || num == 0 || CipherSuite.IsScsv(num) || !TlsUtilities.IsValidCipherSuiteForVersion(num, state.clientContext.ServerVersion))
			{
				throw new TlsFatalAlert(47);
			}
			DtlsProtocol.ValidateSelectedCipherSuite(num, 47);
			state.client.NotifySelectedCipherSuite(num);
			byte b = TlsUtilities.ReadUint8(input);
			if (b != 0)
			{
				throw new TlsFatalAlert(47);
			}
			state.client.NotifySelectedCompressionMethod(b);
			state.serverExtensions = TlsProtocol.ReadExtensions(input);
			securityParameters.extendedMasterSecret = TlsExtensionsUtilities.HasExtendedMasterSecretExtension(state.serverExtensions);
			if (!securityParameters.IsExtendedMasterSecret && (state.resumedSession || state.client.RequiresExtendedMasterSecret()))
			{
				throw new TlsFatalAlert(40);
			}
			if (state.serverExtensions != null)
			{
				foreach (object obj in state.serverExtensions.Keys)
				{
					int num2 = (int)obj;
					if (num2 != 65281)
					{
						if (TlsUtilities.GetExtensionData(state.clientExtensions, num2) == null)
						{
							throw new TlsFatalAlert(110);
						}
						bool resumedSession = state.resumedSession;
					}
				}
			}
			byte[] extensionData = TlsUtilities.GetExtensionData(state.serverExtensions, 65281);
			if (extensionData != null)
			{
				state.secure_renegotiation = true;
				if (!Arrays.ConstantTimeAreEqual(extensionData, TlsProtocol.CreateRenegotiationInfo(TlsUtilities.EmptyBytes)))
				{
					throw new TlsFatalAlert(40);
				}
			}
			state.client.NotifySecureRenegotiation(state.secure_renegotiation);
			IDictionary dictionary = state.clientExtensions;
			IDictionary dictionary2 = state.serverExtensions;
			if (state.resumedSession)
			{
				if (num != state.sessionParameters.CipherSuite || b != state.sessionParameters.CompressionAlgorithm)
				{
					throw new TlsFatalAlert(47);
				}
				dictionary = null;
				dictionary2 = state.sessionParameters.ReadServerExtensions();
			}
			securityParameters.cipherSuite = num;
			securityParameters.compressionAlgorithm = b;
			if (dictionary2 != null && dictionary2.Count > 0)
			{
				bool flag = TlsExtensionsUtilities.HasEncryptThenMacExtension(dictionary2);
				if (flag && !TlsUtilities.IsBlockCipherSuite(securityParameters.CipherSuite))
				{
					throw new TlsFatalAlert(47);
				}
				securityParameters.encryptThenMac = flag;
				securityParameters.maxFragmentLength = DtlsProtocol.EvaluateMaxFragmentLengthExtension(state.resumedSession, dictionary, dictionary2, 47);
				securityParameters.truncatedHMac = TlsExtensionsUtilities.HasTruncatedHMacExtension(dictionary2);
				state.allowCertificateStatus = (!state.resumedSession && TlsUtilities.HasExpectedEmptyExtensionData(dictionary2, 5, 47));
				state.expectSessionTicket = (!state.resumedSession && TlsUtilities.HasExpectedEmptyExtensionData(dictionary2, 35, 47));
			}
			if (dictionary != null)
			{
				state.client.ProcessServerExtensions(dictionary2);
			}
			securityParameters.prfAlgorithm = TlsProtocol.GetPrfAlgorithm(state.clientContext, securityParameters.CipherSuite);
			securityParameters.verifyDataLength = 12;
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x001127F0 File Offset: 0x001109F0
		protected virtual void ProcessServerKeyExchange(DtlsClientProtocol.ClientHandshakeState state, byte[] body)
		{
			MemoryStream memoryStream = new MemoryStream(body, false);
			state.keyExchange.ProcessServerKeyExchange(memoryStream);
			TlsProtocol.AssertEmpty(memoryStream);
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x00112818 File Offset: 0x00110A18
		protected virtual void ProcessServerSupplementalData(DtlsClientProtocol.ClientHandshakeState state, byte[] body)
		{
			IList serverSupplementalData = TlsProtocol.ReadSupplementalDataMessage(new MemoryStream(body, false));
			state.client.ProcessServerSupplementalData(serverSupplementalData);
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x00112840 File Offset: 0x00110A40
		protected virtual void ReportServerVersion(DtlsClientProtocol.ClientHandshakeState state, ProtocolVersion server_version)
		{
			TlsClientContextImpl clientContext = state.clientContext;
			ProtocolVersion serverVersion = clientContext.ServerVersion;
			if (serverVersion == null)
			{
				clientContext.SetServerVersion(server_version);
				state.client.NotifyServerVersion(server_version);
				return;
			}
			if (!serverVersion.Equals(server_version))
			{
				throw new TlsFatalAlert(47);
			}
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x00112884 File Offset: 0x00110A84
		protected static byte[] PatchClientHelloWithCookie(byte[] clientHelloBody, byte[] cookie)
		{
			int num = 34;
			int num2 = (int)TlsUtilities.ReadUint8(clientHelloBody, num);
			int num3 = num + 1 + num2;
			int num4 = num3 + 1;
			byte[] array = new byte[clientHelloBody.Length + cookie.Length];
			Array.Copy(clientHelloBody, 0, array, 0, num3);
			TlsUtilities.CheckUint8(cookie.Length);
			TlsUtilities.WriteUint8((byte)cookie.Length, array, num3);
			Array.Copy(cookie, 0, array, num4, cookie.Length);
			Array.Copy(clientHelloBody, num4, array, num4 + cookie.Length, clientHelloBody.Length - num4);
			return array;
		}

		// Token: 0x02000918 RID: 2328
		protected internal class ClientHandshakeState
		{
			// Token: 0x040034BB RID: 13499
			internal TlsClient client;

			// Token: 0x040034BC RID: 13500
			internal TlsClientContextImpl clientContext;

			// Token: 0x040034BD RID: 13501
			internal TlsSession tlsSession;

			// Token: 0x040034BE RID: 13502
			internal SessionParameters sessionParameters;

			// Token: 0x040034BF RID: 13503
			internal SessionParameters.Builder sessionParametersBuilder;

			// Token: 0x040034C0 RID: 13504
			internal int[] offeredCipherSuites;

			// Token: 0x040034C1 RID: 13505
			internal IDictionary clientExtensions;

			// Token: 0x040034C2 RID: 13506
			internal IDictionary serverExtensions;

			// Token: 0x040034C3 RID: 13507
			internal byte[] selectedSessionID;

			// Token: 0x040034C4 RID: 13508
			internal bool resumedSession;

			// Token: 0x040034C5 RID: 13509
			internal bool secure_renegotiation;

			// Token: 0x040034C6 RID: 13510
			internal bool allowCertificateStatus;

			// Token: 0x040034C7 RID: 13511
			internal bool expectSessionTicket;

			// Token: 0x040034C8 RID: 13512
			internal TlsKeyExchange keyExchange;

			// Token: 0x040034C9 RID: 13513
			internal TlsAuthentication authentication;

			// Token: 0x040034CA RID: 13514
			internal CertificateStatus certificateStatus;

			// Token: 0x040034CB RID: 13515
			internal CertificateRequest certificateRequest;

			// Token: 0x040034CC RID: 13516
			internal TlsCredentials clientCredentials;
		}
	}
}
