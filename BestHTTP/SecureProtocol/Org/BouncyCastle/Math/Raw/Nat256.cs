using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw
{
	// Token: 0x020002F8 RID: 760
	internal abstract class Nat256
	{
		// Token: 0x06001D22 RID: 7458 RVA: 0x000DFA64 File Offset: 0x000DDC64
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
			num += (ulong)x[7] + (ulong)y[7];
			z[7] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x06001D23 RID: 7459 RVA: 0x000DFB28 File Offset: 0x000DDD28
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
			num += (ulong)x[xOff + 7] + (ulong)y[yOff + 7];
			z[zOff + 7] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x06001D24 RID: 7460 RVA: 0x000DFC24 File Offset: 0x000DDE24
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
			num += (ulong)x[7] + (ulong)y[7] + (ulong)z[7];
			z[7] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x000DFD10 File Offset: 0x000DDF10
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
			num += (ulong)x[xOff + 7] + (ulong)y[yOff + 7] + (ulong)z[zOff + 7];
			z[zOff + 7] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x000DFE54 File Offset: 0x000DE054
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
			num += (ulong)x[7] + (ulong)z[7];
			z[7] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x000DFF18 File Offset: 0x000DE118
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
			num += (ulong)x[xOff + 7] + (ulong)z[zOff + 7];
			z[zOff + 7] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x000E0008 File Offset: 0x000DE208
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
			num += (ulong)u[uOff + 7] + (ulong)v[vOff + 7];
			u[uOff + 7] = (uint)num;
			v[vOff + 7] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x000E012A File Offset: 0x000DE32A
		public static void Copy(uint[] x, uint[] z)
		{
			z[0] = x[0];
			z[1] = x[1];
			z[2] = x[2];
			z[3] = x[3];
			z[4] = x[4];
			z[5] = x[5];
			z[6] = x[6];
			z[7] = x[7];
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x000E015C File Offset: 0x000DE35C
		public static void Copy(uint[] x, int xOff, uint[] z, int zOff)
		{
			z[zOff] = x[xOff];
			z[zOff + 1] = x[xOff + 1];
			z[zOff + 2] = x[xOff + 2];
			z[zOff + 3] = x[xOff + 3];
			z[zOff + 4] = x[xOff + 4];
			z[zOff + 5] = x[xOff + 5];
			z[zOff + 6] = x[xOff + 6];
			z[zOff + 7] = x[xOff + 7];
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x000E01B5 File Offset: 0x000DE3B5
		public static void Copy64(ulong[] x, ulong[] z)
		{
			z[0] = x[0];
			z[1] = x[1];
			z[2] = x[2];
			z[3] = x[3];
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x000E01CF File Offset: 0x000DE3CF
		public static void Copy64(ulong[] x, int xOff, ulong[] z, int zOff)
		{
			z[zOff] = x[xOff];
			z[zOff + 1] = x[xOff + 1];
			z[zOff + 2] = x[xOff + 2];
			z[zOff + 3] = x[xOff + 3];
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x000D9A5E File Offset: 0x000D7C5E
		public static uint[] Create()
		{
			return new uint[8];
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x000D9A66 File Offset: 0x000D7C66
		public static ulong[] Create64()
		{
			return new ulong[4];
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x000E01F5 File Offset: 0x000DE3F5
		public static uint[] CreateExt()
		{
			return new uint[16];
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x000E01FE File Offset: 0x000DE3FE
		public static ulong[] CreateExt64()
		{
			return new ulong[8];
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x000E0206 File Offset: 0x000DE406
		public static bool Diff(uint[] x, int xOff, uint[] y, int yOff, uint[] z, int zOff)
		{
			bool flag = Nat256.Gte(x, xOff, y, yOff);
			if (flag)
			{
				Nat256.Sub(x, xOff, y, yOff, z, zOff);
				return flag;
			}
			Nat256.Sub(y, yOff, x, xOff, z, zOff);
			return flag;
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x000E0234 File Offset: 0x000DE434
		public static bool Eq(uint[] x, uint[] y)
		{
			for (int i = 7; i >= 0; i--)
			{
				if (x[i] != y[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x000E0258 File Offset: 0x000DE458
		public static bool Eq64(ulong[] x, ulong[] y)
		{
			for (int i = 3; i >= 0; i--)
			{
				if (x[i] != y[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x000E027C File Offset: 0x000DE47C
		public static uint[] FromBigInteger(BigInteger x)
		{
			if (x.SignValue < 0 || x.BitLength > 256)
			{
				throw new ArgumentException();
			}
			uint[] array = Nat256.Create();
			int num = 0;
			while (x.SignValue != 0)
			{
				array[num++] = (uint)x.IntValue;
				x = x.ShiftRight(32);
			}
			return array;
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x000E02D0 File Offset: 0x000DE4D0
		public static ulong[] FromBigInteger64(BigInteger x)
		{
			if (x.SignValue < 0 || x.BitLength > 256)
			{
				throw new ArgumentException();
			}
			ulong[] array = Nat256.Create64();
			int num = 0;
			while (x.SignValue != 0)
			{
				array[num++] = (ulong)x.LongValue;
				x = x.ShiftRight(64);
			}
			return array;
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x000E0324 File Offset: 0x000DE524
		public static uint GetBit(uint[] x, int bit)
		{
			if (bit == 0)
			{
				return x[0] & 1U;
			}
			if ((bit & 255) != bit)
			{
				return 0U;
			}
			int num = bit >> 5;
			int num2 = bit & 31;
			return x[num] >> num2 & 1U;
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x000E035C File Offset: 0x000DE55C
		public static bool Gte(uint[] x, uint[] y)
		{
			for (int i = 7; i >= 0; i--)
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

		// Token: 0x06001D38 RID: 7480 RVA: 0x000E038C File Offset: 0x000DE58C
		public static bool Gte(uint[] x, int xOff, uint[] y, int yOff)
		{
			for (int i = 7; i >= 0; i--)
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

		// Token: 0x06001D39 RID: 7481 RVA: 0x000E03C0 File Offset: 0x000DE5C0
		public static bool IsOne(uint[] x)
		{
			if (x[0] != 1U)
			{
				return false;
			}
			for (int i = 1; i < 8; i++)
			{
				if (x[i] != 0U)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x000E03EC File Offset: 0x000DE5EC
		public static bool IsOne64(ulong[] x)
		{
			if (x[0] != 1UL)
			{
				return false;
			}
			for (int i = 1; i < 4; i++)
			{
				if (x[i] != 0UL)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x000E0418 File Offset: 0x000DE618
		public static bool IsZero(uint[] x)
		{
			for (int i = 0; i < 8; i++)
			{
				if (x[i] != 0U)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x000E043C File Offset: 0x000DE63C
		public static bool IsZero64(ulong[] x)
		{
			for (int i = 0; i < 4; i++)
			{
				if (x[i] != 0UL)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x000E0460 File Offset: 0x000DE660
		public static void Mul(uint[] x, uint[] y, uint[] zz)
		{
			ulong num = (ulong)y[0];
			ulong num2 = (ulong)y[1];
			ulong num3 = (ulong)y[2];
			ulong num4 = (ulong)y[3];
			ulong num5 = (ulong)y[4];
			ulong num6 = (ulong)y[5];
			ulong num7 = (ulong)y[6];
			ulong num8 = (ulong)y[7];
			ulong num9 = 0UL;
			ulong num10 = (ulong)x[0];
			num9 += num10 * num;
			zz[0] = (uint)num9;
			num9 >>= 32;
			num9 += num10 * num2;
			zz[1] = (uint)num9;
			num9 >>= 32;
			num9 += num10 * num3;
			zz[2] = (uint)num9;
			num9 >>= 32;
			num9 += num10 * num4;
			zz[3] = (uint)num9;
			num9 >>= 32;
			num9 += num10 * num5;
			zz[4] = (uint)num9;
			num9 >>= 32;
			num9 += num10 * num6;
			zz[5] = (uint)num9;
			num9 >>= 32;
			num9 += num10 * num7;
			zz[6] = (uint)num9;
			num9 >>= 32;
			num9 += num10 * num8;
			zz[7] = (uint)num9;
			num9 >>= 32;
			zz[8] = (uint)num9;
			for (int i = 1; i < 8; i++)
			{
				ulong num11 = 0UL;
				ulong num12 = (ulong)x[i];
				num11 += num12 * num + (ulong)zz[i];
				zz[i] = (uint)num11;
				num11 >>= 32;
				num11 += num12 * num2 + (ulong)zz[i + 1];
				zz[i + 1] = (uint)num11;
				num11 >>= 32;
				num11 += num12 * num3 + (ulong)zz[i + 2];
				zz[i + 2] = (uint)num11;
				num11 >>= 32;
				num11 += num12 * num4 + (ulong)zz[i + 3];
				zz[i + 3] = (uint)num11;
				num11 >>= 32;
				num11 += num12 * num5 + (ulong)zz[i + 4];
				zz[i + 4] = (uint)num11;
				num11 >>= 32;
				num11 += num12 * num6 + (ulong)zz[i + 5];
				zz[i + 5] = (uint)num11;
				num11 >>= 32;
				num11 += num12 * num7 + (ulong)zz[i + 6];
				zz[i + 6] = (uint)num11;
				num11 >>= 32;
				num11 += num12 * num8 + (ulong)zz[i + 7];
				zz[i + 7] = (uint)num11;
				num11 >>= 32;
				zz[i + 8] = (uint)num11;
			}
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x000E0690 File Offset: 0x000DE890
		public static void Mul(uint[] x, int xOff, uint[] y, int yOff, uint[] zz, int zzOff)
		{
			ulong num = (ulong)y[yOff];
			ulong num2 = (ulong)y[yOff + 1];
			ulong num3 = (ulong)y[yOff + 2];
			ulong num4 = (ulong)y[yOff + 3];
			ulong num5 = (ulong)y[yOff + 4];
			ulong num6 = (ulong)y[yOff + 5];
			ulong num7 = (ulong)y[yOff + 6];
			ulong num8 = (ulong)y[yOff + 7];
			ulong num9 = 0UL;
			ulong num10 = (ulong)x[xOff];
			num9 += num10 * num;
			zz[zzOff] = (uint)num9;
			num9 >>= 32;
			num9 += num10 * num2;
			zz[zzOff + 1] = (uint)num9;
			num9 >>= 32;
			num9 += num10 * num3;
			zz[zzOff + 2] = (uint)num9;
			num9 >>= 32;
			num9 += num10 * num4;
			zz[zzOff + 3] = (uint)num9;
			num9 >>= 32;
			num9 += num10 * num5;
			zz[zzOff + 4] = (uint)num9;
			num9 >>= 32;
			num9 += num10 * num6;
			zz[zzOff + 5] = (uint)num9;
			num9 >>= 32;
			num9 += num10 * num7;
			zz[zzOff + 6] = (uint)num9;
			num9 >>= 32;
			num9 += num10 * num8;
			zz[zzOff + 7] = (uint)num9;
			num9 >>= 32;
			zz[zzOff + 8] = (uint)num9;
			for (int i = 1; i < 8; i++)
			{
				zzOff++;
				ulong num11 = 0UL;
				ulong num12 = (ulong)x[xOff + i];
				num11 += num12 * num + (ulong)zz[zzOff];
				zz[zzOff] = (uint)num11;
				num11 >>= 32;
				num11 += num12 * num2 + (ulong)zz[zzOff + 1];
				zz[zzOff + 1] = (uint)num11;
				num11 >>= 32;
				num11 += num12 * num3 + (ulong)zz[zzOff + 2];
				zz[zzOff + 2] = (uint)num11;
				num11 >>= 32;
				num11 += num12 * num4 + (ulong)zz[zzOff + 3];
				zz[zzOff + 3] = (uint)num11;
				num11 >>= 32;
				num11 += num12 * num5 + (ulong)zz[zzOff + 4];
				zz[zzOff + 4] = (uint)num11;
				num11 >>= 32;
				num11 += num12 * num6 + (ulong)zz[zzOff + 5];
				zz[zzOff + 5] = (uint)num11;
				num11 >>= 32;
				num11 += num12 * num7 + (ulong)zz[zzOff + 6];
				zz[zzOff + 6] = (uint)num11;
				num11 >>= 32;
				num11 += num12 * num8 + (ulong)zz[zzOff + 7];
				zz[zzOff + 7] = (uint)num11;
				num11 >>= 32;
				zz[zzOff + 8] = (uint)num11;
			}
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x000E0908 File Offset: 0x000DEB08
		public static uint MulAddTo(uint[] x, uint[] y, uint[] zz)
		{
			ulong num = (ulong)y[0];
			ulong num2 = (ulong)y[1];
			ulong num3 = (ulong)y[2];
			ulong num4 = (ulong)y[3];
			ulong num5 = (ulong)y[4];
			ulong num6 = (ulong)y[5];
			ulong num7 = (ulong)y[6];
			ulong num8 = (ulong)y[7];
			ulong num9 = 0UL;
			for (int i = 0; i < 8; i++)
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
				num10 += num11 * num8 + (ulong)zz[i + 7];
				zz[i + 7] = (uint)num10;
				num10 >>= 32;
				num10 += num9 + (ulong)zz[i + 8];
				zz[i + 8] = (uint)num10;
				num9 = num10 >> 32;
			}
			return (uint)num9;
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x000E0A90 File Offset: 0x000DEC90
		public static uint MulAddTo(uint[] x, int xOff, uint[] y, int yOff, uint[] zz, int zzOff)
		{
			ulong num = (ulong)y[yOff];
			ulong num2 = (ulong)y[yOff + 1];
			ulong num3 = (ulong)y[yOff + 2];
			ulong num4 = (ulong)y[yOff + 3];
			ulong num5 = (ulong)y[yOff + 4];
			ulong num6 = (ulong)y[yOff + 5];
			ulong num7 = (ulong)y[yOff + 6];
			ulong num8 = (ulong)y[yOff + 7];
			ulong num9 = 0UL;
			for (int i = 0; i < 8; i++)
			{
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
				num10 += num11 * num8 + (ulong)zz[zzOff + 7];
				zz[zzOff + 7] = (uint)num10;
				num10 >>= 32;
				num10 += num9 + (ulong)zz[zzOff + 8];
				zz[zzOff + 8] = (uint)num10;
				num9 = num10 >> 32;
				zzOff++;
			}
			return (uint)num9;
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x000E0C40 File Offset: 0x000DEE40
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
			ulong num10 = (ulong)x[xOff + 7];
			num += num2 * num10 + num9 + (ulong)y[yOff + 7];
			z[zOff + 7] = (uint)num;
			num >>= 32;
			return num + num10;
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x000E0D8C File Offset: 0x000DEF8C
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
			num += num2 * (ulong)z[7];
			z[7] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x000E0E3C File Offset: 0x000DF03C
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
			num += num2 * (ulong)z[7] + (ulong)y[7];
			z[7] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x000E0F14 File Offset: 0x000DF114
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
			num += num2 * (ulong)y[yOff + 7] + (ulong)z[zOff + 7];
			z[zOff + 7] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x000E1024 File Offset: 0x000DF224
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
				return Nat.IncAt(8, z, zOff, 4);
			}
			return 0U;
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x000E10AC File Offset: 0x000DF2AC
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
				return Nat.IncAt(8, z, zOff, 3);
			}
			return 0U;
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x000E1110 File Offset: 0x000DF310
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
				return Nat.IncAt(8, z, zOff, 3);
			}
			return 0U;
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x000E1178 File Offset: 0x000DF378
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
			while (++num3 < 8);
			return (uint)num;
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x000E11AC File Offset: 0x000DF3AC
		public static void Square(uint[] x, uint[] zz)
		{
			ulong num = (ulong)x[0];
			uint num2 = 0U;
			int num3 = 7;
			int num4 = 16;
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
			num19 &= (ulong)-1;
			num22 += (num20 >> 32) + num24 * num15;
			num20 &= (ulong)-1;
			num23 += (num22 >> 32) + num24 * num18;
			num22 &= (ulong)-1;
			num25 += (num23 >> 32) + num24 * num21;
			num23 &= (ulong)-1;
			num26 += num25 >> 32;
			num25 &= (ulong)-1;
			ulong num27 = (ulong)x[7];
			ulong num28 = (ulong)zz[13] + (num26 >> 32);
			num26 &= (ulong)-1;
			ulong num29 = (ulong)zz[14] + (num28 >> 32);
			num28 &= (ulong)-1;
			num19 += num27 * num;
			num11 = (uint)num19;
			zz[7] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num20 += (num19 >> 32) + num27 * num9;
			num22 += (num20 >> 32) + num27 * num12;
			num23 += (num22 >> 32) + num27 * num15;
			num25 += (num23 >> 32) + num27 * num18;
			num26 += (num25 >> 32) + num27 * num21;
			num28 += (num26 >> 32) + num27 * num24;
			num29 += num28 >> 32;
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
			num11 = (uint)num28;
			zz[13] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = (uint)num29;
			zz[14] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = zz[15] + (uint)(num29 >> 32);
			zz[15] = (num11 << 1 | num2);
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x000E1664 File Offset: 0x000DF864
		public static void Square(uint[] x, int xOff, uint[] zz, int zzOff)
		{
			ulong num = (ulong)x[xOff];
			uint num2 = 0U;
			int num3 = 7;
			int num4 = 16;
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
			num19 &= (ulong)-1;
			num22 += (num20 >> 32) + num24 * num15;
			num20 &= (ulong)-1;
			num23 += (num22 >> 32) + num24 * num18;
			num22 &= (ulong)-1;
			num25 += (num23 >> 32) + num24 * num21;
			num23 &= (ulong)-1;
			num26 += num25 >> 32;
			num25 &= (ulong)-1;
			ulong num27 = (ulong)x[xOff + 7];
			ulong num28 = (ulong)zz[zzOff + 13] + (num26 >> 32);
			num26 &= (ulong)-1;
			ulong num29 = (ulong)zz[zzOff + 14] + (num28 >> 32);
			num28 &= (ulong)-1;
			num19 += num27 * num;
			num11 = (uint)num19;
			zz[zzOff + 7] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num20 += (num19 >> 32) + num27 * num9;
			num22 += (num20 >> 32) + num27 * num12;
			num23 += (num22 >> 32) + num27 * num15;
			num25 += (num23 >> 32) + num27 * num18;
			num26 += (num25 >> 32) + num27 * num21;
			num28 += (num26 >> 32) + num27 * num24;
			num29 += num28 >> 32;
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
			num11 = (uint)num28;
			zz[zzOff + 13] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = (uint)num29;
			zz[zzOff + 14] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = zz[zzOff + 15] + (uint)(num29 >> 32);
			zz[zzOff + 15] = (num11 << 1 | num2);
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x000E1B6C File Offset: 0x000DFD6C
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
			num += (long)((ulong)x[7] - (ulong)y[7]);
			z[7] = (uint)num;
			num >>= 32;
			return (int)num;
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x000E1C30 File Offset: 0x000DFE30
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
			num += (long)((ulong)x[xOff + 7] - (ulong)y[yOff + 7]);
			z[zOff + 7] = (uint)num;
			num >>= 32;
			return (int)num;
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x000E1D2C File Offset: 0x000DFF2C
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
			num += (long)((ulong)z[7] - (ulong)x[7] - (ulong)y[7]);
			z[7] = (uint)num;
			num >>= 32;
			return (int)num;
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x000E1E18 File Offset: 0x000E0018
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
			num += (long)((ulong)z[7] - (ulong)x[7]);
			z[7] = (uint)num;
			num >>= 32;
			return (int)num;
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x000E1EDC File Offset: 0x000E00DC
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
			num += (long)((ulong)z[zOff + 7] - (ulong)x[xOff + 7]);
			z[zOff + 7] = (uint)num;
			num >>= 32;
			return (int)num;
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x000E1FC8 File Offset: 0x000E01C8
		public static BigInteger ToBigInteger(uint[] x)
		{
			byte[] array = new byte[32];
			for (int i = 0; i < 8; i++)
			{
				uint num = x[i];
				if (num != 0U)
				{
					Pack.UInt32_To_BE(num, array, 7 - i << 2);
				}
			}
			return new BigInteger(1, array);
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x000E2004 File Offset: 0x000E0204
		public static BigInteger ToBigInteger64(ulong[] x)
		{
			byte[] array = new byte[32];
			for (int i = 0; i < 4; i++)
			{
				ulong num = x[i];
				if (num != 0UL)
				{
					Pack.UInt64_To_BE(num, array, 3 - i << 3);
				}
			}
			return new BigInteger(1, array);
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x000E203F File Offset: 0x000E023F
		public static void Zero(uint[] z)
		{
			z[0] = 0U;
			z[1] = 0U;
			z[2] = 0U;
			z[3] = 0U;
			z[4] = 0U;
			z[5] = 0U;
			z[6] = 0U;
			z[7] = 0U;
		}

		// Token: 0x0400180A RID: 6154
		private const ulong M = 4294967295UL;
	}
}
