using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng.Drbg;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x0200049E RID: 1182
	public class SP800SecureRandom : SecureRandom
	{
		// Token: 0x06002E9C RID: 11932 RVA: 0x00125128 File Offset: 0x00123328
		internal SP800SecureRandom(SecureRandom randomSource, IEntropySource entropySource, IDrbgProvider drbgProvider, bool predictionResistant) : base(null)
		{
			this.mRandomSource = randomSource;
			this.mEntropySource = entropySource;
			this.mDrbgProvider = drbgProvider;
			this.mPredictionResistant = predictionResistant;
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x00125150 File Offset: 0x00123350
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

		// Token: 0x06002E9E RID: 11934 RVA: 0x0012519C File Offset: 0x0012339C
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

		// Token: 0x06002E9F RID: 11935 RVA: 0x001251E8 File Offset: 0x001233E8
		public override void NextBytes(byte[] bytes)
		{
			lock (this)
			{
				if (this.mDrbg == null)
				{
					this.mDrbg = this.mDrbgProvider.Get(this.mEntropySource);
				}
				if (this.mDrbg.Generate(bytes, null, this.mPredictionResistant) < 0)
				{
					this.mDrbg.Reseed(null);
					this.mDrbg.Generate(bytes, null, this.mPredictionResistant);
				}
			}
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x00125274 File Offset: 0x00123474
		public override void NextBytes(byte[] buf, int off, int len)
		{
			byte[] array = new byte[len];
			this.NextBytes(array);
			Array.Copy(array, 0, buf, off, len);
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x00125299 File Offset: 0x00123499
		public override byte[] GenerateSeed(int numBytes)
		{
			return EntropyUtilities.GenerateSeed(this.mEntropySource, numBytes);
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x001252A8 File Offset: 0x001234A8
		public virtual void Reseed(byte[] additionalInput)
		{
			lock (this)
			{
				if (this.mDrbg == null)
				{
					this.mDrbg = this.mDrbgProvider.Get(this.mEntropySource);
				}
				this.mDrbg.Reseed(additionalInput);
			}
		}

		// Token: 0x04001E2C RID: 7724
		private readonly IDrbgProvider mDrbgProvider;

		// Token: 0x04001E2D RID: 7725
		private readonly bool mPredictionResistant;

		// Token: 0x04001E2E RID: 7726
		private readonly SecureRandom mRandomSource;

		// Token: 0x04001E2F RID: 7727
		private readonly IEntropySource mEntropySource;

		// Token: 0x04001E30 RID: 7728
		private ISP80090Drbg mDrbg;
	}
}
