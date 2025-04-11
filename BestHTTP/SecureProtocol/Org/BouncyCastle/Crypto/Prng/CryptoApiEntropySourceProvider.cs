using System;
using System.Security.Cryptography;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x02000497 RID: 1175
	public class CryptoApiEntropySourceProvider : IEntropySourceProvider
	{
		// Token: 0x06002E7C RID: 11900 RVA: 0x00124C3E File Offset: 0x00122E3E
		public CryptoApiEntropySourceProvider() : this(RandomNumberGenerator.Create(), true)
		{
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x00124C4C File Offset: 0x00122E4C
		public CryptoApiEntropySourceProvider(RandomNumberGenerator rng, bool isPredictionResistant)
		{
			if (rng == null)
			{
				throw new ArgumentNullException("rng");
			}
			this.mRng = rng;
			this.mPredictionResistant = isPredictionResistant;
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x00124C70 File Offset: 0x00122E70
		public IEntropySource Get(int bitsRequired)
		{
			return new CryptoApiEntropySourceProvider.CryptoApiEntropySource(this.mRng, this.mPredictionResistant, bitsRequired);
		}

		// Token: 0x04001E20 RID: 7712
		private readonly RandomNumberGenerator mRng;

		// Token: 0x04001E21 RID: 7713
		private readonly bool mPredictionResistant;

		// Token: 0x02000929 RID: 2345
		private class CryptoApiEntropySource : IEntropySource
		{
			// Token: 0x06004E42 RID: 20034 RVA: 0x001B36D4 File Offset: 0x001B18D4
			internal CryptoApiEntropySource(RandomNumberGenerator rng, bool predictionResistant, int entropySize)
			{
				this.mRng = rng;
				this.mPredictionResistant = predictionResistant;
				this.mEntropySize = entropySize;
			}

			// Token: 0x17000C41 RID: 3137
			// (get) Token: 0x06004E43 RID: 20035 RVA: 0x001B36F1 File Offset: 0x001B18F1
			bool IEntropySource.IsPredictionResistant
			{
				get
				{
					return this.mPredictionResistant;
				}
			}

			// Token: 0x06004E44 RID: 20036 RVA: 0x001B36FC File Offset: 0x001B18FC
			byte[] IEntropySource.GetEntropy()
			{
				byte[] array = new byte[(this.mEntropySize + 7) / 8];
				this.mRng.GetBytes(array);
				return array;
			}

			// Token: 0x17000C42 RID: 3138
			// (get) Token: 0x06004E45 RID: 20037 RVA: 0x001B3726 File Offset: 0x001B1926
			int IEntropySource.EntropySize
			{
				get
				{
					return this.mEntropySize;
				}
			}

			// Token: 0x040034F4 RID: 13556
			private readonly RandomNumberGenerator mRng;

			// Token: 0x040034F5 RID: 13557
			private readonly bool mPredictionResistant;

			// Token: 0x040034F6 RID: 13558
			private readonly int mEntropySize;
		}
	}
}
