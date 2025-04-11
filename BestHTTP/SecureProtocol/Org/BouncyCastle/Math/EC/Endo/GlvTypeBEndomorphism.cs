using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Endo
{
	// Token: 0x0200033B RID: 827
	public class GlvTypeBEndomorphism : GlvEndomorphism, ECEndomorphism
	{
		// Token: 0x06002020 RID: 8224 RVA: 0x000F1BCA File Offset: 0x000EFDCA
		public GlvTypeBEndomorphism(ECCurve curve, GlvTypeBParameters parameters)
		{
			this.m_curve = curve;
			this.m_parameters = parameters;
			this.m_pointMap = new ScaleXPointMap(curve.FromBigInteger(parameters.Beta));
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x000F1BF8 File Offset: 0x000EFDF8
		public virtual BigInteger[] DecomposeScalar(BigInteger k)
		{
			int bits = this.m_parameters.Bits;
			BigInteger bigInteger = this.CalculateB(k, this.m_parameters.G1, bits);
			BigInteger bigInteger2 = this.CalculateB(k, this.m_parameters.G2, bits);
			BigInteger[] v = this.m_parameters.V1;
			BigInteger[] v2 = this.m_parameters.V2;
			BigInteger bigInteger3 = k.Subtract(bigInteger.Multiply(v[0]).Add(bigInteger2.Multiply(v2[0])));
			BigInteger bigInteger4 = bigInteger.Multiply(v[1]).Add(bigInteger2.Multiply(v2[1])).Negate();
			return new BigInteger[]
			{
				bigInteger3,
				bigInteger4
			};
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x000F1CA1 File Offset: 0x000EFEA1
		public virtual ECPointMap PointMap
		{
			get
			{
				return this.m_pointMap;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06002023 RID: 8227 RVA: 0x0006CF70 File Offset: 0x0006B170
		public virtual bool HasEfficientPointMap
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x000F1CAC File Offset: 0x000EFEAC
		protected virtual BigInteger CalculateB(BigInteger k, BigInteger g, int t)
		{
			bool flag = g.SignValue < 0;
			BigInteger bigInteger = k.Multiply(g.Abs());
			bool flag2 = bigInteger.TestBit(t - 1);
			bigInteger = bigInteger.ShiftRight(t);
			if (flag2)
			{
				bigInteger = bigInteger.Add(BigInteger.One);
			}
			if (!flag)
			{
				return bigInteger;
			}
			return bigInteger.Negate();
		}

		// Token: 0x040018B3 RID: 6323
		protected readonly ECCurve m_curve;

		// Token: 0x040018B4 RID: 6324
		protected readonly GlvTypeBParameters m_parameters;

		// Token: 0x040018B5 RID: 6325
		protected readonly ECPointMap m_pointMap;
	}
}
