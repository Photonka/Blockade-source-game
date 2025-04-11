using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Djb
{
	// Token: 0x020003A6 RID: 934
	internal class Curve25519Field
	{
		// Token: 0x060026D1 RID: 9937 RVA: 0x0010B6BB File Offset: 0x001098BB
		public static void Add(uint[] x, uint[] y, uint[] z)
		{
			Nat256.Add(x, y, z);
			if (Nat256.Gte(z, Curve25519Field.P))
			{
				Curve25519Field.SubPFrom(z);
			}
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x0010B6DA File Offset: 0x001098DA
		public static void AddExt(uint[] xx, uint[] yy, uint[] zz)
		{
			Nat.Add(16, xx, yy, zz);
			if (Nat.Gte(16, zz, Curve25519Field.PExt))
			{
				Curve25519Field.SubPExtFrom(zz);
			}
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x0010B6FD File Offset: 0x001098FD
		public static void AddOne(uint[] x, uint[] z)
		{
			Nat.Inc(8, x, z);
			if (Nat256.Gte(z, Curve25519Field.P))
			{
				Curve25519Field.SubPFrom(z);
			}
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x0010B71C File Offset: 0x0010991C
		public static uint[] FromBigInteger(BigInteger x)
		{
			uint[] array = Nat256.FromBigInteger(x);
			while (Nat256.Gte(array, Curve25519Field.P))
			{
				Nat256.SubFrom(Curve25519Field.P, array);
			}
			return array;
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x0010B74C File Offset: 0x0010994C
		public static void Half(uint[] x, uint[] z)
		{
			if ((x[0] & 1U) == 0U)
			{
				Nat.ShiftDownBit(8, x, 0U, z);
				return;
			}
			Nat256.Add(x, Curve25519Field.P, z);
			Nat.ShiftDownBit(8, z, 0U);
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x0010B778 File Offset: 0x00109978
		public static void Multiply(uint[] x, uint[] y, uint[] z)
		{
			uint[] array = Nat256.CreateExt();
			Nat256.Mul(x, y, array);
			Curve25519Field.Reduce(array, z);
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x0010B79A File Offset: 0x0010999A
		public static void MultiplyAddToExt(uint[] x, uint[] y, uint[] zz)
		{
			Nat256.MulAddTo(x, y, zz);
			if (Nat.Gte(16, zz, Curve25519Field.PExt))
			{
				Curve25519Field.SubPExtFrom(zz);
			}
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x0010B7BB File Offset: 0x001099BB
		public static void Negate(uint[] x, uint[] z)
		{
			if (Nat256.IsZero(x))
			{
				Nat256.Zero(z);
				return;
			}
			Nat256.Sub(Curve25519Field.P, x, z);
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x0010B7DC File Offset: 0x001099DC
		public static void Reduce(uint[] xx, uint[] z)
		{
			uint num = xx[7];
			Nat.ShiftUpBit(8, xx, 8, num, z, 0);
			uint num2 = Nat256.MulByWordAddTo(19U, xx, z) << 1;
			uint num3 = z[7];
			num2 += (num3 >> 31) - (num >> 31);
			num3 &= 2147483647U;
			num3 += Nat.AddWordTo(7, num2 * 19U, z);
			z[7] = num3;
			if (num3 >= 2147483647U && Nat256.Gte(z, Curve25519Field.P))
			{
				Curve25519Field.SubPFrom(z);
			}
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x0010B84C File Offset: 0x00109A4C
		public static void Reduce27(uint x, uint[] z)
		{
			uint num = z[7];
			uint num2 = x << 1 | num >> 31;
			num &= 2147483647U;
			num += Nat.AddWordTo(7, num2 * 19U, z);
			z[7] = num;
			if (num >= 2147483647U && Nat256.Gte(z, Curve25519Field.P))
			{
				Curve25519Field.SubPFrom(z);
			}
		}

		// Token: 0x060026DB RID: 9947 RVA: 0x0010B89C File Offset: 0x00109A9C
		public static void Square(uint[] x, uint[] z)
		{
			uint[] array = Nat256.CreateExt();
			Nat256.Square(x, array);
			Curve25519Field.Reduce(array, z);
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x0010B8C0 File Offset: 0x00109AC0
		public static void SquareN(uint[] x, int n, uint[] z)
		{
			uint[] array = Nat256.CreateExt();
			Nat256.Square(x, array);
			Curve25519Field.Reduce(array, z);
			while (--n > 0)
			{
				Nat256.Square(z, array);
				Curve25519Field.Reduce(array, z);
			}
		}

		// Token: 0x060026DD RID: 9949 RVA: 0x0010B8FA File Offset: 0x00109AFA
		public static void Subtract(uint[] x, uint[] y, uint[] z)
		{
			if (Nat256.Sub(x, y, z) != 0)
			{
				Curve25519Field.AddPTo(z);
			}
		}

		// Token: 0x060026DE RID: 9950 RVA: 0x0010B90D File Offset: 0x00109B0D
		public static void SubtractExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if (Nat.Sub(16, xx, yy, zz) != 0)
			{
				Curve25519Field.AddPExtTo(zz);
			}
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x0010B922 File Offset: 0x00109B22
		public static void Twice(uint[] x, uint[] z)
		{
			Nat.ShiftUpBit(8, x, 0U, z);
			if (Nat256.Gte(z, Curve25519Field.P))
			{
				Curve25519Field.SubPFrom(z);
			}
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x0010B944 File Offset: 0x00109B44
		private static uint AddPTo(uint[] z)
		{
			long num = (long)((ulong)z[0] - 19UL);
			z[0] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				num = (long)Nat.DecAt(7, z, 1);
			}
			num += (long)((ulong)z[7] + (ulong)int.MinValue);
			z[7] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x0010B98C File Offset: 0x00109B8C
		private static uint AddPExtTo(uint[] zz)
		{
			long num = (long)((ulong)zz[0] + (ulong)Curve25519Field.PExt[0]);
			zz[0] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				num = (long)((ulong)Nat.IncAt(8, zz, 1));
			}
			num += (long)((ulong)zz[8] - 19UL);
			zz[8] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				num = (long)Nat.DecAt(15, zz, 9);
			}
			num += (long)((ulong)zz[15] + (ulong)(Curve25519Field.PExt[15] + 1U));
			zz[15] = (uint)num;
			num >>= 32;
			return (uint)num;
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x0010BA04 File Offset: 0x00109C04
		private static int SubPFrom(uint[] z)
		{
			long num = (long)((ulong)z[0] + 19UL);
			z[0] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				num = (long)((ulong)Nat.IncAt(7, z, 1));
			}
			num += (long)((ulong)z[7] - (ulong)int.MinValue);
			z[7] = (uint)num;
			num >>= 32;
			return (int)num;
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x0010BA4C File Offset: 0x00109C4C
		private static int SubPExtFrom(uint[] zz)
		{
			long num = (long)((ulong)zz[0] - (ulong)Curve25519Field.PExt[0]);
			zz[0] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				num = (long)Nat.DecAt(8, zz, 1);
			}
			num += (long)((ulong)zz[8] + 19UL);
			zz[8] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				num = (long)((ulong)Nat.IncAt(15, zz, 9));
			}
			num += (long)((ulong)zz[15] - (ulong)(Curve25519Field.PExt[15] + 1U));
			zz[15] = (uint)num;
			num >>= 32;
			return (int)num;
		}

		// Token: 0x040019A1 RID: 6561
		internal static readonly uint[] P = new uint[]
		{
			4294967277U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			2147483647U
		};

		// Token: 0x040019A2 RID: 6562
		private const uint P7 = 2147483647U;

		// Token: 0x040019A3 RID: 6563
		private static readonly uint[] PExt = new uint[]
		{
			361U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			0U,
			4294967277U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			1073741823U
		};

		// Token: 0x040019A4 RID: 6564
		private const uint PInv = 19U;
	}
}
