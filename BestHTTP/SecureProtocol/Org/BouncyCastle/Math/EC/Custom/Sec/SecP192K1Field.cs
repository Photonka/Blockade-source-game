using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200034C RID: 844
	internal class SecP192K1Field
	{
		// Token: 0x06002102 RID: 8450 RVA: 0x000F4F55 File Offset: 0x000F3155
		public static void Add(uint[] x, uint[] y, uint[] z)
		{
			if (Nat192.Add(x, y, z) != 0U || (z[5] == 4294967295U && Nat192.Gte(z, SecP192K1Field.P)))
			{
				Nat.Add33To(6, 4553U, z);
			}
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x000F4F84 File Offset: 0x000F3184
		public static void AddExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if ((Nat.Add(12, xx, yy, zz) != 0U || (zz[11] == 4294967295U && Nat.Gte(12, zz, SecP192K1Field.PExt))) && Nat.AddTo(SecP192K1Field.PExtInv.Length, SecP192K1Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(12, zz, SecP192K1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x000F4FD7 File Offset: 0x000F31D7
		public static void AddOne(uint[] x, uint[] z)
		{
			if (Nat.Inc(6, x, z) != 0U || (z[5] == 4294967295U && Nat192.Gte(z, SecP192K1Field.P)))
			{
				Nat.Add33To(6, 4553U, z);
			}
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x000F5004 File Offset: 0x000F3204
		public static uint[] FromBigInteger(BigInteger x)
		{
			uint[] array = Nat192.FromBigInteger(x);
			if (array[5] == 4294967295U && Nat192.Gte(array, SecP192K1Field.P))
			{
				Nat192.SubFrom(SecP192K1Field.P, array);
			}
			return array;
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x000F5038 File Offset: 0x000F3238
		public static void Half(uint[] x, uint[] z)
		{
			if ((x[0] & 1U) == 0U)
			{
				Nat.ShiftDownBit(6, x, 0U, z);
				return;
			}
			uint c = Nat192.Add(x, SecP192K1Field.P, z);
			Nat.ShiftDownBit(6, z, c);
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x000F5070 File Offset: 0x000F3270
		public static void Multiply(uint[] x, uint[] y, uint[] z)
		{
			uint[] array = Nat192.CreateExt();
			Nat192.Mul(x, y, array);
			SecP192K1Field.Reduce(array, z);
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x000F5094 File Offset: 0x000F3294
		public static void MultiplyAddToExt(uint[] x, uint[] y, uint[] zz)
		{
			if ((Nat192.MulAddTo(x, y, zz) != 0U || (zz[11] == 4294967295U && Nat.Gte(12, zz, SecP192K1Field.PExt))) && Nat.AddTo(SecP192K1Field.PExtInv.Length, SecP192K1Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(12, zz, SecP192K1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x000F50E5 File Offset: 0x000F32E5
		public static void Negate(uint[] x, uint[] z)
		{
			if (Nat192.IsZero(x))
			{
				Nat192.Zero(z);
				return;
			}
			Nat192.Sub(SecP192K1Field.P, x, z);
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x000F5104 File Offset: 0x000F3304
		public static void Reduce(uint[] xx, uint[] z)
		{
			ulong y = Nat192.Mul33Add(4553U, xx, 6, xx, 0, z, 0);
			if (Nat192.Mul33DWordAdd(4553U, y, z, 0) != 0U || (z[5] == 4294967295U && Nat192.Gte(z, SecP192K1Field.P)))
			{
				Nat.Add33To(6, 4553U, z);
			}
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x000F5151 File Offset: 0x000F3351
		public static void Reduce32(uint x, uint[] z)
		{
			if ((x != 0U && Nat192.Mul33WordAdd(4553U, x, z, 0) != 0U) || (z[5] == 4294967295U && Nat192.Gte(z, SecP192K1Field.P)))
			{
				Nat.Add33To(6, 4553U, z);
			}
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x000F5188 File Offset: 0x000F3388
		public static void Square(uint[] x, uint[] z)
		{
			uint[] array = Nat192.CreateExt();
			Nat192.Square(x, array);
			SecP192K1Field.Reduce(array, z);
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x000F51AC File Offset: 0x000F33AC
		public static void SquareN(uint[] x, int n, uint[] z)
		{
			uint[] array = Nat192.CreateExt();
			Nat192.Square(x, array);
			SecP192K1Field.Reduce(array, z);
			while (--n > 0)
			{
				Nat192.Square(z, array);
				SecP192K1Field.Reduce(array, z);
			}
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x000F51E6 File Offset: 0x000F33E6
		public static void Subtract(uint[] x, uint[] y, uint[] z)
		{
			if (Nat192.Sub(x, y, z) != 0)
			{
				Nat.Sub33From(6, 4553U, z);
			}
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x000F51FF File Offset: 0x000F33FF
		public static void SubtractExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if (Nat.Sub(12, xx, yy, zz) != 0 && Nat.SubFrom(SecP192K1Field.PExtInv.Length, SecP192K1Field.PExtInv, zz) != 0)
			{
				Nat.DecAt(12, zz, SecP192K1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x000F5231 File Offset: 0x000F3431
		public static void Twice(uint[] x, uint[] z)
		{
			if (Nat.ShiftUpBit(6, x, 0U, z) != 0U || (z[5] == 4294967295U && Nat192.Gte(z, SecP192K1Field.P)))
			{
				Nat.Add33To(6, 4553U, z);
			}
		}

		// Token: 0x040018E8 RID: 6376
		internal static readonly uint[] P = new uint[]
		{
			4294962743U,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x040018E9 RID: 6377
		internal static readonly uint[] PExt = new uint[]
		{
			20729809U,
			9106U,
			1U,
			0U,
			0U,
			0U,
			4294958190U,
			4294967293U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x040018EA RID: 6378
		private static readonly uint[] PExtInv = new uint[]
		{
			4274237487U,
			4294958189U,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			9105U,
			2U
		};

		// Token: 0x040018EB RID: 6379
		private const uint P5 = 4294967295U;

		// Token: 0x040018EC RID: 6380
		private const uint PExt11 = 4294967295U;

		// Token: 0x040018ED RID: 6381
		private const uint PInv33 = 4553U;
	}
}
