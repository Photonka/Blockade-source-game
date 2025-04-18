﻿using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw
{
	// Token: 0x020002F7 RID: 759
	internal abstract class Nat224
	{
		// Token: 0x06001CF9 RID: 7417 RVA: 0x000DDA3C File Offset: 0x000DBC3C
		public static uint Add(uint[] x, uint[] y, uint[] z)
		{
			ulong num = 0UL;
			num += (ulong)x[0] + (ulong)y[0];
			z[0] = (uint)num;
			num >>= 32;
			num += (ulong)x[1] + (ulong)y[1];
			z[1] = (uint)num;
			num >>= 32;
			num += (ulong)x[2] + (ulong)y[2];
			z[2] = (uint)num;
			num >>= 32;
			num += (ulong)x[3] + (ulong)y[3];
			z[3] = (uint)num;
			num >>= 32;
			num += (ulong)x[4] + (ulong)y[4];
			z[4] = (uint)num;
			num >>= 32;
			num += (ulong)x[5] + (ulong)y[5];
			z[5] = (uint)num;
			num >>= 32;
			num += (ulong)x[6] + (ulong)y[6];
			z[6] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x000DDAE8 File Offset: 0x000DBCE8
		public static uint Add(uint[] x, int xOff, uint[] y, int yOff, uint[] z, int zOff)
		{
			ulong num = 0UL;
			num += (ulong)x[xOff] + (ulong)y[yOff];
			z[zOff] = (uint)num;
			num >>= 32;
			num += (ulong)x[xOff + 1] + (ulong)y[yOff + 1];
			z[zOff + 1] = (uint)num;
			num >>= 32;
			num += (ulong)x[xOff + 2] + (ulong)y[yOff + 2];
			z[zOff + 2] = (uint)num;
			num >>= 32;
			num += (ulong)x[xOff + 3] + (ulong)y[yOff + 3];
			z[zOff + 3] = (uint)num;
			num >>= 32;
			num += (ulong)x[xOff + 4] + (ulong)y[yOff + 4];
			z[zOff + 4] = (uint)num;
			num >>= 32;
			num += (ulong)x[xOff + 5] + (ulong)y[yOff + 5];
			z[zOff + 5] = (uint)num;
			num >>= 32;
			num += (ulong)x[xOff + 6] + (ulong)y[yOff + 6];
			z[zOff + 6] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x000DDBC8 File Offset: 0x000DBDC8
		public static uint AddBothTo(uint[] x, uint[] y, uint[] z)
		{
			ulong num = 0UL;
			num += (ulong)x[0] + (ulong)y[0] + (ulong)z[0];
			z[0] = (uint)num;
			num >>= 32;
			num += (ulong)x[1] + (ulong)y[1] + (ulong)z[1];
			z[1] = (uint)num;
			num >>= 32;
			num += (ulong)x[2] + (ulong)y[2] + (ulong)z[2];
			z[2] = (uint)num;
			num >>= 32;
			num += (ulong)x[3] + (ulong)y[3] + (ulong)z[3];
			z[3] = (uint)num;
			num >>= 32;
			num += (ulong)x[4] + (ulong)y[4] + (ulong)z[4];
			z[4] = (uint)num;
			num >>= 32;
			num += (ulong)x[5] + (ulong)y[5] + (ulong)z[5];
			z[5] = (uint)num;
			num >>= 32;
			num += (ulong)x[6] + (ulong)y[6] + (ulong)z[6];
			z[6] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x000DDC98 File Offset: 0x000DBE98
		public static uint AddBothTo(uint[] x, int xOff, uint[] y, int yOff, uint[] z, int zOff)
		{
			ulong num = 0UL;
			num += (ulong)x[xOff] + (ulong)y[yOff] + (ulong)z[zOff];
			z[zOff] = (uint)num;
			num >>= 32;
			num += (ulong)x[xOff + 1] + (ulong)y[yOff + 1] + (ulong)z[zOff + 1];
			z[zOff + 1] = (uint)num;
			num >>= 32;
			num += (ulong)x[xOff + 2] + (ulong)y[yOff + 2] + (ulong)z[zOff + 2];
			z[zOff + 2] = (uint)num;
			num >>= 32;
			num += (ulong)x[xOff + 3] + (ulong)y[yOff + 3] + (ulong)z[zOff + 3];
			z[zOff + 3] = (uint)num;
			num >>= 32;
			num += (ulong)x[xOff + 4] + (ulong)y[yOff + 4] + (ulong)z[zOff + 4];
			z[zOff + 4] = (uint)num;
			num >>= 32;
			num += (ulong)x[xOff + 5] + (ulong)y[yOff + 5] + (ulong)z[zOff + 5];
			z[zOff + 5] = (uint)num;
			num >>= 32;
			num += (ulong)x[xOff + 6] + (ulong)y[yOff + 6] + (ulong)z[zOff + 6];
			z[zOff + 6] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x000DDDB4 File Offset: 0x000DBFB4
		public static uint AddTo(uint[] x, uint[] z)
		{
			ulong num = 0UL;
			num += (ulong)x[0] + (ulong)z[0];
			z[0] = (uint)num;
			num >>= 32;
			num += (ulong)x[1] + (ulong)z[1];
			z[1] = (uint)num;
			num >>= 32;
			num += (ulong)x[2] + (ulong)z[2];
			z[2] = (uint)num;
			num >>= 32;
			num += (ulong)x[3] + (ulong)z[3];
			z[3] = (uint)num;
			num >>= 32;
			num += (ulong)x[4] + (ulong)z[4];
			z[4] = (uint)num;
			num >>= 32;
			num += (ulong)x[5] + (ulong)z[5];
			z[5] = (uint)num;
			num >>= 32;
			num += (ulong)x[6] + (ulong)z[6];
			z[6] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x000DDE60 File Offset: 0x000DC060
		public static uint AddTo(uint[] x, int xOff, uint[] z, int zOff, uint cIn)
		{
			ulong num = (ulong)cIn;
			num += (ulong)x[xOff] + (ulong)z[zOff];
			z[zOff] = (uint)num;
			num >>= 32;
			num += (ulong)x[xOff + 1] + (ulong)z[zOff + 1];
			z[zOff + 1] = (uint)num;
			num >>= 32;
			num += (ulong)x[xOff + 2] + (ulong)z[zOff + 2];
			z[zOff + 2] = (uint)num;
			num >>= 32;
			num += (ulong)x[xOff + 3] + (ulong)z[zOff + 3];
			z[zOff + 3] = (uint)num;
			num >>= 32;
			num += (ulong)x[xOff + 4] + (ulong)z[zOff + 4];
			z[zOff + 4] = (uint)num;
			num >>= 32;
			num += (ulong)x[xOff + 5] + (ulong)z[zOff + 5];
			z[zOff + 5] = (uint)num;
			num >>= 32;
			num += (ulong)x[xOff + 6] + (ulong)z[zOff + 6];
			z[zOff + 6] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x000DDF34 File Offset: 0x000DC134
		public static uint AddToEachOther(uint[] u, int uOff, uint[] v, int vOff)
		{
			ulong num = 0UL;
			num += (ulong)u[uOff] + (ulong)v[vOff];
			u[uOff] = (uint)num;
			v[vOff] = (uint)num;
			num >>= 32;
			num += (ulong)u[uOff + 1] + (ulong)v[vOff + 1];
			u[uOff + 1] = (uint)num;
			v[vOff + 1] = (uint)num;
			num >>= 32;
			num += (ulong)u[uOff + 2] + (ulong)v[vOff + 2];
			u[uOff + 2] = (uint)num;
			v[vOff + 2] = (uint)num;
			num >>= 32;
			num += (ulong)u[uOff + 3] + (ulong)v[vOff + 3];
			u[uOff + 3] = (uint)num;
			v[vOff + 3] = (uint)num;
			num >>= 32;
			num += (ulong)u[uOff + 4] + (ulong)v[vOff + 4];
			u[uOff + 4] = (uint)num;
			v[vOff + 4] = (uint)num;
			num >>= 32;
			num += (ulong)u[uOff + 5] + (ulong)v[vOff + 5];
			u[uOff + 5] = (uint)num;
			v[vOff + 5] = (uint)num;
			num >>= 32;
			num += (ulong)u[uOff + 6] + (ulong)v[vOff + 6];
			u[uOff + 6] = (uint)num;
			v[vOff + 6] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x000DE033 File Offset: 0x000DC233
		public static void Copy(uint[] x, uint[] z)
		{
			z[0] = x[0];
			z[1] = x[1];
			z[2] = x[2];
			z[3] = x[3];
			z[4] = x[4];
			z[5] = x[5];
			z[6] = x[6];
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x000DE060 File Offset: 0x000DC260
		public static void Copy(uint[] x, int xOff, uint[] z, int zOff)
		{
			z[zOff] = x[xOff];
			z[zOff + 1] = x[xOff + 1];
			z[zOff + 2] = x[xOff + 2];
			z[zOff + 3] = x[xOff + 3];
			z[zOff + 4] = x[xOff + 4];
			z[zOff + 5] = x[xOff + 5];
			z[zOff + 6] = x[xOff + 6];
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x000DE0AF File Offset: 0x000DC2AF
		public static uint[] Create()
		{
			return new uint[7];
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x000DE0B7 File Offset: 0x000DC2B7
		public static uint[] CreateExt()
		{
			return new uint[14];
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x000DE0C0 File Offset: 0x000DC2C0
		public static bool Diff(uint[] x, int xOff, uint[] y, int yOff, uint[] z, int zOff)
		{
			bool flag = Nat224.Gte(x, xOff, y, yOff);
			if (flag)
			{
				Nat224.Sub(x, xOff, y, yOff, z, zOff);
				return flag;
			}
			Nat224.Sub(y, yOff, x, xOff, z, zOff);
			return flag;
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x000DE0EC File Offset: 0x000DC2EC
		public static bool Eq(uint[] x, uint[] y)
		{
			for (int i = 6; i >= 0; i--)
			{
				if (x[i] != y[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x000DE110 File Offset: 0x000DC310
		public static uint[] FromBigInteger(BigInteger x)
		{
			if (x.SignValue < 0 || x.BitLength > 224)
			{
				throw new ArgumentException();
			}
			uint[] array = Nat224.Create();
			int num = 0;
			while (x.SignValue != 0)
			{
				array[num++] = (uint)x.IntValue;
				x = x.ShiftRight(32);
			}
			return array;
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x000DE164 File Offset: 0x000DC364
		public static uint GetBit(uint[] x, int bit)
		{
			if (bit == 0)
			{
				return x[0] & 1U;
			}
			int num = bit >> 5;
			if (num < 0 || num >= 7)
			{
				return 0U;
			}
			int num2 = bit & 31;
			return x[num] >> num2 & 1U;
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x000DE198 File Offset: 0x000DC398
		public static bool Gte(uint[] x, uint[] y)
		{
			for (int i = 6; i >= 0; i--)
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

		// Token: 0x06001D09 RID: 7433 RVA: 0x000DE1C8 File Offset: 0x000DC3C8
		public static bool Gte(uint[] x, int xOff, uint[] y, int yOff)
		{
			for (int i = 6; i >= 0; i--)
			{
				uint num = x[xOff + i];
				uint num2 = y[yOff + i];
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

		// Token: 0x06001D0A RID: 7434 RVA: 0x000DE1FC File Offset: 0x000DC3FC
		public static bool IsOne(uint[] x)
		{
			if (x[0] != 1U)
			{
				return false;
			}
			for (int i = 1; i < 7; i++)
			{
				if (x[i] != 0U)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x000DE228 File Offset: 0x000DC428
		public static bool IsZero(uint[] x)
		{
			for (int i = 0; i < 7; i++)
			{
				if (x[i] != 0U)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x000DE24C File Offset: 0x000DC44C
		public static void Mul(uint[] x, uint[] y, uint[] zz)
		{
			ulong num = (ulong)y[0];
			ulong num2 = (ulong)y[1];
			ulong num3 = (ulong)y[2];
			ulong num4 = (ulong)y[3];
			ulong num5 = (ulong)y[4];
			ulong num6 = (ulong)y[5];
			ulong num7 = (ulong)y[6];
			ulong num8 = 0UL;
			ulong num9 = (ulong)x[0];
			num8 += num9 * num;
			zz[0] = (uint)num8;
			num8 >>= 32;
			num8 += num9 * num2;
			zz[1] = (uint)num8;
			num8 >>= 32;
			num8 += num9 * num3;
			zz[2] = (uint)num8;
			num8 >>= 32;
			num8 += num9 * num4;
			zz[3] = (uint)num8;
			num8 >>= 32;
			num8 += num9 * num5;
			zz[4] = (uint)num8;
			num8 >>= 32;
			num8 += num9 * num6;
			zz[5] = (uint)num8;
			num8 >>= 32;
			num8 += num9 * num7;
			zz[6] = (uint)num8;
			num8 >>= 32;
			zz[7] = (uint)num8;
			for (int i = 1; i < 7; i++)
			{
				ulong num10 = 0UL;
				ulong num11 = (ulong)x[i];
				num10 += num11 * num + (ulong)zz[i];
				zz[i] = (uint)num10;
				num10 >>= 32;
				num10 += num11 * num2 + (ulong)zz[i + 1];
				zz[i + 1] = (uint)num10;
				num10 >>= 32;
				num10 += num11 * num3 + (ulong)zz[i + 2];
				zz[i + 2] = (uint)num10;
				num10 >>= 32;
				num10 += num11 * num4 + (ulong)zz[i + 3];
				zz[i + 3] = (uint)num10;
				num10 >>= 32;
				num10 += num11 * num5 + (ulong)zz[i + 4];
				zz[i + 4] = (uint)num10;
				num10 >>= 32;
				num10 += num11 * num6 + (ulong)zz[i + 5];
				zz[i + 5] = (uint)num10;
				num10 >>= 32;
				num10 += num11 * num7 + (ulong)zz[i + 6];
				zz[i + 6] = (uint)num10;
				num10 >>= 32;
				zz[i + 7] = (uint)num10;
			}
		}

		// Token: 0x06001D0D RID: 7437 RVA: 0x000DE43C File Offset: 0x000DC63C
		public static void Mul(uint[] x, int xOff, uint[] y, int yOff, uint[] zz, int zzOff)
		{
			ulong num = (ulong)y[yOff];
			ulong num2 = (ulong)y[yOff + 1];
			ulong num3 = (ulong)y[yOff + 2];
			ulong num4 = (ulong)y[yOff + 3];
			ulong num5 = (ulong)y[yOff + 4];
			ulong num6 = (ulong)y[yOff + 5];
			ulong num7 = (ulong)y[yOff + 6];
			ulong num8 = 0UL;
			ulong num9 = (ulong)x[xOff];
			num8 += num9 * num;
			zz[zzOff] = (uint)num8;
			num8 >>= 32;
			num8 += num9 * num2;
			zz[zzOff + 1] = (uint)num8;
			num8 >>= 32;
			num8 += num9 * num3;
			zz[zzOff + 2] = (uint)num8;
			num8 >>= 32;
			num8 += num9 * num4;
			zz[zzOff + 3] = (uint)num8;
			num8 >>= 32;
			num8 += num9 * num5;
			zz[zzOff + 4] = (uint)num8;
			num8 >>= 32;
			num8 += num9 * num6;
			zz[zzOff + 5] = (uint)num8;
			num8 >>= 32;
			num8 += num9 * num7;
			zz[zzOff + 6] = (uint)num8;
			num8 >>= 32;
			zz[zzOff + 7] = (uint)num8;
			for (int i = 1; i < 7; i++)
			{
				zzOff++;
				ulong num10 = 0UL;
				ulong num11 = (ulong)x[xOff + i];
				num10 += num11 * num + (ulong)zz[zzOff];
				zz[zzOff] = (uint)num10;
				num10 >>= 32;
				num10 += num11 * num2 + (ulong)zz[zzOff + 1];
				zz[zzOff + 1] = (uint)num10;
				num10 >>= 32;
				num10 += num11 * num3 + (ulong)zz[zzOff + 2];
				zz[zzOff + 2] = (uint)num10;
				num10 >>= 32;
				num10 += num11 * num4 + (ulong)zz[zzOff + 3];
				zz[zzOff + 3] = (uint)num10;
				num10 >>= 32;
				num10 += num11 * num5 + (ulong)zz[zzOff + 4];
				zz[zzOff + 4] = (uint)num10;
				num10 >>= 32;
				num10 += num11 * num6 + (ulong)zz[zzOff + 5];
				zz[zzOff + 5] = (uint)num10;
				num10 >>= 32;
				num10 += num11 * num7 + (ulong)zz[zzOff + 6];
				zz[zzOff + 6] = (uint)num10;
				num10 >>= 32;
				zz[zzOff + 7] = (uint)num10;
			}
		}

		// Token: 0x06001D0E RID: 7438 RVA: 0x000DE670 File Offset: 0x000DC870
		public static uint MulAddTo(uint[] x, uint[] y, uint[] zz)
		{
			ulong num = (ulong)y[0];
			ulong num2 = (ulong)y[1];
			ulong num3 = (ulong)y[2];
			ulong num4 = (ulong)y[3];
			ulong num5 = (ulong)y[4];
			ulong num6 = (ulong)y[5];
			ulong num7 = (ulong)y[6];
			ulong num8 = 0UL;
			for (int i = 0; i < 7; i++)
			{
				ulong num9 = 0UL;
				ulong num10 = (ulong)x[i];
				num9 += num10 * num + (ulong)zz[i];
				zz[i] = (uint)num9;
				num9 >>= 32;
				num9 += num10 * num2 + (ulong)zz[i + 1];
				zz[i + 1] = (uint)num9;
				num9 >>= 32;
				num9 += num10 * num3 + (ulong)zz[i + 2];
				zz[i + 2] = (uint)num9;
				num9 >>= 32;
				num9 += num10 * num4 + (ulong)zz[i + 3];
				zz[i + 3] = (uint)num9;
				num9 >>= 32;
				num9 += num10 * num5 + (ulong)zz[i + 4];
				zz[i + 4] = (uint)num9;
				num9 >>= 32;
				num9 += num10 * num6 + (ulong)zz[i + 5];
				zz[i + 5] = (uint)num9;
				num9 >>= 32;
				num9 += num10 * num7 + (ulong)zz[i + 6];
				zz[i + 6] = (uint)num9;
				num9 >>= 32;
				num9 += num8 + (ulong)zz[i + 7];
				zz[i + 7] = (uint)num9;
				num8 = num9 >> 32;
			}
			return (uint)num8;
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x000DE7D0 File Offset: 0x000DC9D0
		public static uint MulAddTo(uint[] x, int xOff, uint[] y, int yOff, uint[] zz, int zzOff)
		{
			ulong num = (ulong)y[yOff];
			ulong num2 = (ulong)y[yOff + 1];
			ulong num3 = (ulong)y[yOff + 2];
			ulong num4 = (ulong)y[yOff + 3];
			ulong num5 = (ulong)y[yOff + 4];
			ulong num6 = (ulong)y[yOff + 5];
			ulong num7 = (ulong)y[yOff + 6];
			ulong num8 = 0UL;
			for (int i = 0; i < 7; i++)
			{
				ulong num9 = 0UL;
				ulong num10 = (ulong)x[xOff + i];
				num9 += num10 * num + (ulong)zz[zzOff];
				zz[zzOff] = (uint)num9;
				num9 >>= 32;
				num9 += num10 * num2 + (ulong)zz[zzOff + 1];
				zz[zzOff + 1] = (uint)num9;
				num9 >>= 32;
				num9 += num10 * num3 + (ulong)zz[zzOff + 2];
				zz[zzOff + 2] = (uint)num9;
				num9 >>= 32;
				num9 += num10 * num4 + (ulong)zz[zzOff + 3];
				zz[zzOff + 3] = (uint)num9;
				num9 >>= 32;
				num9 += num10 * num5 + (ulong)zz[zzOff + 4];
				zz[zzOff + 4] = (uint)num9;
				num9 >>= 32;
				num9 += num10 * num6 + (ulong)zz[zzOff + 5];
				zz[zzOff + 5] = (uint)num9;
				num9 >>= 32;
				num9 += num10 * num7 + (ulong)zz[zzOff + 6];
				zz[zzOff + 6] = (uint)num9;
				num9 >>= 32;
				num9 += num8 + (ulong)zz[zzOff + 7];
				zz[zzOff + 7] = (uint)num9;
				num8 = num9 >> 32;
				zzOff++;
			}
			return (uint)num8;
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x000DE954 File Offset: 0x000DCB54
		public static ulong Mul33Add(uint w, uint[] x, int xOff, uint[] y, int yOff, uint[] z, int zOff)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)w;
			ulong num3 = (ulong)x[xOff];
			num += num2 * num3 + (ulong)y[yOff];
			z[zOff] = (uint)num;
			num >>= 32;
			ulong num4 = (ulong)x[xOff + 1];
			num += num2 * num4 + num3 + (ulong)y[yOff + 1];
			z[zOff + 1] = (uint)num;
			num >>= 32;
			ulong num5 = (ulong)x[xOff + 2];
			num += num2 * num5 + num4 + (ulong)y[yOff + 2];
			z[zOff + 2] = (uint)num;
			num >>= 32;
			ulong num6 = (ulong)x[xOff + 3];
			num += num2 * num6 + num5 + (ulong)y[yOff + 3];
			z[zOff + 3] = (uint)num;
			num >>= 32;
			ulong num7 = (ulong)x[xOff + 4];
			num += num2 * num7 + num6 + (ulong)y[yOff + 4];
			z[zOff + 4] = (uint)num;
			num >>= 32;
			ulong num8 = (ulong)x[xOff + 5];
			num += num2 * num8 + num7 + (ulong)y[yOff + 5];
			z[zOff + 5] = (uint)num;
			num >>= 32;
			ulong num9 = (ulong)x[xOff + 6];
			num += num2 * num9 + num8 + (ulong)y[yOff + 6];
			z[zOff + 6] = (uint)num;
			num >>= 32;
			return num + num9;
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x000DEA78 File Offset: 0x000DCC78
		public static uint MulByWord(uint x, uint[] z)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x;
			num += num2 * (ulong)z[0];
			z[0] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)z[1];
			z[1] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)z[2];
			z[2] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)z[3];
			z[3] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)z[4];
			z[4] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)z[5];
			z[5] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)z[6];
			z[6] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x000DEB14 File Offset: 0x000DCD14
		public static uint MulByWordAddTo(uint x, uint[] y, uint[] z)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x;
			num += num2 * (ulong)z[0] + (ulong)y[0];
			z[0] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)z[1] + (ulong)y[1];
			z[1] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)z[2] + (ulong)y[2];
			z[2] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)z[3] + (ulong)y[3];
			z[3] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)z[4] + (ulong)y[4];
			z[4] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)z[5] + (ulong)y[5];
			z[5] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)z[6] + (ulong)y[6];
			z[6] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x000DEBD4 File Offset: 0x000DCDD4
		public static uint MulWordAddTo(uint x, uint[] y, int yOff, uint[] z, int zOff)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x;
			num += num2 * (ulong)y[yOff] + (ulong)z[zOff];
			z[zOff] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)y[yOff + 1] + (ulong)z[zOff + 1];
			z[zOff + 1] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)y[yOff + 2] + (ulong)z[zOff + 2];
			z[zOff + 2] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)y[yOff + 3] + (ulong)z[zOff + 3];
			z[zOff + 3] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)y[yOff + 4] + (ulong)z[zOff + 4];
			z[zOff + 4] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)y[yOff + 5] + (ulong)z[zOff + 5];
			z[zOff + 5] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)y[yOff + 6] + (ulong)z[zOff + 6];
			z[zOff + 6] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x000DECC4 File Offset: 0x000DCEC4
		public static uint Mul33DWordAdd(uint x, ulong y, uint[] z, int zOff)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x;
			ulong num3 = y & (ulong)-1;
			num += num2 * num3 + (ulong)z[zOff];
			z[zOff] = (uint)num;
			num >>= 32;
			ulong num4 = y >> 32;
			num += num2 * num4 + num3 + (ulong)z[zOff + 1];
			z[zOff + 1] = (uint)num;
			num >>= 32;
			num += num4 + (ulong)z[zOff + 2];
			z[zOff + 2] = (uint)num;
			num >>= 32;
			num += (ulong)z[zOff + 3];
			z[zOff + 3] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(7, z, zOff, 4);
			}
			return 0U;
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x000DED4C File Offset: 0x000DCF4C
		public static uint Mul33WordAdd(uint x, uint y, uint[] z, int zOff)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)y;
			num += num2 * (ulong)x + (ulong)z[zOff];
			z[zOff] = (uint)num;
			num >>= 32;
			num += num2 + (ulong)z[zOff + 1];
			z[zOff + 1] = (uint)num;
			num >>= 32;
			num += (ulong)z[zOff + 2];
			z[zOff + 2] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(7, z, zOff, 3);
			}
			return 0U;
		}

		// Token: 0x06001D16 RID: 7446 RVA: 0x000DEDB0 File Offset: 0x000DCFB0
		public static uint MulWordDwordAdd(uint x, ulong y, uint[] z, int zOff)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x;
			num += num2 * y + (ulong)z[zOff];
			z[zOff] = (uint)num;
			num >>= 32;
			num += num2 * (y >> 32) + (ulong)z[zOff + 1];
			z[zOff + 1] = (uint)num;
			num >>= 32;
			num += (ulong)z[zOff + 2];
			z[zOff + 2] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(7, z, zOff, 3);
			}
			return 0U;
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x000DEE18 File Offset: 0x000DD018
		public static uint MulWord(uint x, uint[] y, uint[] z, int zOff)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x;
			int num3 = 0;
			do
			{
				num += num2 * (ulong)y[num3];
				z[zOff + num3] = (uint)num;
				num >>= 32;
			}
			while (++num3 < 7);
			return (uint)num;
		}

		// Token: 0x06001D18 RID: 7448 RVA: 0x000DEE4C File Offset: 0x000DD04C
		public static void Square(uint[] x, uint[] zz)
		{
			ulong num = (ulong)x[0];
			uint num2 = 0U;
			int num3 = 6;
			int num4 = 14;
			do
			{
				ulong num5 = (ulong)x[num3--];
				ulong num6 = num5 * num5;
				zz[--num4] = (num2 << 31 | (uint)(num6 >> 33));
				zz[--num4] = (uint)(num6 >> 1);
				num2 = (uint)num6;
			}
			while (num3 > 0);
			ulong num7 = num * num;
			ulong num8 = (ulong)((ulong)num2 << 31) | num7 >> 33;
			zz[0] = (uint)num7;
			num2 = ((uint)(num7 >> 32) & 1U);
			ulong num9 = (ulong)x[1];
			ulong num10 = (ulong)zz[2];
			num8 += num9 * num;
			uint num11 = (uint)num8;
			zz[1] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num10 += num8 >> 32;
			ulong num12 = (ulong)x[2];
			ulong num13 = (ulong)zz[3];
			ulong num14 = (ulong)zz[4];
			num10 += num12 * num;
			num11 = (uint)num10;
			zz[2] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num13 += (num10 >> 32) + num12 * num9;
			num14 += num13 >> 32;
			num13 &= (ulong)-1;
			ulong num15 = (ulong)x[3];
			ulong num16 = (ulong)zz[5] + (num14 >> 32);
			num14 &= (ulong)-1;
			ulong num17 = (ulong)zz[6] + (num16 >> 32);
			num16 &= (ulong)-1;
			num13 += num15 * num;
			num11 = (uint)num13;
			zz[3] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num14 += (num13 >> 32) + num15 * num9;
			num16 += (num14 >> 32) + num15 * num12;
			num14 &= (ulong)-1;
			num17 += num16 >> 32;
			num16 &= (ulong)-1;
			ulong num18 = (ulong)x[4];
			ulong num19 = (ulong)zz[7] + (num17 >> 32);
			num17 &= (ulong)-1;
			ulong num20 = (ulong)zz[8] + (num19 >> 32);
			num19 &= (ulong)-1;
			num14 += num18 * num;
			num11 = (uint)num14;
			zz[4] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num16 += (num14 >> 32) + num18 * num9;
			num17 += (num16 >> 32) + num18 * num12;
			num16 &= (ulong)-1;
			num19 += (num17 >> 32) + num18 * num15;
			num17 &= (ulong)-1;
			num20 += num19 >> 32;
			num19 &= (ulong)-1;
			ulong num21 = (ulong)x[5];
			ulong num22 = (ulong)zz[9] + (num20 >> 32);
			num20 &= (ulong)-1;
			ulong num23 = (ulong)zz[10] + (num22 >> 32);
			num22 &= (ulong)-1;
			num16 += num21 * num;
			num11 = (uint)num16;
			zz[5] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num17 += (num16 >> 32) + num21 * num9;
			num19 += (num17 >> 32) + num21 * num12;
			num17 &= (ulong)-1;
			num20 += (num19 >> 32) + num21 * num15;
			num19 &= (ulong)-1;
			num22 += (num20 >> 32) + num21 * num18;
			num20 &= (ulong)-1;
			num23 += num22 >> 32;
			num22 &= (ulong)-1;
			ulong num24 = (ulong)x[6];
			ulong num25 = (ulong)zz[11] + (num23 >> 32);
			num23 &= (ulong)-1;
			ulong num26 = (ulong)zz[12] + (num25 >> 32);
			num25 &= (ulong)-1;
			num17 += num24 * num;
			num11 = (uint)num17;
			zz[6] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num19 += (num17 >> 32) + num24 * num9;
			num20 += (num19 >> 32) + num24 * num12;
			num22 += (num20 >> 32) + num24 * num15;
			num23 += (num22 >> 32) + num24 * num18;
			num25 += (num23 >> 32) + num24 * num21;
			num26 += num25 >> 32;
			num11 = (uint)num19;
			zz[7] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = (uint)num20;
			zz[8] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = (uint)num22;
			zz[9] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = (uint)num23;
			zz[10] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = (uint)num25;
			zz[11] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = (uint)num26;
			zz[12] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = zz[13] + (uint)(num26 >> 32);
			zz[13] = (num11 << 1 | num2);
		}

		// Token: 0x06001D19 RID: 7449 RVA: 0x000DF21C File Offset: 0x000DD41C
		public static void Square(uint[] x, int xOff, uint[] zz, int zzOff)
		{
			ulong num = (ulong)x[xOff];
			uint num2 = 0U;
			int num3 = 6;
			int num4 = 14;
			do
			{
				ulong num5 = (ulong)x[xOff + num3--];
				ulong num6 = num5 * num5;
				zz[zzOff + --num4] = (num2 << 31 | (uint)(num6 >> 33));
				zz[zzOff + --num4] = (uint)(num6 >> 1);
				num2 = (uint)num6;
			}
			while (num3 > 0);
			ulong num7 = num * num;
			ulong num8 = (ulong)((ulong)num2 << 31) | num7 >> 33;
			zz[zzOff] = (uint)num7;
			num2 = ((uint)(num7 >> 32) & 1U);
			ulong num9 = (ulong)x[xOff + 1];
			ulong num10 = (ulong)zz[zzOff + 2];
			num8 += num9 * num;
			uint num11 = (uint)num8;
			zz[zzOff + 1] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num10 += num8 >> 32;
			ulong num12 = (ulong)x[xOff + 2];
			ulong num13 = (ulong)zz[zzOff + 3];
			ulong num14 = (ulong)zz[zzOff + 4];
			num10 += num12 * num;
			num11 = (uint)num10;
			zz[zzOff + 2] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num13 += (num10 >> 32) + num12 * num9;
			num14 += num13 >> 32;
			num13 &= (ulong)-1;
			ulong num15 = (ulong)x[xOff + 3];
			ulong num16 = (ulong)zz[zzOff + 5] + (num14 >> 32);
			num14 &= (ulong)-1;
			ulong num17 = (ulong)zz[zzOff + 6] + (num16 >> 32);
			num16 &= (ulong)-1;
			num13 += num15 * num;
			num11 = (uint)num13;
			zz[zzOff + 3] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num14 += (num13 >> 32) + num15 * num9;
			num16 += (num14 >> 32) + num15 * num12;
			num14 &= (ulong)-1;
			num17 += num16 >> 32;
			num16 &= (ulong)-1;
			ulong num18 = (ulong)x[xOff + 4];
			ulong num19 = (ulong)zz[zzOff + 7] + (num17 >> 32);
			num17 &= (ulong)-1;
			ulong num20 = (ulong)zz[zzOff + 8] + (num19 >> 32);
			num19 &= (ulong)-1;
			num14 += num18 * num;
			num11 = (uint)num14;
			zz[zzOff + 4] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num16 += (num14 >> 32) + num18 * num9;
			num17 += (num16 >> 32) + num18 * num12;
			num16 &= (ulong)-1;
			num19 += (num17 >> 32) + num18 * num15;
			num17 &= (ulong)-1;
			num20 += num19 >> 32;
			num19 &= (ulong)-1;
			ulong num21 = (ulong)x[xOff + 5];
			ulong num22 = (ulong)zz[zzOff + 9] + (num20 >> 32);
			num20 &= (ulong)-1;
			ulong num23 = (ulong)zz[zzOff + 10] + (num22 >> 32);
			num22 &= (ulong)-1;
			num16 += num21 * num;
			num11 = (uint)num16;
			zz[zzOff + 5] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num17 += (num16 >> 32) + num21 * num9;
			num19 += (num17 >> 32) + num21 * num12;
			num17 &= (ulong)-1;
			num20 += (num19 >> 32) + num21 * num15;
			num19 &= (ulong)-1;
			num22 += (num20 >> 32) + num21 * num18;
			num20 &= (ulong)-1;
			num23 += num22 >> 32;
			num22 &= (ulong)-1;
			ulong num24 = (ulong)x[xOff + 6];
			ulong num25 = (ulong)zz[zzOff + 11] + (num23 >> 32);
			num23 &= (ulong)-1;
			ulong num26 = (ulong)zz[zzOff + 12] + (num25 >> 32);
			num25 &= (ulong)-1;
			num17 += num24 * num;
			num11 = (uint)num17;
			zz[zzOff + 6] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num19 += (num17 >> 32) + num24 * num9;
			num20 += (num19 >> 32) + num24 * num12;
			num22 += (num20 >> 32) + num24 * num15;
			num23 += (num22 >> 32) + num24 * num18;
			num25 += (num23 >> 32) + num24 * num21;
			num26 += num25 >> 32;
			num11 = (uint)num19;
			zz[zzOff + 7] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = (uint)num20;
			zz[zzOff + 8] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = (uint)num22;
			zz[zzOff + 9] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = (uint)num23;
			zz[zzOff + 10] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = (uint)num25;
			zz[zzOff + 11] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = (uint)num26;
			zz[zzOff + 12] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = zz[zzOff + 13] + (uint)(num26 >> 32);
			zz[zzOff + 13] = (num11 << 1 | num2);
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x000DF630 File Offset: 0x000DD830
		public static int Sub(uint[] x, uint[] y, uint[] z)
		{
			long num = 0L;
			num += (long)((ulong)x[0] - (ulong)y[0]);
			z[0] = (uint)num;
			num >>= 32;
			num += (long)((ulong)x[1] - (ulong)y[1]);
			z[1] = (uint)num;
			num >>= 32;
			num += (long)((ulong)x[2] - (ulong)y[2]);
			z[2] = (uint)num;
			num >>= 32;
			num += (long)((ulong)x[3] - (ulong)y[3]);
			z[3] = (uint)num;
			num >>= 32;
			num += (long)((ulong)x[4] - (ulong)y[4]);
			z[4] = (uint)num;
			num >>= 32;
			num += (long)((ulong)x[5] - (ulong)y[5]);
			z[5] = (uint)num;
			num >>= 32;
			num += (long)((ulong)x[6] - (ulong)y[6]);
			z[6] = (uint)num;
			num >>= 32;
			return (int)num;
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x000DF6DC File Offset: 0x000DD8DC
		public static int Sub(uint[] x, int xOff, uint[] y, int yOff, uint[] z, int zOff)
		{
			long num = 0L;
			num += (long)((ulong)x[xOff] - (ulong)y[yOff]);
			z[zOff] = (uint)num;
			num >>= 32;
			num += (long)((ulong)x[xOff + 1] - (ulong)y[yOff + 1]);
			z[zOff + 1] = (uint)num;
			num >>= 32;
			num += (long)((ulong)x[xOff + 2] - (ulong)y[yOff + 2]);
			z[zOff + 2] = (uint)num;
			num >>= 32;
			num += (long)((ulong)x[xOff + 3] - (ulong)y[yOff + 3]);
			z[zOff + 3] = (uint)num;
			num >>= 32;
			num += (long)((ulong)x[xOff + 4] - (ulong)y[yOff + 4]);
			z[zOff + 4] = (uint)num;
			num >>= 32;
			num += (long)((ulong)x[xOff + 5] - (ulong)y[yOff + 5]);
			z[zOff + 5] = (uint)num;
			num >>= 32;
			num += (long)((ulong)x[xOff + 6] - (ulong)y[yOff + 6]);
			z[zOff + 6] = (uint)num;
			num >>= 32;
			return (int)num;
		}

		// Token: 0x06001D1C RID: 7452 RVA: 0x000DF7BC File Offset: 0x000DD9BC
		public static int SubBothFrom(uint[] x, uint[] y, uint[] z)
		{
			long num = 0L;
			num += (long)((ulong)z[0] - (ulong)x[0] - (ulong)y[0]);
			z[0] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[1] - (ulong)x[1] - (ulong)y[1]);
			z[1] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[2] - (ulong)x[2] - (ulong)y[2]);
			z[2] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[3] - (ulong)x[3] - (ulong)y[3]);
			z[3] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[4] - (ulong)x[4] - (ulong)y[4]);
			z[4] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[5] - (ulong)x[5] - (ulong)y[5]);
			z[5] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[6] - (ulong)x[6] - (ulong)y[6]);
			z[6] = (uint)num;
			num >>= 32;
			return (int)num;
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x000DF88C File Offset: 0x000DDA8C
		public static int SubFrom(uint[] x, uint[] z)
		{
			long num = 0L;
			num += (long)((ulong)z[0] - (ulong)x[0]);
			z[0] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[1] - (ulong)x[1]);
			z[1] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[2] - (ulong)x[2]);
			z[2] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[3] - (ulong)x[3]);
			z[3] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[4] - (ulong)x[4]);
			z[4] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[5] - (ulong)x[5]);
			z[5] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[6] - (ulong)x[6]);
			z[6] = (uint)num;
			num >>= 32;
			return (int)num;
		}

		// Token: 0x06001D1E RID: 7454 RVA: 0x000DF938 File Offset: 0x000DDB38
		public static int SubFrom(uint[] x, int xOff, uint[] z, int zOff)
		{
			long num = 0L;
			num += (long)((ulong)z[zOff] - (ulong)x[xOff]);
			z[zOff] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[zOff + 1] - (ulong)x[xOff + 1]);
			z[zOff + 1] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[zOff + 2] - (ulong)x[xOff + 2]);
			z[zOff + 2] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[zOff + 3] - (ulong)x[xOff + 3]);
			z[zOff + 3] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[zOff + 4] - (ulong)x[xOff + 4]);
			z[zOff + 4] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[zOff + 5] - (ulong)x[xOff + 5]);
			z[zOff + 5] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[zOff + 6] - (ulong)x[xOff + 6]);
			z[zOff + 6] = (uint)num;
			num >>= 32;
			return (int)num;
		}

		// Token: 0x06001D1F RID: 7455 RVA: 0x000DFA08 File Offset: 0x000DDC08
		public static BigInteger ToBigInteger(uint[] x)
		{
			byte[] array = new byte[28];
			for (int i = 0; i < 7; i++)
			{
				uint num = x[i];
				if (num != 0U)
				{
					Pack.UInt32_To_BE(num, array, 6 - i << 2);
				}
			}
			return new BigInteger(1, array);
		}

		// Token: 0x06001D20 RID: 7456 RVA: 0x000DFA43 File Offset: 0x000DDC43
		public static void Zero(uint[] z)
		{
			z[0] = 0U;
			z[1] = 0U;
			z[2] = 0U;
			z[3] = 0U;
			z[4] = 0U;
			z[5] = 0U;
			z[6] = 0U;
		}

		// Token: 0x04001809 RID: 6153
		private const ulong M = 4294967295UL;
	}
}
