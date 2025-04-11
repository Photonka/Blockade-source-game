using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000350 RID: 848
	internal class SecP192R1Field
	{
		// Token: 0x0600213E RID: 8510 RVA: 0x000F5D0D File Offset: 0x000F3F0D
		public static void Add(uint[] x, uint[] y, uint[] z)
		{
			if (Nat192.Add(x, y, z) != 0U || (z[5] == 4294967295U && Nat192.Gte(z, SecP192R1Field.P)))
			{
				SecP192R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x000F5D34 File Offset: 0x000F3F34
		public static void AddExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if ((Nat.Add(12, xx, yy, zz) != 0U || (zz[11] == 4294967295U && Nat.Gte(12, zz, SecP192R1Field.PExt))) && Nat.AddTo(SecP192R1Field.PExtInv.Length, SecP192R1Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(12, zz, SecP192R1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x000F5D87 File Offset: 0x000F3F87
		public static void AddOne(uint[] x, uint[] z)
		{
			if (Nat.Inc(6, x, z) != 0U || (z[5] == 4294967295U && Nat192.Gte(z, SecP192R1Field.P)))
			{
				SecP192R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x000F5DAC File Offset: 0x000F3FAC
		public static uint[] FromBigInteger(BigInteger x)
		{
			uint[] array = Nat192.FromBigInteger(x);
			if (array[5] == 4294967295U && Nat192.Gte(array, SecP192R1Field.P))
			{
				Nat192.SubFrom(SecP192R1Field.P, array);
			}
			return array;
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x000F5DE0 File Offset: 0x000F3FE0
		public static void Half(uint[] x, uint[] z)
		{
			if ((x[0] & 1U) == 0U)
			{
				Nat.ShiftDownBit(6, x, 0U, z);
				return;
			}
			uint c = Nat192.Add(x, SecP192R1Field.P, z);
			Nat.ShiftDownBit(6, z, c);
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x000F5E18 File Offset: 0x000F4018
		public static void Multiply(uint[] x, uint[] y, uint[] z)
		{
			uint[] array = Nat192.CreateExt();
			Nat192.Mul(x, y, array);
			SecP192R1Field.Reduce(array, z);
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x000F5E3C File Offset: 0x000F403C
		public static void MultiplyAddToExt(uint[] x, uint[] y, uint[] zz)
		{
			if ((Nat192.MulAddTo(x, y, zz) != 0U || (zz[11] == 4294967295U && Nat.Gte(12, zz, SecP192R1Field.PExt))) && Nat.AddTo(SecP192R1Field.PExtInv.Length, SecP192R1Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(12, zz, SecP192R1Field.PExtInv.Length);
			}
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x000F5E8D File Offset: 0x000F408D
		public static void Negate(uint[] x, uint[] z)
		{
			if (Nat192.IsZero(x))
			{
				Nat192.Zero(z);
				return;
			}
			Nat192.Sub(SecP192R1Field.P, x, z);
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x000F5EAC File Offset: 0x000F40AC
		public static void Reduce(uint[] xx, uint[] z)
		{
			ulong num = (ulong)xx[6];
			ulong num2 = (ulong)xx[7];
			ulong num3 = (ulong)xx[8];
			ulong num4 = (ulong)xx[9];
			ulong num5 = (ulong)xx[10];
			ulong num6 = (ulong)xx[11];
			ulong num7 = num + num5;
			ulong num8 = num2 + num6;
			ulong num9 = 0UL;
			num9 += (ulong)xx[0] + num7;
			uint num10 = (uint)num9;
			num9 >>= 32;
			num9 += (ulong)xx[1] + num8;
			z[1] = (uint)num9;
			num9 >>= 32;
			num7 += num3;
			num8 += num4;
			num9 += (ulong)xx[2] + num7;
			ulong num11 = (ulong)((uint)num9);
			num9 >>= 32;
			num9 += (ulong)xx[3] + num8;
			z[3] = (uint)num9;
			num9 >>= 32;
			num7 -= num;
			num8 -= num2;
			num9 += (ulong)xx[4] + num7;
			z[4] = (uint)num9;
			num9 >>= 32;
			num9 += (ulong)xx[5] + num8;
			z[5] = (uint)num9;
			num9 >>= 32;
			num11 += num9;
			num9 += (ulong)num10;
			z[0] = (uint)num9;
			num9 >>= 32;
			if (num9 != 0UL)
			{
				num9 += (ulong)z[1];
				z[1] = (uint)num9;
				num11 += num9 >> 32;
			}
			z[2] = (uint)num11;
			num9 = num11 >> 32;
			if ((num9 != 0UL && Nat.IncAt(6, z, 3) != 0U) || (z[5] == 4294967295U && Nat192.Gte(z, SecP192R1Field.P)))
			{
				SecP192R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x000F6008 File Offset: 0x000F4208
		public static void Reduce32(uint x, uint[] z)
		{
			ulong num = 0UL;
			if (x != 0U)
			{
				num += (ulong)z[0] + (ulong)x;
				z[0] = (uint)num;
				num >>= 32;
				if (num != 0UL)
				{
					num += (ulong)z[1];
					z[1] = (uint)num;
					num >>= 32;
				}
				num += (ulong)z[2] + (ulong)x;
				z[2] = (uint)num;
				num >>= 32;
			}
			if ((num != 0UL && Nat.IncAt(6, z, 3) != 0U) || (z[5] == 4294967295U && Nat192.Gte(z, SecP192R1Field.P)))
			{
				SecP192R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x000F6080 File Offset: 0x000F4280
		public static void Square(uint[] x, uint[] z)
		{
			uint[] array = Nat192.CreateExt();
			Nat192.Square(x, array);
			SecP192R1Field.Reduce(array, z);
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x000F60A4 File Offset: 0x000F42A4
		public static void SquareN(uint[] x, int n, uint[] z)
		{
			uint[] array = Nat192.CreateExt();
			Nat192.Square(x, array);
			SecP192R1Field.Reduce(array, z);
			while (--n > 0)
			{
				Nat192.Square(z, array);
				SecP192R1Field.Reduce(array, z);
			}
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x000F60DE File Offset: 0x000F42DE
		public static void Subtract(uint[] x, uint[] y, uint[] z)
		{
			if (Nat192.Sub(x, y, z) != 0)
			{
				SecP192R1Field.SubPInvFrom(z);
			}
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x000F60F0 File Offset: 0x000F42F0
		public static void SubtractExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if (Nat.Sub(12, xx, yy, zz) != 0 && Nat.SubFrom(SecP192R1Field.PExtInv.Length, SecP192R1Field.PExtInv, zz) != 0)
			{
				Nat.DecAt(12, zz, SecP192R1Field.PExtInv.Length);
			}
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x000F6122 File Offset: 0x000F4322
		public static void Twice(uint[] x, uint[] z)
		{
			if (Nat.ShiftUpBit(6, x, 0U, z) != 0U || (z[5] == 4294967295U && Nat192.Gte(z, SecP192R1Field.P)))
			{
				SecP192R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x000F6148 File Offset: 0x000F4348
		private static void AddPInvTo(uint[] z)
		{
			long num = (long)((ulong)z[0] + 1UL);
			z[0] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				num += (long)((ulong)z[1]);
				z[1] = (uint)num;
				num >>= 32;
			}
			num += (long)((ulong)z[2] + 1UL);
			z[2] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				Nat.IncAt(6, z, 3);
			}
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x000F619C File Offset: 0x000F439C
		private static void SubPInvFrom(uint[] z)
		{
			long num = (long)((ulong)z[0] - 1UL);
			z[0] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				num += (long)((ulong)z[1]);
				z[1] = (uint)num;
				num >>= 32;
			}
			num += (long)((ulong)z[2] - 1UL);
			z[2] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				Nat.DecAt(6, z, 3);
			}
		}

		// Token: 0x040018F4 RID: 6388
		internal static readonly uint[] P = new uint[]
		{
			uint.MaxValue,
			uint.MaxValue,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x040018F5 RID: 6389
		internal static readonly uint[] PExt = new uint[]
		{
			1U,
			0U,
			2U,
			0U,
			1U,
			0U,
			4294967294U,
			uint.MaxValue,
			4294967293U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x040018F6 RID: 6390
		private static readonly uint[] PExtInv = new uint[]
		{
			uint.MaxValue,
			uint.MaxValue,
			4294967293U,
			uint.MaxValue,
			4294967294U,
			uint.MaxValue,
			1U,
			0U,
			2U
		};

		// Token: 0x040018F7 RID: 6391
		private const uint P5 = 4294967295U;

		// Token: 0x040018F8 RID: 6392
		private const uint PExt11 = 4294967295U;
	}
}
