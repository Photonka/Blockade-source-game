using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000358 RID: 856
	internal class SecP224R1Field
	{
		// Token: 0x060021B8 RID: 8632 RVA: 0x000F7A31 File Offset: 0x000F5C31
		public static void Add(uint[] x, uint[] y, uint[] z)
		{
			if (Nat224.Add(x, y, z) != 0U || (z[6] == 4294967295U && Nat224.Gte(z, SecP224R1Field.P)))
			{
				SecP224R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x000F7A58 File Offset: 0x000F5C58
		public static void AddExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if ((Nat.Add(14, xx, yy, zz) != 0U || (zz[13] == 4294967295U && Nat.Gte(14, zz, SecP224R1Field.PExt))) && Nat.AddTo(SecP224R1Field.PExtInv.Length, SecP224R1Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(14, zz, SecP224R1Field.PExtInv.Length);
			}
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x000F7AAB File Offset: 0x000F5CAB
		public static void AddOne(uint[] x, uint[] z)
		{
			if (Nat.Inc(7, x, z) != 0U || (z[6] == 4294967295U && Nat224.Gte(z, SecP224R1Field.P)))
			{
				SecP224R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x000F7AD0 File Offset: 0x000F5CD0
		public static uint[] FromBigInteger(BigInteger x)
		{
			uint[] array = Nat224.FromBigInteger(x);
			if (array[6] == 4294967295U && Nat224.Gte(array, SecP224R1Field.P))
			{
				Nat224.SubFrom(SecP224R1Field.P, array);
			}
			return array;
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x000F7B04 File Offset: 0x000F5D04
		public static void Half(uint[] x, uint[] z)
		{
			if ((x[0] & 1U) == 0U)
			{
				Nat.ShiftDownBit(7, x, 0U, z);
				return;
			}
			uint c = Nat224.Add(x, SecP224R1Field.P, z);
			Nat.ShiftDownBit(7, z, c);
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x000F7B3C File Offset: 0x000F5D3C
		public static void Multiply(uint[] x, uint[] y, uint[] z)
		{
			uint[] array = Nat224.CreateExt();
			Nat224.Mul(x, y, array);
			SecP224R1Field.Reduce(array, z);
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x000F7B60 File Offset: 0x000F5D60
		public static void MultiplyAddToExt(uint[] x, uint[] y, uint[] zz)
		{
			if ((Nat224.MulAddTo(x, y, zz) != 0U || (zz[13] == 4294967295U && Nat.Gte(14, zz, SecP224R1Field.PExt))) && Nat.AddTo(SecP224R1Field.PExtInv.Length, SecP224R1Field.PExtInv, zz) != 0U)
			{
				Nat.IncAt(14, zz, SecP224R1Field.PExtInv.Length);
			}
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x000F7BB1 File Offset: 0x000F5DB1
		public static void Negate(uint[] x, uint[] z)
		{
			if (Nat224.IsZero(x))
			{
				Nat224.Zero(z);
				return;
			}
			Nat224.Sub(SecP224R1Field.P, x, z);
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x000F7BD0 File Offset: 0x000F5DD0
		public static void Reduce(uint[] xx, uint[] z)
		{
			long num = (long)((ulong)xx[10]);
			long num2 = (long)((ulong)xx[11]);
			long num3 = (long)((ulong)xx[12]);
			long num4 = (long)((ulong)xx[13]);
			long num5 = (long)((ulong)xx[7] + (ulong)num2 - 1UL);
			long num6 = (long)((ulong)xx[8] + (ulong)num3);
			long num7 = (long)((ulong)xx[9] + (ulong)num4);
			long num8 = 0L;
			num8 += (long)((ulong)xx[0] - (ulong)num5);
			long num9 = (long)((ulong)((uint)num8));
			num8 >>= 32;
			num8 += (long)((ulong)xx[1] - (ulong)num6);
			z[1] = (uint)num8;
			num8 >>= 32;
			num8 += (long)((ulong)xx[2] - (ulong)num7);
			z[2] = (uint)num8;
			num8 >>= 32;
			num8 += (long)((ulong)xx[3] + (ulong)num5 - (ulong)num);
			long num10 = (long)((ulong)((uint)num8));
			num8 >>= 32;
			num8 += (long)((ulong)xx[4] + (ulong)num6 - (ulong)num2);
			z[4] = (uint)num8;
			num8 >>= 32;
			num8 += (long)((ulong)xx[5] + (ulong)num7 - (ulong)num3);
			z[5] = (uint)num8;
			num8 >>= 32;
			num8 += (long)((ulong)xx[6] + (ulong)num - (ulong)num4);
			z[6] = (uint)num8;
			num8 >>= 32;
			num8 += 1L;
			num10 += num8;
			num9 -= num8;
			z[0] = (uint)num9;
			num8 = num9 >> 32;
			if (num8 != 0L)
			{
				num8 += (long)((ulong)z[1]);
				z[1] = (uint)num8;
				num8 >>= 32;
				num8 += (long)((ulong)z[2]);
				z[2] = (uint)num8;
				num10 += num8 >> 32;
			}
			z[3] = (uint)num10;
			num8 = num10 >> 32;
			if ((num8 != 0L && Nat.IncAt(7, z, 4) != 0U) || (z[6] == 4294967295U && Nat224.Gte(z, SecP224R1Field.P)))
			{
				SecP224R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x000F7D54 File Offset: 0x000F5F54
		public static void Reduce32(uint x, uint[] z)
		{
			long num = 0L;
			if (x != 0U)
			{
				long num2 = (long)((ulong)x);
				num += (long)((ulong)z[0] - (ulong)num2);
				z[0] = (uint)num;
				num >>= 32;
				if (num != 0L)
				{
					num += (long)((ulong)z[1]);
					z[1] = (uint)num;
					num >>= 32;
					num += (long)((ulong)z[2]);
					z[2] = (uint)num;
					num >>= 32;
				}
				num += (long)((ulong)z[3] + (ulong)num2);
				z[3] = (uint)num;
				num >>= 32;
			}
			if ((num != 0L && Nat.IncAt(7, z, 4) != 0U) || (z[6] == 4294967295U && Nat224.Gte(z, SecP224R1Field.P)))
			{
				SecP224R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x000F7DDC File Offset: 0x000F5FDC
		public static void Square(uint[] x, uint[] z)
		{
			uint[] array = Nat224.CreateExt();
			Nat224.Square(x, array);
			SecP224R1Field.Reduce(array, z);
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x000F7E00 File Offset: 0x000F6000
		public static void SquareN(uint[] x, int n, uint[] z)
		{
			uint[] array = Nat224.CreateExt();
			Nat224.Square(x, array);
			SecP224R1Field.Reduce(array, z);
			while (--n > 0)
			{
				Nat224.Square(z, array);
				SecP224R1Field.Reduce(array, z);
			}
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x000F7E3A File Offset: 0x000F603A
		public static void Subtract(uint[] x, uint[] y, uint[] z)
		{
			if (Nat224.Sub(x, y, z) != 0)
			{
				SecP224R1Field.SubPInvFrom(z);
			}
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x000F7E4C File Offset: 0x000F604C
		public static void SubtractExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if (Nat.Sub(14, xx, yy, zz) != 0 && Nat.SubFrom(SecP224R1Field.PExtInv.Length, SecP224R1Field.PExtInv, zz) != 0)
			{
				Nat.DecAt(14, zz, SecP224R1Field.PExtInv.Length);
			}
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x000F7E7E File Offset: 0x000F607E
		public static void Twice(uint[] x, uint[] z)
		{
			if (Nat.ShiftUpBit(7, x, 0U, z) != 0U || (z[6] == 4294967295U && Nat224.Gte(z, SecP224R1Field.P)))
			{
				SecP224R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x000F7EA4 File Offset: 0x000F60A4
		private static void AddPInvTo(uint[] z)
		{
			long num = (long)((ulong)z[0] - 1UL);
			z[0] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				num += (long)((ulong)z[1]);
				z[1] = (uint)num;
				num >>= 32;
				num += (long)((ulong)z[2]);
				z[2] = (uint)num;
				num >>= 32;
			}
			num += (long)((ulong)z[3] + 1UL);
			z[3] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				Nat.IncAt(7, z, 4);
			}
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x000F7F08 File Offset: 0x000F6108
		private static void SubPInvFrom(uint[] z)
		{
			long num = (long)((ulong)z[0] + 1UL);
			z[0] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				num += (long)((ulong)z[1]);
				z[1] = (uint)num;
				num >>= 32;
				num += (long)((ulong)z[2]);
				z[2] = (uint)num;
				num >>= 32;
			}
			num += (long)((ulong)z[3] - 1UL);
			z[3] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				Nat.DecAt(7, z, 4);
			}
		}

		// Token: 0x0400190C RID: 6412
		internal static readonly uint[] P = new uint[]
		{
			1U,
			0U,
			0U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x0400190D RID: 6413
		internal static readonly uint[] PExt = new uint[]
		{
			1U,
			0U,
			0U,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			0U,
			2U,
			0U,
			0U,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue
		};

		// Token: 0x0400190E RID: 6414
		private static readonly uint[] PExtInv = new uint[]
		{
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			1U,
			0U,
			0U,
			uint.MaxValue,
			4294967293U,
			uint.MaxValue,
			uint.MaxValue,
			1U
		};

		// Token: 0x0400190F RID: 6415
		private const uint P6 = 4294967295U;

		// Token: 0x04001910 RID: 6416
		private const uint PExt13 = 4294967295U;
	}
}
