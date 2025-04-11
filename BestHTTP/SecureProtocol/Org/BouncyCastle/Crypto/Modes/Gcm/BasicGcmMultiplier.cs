using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes.Gcm
{
	// Token: 0x02000512 RID: 1298
	public class BasicGcmMultiplier : IGcmMultiplier
	{
		// Token: 0x06003194 RID: 12692 RVA: 0x0012F8A5 File Offset: 0x0012DAA5
		public void Init(byte[] H)
		{
			this.H = GcmUtilities.AsUints(H);
		}

		// Token: 0x06003195 RID: 12693 RVA: 0x0012F8B3 File Offset: 0x0012DAB3
		public void MultiplyH(byte[] x)
		{
			uint[] x2 = GcmUtilities.AsUints(x);
			GcmUtilities.Multiply(x2, this.H);
			GcmUtilities.AsBytes(x2, x);
		}

		// Token: 0x04001FAD RID: 8109
		private uint[] H;
	}
}
