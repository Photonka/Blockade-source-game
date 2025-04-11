using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004BA RID: 1210
	public class DsaValidationParameters
	{
		// Token: 0x06002F5A RID: 12122 RVA: 0x00127AE8 File Offset: 0x00125CE8
		public DsaValidationParameters(byte[] seed, int counter) : this(seed, counter, -1)
		{
		}

		// Token: 0x06002F5B RID: 12123 RVA: 0x00127AF3 File Offset: 0x00125CF3
		public DsaValidationParameters(byte[] seed, int counter, int usageIndex)
		{
			if (seed == null)
			{
				throw new ArgumentNullException("seed");
			}
			this.seed = (byte[])seed.Clone();
			this.counter = counter;
			this.usageIndex = usageIndex;
		}

		// Token: 0x06002F5C RID: 12124 RVA: 0x00127B28 File Offset: 0x00125D28
		public virtual byte[] GetSeed()
		{
			return (byte[])this.seed.Clone();
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06002F5D RID: 12125 RVA: 0x00127B3A File Offset: 0x00125D3A
		public virtual int Counter
		{
			get
			{
				return this.counter;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06002F5E RID: 12126 RVA: 0x00127B42 File Offset: 0x00125D42
		public virtual int UsageIndex
		{
			get
			{
				return this.usageIndex;
			}
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x00127B4C File Offset: 0x00125D4C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DsaValidationParameters dsaValidationParameters = obj as DsaValidationParameters;
			return dsaValidationParameters != null && this.Equals(dsaValidationParameters);
		}

		// Token: 0x06002F60 RID: 12128 RVA: 0x00127B72 File Offset: 0x00125D72
		protected virtual bool Equals(DsaValidationParameters other)
		{
			return this.counter == other.counter && Arrays.AreEqual(this.seed, other.seed);
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x00127B98 File Offset: 0x00125D98
		public override int GetHashCode()
		{
			return this.counter.GetHashCode() ^ Arrays.GetHashCode(this.seed);
		}

		// Token: 0x04001E92 RID: 7826
		private readonly byte[] seed;

		// Token: 0x04001E93 RID: 7827
		private readonly int counter;

		// Token: 0x04001E94 RID: 7828
		private readonly int usageIndex;
	}
}
