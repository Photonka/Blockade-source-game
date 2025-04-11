using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000368 RID: 872
	internal class SecP521R1Field
	{
		// Token: 0x060022B2 RID: 8882 RVA: 0x000FB9B8 File Offset: 0x000F9BB8
		public static void Add(uint[] x, uint[] y, uint[] z)
		{
			uint num = Nat.Add(16, x, y, z) + x[16] + y[16];
			if (num > 511U || (num == 511U && Nat.Eq(16, z, SecP521R1Field.P)))
			{
				num += Nat.Inc(16, z);
				num &= 511U;
			}
			z[16] = num;
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x000FBA14 File Offset: 0x000F9C14
		public static void AddOne(uint[] x, uint[] z)
		{
			uint num = Nat.Inc(16, x, z) + x[16];
			if (num > 511U || (num == 511U && Nat.Eq(16, z, SecP521R1Field.P)))
			{
				num += Nat.Inc(16, z);
				num &= 511U;
			}
			z[16] = num;
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x000FBA68 File Offset: 0x000F9C68
		public static uint[] FromBigInteger(BigInteger x)
		{
			uint[] array = Nat.FromBigInteger(521, x);
			if (Nat.Eq(17, array, SecP521R1Field.P))
			{
				Nat.Zero(17, array);
			}
			return array;
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x000FBA9C File Offset: 0x000F9C9C
		public static void Half(uint[] x, uint[] z)
		{
			uint num = x[16];
			uint num2 = Nat.ShiftDownBit(16, x, num, z);
			z[16] = (num >> 1 | num2 >> 23);
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x000FBAC8 File Offset: 0x000F9CC8
		public static void Multiply(uint[] x, uint[] y, uint[] z)
		{
			uint[] array = Nat.Create(33);
			SecP521R1Field.ImplMultiply(x, y, array);
			SecP521R1Field.Reduce(array, z);
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x000FBAEC File Offset: 0x000F9CEC
		public static void Negate(uint[] x, uint[] z)
		{
			if (Nat.IsZero(17, x))
			{
				Nat.Zero(17, z);
				return;
			}
			Nat.Sub(17, SecP521R1Field.P, x, z);
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x000FBB10 File Offset: 0x000F9D10
		public static void Reduce(uint[] xx, uint[] z)
		{
			uint num = xx[32];
			uint num2 = Nat.ShiftDownBits(16, xx, 16, 9, num, z, 0) >> 23;
			num2 += num >> 9;
			num2 += Nat.AddTo(16, xx, z);
			if (num2 > 511U || (num2 == 511U && Nat.Eq(16, z, SecP521R1Field.P)))
			{
				num2 += Nat.Inc(16, z);
				num2 &= 511U;
			}
			z[16] = num2;
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x000FBB80 File Offset: 0x000F9D80
		public static void Reduce23(uint[] z)
		{
			uint num = z[16];
			uint num2 = Nat.AddWordTo(16, num >> 9, z) + (num & 511U);
			if (num2 > 511U || (num2 == 511U && Nat.Eq(16, z, SecP521R1Field.P)))
			{
				num2 += Nat.Inc(16, z);
				num2 &= 511U;
			}
			z[16] = num2;
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x000FBBE0 File Offset: 0x000F9DE0
		public static void Square(uint[] x, uint[] z)
		{
			uint[] array = Nat.Create(33);
			SecP521R1Field.ImplSquare(x, array);
			SecP521R1Field.Reduce(array, z);
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x000FBC04 File Offset: 0x000F9E04
		public static void SquareN(uint[] x, int n, uint[] z)
		{
			uint[] array = Nat.Create(33);
			SecP521R1Field.ImplSquare(x, array);
			SecP521R1Field.Reduce(array, z);
			while (--n > 0)
			{
				SecP521R1Field.ImplSquare(z, array);
				SecP521R1Field.Reduce(array, z);
			}
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x000FBC40 File Offset: 0x000F9E40
		public static void Subtract(uint[] x, uint[] y, uint[] z)
		{
			int num = Nat.Sub(16, x, y, z) + (int)(x[16] - y[16]);
			if (num < 0)
			{
				num += Nat.Dec(16, z);
				num &= 511;
			}
			z[16] = (uint)num;
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x000FBC80 File Offset: 0x000F9E80
		public static void Twice(uint[] x, uint[] z)
		{
			uint num = x[16];
			uint num2 = Nat.ShiftUpBit(16, x, num << 23, z) | num << 1;
			z[16] = (num2 & 511U);
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x000FBCB0 File Offset: 0x000F9EB0
		protected static void ImplMultiply(uint[] x, uint[] y, uint[] zz)
		{
			Nat512.Mul(x, y, zz);
			uint num = x[16];
			uint num2 = y[16];
			zz[32] = Nat.Mul31BothAdd(16, num, y, num2, x, zz, 16) + num * num2;
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x000FBCE8 File Offset: 0x000F9EE8
		protected static void ImplSquare(uint[] x, uint[] zz)
		{
			Nat512.Square(x, zz);
			uint num = x[16];
			zz[32] = Nat.MulWordAddTo(16, num << 1, x, 0, zz, 16) + num * num;
		}

		// Token: 0x04001938 RID: 6456
		internal static readonly uint[] P = new uint[]
		{
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			511U
		};

		// Token: 0x04001939 RID: 6457
		private const int P16 = 511;
	}
}
