﻿using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw
{
	// Token: 0x020002FB RID: 763
	internal abstract class Nat448
	{
		// Token: 0x06001D61 RID: 7521 RVA: 0x000E22F6 File Offset: 0x000E04F6
		public static void Copy64(ulong[] x, ulong[] z)
		{
			z[0] = x[0];
			z[1] = x[1];
			z[2] = x[2];
			z[3] = x[3];
			z[4] = x[4];
			z[5] = x[5];
			z[6] = x[6];
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x000E2324 File Offset: 0x000E0524
		public static void Copy64(ulong[] x, int xOff, ulong[] z, int zOff)
		{
			z[zOff] = x[xOff];
			z[zOff + 1] = x[xOff + 1];
			z[zOff + 2] = x[xOff + 2];
			z[zOff + 3] = x[xOff + 3];
			z[zOff + 4] = x[xOff + 4];
			z[zOff + 5] = x[xOff + 5];
			z[zOff + 6] = x[xOff + 6];
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x000E2373 File Offset: 0x000E0573
		public static ulong[] Create64()
		{
			return new ulong[7];
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x000E237B File Offset: 0x000E057B
		public static ulong[] CreateExt64()
		{
			return new ulong[14];
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x000E2384 File Offset: 0x000E0584
		public static bool Eq64(ulong[] x, ulong[] y)
		{
			for (int i = 6; i >= 0; i--)
			{
				if (x[i] != y[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x000E23A8 File Offset: 0x000E05A8
		public static ulong[] FromBigInteger64(BigInteger x)
		{
			if (x.SignValue < 0 || x.BitLength > 448)
			{
				throw new ArgumentException();
			}
			ulong[] array = Nat448.Create64();
			int num = 0;
			while (x.SignValue != 0)
			{
				array[num++] = (ulong)x.LongValue;
				x = x.ShiftRight(64);
			}
			return array;
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x000E23FC File Offset: 0x000E05FC
		public static bool IsOne64(ulong[] x)
		{
			if (x[0] != 1UL)
			{
				return false;
			}
			for (int i = 1; i < 7; i++)
			{
				if (x[i] != 0UL)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x000E2428 File Offset: 0x000E0628
		public static bool IsZero64(ulong[] x)
		{
			for (int i = 0; i < 7; i++)
			{
				if (x[i] != 0UL)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x000E244C File Offset: 0x000E064C
		public static BigInteger ToBigInteger64(ulong[] x)
		{
			byte[] array = new byte[56];
			for (int i = 0; i < 7; i++)
			{
				ulong num = x[i];
				if (num != 0UL)
				{
					Pack.UInt64_To_BE(num, array, 6 - i << 3);
				}
			}
			return new BigInteger(1, array);
		}
	}
}
