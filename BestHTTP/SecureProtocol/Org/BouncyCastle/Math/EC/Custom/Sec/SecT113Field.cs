using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200036B RID: 875
	internal class SecT113Field
	{
		// Token: 0x060022E2 RID: 8930 RVA: 0x000FC5AC File Offset: 0x000FA7AC
		public static void Add(ulong[] x, ulong[] y, ulong[] z)
		{
			z[0] = (x[0] ^ y[0]);
			z[1] = (x[1] ^ y[1]);
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x000FC5C2 File Offset: 0x000FA7C2
		public static void AddExt(ulong[] xx, ulong[] yy, ulong[] zz)
		{
			zz[0] = (xx[0] ^ yy[0]);
			zz[1] = (xx[1] ^ yy[1]);
			zz[2] = (xx[2] ^ yy[2]);
			zz[3] = (xx[3] ^ yy[3]);
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x000FC5EC File Offset: 0x000FA7EC
		public static void AddOne(ulong[] x, ulong[] z)
		{
			z[0] = (x[0] ^ 1UL);
			z[1] = x[1];
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x000FC5FD File Offset: 0x000FA7FD
		public static ulong[] FromBigInteger(BigInteger x)
		{
			ulong[] array = Nat128.FromBigInteger64(x);
			SecT113Field.Reduce15(array, 0);
			return array;
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x000FC60C File Offset: 0x000FA80C
		public static void Invert(ulong[] x, ulong[] z)
		{
			if (Nat128.IsZero64(x))
			{
				throw new InvalidOperationException();
			}
			ulong[] array = Nat128.Create64();
			ulong[] array2 = Nat128.Create64();
			SecT113Field.Square(x, array);
			SecT113Field.Multiply(array, x, array);
			SecT113Field.Square(array, array);
			SecT113Field.Multiply(array, x, array);
			SecT113Field.SquareN(array, 3, array2);
			SecT113Field.Multiply(array2, array, array2);
			SecT113Field.Square(array2, array2);
			SecT113Field.Multiply(array2, x, array2);
			SecT113Field.SquareN(array2, 7, array);
			SecT113Field.Multiply(array, array2, array);
			SecT113Field.SquareN(array, 14, array2);
			SecT113Field.Multiply(array2, array, array2);
			SecT113Field.SquareN(array2, 28, array);
			SecT113Field.Multiply(array, array2, array);
			SecT113Field.SquareN(array, 56, array2);
			SecT113Field.Multiply(array2, array, array2);
			SecT113Field.Square(array2, z);
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x000FC6BC File Offset: 0x000FA8BC
		public static void Multiply(ulong[] x, ulong[] y, ulong[] z)
		{
			ulong[] array = Nat128.CreateExt64();
			SecT113Field.ImplMultiply(x, y, array);
			SecT113Field.Reduce(array, z);
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x000FC6E0 File Offset: 0x000FA8E0
		public static void MultiplyAddToExt(ulong[] x, ulong[] y, ulong[] zz)
		{
			ulong[] array = Nat128.CreateExt64();
			SecT113Field.ImplMultiply(x, y, array);
			SecT113Field.AddExt(zz, array, zz);
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x000FC704 File Offset: 0x000FA904
		public static void Reduce(ulong[] xx, ulong[] z)
		{
			ulong num = xx[0];
			ulong num2 = xx[1];
			ulong num3 = xx[2];
			ulong num4 = xx[3];
			num2 ^= (num4 << 15 ^ num4 << 24);
			num3 ^= (num4 >> 49 ^ num4 >> 40);
			num ^= (num3 << 15 ^ num3 << 24);
			num2 ^= (num3 >> 49 ^ num3 >> 40);
			ulong num5 = num2 >> 49;
			z[0] = (num ^ num5 ^ num5 << 9);
			z[1] = (num2 & 562949953421311UL);
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x000FC774 File Offset: 0x000FA974
		public static void Reduce15(ulong[] z, int zOff)
		{
			ulong num = z[zOff + 1];
			ulong num2 = num >> 49;
			z[zOff] ^= (num2 ^ num2 << 9);
			z[zOff + 1] = (num & 562949953421311UL);
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x000FC7B0 File Offset: 0x000FA9B0
		public static void Sqrt(ulong[] x, ulong[] z)
		{
			ulong num = Interleave.Unshuffle(x[0]);
			ulong num2 = Interleave.Unshuffle(x[1]);
			ulong num3 = (num & (ulong)-1) | num2 << 32;
			ulong num4 = num >> 32 | (num2 & 18446744069414584320UL);
			z[0] = (num3 ^ num4 << 57 ^ num4 << 5);
			z[1] = (num4 >> 7 ^ num4 >> 59);
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x000FC800 File Offset: 0x000FAA00
		public static void Square(ulong[] x, ulong[] z)
		{
			ulong[] array = Nat128.CreateExt64();
			SecT113Field.ImplSquare(x, array);
			SecT113Field.Reduce(array, z);
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x000FC824 File Offset: 0x000FAA24
		public static void SquareAddToExt(ulong[] x, ulong[] zz)
		{
			ulong[] array = Nat128.CreateExt64();
			SecT113Field.ImplSquare(x, array);
			SecT113Field.AddExt(zz, array, zz);
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x000FC848 File Offset: 0x000FAA48
		public static void SquareN(ulong[] x, int n, ulong[] z)
		{
			ulong[] array = Nat128.CreateExt64();
			SecT113Field.ImplSquare(x, array);
			SecT113Field.Reduce(array, z);
			while (--n > 0)
			{
				SecT113Field.ImplSquare(z, array);
				SecT113Field.Reduce(array, z);
			}
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x000FC882 File Offset: 0x000FAA82
		public static uint Trace(ulong[] x)
		{
			return (uint)x[0] & 1U;
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x000FC88C File Offset: 0x000FAA8C
		protected static void ImplMultiply(ulong[] x, ulong[] y, ulong[] zz)
		{
			ulong num = x[0];
			ulong num2 = x[1];
			num2 = ((num >> 57 ^ num2 << 7) & 144115188075855871UL);
			ulong num3 = num & 144115188075855871UL;
			ulong num4 = y[0];
			ulong num5 = y[1];
			num5 = ((num4 >> 57 ^ num5 << 7) & 144115188075855871UL);
			num4 &= 144115188075855871UL;
			ulong[] array = new ulong[6];
			SecT113Field.ImplMulw(num3, num4, array, 0);
			SecT113Field.ImplMulw(num2, num5, array, 2);
			SecT113Field.ImplMulw(num3 ^ num2, num4 ^ num5, array, 4);
			ulong num6 = array[1] ^ array[2];
			ulong num7 = array[0];
			ulong num8 = array[3];
			ulong num9 = array[4] ^ num7 ^ num6;
			ulong num10 = array[5] ^ num8 ^ num6;
			zz[0] = (num7 ^ num9 << 57);
			zz[1] = (num9 >> 7 ^ num10 << 50);
			zz[2] = (num10 >> 14 ^ num8 << 43);
			zz[3] = num8 >> 21;
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x000FC960 File Offset: 0x000FAB60
		protected static void ImplMulw(ulong x, ulong y, ulong[] z, int zOff)
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
			ulong num3 = array[(int)(num & 7U)];
			int num4 = 48;
			do
			{
				num = (uint)(x >> num4);
				ulong num5 = array[(int)(num & 7U)] ^ array[(int)(num >> 3 & 7U)] << 3 ^ array[(int)(num >> 6 & 7U)] << 6;
				num3 ^= num5 << num4;
				num2 ^= num5 >> -num4;
			}
			while ((num4 -= 9) > 0);
			num2 ^= (x & 72198606942111744UL & y << 7 >> 63) >> 8;
			z[zOff] = (num3 & 144115188075855871UL);
			z[zOff + 1] = (num3 >> 57 ^ num2 << 7);
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x000FCA32 File Offset: 0x000FAC32
		protected static void ImplSquare(ulong[] x, ulong[] zz)
		{
			Interleave.Expand64To128(x[0], zz, 0);
			Interleave.Expand64To128(x[1], zz, 2);
		}

		// Token: 0x0400193C RID: 6460
		private const ulong M49 = 562949953421311UL;

		// Token: 0x0400193D RID: 6461
		private const ulong M57 = 144115188075855871UL;
	}
}
