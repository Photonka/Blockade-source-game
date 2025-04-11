using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x02000328 RID: 808
	public class FixedPointUtilities
	{
		// Token: 0x06001FDA RID: 8154 RVA: 0x000F0E30 File Offset: 0x000EF030
		public static int GetCombSize(ECCurve c)
		{
			BigInteger order = c.Order;
			if (order != null)
			{
				return order.BitLength;
			}
			return c.FieldSize + 1;
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x000F0E56 File Offset: 0x000EF056
		public static FixedPointPreCompInfo GetFixedPointPreCompInfo(PreCompInfo preCompInfo)
		{
			return preCompInfo as FixedPointPreCompInfo;
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x000F0E5E File Offset: 0x000EF05E
		public static FixedPointPreCompInfo Precompute(ECPoint p)
		{
			return (FixedPointPreCompInfo)p.Curve.Precompute(p, FixedPointUtilities.PRECOMP_NAME, new FixedPointUtilities.FixedPointCallback(p));
		}

		// Token: 0x040018A2 RID: 6306
		public static readonly string PRECOMP_NAME = "bc_fixed_point";

		// Token: 0x020008F2 RID: 2290
		private class FixedPointCallback : IPreCompCallback
		{
			// Token: 0x06004D98 RID: 19864 RVA: 0x001B127D File Offset: 0x001AF47D
			internal FixedPointCallback(ECPoint p)
			{
				this.m_p = p;
			}

			// Token: 0x06004D99 RID: 19865 RVA: 0x001B128C File Offset: 0x001AF48C
			public PreCompInfo Precompute(PreCompInfo existing)
			{
				FixedPointPreCompInfo fixedPointPreCompInfo = (existing is FixedPointPreCompInfo) ? ((FixedPointPreCompInfo)existing) : null;
				ECCurve curve = this.m_p.Curve;
				int combSize = FixedPointUtilities.GetCombSize(curve);
				int num = (combSize > 250) ? 6 : 5;
				int num2 = 1 << num;
				if (this.CheckExisting(fixedPointPreCompInfo, num2))
				{
					return fixedPointPreCompInfo;
				}
				int e = (combSize + num - 1) / num;
				ECPoint[] array = new ECPoint[num + 1];
				array[0] = this.m_p;
				for (int i = 1; i < num; i++)
				{
					array[i] = array[i - 1].TimesPow2(e);
				}
				array[num] = array[0].Subtract(array[1]);
				curve.NormalizeAll(array);
				ECPoint[] array2 = new ECPoint[num2];
				array2[0] = array[0];
				for (int j = num - 1; j >= 0; j--)
				{
					ECPoint b = array[j];
					int num3 = 1 << j;
					for (int k = num3; k < num2; k += num3 << 1)
					{
						array2[k] = array2[k - num3].Add(b);
					}
				}
				curve.NormalizeAll(array2);
				return new FixedPointPreCompInfo
				{
					LookupTable = curve.CreateCacheSafeLookupTable(array2, 0, array2.Length),
					Offset = array[num],
					Width = num
				};
			}

			// Token: 0x06004D9A RID: 19866 RVA: 0x001B13C6 File Offset: 0x001AF5C6
			private bool CheckExisting(FixedPointPreCompInfo existingFP, int n)
			{
				return existingFP != null && this.CheckTable(existingFP.LookupTable, n);
			}

			// Token: 0x06004D9B RID: 19867 RVA: 0x001B13DA File Offset: 0x001AF5DA
			private bool CheckTable(ECLookupTable table, int n)
			{
				return table != null && table.Size >= n;
			}

			// Token: 0x04003451 RID: 13393
			private readonly ECPoint m_p;
		}
	}
}
