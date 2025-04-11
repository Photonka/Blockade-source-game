using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement.Srp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000472 RID: 1138
	public class TlsSrpKeyExchange : AbstractTlsKeyExchange
	{
		// Token: 0x06002CCA RID: 11466 RVA: 0x0011CC8C File Offset: 0x0011AE8C
		protected static TlsSigner CreateSigner(int keyExchange)
		{
			switch (keyExchange)
			{
			case 21:
				return null;
			case 22:
				return new TlsDssSigner();
			case 23:
				return new TlsRsaSigner();
			default:
				throw new ArgumentException("unsupported key exchange algorithm");
			}
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x0011CCBD File Offset: 0x0011AEBD
		[Obsolete("Use constructor taking an explicit 'groupVerifier' argument")]
		public TlsSrpKeyExchange(int keyExchange, IList supportedSignatureAlgorithms, byte[] identity, byte[] password) : this(keyExchange, supportedSignatureAlgorithms, new DefaultTlsSrpGroupVerifier(), identity, password)
		{
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x0011CCCF File Offset: 0x0011AECF
		public TlsSrpKeyExchange(int keyExchange, IList supportedSignatureAlgorithms, TlsSrpGroupVerifier groupVerifier, byte[] identity, byte[] password) : base(keyExchange, supportedSignatureAlgorithms)
		{
			this.mTlsSigner = TlsSrpKeyExchange.CreateSigner(keyExchange);
			this.mGroupVerifier = groupVerifier;
			this.mIdentity = identity;
			this.mPassword = password;
			this.mSrpClient = new Srp6Client();
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x0011CD08 File Offset: 0x0011AF08
		public TlsSrpKeyExchange(int keyExchange, IList supportedSignatureAlgorithms, byte[] identity, TlsSrpLoginParameters loginParameters) : base(keyExchange, supportedSignatureAlgorithms)
		{
			this.mTlsSigner = TlsSrpKeyExchange.CreateSigner(keyExchange);
			this.mIdentity = identity;
			this.mSrpServer = new Srp6Server();
			this.mSrpGroup = loginParameters.Group;
			this.mSrpVerifier = loginParameters.Verifier;
			this.mSrpSalt = loginParameters.Salt;
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x0011CD62 File Offset: 0x0011AF62
		public override void Init(TlsContext context)
		{
			base.Init(context);
			if (this.mTlsSigner != null)
			{
				this.mTlsSigner.Init(context);
			}
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x0011CD7F File Offset: 0x0011AF7F
		public override void SkipServerCredentials()
		{
			if (this.mTlsSigner != null)
			{
				throw new TlsFatalAlert(10);
			}
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x0011CD94 File Offset: 0x0011AF94
		public override void ProcessServerCertificate(Certificate serverCertificate)
		{
			if (this.mTlsSigner == null)
			{
				throw new TlsFatalAlert(10);
			}
			if (serverCertificate.IsEmpty)
			{
				throw new TlsFatalAlert(42);
			}
			X509CertificateStructure certificateAt = serverCertificate.GetCertificateAt(0);
			SubjectPublicKeyInfo subjectPublicKeyInfo = certificateAt.SubjectPublicKeyInfo;
			try
			{
				this.mServerPublicKey = PublicKeyFactory.CreateKey(subjectPublicKeyInfo);
			}
			catch (Exception alertCause)
			{
				throw new TlsFatalAlert(43, alertCause);
			}
			if (!this.mTlsSigner.IsValidPublicKey(this.mServerPublicKey))
			{
				throw new TlsFatalAlert(46);
			}
			TlsUtilities.ValidateKeyUsage(certificateAt, 128);
			base.ProcessServerCertificate(serverCertificate);
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x0011CE28 File Offset: 0x0011B028
		public override void ProcessServerCredentials(TlsCredentials serverCredentials)
		{
			if (this.mKeyExchange == 21 || !(serverCredentials is TlsSignerCredentials))
			{
				throw new TlsFatalAlert(80);
			}
			this.ProcessServerCertificate(serverCredentials.Certificate);
			this.mServerCredentials = (TlsSignerCredentials)serverCredentials;
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06002CD2 RID: 11474 RVA: 0x0006CF70 File Offset: 0x0006B170
		public override bool RequiresServerKeyExchange
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x0011CE5C File Offset: 0x0011B05C
		public override byte[] GenerateServerKeyExchange()
		{
			this.mSrpServer.Init(this.mSrpGroup, this.mSrpVerifier, TlsUtilities.CreateHash(2), this.mContext.SecureRandom);
			BigInteger b = this.mSrpServer.GenerateServerCredentials();
			ServerSrpParams serverSrpParams = new ServerSrpParams(this.mSrpGroup.N, this.mSrpGroup.G, this.mSrpSalt, b);
			DigestInputBuffer digestInputBuffer = new DigestInputBuffer();
			serverSrpParams.Encode(digestInputBuffer);
			if (this.mServerCredentials != null)
			{
				SignatureAndHashAlgorithm signatureAndHashAlgorithm = TlsUtilities.GetSignatureAndHashAlgorithm(this.mContext, this.mServerCredentials);
				IDigest digest = TlsUtilities.CreateHash(signatureAndHashAlgorithm);
				SecurityParameters securityParameters = this.mContext.SecurityParameters;
				digest.BlockUpdate(securityParameters.clientRandom, 0, securityParameters.clientRandom.Length);
				digest.BlockUpdate(securityParameters.serverRandom, 0, securityParameters.serverRandom.Length);
				digestInputBuffer.UpdateDigest(digest);
				byte[] array = new byte[digest.GetDigestSize()];
				digest.DoFinal(array, 0);
				byte[] signature = this.mServerCredentials.GenerateCertificateSignature(array);
				new DigitallySigned(signatureAndHashAlgorithm, signature).Encode(digestInputBuffer);
			}
			return digestInputBuffer.ToArray();
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x0011CF64 File Offset: 0x0011B164
		public override void ProcessServerKeyExchange(Stream input)
		{
			SecurityParameters securityParameters = this.mContext.SecurityParameters;
			SignerInputBuffer signerInputBuffer = null;
			Stream input2 = input;
			if (this.mTlsSigner != null)
			{
				signerInputBuffer = new SignerInputBuffer();
				input2 = new TeeInputStream(input, signerInputBuffer);
			}
			ServerSrpParams serverSrpParams = ServerSrpParams.Parse(input2);
			if (signerInputBuffer != null)
			{
				DigitallySigned digitallySigned = this.ParseSignature(input);
				ISigner signer = this.InitVerifyer(this.mTlsSigner, digitallySigned.Algorithm, securityParameters);
				signerInputBuffer.UpdateSigner(signer);
				if (!signer.VerifySignature(digitallySigned.Signature))
				{
					throw new TlsFatalAlert(51);
				}
			}
			this.mSrpGroup = new Srp6GroupParameters(serverSrpParams.N, serverSrpParams.G);
			if (!this.mGroupVerifier.Accept(this.mSrpGroup))
			{
				throw new TlsFatalAlert(71);
			}
			this.mSrpSalt = serverSrpParams.S;
			try
			{
				this.mSrpPeerCredentials = Srp6Utilities.ValidatePublicValue(this.mSrpGroup.N, serverSrpParams.B);
			}
			catch (CryptoException alertCause)
			{
				throw new TlsFatalAlert(47, alertCause);
			}
			this.mSrpClient.Init(this.mSrpGroup, TlsUtilities.CreateHash(2), this.mContext.SecureRandom);
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x0011B74D File Offset: 0x0011994D
		public override void ValidateCertificateRequest(CertificateRequest certificateRequest)
		{
			throw new TlsFatalAlert(10);
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x0010EA5F File Offset: 0x0010CC5F
		public override void ProcessClientCredentials(TlsCredentials clientCredentials)
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x0011D07C File Offset: 0x0011B27C
		public override void GenerateClientKeyExchange(Stream output)
		{
			TlsSrpUtilities.WriteSrpParameter(this.mSrpClient.GenerateClientCredentials(this.mSrpSalt, this.mIdentity, this.mPassword), output);
			this.mContext.SecurityParameters.srpIdentity = Arrays.Clone(this.mIdentity);
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x0011D0BC File Offset: 0x0011B2BC
		public override void ProcessClientKeyExchange(Stream input)
		{
			try
			{
				this.mSrpPeerCredentials = Srp6Utilities.ValidatePublicValue(this.mSrpGroup.N, TlsSrpUtilities.ReadSrpParameter(input));
			}
			catch (CryptoException alertCause)
			{
				throw new TlsFatalAlert(47, alertCause);
			}
			this.mContext.SecurityParameters.srpIdentity = Arrays.Clone(this.mIdentity);
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x0011D11C File Offset: 0x0011B31C
		public override byte[] GeneratePremasterSecret()
		{
			byte[] result;
			try
			{
				result = BigIntegers.AsUnsignedByteArray((this.mSrpServer != null) ? this.mSrpServer.CalculateSecret(this.mSrpPeerCredentials) : this.mSrpClient.CalculateSecret(this.mSrpPeerCredentials));
			}
			catch (CryptoException alertCause)
			{
				throw new TlsFatalAlert(47, alertCause);
			}
			return result;
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x0011D178 File Offset: 0x0011B378
		protected virtual ISigner InitVerifyer(TlsSigner tlsSigner, SignatureAndHashAlgorithm algorithm, SecurityParameters securityParameters)
		{
			ISigner signer = tlsSigner.CreateVerifyer(algorithm, this.mServerPublicKey);
			signer.BlockUpdate(securityParameters.clientRandom, 0, securityParameters.clientRandom.Length);
			signer.BlockUpdate(securityParameters.serverRandom, 0, securityParameters.serverRandom.Length);
			return signer;
		}

		// Token: 0x04001D62 RID: 7522
		protected TlsSigner mTlsSigner;

		// Token: 0x04001D63 RID: 7523
		protected TlsSrpGroupVerifier mGroupVerifier;

		// Token: 0x04001D64 RID: 7524
		protected byte[] mIdentity;

		// Token: 0x04001D65 RID: 7525
		protected byte[] mPassword;

		// Token: 0x04001D66 RID: 7526
		protected AsymmetricKeyParameter mServerPublicKey;

		// Token: 0x04001D67 RID: 7527
		protected Srp6GroupParameters mSrpGroup;

		// Token: 0x04001D68 RID: 7528
		protected Srp6Client mSrpClient;

		// Token: 0x04001D69 RID: 7529
		protected Srp6Server mSrpServer;

		// Token: 0x04001D6A RID: 7530
		protected BigInteger mSrpPeerCredentials;

		// Token: 0x04001D6B RID: 7531
		protected BigInteger mSrpVerifier;

		// Token: 0x04001D6C RID: 7532
		protected byte[] mSrpSalt;

		// Token: 0x04001D6D RID: 7533
		protected TlsSignerCredentials mServerCredentials;
	}
}
