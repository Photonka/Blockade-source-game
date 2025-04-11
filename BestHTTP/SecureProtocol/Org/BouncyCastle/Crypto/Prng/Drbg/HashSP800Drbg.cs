using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng.Drbg
{
	// Token: 0x020004A7 RID: 1191
	public class HashSP800Drbg : ISP80090Drbg
	{
		// Token: 0x06002EDD RID: 11997 RVA: 0x001265C4 File Offset: 0x001247C4
		static HashSP800Drbg()
		{
			HashSP800Drbg.seedlens.Add("SHA-1", 440);
			HashSP800Drbg.seedlens.Add("SHA-224", 440);
			HashSP800Drbg.seedlens.Add("SHA-256", 440);
			HashSP800Drbg.seedlens.Add("SHA-512/256", 440);
			HashSP800Drbg.seedlens.Add("SHA-512/224", 440);
			HashSP800Drbg.seedlens.Add("SHA-384", 888);
			HashSP800Drbg.seedlens.Add("SHA-512", 888);
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x001266B4 File Offset: 0x001248B4
		public HashSP800Drbg(IDigest digest, int securityStrength, IEntropySource entropySource, byte[] personalizationString, byte[] nonce)
		{
			if (securityStrength > DrbgUtilities.GetMaxSecurityStrength(digest))
			{
				throw new ArgumentException("Requested security strength is not supported by the derivation function");
			}
			if (entropySource.EntropySize < securityStrength)
			{
				throw new ArgumentException("Not enough entropy for security strength required");
			}
			this.mDigest = digest;
			this.mEntropySource = entropySource;
			this.mSecurityStrength = securityStrength;
			this.mSeedLength = (int)HashSP800Drbg.seedlens[digest.AlgorithmName];
			byte[] entropy = this.GetEntropy();
			byte[] seedMaterial = Arrays.ConcatenateAll(new byte[][]
			{
				entropy,
				nonce,
				personalizationString
			});
			byte[] array = DrbgUtilities.HashDF(this.mDigest, seedMaterial, this.mSeedLength);
			this.mV = array;
			byte[] array2 = new byte[this.mV.Length + 1];
			Array.Copy(this.mV, 0, array2, 1, this.mV.Length);
			this.mC = DrbgUtilities.HashDF(this.mDigest, array2, this.mSeedLength);
			this.mReseedCounter = 1L;
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06002EDF RID: 11999 RVA: 0x001267A0 File Offset: 0x001249A0
		public int BlockSize
		{
			get
			{
				return this.mDigest.GetDigestSize() * 8;
			}
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x001267B0 File Offset: 0x001249B0
		public int Generate(byte[] output, byte[] additionalInput, bool predictionResistant)
		{
			int num = output.Length * 8;
			if (num > HashSP800Drbg.MAX_BITS_REQUEST)
			{
				throw new ArgumentException("Number of bits per request limited to " + HashSP800Drbg.MAX_BITS_REQUEST, "output");
			}
			if (this.mReseedCounter > HashSP800Drbg.RESEED_MAX)
			{
				return -1;
			}
			if (predictionResistant)
			{
				this.Reseed(additionalInput);
				additionalInput = null;
			}
			if (additionalInput != null)
			{
				byte[] array = new byte[1 + this.mV.Length + additionalInput.Length];
				array[0] = 2;
				Array.Copy(this.mV, 0, array, 1, this.mV.Length);
				Array.Copy(additionalInput, 0, array, 1 + this.mV.Length, additionalInput.Length);
				byte[] shorter = this.Hash(array);
				this.AddTo(this.mV, shorter);
			}
			Array sourceArray = this.hashgen(this.mV, num);
			byte[] array2 = new byte[this.mV.Length + 1];
			Array.Copy(this.mV, 0, array2, 1, this.mV.Length);
			array2[0] = 3;
			byte[] shorter2 = this.Hash(array2);
			this.AddTo(this.mV, shorter2);
			this.AddTo(this.mV, this.mC);
			byte[] shorter3 = new byte[]
			{
				(byte)(this.mReseedCounter >> 24),
				(byte)(this.mReseedCounter >> 16),
				(byte)(this.mReseedCounter >> 8),
				(byte)this.mReseedCounter
			};
			this.AddTo(this.mV, shorter3);
			this.mReseedCounter += 1L;
			Array.Copy(sourceArray, 0, output, 0, output.Length);
			return num;
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x00126925 File Offset: 0x00124B25
		private byte[] GetEntropy()
		{
			byte[] entropy = this.mEntropySource.GetEntropy();
			if (entropy.Length < (this.mSecurityStrength + 7) / 8)
			{
				throw new InvalidOperationException("Insufficient entropy provided by entropy source");
			}
			return entropy;
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x0012694C File Offset: 0x00124B4C
		private void AddTo(byte[] longer, byte[] shorter)
		{
			int num = longer.Length - shorter.Length;
			uint num2 = 0U;
			int num3 = shorter.Length;
			while (--num3 >= 0)
			{
				num2 += (uint)(longer[num + num3] + shorter[num3]);
				longer[num + num3] = (byte)num2;
				num2 >>= 8;
			}
			num3 = num;
			while (--num3 >= 0)
			{
				num2 += (uint)longer[num3];
				longer[num3] = (byte)num2;
				num2 >>= 8;
			}
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x001269A4 File Offset: 0x00124BA4
		public void Reseed(byte[] additionalInput)
		{
			byte[] entropy = this.GetEntropy();
			byte[] seedMaterial = Arrays.ConcatenateAll(new byte[][]
			{
				HashSP800Drbg.ONE,
				this.mV,
				entropy,
				additionalInput
			});
			byte[] array = DrbgUtilities.HashDF(this.mDigest, seedMaterial, this.mSeedLength);
			this.mV = array;
			byte[] array2 = new byte[this.mV.Length + 1];
			array2[0] = 0;
			Array.Copy(this.mV, 0, array2, 1, this.mV.Length);
			this.mC = DrbgUtilities.HashDF(this.mDigest, array2, this.mSeedLength);
			this.mReseedCounter = 1L;
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x00126A44 File Offset: 0x00124C44
		private byte[] Hash(byte[] input)
		{
			byte[] array = new byte[this.mDigest.GetDigestSize()];
			this.DoHash(input, array);
			return array;
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x00126A6B File Offset: 0x00124C6B
		private void DoHash(byte[] input, byte[] output)
		{
			this.mDigest.BlockUpdate(input, 0, input.Length);
			this.mDigest.DoFinal(output, 0);
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x00126A8C File Offset: 0x00124C8C
		private byte[] hashgen(byte[] input, int lengthInBits)
		{
			int digestSize = this.mDigest.GetDigestSize();
			int num = lengthInBits / 8 / digestSize;
			byte[] array = new byte[input.Length];
			Array.Copy(input, 0, array, 0, input.Length);
			byte[] array2 = new byte[lengthInBits / 8];
			byte[] array3 = new byte[this.mDigest.GetDigestSize()];
			for (int i = 0; i <= num; i++)
			{
				this.DoHash(array, array3);
				int length = (array2.Length - i * array3.Length > array3.Length) ? array3.Length : (array2.Length - i * array3.Length);
				Array.Copy(array3, 0, array2, i * array3.Length, length);
				this.AddTo(array, HashSP800Drbg.ONE);
			}
			return array2;
		}

		// Token: 0x04001E59 RID: 7769
		private static readonly byte[] ONE = new byte[]
		{
			1
		};

		// Token: 0x04001E5A RID: 7770
		private static readonly long RESEED_MAX = 140737488355328L;

		// Token: 0x04001E5B RID: 7771
		private static readonly int MAX_BITS_REQUEST = 262144;

		// Token: 0x04001E5C RID: 7772
		private static readonly IDictionary seedlens = Platform.CreateHashtable();

		// Token: 0x04001E5D RID: 7773
		private readonly IDigest mDigest;

		// Token: 0x04001E5E RID: 7774
		private readonly IEntropySource mEntropySource;

		// Token: 0x04001E5F RID: 7775
		private readonly int mSecurityStrength;

		// Token: 0x04001E60 RID: 7776
		private readonly int mSeedLength;

		// Token: 0x04001E61 RID: 7777
		private byte[] mV;

		// Token: 0x04001E62 RID: 7778
		private byte[] mC;

		// Token: 0x04001E63 RID: 7779
		private long mReseedCounter;
	}
}
