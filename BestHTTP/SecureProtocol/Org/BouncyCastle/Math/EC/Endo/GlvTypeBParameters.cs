using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Endo
{
	// Token: 0x0200033C RID: 828
	public class GlvTypeBParameters
	{
		// Token: 0x06002025 RID: 8229 RVA: 0x000F1CF8 File Offset: 0x000EFEF8
		public GlvTypeBParameters(BigInteger beta, BigInteger lambda, BigInteger[] v1, BigInteger[] v2, BigInteger g1, BigInteger g2, int bits)
		{
			this.m_beta = beta;
			this.m_lambda = lambda;
			this.m_v1 = v1;
			this.m_v2 = v2;
			this.m_g1 = g1;
			this.m_g2 = g2;
			this.m_bits = bits;
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06002026 RID: 8230 RVA: 0x000F1D35 File Offset: 0x000EFF35
		public virtual BigInteger Beta
		{
			get
			{
				return this.m_beta;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06002027 RID: 8231 RVA: 0x000F1D3D File Offset: 0x000EFF3D
		public virtual BigInteger Lambda
		{
			get
			{
				return this.m_lambda;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06002028 RID: 8232 RVA: 0x000F1D45 File Offset: 0x000EFF45
		public virtual BigInteger[] V1
		{
			get
			{
				return this.m_v1;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06002029 RID: 8233 RVA: 0x000F1D4D File Offset: 0x000EFF4D
		public virtual BigInteger[] V2
		{
			get
			{
				return this.m_v2;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x0600202A RID: 8234 RVA: 0x000F1D55 File Offset: 0x000EFF55
		public virtual BigInteger G1
		{
			get
			{
				return this.m_g1;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x0600202B RID: 8235 RVA: 0x000F1D5D File Offset: 0x000EFF5D
		public virtual BigInteger G2
		{
			get
			{
				return this.m_g2;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x0600202C RID: 8236 RVA: 0x000F1D65 File Offset: 0x000EFF65
		public virtual int Bits
		{
			get
			{
				return this.m_bits;
			}
		}

		// Token: 0x040018B6 RID: 6326
		protected readonly BigInteger m_beta;

		// Token: 0x040018B7 RID: 6327
		protected readonly BigInteger m_lambda;

		// Token: 0x040018B8 RID: 6328
		protected readonly BigInteger[] m_v1;

		// Token: 0x040018B9 RID: 6329
		protected readonly BigInteger[] m_v2;

		// Token: 0x040018BA RID: 6330
		protected readonly BigInteger m_g1;

		// Token: 0x040018BB RID: 6331
		protected readonly BigInteger m_g2;

		// Token: 0x040018BC RID: 6332
		protected readonly int m_bits;
	}
}
