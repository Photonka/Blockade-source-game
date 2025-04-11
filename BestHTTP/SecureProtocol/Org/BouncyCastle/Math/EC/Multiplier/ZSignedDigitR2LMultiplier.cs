using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x02000338 RID: 824
	public class ZSignedDigitR2LMultiplier : AbstractECMultiplier
	{
		// Token: 0x0600201B RID: 8219 RVA: 0x000F1B60 File Offset: 0x000EFD60
		protected override ECPoint MultiplyPositive(ECPoint p, BigInteger k)
		{
			ECPoint ecpoint = p.Curve.Infinity;
			int bitLength = k.BitLength;
			int lowestSetBit = k.GetLowestSetBit();
			ECPoint ecpoint2 = p.TimesPow2(lowestSetBit);
			int num = lowestSetBit;
			while (++num < bitLength)
			{
				ecpoint = ecpoint.Add(k.TestBit(num) ? ecpoint2 : ecpoint2.Negate());
				ecpoint2 = ecpoint2.Twice();
			}
			return ecpoint.Add(ecpoint2);
		}
	}
}
