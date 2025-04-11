using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC
{
	// Token: 0x0200031B RID: 795
	public class ScaleYPointMap : ECPointMap
	{
		// Token: 0x06001F06 RID: 7942 RVA: 0x000E9A96 File Offset: 0x000E7C96
		public ScaleYPointMap(ECFieldElement scale)
		{
			this.scale = scale;
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x000E9AA5 File Offset: 0x000E7CA5
		public virtual ECPoint Map(ECPoint p)
		{
			return p.ScaleY(this.scale);
		}

		// Token: 0x04001846 RID: 6214
		protected readonly ECFieldElement scale;
	}
}
