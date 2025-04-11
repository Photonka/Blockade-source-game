using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000348 RID: 840
	internal class SecP160R2Field
	{
		// Token: 0x060020C6 RID: 8390 RVA: 0x000F4155 File Offset: 0x000F2355
		public static void Add(uint[] x, uint[] y, uint[] z)
		{
			if (Nat160.Add(x, y, z) != 0U || (z[4] == 4294967295U && Nat160.Gte(z, SecP160R2Field.P)))
			{
				Nat.Add33To(5, 21389U, z);
			}
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x000F4184 File Offset: 0x000F2384
		public static void AddExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if ((Nat.Add(10, xx, yy, zz) != 0U || (zz[9] == 4294967295U && Nat.Gte(10, zz, SecP160R2Field.PExt))) && Nat.AddTo(SecP160R2Field.PExtInv.Length, SecP160R2Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(10, zz, SecP160R2Field.PExtInv.Length);
			}
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x000F41D7 File Offset: 0x000F23D7
		public static void AddOne(uint[] x, uint[] z)
		{
			if (Nat.Inc(5, x, z) != 0U || (z[4] == 4294967295U && Nat160.Gte(z, SecP160R2Field.P)))
			{
				Nat.Add33To(5, 21389U, z);
			}
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x000F4204 File Offset: 0x000F2404
		public static uint[] FromBigInteger(BigInteger x)
		{
			uint[] array = Nat160.FromBigInteger(x);
			if (array[4] == 4294967295U && Nat160.Gte(array, SecP160R2Field.P))
			{
				Nat160.SubFrom(SecP160R2Field.P, array);
			}
			return array;
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x000F4238 File Offset: 0x000F2438
		public static void Half(uint[] x, uint[] z)
		{
			if ((x[0] & 1U) == 0U)
			{
				Nat.ShiftDownBit(5, x, 0U, z);
				return;
			}
			uint c = Nat160.Add(x, SecP160R2Field.P, z);
			Nat.ShiftDownBit(5, z, c);
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x000F4270 File Offset: 0x000F2470
		public static void Multiply(uint[] x, uint[] y, uint[] z)
		{
			uint[] array = Nat160.CreateExt();
			Nat160.Mul(x, y, array);
			SecP160R2Field.Reduce(array, z);
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x000F4294 File Offset: 0x000F2494
		public static void MultiplyAddToExt(uint[] x, uint[] y, uint[] zz)
		{
			if ((Nat160.MulAddTo(x, y, zz) != 0U || (zz[9] == 4294967295U && Nat.Gte(10, zz, SecP160R2Field.PExt))) && Nat.AddTo(SecP160R2Field.PExtInv.Length, SecP160R2Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(10, zz, SecP160R2Field.PExtInv.Length);
			}
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x000F42E5 File Offset: 0x000F24E5
		public static void Negate(uint[] x, uint[] z)
		{
			if (Nat160.IsZero(x))
			{
				Nat160.Zero(z);
				return;
			}
			Nat160.Sub(SecP160R2Field.P, x, z);
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x000F4304 File Offset: 0x000F2504
		public static void Reduce(uint[] xx, uint[] z)
		{
			ulong y = Nat160.Mul33Add(21389U, xx, 5, xx, 0, z, 0);
			if (Nat160.Mul33DWordAdd(21389U, y, z, 0) != 0U || (z[4] == 4294967295U && Nat160.Gte(z, SecP160R2Field.P)))
			{
				Nat.Add33To(5, 21389U, z);
			}
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x000F4351 File Offset: 0x000F2551
		public static void Reduce32(uint x, uint[] z)
		{
			if ((x != 0U && Nat160.Mul33WordAdd(21389U, x, z, 0) != 0U) || (z[4] == 4294967295U && Nat160.Gte(z, SecP160R2Field.P)))
			{
				Nat.Add33To(5, 21389U, z);
			}
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x000F4388 File Offset: 0x000F2588
		public static void Square(uint[] x, uint[] z)
		{
			uint[] array = Nat160.CreateExt();
			Nat160.Square(x, array);
			SecP160R2Field.Reduce(array, z);
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x000F43AC File Offset: 0x000F25AC
		public static void SquareN(uint[] x, int n, uint[] z)
		{
			uint[] array = Nat160.CreateExt();
			Nat160.Square(x, array);
			SecP160R2Field.Reduce(array, z);
			while (--n > 0)
			{
				Nat160.Square(z, array);
				SecP160R2Field.Reduce(array, z);
			}
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x000F43E6 File Offset: 0x000F25E6
		public static void Subtract(uint[] x, uint[] y, uint[] z)
		{
			if (Nat160.Sub(x, y, z) != 0)
			{
				Nat.Sub33From(5, 21389U, z);
			}
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x000F43FF File Offset: 0x000F25FF
		public static void SubtractExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if (Nat.Sub(10, xx, yy, zz) != 0 && Nat.SubFrom(SecP160R2Field.PExtInv.Length, SecP160R2Field.PExtInv, zz) != 0)
			{
				Nat.DecAt(10, zz, SecP160R2Field.PExtInv.Length);
			}
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x000F4431 File Offset: 0x000F2631
		public static void Twice(uint[] x, uint[] z)
		{
			if (Nat.ShiftUpBit(5, x, 0U, z) != 0U || (z[4] == 4294967295U && Nat160.Gte(z, SecP160R2Field.P)))
			{
				Nat.Add33To(5, 21389U, z);
			}
		}

		// Token: 0x040018DC RID: 6364
		internal static readonly uint[] P = new uint[]
		{
			4294945907U,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x040018DD RID: 6365
		internal static readonly uint[] PExt = new uint[]
		{
			457489321U,
			42778U,
			1U,
			0U,
			0U,
			4294924518U,
			4294967293U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x040018DE RID: 6366
		private static readonly uint[] PExtInv = new uint[]
		{
			3837477975U,
			4294924517U,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			42777U,
			2U
		};

		// Token: 0x040018DF RID: 6367
		private const uint P4 = 4294967295U;

		// Token: 0x040018E0 RID: 6368
		private const uint PExt9 = 4294967295U;

		// Token: 0x040018E1 RID: 6369
		private const uint PInv33 = 21389U;
	}
}
