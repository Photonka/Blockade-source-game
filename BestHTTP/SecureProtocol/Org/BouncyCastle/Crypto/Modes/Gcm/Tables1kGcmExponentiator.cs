using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes.Gcm
{
	// Token: 0x02000516 RID: 1302
	public class Tables1kGcmExponentiator : IGcmExponentiator
	{
		// Token: 0x060031BE RID: 12734 RVA: 0x0012FFD4 File Offset: 0x0012E1D4
		public void Init(byte[] x)
		{
			uint[] array = GcmUtilities.AsUints(x);
			if (this.lookupPowX2 != null && Arrays.AreEqual(array, (uint[])this.lookupPowX2[0]))
			{
				return;
			}
			this.lookupPowX2 = Platform.CreateArrayList(8);
			this.lookupPowX2.Add(array);
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x00130024 File Offset: 0x0012E224
		public void ExponentiateX(long pow, byte[] output)
		{
			uint[] x = GcmUtilities.OneAsUints();
			int num = 0;
			while (pow > 0L)
			{
				if ((pow & 1L) != 0L)
				{
					this.EnsureAvailable(num);
					GcmUtilities.Multiply(x, (uint[])this.lookupPowX2[num]);
				}
				num++;
				pow >>= 1;
			}
			GcmUtilities.AsBytes(x, output);
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x00130074 File Offset: 0x0012E274
		private void EnsureAvailable(int bit)
		{
			int num = this.lookupPowX2.Count;
			if (num <= bit)
			{
				uint[] array = (uint[])this.lookupPowX2[num - 1];
				do
				{
					array = Arrays.Clone(array);
					GcmUtilities.Multiply(array, array);
					this.lookupPowX2.Add(array);
				}
				while (++num <= bit);
			}
		}

		// Token: 0x04001FB1 RID: 8113
		private IList lookupPowX2;
	}
}
