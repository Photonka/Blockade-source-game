using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x02000323 RID: 803
	public abstract class AbstractECMultiplier : ECMultiplier
	{
		// Token: 0x06001FCA RID: 8138 RVA: 0x000F0C54 File Offset: 0x000EEE54
		public virtual ECPoint Multiply(ECPoint p, BigInteger k)
		{
			int signValue = k.SignValue;
			if (signValue == 0 || p.IsInfinity)
			{
				return p.Curve.Infinity;
			}
			ECPoint ecpoint = this.MultiplyPositive(p, k.Abs());
			ECPoint p2 = (signValue > 0) ? ecpoint : ecpoint.Negate();
			return this.CheckResult(p2);
		}

		// Token: 0x06001FCB RID: 8139
		protected abstract ECPoint MultiplyPositive(ECPoint p, BigInteger k);

		// Token: 0x06001FCC RID: 8140 RVA: 0x000F0CA2 File Offset: 0x000EEEA2
		protected virtual ECPoint CheckResult(ECPoint p)
		{
			return ECAlgorithms.ImplCheckResult(p);
		}
	}
}
