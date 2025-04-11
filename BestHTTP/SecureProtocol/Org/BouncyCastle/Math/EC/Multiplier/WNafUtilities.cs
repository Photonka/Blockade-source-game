using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x02000334 RID: 820
	public abstract class WNafUtilities
	{
		// Token: 0x06002000 RID: 8192 RVA: 0x000F1368 File Offset: 0x000EF568
		public static int[] GenerateCompactNaf(BigInteger k)
		{
			if (k.BitLength >> 16 != 0)
			{
				throw new ArgumentException("must have bitlength < 2^16", "k");
			}
			if (k.SignValue == 0)
			{
				return Arrays.EmptyInts;
			}
			BigInteger bigInteger = k.ShiftLeft(1).Add(k);
			int bitLength = bigInteger.BitLength;
			int[] array = new int[bitLength >> 1];
			BigInteger bigInteger2 = bigInteger.Xor(k);
			int num = bitLength - 1;
			int num2 = 0;
			int num3 = 0;
			for (int i = 1; i < num; i++)
			{
				if (!bigInteger2.TestBit(i))
				{
					num3++;
				}
				else
				{
					int num4 = k.TestBit(i) ? -1 : 1;
					array[num2++] = (num4 << 16 | num3);
					num3 = 1;
					i++;
				}
			}
			array[num2++] = (65536 | num3);
			if (array.Length > num2)
			{
				array = WNafUtilities.Trim(array, num2);
			}
			return array;
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x000F143C File Offset: 0x000EF63C
		public static int[] GenerateCompactWindowNaf(int width, BigInteger k)
		{
			if (width == 2)
			{
				return WNafUtilities.GenerateCompactNaf(k);
			}
			if (width < 2 || width > 16)
			{
				throw new ArgumentException("must be in the range [2, 16]", "width");
			}
			if (k.BitLength >> 16 != 0)
			{
				throw new ArgumentException("must have bitlength < 2^16", "k");
			}
			if (k.SignValue == 0)
			{
				return Arrays.EmptyInts;
			}
			int[] array = new int[k.BitLength / width + 1];
			int num = 1 << width;
			int num2 = num - 1;
			int num3 = num >> 1;
			bool flag = false;
			int num4 = 0;
			int i = 0;
			while (i <= k.BitLength)
			{
				if (k.TestBit(i) == flag)
				{
					i++;
				}
				else
				{
					k = k.ShiftRight(i);
					int num5 = k.IntValue & num2;
					if (flag)
					{
						num5++;
					}
					flag = ((num5 & num3) != 0);
					if (flag)
					{
						num5 -= num;
					}
					int num6 = (num4 > 0) ? (i - 1) : i;
					array[num4++] = (num5 << 16 | num6);
					i = width;
				}
			}
			if (array.Length > num4)
			{
				array = WNafUtilities.Trim(array, num4);
			}
			return array;
		}

		// Token: 0x06002002 RID: 8194 RVA: 0x000F1544 File Offset: 0x000EF744
		public static byte[] GenerateJsf(BigInteger g, BigInteger h)
		{
			byte[] array = new byte[Math.Max(g.BitLength, h.BitLength) + 1];
			BigInteger bigInteger = g;
			BigInteger bigInteger2 = h;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			while ((num2 | num3) != 0 || bigInteger.BitLength > num4 || bigInteger2.BitLength > num4)
			{
				int num5 = (int)(((uint)bigInteger.IntValue >> num4) + (uint)num2 & 7U);
				int num6 = (int)(((uint)bigInteger2.IntValue >> num4) + (uint)num3 & 7U);
				int num7 = num5 & 1;
				if (num7 != 0)
				{
					num7 -= (num5 & 2);
					if (num5 + num7 == 4 && (num6 & 3) == 2)
					{
						num7 = -num7;
					}
				}
				int num8 = num6 & 1;
				if (num8 != 0)
				{
					num8 -= (num6 & 2);
					if (num6 + num8 == 4 && (num5 & 3) == 2)
					{
						num8 = -num8;
					}
				}
				if (num2 << 1 == 1 + num7)
				{
					num2 ^= 1;
				}
				if (num3 << 1 == 1 + num8)
				{
					num3 ^= 1;
				}
				if (++num4 == 30)
				{
					num4 = 0;
					bigInteger = bigInteger.ShiftRight(30);
					bigInteger2 = bigInteger2.ShiftRight(30);
				}
				array[num++] = (byte)(num7 << 4 | (num8 & 15));
			}
			if (array.Length > num)
			{
				array = WNafUtilities.Trim(array, num);
			}
			return array;
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x000F1678 File Offset: 0x000EF878
		public static byte[] GenerateNaf(BigInteger k)
		{
			if (k.SignValue == 0)
			{
				return Arrays.EmptyBytes;
			}
			BigInteger bigInteger = k.ShiftLeft(1).Add(k);
			int num = bigInteger.BitLength - 1;
			byte[] array = new byte[num];
			BigInteger bigInteger2 = bigInteger.Xor(k);
			for (int i = 1; i < num; i++)
			{
				if (bigInteger2.TestBit(i))
				{
					array[i - 1] = (byte)(k.TestBit(i) ? -1 : 1);
					i++;
				}
			}
			array[num - 1] = 1;
			return array;
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x000F16EC File Offset: 0x000EF8EC
		public static byte[] GenerateWindowNaf(int width, BigInteger k)
		{
			if (width == 2)
			{
				return WNafUtilities.GenerateNaf(k);
			}
			if (width < 2 || width > 8)
			{
				throw new ArgumentException("must be in the range [2, 8]", "width");
			}
			if (k.SignValue == 0)
			{
				return Arrays.EmptyBytes;
			}
			byte[] array = new byte[k.BitLength + 1];
			int num = 1 << width;
			int num2 = num - 1;
			int num3 = num >> 1;
			bool flag = false;
			int num4 = 0;
			int i = 0;
			while (i <= k.BitLength)
			{
				if (k.TestBit(i) == flag)
				{
					i++;
				}
				else
				{
					k = k.ShiftRight(i);
					int num5 = k.IntValue & num2;
					if (flag)
					{
						num5++;
					}
					flag = ((num5 & num3) != 0);
					if (flag)
					{
						num5 -= num;
					}
					num4 += ((num4 > 0) ? (i - 1) : i);
					array[num4++] = (byte)num5;
					i = width;
				}
			}
			if (array.Length > num4)
			{
				array = WNafUtilities.Trim(array, num4);
			}
			return array;
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x000F17D3 File Offset: 0x000EF9D3
		public static int GetNafWeight(BigInteger k)
		{
			if (k.SignValue == 0)
			{
				return 0;
			}
			return k.ShiftLeft(1).Add(k).Xor(k).BitCount;
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x000F17F7 File Offset: 0x000EF9F7
		public static WNafPreCompInfo GetWNafPreCompInfo(ECPoint p)
		{
			return WNafUtilities.GetWNafPreCompInfo(p.Curve.GetPreCompInfo(p, WNafUtilities.PRECOMP_NAME));
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x000F180F File Offset: 0x000EFA0F
		public static WNafPreCompInfo GetWNafPreCompInfo(PreCompInfo preCompInfo)
		{
			return preCompInfo as WNafPreCompInfo;
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x000F1817 File Offset: 0x000EFA17
		public static int GetWindowSize(int bits)
		{
			return WNafUtilities.GetWindowSize(bits, WNafUtilities.DEFAULT_WINDOW_SIZE_CUTOFFS);
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x000F1824 File Offset: 0x000EFA24
		public static int GetWindowSize(int bits, int[] windowSizeCutoffs)
		{
			int num = 0;
			while (num < windowSizeCutoffs.Length && bits >= windowSizeCutoffs[num])
			{
				num++;
			}
			return num + 2;
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x000F1848 File Offset: 0x000EFA48
		public static ECPoint MapPointWithPrecomp(ECPoint p, int width, bool includeNegated, ECPointMap pointMap)
		{
			ECCurve curve = p.Curve;
			WNafPreCompInfo wnafPreCompP = WNafUtilities.Precompute(p, width, includeNegated);
			ECPoint ecpoint = pointMap.Map(p);
			curve.Precompute(ecpoint, WNafUtilities.PRECOMP_NAME, new WNafUtilities.MapPointCallback(wnafPreCompP, includeNegated, pointMap));
			return ecpoint;
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x000F1881 File Offset: 0x000EFA81
		public static WNafPreCompInfo Precompute(ECPoint p, int width, bool includeNegated)
		{
			return (WNafPreCompInfo)p.Curve.Precompute(p, WNafUtilities.PRECOMP_NAME, new WNafUtilities.WNafCallback(p, width, includeNegated));
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x000F18A4 File Offset: 0x000EFAA4
		private static byte[] Trim(byte[] a, int length)
		{
			byte[] array = new byte[length];
			Array.Copy(a, 0, array, 0, array.Length);
			return array;
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x000F18C8 File Offset: 0x000EFAC8
		private static int[] Trim(int[] a, int length)
		{
			int[] array = new int[length];
			Array.Copy(a, 0, array, 0, array.Length);
			return array;
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x000F18EC File Offset: 0x000EFAEC
		private static ECPoint[] ResizeTable(ECPoint[] a, int length)
		{
			ECPoint[] array = new ECPoint[length];
			Array.Copy(a, 0, array, 0, a.Length);
			return array;
		}

		// Token: 0x040018AE RID: 6318
		public static readonly string PRECOMP_NAME = "bc_wnaf";

		// Token: 0x040018AF RID: 6319
		private static readonly int[] DEFAULT_WINDOW_SIZE_CUTOFFS = new int[]
		{
			13,
			41,
			121,
			337,
			897,
			2305
		};

		// Token: 0x040018B0 RID: 6320
		private static readonly ECPoint[] EMPTY_POINTS = new ECPoint[0];

		// Token: 0x020008F3 RID: 2291
		private class MapPointCallback : IPreCompCallback
		{
			// Token: 0x06004D9C RID: 19868 RVA: 0x001B13ED File Offset: 0x001AF5ED
			internal MapPointCallback(WNafPreCompInfo wnafPreCompP, bool includeNegated, ECPointMap pointMap)
			{
				this.m_wnafPreCompP = wnafPreCompP;
				this.m_includeNegated = includeNegated;
				this.m_pointMap = pointMap;
			}

			// Token: 0x06004D9D RID: 19869 RVA: 0x001B140C File Offset: 0x001AF60C
			public PreCompInfo Precompute(PreCompInfo existing)
			{
				WNafPreCompInfo wnafPreCompInfo = new WNafPreCompInfo();
				ECPoint twice = this.m_wnafPreCompP.Twice;
				if (twice != null)
				{
					ECPoint twice2 = this.m_pointMap.Map(twice);
					wnafPreCompInfo.Twice = twice2;
				}
				ECPoint[] preComp = this.m_wnafPreCompP.PreComp;
				ECPoint[] array = new ECPoint[preComp.Length];
				for (int i = 0; i < preComp.Length; i++)
				{
					array[i] = this.m_pointMap.Map(preComp[i]);
				}
				wnafPreCompInfo.PreComp = array;
				if (this.m_includeNegated)
				{
					ECPoint[] array2 = new ECPoint[array.Length];
					for (int j = 0; j < array2.Length; j++)
					{
						array2[j] = array[j].Negate();
					}
					wnafPreCompInfo.PreCompNeg = array2;
				}
				return wnafPreCompInfo;
			}

			// Token: 0x04003452 RID: 13394
			private readonly WNafPreCompInfo m_wnafPreCompP;

			// Token: 0x04003453 RID: 13395
			private readonly bool m_includeNegated;

			// Token: 0x04003454 RID: 13396
			private readonly ECPointMap m_pointMap;
		}

		// Token: 0x020008F4 RID: 2292
		private class WNafCallback : IPreCompCallback
		{
			// Token: 0x06004D9E RID: 19870 RVA: 0x001B14C1 File Offset: 0x001AF6C1
			internal WNafCallback(ECPoint p, int width, bool includeNegated)
			{
				this.m_p = p;
				this.m_width = width;
				this.m_includeNegated = includeNegated;
			}

			// Token: 0x06004D9F RID: 19871 RVA: 0x001B14E0 File Offset: 0x001AF6E0
			public PreCompInfo Precompute(PreCompInfo existing)
			{
				WNafPreCompInfo wnafPreCompInfo = existing as WNafPreCompInfo;
				int num = 1 << Math.Max(0, this.m_width - 2);
				if (this.CheckExisting(wnafPreCompInfo, num, this.m_includeNegated))
				{
					return wnafPreCompInfo;
				}
				ECCurve curve = this.m_p.Curve;
				ECPoint[] array = null;
				ECPoint[] array2 = null;
				ECPoint ecpoint = null;
				if (wnafPreCompInfo != null)
				{
					array = wnafPreCompInfo.PreComp;
					array2 = wnafPreCompInfo.PreCompNeg;
					ecpoint = wnafPreCompInfo.Twice;
				}
				int num2 = 0;
				if (array == null)
				{
					array = WNafUtilities.EMPTY_POINTS;
				}
				else
				{
					num2 = array.Length;
				}
				if (num2 < num)
				{
					array = WNafUtilities.ResizeTable(array, num);
					if (num == 1)
					{
						array[0] = this.m_p.Normalize();
					}
					else
					{
						int i = num2;
						if (i == 0)
						{
							array[0] = this.m_p;
							i = 1;
						}
						ECFieldElement ecfieldElement = null;
						if (num == 2)
						{
							array[1] = this.m_p.ThreeTimes();
						}
						else
						{
							ECPoint ecpoint2 = ecpoint;
							ECPoint ecpoint3 = array[i - 1];
							if (ecpoint2 == null)
							{
								ecpoint2 = array[0].Twice();
								ecpoint = ecpoint2;
								if (!ecpoint.IsInfinity && ECAlgorithms.IsFpCurve(curve) && curve.FieldSize >= 64)
								{
									int coordinateSystem = curve.CoordinateSystem;
									if (coordinateSystem - 2 <= 2)
									{
										ecfieldElement = ecpoint.GetZCoord(0);
										ecpoint2 = curve.CreatePoint(ecpoint.XCoord.ToBigInteger(), ecpoint.YCoord.ToBigInteger());
										ECFieldElement ecfieldElement2 = ecfieldElement.Square();
										ECFieldElement scale = ecfieldElement2.Multiply(ecfieldElement);
										ecpoint3 = ecpoint3.ScaleX(ecfieldElement2).ScaleY(scale);
										if (num2 == 0)
										{
											array[0] = ecpoint3;
										}
									}
								}
							}
							while (i < num)
							{
								ecpoint3 = (array[i++] = ecpoint3.Add(ecpoint2));
							}
						}
						curve.NormalizeAll(array, num2, num - num2, ecfieldElement);
					}
				}
				if (this.m_includeNegated)
				{
					int j;
					if (array2 == null)
					{
						j = 0;
						array2 = new ECPoint[num];
					}
					else
					{
						j = array2.Length;
						if (j < num)
						{
							array2 = WNafUtilities.ResizeTable(array2, num);
						}
					}
					while (j < num)
					{
						array2[j] = array[j].Negate();
						j++;
					}
				}
				return new WNafPreCompInfo
				{
					PreComp = array,
					PreCompNeg = array2,
					Twice = ecpoint
				};
			}

			// Token: 0x06004DA0 RID: 19872 RVA: 0x001B16EA File Offset: 0x001AF8EA
			private bool CheckExisting(WNafPreCompInfo existingWNaf, int reqPreCompLen, bool includeNegated)
			{
				return existingWNaf != null && this.CheckTable(existingWNaf.PreComp, reqPreCompLen) && (!includeNegated || this.CheckTable(existingWNaf.PreCompNeg, reqPreCompLen));
			}

			// Token: 0x06004DA1 RID: 19873 RVA: 0x001B1712 File Offset: 0x001AF912
			private bool CheckTable(ECPoint[] table, int reqLen)
			{
				return table != null && table.Length >= reqLen;
			}

			// Token: 0x04003455 RID: 13397
			private readonly ECPoint m_p;

			// Token: 0x04003456 RID: 13398
			private readonly int m_width;

			// Token: 0x04003457 RID: 13399
			private readonly bool m_includeNegated;
		}
	}
}
