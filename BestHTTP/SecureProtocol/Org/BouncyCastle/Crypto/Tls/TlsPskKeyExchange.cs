using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000464 RID: 1124
	public class TlsPskKeyExchange : AbstractTlsKeyExchange
	{
		// Token: 0x06002C65 RID: 11365 RVA: 0x0011B46C File Offset: 0x0011966C
		[Obsolete("Use constructor that takes a TlsDHVerifier")]
		public TlsPskKeyExchange(int keyExchange, IList supportedSignatureAlgorithms, TlsPskIdentity pskIdentity, TlsPskIdentityManager pskIdentityManager, DHParameters dhParameters, int[] namedCurves, byte[] clientECPointFormats, byte[] serverECPointFormats) : this(keyExchange, supportedSignatureAlgorithms, pskIdentity, pskIdentityManager, new DefaultTlsDHVerifier(), dhParameters, namedCurves, clientECPointFormats, serverECPointFormats)
		{
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x0011B494 File Offset: 0x00119694
		public TlsPskKeyExchange(int keyExchange, IList supportedSignatureAlgorithms, TlsPskIdentity pskIdentity, TlsPskIdentityManager pskIdentityManager, TlsDHVerifier dhVerifier, DHParameters dhParameters, int[] namedCurves, byte[] clientECPointFormats, byte[] serverECPointFormats) : base(keyExchange, supportedSignatureAlgorithms)
		{
			if (keyExchange - 13 > 2 && keyExchange != 24)
			{
				throw new InvalidOperationException("unsupported key exchange algorithm");
			}
			this.mPskIdentity = pskIdentity;
			this.mPskIdentityManager = pskIdentityManager;
			this.mDHVerifier = dhVerifier;
			this.mDHParameters = dhParameters;
			this.mNamedCurves = namedCurves;
			this.mClientECPointFormats = clientECPointFormats;
			this.mServerECPointFormats = serverECPointFormats;
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x0011B4F7 File Offset: 0x001196F7
		public override void SkipServerCredentials()
		{
			if (this.mKeyExchange == 15)
			{
				throw new TlsFatalAlert(10);
			}
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x0011B50B File Offset: 0x0011970B
		public override void ProcessServerCredentials(TlsCredentials serverCredentials)
		{
			if (!(serverCredentials is TlsEncryptionCredentials))
			{
				throw new TlsFatalAlert(80);
			}
			this.ProcessServerCertificate(serverCredentials.Certificate);
			this.mServerCredentials = (TlsEncryptionCredentials)serverCredentials;
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x0011B538 File Offset: 0x00119738
		public override byte[] GenerateServerKeyExchange()
		{
			this.mPskIdentityHint = this.mPskIdentityManager.GetHint();
			if (this.mPskIdentityHint == null && !this.RequiresServerKeyExchange)
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			if (this.mPskIdentityHint == null)
			{
				TlsUtilities.WriteOpaque16(TlsUtilities.EmptyBytes, memoryStream);
			}
			else
			{
				TlsUtilities.WriteOpaque16(this.mPskIdentityHint, memoryStream);
			}
			if (this.mKeyExchange == 14)
			{
				if (this.mDHParameters == null)
				{
					throw new TlsFatalAlert(80);
				}
				this.mDHAgreePrivateKey = TlsDHUtilities.GenerateEphemeralServerKeyExchange(this.mContext.SecureRandom, this.mDHParameters, memoryStream);
			}
			else if (this.mKeyExchange == 24)
			{
				this.mECAgreePrivateKey = TlsEccUtilities.GenerateEphemeralServerKeyExchange(this.mContext.SecureRandom, this.mNamedCurves, this.mClientECPointFormats, memoryStream);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x0011B5FC File Offset: 0x001197FC
		public override void ProcessServerCertificate(Certificate serverCertificate)
		{
			if (this.mKeyExchange != 15)
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
			if (this.mServerPublicKey.IsPrivate)
			{
				throw new TlsFatalAlert(80);
			}
			this.mRsaServerPublicKey = this.ValidateRsaPublicKey((RsaKeyParameters)this.mServerPublicKey);
			TlsUtilities.ValidateKeyUsage(certificateAt, 32);
			base.ProcessServerCertificate(serverCertificate);
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06002C6B RID: 11371 RVA: 0x0011B6A0 File Offset: 0x001198A0
		public override bool RequiresServerKeyExchange
		{
			get
			{
				int mKeyExchange = this.mKeyExchange;
				return mKeyExchange == 14 || mKeyExchange == 24;
			}
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x0011B6C4 File Offset: 0x001198C4
		public override void ProcessServerKeyExchange(Stream input)
		{
			this.mPskIdentityHint = TlsUtilities.ReadOpaque16(input);
			if (this.mKeyExchange == 14)
			{
				this.mDHParameters = TlsDHUtilities.ReceiveDHParameters(this.mDHVerifier, input);
				this.mDHAgreePublicKey = new DHPublicKeyParameters(TlsDHUtilities.ReadDHParameter(input), this.mDHParameters);
				return;
			}
			if (this.mKeyExchange == 24)
			{
				ECDomainParameters curve_params = TlsEccUtilities.ReadECParameters(this.mNamedCurves, this.mClientECPointFormats, input);
				byte[] encoding = TlsUtilities.ReadOpaque8(input);
				this.mECAgreePublicKey = TlsEccUtilities.ValidateECPublicKey(TlsEccUtilities.DeserializeECPublicKey(this.mClientECPointFormats, curve_params, encoding));
			}
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x0011B74D File Offset: 0x0011994D
		public override void ValidateCertificateRequest(CertificateRequest certificateRequest)
		{
			throw new TlsFatalAlert(10);
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x0010EA5F File Offset: 0x0010CC5F
		public override void ProcessClientCredentials(TlsCredentials clientCredentials)
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x0011B758 File Offset: 0x00119958
		public override void GenerateClientKeyExchange(Stream output)
		{
			if (this.mPskIdentityHint == null)
			{
				this.mPskIdentity.SkipIdentityHint();
			}
			else
			{
				this.mPskIdentity.NotifyIdentityHint(this.mPskIdentityHint);
			}
			byte[] pskIdentity = this.mPskIdentity.GetPskIdentity();
			if (pskIdentity == null)
			{
				throw new TlsFatalAlert(80);
			}
			this.mPsk = this.mPskIdentity.GetPsk();
			if (this.mPsk == null)
			{
				throw new TlsFatalAlert(80);
			}
			TlsUtilities.WriteOpaque16(pskIdentity, output);
			this.mContext.SecurityParameters.pskIdentity = pskIdentity;
			if (this.mKeyExchange == 14)
			{
				this.mDHAgreePrivateKey = TlsDHUtilities.GenerateEphemeralClientKeyExchange(this.mContext.SecureRandom, this.mDHParameters, output);
				return;
			}
			if (this.mKeyExchange == 24)
			{
				this.mECAgreePrivateKey = TlsEccUtilities.GenerateEphemeralClientKeyExchange(this.mContext.SecureRandom, this.mServerECPointFormats, this.mECAgreePublicKey.Parameters, output);
				return;
			}
			if (this.mKeyExchange == 15)
			{
				this.mPremasterSecret = TlsRsaUtilities.GenerateEncryptedPreMasterSecret(this.mContext, this.mRsaServerPublicKey, output);
			}
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x0011B858 File Offset: 0x00119A58
		public override void ProcessClientKeyExchange(Stream input)
		{
			byte[] array = TlsUtilities.ReadOpaque16(input);
			this.mPsk = this.mPskIdentityManager.GetPsk(array);
			if (this.mPsk == null)
			{
				throw new TlsFatalAlert(115);
			}
			this.mContext.SecurityParameters.pskIdentity = array;
			if (this.mKeyExchange == 14)
			{
				this.mDHAgreePublicKey = new DHPublicKeyParameters(TlsDHUtilities.ReadDHParameter(input), this.mDHParameters);
				return;
			}
			if (this.mKeyExchange == 24)
			{
				byte[] encoding = TlsUtilities.ReadOpaque8(input);
				ECDomainParameters parameters = this.mECAgreePrivateKey.Parameters;
				this.mECAgreePublicKey = TlsEccUtilities.ValidateECPublicKey(TlsEccUtilities.DeserializeECPublicKey(this.mServerECPointFormats, parameters, encoding));
				return;
			}
			if (this.mKeyExchange == 15)
			{
				byte[] encryptedPreMasterSecret;
				if (TlsUtilities.IsSsl(this.mContext))
				{
					encryptedPreMasterSecret = Streams.ReadAll(input);
				}
				else
				{
					encryptedPreMasterSecret = TlsUtilities.ReadOpaque16(input);
				}
				this.mPremasterSecret = this.mServerCredentials.DecryptPreMasterSecret(encryptedPreMasterSecret);
			}
		}

		// Token: 0x06002C71 RID: 11377 RVA: 0x0011B930 File Offset: 0x00119B30
		public override byte[] GeneratePremasterSecret()
		{
			byte[] array = this.GenerateOtherSecret(this.mPsk.Length);
			MemoryStream memoryStream = new MemoryStream(4 + array.Length + this.mPsk.Length);
			TlsUtilities.WriteOpaque16(array, memoryStream);
			TlsUtilities.WriteOpaque16(this.mPsk, memoryStream);
			Arrays.Fill(this.mPsk, 0);
			this.mPsk = null;
			return memoryStream.ToArray();
		}

		// Token: 0x06002C72 RID: 11378 RVA: 0x0011B98C File Offset: 0x00119B8C
		protected virtual byte[] GenerateOtherSecret(int pskLength)
		{
			if (this.mKeyExchange == 14)
			{
				if (this.mDHAgreePrivateKey != null)
				{
					return TlsDHUtilities.CalculateDHBasicAgreement(this.mDHAgreePublicKey, this.mDHAgreePrivateKey);
				}
				throw new TlsFatalAlert(80);
			}
			else if (this.mKeyExchange == 24)
			{
				if (this.mECAgreePrivateKey != null)
				{
					return TlsEccUtilities.CalculateECDHBasicAgreement(this.mECAgreePublicKey, this.mECAgreePrivateKey);
				}
				throw new TlsFatalAlert(80);
			}
			else
			{
				if (this.mKeyExchange == 15)
				{
					return this.mPremasterSecret;
				}
				return new byte[pskLength];
			}
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x0011BA08 File Offset: 0x00119C08
		protected virtual RsaKeyParameters ValidateRsaPublicKey(RsaKeyParameters key)
		{
			if (!key.Exponent.IsProbablePrime(2))
			{
				throw new TlsFatalAlert(47);
			}
			return key;
		}

		// Token: 0x04001D43 RID: 7491
		protected TlsPskIdentity mPskIdentity;

		// Token: 0x04001D44 RID: 7492
		protected TlsPskIdentityManager mPskIdentityManager;

		// Token: 0x04001D45 RID: 7493
		protected TlsDHVerifier mDHVerifier;

		// Token: 0x04001D46 RID: 7494
		protected DHParameters mDHParameters;

		// Token: 0x04001D47 RID: 7495
		protected int[] mNamedCurves;

		// Token: 0x04001D48 RID: 7496
		protected byte[] mClientECPointFormats;

		// Token: 0x04001D49 RID: 7497
		protected byte[] mServerECPointFormats;

		// Token: 0x04001D4A RID: 7498
		protected byte[] mPskIdentityHint;

		// Token: 0x04001D4B RID: 7499
		protected byte[] mPsk;

		// Token: 0x04001D4C RID: 7500
		protected DHPrivateKeyParameters mDHAgreePrivateKey;

		// Token: 0x04001D4D RID: 7501
		protected DHPublicKeyParameters mDHAgreePublicKey;

		// Token: 0x04001D4E RID: 7502
		protected ECPrivateKeyParameters mECAgreePrivateKey;

		// Token: 0x04001D4F RID: 7503
		protected ECPublicKeyParameters mECAgreePublicKey;

		// Token: 0x04001D50 RID: 7504
		protected AsymmetricKeyParameter mServerPublicKey;

		// Token: 0x04001D51 RID: 7505
		protected RsaKeyParameters mRsaServerPublicKey;

		// Token: 0x04001D52 RID: 7506
		protected TlsEncryptionCredentials mServerCredentials;

		// Token: 0x04001D53 RID: 7507
		protected byte[] mPremasterSecret;
	}
}
