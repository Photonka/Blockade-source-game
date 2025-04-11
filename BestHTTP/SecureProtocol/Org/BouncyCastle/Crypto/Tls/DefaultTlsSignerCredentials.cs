using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000404 RID: 1028
	public class DefaultTlsSignerCredentials : AbstractTlsSignerCredentials
	{
		// Token: 0x06002998 RID: 10648 RVA: 0x0011127C File Offset: 0x0010F47C
		public DefaultTlsSignerCredentials(TlsContext context, Certificate certificate, AsymmetricKeyParameter privateKey) : this(context, certificate, privateKey, null)
		{
		}

		// Token: 0x06002999 RID: 10649 RVA: 0x00111288 File Offset: 0x0010F488
		public DefaultTlsSignerCredentials(TlsContext context, Certificate certificate, AsymmetricKeyParameter privateKey, SignatureAndHashAlgorithm signatureAndHashAlgorithm)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (certificate.IsEmpty)
			{
				throw new ArgumentException("cannot be empty", "clientCertificate");
			}
			if (privateKey == null)
			{
				throw new ArgumentNullException("privateKey");
			}
			if (!privateKey.IsPrivate)
			{
				throw new ArgumentException("must be private", "privateKey");
			}
			if (TlsUtilities.IsTlsV12(context) && signatureAndHashAlgorithm == null)
			{
				throw new ArgumentException("cannot be null for (D)TLS 1.2+", "signatureAndHashAlgorithm");
			}
			if (privateKey is RsaKeyParameters)
			{
				this.mSigner = new TlsRsaSigner();
			}
			else if (privateKey is DsaPrivateKeyParameters)
			{
				this.mSigner = new TlsDssSigner();
			}
			else
			{
				if (!(privateKey is ECPrivateKeyParameters))
				{
					throw new ArgumentException("type not supported: " + Platform.GetTypeName(privateKey), "privateKey");
				}
				this.mSigner = new TlsECDsaSigner();
			}
			this.mSigner.Init(context);
			this.mContext = context;
			this.mCertificate = certificate;
			this.mPrivateKey = privateKey;
			this.mSignatureAndHashAlgorithm = signatureAndHashAlgorithm;
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x0600299A RID: 10650 RVA: 0x00111386 File Offset: 0x0010F586
		public override Certificate Certificate
		{
			get
			{
				return this.mCertificate;
			}
		}

		// Token: 0x0600299B RID: 10651 RVA: 0x00111390 File Offset: 0x0010F590
		public override byte[] GenerateCertificateSignature(byte[] hash)
		{
			byte[] result;
			try
			{
				if (TlsUtilities.IsTlsV12(this.mContext))
				{
					result = this.mSigner.GenerateRawSignature(this.mSignatureAndHashAlgorithm, this.mPrivateKey, hash);
				}
				else
				{
					result = this.mSigner.GenerateRawSignature(this.mPrivateKey, hash);
				}
			}
			catch (CryptoException alertCause)
			{
				throw new TlsFatalAlert(80, alertCause);
			}
			return result;
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x0600299C RID: 10652 RVA: 0x001113F8 File Offset: 0x0010F5F8
		public override SignatureAndHashAlgorithm SignatureAndHashAlgorithm
		{
			get
			{
				return this.mSignatureAndHashAlgorithm;
			}
		}

		// Token: 0x04001B77 RID: 7031
		protected readonly TlsContext mContext;

		// Token: 0x04001B78 RID: 7032
		protected readonly Certificate mCertificate;

		// Token: 0x04001B79 RID: 7033
		protected readonly AsymmetricKeyParameter mPrivateKey;

		// Token: 0x04001B7A RID: 7034
		protected readonly SignatureAndHashAlgorithm mSignatureAndHashAlgorithm;

		// Token: 0x04001B7B RID: 7035
		protected readonly TlsSigner mSigner;
	}
}
