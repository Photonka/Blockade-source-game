using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x0200032E RID: 814
	public class NafR2LMultiplier : AbstractECMultiplier
	{
		// Token: 0x06001FEA RID: 8170 RVA: 0x000F112C File Offset: 0x000EF32C
		protected override ECPoint MultiplyPositive(ECPoint p, BigInteger k)
		{
			int[] array = WNafUtilities.GenerateCompactNaf(k);
			ECPoint ecpoint = p.Curve.Infinity;
			ECPoint ecpoint2 = p;
			int num = 0;
			foreach (int num2 in array)
			{
				int num3 = num2 >> 16;
				num += (num2 & 65535);
				ecpoint2 = ecpoint2.TimesPow2(num);
				ecpoint = ecpoint.Add((num3 < 0) ? ecpoint2.Negate() : ecpoint2);
				num = 1;
			}
			return ecpoint;
		}
	}
}
