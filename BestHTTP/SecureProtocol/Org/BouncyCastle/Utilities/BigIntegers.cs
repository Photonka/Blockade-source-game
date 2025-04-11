using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities
{
	// Token: 0x0200024B RID: 587
	public abstract class BigIntegers
	{
		// Token: 0x060015EC RID: 5612 RVA: 0x000B2164 File Offset: 0x000B0364
		public static byte[] AsUnsignedByteArray(BigInteger n)
		{
			return n.ToByteArrayUnsigned();
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x000B216C File Offset: 0x000B036C
		public static byte[] AsUnsignedByteArray(int length, BigInteger n)
		{
			byte[] array = n.ToByteArrayUnsigned();
			if (array.Length > length)
			{
				throw new ArgumentException("standard length exceeded", "n");
			}
			if (array.Length == length)
			{
				return array;
			}
			byte[] array2 = new byte[length];
			Array.Copy(array, 0, array2, array2.Length - array.Length, array.Length);
			return array2;
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x000B21B8 File Offset: 0x000B03B8
		public static BigInteger CreateRandomInRange(BigInteger min, BigInteger max, SecureRandom random)
		{
			int num = min.CompareTo(max);
			if (num >= 0)
			{
				if (num > 0)
				{
					throw new ArgumentException("'min' may not be greater than 'max'");
				}
				return min;
			}
			else
			{
				if (min.BitLength > max.BitLength / 2)
				{
					return BigIntegers.CreateRandomInRange(BigInteger.Zero, max.Subtract(min), random).Add(min);
				}
				for (int i = 0; i < 1000; i++)
				{
					BigInteger bigInteger = new BigInteger(max.BitLength, random);
					if (bigInteger.CompareTo(min) >= 0 && bigInteger.CompareTo(max) <= 0)
					{
						return bigInteger;
					}
				}
				return new BigInteger(max.Subtract(min).BitLength - 1, random).Add(min);
			}
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x000B2258 File Offset: 0x000B0458
		public static int GetUnsignedByteLength(BigInteger n)
		{
			return (n.BitLength + 7) / 8;
		}

		// Token: 0x04001549 RID: 5449
		private const int MaxIterations = 1000;
	}
}
