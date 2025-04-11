using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000354 RID: 852
	internal class SecP224K1Field
	{
		// Token: 0x0600217C RID: 8572 RVA: 0x000F6C09 File Offset: 0x000F4E09
		public static void Add(uint[] x, uint[] y, uint[] z)
		{
			if (Nat224.Add(x, y, z) != 0U || (z[6] == 4294967295U && Nat224.Gte(z, SecP224K1Field.P)))
			{
				Nat.Add33To(7, 6803U, z);
			}
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x000F6C38 File Offset: 0x000F4E38
		public static void AddExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if ((Nat.Add(14, xx, yy, zz) != 0U || (zz[13] == 4294967295U && Nat.Gte(14, zz, SecP224K1Field.PExt))) && Nat.AddTo(SecP224K1Field.PExtInv.Length, SecP224K1Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(14, zz, SecP224K1Field.PExtInv.Length);
			}
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x000F6C8B File Offset: 0x000F4E8B
		public static void AddOne(uint[] x, uint[] z)
		{
			if (Nat.Inc(7, x, z) != 0U || (z[6] == 4294967295U && Nat224.Gte(z, SecP224K1Field.P)))
			{
				Nat.Add33To(7, 6803U, z);
			}
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x000F6CB8 File Offset: 0x000F4EB8
		public static uint[] FromBigInteger(BigInteger x)
		{
			uint[] array = Nat224.FromBigInteger(x);
			if (array[6] == 4294967295U && Nat224.Gte(array, SecP224K1Field.P))
			{
				Nat224.SubFrom(SecP224K1Field.P, array);
			}
			return array;
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x000F6CEC File Offset: 0x000F4EEC
		public static void Half(uint[] x, uint[] z)
		{
			if ((x[0] & 1U) == 0U)
			{
				Nat.ShiftDownBit(7, x, 0U, z);
				return;
			}
			uint c = Nat224.Add(x, SecP224K1Field.P, z);
			Nat.ShiftDownBit(7, z, c);
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x000F6D24 File Offset: 0x000F4F24
		public static void Multiply(uint[] x, uint[] y, uint[] z)
		{
			uint[] array = Nat224.CreateExt();
			Nat224.Mul(x, y, array);
			SecP224K1Field.Reduce(array, z);
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x000F6D48 File Offset: 0x000F4F48
		public static void MultiplyAddToExt(uint[] x, uint[] y, uint[] zz)
		{
			if ((Nat224.MulAddTo(x, y, zz) != 0U || (zz[13] == 4294967295U && Nat.Gte(14, zz, SecP224K1Field.PExt))) && Nat.AddTo(SecP224K1Field.PExtInv.Length, SecP224K1Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(14, zz, SecP224K1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x000F6D99 File Offset: 0x000F4F99
		public static void Negate(uint[] x, uint[] z)
		{
			if (Nat224.IsZero(x))
			{
				Nat224.Zero(z);
				return;
			}
			Nat224.Sub(SecP224K1Field.P, x, z);
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x000F6DB8 File Offset: 0x000F4FB8
		public static void Reduce(uint[] xx, uint[] z)
		{
			ulong y = Nat224.Mul33Add(6803U, xx, 7, xx, 0, z, 0);
			if (Nat224.Mul33DWordAdd(6803U, y, z, 0) != 0U || (z[6] == 4294967295U && Nat224.Gte(z, SecP224K1Field.P)))
			{
				Nat.Add33To(7, 6803U, z);
			}
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x000F6E05 File Offset: 0x000F5005
		public static void Reduce32(uint x, uint[] z)
		{
			if ((x != 0U && Nat224.Mul33WordAdd(6803U, x, z, 0) != 0U) || (z[6] == 4294967295U && Nat224.Gte(z, SecP224K1Field.P)))
			{
				Nat.Add33To(7, 6803U, z);
			}
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x000F6E3C File Offset: 0x000F503C
		public static void Square(uint[] x, uint[] z)
		{
			uint[] array = Nat224.CreateExt();
			Nat224.Square(x, array);
			SecP224K1Field.Reduce(array, z);
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x000F6E60 File Offset: 0x000F5060
		public static void SquareN(uint[] x, int n, uint[] z)
		{
			uint[] array = Nat224.CreateExt();
			Nat224.Square(x, array);
			SecP224K1Field.Reduce(array, z);
			while (--n > 0)
			{
				Nat224.Square(z, array);
				SecP224K1Field.Reduce(array, z);
			}
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x000F6E9A File Offset: 0x000F509A
		public static void Subtract(uint[] x, uint[] y, uint[] z)
		{
			if (Nat224.Sub(x, y, z) != 0)
			{
				Nat.Sub33From(7, 6803U, z);
			}
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x000F6EB3 File Offset: 0x000F50B3
		public static void SubtractExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if (Nat.Sub(14, xx, yy, zz) != 0 && Nat.SubFrom(SecP224K1Field.PExtInv.Length, SecP224K1Field.PExtInv, zz) != 0)
			{
				Nat.DecAt(14, zz, SecP224K1Field.PExtInv.Length);
			}
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x000F6EE5 File Offset: 0x000F50E5
		public static void Twice(uint[] x, uint[] z)
		{
			if (Nat.ShiftUpBit(7, x, 0U, z) != 0U || (z[6] == 4294967295U && Nat224.Gte(z, SecP224K1Field.P)))
			{
				Nat.Add33To(7, 6803U, z);
			}
		}

		// Token: 0x040018FF RID: 6399
		internal static readonly uint[] P = new uint[]
		{
			4294960493U,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x04001900 RID: 6400
		internal static readonly uint[] PExt = new uint[]
		{
			46280809U,
			13606U,
			1U,
			0U,
			0U,
			0U,
			0U,
			4294953690U,
			4294967293U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x04001901 RID: 6401
		private static readonly uint[] PExtInv = new uint[]
		{
			4248686487U,
			4294953689U,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			13605U,
			2U
		};

		// Token: 0x04001902 RID: 6402
		private const uint P6 = 4294967295U;

		// Token: 0x04001903 RID: 6403
		private const uint PExt13 = 4294967295U;

		// Token: 0x04001904 RID: 6404
		private const uint PInv33 = 6803U;
	}
}
