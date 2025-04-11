using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000460 RID: 1120
	public abstract class TlsProtocol
	{
		// Token: 0x06002C1D RID: 11293 RVA: 0x00119F42 File Offset: 0x00118142
		public TlsProtocol(Stream stream, SecureRandom secureRandom) : this(stream, stream, secureRandom)
		{
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x00119F50 File Offset: 0x00118150
		public TlsProtocol(Stream input, Stream output, SecureRandom secureRandom)
		{
			this.mApplicationDataQueue = new ByteQueue(0);
			this.mAlertQueue = new ByteQueue(2);
			this.mHandshakeQueue = new ByteQueue(0);
			this.mAppDataSplitEnabled = true;
			this.mBlocking = true;
			base..ctor();
			this.mRecordStream = new RecordStream(this, input, output);
			this.mSecureRandom = secureRandom;
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x00119FAC File Offset: 0x001181AC
		public TlsProtocol(SecureRandom secureRandom)
		{
			this.mApplicationDataQueue = new ByteQueue(0);
			this.mAlertQueue = new ByteQueue(2);
			this.mHandshakeQueue = new ByteQueue(0);
			this.mAppDataSplitEnabled = true;
			this.mBlocking = true;
			base..ctor();
			this.mBlocking = false;
			this.mInputBuffers = new ByteQueueStream();
			this.mOutputBuffer = new ByteQueueStream();
			this.mRecordStream = new RecordStream(this, this.mInputBuffers, this.mOutputBuffer);
			this.mSecureRandom = secureRandom;
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06002C20 RID: 11296
		protected abstract TlsContext Context { get; }

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06002C21 RID: 11297
		internal abstract AbstractTlsContext ContextAdmin { get; }

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06002C22 RID: 11298
		protected abstract TlsPeer Peer { get; }

		// Token: 0x06002C23 RID: 11299 RVA: 0x0011A02F File Offset: 0x0011822F
		protected virtual void HandleAlertMessage(byte alertLevel, byte alertDescription)
		{
			this.Peer.NotifyAlertReceived(alertLevel, alertDescription);
			if (alertLevel == 1)
			{
				this.HandleAlertWarningMessage(alertDescription);
				return;
			}
			this.HandleFailure();
			throw new TlsFatalAlertReceived(alertDescription);
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x0011A056 File Offset: 0x00118256
		protected virtual void HandleAlertWarningMessage(byte alertDescription)
		{
			if (alertDescription == 0)
			{
				if (!this.mAppDataReady)
				{
					throw new TlsFatalAlert(40);
				}
				this.HandleClose(false);
			}
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x00002B75 File Offset: 0x00000D75
		protected virtual void HandleChangeCipherSpecMessage()
		{
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x0011A074 File Offset: 0x00118274
		protected virtual void HandleClose(bool user_canceled)
		{
			if (!this.mClosed)
			{
				this.mClosed = true;
				if (user_canceled && !this.mAppDataReady)
				{
					this.RaiseAlertWarning(90, "User canceled handshake");
				}
				this.RaiseAlertWarning(0, "Connection closed");
				this.mRecordStream.SafeClose();
				if (!this.mAppDataReady)
				{
					this.CleanupHandshake();
				}
			}
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x0011A0D5 File Offset: 0x001182D5
		protected virtual void HandleException(byte alertDescription, string message, Exception cause)
		{
			if (!this.mClosed)
			{
				this.RaiseAlertFatal(alertDescription, message, cause);
				this.HandleFailure();
			}
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x0011A0F0 File Offset: 0x001182F0
		protected virtual void HandleFailure()
		{
			this.mClosed = true;
			this.mFailedWithError = true;
			this.InvalidateSession();
			this.mRecordStream.SafeClose();
			if (!this.mAppDataReady)
			{
				this.CleanupHandshake();
			}
		}

		// Token: 0x06002C29 RID: 11305
		protected abstract void HandleHandshakeMessage(byte type, MemoryStream buf);

		// Token: 0x06002C2A RID: 11306 RVA: 0x0011A128 File Offset: 0x00118328
		protected virtual void ApplyMaxFragmentLengthExtension()
		{
			if (this.mSecurityParameters.maxFragmentLength >= 0)
			{
				if (!MaxFragmentLength.IsValid((byte)this.mSecurityParameters.maxFragmentLength))
				{
					throw new TlsFatalAlert(80);
				}
				int plaintextLimit = 1 << (int)(8 + this.mSecurityParameters.maxFragmentLength);
				this.mRecordStream.SetPlaintextLimit(plaintextLimit);
			}
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x0011A17D File Offset: 0x0011837D
		protected virtual void CheckReceivedChangeCipherSpec(bool expected)
		{
			if (expected != this.mReceivedChangeCipherSpec)
			{
				throw new TlsFatalAlert(10);
			}
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x0011A190 File Offset: 0x00118390
		protected virtual void CleanupHandshake()
		{
			if (this.mExpectedVerifyData != null)
			{
				Arrays.Fill(this.mExpectedVerifyData, 0);
				this.mExpectedVerifyData = null;
			}
			this.mSecurityParameters.Clear();
			this.mPeerCertificate = null;
			this.mOfferedCipherSuites = null;
			this.mOfferedCompressionMethods = null;
			this.mClientExtensions = null;
			this.mServerExtensions = null;
			this.mResumedSession = false;
			this.mReceivedChangeCipherSpec = false;
			this.mSecureRenegotiation = false;
			this.mAllowCertificateStatus = false;
			this.mExpectSessionTicket = false;
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x0011A209 File Offset: 0x00118409
		protected virtual void BlockForHandshake()
		{
			if (this.mBlocking)
			{
				while (this.mConnectionState != 16)
				{
					if (this.mClosed)
					{
						throw new TlsFatalAlert(80);
					}
					this.SafeReadRecord();
				}
			}
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x0011A238 File Offset: 0x00118438
		protected virtual void CompleteHandshake()
		{
			try
			{
				this.mConnectionState = 16;
				this.mAlertQueue.Shrink();
				this.mHandshakeQueue.Shrink();
				this.mRecordStream.FinaliseHandshake();
				this.mAppDataSplitEnabled = !TlsUtilities.IsTlsV11(this.Context);
				if (!this.mAppDataReady)
				{
					this.mAppDataReady = true;
					if (this.mBlocking)
					{
						this.mTlsStream = new TlsStream(this);
					}
				}
				if (this.mTlsSession != null)
				{
					if (this.mSessionParameters == null)
					{
						this.mSessionParameters = new SessionParameters.Builder().SetCipherSuite(this.mSecurityParameters.CipherSuite).SetCompressionAlgorithm(this.mSecurityParameters.CompressionAlgorithm).SetExtendedMasterSecret(this.mSecurityParameters.IsExtendedMasterSecret).SetMasterSecret(this.mSecurityParameters.MasterSecret).SetPeerCertificate(this.mPeerCertificate).SetPskIdentity(this.mSecurityParameters.PskIdentity).SetSrpIdentity(this.mSecurityParameters.SrpIdentity).SetServerExtensions(this.mServerExtensions).Build();
						this.mTlsSession = new TlsSessionImpl(this.mTlsSession.SessionID, this.mSessionParameters);
					}
					this.ContextAdmin.SetResumableSession(this.mTlsSession);
				}
				this.Peer.NotifyHandshakeComplete();
			}
			finally
			{
				this.CleanupHandshake();
			}
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x0011A3A4 File Offset: 0x001185A4
		protected internal void ProcessRecord(byte protocol, byte[] buf, int off, int len)
		{
			switch (protocol)
			{
			case 20:
				this.ProcessChangeCipherSpec(buf, off, len);
				return;
			case 21:
				this.mAlertQueue.AddData(buf, off, len);
				this.ProcessAlertQueue();
				return;
			case 22:
			{
				if (this.mHandshakeQueue.Available > 0)
				{
					this.mHandshakeQueue.AddData(buf, off, len);
					this.ProcessHandshakeQueue(this.mHandshakeQueue);
					return;
				}
				ByteQueue byteQueue = new ByteQueue(buf, off, len);
				this.ProcessHandshakeQueue(byteQueue);
				int available = byteQueue.Available;
				if (available > 0)
				{
					this.mHandshakeQueue.AddData(buf, off + len - available, available);
					return;
				}
				return;
			}
			case 23:
				if (!this.mAppDataReady)
				{
					throw new TlsFatalAlert(10);
				}
				this.mApplicationDataQueue.AddData(buf, off, len);
				this.ProcessApplicationDataQueue();
				return;
			default:
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x0011A47C File Offset: 0x0011867C
		private void ProcessHandshakeQueue(ByteQueue queue)
		{
			while (queue.Available >= 4)
			{
				byte[] buf = new byte[4];
				queue.Read(buf, 0, 4, 0);
				byte b = TlsUtilities.ReadUint8(buf, 0);
				int num = TlsUtilities.ReadUint24(buf, 1);
				int num2 = 4 + num;
				if (queue.Available < num2)
				{
					break;
				}
				if (b != 0)
				{
					if (20 == b)
					{
						this.CheckReceivedChangeCipherSpec(true);
						TlsContext context = this.Context;
						if (this.mExpectedVerifyData == null && context.SecurityParameters.MasterSecret != null)
						{
							this.mExpectedVerifyData = this.CreateVerifyData(!context.IsServer);
						}
					}
					else
					{
						this.CheckReceivedChangeCipherSpec(this.mConnectionState == 16);
					}
					queue.CopyTo(this.mRecordStream.HandshakeHashUpdater, num2);
				}
				queue.RemoveData(4);
				MemoryStream buf2 = queue.ReadFrom(num);
				this.HandleHandshakeMessage(b, buf2);
			}
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x00002B75 File Offset: 0x00000D75
		private void ProcessApplicationDataQueue()
		{
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x0011A54C File Offset: 0x0011874C
		private void ProcessAlertQueue()
		{
			while (this.mAlertQueue.Available >= 2)
			{
				byte[] array = this.mAlertQueue.RemoveData(2, 0);
				byte alertLevel = array[0];
				byte alertDescription = array[1];
				this.HandleAlertMessage(alertLevel, alertDescription);
			}
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x0011A588 File Offset: 0x00118788
		private void ProcessChangeCipherSpec(byte[] buf, int off, int len)
		{
			for (int i = 0; i < len; i++)
			{
				if (TlsUtilities.ReadUint8(buf, off + i) != 1)
				{
					throw new TlsFatalAlert(50);
				}
				if (this.mReceivedChangeCipherSpec || this.mAlertQueue.Available > 0 || this.mHandshakeQueue.Available > 0)
				{
					throw new TlsFatalAlert(10);
				}
				this.mRecordStream.ReceivedReadCipherSpec();
				this.mReceivedChangeCipherSpec = true;
				this.HandleChangeCipherSpecMessage();
			}
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x0011A5F9 File Offset: 0x001187F9
		protected internal virtual int ApplicationDataAvailable()
		{
			return this.mApplicationDataQueue.Available;
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x0011A608 File Offset: 0x00118808
		protected internal virtual int ReadApplicationData(byte[] buf, int offset, int len)
		{
			if (len < 1)
			{
				return 0;
			}
			while (this.mApplicationDataQueue.Available == 0)
			{
				if (this.mClosed)
				{
					if (this.mFailedWithError)
					{
						throw new IOException("Cannot read application data on failed TLS connection");
					}
					if (!this.mAppDataReady)
					{
						throw new InvalidOperationException("Cannot read application data until initial handshake completed.");
					}
					return 0;
				}
				else
				{
					this.SafeReadRecord();
				}
			}
			len = Math.Min(len, this.mApplicationDataQueue.Available);
			this.mApplicationDataQueue.RemoveData(buf, offset, len, 0);
			return len;
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x0011A688 File Offset: 0x00118888
		protected virtual void SafeCheckRecordHeader(byte[] recordHeader)
		{
			try
			{
				this.mRecordStream.CheckRecordHeader(recordHeader);
			}
			catch (TlsFatalAlert tlsFatalAlert)
			{
				this.HandleException(tlsFatalAlert.AlertDescription, "Failed to read record", tlsFatalAlert);
				throw tlsFatalAlert;
			}
			catch (IOException ex)
			{
				this.HandleException(80, "Failed to read record", ex);
				throw ex;
			}
			catch (Exception ex2)
			{
				this.HandleException(80, "Failed to read record", ex2);
				throw new TlsFatalAlert(80, ex2);
			}
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x0011A70C File Offset: 0x0011890C
		protected virtual void SafeReadRecord()
		{
			try
			{
				if (this.mRecordStream.ReadRecord())
				{
					return;
				}
				if (!this.mAppDataReady)
				{
					throw new TlsFatalAlert(40);
				}
			}
			catch (TlsFatalAlertReceived tlsFatalAlertReceived)
			{
				throw tlsFatalAlertReceived;
			}
			catch (TlsFatalAlert tlsFatalAlert)
			{
				this.HandleException(tlsFatalAlert.AlertDescription, "Failed to read record", tlsFatalAlert);
				throw tlsFatalAlert;
			}
			catch (IOException ex)
			{
				this.HandleException(80, "Failed to read record", ex);
				throw ex;
			}
			catch (Exception ex2)
			{
				this.HandleException(80, "Failed to read record", ex2);
				throw new TlsFatalAlert(80, ex2);
			}
			this.HandleFailure();
			throw new TlsNoCloseNotifyException();
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x0011A7BC File Offset: 0x001189BC
		protected virtual void SafeWriteRecord(byte type, byte[] buf, int offset, int len)
		{
			try
			{
				this.mRecordStream.WriteRecord(type, buf, offset, len);
			}
			catch (TlsFatalAlert tlsFatalAlert)
			{
				this.HandleException(tlsFatalAlert.AlertDescription, "Failed to write record", tlsFatalAlert);
				throw tlsFatalAlert;
			}
			catch (IOException ex)
			{
				this.HandleException(80, "Failed to write record", ex);
				throw ex;
			}
			catch (Exception ex2)
			{
				this.HandleException(80, "Failed to write record", ex2);
				throw new TlsFatalAlert(80, ex2);
			}
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x0011A844 File Offset: 0x00118A44
		protected internal virtual void WriteData(byte[] buf, int offset, int len)
		{
			if (this.mClosed)
			{
				throw new IOException("Cannot write application data on closed/failed TLS connection");
			}
			while (len > 0)
			{
				if (this.mAppDataSplitEnabled)
				{
					switch (this.mAppDataSplitMode)
					{
					case 1:
						this.SafeWriteRecord(23, TlsUtilities.EmptyBytes, 0, 0);
						goto IL_7F;
					case 2:
						this.mAppDataSplitEnabled = false;
						this.SafeWriteRecord(23, TlsUtilities.EmptyBytes, 0, 0);
						goto IL_7F;
					}
					this.SafeWriteRecord(23, buf, offset, 1);
					offset++;
					len--;
				}
				IL_7F:
				if (len > 0)
				{
					int num = Math.Min(len, this.mRecordStream.GetPlaintextLimit());
					this.SafeWriteRecord(23, buf, offset, num);
					offset += num;
					len -= num;
				}
			}
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x0011A902 File Offset: 0x00118B02
		protected virtual void SetAppDataSplitMode(int appDataSplitMode)
		{
			if (appDataSplitMode < 0 || appDataSplitMode > 2)
			{
				throw new ArgumentException("Illegal appDataSplitMode mode: " + appDataSplitMode, "appDataSplitMode");
			}
			this.mAppDataSplitMode = appDataSplitMode;
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x0011A930 File Offset: 0x00118B30
		protected virtual void WriteHandshakeMessage(byte[] buf, int off, int len)
		{
			if (len < 4)
			{
				throw new TlsFatalAlert(80);
			}
			if (TlsUtilities.ReadUint8(buf, off) != 0)
			{
				this.mRecordStream.HandshakeHashUpdater.Write(buf, off, len);
			}
			int num = 0;
			do
			{
				int num2 = Math.Min(len - num, this.mRecordStream.GetPlaintextLimit());
				this.SafeWriteRecord(22, buf, off + num, num2);
				num += num2;
			}
			while (num < len);
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06002C3C RID: 11324 RVA: 0x0011A990 File Offset: 0x00118B90
		public virtual Stream Stream
		{
			get
			{
				if (!this.mBlocking)
				{
					throw new InvalidOperationException("Cannot use Stream in non-blocking mode! Use OfferInput()/OfferOutput() instead.");
				}
				return this.mTlsStream;
			}
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x0011A9AC File Offset: 0x00118BAC
		public virtual void CloseInput()
		{
			if (this.mBlocking)
			{
				throw new InvalidOperationException("Cannot use CloseInput() in blocking mode!");
			}
			if (this.mClosed)
			{
				return;
			}
			if (this.mInputBuffers.Available > 0)
			{
				throw new EndOfStreamException();
			}
			if (!this.mAppDataReady)
			{
				throw new TlsFatalAlert(40);
			}
			throw new TlsNoCloseNotifyException();
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x0011AA04 File Offset: 0x00118C04
		public virtual void OfferInput(byte[] input)
		{
			if (this.mBlocking)
			{
				throw new InvalidOperationException("Cannot use OfferInput() in blocking mode! Use Stream instead.");
			}
			if (this.mClosed)
			{
				throw new IOException("Connection is closed, cannot accept any more input");
			}
			this.mInputBuffers.Write(input);
			while (this.mInputBuffers.Available >= 5)
			{
				byte[] array = new byte[5];
				this.mInputBuffers.Peek(array);
				int num = TlsUtilities.ReadUint16(array, 3) + 5;
				if (this.mInputBuffers.Available < num)
				{
					this.SafeCheckRecordHeader(array);
					return;
				}
				this.SafeReadRecord();
				if (this.mClosed)
				{
					if (this.mConnectionState != 16)
					{
						throw new TlsFatalAlert(80);
					}
					break;
				}
			}
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x0011AAAB File Offset: 0x00118CAB
		public virtual int GetAvailableInputBytes()
		{
			if (this.mBlocking)
			{
				throw new InvalidOperationException("Cannot use GetAvailableInputBytes() in blocking mode! Use ApplicationDataAvailable() instead.");
			}
			return this.ApplicationDataAvailable();
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x0011AAC6 File Offset: 0x00118CC6
		public virtual int ReadInput(byte[] buffer, int offset, int length)
		{
			if (this.mBlocking)
			{
				throw new InvalidOperationException("Cannot use ReadInput() in blocking mode! Use Stream instead.");
			}
			return this.ReadApplicationData(buffer, offset, Math.Min(length, this.ApplicationDataAvailable()));
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x0011AAEF File Offset: 0x00118CEF
		public virtual void OfferOutput(byte[] buffer, int offset, int length)
		{
			if (this.mBlocking)
			{
				throw new InvalidOperationException("Cannot use OfferOutput() in blocking mode! Use Stream instead.");
			}
			if (!this.mAppDataReady)
			{
				throw new IOException("Application data cannot be sent until the handshake is complete!");
			}
			this.WriteData(buffer, offset, length);
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x0011AB22 File Offset: 0x00118D22
		public virtual int GetAvailableOutputBytes()
		{
			if (this.mBlocking)
			{
				throw new InvalidOperationException("Cannot use GetAvailableOutputBytes() in blocking mode! Use Stream instead.");
			}
			return this.mOutputBuffer.Available;
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x0011AB42 File Offset: 0x00118D42
		public virtual int ReadOutput(byte[] buffer, int offset, int length)
		{
			if (this.mBlocking)
			{
				throw new InvalidOperationException("Cannot use ReadOutput() in blocking mode! Use Stream instead.");
			}
			return this.mOutputBuffer.Read(buffer, offset, length);
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x0011AB65 File Offset: 0x00118D65
		protected virtual void InvalidateSession()
		{
			if (this.mSessionParameters != null)
			{
				this.mSessionParameters.Clear();
				this.mSessionParameters = null;
			}
			if (this.mTlsSession != null)
			{
				this.mTlsSession.Invalidate();
				this.mTlsSession = null;
			}
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x0011AB9C File Offset: 0x00118D9C
		protected virtual void ProcessFinishedMessage(MemoryStream buf)
		{
			if (this.mExpectedVerifyData == null)
			{
				throw new TlsFatalAlert(80);
			}
			byte[] b = TlsUtilities.ReadFully(this.mExpectedVerifyData.Length, buf);
			TlsProtocol.AssertEmpty(buf);
			if (!Arrays.ConstantTimeAreEqual(this.mExpectedVerifyData, b))
			{
				throw new TlsFatalAlert(51);
			}
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x0011ABE4 File Offset: 0x00118DE4
		protected virtual void RaiseAlertFatal(byte alertDescription, string message, Exception cause)
		{
			this.Peer.NotifyAlertRaised(2, alertDescription, message, cause);
			byte[] plaintext = new byte[]
			{
				2,
				alertDescription
			};
			try
			{
				this.mRecordStream.WriteRecord(21, plaintext, 0, 2);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x0011AC34 File Offset: 0x00118E34
		protected virtual void RaiseAlertWarning(byte alertDescription, string message)
		{
			this.Peer.NotifyAlertRaised(1, alertDescription, message, null);
			byte[] buf = new byte[]
			{
				1,
				alertDescription
			};
			this.SafeWriteRecord(21, buf, 0, 2);
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x0011AC6C File Offset: 0x00118E6C
		protected virtual void SendCertificateMessage(Certificate certificate)
		{
			if (certificate == null)
			{
				certificate = Certificate.EmptyChain;
			}
			if (certificate.IsEmpty && !this.Context.IsServer)
			{
				ProtocolVersion serverVersion = this.Context.ServerVersion;
				if (serverVersion.IsSsl)
				{
					string message = serverVersion.ToString() + " client didn't provide credentials";
					this.RaiseAlertWarning(41, message);
					return;
				}
			}
			TlsProtocol.HandshakeMessage handshakeMessage = new TlsProtocol.HandshakeMessage(11);
			certificate.Encode(handshakeMessage);
			handshakeMessage.WriteToRecordStream(this);
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x0011ACE0 File Offset: 0x00118EE0
		protected virtual void SendChangeCipherSpecMessage()
		{
			byte[] array = new byte[]
			{
				1
			};
			this.SafeWriteRecord(20, array, 0, array.Length);
			this.mRecordStream.SentWriteCipherSpec();
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x0011AD10 File Offset: 0x00118F10
		protected virtual void SendFinishedMessage()
		{
			byte[] array = this.CreateVerifyData(this.Context.IsServer);
			TlsProtocol.HandshakeMessage handshakeMessage = new TlsProtocol.HandshakeMessage(20, array.Length);
			handshakeMessage.Write(array, 0, array.Length);
			handshakeMessage.WriteToRecordStream(this);
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x0011AD4A File Offset: 0x00118F4A
		protected virtual void SendSupplementalDataMessage(IList supplementalData)
		{
			TlsProtocol.HandshakeMessage handshakeMessage = new TlsProtocol.HandshakeMessage(23);
			TlsProtocol.WriteSupplementalData(handshakeMessage, supplementalData);
			handshakeMessage.WriteToRecordStream(this);
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x0011AD60 File Offset: 0x00118F60
		protected virtual byte[] CreateVerifyData(bool isServer)
		{
			TlsContext context = this.Context;
			string asciiLabel = isServer ? "server finished" : "client finished";
			byte[] sslSender = isServer ? TlsUtilities.SSL_SERVER : TlsUtilities.SSL_CLIENT;
			byte[] currentPrfHash = TlsProtocol.GetCurrentPrfHash(context, this.mRecordStream.HandshakeHash, sslSender);
			return TlsUtilities.CalculateVerifyData(context, asciiLabel, currentPrfHash);
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x0011ADAD File Offset: 0x00118FAD
		public virtual void Close()
		{
			this.HandleClose(true);
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x0011ADB6 File Offset: 0x00118FB6
		protected internal virtual void Flush()
		{
			this.mRecordStream.Flush();
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06002C4F RID: 11343 RVA: 0x0011ADC3 File Offset: 0x00118FC3
		public virtual bool IsClosed
		{
			get
			{
				return this.mClosed;
			}
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x0011ADD0 File Offset: 0x00118FD0
		protected virtual short ProcessMaxFragmentLengthExtension(IDictionary clientExtensions, IDictionary serverExtensions, byte alertDescription)
		{
			short maxFragmentLengthExtension = TlsExtensionsUtilities.GetMaxFragmentLengthExtension(serverExtensions);
			if (maxFragmentLengthExtension >= 0 && (!MaxFragmentLength.IsValid((byte)maxFragmentLengthExtension) || (!this.mResumedSession && maxFragmentLengthExtension != TlsExtensionsUtilities.GetMaxFragmentLengthExtension(clientExtensions))))
			{
				throw new TlsFatalAlert(alertDescription);
			}
			return maxFragmentLengthExtension;
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x0011AE0A File Offset: 0x0011900A
		protected virtual void RefuseRenegotiation()
		{
			if (TlsUtilities.IsSsl(this.Context))
			{
				throw new TlsFatalAlert(40);
			}
			this.RaiseAlertWarning(100, "Renegotiation not supported");
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x0011AE2E File Offset: 0x0011902E
		protected internal static void AssertEmpty(MemoryStream buf)
		{
			if (buf.Position < buf.Length)
			{
				throw new TlsFatalAlert(50);
			}
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x0011AE48 File Offset: 0x00119048
		protected internal static byte[] CreateRandomBlock(bool useGmtUnixTime, IRandomGenerator randomGenerator)
		{
			byte[] array = new byte[32];
			randomGenerator.NextBytes(array);
			if (useGmtUnixTime)
			{
				TlsUtilities.WriteGmtUnixTime(array, 0);
			}
			return array;
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x0011AE6F File Offset: 0x0011906F
		protected internal static byte[] CreateRenegotiationInfo(byte[] renegotiated_connection)
		{
			return TlsUtilities.EncodeOpaque8(renegotiated_connection);
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x0011AE78 File Offset: 0x00119078
		protected internal static void EstablishMasterSecret(TlsContext context, TlsKeyExchange keyExchange)
		{
			byte[] array = keyExchange.GeneratePremasterSecret();
			try
			{
				context.SecurityParameters.masterSecret = TlsUtilities.CalculateMasterSecret(context, array);
			}
			finally
			{
				if (array != null)
				{
					Arrays.Fill(array, 0);
				}
			}
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x0011AEBC File Offset: 0x001190BC
		protected internal static byte[] GetCurrentPrfHash(TlsContext context, TlsHandshakeHash handshakeHash, byte[] sslSender)
		{
			IDigest digest = handshakeHash.ForkPrfHash();
			if (sslSender != null && TlsUtilities.IsSsl(context))
			{
				digest.BlockUpdate(sslSender, 0, sslSender.Length);
			}
			return DigestUtilities.DoFinal(digest);
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x0011AEEC File Offset: 0x001190EC
		protected internal static IDictionary ReadExtensions(MemoryStream input)
		{
			if (input.Position >= input.Length)
			{
				return null;
			}
			byte[] buffer = TlsUtilities.ReadOpaque16(input);
			TlsProtocol.AssertEmpty(input);
			MemoryStream memoryStream = new MemoryStream(buffer, false);
			IDictionary dictionary = Platform.CreateHashtable();
			while (memoryStream.Position < memoryStream.Length)
			{
				int num = TlsUtilities.ReadUint16(memoryStream);
				byte[] value = TlsUtilities.ReadOpaque16(memoryStream);
				if (dictionary.Contains(num))
				{
					throw new TlsFatalAlert(47);
				}
				dictionary.Add(num, value);
			}
			return dictionary;
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x0011AF64 File Offset: 0x00119164
		protected internal static IList ReadSupplementalDataMessage(MemoryStream input)
		{
			byte[] buffer = TlsUtilities.ReadOpaque24(input);
			TlsProtocol.AssertEmpty(input);
			MemoryStream memoryStream = new MemoryStream(buffer, false);
			IList list = Platform.CreateArrayList();
			while (memoryStream.Position < memoryStream.Length)
			{
				int dataType = TlsUtilities.ReadUint16(memoryStream);
				byte[] data = TlsUtilities.ReadOpaque16(memoryStream);
				list.Add(new SupplementalDataEntry(dataType, data));
			}
			return list;
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x0011AFB7 File Offset: 0x001191B7
		protected internal static void WriteExtensions(Stream output, IDictionary extensions)
		{
			MemoryStream memoryStream = new MemoryStream();
			TlsProtocol.WriteSelectedExtensions(memoryStream, extensions, true);
			TlsProtocol.WriteSelectedExtensions(memoryStream, extensions, false);
			TlsUtilities.WriteOpaque16(memoryStream.ToArray(), output);
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x0011AFDC File Offset: 0x001191DC
		protected internal static void WriteSelectedExtensions(Stream output, IDictionary extensions, bool selectEmpty)
		{
			foreach (object obj in extensions.Keys)
			{
				int num = (int)obj;
				byte[] array = (byte[])extensions[num];
				if (selectEmpty == (array.Length == 0))
				{
					TlsUtilities.CheckUint16(num);
					TlsUtilities.WriteUint16(num, output);
					TlsUtilities.WriteOpaque16(array, output);
				}
			}
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x0011B05C File Offset: 0x0011925C
		protected internal static void WriteSupplementalData(Stream output, IList supplementalData)
		{
			MemoryStream memoryStream = new MemoryStream();
			foreach (object obj in supplementalData)
			{
				SupplementalDataEntry supplementalDataEntry = (SupplementalDataEntry)obj;
				int dataType = supplementalDataEntry.DataType;
				TlsUtilities.CheckUint16(dataType);
				TlsUtilities.WriteUint16(dataType, memoryStream);
				TlsUtilities.WriteOpaque16(supplementalDataEntry.Data, memoryStream);
			}
			TlsUtilities.WriteOpaque24(memoryStream.ToArray(), output);
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x0011B0D8 File Offset: 0x001192D8
		protected internal static int GetPrfAlgorithm(TlsContext context, int ciphersuite)
		{
			bool flag = TlsUtilities.IsTlsV12(context);
			if (ciphersuite <= 197)
			{
				if (ciphersuite - 59 > 5 && ciphersuite - 103 > 6)
				{
					switch (ciphersuite)
					{
					case 156:
					case 158:
					case 160:
					case 162:
					case 164:
					case 166:
					case 168:
					case 170:
					case 172:
					case 186:
					case 187:
					case 188:
					case 189:
					case 190:
					case 191:
					case 192:
					case 193:
					case 194:
					case 195:
					case 196:
					case 197:
						break;
					case 157:
					case 159:
					case 161:
					case 163:
					case 165:
					case 167:
					case 169:
					case 171:
					case 173:
						goto IL_357;
					case 174:
					case 176:
					case 178:
					case 180:
					case 182:
					case 184:
						goto IL_36B;
					case 175:
					case 177:
					case 179:
					case 181:
					case 183:
					case 185:
						goto IL_364;
					default:
						goto IL_36B;
					}
				}
			}
			else if (ciphersuite <= 52398)
			{
				switch (ciphersuite)
				{
				case 49187:
				case 49189:
				case 49191:
				case 49193:
				case 49195:
				case 49197:
				case 49199:
				case 49201:
				case 49266:
				case 49268:
				case 49270:
				case 49272:
				case 49274:
				case 49276:
				case 49278:
				case 49280:
				case 49282:
				case 49284:
				case 49286:
				case 49288:
				case 49290:
				case 49292:
				case 49294:
				case 49296:
				case 49298:
				case 49308:
				case 49309:
				case 49310:
				case 49311:
				case 49312:
				case 49313:
				case 49314:
				case 49315:
				case 49316:
				case 49317:
				case 49318:
				case 49319:
				case 49320:
				case 49321:
				case 49322:
				case 49323:
				case 49324:
				case 49325:
				case 49326:
				case 49327:
					break;
				case 49188:
				case 49190:
				case 49192:
				case 49194:
				case 49196:
				case 49198:
				case 49200:
				case 49202:
				case 49267:
				case 49269:
				case 49271:
				case 49273:
				case 49275:
				case 49277:
				case 49279:
				case 49281:
				case 49283:
				case 49285:
				case 49287:
				case 49289:
				case 49291:
				case 49293:
				case 49295:
				case 49297:
				case 49299:
					goto IL_357;
				case 49203:
				case 49204:
				case 49205:
				case 49206:
				case 49207:
				case 49209:
				case 49210:
				case 49212:
				case 49213:
				case 49214:
				case 49215:
				case 49216:
				case 49217:
				case 49218:
				case 49219:
				case 49220:
				case 49221:
				case 49222:
				case 49223:
				case 49224:
				case 49225:
				case 49226:
				case 49227:
				case 49228:
				case 49229:
				case 49230:
				case 49231:
				case 49232:
				case 49233:
				case 49234:
				case 49235:
				case 49236:
				case 49237:
				case 49238:
				case 49239:
				case 49240:
				case 49241:
				case 49242:
				case 49243:
				case 49244:
				case 49245:
				case 49246:
				case 49247:
				case 49248:
				case 49249:
				case 49250:
				case 49251:
				case 49252:
				case 49253:
				case 49254:
				case 49255:
				case 49256:
				case 49257:
				case 49258:
				case 49259:
				case 49260:
				case 49261:
				case 49262:
				case 49263:
				case 49264:
				case 49265:
				case 49300:
				case 49302:
				case 49304:
				case 49306:
					goto IL_36B;
				case 49208:
				case 49211:
				case 49301:
				case 49303:
				case 49305:
				case 49307:
					goto IL_364;
				default:
					if (ciphersuite - 52392 > 6)
					{
						goto IL_36B;
					}
					break;
				}
			}
			else if (ciphersuite - 65280 > 5 && ciphersuite - 65296 > 5)
			{
				goto IL_36B;
			}
			if (flag)
			{
				return 1;
			}
			throw new TlsFatalAlert(47);
			IL_357:
			if (flag)
			{
				return 2;
			}
			throw new TlsFatalAlert(47);
			IL_364:
			if (flag)
			{
				return 2;
			}
			return 0;
			IL_36B:
			if (flag)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x04001D12 RID: 7442
		protected const short CS_START = 0;

		// Token: 0x04001D13 RID: 7443
		protected const short CS_CLIENT_HELLO = 1;

		// Token: 0x04001D14 RID: 7444
		protected const short CS_SERVER_HELLO = 2;

		// Token: 0x04001D15 RID: 7445
		protected const short CS_SERVER_SUPPLEMENTAL_DATA = 3;

		// Token: 0x04001D16 RID: 7446
		protected const short CS_SERVER_CERTIFICATE = 4;

		// Token: 0x04001D17 RID: 7447
		protected const short CS_CERTIFICATE_STATUS = 5;

		// Token: 0x04001D18 RID: 7448
		protected const short CS_SERVER_KEY_EXCHANGE = 6;

		// Token: 0x04001D19 RID: 7449
		protected const short CS_CERTIFICATE_REQUEST = 7;

		// Token: 0x04001D1A RID: 7450
		protected const short CS_SERVER_HELLO_DONE = 8;

		// Token: 0x04001D1B RID: 7451
		protected const short CS_CLIENT_SUPPLEMENTAL_DATA = 9;

		// Token: 0x04001D1C RID: 7452
		protected const short CS_CLIENT_CERTIFICATE = 10;

		// Token: 0x04001D1D RID: 7453
		protected const short CS_CLIENT_KEY_EXCHANGE = 11;

		// Token: 0x04001D1E RID: 7454
		protected const short CS_CERTIFICATE_VERIFY = 12;

		// Token: 0x04001D1F RID: 7455
		protected const short CS_CLIENT_FINISHED = 13;

		// Token: 0x04001D20 RID: 7456
		protected const short CS_SERVER_SESSION_TICKET = 14;

		// Token: 0x04001D21 RID: 7457
		protected const short CS_SERVER_FINISHED = 15;

		// Token: 0x04001D22 RID: 7458
		protected const short CS_END = 16;

		// Token: 0x04001D23 RID: 7459
		protected const short ADS_MODE_1_Nsub1 = 0;

		// Token: 0x04001D24 RID: 7460
		protected const short ADS_MODE_0_N = 1;

		// Token: 0x04001D25 RID: 7461
		protected const short ADS_MODE_0_N_FIRSTONLY = 2;

		// Token: 0x04001D26 RID: 7462
		private ByteQueue mApplicationDataQueue;

		// Token: 0x04001D27 RID: 7463
		private ByteQueue mAlertQueue;

		// Token: 0x04001D28 RID: 7464
		private ByteQueue mHandshakeQueue;

		// Token: 0x04001D29 RID: 7465
		internal RecordStream mRecordStream;

		// Token: 0x04001D2A RID: 7466
		protected SecureRandom mSecureRandom;

		// Token: 0x04001D2B RID: 7467
		private TlsStream mTlsStream;

		// Token: 0x04001D2C RID: 7468
		private volatile bool mClosed;

		// Token: 0x04001D2D RID: 7469
		private volatile bool mFailedWithError;

		// Token: 0x04001D2E RID: 7470
		private volatile bool mAppDataReady;

		// Token: 0x04001D2F RID: 7471
		private volatile bool mAppDataSplitEnabled;

		// Token: 0x04001D30 RID: 7472
		private volatile int mAppDataSplitMode;

		// Token: 0x04001D31 RID: 7473
		private byte[] mExpectedVerifyData;

		// Token: 0x04001D32 RID: 7474
		protected TlsSession mTlsSession;

		// Token: 0x04001D33 RID: 7475
		protected SessionParameters mSessionParameters;

		// Token: 0x04001D34 RID: 7476
		protected SecurityParameters mSecurityParameters;

		// Token: 0x04001D35 RID: 7477
		protected Certificate mPeerCertificate;

		// Token: 0x04001D36 RID: 7478
		protected int[] mOfferedCipherSuites;

		// Token: 0x04001D37 RID: 7479
		protected byte[] mOfferedCompressionMethods;

		// Token: 0x04001D38 RID: 7480
		protected IDictionary mClientExtensions;

		// Token: 0x04001D39 RID: 7481
		protected IDictionary mServerExtensions;

		// Token: 0x04001D3A RID: 7482
		protected short mConnectionState;

		// Token: 0x04001D3B RID: 7483
		protected bool mResumedSession;

		// Token: 0x04001D3C RID: 7484
		protected bool mReceivedChangeCipherSpec;

		// Token: 0x04001D3D RID: 7485
		protected bool mSecureRenegotiation;

		// Token: 0x04001D3E RID: 7486
		protected bool mAllowCertificateStatus;

		// Token: 0x04001D3F RID: 7487
		protected bool mExpectSessionTicket;

		// Token: 0x04001D40 RID: 7488
		protected bool mBlocking;

		// Token: 0x04001D41 RID: 7489
		protected ByteQueueStream mInputBuffers;

		// Token: 0x04001D42 RID: 7490
		protected ByteQueueStream mOutputBuffer;

		// Token: 0x02000924 RID: 2340
		internal class HandshakeMessage : MemoryStream
		{
			// Token: 0x06004E2E RID: 20014 RVA: 0x001B32B3 File Offset: 0x001B14B3
			internal HandshakeMessage(byte handshakeType) : this(handshakeType, 60)
			{
			}

			// Token: 0x06004E2F RID: 20015 RVA: 0x001B32BE File Offset: 0x001B14BE
			internal HandshakeMessage(byte handshakeType, int length) : base(length + 4)
			{
				TlsUtilities.WriteUint8(handshakeType, this);
				TlsUtilities.WriteUint24(0, this);
			}

			// Token: 0x06004E30 RID: 20016 RVA: 0x000B961E File Offset: 0x000B781E
			internal void Write(byte[] data)
			{
				this.Write(data, 0, data.Length);
			}

			// Token: 0x06004E31 RID: 20017 RVA: 0x001B32D8 File Offset: 0x001B14D8
			internal void WriteToRecordStream(TlsProtocol protocol)
			{
				long num = this.Length - 4L;
				TlsUtilities.CheckUint24(num);
				this.Position = 1L;
				TlsUtilities.WriteUint24((int)num, this);
				byte[] buffer = this.GetBuffer();
				int len = (int)this.Length;
				protocol.WriteHandshakeMessage(buffer, 0, len);
				Platform.Dispose(this);
			}
		}
	}
}
