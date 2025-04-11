using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x02000327 RID: 807
	public class FixedPointPreCompInfo : PreCompInfo
	{
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001FD3 RID: 8147 RVA: 0x000F0DEB File Offset: 0x000EEFEB
		// (set) Token: 0x06001FD4 RID: 8148 RVA: 0x000F0DF3 File Offset: 0x000EEFF3
		public virtual ECLookupTable LookupTable
		{
			get
			{
				return this.m_lookupTable;
			}
			set
			{
				this.m_lookupTable = value;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001FD5 RID: 8149 RVA: 0x000F0DFC File Offset: 0x000EEFFC
		// (set) Token: 0x06001FD6 RID: 8150 RVA: 0x000F0E04 File Offset: 0x000EF004
		public virtual ECPoint Offset
		{
			get
			{
				return this.m_offset;
			}
			set
			{
				this.m_offset = value;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x000F0E0D File Offset: 0x000EF00D
		// (set) Token: 0x06001FD8 RID: 8152 RVA: 0x000F0E15 File Offset: 0x000EF015
		public virtual int Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
			}
		}

		// Token: 0x0400189F RID: 6303
		protected ECPoint m_offset;

		// Token: 0x040018A0 RID: 6304
		protected ECLookupTable m_lookupTable;

		// Token: 0x040018A1 RID: 6305
		protected int m_width = -1;
	}
}
