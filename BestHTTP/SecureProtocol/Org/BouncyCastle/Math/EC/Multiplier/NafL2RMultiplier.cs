using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x0200032D RID: 813
	public class NafL2RMultiplier : AbstractECMultiplier
	{
		// Token: 0x06001FE8 RID: 8168 RVA: 0x000F10C0 File Offset: 0x000EF2C0
		protected override ECPoint MultiplyPositive(ECPoint p, BigInteger k)
		{
			int[] array = WNafUtilities.GenerateCompactNaf(k);
			ECPoint ecpoint = p.Normalize();
			ECPoint ecpoint2 = ecpoint.Negate();
			ECPoint ecpoint3 = p.Curve.Infinity;
			int num = array.Length;
			while (--num >= 0)
			{
				int num2 = array[num];
				int num3 = num2 >> 16;
				int e = num2 & 65535;
				ecpoint3 = ecpoint3.TwicePlus((num3 < 0) ? ecpoint2 : ecpoint);
				ecpoint3 = ecpoint3.TimesPow2(e);
			}
			return ecpoint3;
		}
	}
}
