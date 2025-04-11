using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000344 RID: 836
	internal class SecP160R1Field
	{
		// Token: 0x0600208A RID: 8330 RVA: 0x000F3365 File Offset: 0x000F1565
		public static void Add(uint[] x, uint[] y, uint[] z)
		{
			if (Nat160.Add(x, y, z) != 0U || (z[4] == 4294967295U && Nat160.Gte(z, SecP160R1Field.P)))
			{
				Nat.AddWordTo(5, 2147483649U, z);
			}
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x000F3394 File Offset: 0x000F1594
		public static void AddExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if ((Nat.Add(10, xx, yy, zz) != 0U || (zz[9] == 4294967295U && Nat.Gte(10, zz, SecP160R1Field.PExt))) && Nat.AddTo(SecP160R1Field.PExtInv.Length, SecP160R1Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(10, zz, SecP160R1Field.PExtInv.Length);
			}
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x000F33E7 File Offset: 0x000F15E7
		public static void AddOne(uint[] x, uint[] z)
		{
			if (Nat.Inc(5, x, z) != 0U || (z[4] == 4294967295U && Nat160.Gte(z, SecP160R1Field.P)))
			{
				Nat.AddWordTo(5, 2147483649U, z);
			}
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x000F3414 File Offset: 0x000F1614
		public static uint[] FromBigInteger(BigInteger x)
		{
			uint[] array = Nat160.FromBigInteger(x);
			if (array[4] == 4294967295U && Nat160.Gte(array, SecP160R1Field.P))
			{
				Nat160.SubFrom(SecP160R1Field.P, array);
			}
			return array;
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x000F3448 File Offset: 0x000F1648
		public static void Half(uint[] x, uint[] z)
		{
			if ((x[0] & 1U) == 0U)
			{
				Nat.ShiftDownBit(5, x, 0U, z);
				return;
			}
			uint c = Nat160.Add(x, SecP160R1Field.P, z);
			Nat.ShiftDownBit(5, z, c);
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x000F3480 File Offset: 0x000F1680
		public static void Multiply(uint[] x, uint[] y, uint[] z)
		{
			uint[] array = Nat160.CreateExt();
			Nat160.Mul(x, y, array);
			SecP160R1Field.Reduce(array, z);
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x000F34A4 File Offset: 0x000F16A4
		public static void MultiplyAddToExt(uint[] x, uint[] y, uint[] zz)
		{
			if ((Nat160.MulAddTo(x, y, zz) != 0U || (zz[9] == 4294967295U && Nat.Gte(10, zz, SecP160R1Field.PExt))) && Nat.AddTo(SecP160R1Field.PExtInv.Length, SecP160R1Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(10, zz, SecP160R1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x000F34F5 File Offset: 0x000F16F5
		public static void Negate(uint[] x, uint[] z)
		{
			if (Nat160.IsZero(x))
			{
				Nat160.Zero(z);
				return;
			}
			Nat160.Sub(SecP160R1Field.P, x, z);
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x000F3514 File Offset: 0x000F1714
		public static void Reduce(uint[] xx, uint[] z)
		{
			ulong num = (ulong)xx[5];
			ulong num2 = (ulong)xx[6];
			ulong num3 = (ulong)xx[7];
			ulong num4 = (ulong)xx[8];
			ulong num5 = (ulong)xx[9];
			ulong num6 = 0UL;
			num6 += (ulong)xx[0] + num + (num << 31);
			z[0] = (uint)num6;
			num6 >>= 32;
			num6 += (ulong)xx[1] + num2 + (num2 << 31);
			z[1] = (uint)num6;
			num6 >>= 32;
			num6 += (ulong)xx[2] + num3 + (num3 << 31);
			z[2] = (uint)num6;
			num6 >>= 32;
			num6 += (ulong)xx[3] + num4 + (num4 << 31);
			z[3] = (uint)num6;
			num6 >>= 32;
			num6 += (ulong)xx[4] + num5 + (num5 << 31);
			z[4] = (uint)num6;
			num6 >>= 32;
			SecP160R1Field.Reduce32((uint)num6, z);
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x000F35DC File Offset: 0x000F17DC
		public static void Reduce32(uint x, uint[] z)
		{
			if ((x != 0U && Nat160.MulWordsAdd(2147483649U, x, z, 0) != 0U) || (z[4] == 4294967295U && Nat160.Gte(z, SecP160R1Field.P)))
			{
				Nat.AddWordTo(5, 2147483649U, z);
			}
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x000F3610 File Offset: 0x000F1810
		public static void Square(uint[] x, uint[] z)
		{
			uint[] array = Nat160.CreateExt();
			Nat160.Square(x, array);
			SecP160R1Field.Reduce(array, z);
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x000F3634 File Offset: 0x000F1834
		public static void SquareN(uint[] x, int n, uint[] z)
		{
			uint[] array = Nat160.CreateExt();
			Nat160.Square(x, array);
			SecP160R1Field.Reduce(array, z);
			while (--n > 0)
			{
				Nat160.Square(z, array);
				SecP160R1Field.Reduce(array, z);
			}
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x000F366E File Offset: 0x000F186E
		public static void Subtract(uint[] x, uint[] y, uint[] z)
		{
			if (Nat160.Sub(x, y, z) != 0)
			{
				Nat.SubWordFrom(5, 2147483649U, z);
			}
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x000F3687 File Offset: 0x000F1887
		public static void SubtractExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if (Nat.Sub(10, xx, yy, zz) != 0 && Nat.SubFrom(SecP160R1Field.PExtInv.Length, SecP160R1Field.PExtInv, zz) != 0)
			{
				Nat.DecAt(10, zz, SecP160R1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x000F36B9 File Offset: 0x000F18B9
		public static void Twice(uint[] x, uint[] z)
		{
			if (Nat.ShiftUpBit(5, x, 0U, z) != 0U || (z[4] == 4294967295U && Nat160.Gte(z, SecP160R1Field.P)))
			{
				Nat.AddWordTo(5, 2147483649U, z);
			}
		}

		// Token: 0x040018D0 RID: 6352
		internal static readonly uint[] P = new uint[]
		{
			2147483647U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x040018D1 RID: 6353
		internal static readonly uint[] PExt = new uint[]
		{
			1U,
			1073741825U,
			0U,
			0U,
			0U,
			4294967294U,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x040018D2 RID: 6354
		private static readonly uint[] PExtInv = new uint[]
		{
			uint.MaxValue,
			3221225470U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			1U,
			1U
		};

		// Token: 0x040018D3 RID: 6355
		private const uint P4 = 4294967295U;

		// Token: 0x040018D4 RID: 6356
		private const uint PExt9 = 4294967295U;

		// Token: 0x040018D5 RID: 6357
		private const uint PInv = 2147483649U;
	}
}
