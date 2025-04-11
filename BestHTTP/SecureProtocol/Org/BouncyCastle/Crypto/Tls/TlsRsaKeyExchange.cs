using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000465 RID: 1125
	public class TlsRsaKeyExchange : AbstractTlsKeyExchange
	{
		// Token: 0x06002C74 RID: 11380 RVA: 0x0011BA21 File Offset: 0x00119C21
		public TlsRsaKeyExchange(IList supportedSignatureAlgorithms) : base(1, supportedSignatureAlgorithms)
		{
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x0011B74D File Offset: 0x0011994D
		public override void SkipServerCredentials()
		{
			throw new TlsFatalAlert(10);
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x0011BA2B File Offset: 0x00119C2B
		public override void ProcessServerCredentials(TlsCredentials serverCredentials)
		{
			if (!(serverCredentials is TlsEncryptionCredentials))
			{
				throw new TlsFatalAlert(80);
			}
			this.ProcessServerCertificate(serverCredentials.Certificate);
			this.mServerCredentials = (TlsEncryptionCredentials)serverCredentials;
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x0011BA58 File Offset: 0x00119C58
		public override void ProcessServerCertificate(Certificate serverCertificate)
		{
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

		// Token: 0x06002C78 RID: 11384 RVA: 0x0011BAE8 File Offset: 0x00119CE8
		public override void ValidateCertificateRequest(CertificateRequest certificateRequest)
		{
			foreach (byte b in certificateRequest.CertificateTypes)
			{
				if (b - 1 > 1 && b != 64)
				{
					throw new TlsFatalAlert(47);
				}
			}
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x001192DD File Offset: 0x001174DD
		public override void ProcessClientCredentials(TlsCredentials clientCredentials)
		{
			if (!(clientCredentials is TlsSignerCredentials))
			{
				throw new TlsFatalAlert(80);
			}
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x0011BB21 File Offset: 0x00119D21
		public override void GenerateClientKeyExchange(Stream output)
		{
			this.mPremasterSecret = TlsRsaUtilities.GenerateEncryptedPreMasterSecret(this.mContext, this.mRsaServerPublicKey, output);
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x0011BB3C File Offset: 0x00119D3C
		public override void ProcessClientKeyExchange(Stream input)
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

		// Token: 0x06002C7C RID: 11388 RVA: 0x0011BB78 File Offset: 0x00119D78
		public override byte[] GeneratePremasterSecret()
		{
			if (this.mPremasterSecret == null)
			{
				throw new TlsFatalAlert(80);
			}
			byte[] result = this.mPremasterSecret;
			this.mPremasterSecret = null;
			return result;
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x0011BA08 File Offset: 0x00119C08
		protected virtual RsaKeyParameters ValidateRsaPublicKey(RsaKeyParameters key)
		{
			if (!key.Exponent.IsProbablePrime(2))
			{
				throw new TlsFatalAlert(47);
			}
			return key;
		}

		// Token: 0x04001D54 RID: 7508
		protected AsymmetricKeyParameter mServerPublicKey;

		// Token: 0x04001D55 RID: 7509
		protected RsaKeyParameters mRsaServerPublicKey;

		// Token: 0x04001D56 RID: 7510
		protected TlsEncryptionCredentials mServerCredentials;

		// Token: 0x04001D57 RID: 7511
		protected byte[] mPremasterSecret;
	}
}
