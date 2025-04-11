using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw
{
	// Token: 0x020002F9 RID: 761
	internal abstract class Nat320
	{
		// Token: 0x06001D54 RID: 7508 RVA: 0x000E2061 File Offset: 0x000E0261
		public static void Copy64(ulong[] x, ulong[] z)
		{
			z[0] = x[0];
			z[1] = x[1];
			z[2] = x[2];
			z[3] = x[3];
			z[4] = x[4];
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x000E2081 File Offset: 0x000E0281
		public static void Copy64(ulong[] x, int xOff, ulong[] z, int zOff)
		{
			z[zOff] = x[xOff];
			z[zOff + 1] = x[xOff + 1];
			z[zOff + 2] = x[xOff + 2];
			z[zOff + 3] = x[xOff + 3];
			z[zOff + 4] = x[xOff + 4];
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x000E20B1 File Offset: 0x000E02B1
		public static ulong[] Create64()
		{
			return new ulong[5];
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x000E20B9 File Offset: 0x000E02B9
		public static ulong[] CreateExt64()
		{
			return new ulong[10];
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x000E20C4 File Offset: 0x000E02C4
		public static bool Eq64(ulong[] x, ulong[] y)
		{
			for (int i = 4; i >= 0; i--)
			{
				if (x[i] != y[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x000E20E8 File Offset: 0x000E02E8
		public static ulong[] FromBigInteger64(BigInteger x)
		{
			if (x.SignValue < 0 || x.BitLength > 320)
			{
				throw new ArgumentException();
			}
			ulong[] array = Nat320.Create64();
			int num = 0;
			while (x.SignValue != 0)
			{
				array[num++] = (ulong)x.LongValue;
				x = x.ShiftRight(64);
			}
			return array;
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x000E213C File Offset: 0x000E033C
		public static bool IsOne64(ulong[] x)
		{
			if (x[0] != 1UL)
			{
				return false;
			}
			for (int i = 1; i < 5; i++)
			{
				if (x[i] != 0UL)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x000E2168 File Offset: 0x000E0368
		public static bool IsZero64(ulong[] x)
		{
			for (int i = 0; i < 5; i++)
			{
				if (x[i] != 0UL)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x000E218C File Offset: 0x000E038C
		public static BigInteger ToBigInteger64(ulong[] x)
		{
			byte[] array = new byte[40];
			for (int i = 0; i < 5; i++)
			{
				ulong num = x[i];
				if (num != 0UL)
				{
					Pack.UInt64_To_BE(num, array, 4 - i << 3);
				}
			}
			return new BigInteger(1, array);
		}
	}
}
