using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004AA RID: 1194
	public class AeadParameters : ICipherParameters
	{
		// Token: 0x06002EF2 RID: 12018 RVA: 0x00126E92 File Offset: 0x00125092
		public AeadParameters(KeyParameter key, int macSize, byte[] nonce) : this(key, macSize, nonce, null)
		{
		}

		// Token: 0x06002EF3 RID: 12019 RVA: 0x00126E9E File Offset: 0x0012509E
		public AeadParameters(KeyParameter key, int macSize, byte[] nonce, byte[] associatedText)
		{
			this.key = key;
			this.nonce = nonce;
			this.macSize = macSize;
			this.associatedText = associatedText;
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06002EF4 RID: 12020 RVA: 0x00126EC3 File Offset: 0x001250C3
		public virtual KeyParameter Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06002EF5 RID: 12021 RVA: 0x00126ECB File Offset: 0x001250CB
		public virtual int MacSize
		{
			get
			{
				return this.macSize;
			}
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x00126ED3 File Offset: 0x001250D3
		public virtual byte[] GetAssociatedText()
		{
			return this.associatedText;
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x00126EDB File Offset: 0x001250DB
		public virtual byte[] GetNonce()
		{
			return this.nonce;
		}

		// Token: 0x04001E6C RID: 7788
		private readonly byte[] associatedText;

		// Token: 0x04001E6D RID: 7789
		private readonly byte[] nonce;

		// Token: 0x04001E6E RID: 7790
		private readonly KeyParameter key;

		// Token: 0x04001E6F RID: 7791
		private readonly int macSize;
	}
}
