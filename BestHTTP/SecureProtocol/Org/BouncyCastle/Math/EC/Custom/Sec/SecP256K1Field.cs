using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200035C RID: 860
	internal class SecP256K1Field
	{
		// Token: 0x060021FB RID: 8699 RVA: 0x000F8B29 File Offset: 0x000F6D29
		public static void Add(uint[] x, uint[] y, uint[] z)
		{
			if (Nat256.Add(x, y, z) != 0U || (z[7] == 4294967295U && Nat256.Gte(z, SecP256K1Field.P)))
			{
				Nat.Add33To(8, 977U, z);
			}
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x000F8B58 File Offset: 0x000F6D58
		public static void AddExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if ((Nat.Add(16, xx, yy, zz) != 0U || (zz[15] == 4294967295U && Nat.Gte(16, zz, SecP256K1Field.PExt))) && Nat.AddTo(SecP256K1Field.PExtInv.Length, SecP256K1Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(16, zz, SecP256K1Field.PExtInv.Length);
			}
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x000F8BAB File Offset: 0x000F6DAB
		public static void AddOne(uint[] x, uint[] z)
		{
			if (Nat.Inc(8, x, z) != 0U || (z[7] == 4294967295U && Nat256.Gte(z, SecP256K1Field.P)))
			{
				Nat.Add33To(8, 977U, z);
			}
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x000F8BD8 File Offset: 0x000F6DD8
		public static uint[] FromBigInteger(BigInteger x)
		{
			uint[] array = Nat256.FromBigInteger(x);
			if (array[7] == 4294967295U && Nat256.Gte(array, SecP256K1Field.P))
			{
				Nat256.SubFrom(SecP256K1Field.P, array);
			}
			return array;
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x000F8C0C File Offset: 0x000F6E0C
		public static void Half(uint[] x, uint[] z)
		{
			if ((x[0] & 1U) == 0U)
			{
				Nat.ShiftDownBit(8, x, 0U, z);
				return;
			}
			uint c = Nat256.Add(x, SecP256K1Field.P, z);
			Nat.ShiftDownBit(8, z, c);
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x000F8C44 File Offset: 0x000F6E44
		public static void Multiply(uint[] x, uint[] y, uint[] z)
		{
			uint[] array = Nat256.CreateExt();
			Nat256.Mul(x, y, array);
			SecP256K1Field.Reduce(array, z);
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x000F8C68 File Offset: 0x000F6E68
		public static void MultiplyAddToExt(uint[] x, uint[] y, uint[] zz)
		{
			if ((Nat256.MulAddTo(x, y, zz) != 0U || (zz[15] == 4294967295U && Nat.Gte(16, zz, SecP256K1Field.PExt))) && Nat.AddTo(SecP256K1Field.PExtInv.Length, SecP256K1Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(16, zz, SecP256K1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x000F8CB9 File Offset: 0x000F6EB9
		public static void Negate(uint[] x, uint[] z)
		{
			if (Nat256.IsZero(x))
			{
				Nat256.Zero(z);
				return;
			}
			Nat256.Sub(SecP256K1Field.P, x, z);
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x000F8CD8 File Offset: 0x000F6ED8
		public static void Reduce(uint[] xx, uint[] z)
		{
			ulong y = Nat256.Mul33Add(977U, xx, 8, xx, 0, z, 0);
			if (Nat256.Mul33DWordAdd(977U, y, z, 0) != 0U || (z[7] == 4294967295U && Nat256.Gte(z, SecP256K1Field.P)))
			{
				Nat.Add33To(8, 977U, z);
			}
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x000F8D25 File Offset: 0x000F6F25
		public static void Reduce32(uint x, uint[] z)
		{
			if ((x != 0U && Nat256.Mul33WordAdd(977U, x, z, 0) != 0U) || (z[7] == 4294967295U && Nat256.Gte(z, SecP256K1Field.P)))
			{
				Nat.Add33To(8, 977U, z);
			}
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x000F8D5C File Offset: 0x000F6F5C
		public static void Square(uint[] x, uint[] z)
		{
			uint[] array = Nat256.CreateExt();
			Nat256.Square(x, array);
			SecP256K1Field.Reduce(array, z);
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x000F8D80 File Offset: 0x000F6F80
		public static void SquareN(uint[] x, int n, uint[] z)
		{
			uint[] array = Nat256.CreateExt();
			Nat256.Square(x, array);
			SecP256K1Field.Reduce(array, z);
			while (--n > 0)
			{
				Nat256.Square(z, array);
				SecP256K1Field.Reduce(array, z);
			}
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x000F8DBA File Offset: 0x000F6FBA
		public static void Subtract(uint[] x, uint[] y, uint[] z)
		{
			if (Nat256.Sub(x, y, z) != 0)
			{
				Nat.Sub33From(8, 977U, z);
			}
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x000F8DD3 File Offset: 0x000F6FD3
		public static void SubtractExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if (Nat.Sub(16, xx, yy, zz) != 0 && Nat.SubFrom(SecP256K1Field.PExtInv.Length, SecP256K1Field.PExtInv, zz) != 0)
			{
				Nat.DecAt(16, zz, SecP256K1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x000F8E05 File Offset: 0x000F7005
		public static void Twice(uint[] x, uint[] z)
		{
			if (Nat.ShiftUpBit(8, x, 0U, z) != 0U || (z[7] == 4294967295U && Nat256.Gte(z, SecP256K1Field.P)))
			{
				Nat.Add33To(8, 977U, z);
			}
		}

		// Token: 0x04001917 RID: 6423
		internal static readonly uint[] P = new uint[]
		{
			4294966319U,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x04001918 RID: 6424
		internal static readonly uint[] PExt = new uint[]
		{
			954529U,
			1954U,
			1U,
			0U,
			0U,
			0U,
			0U,
			0U,
			4294965342U,
			4294967293U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x04001919 RID: 6425
		private static readonly uint[] PExtInv = new uint[]
		{
			4294012767U,
			4294965341U,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			1953U,
			2U
		};

		// Token: 0x0400191A RID: 6426
		private const uint P7 = 4294967295U;

		// Token: 0x0400191B RID: 6427
		private const uint PExt15 = 4294967295U;

		// Token: 0x0400191C RID: 6428
		private const uint PInv33 = 977U;
	}
}
