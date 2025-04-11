using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng.Drbg
{
	// Token: 0x020004A5 RID: 1189
	public class CtrSP800Drbg : ISP80090Drbg
	{
		// Token: 0x06002EC5 RID: 11973 RVA: 0x00125B78 File Offset: 0x00123D78
		public CtrSP800Drbg(IBlockCipher engine, int keySizeInBits, int securityStrength, IEntropySource entropySource, byte[] personalizationString, byte[] nonce)
		{
			if (securityStrength > 256)
			{
				throw new ArgumentException("Requested security strength is not supported by the derivation function");
			}
			if (this.GetMaxSecurityStrength(engine, keySizeInBits) < securityStrength)
			{
				throw new ArgumentException("Requested security strength is not supported by block cipher and key size");
			}
			if (entropySource.EntropySize < securityStrength)
			{
				throw new ArgumentException("Not enough entropy for security strength required");
			}
			this.mEntropySource = entropySource;
			this.mEngine = engine;
			this.mKeySizeInBits = keySizeInBits;
			this.mSecurityStrength = securityStrength;
			this.mSeedLength = keySizeInBits + engine.GetBlockSize() * 8;
			this.mIsTdea = this.IsTdea(engine);
			byte[] entropy = this.GetEntropy();
			this.CTR_DRBG_Instantiate_algorithm(entropy, nonce, personalizationString);
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x00125C18 File Offset: 0x00123E18
		private void CTR_DRBG_Instantiate_algorithm(byte[] entropy, byte[] nonce, byte[] personalisationString)
		{
			byte[] inputString = Arrays.ConcatenateAll(new byte[][]
			{
				entropy,
				nonce,
				personalisationString
			});
			byte[] seed = this.Block_Cipher_df(inputString, this.mSeedLength);
			int blockSize = this.mEngine.GetBlockSize();
			this.mKey = new byte[(this.mKeySizeInBits + 7) / 8];
			this.mV = new byte[blockSize];
			this.CTR_DRBG_Update(seed, this.mKey, this.mV);
			this.mReseedCounter = 1L;
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x00125C94 File Offset: 0x00123E94
		private void CTR_DRBG_Update(byte[] seed, byte[] key, byte[] v)
		{
			byte[] array = new byte[seed.Length];
			byte[] array2 = new byte[this.mEngine.GetBlockSize()];
			int num = 0;
			int blockSize = this.mEngine.GetBlockSize();
			this.mEngine.Init(true, new KeyParameter(this.ExpandKey(key)));
			while (num * blockSize < seed.Length)
			{
				this.AddOneTo(v);
				this.mEngine.ProcessBlock(v, 0, array2, 0);
				int length = (array.Length - num * blockSize > blockSize) ? blockSize : (array.Length - num * blockSize);
				Array.Copy(array2, 0, array, num * blockSize, length);
				num++;
			}
			this.XOR(array, seed, array, 0);
			Array.Copy(array, 0, key, 0, key.Length);
			Array.Copy(array, key.Length, v, 0, v.Length);
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x00125D50 File Offset: 0x00123F50
		private void CTR_DRBG_Reseed_algorithm(byte[] additionalInput)
		{
			byte[] array = Arrays.Concatenate(this.GetEntropy(), additionalInput);
			array = this.Block_Cipher_df(array, this.mSeedLength);
			this.CTR_DRBG_Update(array, this.mKey, this.mV);
			this.mReseedCounter = 1L;
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x00125D94 File Offset: 0x00123F94
		private void XOR(byte[] output, byte[] a, byte[] b, int bOff)
		{
			for (int i = 0; i < output.Length; i++)
			{
				output[i] = (a[i] ^ b[bOff + i]);
			}
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x00125DC0 File Offset: 0x00123FC0
		private void AddOneTo(byte[] longer)
		{
			uint num = 1U;
			int num2 = longer.Length;
			while (--num2 >= 0)
			{
				num += (uint)longer[num2];
				longer[num2] = (byte)num;
				num >>= 8;
			}
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x00125DEC File Offset: 0x00123FEC
		private byte[] GetEntropy()
		{
			byte[] entropy = this.mEntropySource.GetEntropy();
			if (entropy.Length < (this.mSecurityStrength + 7) / 8)
			{
				throw new InvalidOperationException("Insufficient entropy provided by entropy source");
			}
			return entropy;
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x00125E14 File Offset: 0x00124014
		private byte[] Block_Cipher_df(byte[] inputString, int bitLength)
		{
			int blockSize = this.mEngine.GetBlockSize();
			int num = inputString.Length;
			int value = bitLength / 8;
			byte[] array = new byte[(8 + num + 1 + blockSize - 1) / blockSize * blockSize];
			this.copyIntToByteArray(array, num, 0);
			this.copyIntToByteArray(array, value, 4);
			Array.Copy(inputString, 0, array, 8, num);
			array[8 + num] = 128;
			byte[] array2 = new byte[this.mKeySizeInBits / 8 + blockSize];
			byte[] array3 = new byte[blockSize];
			byte[] array4 = new byte[blockSize];
			int num2 = 0;
			byte[] array5 = new byte[this.mKeySizeInBits / 8];
			Array.Copy(CtrSP800Drbg.K_BITS, 0, array5, 0, array5.Length);
			while (num2 * blockSize * 8 < this.mKeySizeInBits + blockSize * 8)
			{
				this.copyIntToByteArray(array4, num2, 0);
				this.BCC(array3, array5, array4, array);
				int length = (array2.Length - num2 * blockSize > blockSize) ? blockSize : (array2.Length - num2 * blockSize);
				Array.Copy(array3, 0, array2, num2 * blockSize, length);
				num2++;
			}
			byte[] array6 = new byte[blockSize];
			Array.Copy(array2, 0, array5, 0, array5.Length);
			Array.Copy(array2, array5.Length, array6, 0, array6.Length);
			array2 = new byte[bitLength / 2];
			num2 = 0;
			this.mEngine.Init(true, new KeyParameter(this.ExpandKey(array5)));
			while (num2 * blockSize < array2.Length)
			{
				this.mEngine.ProcessBlock(array6, 0, array6, 0);
				int length2 = (array2.Length - num2 * blockSize > blockSize) ? blockSize : (array2.Length - num2 * blockSize);
				Array.Copy(array6, 0, array2, num2 * blockSize, length2);
				num2++;
			}
			return array2;
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x00125FB0 File Offset: 0x001241B0
		private void BCC(byte[] bccOut, byte[] k, byte[] iV, byte[] data)
		{
			int blockSize = this.mEngine.GetBlockSize();
			byte[] array = new byte[blockSize];
			int num = data.Length / blockSize;
			byte[] array2 = new byte[blockSize];
			this.mEngine.Init(true, new KeyParameter(this.ExpandKey(k)));
			this.mEngine.ProcessBlock(iV, 0, array, 0);
			for (int i = 0; i < num; i++)
			{
				this.XOR(array2, array, data, i * blockSize);
				this.mEngine.ProcessBlock(array2, 0, array, 0);
			}
			Array.Copy(array, 0, bccOut, 0, bccOut.Length);
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x00126040 File Offset: 0x00124240
		private void copyIntToByteArray(byte[] buf, int value, int offSet)
		{
			buf[offSet] = (byte)(value >> 24);
			buf[offSet + 1] = (byte)(value >> 16);
			buf[offSet + 2] = (byte)(value >> 8);
			buf[offSet + 3] = (byte)value;
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06002ECF RID: 11983 RVA: 0x00126064 File Offset: 0x00124264
		public int BlockSize
		{
			get
			{
				return this.mV.Length * 8;
			}
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x00126070 File Offset: 0x00124270
		public int Generate(byte[] output, byte[] additionalInput, bool predictionResistant)
		{
			if (this.mIsTdea)
			{
				if (this.mReseedCounter > CtrSP800Drbg.TDEA_RESEED_MAX)
				{
					return -1;
				}
				if (DrbgUtilities.IsTooLarge(output, CtrSP800Drbg.TDEA_MAX_BITS_REQUEST / 8))
				{
					throw new ArgumentException("Number of bits per request limited to " + CtrSP800Drbg.TDEA_MAX_BITS_REQUEST, "output");
				}
			}
			else
			{
				if (this.mReseedCounter > CtrSP800Drbg.AES_RESEED_MAX)
				{
					return -1;
				}
				if (DrbgUtilities.IsTooLarge(output, CtrSP800Drbg.AES_MAX_BITS_REQUEST / 8))
				{
					throw new ArgumentException("Number of bits per request limited to " + CtrSP800Drbg.AES_MAX_BITS_REQUEST, "output");
				}
			}
			if (predictionResistant)
			{
				this.CTR_DRBG_Reseed_algorithm(additionalInput);
				additionalInput = null;
			}
			if (additionalInput != null)
			{
				additionalInput = this.Block_Cipher_df(additionalInput, this.mSeedLength);
				this.CTR_DRBG_Update(additionalInput, this.mKey, this.mV);
			}
			else
			{
				additionalInput = new byte[this.mSeedLength];
			}
			byte[] array = new byte[this.mV.Length];
			this.mEngine.Init(true, new KeyParameter(this.ExpandKey(this.mKey)));
			for (int i = 0; i <= output.Length / array.Length; i++)
			{
				int num = (output.Length - i * array.Length > array.Length) ? array.Length : (output.Length - i * this.mV.Length);
				if (num != 0)
				{
					this.AddOneTo(this.mV);
					this.mEngine.ProcessBlock(this.mV, 0, array, 0);
					Array.Copy(array, 0, output, i * array.Length, num);
				}
			}
			this.CTR_DRBG_Update(additionalInput, this.mKey, this.mV);
			this.mReseedCounter += 1L;
			return output.Length * 8;
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x001261F8 File Offset: 0x001243F8
		public void Reseed(byte[] additionalInput)
		{
			this.CTR_DRBG_Reseed_algorithm(additionalInput);
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x00126201 File Offset: 0x00124401
		private bool IsTdea(IBlockCipher cipher)
		{
			return cipher.AlgorithmName.Equals("DESede") || cipher.AlgorithmName.Equals("TDEA");
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x00126227 File Offset: 0x00124427
		private int GetMaxSecurityStrength(IBlockCipher cipher, int keySizeInBits)
		{
			if (this.IsTdea(cipher) && keySizeInBits == 168)
			{
				return 112;
			}
			if (cipher.AlgorithmName.Equals("AES"))
			{
				return keySizeInBits;
			}
			return -1;
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x00126254 File Offset: 0x00124454
		private byte[] ExpandKey(byte[] key)
		{
			if (this.mIsTdea)
			{
				byte[] array = new byte[24];
				this.PadKey(key, 0, array, 0);
				this.PadKey(key, 7, array, 8);
				this.PadKey(key, 14, array, 16);
				return array;
			}
			return key;
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x00126294 File Offset: 0x00124494
		private void PadKey(byte[] keyMaster, int keyOff, byte[] tmp, int tmpOff)
		{
			tmp[tmpOff] = (keyMaster[keyOff] & 254);
			tmp[tmpOff + 1] = (byte)((int)keyMaster[keyOff] << 7 | (keyMaster[keyOff + 1] & 252) >> 1);
			tmp[tmpOff + 2] = (byte)((int)keyMaster[keyOff + 1] << 6 | (keyMaster[keyOff + 2] & 248) >> 2);
			tmp[tmpOff + 3] = (byte)((int)keyMaster[keyOff + 2] << 5 | (keyMaster[keyOff + 3] & 240) >> 3);
			tmp[tmpOff + 4] = (byte)((int)keyMaster[keyOff + 3] << 4 | (keyMaster[keyOff + 4] & 224) >> 4);
			tmp[tmpOff + 5] = (byte)((int)keyMaster[keyOff + 4] << 3 | (keyMaster[keyOff + 5] & 192) >> 5);
			tmp[tmpOff + 6] = (byte)((int)keyMaster[keyOff + 5] << 2 | (keyMaster[keyOff + 6] & 128) >> 6);
			tmp[tmpOff + 7] = (byte)(keyMaster[keyOff + 6] << 1);
			DesParameters.SetOddParity(tmp, tmpOff, 8);
		}

		// Token: 0x04001E4A RID: 7754
		private static readonly long TDEA_RESEED_MAX = (long)((ulong)int.MinValue);

		// Token: 0x04001E4B RID: 7755
		private static readonly long AES_RESEED_MAX = 140737488355328L;

		// Token: 0x04001E4C RID: 7756
		private static readonly int TDEA_MAX_BITS_REQUEST = 4096;

		// Token: 0x04001E4D RID: 7757
		private static readonly int AES_MAX_BITS_REQUEST = 262144;

		// Token: 0x04001E4E RID: 7758
		private readonly IEntropySource mEntropySource;

		// Token: 0x04001E4F RID: 7759
		private readonly IBlockCipher mEngine;

		// Token: 0x04001E50 RID: 7760
		private readonly int mKeySizeInBits;

		// Token: 0x04001E51 RID: 7761
		private readonly int mSeedLength;

		// Token: 0x04001E52 RID: 7762
		private readonly int mSecurityStrength;

		// Token: 0x04001E53 RID: 7763
		private byte[] mKey;

		// Token: 0x04001E54 RID: 7764
		private byte[] mV;

		// Token: 0x04001E55 RID: 7765
		private long mReseedCounter;

		// Token: 0x04001E56 RID: 7766
		private bool mIsTdea;

		// Token: 0x04001E57 RID: 7767
		private static readonly byte[] K_BITS = Hex.Decode("000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F");
	}
}
