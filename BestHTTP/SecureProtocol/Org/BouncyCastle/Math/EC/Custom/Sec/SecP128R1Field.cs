using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Custom.Sec
{
	// Token: 0x0200033E RID: 830
	internal class SecP128R1Field
	{
		// Token: 0x06002038 RID: 8248 RVA: 0x000F1EC5 File Offset: 0x000F00C5
		public static void Add(uint[] x, uint[] y, uint[] z)
		{
			if (Nat128.Add(x, y, z) != 0U || (z[3] >= 4294967293U && Nat128.Gte(z, SecP128R1Field.P)))
			{
				SecP128R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x000F1EEB File Offset: 0x000F00EB
		public static void AddExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if (Nat256.Add(xx, yy, zz) != 0U || (zz[7] >= 4294967292U && Nat256.Gte(zz, SecP128R1Field.PExt)))
			{
				Nat.AddTo(SecP128R1Field.PExtInv.Length, SecP128R1Field.PExtInv, zz);
			}
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x000F1F1E File Offset: 0x000F011E
		public static void AddOne(uint[] x, uint[] z)
		{
			if (Nat.Inc(4, x, z) != 0U || (z[3] >= 4294967293U && Nat128.Gte(z, SecP128R1Field.P)))
			{
				SecP128R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x000F1F44 File Offset: 0x000F0144
		public static uint[] FromBigInteger(BigInteger x)
		{
			uint[] array = Nat128.FromBigInteger(x);
			if (array[3] >= 4294967293U && Nat128.Gte(array, SecP128R1Field.P))
			{
				Nat128.SubFrom(SecP128R1Field.P, array);
			}
			return array;
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x000F1F7C File Offset: 0x000F017C
		public static void Half(uint[] x, uint[] z)
		{
			if ((x[0] & 1U) == 0U)
			{
				Nat.ShiftDownBit(4, x, 0U, z);
				return;
			}
			uint c = Nat128.Add(x, SecP128R1Field.P, z);
			Nat.ShiftDownBit(4, z, c);
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x000F1FB4 File Offset: 0x000F01B4
		public static void Multiply(uint[] x, uint[] y, uint[] z)
		{
			uint[] array = Nat128.CreateExt();
			Nat128.Mul(x, y, array);
			SecP128R1Field.Reduce(array, z);
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x000F1FD6 File Offset: 0x000F01D6
		public static void MultiplyAddToExt(uint[] x, uint[] y, uint[] zz)
		{
			if (Nat128.MulAddTo(x, y, zz) != 0U || (zz[7] >= 4294967292U && Nat256.Gte(zz, SecP128R1Field.PExt)))
			{
				Nat.AddTo(SecP128R1Field.PExtInv.Length, SecP128R1Field.PExtInv, zz);
			}
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x000F2009 File Offset: 0x000F0209
		public static void Negate(uint[] x, uint[] z)
		{
			if (Nat128.IsZero(x))
			{
				Nat128.Zero(z);
				return;
			}
			Nat128.Sub(SecP128R1Field.P, x, z);
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x000F2028 File Offset: 0x000F0228
		public static void Reduce(uint[] xx, uint[] z)
		{
			ulong num = (ulong)xx[0];
			ulong num2 = (ulong)xx[1];
			ulong num3 = (ulong)xx[2];
			ulong num4 = (ulong)xx[3];
			ulong num5 = (ulong)xx[4];
			ulong num6 = (ulong)xx[5];
			ulong num7 = (ulong)xx[6];
			ulong num8 = (ulong)xx[7];
			num4 += num8;
			num7 += num8 << 1;
			num3 += num7;
			num6 += num7 << 1;
			num2 += num6;
			num5 += num6 << 1;
			num += num5;
			num4 += num5 << 1;
			z[0] = (uint)num;
			num2 += num >> 32;
			z[1] = (uint)num2;
			num3 += num2 >> 32;
			z[2] = (uint)num3;
			num4 += num3 >> 32;
			z[3] = (uint)num4;
			SecP128R1Field.Reduce32((uint)(num4 >> 32), z);
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x000F20CC File Offset: 0x000F02CC
		public static void Reduce32(uint x, uint[] z)
		{
			while (x != 0U)
			{
				ulong num = (ulong)x;
				ulong num2 = (ulong)z[0] + num;
				z[0] = (uint)num2;
				num2 >>= 32;
				if (num2 != 0UL)
				{
					num2 += (ulong)z[1];
					z[1] = (uint)num2;
					num2 >>= 32;
					num2 += (ulong)z[2];
					z[2] = (uint)num2;
					num2 >>= 32;
				}
				num2 += (ulong)z[3] + (num << 1);
				z[3] = (uint)num2;
				num2 >>= 32;
				x = (uint)num2;
			}
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x000F2130 File Offset: 0x000F0330
		public static void Square(uint[] x, uint[] z)
		{
			uint[] array = Nat128.CreateExt();
			Nat128.Square(x, array);
			SecP128R1Field.Reduce(array, z);
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x000F2154 File Offset: 0x000F0354
		public static void SquareN(uint[] x, int n, uint[] z)
		{
			uint[] array = Nat128.CreateExt();
			Nat128.Square(x, array);
			SecP128R1Field.Reduce(array, z);
			while (--n > 0)
			{
				Nat128.Square(z, array);
				SecP128R1Field.Reduce(array, z);
			}
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x000F218E File Offset: 0x000F038E
		public static void Subtract(uint[] x, uint[] y, uint[] z)
		{
			if (Nat128.Sub(x, y, z) != 0)
			{
				SecP128R1Field.SubPInvFrom(z);
			}
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x000F21A0 File Offset: 0x000F03A0
		public static void SubtractExt(uint[] xx, uint[] yy, uint[] zz)
		{
			if (Nat.Sub(10, xx, yy, zz) != 0)
			{
				Nat.SubFrom(SecP128R1Field.PExtInv.Length, SecP128R1Field.PExtInv, zz);
			}
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x000F21C1 File Offset: 0x000F03C1
		public static void Twice(uint[] x, uint[] z)
		{
			if (Nat.ShiftUpBit(4, x, 0U, z) != 0U || (z[3] >= 4294967293U && Nat128.Gte(z, SecP128R1Field.P)))
			{
				SecP128R1Field.AddPInvTo(z);
			}
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x000F21E8 File Offset: 0x000F03E8
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
			num += (long)((ulong)z[3] + 2UL);
			z[3] = (uint)num;
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x000F223C File Offset: 0x000F043C
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
			num += (long)((ulong)z[3] - 2UL);
			z[3] = (uint)num;
		}

		// Token: 0x040018C1 RID: 6337
		internal static readonly uint[] P = new uint[]
		{
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			4294967293U
		};

		// Token: 0x040018C2 RID: 6338
		internal static readonly uint[] PExt = new uint[]
		{
			1U,
			0U,
			0U,
			4U,
			4294967294U,
			uint.MaxValue,
			3U,
			4294967292U
		};

		// Token: 0x040018C3 RID: 6339
		private static readonly uint[] PExtInv = new uint[]
		{
			uint.MaxValue,
			uint.MaxValue,
			uint.MaxValue,
			4294967291U,
			1U,
			0U,
			4294967292U,
			3U
		};

		// Token: 0x040018C4 RID: 6340
		private const uint P3 = 4294967293U;

		// Token: 0x040018C5 RID: 6341
		private const uint PExt7 = 4294967292U;
	}
}
