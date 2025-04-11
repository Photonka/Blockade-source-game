using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000385 RID: 901
	internal class SecT233Field
	{
		// Token: 0x06002496 RID: 9366 RVA: 0x000FC5C2 File Offset: 0x000FA7C2
		public static void Add(ulong[] x, ulong[] y, ulong[] z)
		{
			z[0] = (x[0] ^ y[0]);
			z[1] = (x[1] ^ y[1]);
			z[2] = (x[2] ^ y[2]);
			z[3] = (x[3] ^ y[3]);
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x00102E2C File Offset: 0x0010102C
		public static void AddExt(ulong[] xx, ulong[] yy, ulong[] zz)
		{
			zz[0] = (xx[0] ^ yy[0]);
			zz[1] = (xx[1] ^ yy[1]);
			zz[2] = (xx[2] ^ yy[2]);
			zz[3] = (xx[3] ^ yy[3]);
			zz[4] = (xx[4] ^ yy[4]);
			zz[5] = (xx[5] ^ yy[5]);
			zz[6] = (xx[6] ^ yy[6]);
			zz[7] = (xx[7] ^ yy[7]);
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x001015FF File Offset: 0x000FF7FF
		public static void AddOne(ulong[] x, ulong[] z)
		{
			z[0] = (x[0] ^ 1UL);
			z[1] = x[1];
			z[2] = x[2];
			z[3] = x[3];
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x00102E89 File Offset: 0x00101089
		public static ulong[] FromBigInteger(BigInteger x)
		{
			ulong[] array = Nat256.FromBigInteger64(x);
			SecT233Field.Reduce23(array, 0);
			return array;
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x00102E98 File Offset: 0x00101098
		public static void Invert(ulong[] x, ulong[] z)
		{
			if (Nat256.IsZero64(x))
			{
				throw new InvalidOperationException();
			}
			ulong[] array = Nat256.Create64();
			ulong[] array2 = Nat256.Create64();
			SecT233Field.Square(x, array);
			SecT233Field.Multiply(array, x, array);
			SecT233Field.Square(array, array);
			SecT233Field.Multiply(array, x, array);
			SecT233Field.SquareN(array, 3, array2);
			SecT233Field.Multiply(array2, array, array2);
			SecT233Field.Square(array2, array2);
			SecT233Field.Multiply(array2, x, array2);
			SecT233Field.SquareN(array2, 7, array);
			SecT233Field.Multiply(array, array2, array);
			SecT233Field.SquareN(array, 14, array2);
			SecT233Field.Multiply(array2, array, array2);
			SecT233Field.Square(array2, array2);
			SecT233Field.Multiply(array2, x, array2);
			SecT233Field.SquareN(array2, 29, array);
			SecT233Field.Multiply(array, array2, array);
			SecT233Field.SquareN(array, 58, array2);
			SecT233Field.Multiply(array2, array, array2);
			SecT233Field.SquareN(array2, 116, array);
			SecT233Field.Multiply(array, array2, array);
			SecT233Field.Square(array, z);
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x00102F68 File Offset: 0x00101168
		public static void Multiply(ulong[] x, ulong[] y, ulong[] z)
		{
			ulong[] array = Nat256.CreateExt64();
			SecT233Field.ImplMultiply(x, y, array);
			SecT233Field.Reduce(array, z);
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x00102F8C File Offset: 0x0010118C
		public static void MultiplyAddToExt(ulong[] x, ulong[] y, ulong[] zz)
		{
			ulong[] array = Nat256.CreateExt64();
			SecT233Field.ImplMultiply(x, y, array);
			SecT233Field.AddExt(zz, array, zz);
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x00102FB0 File Offset: 0x001011B0
		public static void Reduce(ulong[] xx, ulong[] z)
		{
			ulong num = xx[0];
			ulong num2 = xx[1];
			ulong num3 = xx[2];
			ulong num4 = xx[3];
			ulong num5 = xx[4];
			ulong num6 = xx[5];
			ulong num7 = xx[6];
			ulong num8 = xx[7];
			num4 ^= num8 << 23;
			num5 ^= (num8 >> 41 ^ num8 << 33);
			num6 ^= num8 >> 31;
			num3 ^= num7 << 23;
			num4 ^= (num7 >> 41 ^ num7 << 33);
			num5 ^= num7 >> 31;
			num2 ^= num6 << 23;
			num3 ^= (num6 >> 41 ^ num6 << 33);
			num4 ^= num6 >> 31;
			num ^= num5 << 23;
			num2 ^= (num5 >> 41 ^ num5 << 33);
			num3 ^= num5 >> 31;
			ulong num9 = num4 >> 41;
			z[0] = (num ^ num9);
			z[1] = (num2 ^ num9 << 10);
			z[2] = num3;
			z[3] = (num4 & 2199023255551UL);
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x00103088 File Offset: 0x00101288
		public static void Reduce23(ulong[] z, int zOff)
		{
			ulong num = z[zOff + 3];
			ulong num2 = num >> 41;
			z[zOff] ^= num2;
			z[zOff + 1] ^= num2 << 10;
			z[zOff + 3] = (num & 2199023255551UL);
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x001030D0 File Offset: 0x001012D0
		public static void Sqrt(ulong[] x, ulong[] z)
		{
			ulong num = Interleave.Unshuffle(x[0]);
			ulong num2 = Interleave.Unshuffle(x[1]);
			ulong num3 = (num & (ulong)-1) | num2 << 32;
			ulong num4 = num >> 32 | (num2 & 18446744069414584320UL);
			ulong num5 = Interleave.Unshuffle(x[2]);
			num2 = Interleave.Unshuffle(x[3]);
			ulong num6 = (num5 & (ulong)-1) | num2 << 32;
			ulong num7 = num5 >> 32 | (num2 & 18446744069414584320UL);
			ulong num8 = num7 >> 27;
			num7 ^= (num4 >> 27 | num7 << 37);
			num4 ^= num4 << 37;
			ulong[] array = Nat256.CreateExt64();
			int[] array2 = new int[]
			{
				32,
				117,
				191
			};
			for (int i = 0; i < array2.Length; i++)
			{
				int num9 = array2[i] >> 6;
				int num10 = array2[i] & 63;
				array[num9] ^= num4 << num10;
				array[num9 + 1] ^= (num7 << num10 | num4 >> -num10);
				array[num9 + 2] ^= (num8 << num10 | num7 >> -num10);
				array[num9 + 3] ^= num8 >> -num10;
			}
			SecT233Field.Reduce(array, z);
			z[0] ^= num3;
			z[1] ^= num6;
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x00103224 File Offset: 0x00101424
		public static void Square(ulong[] x, ulong[] z)
		{
			ulong[] array = Nat256.CreateExt64();
			SecT233Field.ImplSquare(x, array);
			SecT233Field.Reduce(array, z);
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x00103248 File Offset: 0x00101448
		public static void SquareAddToExt(ulong[] x, ulong[] zz)
		{
			ulong[] array = Nat256.CreateExt64();
			SecT233Field.ImplSquare(x, array);
			SecT233Field.AddExt(zz, array, zz);
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x0010326C File Offset: 0x0010146C
		public static void SquareN(ulong[] x, int n, ulong[] z)
		{
			ulong[] array = Nat256.CreateExt64();
			SecT233Field.ImplSquare(x, array);
			SecT233Field.Reduce(array, z);
			while (--n > 0)
			{
				SecT233Field.ImplSquare(z, array);
				SecT233Field.Reduce(array, z);
			}
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x001032A6 File Offset: 0x001014A6
		public static uint Trace(ulong[] x)
		{
			return (uint)(x[0] ^ x[2] >> 31) & 1U;
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x001032B8 File Offset: 0x001014B8
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
			zz[0] = (num ^ num2 << 59);
			zz[1] = (num2 >> 5 ^ num3 << 54);
			zz[2] = (num3 >> 10 ^ num4 << 49);
			zz[3] = (num4 >> 15 ^ num5 << 44);
			zz[4] = (num5 >> 20 ^ num6 << 39);
			zz[5] = (num6 >> 25 ^ num7 << 34);
			zz[6] = (num7 >> 30 ^ num8 << 29);
			zz[7] = num8 >> 35;
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x00103348 File Offset: 0x00101548
		protected static void ImplExpand(ulong[] x, ulong[] z)
		{
			ulong num = x[0];
			ulong num2 = x[1];
			ulong num3 = x[2];
			ulong num4 = x[3];
			z[0] = (num & 576460752303423487UL);
			z[1] = ((num >> 59 ^ num2 << 5) & 576460752303423487UL);
			z[2] = ((num2 >> 54 ^ num3 << 10) & 576460752303423487UL);
			z[3] = (num3 >> 49 ^ num4 << 15);
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x001033AC File Offset: 0x001015AC
		protected static void ImplMultiply(ulong[] x, ulong[] y, ulong[] zz)
		{
			ulong[] array = new ulong[4];
			ulong[] array2 = new ulong[4];
			SecT233Field.ImplExpand(x, array);
			SecT233Field.ImplExpand(y, array2);
			SecT233Field.ImplMulwAcc(array[0], array2[0], zz, 0);
			SecT233Field.ImplMulwAcc(array[1], array2[1], zz, 1);
			SecT233Field.ImplMulwAcc(array[2], array2[2], zz, 2);
			SecT233Field.ImplMulwAcc(array[3], array2[3], zz, 3);
			for (int i = 5; i > 0; i--)
			{
				zz[i] ^= zz[i - 1];
			}
			SecT233Field.ImplMulwAcc(array[0] ^ array[1], array2[0] ^ array2[1], zz, 1);
			SecT233Field.ImplMulwAcc(array[2] ^ array[3], array2[2] ^ array2[3], zz, 3);
			for (int j = 7; j > 1; j--)
			{
				zz[j] ^= zz[j - 2];
			}
			ulong num = array[0] ^ array[2];
			ulong num2 = array[1] ^ array[3];
			ulong num3 = array2[0] ^ array2[2];
			ulong num4 = array2[1] ^ array2[3];
			SecT233Field.ImplMulwAcc(num ^ num2, num3 ^ num4, zz, 3);
			ulong[] array3 = new ulong[3];
			SecT233Field.ImplMulwAcc(num, num3, array3, 0);
			SecT233Field.ImplMulwAcc(num2, num4, array3, 1);
			ulong num5 = array3[0];
			ulong num6 = array3[1];
			ulong num7 = array3[2];
			zz[2] ^= num5;
			zz[3] ^= (num5 ^ num6);
			zz[4] ^= (num7 ^ num6);
			zz[5] ^= num7;
			SecT233Field.ImplCompactExt(zz);
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x00103510 File Offset: 0x00101710
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
			int num4 = 54;
			do
			{
				num = (uint)(x >> num4);
				ulong num5 = array[(int)(num & 7U)] ^ array[(int)(num >> 3 & 7U)] << 3;
				num3 ^= num5 << num4;
				num2 ^= num5 >> -num4;
			}
			while ((num4 -= 6) > 0);
			z[zOff] ^= (num3 & 576460752303423487UL);
			z[zOff + 1] ^= (num3 >> 59 ^ num2 << 5);
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x001035DC File Offset: 0x001017DC
		protected static void ImplSquare(ulong[] x, ulong[] zz)
		{
			Interleave.Expand64To128(x[0], zz, 0);
			Interleave.Expand64To128(x[1], zz, 2);
			Interleave.Expand64To128(x[2], zz, 4);
			ulong num = x[3];
			zz[6] = Interleave.Expand32to64((uint)num);
			zz[7] = (ulong)Interleave.Expand16to32((uint)(num >> 32));
		}

		// Token: 0x04001965 RID: 6501
		private const ulong M41 = 2199023255551UL;

		// Token: 0x04001966 RID: 6502
		private const ulong M59 = 576460752303423487UL;
	}
}
