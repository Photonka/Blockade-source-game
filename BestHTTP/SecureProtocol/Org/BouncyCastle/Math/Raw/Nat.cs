using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw
{
	// Token: 0x020002F3 RID: 755
	internal abstract class Nat
	{
		// Token: 0x06001C18 RID: 7192 RVA: 0x000D8190 File Offset: 0x000D6390
		public static uint Add(int len, uint[] x, uint[] y, uint[] z)
		{
			ulong num = 0UL;
			for (int i = 0; i < len; i++)
			{
				num += (ulong)x[i] + (ulong)y[i];
				z[i] = (uint)num;
				num >>= 32;
			}
			return (uint)num;
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x000D81C4 File Offset: 0x000D63C4
		public static uint Add33At(int len, uint x, uint[] z, int zPos)
		{
			ulong num = (ulong)z[zPos] + (ulong)x;
			z[zPos] = (uint)num;
			num >>= 32;
			num += (ulong)z[zPos + 1] + 1UL;
			z[zPos + 1] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, zPos + 2);
			}
			return 0U;
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x000D820C File Offset: 0x000D640C
		public static uint Add33At(int len, uint x, uint[] z, int zOff, int zPos)
		{
			ulong num = (ulong)z[zOff + zPos] + (ulong)x;
			z[zOff + zPos] = (uint)num;
			num >>= 32;
			num += (ulong)z[zOff + zPos + 1] + 1UL;
			z[zOff + zPos + 1] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, zOff, zPos + 2);
			}
			return 0U;
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x000D8260 File Offset: 0x000D6460
		public static uint Add33To(int len, uint x, uint[] z)
		{
			ulong num = (ulong)z[0] + (ulong)x;
			z[0] = (uint)num;
			num >>= 32;
			num += (ulong)z[1] + 1UL;
			z[1] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, 2);
			}
			return 0U;
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x000D82A0 File Offset: 0x000D64A0
		public static uint Add33To(int len, uint x, uint[] z, int zOff)
		{
			ulong num = (ulong)z[zOff] + (ulong)x;
			z[zOff] = (uint)num;
			num >>= 32;
			num += (ulong)z[zOff + 1] + 1UL;
			z[zOff + 1] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, zOff, 2);
			}
			return 0U;
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x000D82E8 File Offset: 0x000D64E8
		public static uint AddBothTo(int len, uint[] x, uint[] y, uint[] z)
		{
			ulong num = 0UL;
			for (int i = 0; i < len; i++)
			{
				num += (ulong)x[i] + (ulong)y[i] + (ulong)z[i];
				z[i] = (uint)num;
				num >>= 32;
			}
			return (uint)num;
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x000D8324 File Offset: 0x000D6524
		public static uint AddBothTo(int len, uint[] x, int xOff, uint[] y, int yOff, uint[] z, int zOff)
		{
			ulong num = 0UL;
			for (int i = 0; i < len; i++)
			{
				num += (ulong)x[xOff + i] + (ulong)y[yOff + i] + (ulong)z[zOff + i];
				z[zOff + i] = (uint)num;
				num >>= 32;
			}
			return (uint)num;
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x000D836C File Offset: 0x000D656C
		public static uint AddDWordAt(int len, ulong x, uint[] z, int zPos)
		{
			ulong num = (ulong)z[zPos] + (x & (ulong)-1);
			z[zPos] = (uint)num;
			num >>= 32;
			num += (ulong)z[zPos + 1] + (x >> 32);
			z[zPos + 1] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, zPos + 2);
			}
			return 0U;
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x000D83B8 File Offset: 0x000D65B8
		public static uint AddDWordAt(int len, ulong x, uint[] z, int zOff, int zPos)
		{
			ulong num = (ulong)z[zOff + zPos] + (x & (ulong)-1);
			z[zOff + zPos] = (uint)num;
			num >>= 32;
			num += (ulong)z[zOff + zPos + 1] + (x >> 32);
			z[zOff + zPos + 1] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, zOff, zPos + 2);
			}
			return 0U;
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x000D8410 File Offset: 0x000D6610
		public static uint AddDWordTo(int len, ulong x, uint[] z)
		{
			ulong num = (ulong)z[0] + (x & (ulong)-1);
			z[0] = (uint)num;
			num >>= 32;
			num += (ulong)z[1] + (x >> 32);
			z[1] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, 2);
			}
			return 0U;
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x000D8454 File Offset: 0x000D6654
		public static uint AddDWordTo(int len, ulong x, uint[] z, int zOff)
		{
			ulong num = (ulong)z[zOff] + (x & (ulong)-1);
			z[zOff] = (uint)num;
			num >>= 32;
			num += (ulong)z[zOff + 1] + (x >> 32);
			z[zOff + 1] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, zOff, 2);
			}
			return 0U;
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x000D84A0 File Offset: 0x000D66A0
		public static uint AddTo(int len, uint[] x, uint[] z)
		{
			ulong num = 0UL;
			for (int i = 0; i < len; i++)
			{
				num += (ulong)x[i] + (ulong)z[i];
				z[i] = (uint)num;
				num >>= 32;
			}
			return (uint)num;
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x000D84D4 File Offset: 0x000D66D4
		public static uint AddTo(int len, uint[] x, int xOff, uint[] z, int zOff)
		{
			ulong num = 0UL;
			for (int i = 0; i < len; i++)
			{
				num += (ulong)x[xOff + i] + (ulong)z[zOff + i];
				z[zOff + i] = (uint)num;
				num >>= 32;
			}
			return (uint)num;
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x000D8510 File Offset: 0x000D6710
		public static uint AddWordAt(int len, uint x, uint[] z, int zPos)
		{
			ulong num = (ulong)x + (ulong)z[zPos];
			z[zPos] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, zPos + 1);
			}
			return 0U;
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x000D8540 File Offset: 0x000D6740
		public static uint AddWordAt(int len, uint x, uint[] z, int zOff, int zPos)
		{
			ulong num = (ulong)x + (ulong)z[zOff + zPos];
			z[zOff + zPos] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, zOff, zPos + 1);
			}
			return 0U;
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x000D8578 File Offset: 0x000D6778
		public static uint AddWordTo(int len, uint x, uint[] z)
		{
			ulong num = (ulong)x + (ulong)z[0];
			z[0] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, 1);
			}
			return 0U;
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x000D85A4 File Offset: 0x000D67A4
		public static uint AddWordTo(int len, uint x, uint[] z, int zOff)
		{
			ulong num = (ulong)x + (ulong)z[zOff];
			z[zOff] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, zOff, 1);
			}
			return 0U;
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x000D85D4 File Offset: 0x000D67D4
		public static uint CAdd(int len, int mask, uint[] x, uint[] y, uint[] z)
		{
			uint num = (uint)(-(uint)(mask & 1));
			ulong num2 = 0UL;
			for (int i = 0; i < len; i++)
			{
				num2 += (ulong)x[i] + (ulong)(y[i] & num);
				z[i] = (uint)num2;
				num2 >>= 32;
			}
			return (uint)num2;
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x000D8610 File Offset: 0x000D6810
		public static void CMov(int len, int mask, uint[] x, int xOff, uint[] z, int zOff)
		{
			uint num = (uint)(-(uint)(mask & 1));
			for (int i = 0; i < len; i++)
			{
				uint num2 = z[zOff + i];
				uint num3 = num2 ^ x[xOff + i];
				num2 ^= (num3 & num);
				z[zOff + i] = num2;
			}
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x000D864C File Offset: 0x000D684C
		public static void CMov(int len, int mask, int[] x, int xOff, int[] z, int zOff)
		{
			mask = -(mask & 1);
			for (int i = 0; i < len; i++)
			{
				int num = z[zOff + i];
				int num2 = num ^ x[xOff + i];
				num ^= (num2 & mask);
				z[zOff + i] = num;
			}
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x000D8689 File Offset: 0x000D6889
		public static void Copy(int len, uint[] x, uint[] z)
		{
			Array.Copy(x, 0, z, 0, len);
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x000D8698 File Offset: 0x000D6898
		public static uint[] Copy(int len, uint[] x)
		{
			uint[] array = new uint[len];
			Array.Copy(x, 0, array, 0, len);
			return array;
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x000D86B7 File Offset: 0x000D68B7
		public static void Copy(int len, uint[] x, int xOff, uint[] z, int zOff)
		{
			Array.Copy(x, xOff, z, zOff, len);
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x000D86C4 File Offset: 0x000D68C4
		public static uint[] Create(int len)
		{
			return new uint[len];
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x000D86CC File Offset: 0x000D68CC
		public static ulong[] Create64(int len)
		{
			return new ulong[len];
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x000D86D4 File Offset: 0x000D68D4
		public static int Dec(int len, uint[] z)
		{
			for (int i = 0; i < len; i++)
			{
				int num = i;
				uint num2 = z[num] - 1U;
				z[num] = num2;
				if (num2 != 4294967295U)
				{
					return 0;
				}
			}
			return -1;
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x000D8704 File Offset: 0x000D6904
		public static int Dec(int len, uint[] x, uint[] z)
		{
			int i = 0;
			while (i < len)
			{
				uint num = x[i] - 1U;
				z[i] = num;
				i++;
				if (num != 4294967295U)
				{
					while (i < len)
					{
						z[i] = x[i];
						i++;
					}
					return 0;
				}
			}
			return -1;
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x000D8740 File Offset: 0x000D6940
		public static int DecAt(int len, uint[] z, int zPos)
		{
			for (int i = zPos; i < len; i++)
			{
				int num = i;
				uint num2 = z[num] - 1U;
				z[num] = num2;
				if (num2 != 4294967295U)
				{
					return 0;
				}
			}
			return -1;
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x000D8770 File Offset: 0x000D6970
		public static int DecAt(int len, uint[] z, int zOff, int zPos)
		{
			for (int i = zPos; i < len; i++)
			{
				int num = zOff + i;
				uint num2 = z[num] - 1U;
				z[num] = num2;
				if (num2 != 4294967295U)
				{
					return 0;
				}
			}
			return -1;
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x000D87A0 File Offset: 0x000D69A0
		public static bool Eq(int len, uint[] x, uint[] y)
		{
			for (int i = len - 1; i >= 0; i--)
			{
				if (x[i] != y[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x000D87C8 File Offset: 0x000D69C8
		public static uint[] FromBigInteger(int bits, BigInteger x)
		{
			if (x.SignValue < 0 || x.BitLength > bits)
			{
				throw new ArgumentException();
			}
			uint[] array = Nat.Create(bits + 31 >> 5);
			int num = 0;
			while (x.SignValue != 0)
			{
				array[num++] = (uint)x.IntValue;
				x = x.ShiftRight(32);
			}
			return array;
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x000D8820 File Offset: 0x000D6A20
		public static uint GetBit(uint[] x, int bit)
		{
			if (bit == 0)
			{
				return x[0] & 1U;
			}
			int num = bit >> 5;
			if (num < 0 || num >= x.Length)
			{
				return 0U;
			}
			int num2 = bit & 31;
			return x[num] >> num2 & 1U;
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x000D8858 File Offset: 0x000D6A58
		public static bool Gte(int len, uint[] x, uint[] y)
		{
			for (int i = len - 1; i >= 0; i--)
			{
				uint num = x[i];
				uint num2 = y[i];
				if (num < num2)
				{
					return false;
				}
				if (num > num2)
				{
					return true;
				}
			}
			return true;
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x000D8888 File Offset: 0x000D6A88
		public static uint Inc(int len, uint[] z)
		{
			for (int i = 0; i < len; i++)
			{
				int num = i;
				uint num2 = z[num] + 1U;
				z[num] = num2;
				if (num2 != 0U)
				{
					return 0U;
				}
			}
			return 1U;
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x000D88B8 File Offset: 0x000D6AB8
		public static uint Inc(int len, uint[] x, uint[] z)
		{
			int i = 0;
			while (i < len)
			{
				uint num = x[i] + 1U;
				z[i] = num;
				i++;
				if (num != 0U)
				{
					while (i < len)
					{
						z[i] = x[i];
						i++;
					}
					return 0U;
				}
			}
			return 1U;
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x000D88F4 File Offset: 0x000D6AF4
		public static uint IncAt(int len, uint[] z, int zPos)
		{
			for (int i = zPos; i < len; i++)
			{
				int num = i;
				uint num2 = z[num] + 1U;
				z[num] = num2;
				if (num2 != 0U)
				{
					return 0U;
				}
			}
			return 1U;
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x000D8924 File Offset: 0x000D6B24
		public static uint IncAt(int len, uint[] z, int zOff, int zPos)
		{
			for (int i = zPos; i < len; i++)
			{
				int num = zOff + i;
				uint num2 = z[num] + 1U;
				z[num] = num2;
				if (num2 != 0U)
				{
					return 0U;
				}
			}
			return 1U;
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x000D8954 File Offset: 0x000D6B54
		public static bool IsOne(int len, uint[] x)
		{
			if (x[0] != 1U)
			{
				return false;
			}
			for (int i = 1; i < len; i++)
			{
				if (x[i] != 0U)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x000D8980 File Offset: 0x000D6B80
		public static bool IsZero(int len, uint[] x)
		{
			if (x[0] != 0U)
			{
				return false;
			}
			for (int i = 1; i < len; i++)
			{
				if (x[i] != 0U)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x000D89A8 File Offset: 0x000D6BA8
		public static void Mul(int len, uint[] x, uint[] y, uint[] zz)
		{
			zz[len] = Nat.MulWord(len, x[0], y, zz);
			for (int i = 1; i < len; i++)
			{
				zz[i + len] = Nat.MulWordAddTo(len, x[i], y, 0, zz, i);
			}
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x000D89E4 File Offset: 0x000D6BE4
		public static void Mul(int len, uint[] x, int xOff, uint[] y, int yOff, uint[] zz, int zzOff)
		{
			zz[zzOff + len] = Nat.MulWord(len, x[xOff], y, yOff, zz, zzOff);
			for (int i = 1; i < len; i++)
			{
				zz[zzOff + i + len] = Nat.MulWordAddTo(len, x[xOff + i], y, yOff, zz, zzOff + i);
			}
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x000D8A34 File Offset: 0x000D6C34
		public static void Mul(uint[] x, int xOff, int xLen, uint[] y, int yOff, int yLen, uint[] zz, int zzOff)
		{
			zz[zzOff + yLen] = Nat.MulWord(yLen, x[xOff], y, yOff, zz, zzOff);
			for (int i = 1; i < xLen; i++)
			{
				zz[zzOff + i + yLen] = Nat.MulWordAddTo(yLen, x[xOff + i], y, yOff, zz, zzOff + i);
			}
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x000D8A88 File Offset: 0x000D6C88
		public static uint MulAddTo(int len, uint[] x, uint[] y, uint[] zz)
		{
			ulong num = 0UL;
			for (int i = 0; i < len; i++)
			{
				ulong num2 = (ulong)Nat.MulWordAddTo(len, x[i], y, 0, zz, i) & (ulong)-1;
				num2 += num + ((ulong)zz[i + len] & (ulong)-1);
				zz[i + len] = (uint)num2;
				num = num2 >> 32;
			}
			return (uint)num;
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x000D8AD4 File Offset: 0x000D6CD4
		public static uint MulAddTo(int len, uint[] x, int xOff, uint[] y, int yOff, uint[] zz, int zzOff)
		{
			ulong num = 0UL;
			for (int i = 0; i < len; i++)
			{
				ulong num2 = (ulong)Nat.MulWordAddTo(len, x[xOff + i], y, yOff, zz, zzOff) & (ulong)-1;
				num2 += num + ((ulong)zz[zzOff + len] & (ulong)-1);
				zz[zzOff + len] = (uint)num2;
				num = num2 >> 32;
				zzOff++;
			}
			return (uint)num;
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x000D8B30 File Offset: 0x000D6D30
		public static uint Mul31BothAdd(int len, uint a, uint[] x, uint b, uint[] y, uint[] z, int zOff)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)a;
			ulong num3 = (ulong)b;
			int num4 = 0;
			do
			{
				num += num2 * (ulong)x[num4] + num3 * (ulong)y[num4] + (ulong)z[zOff + num4];
				z[zOff + num4] = (uint)num;
				num >>= 32;
			}
			while (++num4 < len);
			return (uint)num;
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x000D8B7C File Offset: 0x000D6D7C
		public static uint MulWord(int len, uint x, uint[] y, uint[] z)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x;
			int num3 = 0;
			do
			{
				num += num2 * (ulong)y[num3];
				z[num3] = (uint)num;
				num >>= 32;
			}
			while (++num3 < len);
			return (uint)num;
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x000D8BB0 File Offset: 0x000D6DB0
		public static uint MulWord(int len, uint x, uint[] y, int yOff, uint[] z, int zOff)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x;
			int num3 = 0;
			do
			{
				num += num2 * (ulong)y[yOff + num3];
				z[zOff + num3] = (uint)num;
				num >>= 32;
			}
			while (++num3 < len);
			return (uint)num;
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x000D8BE8 File Offset: 0x000D6DE8
		public static uint MulWordAddTo(int len, uint x, uint[] y, int yOff, uint[] z, int zOff)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x;
			int num3 = 0;
			do
			{
				num += num2 * (ulong)y[yOff + num3] + (ulong)z[zOff + num3];
				z[zOff + num3] = (uint)num;
				num >>= 32;
			}
			while (++num3 < len);
			return (uint)num;
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x000D8C2C File Offset: 0x000D6E2C
		public static uint MulWordDwordAddAt(int len, uint x, ulong y, uint[] z, int zPos)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x;
			num += num2 * (ulong)((uint)y) + (ulong)z[zPos];
			z[zPos] = (uint)num;
			num >>= 32;
			num += num2 * (y >> 32) + (ulong)z[zPos + 1];
			z[zPos + 1] = (uint)num;
			num >>= 32;
			num += (ulong)z[zPos + 2];
			z[zPos + 2] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(len, z, zPos + 3);
			}
			return 0U;
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x000D8CA0 File Offset: 0x000D6EA0
		public static uint ShiftDownBit(int len, uint[] z, uint c)
		{
			int num = len;
			while (--num >= 0)
			{
				uint num2 = z[num];
				z[num] = (num2 >> 1 | c << 31);
				c = num2;
			}
			return c << 31;
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x000D8CD0 File Offset: 0x000D6ED0
		public static uint ShiftDownBit(int len, uint[] z, int zOff, uint c)
		{
			int num = len;
			while (--num >= 0)
			{
				uint num2 = z[zOff + num];
				z[zOff + num] = (num2 >> 1 | c << 31);
				c = num2;
			}
			return c << 31;
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x000D8D04 File Offset: 0x000D6F04
		public static uint ShiftDownBit(int len, uint[] x, uint c, uint[] z)
		{
			int num = len;
			while (--num >= 0)
			{
				uint num2 = x[num];
				z[num] = (num2 >> 1 | c << 31);
				c = num2;
			}
			return c << 31;
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x000D8D34 File Offset: 0x000D6F34
		public static uint ShiftDownBit(int len, uint[] x, int xOff, uint c, uint[] z, int zOff)
		{
			int num = len;
			while (--num >= 0)
			{
				uint num2 = x[xOff + num];
				z[zOff + num] = (num2 >> 1 | c << 31);
				c = num2;
			}
			return c << 31;
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x000D8D6C File Offset: 0x000D6F6C
		public static uint ShiftDownBits(int len, uint[] z, int bits, uint c)
		{
			int num = len;
			while (--num >= 0)
			{
				uint num2 = z[num];
				z[num] = (num2 >> bits | c << -bits);
				c = num2;
			}
			return c << -bits;
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x000D8DA4 File Offset: 0x000D6FA4
		public static uint ShiftDownBits(int len, uint[] z, int zOff, int bits, uint c)
		{
			int num = len;
			while (--num >= 0)
			{
				uint num2 = z[zOff + num];
				z[zOff + num] = (num2 >> bits | c << -bits);
				c = num2;
			}
			return c << -bits;
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x000D8DE4 File Offset: 0x000D6FE4
		public static uint ShiftDownBits(int len, uint[] x, int bits, uint c, uint[] z)
		{
			int num = len;
			while (--num >= 0)
			{
				uint num2 = x[num];
				z[num] = (num2 >> bits | c << -bits);
				c = num2;
			}
			return c << -bits;
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x000D8E20 File Offset: 0x000D7020
		public static uint ShiftDownBits(int len, uint[] x, int xOff, int bits, uint c, uint[] z, int zOff)
		{
			int num = len;
			while (--num >= 0)
			{
				uint num2 = x[xOff + num];
				z[zOff + num] = (num2 >> bits | c << -bits);
				c = num2;
			}
			return c << -bits;
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x000D8E60 File Offset: 0x000D7060
		public static uint ShiftDownWord(int len, uint[] z, uint c)
		{
			int num = len;
			while (--num >= 0)
			{
				uint num2 = z[num];
				z[num] = c;
				c = num2;
			}
			return c;
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x000D8E84 File Offset: 0x000D7084
		public static uint ShiftUpBit(int len, uint[] z, uint c)
		{
			for (int i = 0; i < len; i++)
			{
				uint num = z[i];
				z[i] = (num << 1 | c >> 31);
				c = num;
			}
			return c >> 31;
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x000D8EB4 File Offset: 0x000D70B4
		public static uint ShiftUpBit(int len, uint[] z, int zOff, uint c)
		{
			for (int i = 0; i < len; i++)
			{
				uint num = z[zOff + i];
				z[zOff + i] = (num << 1 | c >> 31);
				c = num;
			}
			return c >> 31;
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x000D8EE8 File Offset: 0x000D70E8
		public static uint ShiftUpBit(int len, uint[] x, uint c, uint[] z)
		{
			for (int i = 0; i < len; i++)
			{
				uint num = x[i];
				z[i] = (num << 1 | c >> 31);
				c = num;
			}
			return c >> 31;
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x000D8F18 File Offset: 0x000D7118
		public static uint ShiftUpBit(int len, uint[] x, int xOff, uint c, uint[] z, int zOff)
		{
			for (int i = 0; i < len; i++)
			{
				uint num = x[xOff + i];
				z[zOff + i] = (num << 1 | c >> 31);
				c = num;
			}
			return c >> 31;
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x000D8F50 File Offset: 0x000D7150
		public static ulong ShiftUpBit64(int len, ulong[] x, int xOff, ulong c, ulong[] z, int zOff)
		{
			for (int i = 0; i < len; i++)
			{
				ulong num = x[xOff + i];
				z[zOff + i] = (num << 1 | c >> 63);
				c = num;
			}
			return c >> 63;
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x000D8F88 File Offset: 0x000D7188
		public static uint ShiftUpBits(int len, uint[] z, int bits, uint c)
		{
			for (int i = 0; i < len; i++)
			{
				uint num = z[i];
				z[i] = (num << bits | c >> -bits);
				c = num;
			}
			return c >> -bits;
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x000D8FC0 File Offset: 0x000D71C0
		public static uint ShiftUpBits(int len, uint[] z, int zOff, int bits, uint c)
		{
			for (int i = 0; i < len; i++)
			{
				uint num = z[zOff + i];
				z[zOff + i] = (num << bits | c >> -bits);
				c = num;
			}
			return c >> -bits;
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x000D9000 File Offset: 0x000D7200
		public static ulong ShiftUpBits64(int len, ulong[] z, int zOff, int bits, ulong c)
		{
			for (int i = 0; i < len; i++)
			{
				ulong num = z[zOff + i];
				z[zOff + i] = (num << bits | c >> -bits);
				c = num;
			}
			return c >> -bits;
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x000D9040 File Offset: 0x000D7240
		public static uint ShiftUpBits(int len, uint[] x, int bits, uint c, uint[] z)
		{
			for (int i = 0; i < len; i++)
			{
				uint num = x[i];
				z[i] = (num << bits | c >> -bits);
				c = num;
			}
			return c >> -bits;
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x000D907C File Offset: 0x000D727C
		public static uint ShiftUpBits(int len, uint[] x, int xOff, int bits, uint c, uint[] z, int zOff)
		{
			for (int i = 0; i < len; i++)
			{
				uint num = x[xOff + i];
				z[zOff + i] = (num << bits | c >> -bits);
				c = num;
			}
			return c >> -bits;
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x000D90BC File Offset: 0x000D72BC
		public static ulong ShiftUpBits64(int len, ulong[] x, int xOff, int bits, ulong c, ulong[] z, int zOff)
		{
			for (int i = 0; i < len; i++)
			{
				ulong num = x[xOff + i];
				z[zOff + i] = (num << bits | c >> -bits);
				c = num;
			}
			return c >> -bits;
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x000D90FC File Offset: 0x000D72FC
		public static void Square(int len, uint[] x, uint[] zz)
		{
			int num = len << 1;
			uint num2 = 0U;
			int num3 = len;
			int num4 = num;
			do
			{
				ulong num5 = (ulong)x[--num3];
				ulong num6 = num5 * num5;
				zz[--num4] = (num2 << 31 | (uint)(num6 >> 33));
				zz[--num4] = (uint)(num6 >> 1);
				num2 = (uint)num6;
			}
			while (num3 > 0);
			for (int i = 1; i < len; i++)
			{
				num2 = Nat.SquareWordAdd(x, i, zz);
				Nat.AddWordAt(num, num2, zz, i << 1);
			}
			Nat.ShiftUpBit(num, zz, x[0] << 31);
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x000D917C File Offset: 0x000D737C
		public static void Square(int len, uint[] x, int xOff, uint[] zz, int zzOff)
		{
			int num = len << 1;
			uint num2 = 0U;
			int num3 = len;
			int num4 = num;
			do
			{
				ulong num5 = (ulong)x[xOff + --num3];
				ulong num6 = num5 * num5;
				zz[zzOff + --num4] = (num2 << 31 | (uint)(num6 >> 33));
				zz[zzOff + --num4] = (uint)(num6 >> 1);
				num2 = (uint)num6;
			}
			while (num3 > 0);
			for (int i = 1; i < len; i++)
			{
				num2 = Nat.SquareWordAdd(x, xOff, i, zz, zzOff);
				Nat.AddWordAt(num, num2, zz, zzOff, i << 1);
			}
			Nat.ShiftUpBit(num, zz, zzOff, x[xOff] << 31);
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x000D920C File Offset: 0x000D740C
		public static uint SquareWordAdd(uint[] x, int xPos, uint[] z)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x[xPos];
			int num3 = 0;
			do
			{
				num += num2 * (ulong)x[num3] + (ulong)z[xPos + num3];
				z[xPos + num3] = (uint)num;
				num >>= 32;
			}
			while (++num3 < xPos);
			return (uint)num;
		}

		// Token: 0x06001C60 RID: 7264 RVA: 0x000D924C File Offset: 0x000D744C
		public static uint SquareWordAdd(uint[] x, int xOff, int xPos, uint[] z, int zOff)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x[xOff + xPos];
			int num3 = 0;
			do
			{
				num += num2 * ((ulong)x[xOff + num3] & (ulong)-1) + ((ulong)z[xPos + zOff] & (ulong)-1);
				z[xPos + zOff] = (uint)num;
				num >>= 32;
				zOff++;
			}
			while (++num3 < xPos);
			return (uint)num;
		}

		// Token: 0x06001C61 RID: 7265 RVA: 0x000D929C File Offset: 0x000D749C
		public static int Sub(int len, uint[] x, uint[] y, uint[] z)
		{
			long num = 0L;
			for (int i = 0; i < len; i++)
			{
				num += (long)((ulong)x[i] - (ulong)y[i]);
				z[i] = (uint)num;
				num >>= 32;
			}
			return (int)num;
		}

		// Token: 0x06001C62 RID: 7266 RVA: 0x000D92D0 File Offset: 0x000D74D0
		public static int Sub(int len, uint[] x, int xOff, uint[] y, int yOff, uint[] z, int zOff)
		{
			long num = 0L;
			for (int i = 0; i < len; i++)
			{
				num += (long)((ulong)x[xOff + i] - (ulong)y[yOff + i]);
				z[zOff + i] = (uint)num;
				num >>= 32;
			}
			return (int)num;
		}

		// Token: 0x06001C63 RID: 7267 RVA: 0x000D9310 File Offset: 0x000D7510
		public static int Sub33At(int len, uint x, uint[] z, int zPos)
		{
			long num = (long)((ulong)z[zPos] - (ulong)x);
			z[zPos] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[zPos + 1] - 1UL);
			z[zPos + 1] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				return Nat.DecAt(len, z, zPos + 2);
			}
			return 0;
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x000D9358 File Offset: 0x000D7558
		public static int Sub33At(int len, uint x, uint[] z, int zOff, int zPos)
		{
			long num = (long)((ulong)z[zOff + zPos] - (ulong)x);
			z[zOff + zPos] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[zOff + zPos + 1] - 1UL);
			z[zOff + zPos + 1] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				return Nat.DecAt(len, z, zOff, zPos + 2);
			}
			return 0;
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x000D93AC File Offset: 0x000D75AC
		public static int Sub33From(int len, uint x, uint[] z)
		{
			long num = (long)((ulong)z[0] - (ulong)x);
			z[0] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[1] - 1UL);
			z[1] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				return Nat.DecAt(len, z, 2);
			}
			return 0;
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x000D93EC File Offset: 0x000D75EC
		public static int Sub33From(int len, uint x, uint[] z, int zOff)
		{
			long num = (long)((ulong)z[zOff] - (ulong)x);
			z[zOff] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[zOff + 1] - 1UL);
			z[zOff + 1] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				return Nat.DecAt(len, z, zOff, 2);
			}
			return 0;
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x000D9434 File Offset: 0x000D7634
		public static int SubBothFrom(int len, uint[] x, uint[] y, uint[] z)
		{
			long num = 0L;
			for (int i = 0; i < len; i++)
			{
				num += (long)((ulong)z[i] - (ulong)x[i] - (ulong)y[i]);
				z[i] = (uint)num;
				num >>= 32;
			}
			return (int)num;
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x000D9470 File Offset: 0x000D7670
		public static int SubBothFrom(int len, uint[] x, int xOff, uint[] y, int yOff, uint[] z, int zOff)
		{
			long num = 0L;
			for (int i = 0; i < len; i++)
			{
				num += (long)((ulong)z[zOff + i] - (ulong)x[xOff + i] - (ulong)y[yOff + i]);
				z[zOff + i] = (uint)num;
				num >>= 32;
			}
			return (int)num;
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x000D94B8 File Offset: 0x000D76B8
		public static int SubDWordAt(int len, ulong x, uint[] z, int zPos)
		{
			long num = (long)((ulong)z[zPos] - (x & (ulong)-1));
			z[zPos] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[zPos + 1] - (x >> 32));
			z[zPos + 1] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				return Nat.DecAt(len, z, zPos + 2);
			}
			return 0;
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x000D9504 File Offset: 0x000D7704
		public static int SubDWordAt(int len, ulong x, uint[] z, int zOff, int zPos)
		{
			long num = (long)((ulong)z[zOff + zPos] - (x & (ulong)-1));
			z[zOff + zPos] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[zOff + zPos + 1] - (x >> 32));
			z[zOff + zPos + 1] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				return Nat.DecAt(len, z, zOff, zPos + 2);
			}
			return 0;
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x000D955C File Offset: 0x000D775C
		public static int SubDWordFrom(int len, ulong x, uint[] z)
		{
			long num = (long)((ulong)z[0] - (x & (ulong)-1));
			z[0] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[1] - (x >> 32));
			z[1] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				return Nat.DecAt(len, z, 2);
			}
			return 0;
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x000D95A0 File Offset: 0x000D77A0
		public static int SubDWordFrom(int len, ulong x, uint[] z, int zOff)
		{
			long num = (long)((ulong)z[zOff] - (x & (ulong)-1));
			z[zOff] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[zOff + 1] - (x >> 32));
			z[zOff + 1] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				return Nat.DecAt(len, z, zOff, 2);
			}
			return 0;
		}

		// Token: 0x06001C6D RID: 7277 RVA: 0x000D95EC File Offset: 0x000D77EC
		public static int SubFrom(int len, uint[] x, uint[] z)
		{
			long num = 0L;
			for (int i = 0; i < len; i++)
			{
				num += (long)((ulong)z[i] - (ulong)x[i]);
				z[i] = (uint)num;
				num >>= 32;
			}
			return (int)num;
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x000D9620 File Offset: 0x000D7820
		public static int SubFrom(int len, uint[] x, int xOff, uint[] z, int zOff)
		{
			long num = 0L;
			for (int i = 0; i < len; i++)
			{
				num += (long)((ulong)z[zOff + i] - (ulong)x[xOff + i]);
				z[zOff + i] = (uint)num;
				num >>= 32;
			}
			return (int)num;
		}

		// Token: 0x06001C6F RID: 7279 RVA: 0x000D965C File Offset: 0x000D785C
		public static int SubWordAt(int len, uint x, uint[] z, int zPos)
		{
			long num = (long)((ulong)z[zPos] - (ulong)x);
			z[zPos] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				return Nat.DecAt(len, z, zPos + 1);
			}
			return 0;
		}

		// Token: 0x06001C70 RID: 7280 RVA: 0x000D968C File Offset: 0x000D788C
		public static int SubWordAt(int len, uint x, uint[] z, int zOff, int zPos)
		{
			long num = (long)((ulong)z[zOff + zPos] - (ulong)x);
			z[zOff + zPos] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				return Nat.DecAt(len, z, zOff, zPos + 1);
			}
			return 0;
		}

		// Token: 0x06001C71 RID: 7281 RVA: 0x000D96C4 File Offset: 0x000D78C4
		public static int SubWordFrom(int len, uint x, uint[] z)
		{
			long num = (long)((ulong)z[0] - (ulong)x);
			z[0] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				return Nat.DecAt(len, z, 1);
			}
			return 0;
		}

		// Token: 0x06001C72 RID: 7282 RVA: 0x000D96F0 File Offset: 0x000D78F0
		public static int SubWordFrom(int len, uint x, uint[] z, int zOff)
		{
			long num = (long)((ulong)z[zOff] - (ulong)x);
			z[zOff] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				return Nat.DecAt(len, z, zOff, 1);
			}
			return 0;
		}

		// Token: 0x06001C73 RID: 7283 RVA: 0x000D9720 File Offset: 0x000D7920
		public static BigInteger ToBigInteger(int len, uint[] x)
		{
			byte[] array = new byte[len << 2];
			for (int i = 0; i < len; i++)
			{
				uint num = x[i];
				if (num != 0U)
				{
					Pack.UInt32_To_BE(num, array, len - 1 - i << 2);
				}
			}
			return new BigInteger(1, array);
		}

		// Token: 0x06001C74 RID: 7284 RVA: 0x000D9760 File Offset: 0x000D7960
		public static void Zero(int len, uint[] z)
		{
			for (int i = 0; i < len; i++)
			{
				z[i] = 0U;
			}
		}

		// Token: 0x04001805 RID: 6149
		private const ulong M = 4294967295UL;
	}
}
