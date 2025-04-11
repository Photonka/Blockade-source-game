using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.GM
{
	// Token: 0x020003A2 RID: 930
	internal class SM2P256V1Field
	{
		// Token: 0x06002693 RID: 9875 RVA: 0x0010A675 File Offset: 0x00108875
		public static void Add(uint[] x, uint[] y, uint[] z)
		{
			if (Nat256.Add(x, y, z) != 0U || (z[7] >= 4294967294U && Nat256.Gte(z, SM2P256V1Field.P)))
			{
				SM2P256V1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x0010A69B File Offset: 0x0010889B
		public static void AddExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if (Nat.Add(16, xx, yy, zz) != 0U || (zz[15] >= 4294967294U && Nat.Gte(16, zz, SM2P256V1Field.PExt)))
			{
				Nat.SubFrom(16, SM2P256V1Field.PExt, zz);
			}
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x0010A6CE File Offset: 0x001088CE
		public static void AddOne(uint[] x, uint[] z)
		{
			if (Nat.Inc(8, x, z) != 0U || (z[7] >= 4294967294U && Nat256.Gte(z, SM2P256V1Field.P)))
			{
				SM2P256V1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x0010A6F4 File Offset: 0x001088F4
		public static uint[] FromBigInteger(BigInteger x)
		{
			uint[] array = Nat256.FromBigInteger(x);
			if (array[7] >= 4294967294U && Nat256.Gte(array, SM2P256V1Field.P))
			{
				Nat256.SubFrom(SM2P256V1Field.P, array);
			}
			return array;
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x0010A72C File Offset: 0x0010892C
		public static void Half(uint[] x, uint[] z)
		{
			if ((x[0] & 1U) == 0U)
			{
				Nat.ShiftDownBit(8, x, 0U, z);
				return;
			}
			uint c = Nat256.Add(x, SM2P256V1Field.P, z);
			Nat.ShiftDownBit(8, z, c);
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x0010A764 File Offset: 0x00108964
		public static void Multiply(uint[] x, uint[] y, uint[] z)
		{
			uint[] array = Nat256.CreateExt();
			Nat256.Mul(x, y, array);
			SM2P256V1Field.Reduce(array, z);
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x0010A786 File Offset: 0x00108986
		public static void MultiplyAddToExt(uint[] x, uint[] y, uint[] zz)
		{
			if (Nat256.MulAddTo(x, y, zz) != 0U || (zz[15] >= 4294967294U && Nat.Gte(16, zz, SM2P256V1Field.PExt)))
			{
				Nat.SubFrom(16, SM2P256V1Field.PExt, zz);
			}
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x0010A7B7 File Offset: 0x001089B7
		public static void Negate(uint[] x, uint[] z)
		{
			if (Nat256.IsZero(x))
			{
				Nat256.Zero(z);
				return;
			}
			Nat256.Sub(SM2P256V1Field.P, x, z);
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x0010A7D8 File Offset: 0x001089D8
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
			long num9 = num + num2;
			long num10 = num3 + num4;
			long num11 = num5 + num8;
			long num12 = num6 + num7;
			long num13 = num12 + (num8 << 1);
			long num14 = num9 + num12;
			long num15 = num10 + num11 + num14;
			long num16 = 0L;
			num16 += (long)((ulong)xx[0] + (ulong)num15 + (ulong)num6 + (ulong)num7 + (ulong)num8);
			z[0] = (uint)num16;
			num16 >>= 32;
			num16 += (long)((ulong)xx[1] + (ulong)num15 - (ulong)num + (ulong)num7 + (ulong)num8);
			z[1] = (uint)num16;
			num16 >>= 32;
			num16 += (long)((ulong)xx[2] - (ulong)num14);
			z[2] = (uint)num16;
			num16 >>= 32;
			num16 += (long)((ulong)xx[3] + (ulong)num15 - (ulong)num2 - (ulong)num3 + (ulong)num6);
			z[3] = (uint)num16;
			num16 >>= 32;
			num16 += (long)((ulong)xx[4] + (ulong)num15 - (ulong)num10 - (ulong)num + (ulong)num7);
			z[4] = (uint)num16;
			num16 >>= 32;
			num16 += (long)((ulong)xx[5] + (ulong)num13 + (ulong)num3);
			z[5] = (uint)num16;
			num16 >>= 32;
			num16 += (long)((ulong)xx[6] + (ulong)num4 + (ulong)num7 + (ulong)num8);
			z[6] = (uint)num16;
			num16 >>= 32;
			num16 += (long)((ulong)xx[7] + (ulong)num15 + (ulong)num13 + (ulong)num5);
			z[7] = (uint)num16;
			num16 >>= 32;
			SM2P256V1Field.Reduce32((uint)num16, z);
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x0010A948 File Offset: 0x00108B48
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
				}
				num += (long)((ulong)z[2] - (ulong)num2);
				z[2] = (uint)num;
				num >>= 32;
				num += (long)((ulong)z[3] + (ulong)num2);
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
					num += (long)((ulong)z[6]);
					z[6] = (uint)num;
					num >>= 32;
				}
				num += (long)((ulong)z[7] + (ulong)num2);
				z[7] = (uint)num;
				num >>= 32;
			}
			if (num != 0L || (z[7] >= 4294967294U && Nat256.Gte(z, SM2P256V1Field.P)))
			{
				SM2P256V1Field.AddPInvTo(z);
			}
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x0010AA14 File Offset: 0x00108C14
		public static void Square(uint[] x, uint[] z)
		{
			uint[] array = Nat256.CreateExt();
			Nat256.Square(x, array);
			SM2P256V1Field.Reduce(array, z);
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x0010AA38 File Offset: 0x00108C38
		public static void SquareN(uint[] x, int n, uint[] z)
		{
			uint[] array = Nat256.CreateExt();
			Nat256.Square(x, array);
			SM2P256V1Field.Reduce(array, z);
			while (--n > 0)
			{
				Nat256.Square(z, array);
				SM2P256V1Field.Reduce(array, z);
			}
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x0010AA72 File Offset: 0x00108C72
		public static void Subtract(uint[] x, uint[] y, uint[] z)
		{
			if (Nat256.Sub(x, y, z) != 0)
			{
				SM2P256V1Field.SubPInvFrom(z);
			}
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x0010AA84 File Offset: 0x00108C84
		public static void SubtractExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if (Nat.Sub(16, xx, yy, zz) != 0)
			{
				Nat.AddTo(16, SM2P256V1Field.PExt, zz);
			}
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x0010AAA0 File Offset: 0x00108CA0
		public static void Twice(uint[] x, uint[] z)
		{
			if (Nat.ShiftUpBit(8, x, 0U, z) != 0U || (z[7] >= 4294967294U && Nat256.Gte(z, SM2P256V1Field.P)))
			{
				SM2P256V1Field.AddPInvTo(z);
			}
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x0010AAC8 File Offset: 0x00108CC8
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
			num += (long)((ulong)z[2] - 1UL);
			z[2] = (uint)num;
			num >>= 32;
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
				num += (long)((ulong)z[6]);
				z[6] = (uint)num;
				num >>= 32;
			}
			num += (long)((ulong)z[7] + 1UL);
			z[7] = (uint)num;
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x0010AB68 File Offset: 0x00108D68
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
			num += (long)((ulong)z[2] + 1UL);
			z[2] = (uint)num;
			num >>= 32;
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
				num += (long)((ulong)z[6]);
				z[6] = (uint)num;
				num >>= 32;
			}
			num += (long)((ulong)z[7] - 1UL);
			z[7] = (uint)num;
		}

		// Token: 0x04001997 RID: 6551
		internal static readonly uint[] P = new uint[]
		{
			uint.MaxValue,
			uint.MaxValue,
			0U,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			4294967294U
		};

		// Token: 0x04001998 RID: 6552
		internal static readonly uint[] PExt = new uint[]
		{
			1U,
			0U,
			4294967294U,
			1U,
			1U,
			4294967294U,
			0U,
			2U,
			4294967294U,
			4294967293U,
			3U,
			4294967294U,
			uint.MaxValue,
			uint.MaxValue,
			0U,
			4294967294U
		};

		// Token: 0x04001999 RID: 6553
		internal const uint P7 = 4294967294U;

		// Token: 0x0400199A RID: 6554
		internal const uint PExt15 = 4294967294U;
	}
}
