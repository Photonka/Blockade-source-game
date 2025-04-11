using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Endo;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x02000329 RID: 809
	public class GlvMultiplier : AbstractECMultiplier
	{
		// Token: 0x06001FDF RID: 8159 RVA: 0x000F0E88 File Offset: 0x000EF088
		public GlvMultiplier(ECCurve curve, GlvEndomorphism glvEndomorphism)
		{
			if (curve == null || curve.Order == null)
			{
				throw new ArgumentException("Need curve with known group order", "curve");
			}
			this.curve = curve;
			this.glvEndomorphism = glvEndomorphism;
		}

		// Token: 0x06001FE0 RID: 8160 RVA: 0x000F0EBC File Offset: 0x000EF0BC
		protected override ECPoint MultiplyPositive(ECPoint p, BigInteger k)
		{
			if (!this.curve.Equals(p.Curve))
			{
				throw new InvalidOperationException();
			}
			BigInteger order = p.Curve.Order;
			BigInteger[] array = this.glvEndomorphism.DecomposeScalar(k.Mod(order));
			BigInteger k2 = array[0];
			BigInteger l = array[1];
			ECPointMap pointMap = this.glvEndomorphism.PointMap;
			if (this.glvEndomorphism.HasEfficientPointMap)
			{
				return ECAlgorithms.ImplShamirsTrickWNaf(p, k2, pointMap, l);
			}
			return ECAlgorithms.ImplShamirsTrickWNaf(p, k2, pointMap.Map(p), l);
		}

		// Token: 0x040018A3 RID: 6307
		protected readonly ECCurve curve;

		// Token: 0x040018A4 RID: 6308
		protected readonly GlvEndomorphism glvEndomorphism;
	}
}
