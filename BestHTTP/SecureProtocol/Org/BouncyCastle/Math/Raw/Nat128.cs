using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw
{
	// Token: 0x020002F4 RID: 756
	internal abstract class Nat128
	{
		// Token: 0x06001C76 RID: 7286 RVA: 0x000D9780 File Offset: 0x000D7980
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
			return (uint)num;
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x000D97EC File Offset: 0x000D79EC
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
			return (uint)num;
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x000D986C File Offset: 0x000D7A6C
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
			return (uint)num;
		}

		// Token: 0x06001C79 RID: 7289 RVA: 0x000D98D8 File Offset: 0x000D7AD8
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
			return (uint)num;
		}

		// Token: 0x06001C7A RID: 7290 RVA: 0x000D9958 File Offset: 0x000D7B58
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
			return (uint)num;
		}

		// Token: 0x06001C7B RID: 7291 RVA: 0x000D99EE File Offset: 0x000D7BEE
		public static void Copy(uint[] x, uint[] z)
		{
			z[0] = x[0];
			z[1] = x[1];
			z[2] = x[2];
			z[3] = x[3];
		}

		// Token: 0x06001C7C RID: 7292 RVA: 0x000D9A08 File Offset: 0x000D7C08
		public static void Copy(uint[] x, int xOff, uint[] z, int zOff)
		{
			z[zOff] = x[xOff];
			z[zOff + 1] = x[xOff + 1];
			z[zOff + 2] = x[xOff + 2];
			z[zOff + 3] = x[xOff + 3];
		}

		// Token: 0x06001C7D RID: 7293 RVA: 0x000D9A2E File Offset: 0x000D7C2E
		public static void Copy64(ulong[] x, ulong[] z)
		{
			z[0] = x[0];
			z[1] = x[1];
		}

		// Token: 0x06001C7E RID: 7294 RVA: 0x000D9A3C File Offset: 0x000D7C3C
		public static void Copy64(ulong[] x, int xOff, ulong[] z, int zOff)
		{
			z[zOff] = x[xOff];
			z[zOff + 1] = x[xOff + 1];
		}

		// Token: 0x06001C7F RID: 7295 RVA: 0x000D9A4E File Offset: 0x000D7C4E
		public static uint[] Create()
		{
			return new uint[4];
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x000D9A56 File Offset: 0x000D7C56
		public static ulong[] Create64()
		{
			return new ulong[2];
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x000D9A5E File Offset: 0x000D7C5E
		public static uint[] CreateExt()
		{
			return new uint[8];
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x000D9A66 File Offset: 0x000D7C66
		public static ulong[] CreateExt64()
		{
			return new ulong[4];
		}

		// Token: 0x06001C83 RID: 7299 RVA: 0x000D9A6E File Offset: 0x000D7C6E
		public static bool Diff(uint[] x, int xOff, uint[] y, int yOff, uint[] z, int zOff)
		{
			bool flag = Nat128.Gte(x, xOff, y, yOff);
			if (flag)
			{
				Nat128.Sub(x, xOff, y, yOff, z, zOff);
				return flag;
			}
			Nat128.Sub(y, yOff, x, xOff, z, zOff);
			return flag;
		}

		// Token: 0x06001C84 RID: 7300 RVA: 0x000D9A9C File Offset: 0x000D7C9C
		public static bool Eq(uint[] x, uint[] y)
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

		// Token: 0x06001C85 RID: 7301 RVA: 0x000D9AC0 File Offset: 0x000D7CC0
		public static bool Eq64(ulong[] x, ulong[] y)
		{
			for (int i = 1; i >= 0; i--)
			{
				if (x[i] != y[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x000D9AE4 File Offset: 0x000D7CE4
		public static uint[] FromBigInteger(BigInteger x)
		{
			if (x.SignValue < 0 || x.BitLength > 128)
			{
				throw new ArgumentException();
			}
			uint[] array = Nat128.Create();
			int num = 0;
			while (x.SignValue != 0)
			{
				array[num++] = (uint)x.IntValue;
				x = x.ShiftRight(32);
			}
			return array;
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x000D9B38 File Offset: 0x000D7D38
		public static ulong[] FromBigInteger64(BigInteger x)
		{
			if (x.SignValue < 0 || x.BitLength > 128)
			{
				throw new ArgumentException();
			}
			ulong[] array = Nat128.Create64();
			int num = 0;
			while (x.SignValue != 0)
			{
				array[num++] = (ulong)x.LongValue;
				x = x.ShiftRight(64);
			}
			return array;
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x000D9B8C File Offset: 0x000D7D8C
		public static uint GetBit(uint[] x, int bit)
		{
			if (bit == 0)
			{
				return x[0] & 1U;
			}
			if ((bit & 127) != bit)
			{
				return 0U;
			}
			int num = bit >> 5;
			int num2 = bit & 31;
			return x[num] >> num2 & 1U;
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x000D9BC0 File Offset: 0x000D7DC0
		public static bool Gte(uint[] x, uint[] y)
		{
			for (int i = 3; i >= 0; i--)
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

		// Token: 0x06001C8A RID: 7306 RVA: 0x000D9BF0 File Offset: 0x000D7DF0
		public static bool Gte(uint[] x, int xOff, uint[] y, int yOff)
		{
			for (int i = 3; i >= 0; i--)
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

		// Token: 0x06001C8B RID: 7307 RVA: 0x000D9C24 File Offset: 0x000D7E24
		public static bool IsOne(uint[] x)
		{
			if (x[0] != 1U)
			{
				return false;
			}
			for (int i = 1; i < 4; i++)
			{
				if (x[i] != 0U)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x000D9C50 File Offset: 0x000D7E50
		public static bool IsOne64(ulong[] x)
		{
			if (x[0] != 1UL)
			{
				return false;
			}
			for (int i = 1; i < 2; i++)
			{
				if (x[i] != 0UL)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x000D9C7C File Offset: 0x000D7E7C
		public static bool IsZero(uint[] x)
		{
			for (int i = 0; i < 4; i++)
			{
				if (x[i] != 0U)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x000D9CA0 File Offset: 0x000D7EA0
		public static bool IsZero64(ulong[] x)
		{
			for (int i = 0; i < 2; i++)
			{
				if (x[i] != 0UL)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x000D9CC4 File Offset: 0x000D7EC4
		public static void Mul(uint[] x, uint[] y, uint[] zz)
		{
			ulong num = (ulong)y[0];
			ulong num2 = (ulong)y[1];
			ulong num3 = (ulong)y[2];
			ulong num4 = (ulong)y[3];
			ulong num5 = 0UL;
			ulong num6 = (ulong)x[0];
			num5 += num6 * num;
			zz[0] = (uint)num5;
			num5 >>= 32;
			num5 += num6 * num2;
			zz[1] = (uint)num5;
			num5 >>= 32;
			num5 += num6 * num3;
			zz[2] = (uint)num5;
			num5 >>= 32;
			num5 += num6 * num4;
			zz[3] = (uint)num5;
			num5 >>= 32;
			zz[4] = (uint)num5;
			for (int i = 1; i < 4; i++)
			{
				ulong num7 = 0UL;
				ulong num8 = (ulong)x[i];
				num7 += num8 * num + (ulong)zz[i];
				zz[i] = (uint)num7;
				num7 >>= 32;
				num7 += num8 * num2 + (ulong)zz[i + 1];
				zz[i + 1] = (uint)num7;
				num7 >>= 32;
				num7 += num8 * num3 + (ulong)zz[i + 2];
				zz[i + 2] = (uint)num7;
				num7 >>= 32;
				num7 += num8 * num4 + (ulong)zz[i + 3];
				zz[i + 3] = (uint)num7;
				num7 >>= 32;
				zz[i + 4] = (uint)num7;
			}
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x000D9DF8 File Offset: 0x000D7FF8
		public static void Mul(uint[] x, int xOff, uint[] y, int yOff, uint[] zz, int zzOff)
		{
			ulong num = (ulong)y[yOff];
			ulong num2 = (ulong)y[yOff + 1];
			ulong num3 = (ulong)y[yOff + 2];
			ulong num4 = (ulong)y[yOff + 3];
			ulong num5 = 0UL;
			ulong num6 = (ulong)x[xOff];
			num5 += num6 * num;
			zz[zzOff] = (uint)num5;
			num5 >>= 32;
			num5 += num6 * num2;
			zz[zzOff + 1] = (uint)num5;
			num5 >>= 32;
			num5 += num6 * num3;
			zz[zzOff + 2] = (uint)num5;
			num5 >>= 32;
			num5 += num6 * num4;
			zz[zzOff + 3] = (uint)num5;
			num5 >>= 32;
			zz[zzOff + 4] = (uint)num5;
			for (int i = 1; i < 4; i++)
			{
				zzOff++;
				ulong num7 = 0UL;
				ulong num8 = (ulong)x[xOff + i];
				num7 += num8 * num + (ulong)zz[zzOff];
				zz[zzOff] = (uint)num7;
				num7 >>= 32;
				num7 += num8 * num2 + (ulong)zz[zzOff + 1];
				zz[zzOff + 1] = (uint)num7;
				num7 >>= 32;
				num7 += num8 * num3 + (ulong)zz[zzOff + 2];
				zz[zzOff + 2] = (uint)num7;
				num7 >>= 32;
				num7 += num8 * num4 + (ulong)zz[zzOff + 3];
				zz[zzOff + 3] = (uint)num7;
				num7 >>= 32;
				zz[zzOff + 4] = (uint)num7;
			}
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x000D9F54 File Offset: 0x000D8154
		public static uint MulAddTo(uint[] x, uint[] y, uint[] zz)
		{
			ulong num = (ulong)y[0];
			ulong num2 = (ulong)y[1];
			ulong num3 = (ulong)y[2];
			ulong num4 = (ulong)y[3];
			ulong num5 = 0UL;
			for (int i = 0; i < 4; i++)
			{
				ulong num6 = 0UL;
				ulong num7 = (ulong)x[i];
				num6 += num7 * num + (ulong)zz[i];
				zz[i] = (uint)num6;
				num6 >>= 32;
				num6 += num7 * num2 + (ulong)zz[i + 1];
				zz[i + 1] = (uint)num6;
				num6 >>= 32;
				num6 += num7 * num3 + (ulong)zz[i + 2];
				zz[i + 2] = (uint)num6;
				num6 >>= 32;
				num6 += num7 * num4 + (ulong)zz[i + 3];
				zz[i + 3] = (uint)num6;
				num6 >>= 32;
				num6 += num5 + (ulong)zz[i + 4];
				zz[i + 4] = (uint)num6;
				num5 = num6 >> 32;
			}
			return (uint)num5;
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x000DA03C File Offset: 0x000D823C
		public static uint MulAddTo(uint[] x, int xOff, uint[] y, int yOff, uint[] zz, int zzOff)
		{
			ulong num = (ulong)y[yOff];
			ulong num2 = (ulong)y[yOff + 1];
			ulong num3 = (ulong)y[yOff + 2];
			ulong num4 = (ulong)y[yOff + 3];
			ulong num5 = 0UL;
			for (int i = 0; i < 4; i++)
			{
				ulong num6 = 0UL;
				ulong num7 = (ulong)x[xOff + i];
				num6 += num7 * num + (ulong)zz[zzOff];
				zz[zzOff] = (uint)num6;
				num6 >>= 32;
				num6 += num7 * num2 + (ulong)zz[zzOff + 1];
				zz[zzOff + 1] = (uint)num6;
				num6 >>= 32;
				num6 += num7 * num3 + (ulong)zz[zzOff + 2];
				zz[zzOff + 2] = (uint)num6;
				num6 >>= 32;
				num6 += num7 * num4 + (ulong)zz[zzOff + 3];
				zz[zzOff + 3] = (uint)num6;
				num6 >>= 32;
				num6 += num5 + (ulong)zz[zzOff + 4];
				zz[zzOff + 4] = (uint)num6;
				num5 = num6 >> 32;
				zzOff++;
			}
			return (uint)num5;
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x000DA13C File Offset: 0x000D833C
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
			return num + num6;
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x000DA1E8 File Offset: 0x000D83E8
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
			return (uint)num;
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x000DA278 File Offset: 0x000D8478
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
			return (uint)num;
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x000DA2F4 File Offset: 0x000D84F4
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
				return Nat.IncAt(4, z, zOff, 3);
			}
			return 0U;
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x000DA358 File Offset: 0x000D8558
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
				return Nat.IncAt(4, z, zOff, 3);
			}
			return 0U;
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x000DA3C0 File Offset: 0x000D85C0
		public static uint MulWordsAdd(uint x, uint y, uint[] z, int zOff)
		{
			ulong num = 0UL;
			ulong num2 = (ulong)x;
			ulong num3 = (ulong)y;
			num += num3 * num2 + (ulong)z[zOff];
			z[zOff] = (uint)num;
			num >>= 32;
			num += (ulong)z[zOff + 1];
			z[zOff + 1] = (uint)num;
			num >>= 32;
			if (num != 0UL)
			{
				return Nat.IncAt(4, z, zOff, 2);
			}
			return 0U;
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x000DA410 File Offset: 0x000D8610
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
			while (++num3 < 4);
			return (uint)num;
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x000DA444 File Offset: 0x000D8644
		public static void Square(uint[] x, uint[] zz)
		{
			ulong num = (ulong)x[0];
			uint num2 = 0U;
			int num3 = 3;
			int num4 = 8;
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
			num17 += num16 >> 32;
			num11 = (uint)num14;
			zz[4] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = (uint)num16;
			zz[5] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = (uint)num17;
			zz[6] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = zz[7] + (uint)(num17 >> 32);
			zz[7] = (num11 << 1 | num2);
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x000DA5E8 File Offset: 0x000D87E8
		public static void Square(uint[] x, int xOff, uint[] zz, int zzOff)
		{
			ulong num = (ulong)x[xOff];
			uint num2 = 0U;
			int num3 = 3;
			int num4 = 8;
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
			num17 += num16 >> 32;
			num11 = (uint)num14;
			zz[zzOff + 4] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = (uint)num16;
			zz[zzOff + 5] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = (uint)num17;
			zz[zzOff + 6] = (num11 << 1 | num2);
			num2 = num11 >> 31;
			num11 = zz[zzOff + 7] + (uint)(num17 >> 32);
			zz[zzOff + 7] = (num11 << 1 | num2);
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x000DA7B0 File Offset: 0x000D89B0
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
			return (int)num;
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x000DA81C File Offset: 0x000D8A1C
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
			return (int)num;
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x000DA8A0 File Offset: 0x000D8AA0
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
			return (int)num;
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x000DA920 File Offset: 0x000D8B20
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
			return (int)num;
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x000DA98C File Offset: 0x000D8B8C
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
			return (int)num;
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x000DAA08 File Offset: 0x000D8C08
		public static BigInteger ToBigInteger(uint[] x)
		{
			byte[] array = new byte[16];
			for (int i = 0; i < 4; i++)
			{
				uint num = x[i];
				if (num != 0U)
				{
					Pack.UInt32_To_BE(num, array, 3 - i << 2);
				}
			}
			return new BigInteger(1, array);
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x000DAA44 File Offset: 0x000D8C44
		public static BigInteger ToBigInteger64(ulong[] x)
		{
			byte[] array = new byte[16];
			for (int i = 0; i < 2; i++)
			{
				ulong num = x[i];
				if (num != 0UL)
				{
					Pack.UInt64_To_BE(num, array, 1 - i << 3);
				}
			}
			return new BigInteger(1, array);
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x000DAA7F File Offset: 0x000D8C7F
		public static void Zero(uint[] z)
		{
			z[0] = 0U;
			z[1] = 0U;
			z[2] = 0U;
			z[3] = 0U;
		}

		// Token: 0x04001806 RID: 6150
		private const ulong M = 4294967295UL;
	}
}
