using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw
{
	// Token: 0x020002FD RID: 765
	internal abstract class Nat576
	{
		// Token: 0x06001D6E RID: 7534 RVA: 0x000E25B6 File Offset: 0x000E07B6
		public static void Copy64(ulong[] x, ulong[] z)
		{
			z[0] = x[0];
			z[1] = x[1];
			z[2] = x[2];
			z[3] = x[3];
			z[4] = x[4];
			z[5] = x[5];
			z[6] = x[6];
			z[7] = x[7];
			z[8] = x[8];
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x000E25F0 File Offset: 0x000E07F0
		public static void Copy64(ulong[] x, int xOff, ulong[] z, int zOff)
		{
			z[zOff] = x[xOff];
			z[zOff + 1] = x[xOff + 1];
			z[zOff + 2] = x[xOff + 2];
			z[zOff + 3] = x[xOff + 3];
			z[zOff + 4] = x[xOff + 4];
			z[zOff + 5] = x[xOff + 5];
			z[zOff + 6] = x[xOff + 6];
			z[zOff + 7] = x[xOff + 7];
			z[zOff + 8] = x[xOff + 8];
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x000E2653 File Offset: 0x000E0853
		public static ulong[] Create64()
		{
			return new ulong[9];
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x000E265C File Offset: 0x000E085C
		public static ulong[] CreateExt64()
		{
			return new ulong[18];
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x000E2668 File Offset: 0x000E0868
		public static bool Eq64(ulong[] x, ulong[] y)
		{
			for (int i = 8; i >= 0; i--)
			{
				if (x[i] != y[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x000E268C File Offset: 0x000E088C
		public static ulong[] FromBigInteger64(BigInteger x)
		{
			if (x.SignValue < 0 || x.BitLength > 576)
			{
				throw new ArgumentException();
			}
			ulong[] array = Nat576.Create64();
			int num = 0;
			while (x.SignValue != 0)
			{
				array[num++] = (ulong)x.LongValue;
				x = x.ShiftRight(64);
			}
			return array;
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x000E26E0 File Offset: 0x000E08E0
		public static bool IsOne64(ulong[] x)
		{
			if (x[0] != 1UL)
			{
				return false;
			}
			for (int i = 1; i < 9; i++)
			{
				if (x[i] != 0UL)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x000E270C File Offset: 0x000E090C
		public static bool IsZero64(ulong[] x)
		{
			for (int i = 0; i < 9; i++)
			{
				if (x[i] != 0UL)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x000E2730 File Offset: 0x000E0930
		public static BigInteger ToBigInteger64(ulong[] x)
		{
			byte[] array = new byte[72];
			for (int i = 0; i < 9; i++)
			{
				ulong num = x[i];
				if (num != 0UL)
				{
					Pack.UInt64_To_BE(num, array, 8 - i << 3);
				}
			}
			return new BigInteger(1, array);
		}
	}
}
