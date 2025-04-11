using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x02000337 RID: 823
	public class ZSignedDigitL2RMultiplier : AbstractECMultiplier
	{
		// Token: 0x06002019 RID: 8217 RVA: 0x000F1B08 File Offset: 0x000EFD08
		protected override ECPoint MultiplyPositive(ECPoint p, BigInteger k)
		{
			ECPoint ecpoint = p.Normalize();
			ECPoint ecpoint2 = ecpoint.Negate();
			ECPoint ecpoint3 = ecpoint;
			int bitLength = k.BitLength;
			int lowestSetBit = k.GetLowestSetBit();
			int num = bitLength;
			while (--num > lowestSetBit)
			{
				ecpoint3 = ecpoint3.TwicePlus(k.TestBit(num) ? ecpoint : ecpoint2);
			}
			return ecpoint3.TimesPow2(lowestSetBit);
		}
	}
}
