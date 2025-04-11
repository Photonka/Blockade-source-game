using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x02000496 RID: 1174
	public class BasicEntropySourceProvider : IEntropySourceProvider
	{
		// Token: 0x06002E7A RID: 11898 RVA: 0x00124C14 File Offset: 0x00122E14
		public BasicEntropySourceProvider(SecureRandom secureRandom, bool isPredictionResistant)
		{
			this.mSecureRandom = secureRandom;
			this.mPredictionResistant = isPredictionResistant;
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x00124C2A File Offset: 0x00122E2A
		public IEntropySource Get(int bitsRequired)
		{
			return new BasicEntropySourceProvider.BasicEntropySource(this.mSecureRandom, this.mPredictionResistant, bitsRequired);
		}

		// Token: 0x04001E1E RID: 7710
		private readonly SecureRandom mSecureRandom;

		// Token: 0x04001E1F RID: 7711
		private readonly bool mPredictionResistant;

		// Token: 0x02000928 RID: 2344
		private class BasicEntropySource : IEntropySource
		{
			// Token: 0x06004E3E RID: 20030 RVA: 0x001B3690 File Offset: 0x001B1890
			internal BasicEntropySource(SecureRandom secureRandom, bool predictionResistant, int entropySize)
			{
				this.mSecureRandom = secureRandom;
				this.mPredictionResistant = predictionResistant;
				this.mEntropySize = entropySize;
			}

			// Token: 0x17000C3F RID: 3135
			// (get) Token: 0x06004E3F RID: 20031 RVA: 0x001B36AD File Offset: 0x001B18AD
			bool IEntropySource.IsPredictionResistant
			{
				get
				{
					return this.mPredictionResistant;
				}
			}

			// Token: 0x06004E40 RID: 20032 RVA: 0x001B36B5 File Offset: 0x001B18B5
			byte[] IEntropySource.GetEntropy()
			{
				return SecureRandom.GetNextBytes(this.mSecureRandom, (this.mEntropySize + 7) / 8);
			}

			// Token: 0x17000C40 RID: 3136
			// (get) Token: 0x06004E41 RID: 20033 RVA: 0x001B36CC File Offset: 0x001B18CC
			int IEntropySource.EntropySize
			{
				get
				{
					return this.mEntropySize;
				}
			}

			// Token: 0x040034F1 RID: 13553
			private readonly SecureRandom mSecureRandom;

			// Token: 0x040034F2 RID: 13554
			private readonly bool mPredictionResistant;

			// Token: 0x040034F3 RID: 13555
			private readonly int mEntropySize;
		}
	}
}
