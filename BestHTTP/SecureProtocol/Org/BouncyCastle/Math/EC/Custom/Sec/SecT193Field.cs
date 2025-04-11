using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200037F RID: 895
	internal class SecT193Field
	{
		// Token: 0x0600242F RID: 9263 RVA: 0x000FC5C2 File Offset: 0x000FA7C2
		public static void Add(ulong[] x, ulong[] y, ulong[] z)
		{
			z[0] = (x[0] ^ y[0]);
			z[1] = (x[1] ^ y[1]);
			z[2] = (x[2] ^ y[2]);
			z[3] = (x[3] ^ y[3]);
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x001015AC File Offset: 0x000FF7AC
		public static void AddExt(ulong[] xx, ulong[] yy, ulong[] zz)
		{
			zz[0] = (xx[0] ^ yy[0]);
			zz[1] = (xx[1] ^ yy[1]);
			zz[2] = (xx[2] ^ yy[2]);
			zz[3] = (xx[3] ^ yy[3]);
			zz[4] = (xx[4] ^ yy[4]);
			zz[5] = (xx[5] ^ yy[5]);
			zz[6] = (xx[6] ^ yy[6]);
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x001015FF File Offset: 0x000FF7FF
		public static void AddOne(ulong[] x, ulong[] z)
		{
			z[0] = (x[0] ^ 1UL);
			z[1] = x[1];
			z[2] = x[2];
			z[3] = x[3];
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x0010161C File Offset: 0x000FF81C
		public static ulong[] FromBigInteger(BigInteger x)
		{
			ulong[] array = Nat256.FromBigInteger64(x);
			SecT193Field.Reduce63(array, 0);
			return array;
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x0010162C File Offset: 0x000FF82C
		public static void Invert(ulong[] x, ulong[] z)
		{
			if (Nat256.IsZero64(x))
			{
				throw new InvalidOperationException();
			}
			ulong[] array = Nat256.Create64();
			ulong[] array2 = Nat256.Create64();
			SecT193Field.Square(x, array);
			SecT193Field.SquareN(array, 1, array2);
			SecT193Field.Multiply(array, array2, array);
			SecT193Field.SquareN(array2, 1, array2);
			SecT193Field.Multiply(array, array2, array);
			SecT193Field.SquareN(array, 3, array2);
			SecT193Field.Multiply(array, array2, array);
			SecT193Field.SquareN(array, 6, array2);
			SecT193Field.Multiply(array, array2, array);
			SecT193Field.SquareN(array, 12, array2);
			SecT193Field.Multiply(array, array2, array);
			SecT193Field.SquareN(array, 24, array2);
			SecT193Field.Multiply(array, array2, array);
			SecT193Field.SquareN(array, 48, array2);
			SecT193Field.Multiply(array, array2, array);
			SecT193Field.SquareN(array, 96, array2);
			SecT193Field.Multiply(array, array2, z);
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x001016E0 File Offset: 0x000FF8E0
		public static void Multiply(ulong[] x, ulong[] y, ulong[] z)
		{
			ulong[] array = Nat256.CreateExt64();
			SecT193Field.ImplMultiply(x, y, array);
			SecT193Field.Reduce(array, z);
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x00101704 File Offset: 0x000FF904
		public static void MultiplyAddToExt(ulong[] x, ulong[] y, ulong[] zz)
		{
			ulong[] array = Nat256.CreateExt64();
			SecT193Field.ImplMultiply(x, y, array);
			SecT193Field.AddExt(zz, array, zz);
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x00101728 File Offset: 0x000FF928
		public static void Reduce(ulong[] xx, ulong[] z)
		{
			ulong num = xx[0];
			ulong num2 = xx[1];
			ulong num3 = xx[2];
			ulong num4 = xx[3];
			ulong num5 = xx[4];
			ulong num6 = xx[5];
			ulong num7 = xx[6];
			num3 ^= num7 << 63;
			num4 ^= (num7 >> 1 ^ num7 << 14);
			num5 ^= num7 >> 50;
			num2 ^= num6 << 63;
			num3 ^= (num6 >> 1 ^ num6 << 14);
			num4 ^= num6 >> 50;
			num ^= num5 << 63;
			num2 ^= (num5 >> 1 ^ num5 << 14);
			num3 ^= num5 >> 50;
			ulong num8 = num4 >> 1;
			z[0] = (num ^ num8 ^ num8 << 15);
			z[1] = (num2 ^ num8 >> 49);
			z[2] = num3;
			z[3] = (num4 & 1UL);
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x001017D4 File Offset: 0x000FF9D4
		public static void Reduce63(ulong[] z, int zOff)
		{
			ulong num = z[zOff + 3];
			ulong num2 = num >> 1;
			z[zOff] ^= (num2 ^ num2 << 15);
			z[zOff + 1] ^= num2 >> 49;
			z[zOff + 3] = (num & 1UL);
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x00101818 File Offset: 0x000FFA18
		public static void Sqrt(ulong[] x, ulong[] z)
		{
			ulong num = Interleave.Unshuffle(x[0]);
			ulong num2 = Interleave.Unshuffle(x[1]);
			ulong num3 = (num & (ulong)-1) | num2 << 32;
			ulong num4 = num >> 32 | (num2 & 18446744069414584320UL);
			ulong num5 = Interleave.Unshuffle(x[2]);
			ulong num6 = (num5 & (ulong)-1) ^ x[3] << 32;
			ulong num7 = num5 >> 32;
			z[0] = (num3 ^ num4 << 8);
			z[1] = (num6 ^ num7 << 8 ^ num4 >> 56 ^ num4 << 33);
			z[2] = (num7 >> 56 ^ num7 << 33 ^ num4 >> 31);
			z[3] = num7 >> 31;
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x001018A0 File Offset: 0x000FFAA0
		public static void Square(ulong[] x, ulong[] z)
		{
			ulong[] array = Nat256.CreateExt64();
			SecT193Field.ImplSquare(x, array);
			SecT193Field.Reduce(array, z);
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x001018C4 File Offset: 0x000FFAC4
		public static void SquareAddToExt(ulong[] x, ulong[] zz)
		{
			ulong[] array = Nat256.CreateExt64();
			SecT193Field.ImplSquare(x, array);
			SecT193Field.AddExt(zz, array, zz);
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x001018E8 File Offset: 0x000FFAE8
		public static void SquareN(ulong[] x, int n, ulong[] z)
		{
			ulong[] array = Nat256.CreateExt64();
			SecT193Field.ImplSquare(x, array);
			SecT193Field.Reduce(array, z);
			while (--n > 0)
			{
				SecT193Field.ImplSquare(z, array);
				SecT193Field.Reduce(array, z);
			}
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000FC882 File Offset: 0x000FAA82
		public static uint Trace(ulong[] x)
		{
			return (uint)x[0] & 1U;
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x00101924 File Offset: 0x000FFB24
		protected static void ImplCompactExt(ulong[] zz)
		{
			ulong num = zz[0];
			ulong num2 = zz[1];
			ulong num3 = zz[2];
			ulong num4 = zz[3];
			ulong num5 = zz[4];
			ulong num6 = zz[5];
			ulong num7 = zz[6];
			ulong num8 = zz[7];
			zz[0] = (num ^ num2 << 49);
			zz[1] = (num2 >> 15 ^ num3 << 34);
			zz[2] = (num3 >> 30 ^ num4 << 19);
			zz[3] = (num4 >> 45 ^ num5 << 4 ^ num6 << 53);
			zz[4] = (num5 >> 60 ^ num7 << 38 ^ num6 >> 11);
			zz[5] = (num7 >> 26 ^ num8 << 23);
			zz[6] = num8 >> 41;
			zz[7] = 0UL;
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x001019B8 File Offset: 0x000FFBB8
		protected static void ImplExpand(ulong[] x, ulong[] z)
		{
			ulong num = x[0];
			ulong num2 = x[1];
			ulong num3 = x[2];
			ulong num4 = x[3];
			z[0] = (num & 562949953421311UL);
			z[1] = ((num >> 49 ^ num2 << 15) & 562949953421311UL);
			z[2] = ((num2 >> 34 ^ num3 << 30) & 562949953421311UL);
			z[3] = (num3 >> 19 ^ num4 << 45);
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x00101A1C File Offset: 0x000FFC1C
		protected static void ImplMultiply(ulong[] x, ulong[] y, ulong[] zz)
		{
			ulong[] array = new ulong[4];
			ulong[] array2 = new ulong[4];
			SecT193Field.ImplExpand(x, array);
			SecT193Field.ImplExpand(y, array2);
			SecT193Field.ImplMulwAcc(array[0], array2[0], zz, 0);
			SecT193Field.ImplMulwAcc(array[1], array2[1], zz, 1);
			SecT193Field.ImplMulwAcc(array[2], array2[2], zz, 2);
			SecT193Field.ImplMulwAcc(array[3], array2[3], zz, 3);
			for (int i = 5; i > 0; i--)
			{
				zz[i] ^= zz[i - 1];
			}
			SecT193Field.ImplMulwAcc(array[0] ^ array[1], array2[0] ^ array2[1], zz, 1);
			SecT193Field.ImplMulwAcc(array[2] ^ array[3], array2[2] ^ array2[3], zz, 3);
			for (int j = 7; j > 1; j--)
			{
				zz[j] ^= zz[j - 2];
			}
			ulong num = array[0] ^ array[2];
			ulong num2 = array[1] ^ array[3];
			ulong num3 = array2[0] ^ array2[2];
			ulong num4 = array2[1] ^ array2[3];
			SecT193Field.ImplMulwAcc(num ^ num2, num3 ^ num4, zz, 3);
			ulong[] array3 = new ulong[3];
			SecT193Field.ImplMulwAcc(num, num3, array3, 0);
			SecT193Field.ImplMulwAcc(num2, num4, array3, 1);
			ulong num5 = array3[0];
			ulong num6 = array3[1];
			ulong num7 = array3[2];
			zz[2] ^= num5;
			zz[3] ^= (num5 ^ num6);
			zz[4] ^= (num7 ^ num6);
			zz[5] ^= num7;
			SecT193Field.ImplCompactExt(zz);
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x00101B80 File Offset: 0x000FFD80
		protected static void ImplMulwAcc(ulong x, ulong y, ulong[] z, int zOff)
		{
			ulong[] array = new ulong[8];
			array[1] = y;
			array[2] = array[1] << 1;
			array[3] = (array[2] ^ y);
			array[4] = array[2] << 1;
			array[5] = (array[4] ^ y);
			array[6] = array[3] << 1;
			array[7] = (array[6] ^ y);
			uint num = (uint)x;
			ulong num2 = 0UL;
			ulong num3 = array[(int)(num & 7U)] ^ array[(int)(num >> 3 & 7U)] << 3;
			int num4 = 36;
			do
			{
				num = (uint)(x >> num4);
				ulong num5 = array[(int)(num & 7U)] ^ array[(int)(num >> 3 & 7U)] << 3 ^ array[(int)(num >> 6 & 7U)] << 6 ^ array[(int)(num >> 9 & 7U)] << 9 ^ array[(int)(num >> 12 & 7U)] << 12;
				num3 ^= num5 << num4;
				num2 ^= num5 >> -num4;
			}
			while ((num4 -= 15) > 0);
			z[zOff] ^= (num3 & 562949953421311UL);
			z[zOff + 1] ^= (num3 >> 49 ^ num2 << 15);
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x00101C6E File Offset: 0x000FFE6E
		protected static void ImplSquare(ulong[] x, ulong[] zz)
		{
			Interleave.Expand64To128(x[0], zz, 0);
			Interleave.Expand64To128(x[1], zz, 2);
			Interleave.Expand64To128(x[2], zz, 4);
			zz[6] = (x[3] & 1UL);
		}

		// Token: 0x0400195C RID: 6492
		private const ulong M01 = 1UL;

		// Token: 0x0400195D RID: 6493
		private const ulong M49 = 562949953421311UL;
	}
}
