using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003AD RID: 941
	public abstract class AsymmetricKeyParameter : ICipherParameters
	{
		// Token: 0x0600273D RID: 10045 RVA: 0x0010D59D File Offset: 0x0010B79D
		protected AsymmetricKeyParameter(bool privateKey)
		{
			this.privateKey = privateKey;
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x0600273E RID: 10046 RVA: 0x0010D5AC File Offset: 0x0010B7AC
		public bool IsPrivate
		{
			get
			{
				return this.privateKey;
			}
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x0010D5B4 File Offset: 0x0010B7B4
		public override bool Equals(object obj)
		{
			AsymmetricKeyParameter asymmetricKeyParameter = obj as AsymmetricKeyParameter;
			return asymmetricKeyParameter != null && this.Equals(asymmetricKeyParameter);
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x0010D5D4 File Offset: 0x0010B7D4
		protected bool Equals(AsymmetricKeyParameter other)
		{
			return this.privateKey == other.privateKey;
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x0010D5E4 File Offset: 0x0010B7E4
		public override int GetHashCode()
		{
			return this.privateKey.GetHashCode();
		}

		// Token: 0x040019B8 RID: 6584
		private readonly bool privateKey;
	}
}
