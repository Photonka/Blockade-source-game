using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x02000336 RID: 822
	public class WTauNafPreCompInfo : PreCompInfo
	{
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06002016 RID: 8214 RVA: 0x000F1AF5 File Offset: 0x000EFCF5
		// (set) Token: 0x06002017 RID: 8215 RVA: 0x000F1AFD File Offset: 0x000EFCFD
		public virtual AbstractF2mPoint[] PreComp
		{
			get
			{
				return this.m_preComp;
			}
			set
			{
				this.m_preComp = value;
			}
		}

		// Token: 0x040018B2 RID: 6322
		protected AbstractF2mPoint[] m_preComp;
	}
}
