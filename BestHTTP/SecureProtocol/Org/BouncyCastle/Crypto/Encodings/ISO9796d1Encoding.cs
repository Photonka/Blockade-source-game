using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Encodings
{
	// Token: 0x02000588 RID: 1416
	public class ISO9796d1Encoding : IAsymmetricBlockCipher
	{
		// Token: 0x06003605 RID: 13829 RVA: 0x00152A8F File Offset: 0x00150C8F
		public ISO9796d1Encoding(IAsymmetricBlockCipher cipher)
		{
			this.engine = cipher;
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06003606 RID: 13830 RVA: 0x00152A9E File Offset: 0x00150C9E
		public string AlgorithmName
		{
			get
			{
				return this.engine.AlgorithmName + "/ISO9796-1Padding";
			}
		}

		// Token: 0x06003607 RID: 13831 RVA: 0x00152AB5 File Offset: 0x00150CB5
		public IAsymmetricBlockCipher GetUnderlyingCipher()
		{
			return this.engine;
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x00152AC0 File Offset: 0x00150CC0
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			RsaKeyParameters rsaKeyParameters;
			if (parameters is ParametersWithRandom)
			{
				rsaKeyParameters = (RsaKeyParameters)((ParametersWithRandom)parameters).Parameters;
			}
			else
			{
				rsaKeyParameters = (RsaKeyParameters)parameters;
			}
			this.engine.Init(forEncryption, parameters);
			this.modulus = rsaKeyParameters.Modulus;
			this.bitSize = this.modulus.BitLength;
			this.forEncryption = forEncryption;
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x00152B20 File Offset: 0x00150D20
		public int GetInputBlockSize()
		{
			int inputBlockSize = this.engine.GetInputBlockSize();
			if (this.forEncryption)
			{
				return (inputBlockSize + 1) / 2;
			}
			return inputBlockSize;
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x00152B48 File Offset: 0x00150D48
		public int GetOutputBlockSize()
		{
			int outputBlockSize = this.engine.GetOutputBlockSize();
			if (this.forEncryption)
			{
				return outputBlockSize;
			}
			return (outputBlockSize + 1) / 2;
		}

		// Token: 0x0600360B RID: 13835 RVA: 0x00152B70 File Offset: 0x00150D70
		public void SetPadBits(int padBits)
		{
			if (padBits > 7)
			{
				throw new ArgumentException("padBits > 7");
			}
			this.padBits = padBits;
		}

		// Token: 0x0600360C RID: 13836 RVA: 0x00152B88 File Offset: 0x00150D88
		public int GetPadBits()
		{
			return this.padBits;
		}

		// Token: 0x0600360D RID: 13837 RVA: 0x00152B90 File Offset: 0x00150D90
		public byte[] ProcessBlock(byte[] input, int inOff, int length)
		{
			if (this.forEncryption)
			{
				return this.EncodeBlock(input, inOff, length);
			}
			return this.DecodeBlock(input, inOff, length);
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x00152BB0 File Offset: 0x00150DB0
		private byte[] EncodeBlock(byte[] input, int inOff, int inLen)
		{
			byte[] array = new byte[(this.bitSize + 7) / 8];
			int num = this.padBits + 1;
			int num2 = (this.bitSize + 13) / 16;
			for (int i = 0; i < num2; i += inLen)
			{
				if (i > num2 - inLen)
				{
					Array.Copy(input, inOff + inLen - (num2 - i), array, array.Length - num2, num2 - i);
				}
				else
				{
					Array.Copy(input, inOff, array, array.Length - (i + inLen), inLen);
				}
			}
			for (int num3 = array.Length - 2 * num2; num3 != array.Length; num3 += 2)
			{
				byte b = array[array.Length - num2 + num3 / 2];
				array[num3] = (byte)((int)ISO9796d1Encoding.shadows[(int)((uint)(b & byte.MaxValue) >> 4)] << 4 | (int)ISO9796d1Encoding.shadows[(int)(b & 15)]);
				array[num3 + 1] = b;
			}
			byte[] array2 = array;
			int num4 = array.Length - 2 * inLen;
			array2[num4] ^= (byte)num;
			array[array.Length - 1] = (byte)((int)array[array.Length - 1] << 4 | 6);
			int num5 = 8 - (this.bitSize - 1) % 8;
			int num6 = 0;
			if (num5 != 8)
			{
				byte[] array3 = array;
				int num7 = 0;
				array3[num7] &= (byte)(255 >> num5);
				byte[] array4 = array;
				int num8 = 0;
				array4[num8] |= (byte)(128 >> num5);
			}
			else
			{
				array[0] = 0;
				byte[] array5 = array;
				int num9 = 1;
				array5[num9] |= 128;
				num6 = 1;
			}
			return this.engine.ProcessBlock(array, num6, array.Length - num6);
		}

		// Token: 0x0600360F RID: 13839 RVA: 0x00152D14 File Offset: 0x00150F14
		private byte[] DecodeBlock(byte[] input, int inOff, int inLen)
		{
			byte[] array = this.engine.ProcessBlock(input, inOff, inLen);
			int num = 1;
			int num2 = (this.bitSize + 13) / 16;
			BigInteger bigInteger = new BigInteger(1, array);
			BigInteger bigInteger2;
			if (bigInteger.Mod(ISO9796d1Encoding.Sixteen).Equals(ISO9796d1Encoding.Six))
			{
				bigInteger2 = bigInteger;
			}
			else
			{
				bigInteger2 = this.modulus.Subtract(bigInteger);
				if (!bigInteger2.Mod(ISO9796d1Encoding.Sixteen).Equals(ISO9796d1Encoding.Six))
				{
					throw new InvalidCipherTextException("resulting integer iS or (modulus - iS) is not congruent to 6 mod 16");
				}
			}
			array = bigInteger2.ToByteArrayUnsigned();
			if ((array[array.Length - 1] & 15) != 6)
			{
				throw new InvalidCipherTextException("invalid forcing byte in block");
			}
			array[array.Length - 1] = (byte)((ushort)(array[array.Length - 1] & byte.MaxValue) >> 4 | (int)ISO9796d1Encoding.inverse[(array[array.Length - 2] & byte.MaxValue) >> 4] << 4);
			array[0] = (byte)((int)ISO9796d1Encoding.shadows[(int)((uint)(array[1] & byte.MaxValue) >> 4)] << 4 | (int)ISO9796d1Encoding.shadows[(int)(array[1] & 15)]);
			bool flag = false;
			int num3 = 0;
			for (int i = array.Length - 1; i >= array.Length - 2 * num2; i -= 2)
			{
				int num4 = (int)ISO9796d1Encoding.shadows[(int)((uint)(array[i] & byte.MaxValue) >> 4)] << 4 | (int)ISO9796d1Encoding.shadows[(int)(array[i] & 15)];
				if ((((int)array[i - 1] ^ num4) & 255) != 0)
				{
					if (flag)
					{
						throw new InvalidCipherTextException("invalid tsums in block");
					}
					flag = true;
					num = (((int)array[i - 1] ^ num4) & 255);
					num3 = i - 1;
				}
			}
			array[num3] = 0;
			byte[] array2 = new byte[(array.Length - num3) / 2];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = array[2 * j + num3 + 1];
			}
			this.padBits = num - 1;
			return array2;
		}

		// Token: 0x04002262 RID: 8802
		private static readonly BigInteger Sixteen = BigInteger.ValueOf(16L);

		// Token: 0x04002263 RID: 8803
		private static readonly BigInteger Six = BigInteger.ValueOf(6L);

		// Token: 0x04002264 RID: 8804
		private static readonly byte[] shadows = new byte[]
		{
			14,
			3,
			5,
			8,
			9,
			4,
			2,
			15,
			0,
			13,
			11,
			6,
			7,
			10,
			12,
			1
		};

		// Token: 0x04002265 RID: 8805
		private static readonly byte[] inverse = new byte[]
		{
			8,
			15,
			6,
			1,
			5,
			2,
			11,
			12,
			3,
			4,
			13,
			10,
			14,
			9,
			0,
			7
		};

		// Token: 0x04002266 RID: 8806
		private readonly IAsymmetricBlockCipher engine;

		// Token: 0x04002267 RID: 8807
		private bool forEncryption;

		// Token: 0x04002268 RID: 8808
		private int bitSize;

		// Token: 0x04002269 RID: 8809
		private int padBits;

		// Token: 0x0400226A RID: 8810
		private BigInteger modulus;
	}
}
