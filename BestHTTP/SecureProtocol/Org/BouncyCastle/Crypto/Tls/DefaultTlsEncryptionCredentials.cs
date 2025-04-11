using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000402 RID: 1026
	public class DefaultTlsEncryptionCredentials : AbstractTlsEncryptionCredentials
	{
		// Token: 0x06002986 RID: 10630 RVA: 0x00111004 File Offset: 0x0010F204
		public DefaultTlsEncryptionCredentials(TlsContext context, Certificate certificate, AsymmetricKeyParameter privateKey)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (certificate.IsEmpty)
			{
				throw new ArgumentException("cannot be empty", "certificate");
			}
			if (privateKey == null)
			{
				throw new ArgumentNullException("'privateKey' cannot be null");
			}
			if (!privateKey.IsPrivate)
			{
				throw new ArgumentException("must be private", "privateKey");
			}
			if (!(privateKey is RsaKeyParameters))
			{
				throw new ArgumentException("type not supported: " + Platform.GetTypeName(privateKey), "privateKey");
			}
			this.mContext = context;
			this.mCertificate = certificate;
			this.mPrivateKey = privateKey;
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06002987 RID: 10631 RVA: 0x0011109B File Offset: 0x0010F29B
		public override Certificate Certificate
		{
			get
			{
				return this.mCertificate;
			}
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x001110A3 File Offset: 0x0010F2A3
		public override byte[] DecryptPreMasterSecret(byte[] encryptedPreMasterSecret)
		{
			return TlsRsaUtilities.SafeDecryptPreMasterSecret(this.mContext, (RsaKeyParameters)this.mPrivateKey, encryptedPreMasterSecret);
		}

		// Token: 0x04001B74 RID: 7028
		protected readonly TlsContext mContext;

		// Token: 0x04001B75 RID: 7029
		protected readonly Certificate mCertificate;

		// Token: 0x04001B76 RID: 7030
		protected readonly AsymmetricKeyParameter mPrivateKey;
	}
}
