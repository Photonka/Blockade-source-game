using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x02000333 RID: 819
	public class WNafPreCompInfo : PreCompInfo
	{
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001FF9 RID: 8185 RVA: 0x000F1334 File Offset: 0x000EF534
		// (set) Token: 0x06001FFA RID: 8186 RVA: 0x000F133C File Offset: 0x000EF53C
		public virtual ECPoint[] PreComp
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

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001FFB RID: 8187 RVA: 0x000F1345 File Offset: 0x000EF545
		// (set) Token: 0x06001FFC RID: 8188 RVA: 0x000F134D File Offset: 0x000EF54D
		public virtual ECPoint[] PreCompNeg
		{
			get
			{
				return this.m_preCompNeg;
			}
			set
			{
				this.m_preCompNeg = value;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001FFD RID: 8189 RVA: 0x000F1356 File Offset: 0x000EF556
		// (set) Token: 0x06001FFE RID: 8190 RVA: 0x000F135E File Offset: 0x000EF55E
		public virtual ECPoint Twice
		{
			get
			{
				return this.m_twice;
			}
			set
			{
				this.m_twice = value;
			}
		}

		// Token: 0x040018AB RID: 6315
		protected ECPoint[] m_preComp;

		// Token: 0x040018AC RID: 6316
		protected ECPoint[] m_preCompNeg;

		// Token: 0x040018AD RID: 6317
		protected ECPoint m_twice;
	}
}
