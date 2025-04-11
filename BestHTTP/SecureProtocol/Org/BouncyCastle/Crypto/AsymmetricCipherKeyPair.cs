using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003AC RID: 940
	public class AsymmetricCipherKeyPair
	{
		// Token: 0x0600273A RID: 10042 RVA: 0x0010D53C File Offset: 0x0010B73C
		public AsymmetricCipherKeyPair(AsymmetricKeyParameter publicParameter, AsymmetricKeyParameter privateParameter)
		{
			if (publicParameter.IsPrivate)
			{
				throw new ArgumentException("Expected a public key", "publicParameter");
			}
			if (!privateParameter.IsPrivate)
			{
				throw new ArgumentException("Expected a private key", "privateParameter");
			}
			this.publicParameter = publicParameter;
			this.privateParameter = privateParameter;
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x0600273B RID: 10043 RVA: 0x0010D58D File Offset: 0x0010B78D
		public AsymmetricKeyParameter Public
		{
			get
			{
				return this.publicParameter;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x0600273C RID: 10044 RVA: 0x0010D595 File Offset: 0x0010B795
		public AsymmetricKeyParameter Private
		{
			get
			{
				return this.privateParameter;
			}
		}

		// Token: 0x040019B6 RID: 6582
		private readonly AsymmetricKeyParameter publicParameter;

		// Token: 0x040019B7 RID: 6583
		private readonly AsymmetricKeyParameter privateParameter;
	}
}
