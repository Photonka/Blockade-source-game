using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004D5 RID: 1237
	public class KdfParameters : IDerivationParameters
	{
		// Token: 0x06002FF6 RID: 12278 RVA: 0x00128E66 File Offset: 0x00127066
		public KdfParameters(byte[] shared, byte[] iv)
		{
			this.shared = shared;
			this.iv = iv;
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x00128E7C File Offset: 0x0012707C
		public byte[] GetSharedSecret()
		{
			return this.shared;
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x00128E84 File Offset: 0x00127084
		public byte[] GetIV()
		{
			return this.iv;
		}

		// Token: 0x04001ECB RID: 7883
		private byte[] iv;

		// Token: 0x04001ECC RID: 7884
		private byte[] shared;
	}
}
