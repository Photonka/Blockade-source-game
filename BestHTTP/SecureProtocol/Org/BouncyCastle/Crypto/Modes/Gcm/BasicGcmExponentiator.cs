using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes.Gcm
{
	// Token: 0x02000511 RID: 1297
	public class BasicGcmExponentiator : IGcmExponentiator
	{
		// Token: 0x06003191 RID: 12689 RVA: 0x0012F84B File Offset: 0x0012DA4B
		public void Init(byte[] x)
		{
			this.x = GcmUtilities.AsUints(x);
		}

		// Token: 0x06003192 RID: 12690 RVA: 0x0012F85C File Offset: 0x0012DA5C
		public void ExponentiateX(long pow, byte[] output)
		{
			uint[] array = GcmUtilities.OneAsUints();
			if (pow > 0L)
			{
				uint[] y = Arrays.Clone(this.x);
				do
				{
					if ((pow & 1L) != 0L)
					{
						GcmUtilities.Multiply(array, y);
					}
					GcmUtilities.Multiply(y, y);
					pow >>= 1;
				}
				while (pow > 0L);
			}
			GcmUtilities.AsBytes(array, output);
		}

		// Token: 0x04001FAC RID: 8108
		private uint[] x;
	}
}
