using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes.Gcm
{
	// Token: 0x02000513 RID: 1299
	internal abstract class GcmUtilities
	{
		// Token: 0x06003197 RID: 12695 RVA: 0x0012F8D0 File Offset: 0x0012DAD0
		private static uint[] GenerateLookup()
		{
			uint[] array = new uint[256];
			for (int i = 0; i < 256; i++)
			{
				uint num = 0U;
				for (int j = 7; j >= 0; j--)
				{
					if ((i & 1 << j) != 0)
					{
						num ^= 3774873600U >> 7 - j;
					}
				}
				array[i] = num;
			}
			return array;
		}

		// Token: 0x06003198 RID: 12696 RVA: 0x0012F924 File Offset: 0x0012DB24
		internal static byte[] OneAsBytes()
		{
			byte[] array = new byte[16];
			array[0] = 128;
			return array;
		}

		// Token: 0x06003199 RID: 12697 RVA: 0x0012F935 File Offset: 0x0012DB35
		internal static uint[] OneAsUints()
		{
			uint[] array = new uint[4];
			array[0] = 2147483648U;
			return array;
		}

		// Token: 0x0600319A RID: 12698 RVA: 0x0012F945 File Offset: 0x0012DB45
		internal static ulong[] OneAsUlongs()
		{
			ulong[] array = new ulong[2];
			array[0] = 9223372036854775808UL;
			return array;
		}

		// Token: 0x0600319B RID: 12699 RVA: 0x0012F959 File Offset: 0x0012DB59
		internal static byte[] AsBytes(uint[] x)
		{
			return Pack.UInt32_To_BE(x);
		}

		// Token: 0x0600319C RID: 12700 RVA: 0x0012F961 File Offset: 0x0012DB61
		internal static void AsBytes(uint[] x, byte[] z)
		{
			Pack.UInt32_To_BE(x, z, 0);
		}

		// Token: 0x0600319D RID: 12701 RVA: 0x0012F96C File Offset: 0x0012DB6C
		internal static byte[] AsBytes(ulong[] x)
		{
			byte[] array = new byte[16];
			Pack.UInt64_To_BE(x, array, 0);
			return array;
		}

		// Token: 0x0600319E RID: 12702 RVA: 0x0012F98A File Offset: 0x0012DB8A
		internal static void AsBytes(ulong[] x, byte[] z)
		{
			Pack.UInt64_To_BE(x, z, 0);
		}

		// Token: 0x0600319F RID: 12703 RVA: 0x0012F994 File Offset: 0x0012DB94
		internal static uint[] AsUints(byte[] bs)
		{
			uint[] array = new uint[4];
			Pack.BE_To_UInt32(bs, 0, array);
			return array;
		}

		// Token: 0x060031A0 RID: 12704 RVA: 0x0012F9B1 File Offset: 0x0012DBB1
		internal static void AsUints(byte[] bs, uint[] output)
		{
			Pack.BE_To_UInt32(bs, 0, output);
		}

		// Token: 0x060031A1 RID: 12705 RVA: 0x0012F9BC File Offset: 0x0012DBBC
		internal static ulong[] AsUlongs(byte[] x)
		{
			ulong[] array = new ulong[2];
			Pack.BE_To_UInt64(x, 0, array);
			return array;
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x0012F9D9 File Offset: 0x0012DBD9
		public static void AsUlongs(byte[] x, ulong[] z)
		{
			Pack.BE_To_UInt64(x, 0, z);
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x0012F9E4 File Offset: 0x0012DBE4
		internal static void Multiply(byte[] x, byte[] y)
		{
			uint[] x2 = GcmUtilities.AsUints(x);
			uint[] y2 = GcmUtilities.AsUints(y);
			GcmUtilities.Multiply(x2, y2);
			GcmUtilities.AsBytes(x2, x);
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x0012FA0C File Offset: 0x0012DC0C
		internal static void Multiply(uint[] x, uint[] y)
		{
			uint num = x[0];
			uint num2 = x[1];
			uint num3 = x[2];
			uint num4 = x[3];
			uint num5 = 0U;
			uint num6 = 0U;
			uint num7 = 0U;
			uint num8 = 0U;
			for (int i = 0; i < 4; i++)
			{
				int num9 = (int)y[i];
				for (int j = 0; j < 32; j++)
				{
					uint num10 = (uint)(num9 >> 31);
					num9 <<= 1;
					num5 ^= (num & num10);
					num6 ^= (num2 & num10);
					num7 ^= (num3 & num10);
					num8 ^= (num4 & num10);
					uint num11 = (uint)((int)((int)num4 << 31) >> 8);
					num4 = (num4 >> 1 | num3 << 31);
					num3 = (num3 >> 1 | num2 << 31);
					num2 = (num2 >> 1 | num << 31);
					num = (num >> 1 ^ (num11 & 3774873600U));
				}
			}
			x[0] = num5;
			x[1] = num6;
			x[2] = num7;
			x[3] = num8;
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x0012FAD4 File Offset: 0x0012DCD4
		internal static void Multiply(ulong[] x, ulong[] y)
		{
			ulong num = x[0];
			ulong num2 = x[1];
			ulong num3 = 0UL;
			ulong num4 = 0UL;
			for (int i = 0; i < 2; i++)
			{
				long num5 = (long)y[i];
				for (int j = 0; j < 64; j++)
				{
					ulong num6 = (ulong)(num5 >> 63);
					num5 <<= 1;
					num3 ^= (num & num6);
					num4 ^= (num2 & num6);
					ulong num7 = num2 << 63 >> 8;
					num2 = (num2 >> 1 | num << 63);
					num = (num >> 1 ^ (num7 & 16212958658533785600UL));
				}
			}
			x[0] = num3;
			x[1] = num4;
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x0012FB5C File Offset: 0x0012DD5C
		internal static void MultiplyP(uint[] x)
		{
			uint num = (uint)((int)GcmUtilities.ShiftRight(x) >> 8);
			x[0] ^= (num & 3774873600U);
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x0012FB84 File Offset: 0x0012DD84
		internal static void MultiplyP(uint[] x, uint[] z)
		{
			uint num = (uint)((int)GcmUtilities.ShiftRight(x, z) >> 8);
			z[0] ^= (num & 3774873600U);
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x0012FBB0 File Offset: 0x0012DDB0
		internal static void MultiplyP8(uint[] x)
		{
			uint num = GcmUtilities.ShiftRightN(x, 8);
			x[0] ^= GcmUtilities.LOOKUP[(int)(num >> 24)];
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x0012FBDC File Offset: 0x0012DDDC
		internal static void MultiplyP8(uint[] x, uint[] y)
		{
			uint num = GcmUtilities.ShiftRightN(x, 8, y);
			y[0] ^= GcmUtilities.LOOKUP[(int)(num >> 24)];
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x0012FC08 File Offset: 0x0012DE08
		internal static uint ShiftRight(uint[] x)
		{
			uint num = x[0];
			x[0] = num >> 1;
			uint num2 = num << 31;
			num = x[1];
			x[1] = (num >> 1 | num2);
			num2 = num << 31;
			num = x[2];
			x[2] = (num >> 1 | num2);
			num2 = num << 31;
			num = x[3];
			x[3] = (num >> 1 | num2);
			return num << 31;
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x0012FC58 File Offset: 0x0012DE58
		internal static uint ShiftRight(uint[] x, uint[] z)
		{
			uint num = x[0];
			z[0] = num >> 1;
			uint num2 = num << 31;
			num = x[1];
			z[1] = (num >> 1 | num2);
			num2 = num << 31;
			num = x[2];
			z[2] = (num >> 1 | num2);
			num2 = num << 31;
			num = x[3];
			z[3] = (num >> 1 | num2);
			return num << 31;
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x0012FCA8 File Offset: 0x0012DEA8
		internal static uint ShiftRightN(uint[] x, int n)
		{
			uint num = x[0];
			int num2 = 32 - n;
			x[0] = num >> n;
			uint num3 = num << num2;
			num = x[1];
			x[1] = (num >> n | num3);
			num3 = num << num2;
			num = x[2];
			x[2] = (num >> n | num3);
			num3 = num << num2;
			num = x[3];
			x[3] = (num >> n | num3);
			return num << num2;
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x0012FD10 File Offset: 0x0012DF10
		internal static uint ShiftRightN(uint[] x, int n, uint[] z)
		{
			uint num = x[0];
			int num2 = 32 - n;
			z[0] = num >> n;
			uint num3 = num << num2;
			num = x[1];
			z[1] = (num >> n | num3);
			num3 = num << num2;
			num = x[2];
			z[2] = (num >> n | num3);
			num3 = num << num2;
			num = x[3];
			z[3] = (num >> n | num3);
			return num << num2;
		}

		// Token: 0x060031AE RID: 12718 RVA: 0x0012FD78 File Offset: 0x0012DF78
		internal static void Xor(byte[] x, byte[] y)
		{
			int num = 0;
			do
			{
				int num2 = num;
				x[num2] ^= y[num];
				num++;
				int num3 = num;
				x[num3] ^= y[num];
				num++;
				int num4 = num;
				x[num4] ^= y[num];
				num++;
				int num5 = num;
				x[num5] ^= y[num];
				num++;
			}
			while (num < 16);
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x0012FDD8 File Offset: 0x0012DFD8
		internal static void Xor(byte[] x, byte[] y, int yOff)
		{
			int num = 0;
			do
			{
				int num2 = num;
				x[num2] ^= y[yOff + num];
				num++;
				int num3 = num;
				x[num3] ^= y[yOff + num];
				num++;
				int num4 = num;
				x[num4] ^= y[yOff + num];
				num++;
				int num5 = num;
				x[num5] ^= y[yOff + num];
				num++;
			}
			while (num < 16);
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x0012FE40 File Offset: 0x0012E040
		internal static void Xor(byte[] x, int xOff, byte[] y, int yOff, byte[] z, int zOff)
		{
			int num = 0;
			do
			{
				z[zOff + num] = (x[xOff + num] ^ y[yOff + num]);
				num++;
				z[zOff + num] = (x[xOff + num] ^ y[yOff + num]);
				num++;
				z[zOff + num] = (x[xOff + num] ^ y[yOff + num]);
				num++;
				z[zOff + num] = (x[xOff + num] ^ y[yOff + num]);
				num++;
			}
			while (num < 16);
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x0012FEB0 File Offset: 0x0012E0B0
		internal static void Xor(byte[] x, byte[] y, int yOff, int yLen)
		{
			while (--yLen >= 0)
			{
				int num = yLen;
				x[num] ^= y[yOff + yLen];
			}
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x0012FECE File Offset: 0x0012E0CE
		internal static void Xor(byte[] x, int xOff, byte[] y, int yOff, int len)
		{
			while (--len >= 0)
			{
				int num = xOff + len;
				x[num] ^= y[yOff + len];
			}
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x0012FEF4 File Offset: 0x0012E0F4
		internal static void Xor(byte[] x, byte[] y, byte[] z)
		{
			int num = 0;
			do
			{
				z[num] = (x[num] ^ y[num]);
				num++;
				z[num] = (x[num] ^ y[num]);
				num++;
				z[num] = (x[num] ^ y[num]);
				num++;
				z[num] = (x[num] ^ y[num]);
				num++;
			}
			while (num < 16);
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x0012FF44 File Offset: 0x0012E144
		internal static void Xor(uint[] x, uint[] y)
		{
			x[0] ^= y[0];
			x[1] ^= y[1];
			x[2] ^= y[2];
			x[3] ^= y[3];
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x0012FF7E File Offset: 0x0012E17E
		internal static void Xor(uint[] x, uint[] y, uint[] z)
		{
			z[0] = (x[0] ^ y[0]);
			z[1] = (x[1] ^ y[1]);
			z[2] = (x[2] ^ y[2]);
			z[3] = (x[3] ^ y[3]);
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x0012FFA8 File Offset: 0x0012E1A8
		internal static void Xor(ulong[] x, ulong[] y)
		{
			x[0] ^= y[0];
			x[1] ^= y[1];
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x000FC5AC File Offset: 0x000FA7AC
		internal static void Xor(ulong[] x, ulong[] y, ulong[] z)
		{
			z[0] = (x[0] ^ y[0]);
			z[1] = (x[1] ^ y[1]);
		}

		// Token: 0x04001FAE RID: 8110
		private const uint E1 = 3774873600U;

		// Token: 0x04001FAF RID: 8111
		private const ulong E1L = 16212958658533785600UL;

		// Token: 0x04001FB0 RID: 8112
		private static readonly uint[] LOOKUP = GcmUtilities.GenerateLookup();
	}
}
