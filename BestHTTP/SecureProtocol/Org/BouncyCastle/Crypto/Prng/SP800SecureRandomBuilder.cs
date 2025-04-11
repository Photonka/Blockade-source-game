using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng.Drbg;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x0200049F RID: 1183
	public class SP800SecureRandomBuilder
	{
		// Token: 0x06002EA3 RID: 11939 RVA: 0x00125308 File Offset: 0x00123508
		public SP800SecureRandomBuilder() : this(new SecureRandom(), false)
		{
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x00125316 File Offset: 0x00123516
		public SP800SecureRandomBuilder(SecureRandom entropySource, bool predictionResistant)
		{
			this.mSecurityStrength = 256;
			this.mEntropyBitsRequired = 256;
			base..ctor();
			this.mRandom = entropySource;
			this.mEntropySourceProvider = new BasicEntropySourceProvider(entropySource, predictionResistant);
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x00125348 File Offset: 0x00123548
		public SP800SecureRandomBuilder(IEntropySourceProvider entropySourceProvider)
		{
			this.mSecurityStrength = 256;
			this.mEntropyBitsRequired = 256;
			base..ctor();
			this.mRandom = null;
			this.mEntropySourceProvider = entropySourceProvider;
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x00125374 File Offset: 0x00123574
		public SP800SecureRandomBuilder SetPersonalizationString(byte[] personalizationString)
		{
			this.mPersonalizationString = personalizationString;
			return this;
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x0012537E File Offset: 0x0012357E
		public SP800SecureRandomBuilder SetSecurityStrength(int securityStrength)
		{
			this.mSecurityStrength = securityStrength;
			return this;
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x00125388 File Offset: 0x00123588
		public SP800SecureRandomBuilder SetEntropyBitsRequired(int entropyBitsRequired)
		{
			this.mEntropyBitsRequired = entropyBitsRequired;
			return this;
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x00125392 File Offset: 0x00123592
		public SP800SecureRandom BuildHash(IDigest digest, byte[] nonce, bool predictionResistant)
		{
			return new SP800SecureRandom(this.mRandom, this.mEntropySourceProvider.Get(this.mEntropyBitsRequired), new SP800SecureRandomBuilder.HashDrbgProvider(digest, nonce, this.mPersonalizationString, this.mSecurityStrength), predictionResistant);
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x001253C4 File Offset: 0x001235C4
		public SP800SecureRandom BuildCtr(IBlockCipher cipher, int keySizeInBits, byte[] nonce, bool predictionResistant)
		{
			return new SP800SecureRandom(this.mRandom, this.mEntropySourceProvider.Get(this.mEntropyBitsRequired), new SP800SecureRandomBuilder.CtrDrbgProvider(cipher, keySizeInBits, nonce, this.mPersonalizationString, this.mSecurityStrength), predictionResistant);
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x001253F8 File Offset: 0x001235F8
		public SP800SecureRandom BuildHMac(IMac hMac, byte[] nonce, bool predictionResistant)
		{
			return new SP800SecureRandom(this.mRandom, this.mEntropySourceProvider.Get(this.mEntropyBitsRequired), new SP800SecureRandomBuilder.HMacDrbgProvider(hMac, nonce, this.mPersonalizationString, this.mSecurityStrength), predictionResistant);
		}

		// Token: 0x04001E31 RID: 7729
		private readonly SecureRandom mRandom;

		// Token: 0x04001E32 RID: 7730
		private readonly IEntropySourceProvider mEntropySourceProvider;

		// Token: 0x04001E33 RID: 7731
		private byte[] mPersonalizationString;

		// Token: 0x04001E34 RID: 7732
		private int mSecurityStrength;

		// Token: 0x04001E35 RID: 7733
		private int mEntropyBitsRequired;

		// Token: 0x0200092A RID: 2346
		private class HashDrbgProvider : IDrbgProvider
		{
			// Token: 0x06004E46 RID: 20038 RVA: 0x001B372E File Offset: 0x001B192E
			public HashDrbgProvider(IDigest digest, byte[] nonce, byte[] personalizationString, int securityStrength)
			{
				this.mDigest = digest;
				this.mNonce = nonce;
				this.mPersonalizationString = personalizationString;
				this.mSecurityStrength = securityStrength;
			}

			// Token: 0x06004E47 RID: 20039 RVA: 0x001B3753 File Offset: 0x001B1953
			public ISP80090Drbg Get(IEntropySource entropySource)
			{
				return new HashSP800Drbg(this.mDigest, this.mSecurityStrength, entropySource, this.mPersonalizationString, this.mNonce);
			}

			// Token: 0x040034F7 RID: 13559
			private readonly IDigest mDigest;

			// Token: 0x040034F8 RID: 13560
			private readonly byte[] mNonce;

			// Token: 0x040034F9 RID: 13561
			private readonly byte[] mPersonalizationString;

			// Token: 0x040034FA RID: 13562
			private readonly int mSecurityStrength;
		}

		// Token: 0x0200092B RID: 2347
		private class HMacDrbgProvider : IDrbgProvider
		{
			// Token: 0x06004E48 RID: 20040 RVA: 0x001B3773 File Offset: 0x001B1973
			public HMacDrbgProvider(IMac hMac, byte[] nonce, byte[] personalizationString, int securityStrength)
			{
				this.mHMac = hMac;
				this.mNonce = nonce;
				this.mPersonalizationString = personalizationString;
				this.mSecurityStrength = securityStrength;
			}

			// Token: 0x06004E49 RID: 20041 RVA: 0x001B3798 File Offset: 0x001B1998
			public ISP80090Drbg Get(IEntropySource entropySource)
			{
				return new HMacSP800Drbg(this.mHMac, this.mSecurityStrength, entropySource, this.mPersonalizationString, this.mNonce);
			}

			// Token: 0x040034FB RID: 13563
			private readonly IMac mHMac;

			// Token: 0x040034FC RID: 13564
			private readonly byte[] mNonce;

			// Token: 0x040034FD RID: 13565
			private readonly byte[] mPersonalizationString;

			// Token: 0x040034FE RID: 13566
			private readonly int mSecurityStrength;
		}

		// Token: 0x0200092C RID: 2348
		private class CtrDrbgProvider : IDrbgProvider
		{
			// Token: 0x06004E4A RID: 20042 RVA: 0x001B37B8 File Offset: 0x001B19B8
			public CtrDrbgProvider(IBlockCipher blockCipher, int keySizeInBits, byte[] nonce, byte[] personalizationString, int securityStrength)
			{
				this.mBlockCipher = blockCipher;
				this.mKeySizeInBits = keySizeInBits;
				this.mNonce = nonce;
				this.mPersonalizationString = personalizationString;
				this.mSecurityStrength = securityStrength;
			}

			// Token: 0x06004E4B RID: 20043 RVA: 0x001B37E5 File Offset: 0x001B19E5
			public ISP80090Drbg Get(IEntropySource entropySource)
			{
				return new CtrSP800Drbg(this.mBlockCipher, this.mKeySizeInBits, this.mSecurityStrength, entropySource, this.mPersonalizationString, this.mNonce);
			}

			// Token: 0x040034FF RID: 13567
			private readonly IBlockCipher mBlockCipher;

			// Token: 0x04003500 RID: 13568
			private readonly int mKeySizeInBits;

			// Token: 0x04003501 RID: 13569
			private readonly byte[] mNonce;

			// Token: 0x04003502 RID: 13570
			private readonly byte[] mPersonalizationString;

			// Token: 0x04003503 RID: 13571
			private readonly int mSecurityStrength;
		}
	}
}
