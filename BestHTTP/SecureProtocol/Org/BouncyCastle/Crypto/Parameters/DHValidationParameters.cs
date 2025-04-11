using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004B3 RID: 1203
	public class DHValidationParameters
	{
		// Token: 0x06002F32 RID: 12082 RVA: 0x001276A4 File Offset: 0x001258A4
		public DHValidationParameters(byte[] seed, int counter)
		{
			if (seed == null)
			{
				throw new ArgumentNullException("seed");
			}
			this.seed = (byte[])seed.Clone();
			this.counter = counter;
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x001276D2 File Offset: 0x001258D2
		public byte[] GetSeed()
		{
			return (byte[])this.seed.Clone();
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06002F34 RID: 12084 RVA: 0x001276E4 File Offset: 0x001258E4
		public int Counter
		{
			get
			{
				return this.counter;
			}
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x001276EC File Offset: 0x001258EC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DHValidationParameters dhvalidationParameters = obj as DHValidationParameters;
			return dhvalidationParameters != null && this.Equals(dhvalidationParameters);
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x00127712 File Offset: 0x00125912
		protected bool Equals(DHValidationParameters other)
		{
			return this.counter == other.counter && Arrays.AreEqual(this.seed, other.seed);
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x00127738 File Offset: 0x00125938
		public override int GetHashCode()
		{
			return this.counter.GetHashCode() ^ Arrays.GetHashCode(this.seed);
		}

		// Token: 0x04001E81 RID: 7809
		private readonly byte[] seed;

		// Token: 0x04001E82 RID: 7810
		private readonly int counter;
	}
}
