using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x0200032C RID: 812
	public class MontgomeryLadderMultiplier : AbstractECMultiplier
	{
		// Token: 0x06001FE6 RID: 8166 RVA: 0x000F1058 File Offset: 0x000EF258
		protected override ECPoint MultiplyPositive(ECPoint p, BigInteger k)
		{
			ECPoint[] array = new ECPoint[]
			{
				p.Curve.Infinity,
				p
			};
			int num = k.BitLength;
			while (--num >= 0)
			{
				int num2 = k.TestBit(num) ? 1 : 0;
				int num3 = 1 - num2;
				array[num3] = array[num3].Add(array[num2]);
				array[num2] = array[num2].Twice();
			}
			return array[0];
		}
	}
}
