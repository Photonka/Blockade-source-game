using System;
using System.Threading;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Security
{
	// Token: 0x020002D2 RID: 722
	public class SecureRandom : Random
	{
		// Token: 0x06001ACA RID: 6858 RVA: 0x000CF7A8 File Offset: 0x000CD9A8
		private static long NextCounterValue()
		{
			return Interlocked.Increment(ref SecureRandom.counter);
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06001ACB RID: 6859 RVA: 0x000CF7B4 File Offset: 0x000CD9B4
		private static SecureRandom Master
		{
			get
			{
				return SecureRandom.master;
			}
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x000CF7BC File Offset: 0x000CD9BC
		private static DigestRandomGenerator CreatePrng(string digestName, bool autoSeed)
		{
			IDigest digest = DigestUtilities.GetDigest(digestName);
			if (digest == null)
			{
				return null;
			}
			DigestRandomGenerator digestRandomGenerator = new DigestRandomGenerator(digest);
			if (autoSeed)
			{
				digestRandomGenerator.AddSeedMaterial(SecureRandom.NextCounterValue());
				digestRandomGenerator.AddSeedMaterial(SecureRandom.GetNextBytes(SecureRandom.Master, digest.GetDigestSize()));
			}
			return digestRandomGenerator;
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x000CF804 File Offset: 0x000CDA04
		public static byte[] GetNextBytes(SecureRandom secureRandom, int length)
		{
			byte[] array = new byte[length];
			secureRandom.NextBytes(array);
			return array;
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x000CF820 File Offset: 0x000CDA20
		public static SecureRandom GetInstance(string algorithm)
		{
			return SecureRandom.GetInstance(algorithm, true);
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x000CF82C File Offset: 0x000CDA2C
		public static SecureRandom GetInstance(string algorithm, bool autoSeed)
		{
			string text = Platform.ToUpperInvariant(algorithm);
			if (Platform.EndsWith(text, "PRNG"))
			{
				DigestRandomGenerator digestRandomGenerator = SecureRandom.CreatePrng(text.Substring(0, text.Length - "PRNG".Length), autoSeed);
				if (digestRandomGenerator != null)
				{
					return new SecureRandom(digestRandomGenerator);
				}
			}
			throw new ArgumentException("Unrecognised PRNG algorithm: " + algorithm, "algorithm");
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x000CF88B File Offset: 0x000CDA8B
		[Obsolete("Call GenerateSeed() on a SecureRandom instance instead")]
		public static byte[] GetSeed(int length)
		{
			return SecureRandom.GetNextBytes(SecureRandom.Master, length);
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x000CF898 File Offset: 0x000CDA98
		public SecureRandom() : this(SecureRandom.CreatePrng("SHA256", true))
		{
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x000CF8AB File Offset: 0x000CDAAB
		[Obsolete("Use GetInstance/SetSeed instead")]
		public SecureRandom(byte[] seed) : this(SecureRandom.CreatePrng("SHA1", false))
		{
			this.SetSeed(seed);
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x000CF8C5 File Offset: 0x000CDAC5
		public SecureRandom(IRandomGenerator generator) : base(0)
		{
			this.generator = generator;
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x000CF8D5 File Offset: 0x000CDAD5
		public virtual byte[] GenerateSeed(int length)
		{
			return SecureRandom.GetNextBytes(SecureRandom.Master, length);
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x000CF8E2 File Offset: 0x000CDAE2
		public virtual void SetSeed(byte[] seed)
		{
			this.generator.AddSeedMaterial(seed);
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x000CF8F0 File Offset: 0x000CDAF0
		public virtual void SetSeed(long seed)
		{
			this.generator.AddSeedMaterial(seed);
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x000CF8FE File Offset: 0x000CDAFE
		public override int Next()
		{
			return this.NextInt() & int.MaxValue;
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x000CF90C File Offset: 0x000CDB0C
		public override int Next(int maxValue)
		{
			if (maxValue < 2)
			{
				if (maxValue < 0)
				{
					throw new ArgumentOutOfRangeException("maxValue", "cannot be negative");
				}
				return 0;
			}
			else
			{
				int num;
				if ((maxValue & maxValue - 1) == 0)
				{
					num = (this.NextInt() & int.MaxValue);
					return (int)((long)num * (long)maxValue >> 31);
				}
				int num2;
				do
				{
					num = (this.NextInt() & int.MaxValue);
					num2 = num % maxValue;
				}
				while (num - num2 + (maxValue - 1) < 0);
				return num2;
			}
		}

		// Token: 0x06001AD9 RID: 6873 RVA: 0x000CF970 File Offset: 0x000CDB70
		public override int Next(int minValue, int maxValue)
		{
			if (maxValue <= minValue)
			{
				if (maxValue == minValue)
				{
					return minValue;
				}
				throw new ArgumentException("maxValue cannot be less than minValue");
			}
			else
			{
				int num = maxValue - minValue;
				if (num > 0)
				{
					return minValue + this.Next(num);
				}
				int num2;
				do
				{
					num2 = this.NextInt();
				}
				while (num2 < minValue || num2 >= maxValue);
				return num2;
			}
		}

		// Token: 0x06001ADA RID: 6874 RVA: 0x000CF9B4 File Offset: 0x000CDBB4
		public override void NextBytes(byte[] buf)
		{
			this.generator.NextBytes(buf);
		}

		// Token: 0x06001ADB RID: 6875 RVA: 0x000CF9C2 File Offset: 0x000CDBC2
		public virtual void NextBytes(byte[] buf, int off, int len)
		{
			this.generator.NextBytes(buf, off, len);
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x000CF9D2 File Offset: 0x000CDBD2
		public override double NextDouble()
		{
			return Convert.ToDouble((ulong)this.NextLong()) / SecureRandom.DoubleScale;
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x000CF9E8 File Offset: 0x000CDBE8
		public virtual int NextInt()
		{
			byte[] array = new byte[4];
			this.NextBytes(array);
			return (((int)array[0] << 8 | (int)array[1]) << 8 | (int)array[2]) << 8 | (int)array[3];
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x000CFA18 File Offset: 0x000CDC18
		public virtual long NextLong()
		{
			return (long)((ulong)this.NextInt() << 32 | (ulong)this.NextInt());
		}

		// Token: 0x040017B2 RID: 6066
		private static long counter = Times.NanoTime();

		// Token: 0x040017B3 RID: 6067
		private static readonly SecureRandom master = new SecureRandom(new CryptoApiRandomGenerator());

		// Token: 0x040017B4 RID: 6068
		protected readonly IRandomGenerator generator;

		// Token: 0x040017B5 RID: 6069
		private static readonly double DoubleScale = Math.Pow(2.0, 64.0);
	}
}
