using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004D2 RID: 1234
	public class IesParameters : ICipherParameters
	{
		// Token: 0x06002FEE RID: 12270 RVA: 0x00128DFF File Offset: 0x00126FFF
		public IesParameters(byte[] derivation, byte[] encoding, int macKeySize)
		{
			this.derivation = derivation;
			this.encoding = encoding;
			this.macKeySize = macKeySize;
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x00128E1C File Offset: 0x0012701C
		public byte[] GetDerivationV()
		{
			return this.derivation;
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x00128E24 File Offset: 0x00127024
		public byte[] GetEncodingV()
		{
			return this.encoding;
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06002FF1 RID: 12273 RVA: 0x00128E2C File Offset: 0x0012702C
		public int MacKeySize
		{
			get
			{
				return this.macKeySize;
			}
		}

		// Token: 0x04001EC6 RID: 7878
		private byte[] derivation;

		// Token: 0x04001EC7 RID: 7879
		private byte[] encoding;

		// Token: 0x04001EC8 RID: 7880
		private int macKeySize;
	}
}
