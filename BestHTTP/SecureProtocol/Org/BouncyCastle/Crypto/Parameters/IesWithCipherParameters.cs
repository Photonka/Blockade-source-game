using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004D3 RID: 1235
	public class IesWithCipherParameters : IesParameters
	{
		// Token: 0x06002FF2 RID: 12274 RVA: 0x00128E34 File Offset: 0x00127034
		public IesWithCipherParameters(byte[] derivation, byte[] encoding, int macKeySize, int cipherKeySize) : base(derivation, encoding, macKeySize)
		{
			this.cipherKeySize = cipherKeySize;
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06002FF3 RID: 12275 RVA: 0x00128E47 File Offset: 0x00127047
		public int CipherKeySize
		{
			get
			{
				return this.cipherKeySize;
			}
		}

		// Token: 0x04001EC9 RID: 7881
		private int cipherKeySize;
	}
}
