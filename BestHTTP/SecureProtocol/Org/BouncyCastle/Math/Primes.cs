using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math
{
	// Token: 0x020002F0 RID: 752
	public abstract class Primes
	{
		// Token: 0x06001BF3 RID: 7155 RVA: 0x000D6FD8 File Offset: 0x000D51D8
		public static Primes.STOutput GenerateSTRandomPrime(IDigest hash, int length, byte[] inputSeed)
		{
			if (hash == null)
			{
				throw new ArgumentNullException("hash");
			}
			if (length < 2)
			{
				throw new ArgumentException("must be >= 2", "length");
			}
			if (inputSeed == null)
			{
				throw new ArgumentNullException("inputSeed");
			}
			if (inputSeed.Length == 0)
			{
				throw new ArgumentException("cannot be empty", "inputSeed");
			}
			return Primes.ImplSTRandomPrime(hash, length, Arrays.Clone(inputSeed));
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x000D7038 File Offset: 0x000D5238
		public static Primes.MROutput EnhancedMRProbablePrimeTest(BigInteger candidate, SecureRandom random, int iterations)
		{
			Primes.CheckCandidate(candidate, "candidate");
			if (random == null)
			{
				throw new ArgumentNullException("random");
			}
			if (iterations < 1)
			{
				throw new ArgumentException("must be > 0", "iterations");
			}
			if (candidate.BitLength == 2)
			{
				return Primes.MROutput.ProbablyPrime();
			}
			if (!candidate.TestBit(0))
			{
				return Primes.MROutput.ProvablyCompositeWithFactor(Primes.Two);
			}
			BigInteger bigInteger = candidate.Subtract(Primes.One);
			BigInteger max = candidate.Subtract(Primes.Two);
			int lowestSetBit = bigInteger.GetLowestSetBit();
			BigInteger e = bigInteger.ShiftRight(lowestSetBit);
			for (int i = 0; i < iterations; i++)
			{
				BigInteger bigInteger2 = BigIntegers.CreateRandomInRange(Primes.Two, max, random);
				BigInteger bigInteger3 = bigInteger2.Gcd(candidate);
				if (bigInteger3.CompareTo(Primes.One) > 0)
				{
					return Primes.MROutput.ProvablyCompositeWithFactor(bigInteger3);
				}
				BigInteger bigInteger4 = bigInteger2.ModPow(e, candidate);
				if (!bigInteger4.Equals(Primes.One) && !bigInteger4.Equals(bigInteger))
				{
					bool flag = false;
					BigInteger bigInteger5 = bigInteger4;
					for (int j = 1; j < lowestSetBit; j++)
					{
						bigInteger4 = bigInteger4.ModPow(Primes.Two, candidate);
						if (bigInteger4.Equals(bigInteger))
						{
							flag = true;
							break;
						}
						if (bigInteger4.Equals(Primes.One))
						{
							break;
						}
						bigInteger5 = bigInteger4;
					}
					if (!flag)
					{
						if (!bigInteger4.Equals(Primes.One))
						{
							bigInteger5 = bigInteger4;
							bigInteger4 = bigInteger4.ModPow(Primes.Two, candidate);
							if (!bigInteger4.Equals(Primes.One))
							{
								bigInteger5 = bigInteger4;
							}
						}
						bigInteger3 = bigInteger5.Subtract(Primes.One).Gcd(candidate);
						if (bigInteger3.CompareTo(Primes.One) > 0)
						{
							return Primes.MROutput.ProvablyCompositeWithFactor(bigInteger3);
						}
						return Primes.MROutput.ProvablyCompositeNotPrimePower();
					}
				}
			}
			return Primes.MROutput.ProbablyPrime();
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x000D71E2 File Offset: 0x000D53E2
		public static bool HasAnySmallFactors(BigInteger candidate)
		{
			Primes.CheckCandidate(candidate, "candidate");
			return Primes.ImplHasAnySmallFactors(candidate);
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x000D71F8 File Offset: 0x000D53F8
		public static bool IsMRProbablePrime(BigInteger candidate, SecureRandom random, int iterations)
		{
			Primes.CheckCandidate(candidate, "candidate");
			if (random == null)
			{
				throw new ArgumentException("cannot be null", "random");
			}
			if (iterations < 1)
			{
				throw new ArgumentException("must be > 0", "iterations");
			}
			if (candidate.BitLength == 2)
			{
				return true;
			}
			if (!candidate.TestBit(0))
			{
				return false;
			}
			BigInteger bigInteger = candidate.Subtract(Primes.One);
			BigInteger max = candidate.Subtract(Primes.Two);
			int lowestSetBit = bigInteger.GetLowestSetBit();
			BigInteger m = bigInteger.ShiftRight(lowestSetBit);
			for (int i = 0; i < iterations; i++)
			{
				BigInteger b = BigIntegers.CreateRandomInRange(Primes.Two, max, random);
				if (!Primes.ImplMRProbablePrimeToBase(candidate, bigInteger, m, lowestSetBit, b))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x000D72A8 File Offset: 0x000D54A8
		public static bool IsMRProbablePrimeToBase(BigInteger candidate, BigInteger baseValue)
		{
			Primes.CheckCandidate(candidate, "candidate");
			Primes.CheckCandidate(baseValue, "baseValue");
			if (baseValue.CompareTo(candidate.Subtract(Primes.One)) >= 0)
			{
				throw new ArgumentException("must be < ('candidate' - 1)", "baseValue");
			}
			if (candidate.BitLength == 2)
			{
				return true;
			}
			BigInteger bigInteger = candidate.Subtract(Primes.One);
			int lowestSetBit = bigInteger.GetLowestSetBit();
			BigInteger m = bigInteger.ShiftRight(lowestSetBit);
			return Primes.ImplMRProbablePrimeToBase(candidate, bigInteger, m, lowestSetBit, baseValue);
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x000D731F File Offset: 0x000D551F
		private static void CheckCandidate(BigInteger n, string name)
		{
			if (n == null || n.SignValue < 1 || n.BitLength < 2)
			{
				throw new ArgumentException("must be non-null and >= 2", name);
			}
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x000D7344 File Offset: 0x000D5544
		private static bool ImplHasAnySmallFactors(BigInteger x)
		{
			int num = 223092870;
			int intValue = x.Mod(BigInteger.ValueOf((long)num)).IntValue;
			if (intValue % 2 == 0 || intValue % 3 == 0 || intValue % 5 == 0 || intValue % 7 == 0 || intValue % 11 == 0 || intValue % 13 == 0 || intValue % 17 == 0 || intValue % 19 == 0 || intValue % 23 == 0)
			{
				return true;
			}
			num = 58642669;
			intValue = x.Mod(BigInteger.ValueOf((long)num)).IntValue;
			if (intValue % 29 == 0 || intValue % 31 == 0 || intValue % 37 == 0 || intValue % 41 == 0 || intValue % 43 == 0)
			{
				return true;
			}
			num = 600662303;
			intValue = x.Mod(BigInteger.ValueOf((long)num)).IntValue;
			if (intValue % 47 == 0 || intValue % 53 == 0 || intValue % 59 == 0 || intValue % 61 == 0 || intValue % 67 == 0)
			{
				return true;
			}
			num = 33984931;
			intValue = x.Mod(BigInteger.ValueOf((long)num)).IntValue;
			if (intValue % 71 == 0 || intValue % 73 == 0 || intValue % 79 == 0 || intValue % 83 == 0)
			{
				return true;
			}
			num = 89809099;
			intValue = x.Mod(BigInteger.ValueOf((long)num)).IntValue;
			if (intValue % 89 == 0 || intValue % 97 == 0 || intValue % 101 == 0 || intValue % 103 == 0)
			{
				return true;
			}
			num = 167375713;
			intValue = x.Mod(BigInteger.ValueOf((long)num)).IntValue;
			if (intValue % 107 == 0 || intValue % 109 == 0 || intValue % 113 == 0 || intValue % 127 == 0)
			{
				return true;
			}
			num = 371700317;
			intValue = x.Mod(BigInteger.ValueOf((long)num)).IntValue;
			if (intValue % 131 == 0 || intValue % 137 == 0 || intValue % 139 == 0 || intValue % 149 == 0)
			{
				return true;
			}
			num = 645328247;
			intValue = x.Mod(BigInteger.ValueOf((long)num)).IntValue;
			if (intValue % 151 == 0 || intValue % 157 == 0 || intValue % 163 == 0 || intValue % 167 == 0)
			{
				return true;
			}
			num = 1070560157;
			intValue = x.Mod(BigInteger.ValueOf((long)num)).IntValue;
			if (intValue % 173 == 0 || intValue % 179 == 0 || intValue % 181 == 0 || intValue % 191 == 0)
			{
				return true;
			}
			num = 1596463769;
			intValue = x.Mod(BigInteger.ValueOf((long)num)).IntValue;
			return intValue % 193 == 0 || intValue % 197 == 0 || intValue % 199 == 0 || intValue % 211 == 0;
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x000D75A8 File Offset: 0x000D57A8
		private static bool ImplMRProbablePrimeToBase(BigInteger w, BigInteger wSubOne, BigInteger m, int a, BigInteger b)
		{
			BigInteger bigInteger = b.ModPow(m, w);
			if (bigInteger.Equals(Primes.One) || bigInteger.Equals(wSubOne))
			{
				return true;
			}
			bool result = false;
			for (int i = 1; i < a; i++)
			{
				bigInteger = bigInteger.ModPow(Primes.Two, w);
				if (bigInteger.Equals(wSubOne))
				{
					result = true;
					break;
				}
				if (bigInteger.Equals(Primes.One))
				{
					return false;
				}
			}
			return result;
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x000D7610 File Offset: 0x000D5810
		private static Primes.STOutput ImplSTRandomPrime(IDigest d, int length, byte[] primeSeed)
		{
			int digestSize = d.GetDigestSize();
			if (length < 33)
			{
				int num = 0;
				byte[] array = new byte[digestSize];
				byte[] array2 = new byte[digestSize];
				uint num2;
				for (;;)
				{
					Primes.Hash(d, primeSeed, array, 0);
					Primes.Inc(primeSeed, 1);
					Primes.Hash(d, primeSeed, array2, 0);
					Primes.Inc(primeSeed, 1);
					num2 = (Primes.Extract32(array) ^ Primes.Extract32(array2));
					num2 &= uint.MaxValue >> 32 - length;
					num2 |= (1U << length - 1 | 1U);
					num++;
					if (Primes.IsPrime32(num2))
					{
						break;
					}
					if (num > 4 * length)
					{
						goto Block_3;
					}
				}
				return new Primes.STOutput(BigInteger.ValueOf((long)((ulong)num2)), primeSeed, num);
				Block_3:
				throw new InvalidOperationException("Too many iterations in Shawe-Taylor Random_Prime Routine");
			}
			Primes.STOutput stoutput = Primes.ImplSTRandomPrime(d, (length + 3) / 2, primeSeed);
			BigInteger prime = stoutput.Prime;
			primeSeed = stoutput.PrimeSeed;
			int num3 = stoutput.PrimeGenCounter;
			int num4 = 8 * digestSize;
			int num5 = (length - 1) / num4;
			int num6 = num3;
			BigInteger bigInteger = Primes.HashGen(d, primeSeed, num5 + 1).Mod(Primes.One.ShiftLeft(length - 1)).SetBit(length - 1);
			BigInteger bigInteger2 = prime.ShiftLeft(1);
			BigInteger bigInteger3 = bigInteger.Subtract(Primes.One).Divide(bigInteger2).Add(Primes.One).ShiftLeft(1);
			int num7 = 0;
			BigInteger bigInteger4 = bigInteger3.Multiply(prime).Add(Primes.One);
			for (;;)
			{
				if (bigInteger4.BitLength > length)
				{
					bigInteger3 = Primes.One.ShiftLeft(length - 1).Subtract(Primes.One).Divide(bigInteger2).Add(Primes.One).ShiftLeft(1);
					bigInteger4 = bigInteger3.Multiply(prime).Add(Primes.One);
				}
				num3++;
				if (!Primes.ImplHasAnySmallFactors(bigInteger4))
				{
					BigInteger bigInteger5 = Primes.HashGen(d, primeSeed, num5 + 1).Mod(bigInteger4.Subtract(Primes.Three)).Add(Primes.Two);
					bigInteger3 = bigInteger3.Add(BigInteger.ValueOf((long)num7));
					num7 = 0;
					BigInteger bigInteger6 = bigInteger5.ModPow(bigInteger3, bigInteger4);
					if (bigInteger4.Gcd(bigInteger6.Subtract(Primes.One)).Equals(Primes.One) && bigInteger6.ModPow(prime, bigInteger4).Equals(Primes.One))
					{
						break;
					}
				}
				else
				{
					Primes.Inc(primeSeed, num5 + 1);
				}
				if (num3 >= 4 * length + num6)
				{
					goto Block_8;
				}
				num7 += 2;
				bigInteger4 = bigInteger4.Add(bigInteger2);
			}
			return new Primes.STOutput(bigInteger4, primeSeed, num3);
			Block_8:
			throw new InvalidOperationException("Too many iterations in Shawe-Taylor Random_Prime Routine");
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x000D7870 File Offset: 0x000D5A70
		private static uint Extract32(byte[] bs)
		{
			uint num = 0U;
			int num2 = Math.Min(4, bs.Length);
			for (int i = 0; i < num2; i++)
			{
				uint num3 = (uint)bs[bs.Length - (i + 1)];
				num |= num3 << 8 * i;
			}
			return num;
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x000D78AB File Offset: 0x000D5AAB
		private static void Hash(IDigest d, byte[] input, byte[] output, int outPos)
		{
			d.BlockUpdate(input, 0, input.Length);
			d.DoFinal(output, outPos);
		}

		// Token: 0x06001BFE RID: 7166 RVA: 0x000D78C4 File Offset: 0x000D5AC4
		private static BigInteger HashGen(IDigest d, byte[] seed, int count)
		{
			int digestSize = d.GetDigestSize();
			int num = count * digestSize;
			byte[] array = new byte[num];
			for (int i = 0; i < count; i++)
			{
				num -= digestSize;
				Primes.Hash(d, seed, array, num);
				Primes.Inc(seed, 1);
			}
			return new BigInteger(1, array);
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x000D790C File Offset: 0x000D5B0C
		private static void Inc(byte[] seed, int c)
		{
			int num = seed.Length;
			while (c > 0 && --num >= 0)
			{
				c += (int)seed[num];
				seed[num] = (byte)c;
				c >>= 8;
			}
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x000D793C File Offset: 0x000D5B3C
		private static bool IsPrime32(uint x)
		{
			if (x <= 5U)
			{
				return x == 2U || x == 3U || x == 5U;
			}
			if ((x & 1U) == 0U || x % 3U == 0U || x % 5U == 0U)
			{
				return false;
			}
			uint[] array = new uint[]
			{
				1U,
				7U,
				11U,
				13U,
				17U,
				19U,
				23U,
				29U
			};
			uint num = 0U;
			int num2 = 1;
			for (;;)
			{
				if (num2 >= array.Length)
				{
					num += 30U;
					if (num >> 16 != 0U || num * num >= x)
					{
						return true;
					}
					num2 = 0;
				}
				else
				{
					uint num3 = num + array[num2];
					if (x % num3 == 0U)
					{
						break;
					}
					num2++;
				}
			}
			return x < 30U;
		}

		// Token: 0x040017FD RID: 6141
		public static readonly int SmallFactorLimit = 211;

		// Token: 0x040017FE RID: 6142
		private static readonly BigInteger One = BigInteger.One;

		// Token: 0x040017FF RID: 6143
		private static readonly BigInteger Two = BigInteger.Two;

		// Token: 0x04001800 RID: 6144
		private static readonly BigInteger Three = BigInteger.Three;

		// Token: 0x020008E5 RID: 2277
		public class MROutput
		{
			// Token: 0x06004D7B RID: 19835 RVA: 0x001B0D6E File Offset: 0x001AEF6E
			internal static Primes.MROutput ProbablyPrime()
			{
				return new Primes.MROutput(false, null);
			}

			// Token: 0x06004D7C RID: 19836 RVA: 0x001B0D77 File Offset: 0x001AEF77
			internal static Primes.MROutput ProvablyCompositeWithFactor(BigInteger factor)
			{
				return new Primes.MROutput(true, factor);
			}

			// Token: 0x06004D7D RID: 19837 RVA: 0x001B0D80 File Offset: 0x001AEF80
			internal static Primes.MROutput ProvablyCompositeNotPrimePower()
			{
				return new Primes.MROutput(true, null);
			}

			// Token: 0x06004D7E RID: 19838 RVA: 0x001B0D89 File Offset: 0x001AEF89
			private MROutput(bool provablyComposite, BigInteger factor)
			{
				this.mProvablyComposite = provablyComposite;
				this.mFactor = factor;
			}

			// Token: 0x17000C12 RID: 3090
			// (get) Token: 0x06004D7F RID: 19839 RVA: 0x001B0D9F File Offset: 0x001AEF9F
			public BigInteger Factor
			{
				get
				{
					return this.mFactor;
				}
			}

			// Token: 0x17000C13 RID: 3091
			// (get) Token: 0x06004D80 RID: 19840 RVA: 0x001B0DA7 File Offset: 0x001AEFA7
			public bool IsProvablyComposite
			{
				get
				{
					return this.mProvablyComposite;
				}
			}

			// Token: 0x17000C14 RID: 3092
			// (get) Token: 0x06004D81 RID: 19841 RVA: 0x001B0DAF File Offset: 0x001AEFAF
			public bool IsNotPrimePower
			{
				get
				{
					return this.mProvablyComposite && this.mFactor == null;
				}
			}

			// Token: 0x04003427 RID: 13351
			private readonly bool mProvablyComposite;

			// Token: 0x04003428 RID: 13352
			private readonly BigInteger mFactor;
		}

		// Token: 0x020008E6 RID: 2278
		public class STOutput
		{
			// Token: 0x06004D82 RID: 19842 RVA: 0x001B0DC4 File Offset: 0x001AEFC4
			internal STOutput(BigInteger prime, byte[] primeSeed, int primeGenCounter)
			{
				this.mPrime = prime;
				this.mPrimeSeed = primeSeed;
				this.mPrimeGenCounter = primeGenCounter;
			}

			// Token: 0x17000C15 RID: 3093
			// (get) Token: 0x06004D83 RID: 19843 RVA: 0x001B0DE1 File Offset: 0x001AEFE1
			public BigInteger Prime
			{
				get
				{
					return this.mPrime;
				}
			}

			// Token: 0x17000C16 RID: 3094
			// (get) Token: 0x06004D84 RID: 19844 RVA: 0x001B0DE9 File Offset: 0x001AEFE9
			public byte[] PrimeSeed
			{
				get
				{
					return this.mPrimeSeed;
				}
			}

			// Token: 0x17000C17 RID: 3095
			// (get) Token: 0x06004D85 RID: 19845 RVA: 0x001B0DF1 File Offset: 0x001AEFF1
			public int PrimeGenCounter
			{
				get
				{
					return this.mPrimeGenCounter;
				}
			}

			// Token: 0x04003429 RID: 13353
			private readonly BigInteger mPrime;

			// Token: 0x0400342A RID: 13354
			private readonly byte[] mPrimeSeed;

			// Token: 0x0400342B RID: 13355
			private readonly int mPrimeGenCounter;
		}
	}
}
