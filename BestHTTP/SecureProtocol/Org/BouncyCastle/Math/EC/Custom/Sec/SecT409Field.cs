﻿using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000395 RID: 917
	internal class SecT409Field
	{
		// Token: 0x060025B6 RID: 9654 RVA: 0x001073EC File Offset: 0x001055EC
		public static void Add(ulong[] x, ulong[] y, ulong[] z)
		{
			z[0] = (x[0] ^ y[0]);
			z[1] = (x[1] ^ y[1]);
			z[2] = (x[2] ^ y[2]);
			z[3] = (x[3] ^ y[3]);
			z[4] = (x[4] ^ y[4]);
			z[5] = (x[5] ^ y[5]);
			z[6] = (x[6] ^ y[6]);
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x00107440 File Offset: 0x00105640
		public static void AddExt(ulong[] xx, ulong[] yy, ulong[] zz)
		{
			for (int i = 0; i < 13; i++)
			{
				zz[i] = (xx[i] ^ yy[i]);
			}
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x00107464 File Offset: 0x00105664
		public static void AddOne(ulong[] x, ulong[] z)
		{
			z[0] = (x[0] ^ 1UL);
			z[1] = x[1];
			z[2] = x[2];
			z[3] = x[3];
			z[4] = x[4];
			z[5] = x[5];
			z[6] = x[6];
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x00107493 File Offset: 0x00105693
		public static ulong[] FromBigInteger(BigInteger x)
		{
			ulong[] array = Nat448.FromBigInteger64(x);
			SecT409Field.Reduce39(array, 0);
			return array;
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x001074A4 File Offset: 0x001056A4
		public static void Invert(ulong[] x, ulong[] z)
		{
			if (Nat448.IsZero64(x))
			{
				throw new InvalidOperationException();
			}
			ulong[] array = Nat448.Create64();
			ulong[] array2 = Nat448.Create64();
			ulong[] array3 = Nat448.Create64();
			SecT409Field.Square(x, array);
			SecT409Field.SquareN(array, 1, array2);
			SecT409Field.Multiply(array, array2, array);
			SecT409Field.SquareN(array2, 1, array2);
			SecT409Field.Multiply(array, array2, array);
			SecT409Field.SquareN(array, 3, array2);
			SecT409Field.Multiply(array, array2, array);
			SecT409Field.SquareN(array, 6, array2);
			SecT409Field.Multiply(array, array2, array);
			SecT409Field.SquareN(array, 12, array2);
			SecT409Field.Multiply(array, array2, array3);
			SecT409Field.SquareN(array3, 24, array);
			SecT409Field.SquareN(array, 24, array2);
			SecT409Field.Multiply(array, array2, array);
			SecT409Field.SquareN(array, 48, array2);
			SecT409Field.Multiply(array, array2, array);
			SecT409Field.SquareN(array, 96, array2);
			SecT409Field.Multiply(array, array2, array);
			SecT409Field.SquareN(array, 192, array2);
			SecT409Field.Multiply(array, array2, array);
			SecT409Field.Multiply(array, array3, z);
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x00107584 File Offset: 0x00105784
		public static void Multiply(ulong[] x, ulong[] y, ulong[] z)
		{
			ulong[] array = Nat448.CreateExt64();
			SecT409Field.ImplMultiply(x, y, array);
			SecT409Field.Reduce(array, z);
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x001075A8 File Offset: 0x001057A8
		public static void MultiplyAddToExt(ulong[] x, ulong[] y, ulong[] zz)
		{
			ulong[] array = Nat448.CreateExt64();
			SecT409Field.ImplMultiply(x, y, array);
			SecT409Field.AddExt(zz, array, zz);
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x001075CC File Offset: 0x001057CC
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
			ulong num9 = xx[12];
			num6 ^= num9 << 39;
			num7 ^= (num9 >> 25 ^ num9 << 62);
			ulong num10 = num8 ^ num9 >> 2;
			num9 = xx[11];
			num5 ^= num9 << 39;
			num6 ^= (num9 >> 25 ^ num9 << 62);
			num7 ^= num9 >> 2;
			num9 = xx[10];
			num4 ^= num9 << 39;
			num5 ^= (num9 >> 25 ^ num9 << 62);
			num6 ^= num9 >> 2;
			num9 = xx[9];
			num3 ^= num9 << 39;
			num4 ^= (num9 >> 25 ^ num9 << 62);
			num5 ^= num9 >> 2;
			num9 = xx[8];
			num2 ^= num9 << 39;
			num3 ^= (num9 >> 25 ^ num9 << 62);
			num4 ^= num9 >> 2;
			num9 = num10;
			num ^= num9 << 39;
			num2 ^= (num9 >> 25 ^ num9 << 62);
			num3 ^= num9 >> 2;
			ulong num11 = num7 >> 25;
			z[0] = (num ^ num11);
			z[1] = (num2 ^ num11 << 23);
			z[2] = num3;
			z[3] = num4;
			z[4] = num5;
			z[5] = num6;
			z[6] = (num7 & 33554431UL);
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x0010770C File Offset: 0x0010590C
		public static void Reduce39(ulong[] z, int zOff)
		{
			ulong num = z[zOff + 6];
			ulong num2 = num >> 25;
			z[zOff] ^= num2;
			z[zOff + 1] ^= num2 << 23;
			z[zOff + 6] = (num & 33554431UL);
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x00107750 File Offset: 0x00105950
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
			ulong num8 = Interleave.Unshuffle(x[4]);
			num2 = Interleave.Unshuffle(x[5]);
			ulong num9 = (num8 & (ulong)-1) | num2 << 32;
			ulong num10 = num8 >> 32 | (num2 & 18446744069414584320UL);
			ulong num11 = Interleave.Unshuffle(x[6]);
			ulong num12 = num11 & (ulong)-1;
			ulong num13 = num11 >> 32;
			z[0] = (num3 ^ num4 << 44);
			z[1] = (num6 ^ num7 << 44 ^ num4 >> 20);
			z[2] = (num9 ^ num10 << 44 ^ num7 >> 20);
			z[3] = (num12 ^ num13 << 44 ^ num10 >> 20 ^ num4 << 13);
			z[4] = (num13 >> 20 ^ num7 << 13 ^ num4 >> 51);
			z[5] = (num10 << 13 ^ num7 >> 51);
			z[6] = (num13 << 13 ^ num10 >> 51);
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x00107864 File Offset: 0x00105A64
		public static void Square(ulong[] x, ulong[] z)
		{
			ulong[] array = Nat.Create64(13);
			SecT409Field.ImplSquare(x, array);
			SecT409Field.Reduce(array, z);
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x00107888 File Offset: 0x00105A88
		public static void SquareAddToExt(ulong[] x, ulong[] zz)
		{
			ulong[] array = Nat.Create64(13);
			SecT409Field.ImplSquare(x, array);
			SecT409Field.AddExt(zz, array, zz);
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x001078AC File Offset: 0x00105AAC
		public static void SquareN(ulong[] x, int n, ulong[] z)
		{
			ulong[] array = Nat.Create64(13);
			SecT409Field.ImplSquare(x, array);
			SecT409Field.Reduce(array, z);
			while (--n > 0)
			{
				SecT409Field.ImplSquare(z, array);
				SecT409Field.Reduce(array, z);
			}
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x000FC882 File Offset: 0x000FAA82
		public static uint Trace(ulong[] x)
		{
			return (uint)x[0] & 1U;
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x001078E8 File Offset: 0x00105AE8
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
			ulong num9 = zz[8];
			ulong num10 = zz[9];
			ulong num11 = zz[10];
			ulong num12 = zz[11];
			ulong num13 = zz[12];
			ulong num14 = zz[13];
			zz[0] = (num ^ num2 << 59);
			zz[1] = (num2 >> 5 ^ num3 << 54);
			zz[2] = (num3 >> 10 ^ num4 << 49);
			zz[3] = (num4 >> 15 ^ num5 << 44);
			zz[4] = (num5 >> 20 ^ num6 << 39);
			zz[5] = (num6 >> 25 ^ num7 << 34);
			zz[6] = (num7 >> 30 ^ num8 << 29);
			zz[7] = (num8 >> 35 ^ num9 << 24);
			zz[8] = (num9 >> 40 ^ num10 << 19);
			zz[9] = (num10 >> 45 ^ num11 << 14);
			zz[10] = (num11 >> 50 ^ num12 << 9);
			zz[11] = (num12 >> 55 ^ num13 << 4 ^ num14 << 63);
			zz[12] = (num13 >> 60 ^ num14 >> 1);
			zz[13] = 0UL;
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x001079F8 File Offset: 0x00105BF8
		protected static void ImplExpand(ulong[] x, ulong[] z)
		{
			ulong num = x[0];
			ulong num2 = x[1];
			ulong num3 = x[2];
			ulong num4 = x[3];
			ulong num5 = x[4];
			ulong num6 = x[5];
			ulong num7 = x[6];
			z[0] = (num & 576460752303423487UL);
			z[1] = ((num >> 59 ^ num2 << 5) & 576460752303423487UL);
			z[2] = ((num2 >> 54 ^ num3 << 10) & 576460752303423487UL);
			z[3] = ((num3 >> 49 ^ num4 << 15) & 576460752303423487UL);
			z[4] = ((num4 >> 44 ^ num5 << 20) & 576460752303423487UL);
			z[5] = ((num5 >> 39 ^ num6 << 25) & 576460752303423487UL);
			z[6] = (num6 >> 34 ^ num7 << 30);
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x00107AB0 File Offset: 0x00105CB0
		protected static void ImplMultiply(ulong[] x, ulong[] y, ulong[] zz)
		{
			ulong[] array = new ulong[7];
			ulong[] array2 = new ulong[7];
			SecT409Field.ImplExpand(x, array);
			SecT409Field.ImplExpand(y, array2);
			for (int i = 0; i < 7; i++)
			{
				SecT409Field.ImplMulwAcc(array, array2[i], zz, i);
			}
			SecT409Field.ImplCompactExt(zz);
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x00107AF8 File Offset: 0x00105CF8
		protected static void ImplMulwAcc(ulong[] xs, ulong y, ulong[] z, int zOff)
		{
			ulong[] array = new ulong[8];
			array[1] = y;
			array[2] = array[1] << 1;
			array[3] = (array[2] ^ y);
			array[4] = array[2] << 1;
			array[5] = (array[4] ^ y);
			array[6] = array[3] << 1;
			array[7] = (array[6] ^ y);
			for (int i = 0; i < 7; i++)
			{
				ulong num = xs[i];
				uint num2 = (uint)num;
				ulong num3 = 0UL;
				ulong num4 = array[(int)(num2 & 7U)] ^ array[(int)(num2 >> 3 & 7U)] << 3;
				int num5 = 54;
				do
				{
					num2 = (uint)(num >> num5);
					ulong num6 = array[(int)(num2 & 7U)] ^ array[(int)(num2 >> 3 & 7U)] << 3;
					num4 ^= num6 << num5;
					num3 ^= num6 >> -num5;
				}
				while ((num5 -= 6) > 0);
				z[zOff + i] ^= (num4 & 576460752303423487UL);
				z[zOff + i + 1] ^= (num4 >> 59 ^ num3 << 5);
			}
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x00107BE4 File Offset: 0x00105DE4
		protected static void ImplSquare(ulong[] x, ulong[] zz)
		{
			for (int i = 0; i < 6; i++)
			{
				Interleave.Expand64To128(x[i], zz, i << 1);
			}
			zz[12] = Interleave.Expand32to64((uint)x[6]);
		}

		// Token: 0x0400197E RID: 6526
		private const ulong M25 = 33554431UL;

		// Token: 0x0400197F RID: 6527
		private const ulong M59 = 576460752303423487UL;
	}
}
