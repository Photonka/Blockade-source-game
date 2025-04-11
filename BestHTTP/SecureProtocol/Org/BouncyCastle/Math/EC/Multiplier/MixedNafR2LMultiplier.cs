using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x0200032B RID: 811
	public class MixedNafR2LMultiplier : AbstractECMultiplier
	{
		// Token: 0x06001FE2 RID: 8162 RVA: 0x000F0F39 File Offset: 0x000EF139
		public MixedNafR2LMultiplier() : this(2, 4)
		{
		}

		// Token: 0x06001FE3 RID: 8163 RVA: 0x000F0F43 File Offset: 0x000EF143
		public MixedNafR2LMultiplier(int additionCoord, int doublingCoord)
		{
			this.additionCoord = additionCoord;
			this.doublingCoord = doublingCoord;
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x000F0F5C File Offset: 0x000EF15C
		protected override ECPoint MultiplyPositive(ECPoint p, BigInteger k)
		{
			ECCurve curve = p.Curve;
			ECCurve eccurve = this.ConfigureCurve(curve, this.additionCoord);
			ECCurve eccurve2 = this.ConfigureCurve(curve, this.doublingCoord);
			int[] array = WNafUtilities.GenerateCompactNaf(k);
			ECPoint ecpoint = eccurve.Infinity;
			ECPoint ecpoint2 = eccurve2.ImportPoint(p);
			int num = 0;
			foreach (int num2 in array)
			{
				int num3 = num2 >> 16;
				num += (num2 & 65535);
				ecpoint2 = ecpoint2.TimesPow2(num);
				ECPoint ecpoint3 = eccurve.ImportPoint(ecpoint2);
				if (num3 < 0)
				{
					ecpoint3 = ecpoint3.Negate();
				}
				ecpoint = ecpoint.Add(ecpoint3);
				num = 1;
			}
			return curve.ImportPoint(ecpoint);
		}

		// Token: 0x06001FE5 RID: 8165 RVA: 0x000F1004 File Offset: 0x000EF204
		protected virtual ECCurve ConfigureCurve(ECCurve c, int coord)
		{
			if (c.CoordinateSystem == coord)
			{
				return c;
			}
			if (!c.SupportsCoordinateSystem(coord))
			{
				throw new ArgumentException("Coordinate system " + coord + " not supported by this curve", "coord");
			}
			return c.Configure().SetCoordinateSystem(coord).Create();
		}

		// Token: 0x040018A5 RID: 6309
		protected readonly int additionCoord;

		// Token: 0x040018A6 RID: 6310
		protected readonly int doublingCoord;
	}
}
