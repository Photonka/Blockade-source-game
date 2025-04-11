using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Encodings
{
	// Token: 0x02000589 RID: 1417
	public class OaepEncoding : IAsymmetricBlockCipher
	{
		// Token: 0x06003611 RID: 13841 RVA: 0x00152F20 File Offset: 0x00151120
		public OaepEncoding(IAsymmetricBlockCipher cipher) : this(cipher, new Sha1Digest(), null)
		{
		}

		// Token: 0x06003612 RID: 13842 RVA: 0x00152F2F File Offset: 0x0015112F
		public OaepEncoding(IAsymmetricBlockCipher cipher, IDigest hash) : this(cipher, hash, null)
		{
		}

		// Token: 0x06003613 RID: 13843 RVA: 0x00152F3A File Offset: 0x0015113A
		public OaepEncoding(IAsymmetricBlockCipher cipher, IDigest hash, byte[] encodingParams) : this(cipher, hash, hash, encodingParams)
		{
		}

		// Token: 0x06003614 RID: 13844 RVA: 0x00152F48 File Offset: 0x00151148
		public OaepEncoding(IAsymmetricBlockCipher cipher, IDigest hash, IDigest mgf1Hash, byte[] encodingParams)
		{
			this.engine = cipher;
			this.mgf1Hash = mgf1Hash;
			this.defHash = new byte[hash.GetDigestSize()];
			hash.Reset();
			if (encodingParams != null)
			{
				hash.BlockUpdate(encodingParams, 0, encodingParams.Length);
			}
			hash.DoFinal(this.defHash, 0);
		}

		// Token: 0x06003615 RID: 13845 RVA: 0x00152F9F File Offset: 0x0015119F
		public IAsymmetricBlockCipher GetUnderlyingCipher()
		{
			return this.engine;
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06003616 RID: 13846 RVA: 0x00152FA7 File Offset: 0x001511A7
		public string AlgorithmName
		{
			get
			{
				return this.engine.AlgorithmName + "/OAEPPadding";
			}
		}

		// Token: 0x06003617 RID: 13847 RVA: 0x00152FC0 File Offset: 0x001511C0
		public void Init(bool forEncryption, ICipherParameters param)
		{
			if (param is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)param;
				this.random = parametersWithRandom.Random;
			}
			else
			{
				this.random = new SecureRandom();
			}
			this.engine.Init(forEncryption, param);
			this.forEncryption = forEncryption;
		}

		// Token: 0x06003618 RID: 13848 RVA: 0x0015300C File Offset: 0x0015120C
		public int GetInputBlockSize()
		{
			int inputBlockSize = this.engine.GetInputBlockSize();
			if (this.forEncryption)
			{
				return inputBlockSize - 1 - 2 * this.defHash.Length;
			}
			return inputBlockSize;
		}

		// Token: 0x06003619 RID: 13849 RVA: 0x00153040 File Offset: 0x00151240
		public int GetOutputBlockSize()
		{
			int outputBlockSize = this.engine.GetOutputBlockSize();
			if (this.forEncryption)
			{
				return outputBlockSize;
			}
			return outputBlockSize - 1 - 2 * this.defHash.Length;
		}

		// Token: 0x0600361A RID: 13850 RVA: 0x00153071 File Offset: 0x00151271
		public byte[] ProcessBlock(byte[] inBytes, int inOff, int inLen)
		{
			if (this.forEncryption)
			{
				return this.EncodeBlock(inBytes, inOff, inLen);
			}
			return this.DecodeBlock(inBytes, inOff, inLen);
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x00153090 File Offset: 0x00151290
		private byte[] EncodeBlock(byte[] inBytes, int inOff, int inLen)
		{
			Check.DataLength(inLen > this.GetInputBlockSize(), "input data too long");
			byte[] array = new byte[this.GetInputBlockSize() + 1 + 2 * this.defHash.Length];
			Array.Copy(inBytes, inOff, array, array.Length - inLen, inLen);
			array[array.Length - inLen - 1] = 1;
			Array.Copy(this.defHash, 0, array, this.defHash.Length, this.defHash.Length);
			byte[] nextBytes = SecureRandom.GetNextBytes(this.random, this.defHash.Length);
			byte[] array2 = this.maskGeneratorFunction1(nextBytes, 0, nextBytes.Length, array.Length - this.defHash.Length);
			for (int num = this.defHash.Length; num != array.Length; num++)
			{
				byte[] array3 = array;
				int num2 = num;
				array3[num2] ^= array2[num - this.defHash.Length];
			}
			Array.Copy(nextBytes, 0, array, 0, this.defHash.Length);
			array2 = this.maskGeneratorFunction1(array, this.defHash.Length, array.Length - this.defHash.Length, this.defHash.Length);
			for (int num3 = 0; num3 != this.defHash.Length; num3++)
			{
				byte[] array4 = array;
				int num4 = num3;
				array4[num4] ^= array2[num3];
			}
			return this.engine.ProcessBlock(array, 0, array.Length);
		}

		// Token: 0x0600361C RID: 13852 RVA: 0x001531C4 File Offset: 0x001513C4
		private byte[] DecodeBlock(byte[] inBytes, int inOff, int inLen)
		{
			byte[] array = this.engine.ProcessBlock(inBytes, inOff, inLen);
			byte[] array2 = new byte[this.engine.GetOutputBlockSize()];
			bool flag = array2.Length < 2 * this.defHash.Length + 1;
			if (array.Length <= array2.Length)
			{
				Array.Copy(array, 0, array2, array2.Length - array.Length, array.Length);
			}
			else
			{
				Array.Copy(array, 0, array2, 0, array2.Length);
				flag = true;
			}
			byte[] array3 = this.maskGeneratorFunction1(array2, this.defHash.Length, array2.Length - this.defHash.Length, this.defHash.Length);
			for (int num = 0; num != this.defHash.Length; num++)
			{
				byte[] array4 = array2;
				int num2 = num;
				array4[num2] ^= array3[num];
			}
			array3 = this.maskGeneratorFunction1(array2, 0, this.defHash.Length, array2.Length - this.defHash.Length);
			for (int num3 = this.defHash.Length; num3 != array2.Length; num3++)
			{
				byte[] array5 = array2;
				int num4 = num3;
				array5[num4] ^= array3[num3 - this.defHash.Length];
			}
			bool flag2 = false;
			for (int num5 = 0; num5 != this.defHash.Length; num5++)
			{
				if (this.defHash[num5] != array2[this.defHash.Length + num5])
				{
					flag2 = true;
				}
			}
			int num6 = array2.Length;
			for (int num7 = 2 * this.defHash.Length; num7 != array2.Length; num7++)
			{
				if (array2[num7] > 0 & num6 == array2.Length)
				{
					num6 = num7;
				}
			}
			bool flag3 = num6 > array2.Length - 1 | array2[num6] != 1;
			num6++;
			if (flag2 || flag || flag3)
			{
				Arrays.Fill(array2, 0);
				throw new InvalidCipherTextException("data wrong");
			}
			byte[] array6 = new byte[array2.Length - num6];
			Array.Copy(array2, num6, array6, 0, array6.Length);
			return array6;
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x00122A78 File Offset: 0x00120C78
		private void ItoOSP(int i, byte[] sp)
		{
			sp[0] = (byte)((uint)i >> 24);
			sp[1] = (byte)((uint)i >> 16);
			sp[2] = (byte)((uint)i >> 8);
			sp[3] = (byte)i;
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x00153388 File Offset: 0x00151588
		private byte[] maskGeneratorFunction1(byte[] Z, int zOff, int zLen, int length)
		{
			byte[] array = new byte[length];
			byte[] array2 = new byte[this.mgf1Hash.GetDigestSize()];
			byte[] array3 = new byte[4];
			int i = 0;
			this.mgf1Hash.Reset();
			while (i < length / array2.Length)
			{
				this.ItoOSP(i, array3);
				this.mgf1Hash.BlockUpdate(Z, zOff, zLen);
				this.mgf1Hash.BlockUpdate(array3, 0, array3.Length);
				this.mgf1Hash.DoFinal(array2, 0);
				Array.Copy(array2, 0, array, i * array2.Length, array2.Length);
				i++;
			}
			if (i * array2.Length < length)
			{
				this.ItoOSP(i, array3);
				this.mgf1Hash.BlockUpdate(Z, zOff, zLen);
				this.mgf1Hash.BlockUpdate(array3, 0, array3.Length);
				this.mgf1Hash.DoFinal(array2, 0);
				Array.Copy(array2, 0, array, i * array2.Length, array.Length - i * array2.Length);
			}
			return array;
		}

		// Token: 0x0400226B RID: 8811
		private byte[] defHash;

		// Token: 0x0400226C RID: 8812
		private IDigest mgf1Hash;

		// Token: 0x0400226D RID: 8813
		private IAsymmetricBlockCipher engine;

		// Token: 0x0400226E RID: 8814
		private SecureRandom random;

		// Token: 0x0400226F RID: 8815
		private bool forEncryption;
	}
}
