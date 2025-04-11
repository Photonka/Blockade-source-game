using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC
{
	// Token: 0x0200031A RID: 794
	public class ScaleXPointMap : ECPointMap
	{
		// Token: 0x06001F04 RID: 7940 RVA: 0x000E9A79 File Offset: 0x000E7C79
		public ScaleXPointMap(ECFieldElement scale)
		{
			this.scale = scale;
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x000E9A88 File Offset: 0x000E7C88
		public virtual ECPoint Map(ECPoint p)
		{
			return p.ScaleX(this.scale);
		}

		// Token: 0x04001845 RID: 6213
		protected readonly ECFieldElement scale;
	}
}
