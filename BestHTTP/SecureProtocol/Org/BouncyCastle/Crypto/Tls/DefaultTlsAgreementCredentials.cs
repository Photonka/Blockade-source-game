using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003FE RID: 1022
	public class DefaultTlsAgreementCredentials : AbstractTlsAgreementCredentials
	{
		// Token: 0x06002955 RID: 10581 RVA: 0x00110894 File Offset: 0x0010EA94
		public DefaultTlsAgreementCredentials(Certificate certificate, AsymmetricKeyParameter privateKey)
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
				throw new ArgumentNullException("privateKey");
			}
			if (!privateKey.IsPrivate)
			{
				throw new ArgumentException("must be private", "privateKey");
			}
			if (privateKey is DHPrivateKeyParameters)
			{
				this.mBasicAgreement = new DHBasicAgreement();
				this.mTruncateAgreement = true;
			}
			else
			{
				if (!(privateKey is ECPrivateKeyParameters))
				{
					throw new ArgumentException("type not supported: " + Platform.GetTypeName(privateKey), "privateKey");
				}
				this.mBasicAgreement = new ECDHBasicAgreement();
				this.mTruncateAgreement = false;
			}
			this.mCertificate = certificate;
			this.mPrivateKey = privateKey;
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06002956 RID: 10582 RVA: 0x00110954 File Offset: 0x0010EB54
		public override Certificate Certificate
		{
			get
			{
				return this.mCertificate;
			}
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x0011095C File Offset: 0x0010EB5C
		public override byte[] GenerateAgreement(AsymmetricKeyParameter peerPublicKey)
		{
			this.mBasicAgreement.Init(this.mPrivateKey);
			BigInteger n = this.mBasicAgreement.CalculateAgreement(peerPublicKey);
			if (this.mTruncateAgreement)
			{
				return BigIntegers.AsUnsignedByteArray(n);
			}
			return BigIntegers.AsUnsignedByteArray(this.mBasicAgreement.GetFieldSize(), n);
		}

		// Token: 0x04001B6B RID: 7019
		protected readonly Certificate mCertificate;

		// Token: 0x04001B6C RID: 7020
		protected readonly AsymmetricKeyParameter mPrivateKey;

		// Token: 0x04001B6D RID: 7021
		protected readonly IBasicAgreement mBasicAgreement;

		// Token: 0x04001B6E RID: 7022
		protected readonly bool mTruncateAgreement;
	}
}
