using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x02000360 RID: 864
	internal class SecP256R1Field
	{
		// Token: 0x06002237 RID: 8759 RVA: 0x000F98ED File Offset: 0x000F7AED
		public static void Add(uint[] x, uint[] y, uint[] z)
		{
			if (Nat256.Add(x, y, z) != 0U || (z[7] == 4294967295U && Nat256.Gte(z, SecP256R1Field.P)))
			{
				SecP256R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x000F9912 File Offset: 0x000F7B12
		public static void AddExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if (Nat.Add(16, xx, yy, zz) != 0U || (zz[15] >= 4294967294U && Nat.Gte(16, zz, SecP256R1Field.PExt)))
			{
				Nat.SubFrom(16, SecP256R1Field.PExt, zz);
			}
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x000F9945 File Offset: 0x000F7B45
		public static void AddOne(uint[] x, uint[] z)
		{
			if (Nat.Inc(8, x, z) != 0U || (z[7] == 4294967295U && Nat256.Gte(z, SecP256R1Field.P)))
			{
				SecP256R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x000F996C File Offset: 0x000F7B6C
		public static uint[] FromBigInteger(BigInteger x)
		{
			uint[] array = Nat256.FromBigInteger(x);
			if (array[7] == 4294967295U && Nat256.Gte(array, SecP256R1Field.P))
			{
				Nat256.SubFrom(SecP256R1Field.P, array);
			}
			return array;
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x000F99A0 File Offset: 0x000F7BA0
		public static void Half(uint[] x, uint[] z)
		{
			if ((x[0] & 1U) == 0U)
			{
				Nat.ShiftDownBit(8, x, 0U, z);
				return;
			}
			uint c = Nat256.Add(x, SecP256R1Field.P, z);
			Nat.ShiftDownBit(8, z, c);
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x000F99D8 File Offset: 0x000F7BD8
		public static void Multiply(uint[] x, uint[] y, uint[] z)
		{
			uint[] array = Nat256.CreateExt();
			Nat256.Mul(x, y, array);
			SecP256R1Field.Reduce(array, z);
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x000F99FA File Offset: 0x000F7BFA
		public static void MultiplyAddToExt(uint[] x, uint[] y, uint[] zz)
		{
			if (Nat256.MulAddTo(x, y, zz) != 0U || (zz[15] >= 4294967294U && Nat.Gte(16, zz, SecP256R1Field.PExt)))
			{
				Nat.SubFrom(16, SecP256R1Field.PExt, zz);
			}
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x000F9A2B File Offset: 0x000F7C2B
		public static void Negate(uint[] x, uint[] z)
		{
			if (Nat256.IsZero(x))
			{
				Nat256.Zero(z);
				return;
			}
			Nat256.Sub(SecP256R1Field.P, x, z);
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x000F9A4C File Offset: 0x000F7C4C
		public static void Reduce(uint[] xx, uint[] z)
		{
			long num = (long)((ulong)xx[8]);
			long num2 = (long)((ulong)xx[9]);
			long num3 = (long)((ulong)xx[10]);
			long num4 = (long)((ulong)xx[11]);
			long num5 = (long)((ulong)xx[12]);
			long num6 = (long)((ulong)xx[13]);
			long num7 = (long)((ulong)xx[14]);
			long num8 = (long)((ulong)xx[15]);
			num -= 6L;
			long num9 = num + num2;
			long num10 = num2 + num3;
			long num11 = num3 + num4 - num8;
			long num12 = num4 + num5;
			long num13 = num5 + num6;
			long num14 = num6 + num7;
			long num15 = num7 + num8;
			long num16 = num14 - num9;
			long num17 = 0L;
			num17 += (long)((ulong)xx[0] - (ulong)num12 - (ulong)num16);
			z[0] = (uint)num17;
			num17 >>= 32;
			num17 += (long)((ulong)xx[1] + (ulong)num10 - (ulong)num13 - (ulong)num15);
			z[1] = (uint)num17;
			num17 >>= 32;
			num17 += (long)((ulong)xx[2] + (ulong)num11 - (ulong)num14);
			z[2] = (uint)num17;
			num17 >>= 32;
			num17 += (long)((ulong)xx[3] + (ulong)((ulong)num12 << 1) + (ulong)num16 - (ulong)num15);
			z[3] = (uint)num17;
			num17 >>= 32;
			num17 += (long)((ulong)xx[4] + (ulong)((ulong)num13 << 1) + (ulong)num7 - (ulong)num10);
			z[4] = (uint)num17;
			num17 >>= 32;
			num17 += (long)((ulong)xx[5] + (ulong)((ulong)num14 << 1) - (ulong)num11);
			z[5] = (uint)num17;
			num17 >>= 32;
			num17 += (long)((ulong)xx[6] + (ulong)((ulong)num15 << 1) + (ulong)num16);
			z[6] = (uint)num17;
			num17 >>= 32;
			num17 += (long)((ulong)xx[7] + (ulong)((ulong)num8 << 1) + (ulong)num - (ulong)num11 - (ulong)num13);
			z[7] = (uint)num17;
			num17 >>= 32;
			num17 += 6L;
			SecP256R1Field.Reduce32((uint)num17, z);
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x000F9BD4 File Offset: 0x000F7DD4
		public static void Reduce32(uint x, uint[] z)
		{
			long num = 0L;
			if (x != 0U)
			{
				long num2 = (long)((ulong)x);
				num += (long)((ulong)z[0] + (ulong)num2);
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
				num += (long)((ulong)z[3] - (ulong)num2);
				z[3] = (uint)num;
				num >>= 32;
				if (num != 0L)
				{
					num += (long)((ulong)z[4]);
					z[4] = (uint)num;
					num >>= 32;
					num += (long)((ulong)z[5]);
					z[5] = (uint)num;
					num >>= 32;
				}
				num += (long)((ulong)z[6] - (ulong)num2);
				z[6] = (uint)num;
				num >>= 32;
				num += (long)((ulong)z[7] + (ulong)num2);
				z[7] = (uint)num;
				num >>= 32;
			}
			if (num != 0L || (z[7] == 4294967295U && Nat256.Gte(z, SecP256R1Field.P)))
			{
				SecP256R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x000F9CA0 File Offset: 0x000F7EA0
		public static void Square(uint[] x, uint[] z)
		{
			uint[] array = Nat256.CreateExt();
			Nat256.Square(x, array);
			SecP256R1Field.Reduce(array, z);
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x000F9CC4 File Offset: 0x000F7EC4
		public static void SquareN(uint[] x, int n, uint[] z)
		{
			uint[] array = Nat256.CreateExt();
			Nat256.Square(x, array);
			SecP256R1Field.Reduce(array, z);
			while (--n > 0)
			{
				Nat256.Square(z, array);
				SecP256R1Field.Reduce(array, z);
			}
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x000F9CFE File Offset: 0x000F7EFE
		public static void Subtract(uint[] x, uint[] y, uint[] z)
		{
			if (Nat256.Sub(x, y, z) != 0)
			{
				SecP256R1Field.SubPInvFrom(z);
			}
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x000F9D10 File Offset: 0x000F7F10
		public static void SubtractExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if (Nat.Sub(16, xx, yy, zz) != 0)
			{
				Nat.AddTo(16, SecP256R1Field.PExt, zz);
			}
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x000F9D2C File Offset: 0x000F7F2C
		public static void Twice(uint[] x, uint[] z)
		{
			if (Nat.ShiftUpBit(8, x, 0U, z) != 0U || (z[7] == 4294967295U && Nat256.Gte(z, SecP256R1Field.P)))
			{
				SecP256R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x000F9D54 File Offset: 0x000F7F54
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
				num += (long)((ulong)z[2]);
				z[2] = (uint)num;
				num >>= 32;
			}
			num += (long)((ulong)z[3] - 1UL);
			z[3] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				num += (long)((ulong)z[4]);
				z[4] = (uint)num;
				num >>= 32;
				num += (long)((ulong)z[5]);
				z[5] = (uint)num;
				num >>= 32;
			}
			num += (long)((ulong)z[6] - 1UL);
			z[6] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[7] + 1UL);
			z[7] = (uint)num;
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x000F9DF4 File Offset: 0x000F7FF4
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
				num += (long)((ulong)z[2]);
				z[2] = (uint)num;
				num >>= 32;
			}
			num += (long)((ulong)z[3] + 1UL);
			z[3] = (uint)num;
			num >>= 32;
			if (num != 0L)
			{
				num += (long)((ulong)z[4]);
				z[4] = (uint)num;
				num >>= 32;
				num += (long)((ulong)z[5]);
				z[5] = (uint)num;
				num >>= 32;
			}
			num += (long)((ulong)z[6] + 1UL);
			z[6] = (uint)num;
			num >>= 32;
			num += (long)((ulong)z[7] - 1UL);
			z[7] = (uint)num;
		}

		// Token: 0x04001923 RID: 6435
		internal static readonly uint[] P = new uint[]
		{
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			0U,
			0U,
			0U,
			1U,
			uint.MaxValue
		};

		// Token: 0x04001924 RID: 6436
		internal static readonly uint[] PExt = new uint[]
		{
			1U,
			0U,
			0U,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			4294967294U,
			1U,
			4294967294U,
			1U,
			4294967294U,
			1U,
			1U,
			4294967294U,
			2U,
			4294967294U
		};

		// Token: 0x04001925 RID: 6437
		internal const uint P7 = 4294967295U;

		// Token: 0x04001926 RID: 6438
		internal const uint PExt15 = 4294967294U;
	}
}
