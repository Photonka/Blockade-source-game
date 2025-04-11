using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Date;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x020004A4 RID: 1188
	public class X931SecureRandomBuilder
	{
		// Token: 0x06002EC0 RID: 11968 RVA: 0x00125ABC File Offset: 0x00123CBC
		public X931SecureRandomBuilder() : this(new SecureRandom(), false)
		{
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x00125ACA File Offset: 0x00123CCA
		public X931SecureRandomBuilder(SecureRandom entropySource, bool predictionResistant)
		{
			this.mRandom = entropySource;
			this.mEntropySourceProvider = new BasicEntropySourceProvider(this.mRandom, predictionResistant);
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x00125AEB File Offset: 0x00123CEB
		public X931SecureRandomBuilder(IEntropySourceProvider entropySourceProvider)
		{
			this.mRandom = null;
			this.mEntropySourceProvider = entropySourceProvider;
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x00125B01 File Offset: 0x00123D01
		public X931SecureRandomBuilder SetDateTimeVector(byte[] dateTimeVector)
		{
			this.mDateTimeVector = dateTimeVector;
			return this;
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x00125B0C File Offset: 0x00123D0C
		public X931SecureRandom Build(IBlockCipher engine, KeyParameter key, bool predictionResistant)
		{
			if (this.mDateTimeVector == null)
			{
				this.mDateTimeVector = new byte[engine.GetBlockSize()];
				Pack.UInt64_To_BE((ulong)DateTimeUtilities.CurrentUnixMs(), this.mDateTimeVector, 0);
			}
			engine.Init(true, key);
			return new X931SecureRandom(this.mRandom, new X931Rng(engine, this.mDateTimeVector, this.mEntropySourceProvider.Get(engine.GetBlockSize() * 8)), predictionResistant);
		}

		// Token: 0x04001E47 RID: 7751
		private readonly SecureRandom mRandom;

		// Token: 0x04001E48 RID: 7752
		private IEntropySourceProvider mEntropySourceProvider;

		// Token: 0x04001E49 RID: 7753
		private byte[] mDateTimeVector;
	}
}
