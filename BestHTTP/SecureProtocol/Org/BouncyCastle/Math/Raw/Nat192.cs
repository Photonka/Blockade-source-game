using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw
{
	// Token: 0x020002F6 RID: 758
	internal abstract class Nat192
	{
		// Token: 0x06001CCB RID: 7371 RVA: 0x000DC018 File Offset: 0x000DA218
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
			return (uint)num;
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x000DC0B0 File Offset: 0x000DA2B0
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
			return (uint)num;
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x000DC164 File Offset: 0x000DA364
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
			return (uint)num;
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x000DC1FC File Offset: 0x000DA3FC
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
			return (uint)num;
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x000DC2B4 File Offset: 0x000DA4B4
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
			return (uint)num;
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x000DC390 File Offset: 0x000DA590
		public static void Copy(uint[] x, uint[] z)
		{
			z[0] = x[0];
			z[1] = x[1];
			z[2] = x[2];
			z[3] = x[3];
			z[4] = x[4];
			z[5] = x[5];
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x000DC3B6 File Offset: 0x000DA5B6
		public static void Copy(uint[] x, int xOff, uint[] z, int zOff)
		{
			z[zOff] = x[xOff];
			z[zOff + 1] = x[xOff + 1];
			z[zOff + 2] = x[xOff + 2];
			z[zOff + 3] = x[xOff + 3];
			z[zOff + 4] = x[xOff + 4];
			z[zOff + 5] = x[xOff + 5];
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x000DC3F0 File Offset: 0x000DA5F0
		public static void Copy64(ulong[] x, ulong[] z)
		{
			z[0] = x[0];
			z[1] = x[1];
			z[2] = x[2];
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x000DC404 File Offset: 0x000DA604
		public static void Copy64(ulong[] x, int xOff, ulong[] z, int zOff)
		{
			z[zOff] = x[xOff];
			z[zOff + 1] = x[xOff + 1];
			z[zOff + 2] = x[xOff + 2];
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x000DC420 File Offset: 0x000DA620
		public static uint[] Create()
		{
			return new uint[6];
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x000DC428 File Offset: 0x000DA628
		public static ulong[] Create64()
		{
			return new ulong[3];
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x000DC430 File Offset: 0x000DA630
		public static uint[] CreateExt()
		{
			return new uint[12];
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x000DC439 File Offset: 0x000DA639
		public static ulong[] CreateExt64()
		{
			return new ulong[6];
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x000DC441 File Offset: 0x000DA641
		public static bool Diff(uint[] x, int xOff, uint[] y, int yOff, uint[] z, int zOff)
		{
			bool flag = Nat192.Gte(x, xOff, y, yOff);
			if (flag)
			{
				Nat192.Sub(x, xOff, y, yOff, z, zOff);
				return flag;
			}
			Nat192.Sub(y, yOff, x, xOff, z, zOff);
			return flag;
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x000DC46C File Offset: 0x000DA66C
		public static bool Eq(uint[] x, uint[] y)
		{
			for (int i = 5; i >= 0; i--)
			{
				if (x[i] != y[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x000DC490 File Offset: 0x000DA690
		public static bool Eq64(ulong[] x, ulong[] y)
		{
			for (int i = 2; i >= 0; i--)
			{
				if (x[i] != y[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x000DC4B4 File Offset: 0x000DA6B4
		public static uint[] FromBigInteger(BigInteger x)
		{
			if (x.SignValue < 0 || x.BitLength > 192)
			{
				throw new ArgumentException();
			}
			uint[] array = Nat192.Create();
			int num = 0;
			while (x.SignValue != 0)
			{
				array[num++] = (uint)x.IntValue;
				x = x.ShiftRight(32);
			}
			return array;
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x000DC508 File Offset: 0x000DA708
		public static ulong[] FromBigInteger64(BigInteger x)
		{
			if (x.SignValue < 0 || x.BitLength > 192)
			{
				throw new ArgumentException();
			}
			ulong[] array = Nat192.Create64();
			int num = 0;
			while (x.SignValue != 0)
			{
				array[num++] = (ulong)x.LongValue;
				x = x.ShiftRight(64);
			}
			return array;
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x000DC55C File Offset: 0x000DA75C
		public static uint GetBit(uint[] x, int bit)
		{
			if (bit == 0)
			{
				return x[0] & 1U;
			}
			int num = bit >> 5;
			if (num < 0 || num >= 6)
			{
				return 0U;
			}
			int num2 = bit & 31;
			return x[num] >> num2 & 1U;
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x000DC590 File Offset: 0x000DA790
		public static bool Gte(uint[] x, uint[] y)
		{
			for (int i = 5; i >= 0; i--)
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

		// Token: 0x06001CDF RID: 7391 RVA: 0x000DC5C0 File Offset: 0x000DA7C0
		public static bool Gte(uint[] x, int xOff, uint[] y, int yOff)
		{
			for (int i = 5; i >= 0; i--)
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

		// Token: 0x06001CE0 RID: 7392 RVA: 0x000DC5F4 File Offset: 0x000DA7F4
		public static bool IsOne(uint[] x)
		{
			if (x[0] != 1U)
			{
				return false;
			}
			for (int i = 1; i < 6; i++)
			{
				if (x[i] != 0U)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x000DC620 File Offset: 0x000DA820
		public static bool IsOne64(ulong[] x)
		{
			if (x[0] != 1UL)
			{
				return false;
			}
			for (int i = 1; i < 3; i++)
			{
				if (x[i] != 0UL)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x000DC64C File Offset: 0x000DA84C
		public static bool IsZero(uint[] x)
		{
			for (int i = 0; i < 6; i++)
			{
				if (x[i] != 0U)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x000DC670 File Offset: 0x000DA870
		public static bool IsZero64(ulong[] x)
		{
			for (int i = 0; i < 3; i++)
			{
				if (x[i] != 0UL)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x000DC694 File Offset: 0x000DA894
		public static void Mul(uint[] x, uint[] y, uint[] zz)
		{
			ulong num = (ulong)y[0];
			ulong num2 = (ulong)y[1];
			ulong num3 = (ulong)y[2];
			ulong num4 = (ulong)y[3];
			ulong num5 = (ulong)y[4];
			ulong num6 = (ulong)y[5];
			ulong num7 = 0UL;
			ulong num8 = (ulong)x[0];
			num7 += num8 * num;
			zz[0] = (uint)num7;
			num7 >>= 32;
			num7 += num8 * num2;
			zz[1] = (uint)num7;
			num7 >>= 32;
			num7 += num8 * num3;
			zz[2] = (uint)num7;
			num7 >>= 32;
			num7 += num8 * num4;
			zz[3] = (uint)num7;
			num7 >>= 32;
			num7 += num8 * num5;
			zz[4] = (uint)num7;
			num7 >>= 32;
			num7 += num8 * num6;
			zz[5] = (uint)num7;
			num7 >>= 32;
			zz[6] = (uint)num7;
			for (int i = 1; i < 6; i++)
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
				zz[i + 6] = (uint)num9;
			}
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x000DC848 File Offset: 0x000DAA48
		public static void Mul(uint[] x, int xOff, uint[] y, int yOff, uint[] zz, int zzOff)
		{
			ulong num = (ulong)y[yOff];
			ulong num2 = (ulong)y[yOff + 1];
			ulong num3 = (ulong)y[yOff + 2];
			ulong num4 = (ulong)y[yOff + 3];
			ulong num5 = (ulong)y[yOff + 4];
			ulong num6 = (ulong)y[yOff + 5];
			ulong num7 = 0UL;
			ulong num8 = (ulong)x[xOff];
			num7 += num8 * num;
			zz[zzOff] = (uint)num7;
			num7 >>= 32;
			num7 += num8 * num2;
			zz[zzOff + 1] = (uint)num7;
			num7 >>= 32;
			num7 += num8 * num3;
			zz[zzOff + 2] = (uint)num7;
			num7 >>= 32;
			num7 += num8 * num4;
			zz[zzOff + 3] = (uint)num7;
			num7 >>= 32;
			num7 += num8 * num5;
			zz[zzOff + 4] = (uint)num7;
			num7 >>= 32;
			num7 += num8 * num6;
			zz[zzOff + 5] = (uint)num7;
			num7 >>= 32;
			zz[zzOff + 6] = (uint)num7;
			for (int i = 1; i < 6; i++)
			{
				zzOff++;
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
				zz[zzOff + 6] = (uint)num9;
			}
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x000DCA34 File Offset: 0x000DAC34
		public static uint MulAddTo(uint[] x, uint[] y, uint[] zz)
		{
			ulong num = (ulong)y[0];
			ulong num2 = (ulong)y[1];
			ulong num3 = (ulong)y[2];
			ulong num4 = (ulong)y[3];
			ulong num5 = (ulong)y[4];
			ulong num6 = (ulong)y[5];
			ulong num7 = 0UL;
			for (int i = 0; i < 6; i++)
			{
				ulong num8 = 0UL;
				ulong num9 = (ulong)x[i];
				num8 += num9 * num + (ulong)zz[i];
				zz[i] = (uint)num8;
				num8 >>= 32;
				num8 += num9 * num2 + (ulong)zz[i + 1];
				zz[i + 1] = (uint)num8;
				num8 >>= 32;
				num8 += num9 * num3 + (ulong)zz[i + 2];
				zz[i + 2] = (uint)num8;
				num8 >>= 32;
				num8 += num9 * num4 + (ulong)zz[i + 3];
				zz[i + 3] = (uint)num8;
				num8 >>= 32;
				num8 += num9 * num5 + (ulong)zz[i + 4];
				zz[i + 4] = (uint)num8;
				num8 >>= 32;
				num8 += num9 * num6 + (ulong)zz[i + 5];
				zz[i + 5] = (uint)num8;
				num8 >>= 32;
				num8 += num7 + (ulong)zz[i + 6];
				zz[i + 6] = (uint)num8;
				num7 = num8 >> 32;
			}
			return (uint)num7;
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x000DCB6C File Offset: 0x000DAD6C
		public static uint MulAddTo(uint[] x, int xOff, uint[] y, int yOff, uint[] zz, int zzOff)
		{
			ulong num = (ulong)y[yOff];
			ulong num2 = (ulong)y[yOff + 1];
			ulong num3 = (ulong)y[yOff + 2];
			ulong num4 = (ulong)y[yOff + 3];
			ulong num5 = (ulong)y[yOff + 4];
			ulong num6 = (ulong)y[yOff + 5];
			ulong num7 = 0UL;
			for (int i = 0; i < 6; i++)
			{
				ulong num8 = 0UL;
				ulong num9 = (ulong)x[xOff + i];
				num8 += num9 * num + (ulong)zz[zzOff];
				zz[zzOff] = (uint)num8;
				num8 >>= 32;
				num8 += num9 * num2 + (ulong)zz[zzOff + 1];
				zz[zzOff + 1] = (uint)num8;
				num8 >>= 32;
				num8 += num9 * num3 + (ulong)zz[zzOff + 2];
				zz[zzOff + 2] = (uint)num8;
				num8 >>= 32;
				num8 += num9 * num4 + (ulong)zz[zzOff + 3];
				zz[zzOff + 3] = (uint)num8;
				num8 >>= 32;
				num8 += num9 * num5 + (ulong)zz[zzOff + 4];
				zz[zzOff + 4] = (uint)num8;
				num8 >>= 32;
				num8 += num9 * num6 + (ulong)zz[zzOff + 5];
				zz[zzOff + 5] = (uint)num8;
				num8 >>= 32;
				num8 += num7 + (ulong)zz[zzOff + 6];
				zz[zzOff + 6] = (uint)num8;
				num7 = num8 >> 32;
				zzOff++;
			}
			return (uint)num7;
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x000DCCC4 File Offset: 0x000DAEC4
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
			return num + num8;
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x000DCDC0 File Offset: 0x000DAFC0
		public static uint MulWordAddExt(uint x, uint[] yy, int yyOff, uint[] zz, int zzOff)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x;
			num += num2 * (ulong)yy[yyOff] + (ulong)zz[zzOff];
			zz[zzOff] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)yy[yyOff + 1] + (ulong)zz[zzOff + 1];
			zz[zzOff + 1] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)yy[yyOff + 2] + (ulong)zz[zzOff + 2];
			zz[zzOff + 2] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)yy[yyOff + 3] + (ulong)zz[zzOff + 3];
			zz[zzOff + 3] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)yy[yyOff + 4] + (ulong)zz[zzOff + 4];
			zz[zzOff + 4] = (uint)num;
			num >>= 32;
			num += num2 * (ulong)yy[yyOff + 5] + (ulong)zz[zzOff + 5];
			zz[zzOff + 5] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x000DCE90 File Offset: 0x000DB090
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
				return Nat.IncAt(6, z, zOff, 4);
			}
			return 0U;
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x000DCF18 File Offset: 0x000DB118
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
				return Nat.IncAt(6, z, zOff, 3);
			}
			return 0U;
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x000DCF7C File Offset: 0x000DB17C
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
				return Nat.IncAt(6, z, zOff, 3);
			}
			return 0U;
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x000DCFE4 File Offset: 0x000DB1E4
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
			while (++num3 < 6);
			return (uint)num;
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x000DD018 File Offset: 0x000DB218
		public static void Square(uint[] x, uint[] zz)
		{
			ulong num = (ulong)x[0];
			uint num2 = 0U;
			int num3 = 5;
			int num4 = 12;
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
			num20 += (num19 >> 32) + num21 * num15;
			num22 += (num20 >> 32) + num21 * num18;
			num23 += num22 >> 32;
			num11 = (uint)num17;
			zz[6] = (num11 << 1 | num2);
			num2 = num11 >> 31;
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
			num11 = zz[11] + (uint)(num23 >> 32);
			zz[11] = (num11 << 1 | num2);
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x000DD318 File Offset: 0x000DB518
		public static void Square(uint[] x, int xOff, uint[] zz, int zzOff)
		{
			ulong num = (ulong)x[xOff];
			uint num2 = 0U;
			int num3 = 5;
			int num4 = 12;
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
			num20 += (num19 >> 32) + num21 * num15;
			num22 += (num20 >> 32) + num21 * num18;
			num23 += num22 >> 32;
			num11 = (uint)num17;
			zz[zzOff + 6] = (num11 << 1 | num2);
			num2 = num11 >> 31;
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
			num11 = zz[zzOff + 11] + (uint)(num23 >> 32);
			zz[zzOff + 11] = (num11 << 1 | num2);
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x000DD650 File Offset: 0x000DB850
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
			return (int)num;
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x000DD6E8 File Offset: 0x000DB8E8
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
			return (int)num;
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x000DD7A8 File Offset: 0x000DB9A8
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
			return (int)num;
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x000DD85C File Offset: 0x000DBA5C
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
			return (int)num;
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x000DD8F4 File Offset: 0x000DBAF4
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
			return (int)num;
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x000DD9A8 File Offset: 0x000DBBA8
		public static BigInteger ToBigInteger(uint[] x)
		{
			byte[] array = new byte[24];
			for (int i = 0; i < 6; i++)
			{
				uint num = x[i];
				if (num != 0U)
				{
					Pack.UInt32_To_BE(num, array, 5 - i << 2);
				}
			}
			return new BigInteger(1, array);
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x000DD9E4 File Offset: 0x000DBBE4
		public static BigInteger ToBigInteger64(ulong[] x)
		{
			byte[] array = new byte[24];
			for (int i = 0; i < 3; i++)
			{
				ulong num = x[i];
				if (num != 0UL)
				{
					Pack.UInt64_To_BE(num, array, 2 - i << 3);
				}
			}
			return new BigInteger(1, array);
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x000DDA1F File Offset: 0x000DBC1F
		public static void Zero(uint[] z)
		{
			z[0] = 0U;
			z[1] = 0U;
			z[2] = 0U;
			z[3] = 0U;
			z[4] = 0U;
			z[5] = 0U;
		}

		// Token: 0x04001808 RID: 6152
		private const ulong M = 4294967295UL;
	}
}
