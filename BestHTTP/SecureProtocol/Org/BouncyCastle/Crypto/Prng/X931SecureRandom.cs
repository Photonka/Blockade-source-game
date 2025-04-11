using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x020004A3 RID: 1187
	public class X931SecureRandom : SecureRandom
	{
		// Token: 0x06002EBA RID: 11962 RVA: 0x00125964 File Offset: 0x00123B64
		internal X931SecureRandom(SecureRandom randomSource, X931Rng drbg, bool predictionResistant) : base(null)
		{
			this.mRandomSource = randomSource;
			this.mDrbg = drbg;
			this.mPredictionResistant = predictionResistant;
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x00125984 File Offset: 0x00123B84
		public override void SetSeed(byte[] seed)
		{
			lock (this)
			{
				if (this.mRandomSource != null)
				{
					this.mRandomSource.SetSeed(seed);
				}
			}
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x001259D0 File Offset: 0x00123BD0
		public override void SetSeed(long seed)
		{
			lock (this)
			{
				if (this.mRandomSource != null)
				{
					this.mRandomSource.SetSeed(seed);
				}
			}
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x00125A1C File Offset: 0x00123C1C
		public override void NextBytes(byte[] bytes)
		{
			lock (this)
			{
				if (this.mDrbg.Generate(bytes, this.mPredictionResistant) < 0)
				{
					this.mDrbg.Reseed();
					this.mDrbg.Generate(bytes, this.mPredictionResistant);
				}
			}
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x00125A84 File Offset: 0x00123C84
		public override void NextBytes(byte[] buf, int off, int len)
		{
			byte[] array = new byte[len];
			this.NextBytes(array);
			Array.Copy(array, 0, buf, off, len);
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x00125AA9 File Offset: 0x00123CA9
		public override byte[] GenerateSeed(int numBytes)
		{
			return EntropyUtilities.GenerateSeed(this.mDrbg.EntropySource, numBytes);
		}

		// Token: 0x04001E44 RID: 7748
		private readonly bool mPredictionResistant;

		// Token: 0x04001E45 RID: 7749
		private readonly SecureRandom mRandomSource;

		// Token: 0x04001E46 RID: 7750
		private readonly X931Rng mDrbg;
	}
}
