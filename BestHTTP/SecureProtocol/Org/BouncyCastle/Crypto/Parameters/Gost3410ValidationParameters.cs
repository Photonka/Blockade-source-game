using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004D0 RID: 1232
	public class Gost3410ValidationParameters
	{
		// Token: 0x06002FDE RID: 12254 RVA: 0x00128C6E File Offset: 0x00126E6E
		public Gost3410ValidationParameters(int x0, int c)
		{
			this.x0 = x0;
			this.c = c;
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x00128C84 File Offset: 0x00126E84
		public Gost3410ValidationParameters(long x0L, long cL)
		{
			this.x0L = x0L;
			this.cL = cL;
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06002FE0 RID: 12256 RVA: 0x00128C9A File Offset: 0x00126E9A
		public int C
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06002FE1 RID: 12257 RVA: 0x00128CA2 File Offset: 0x00126EA2
		public int X0
		{
			get
			{
				return this.x0;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06002FE2 RID: 12258 RVA: 0x00128CAA File Offset: 0x00126EAA
		public long CL
		{
			get
			{
				return this.cL;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06002FE3 RID: 12259 RVA: 0x00128CB2 File Offset: 0x00126EB2
		public long X0L
		{
			get
			{
				return this.x0L;
			}
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x00128CBC File Offset: 0x00126EBC
		public override bool Equals(object obj)
		{
			Gost3410ValidationParameters gost3410ValidationParameters = obj as Gost3410ValidationParameters;
			return gost3410ValidationParameters != null && gost3410ValidationParameters.c == this.c && gost3410ValidationParameters.x0 == this.x0 && gost3410ValidationParameters.cL == this.cL && gost3410ValidationParameters.x0L == this.x0L;
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x00128D0D File Offset: 0x00126F0D
		public override int GetHashCode()
		{
			return this.c.GetHashCode() ^ this.x0.GetHashCode() ^ this.cL.GetHashCode() ^ this.x0L.GetHashCode();
		}

		// Token: 0x04001EBE RID: 7870
		private int x0;

		// Token: 0x04001EBF RID: 7871
		private int c;

		// Token: 0x04001EC0 RID: 7872
		private long x0L;

		// Token: 0x04001EC1 RID: 7873
		private long cL;
	}
}
