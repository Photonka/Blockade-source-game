using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004E2 RID: 1250
	public class RC2Parameters : KeyParameter
	{
		// Token: 0x06003029 RID: 12329 RVA: 0x001292C0 File Offset: 0x001274C0
		public RC2Parameters(byte[] key) : this(key, (key.Length > 128) ? 1024 : (key.Length * 8))
		{
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x001292DF File Offset: 0x001274DF
		public RC2Parameters(byte[] key, int keyOff, int keyLen) : this(key, keyOff, keyLen, (keyLen > 128) ? 1024 : (keyLen * 8))
		{
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x001292FC File Offset: 0x001274FC
		public RC2Parameters(byte[] key, int bits) : base(key)
		{
			this.bits = bits;
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x0012930C File Offset: 0x0012750C
		public RC2Parameters(byte[] key, int keyOff, int keyLen, int bits) : base(key, keyOff, keyLen)
		{
			this.bits = bits;
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x0600302D RID: 12333 RVA: 0x0012931F File Offset: 0x0012751F
		public int EffectiveKeyBits
		{
			get
			{
				return this.bits;
			}
		}

		// Token: 0x04001EE5 RID: 7909
		private readonly int bits;
	}
}
