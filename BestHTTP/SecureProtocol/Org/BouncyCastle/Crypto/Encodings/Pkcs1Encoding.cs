using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Encodings
{
	// Token: 0x0200058A RID: 1418
	public class Pkcs1Encoding : IAsymmetricBlockCipher
	{
		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x0600361F RID: 13855 RVA: 0x00153469 File Offset: 0x00151669
		// (set) Token: 0x06003620 RID: 13856 RVA: 0x00153472 File Offset: 0x00151672
		public static bool StrictLengthEnabled
		{
			get
			{
				return Pkcs1Encoding.strictLengthEnabled[0];
			}
			set
			{
				Pkcs1Encoding.strictLengthEnabled[0] = value;
			}
		}

		// Token: 0x06003621 RID: 13857 RVA: 0x0015347C File Offset: 0x0015167C
		static Pkcs1Encoding()
		{
			string environmentVariable = Platform.GetEnvironmentVariable("BestHTTP.SecureProtocol.Org.BouncyCastle.Pkcs1.Strict");
			Pkcs1Encoding.strictLengthEnabled = new bool[]
			{
				environmentVariable == null || environmentVariable.Equals("true")
			};
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x001534B3 File Offset: 0x001516B3
		public Pkcs1Encoding(IAsymmetricBlockCipher cipher)
		{
			this.engine = cipher;
			this.useStrictLength = Pkcs1Encoding.StrictLengthEnabled;
		}

		// Token: 0x06003623 RID: 13859 RVA: 0x001534D4 File Offset: 0x001516D4
		public Pkcs1Encoding(IAsymmetricBlockCipher cipher, int pLen)
		{
			this.engine = cipher;
			this.useStrictLength = Pkcs1Encoding.StrictLengthEnabled;
			this.pLen = pLen;
		}

		// Token: 0x06003624 RID: 13860 RVA: 0x001534FC File Offset: 0x001516FC
		public Pkcs1Encoding(IAsymmetricBlockCipher cipher, byte[] fallback)
		{
			this.engine = cipher;
			this.useStrictLength = Pkcs1Encoding.StrictLengthEnabled;
			this.fallback = fallback;
			this.pLen = fallback.Length;
		}

		// Token: 0x06003625 RID: 13861 RVA: 0x0015352D File Offset: 0x0015172D
		public IAsymmetricBlockCipher GetUnderlyingCipher()
		{
			return this.engine;
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06003626 RID: 13862 RVA: 0x00153535 File Offset: 0x00151735
		public string AlgorithmName
		{
			get
			{
				return this.engine.AlgorithmName + "/PKCS1Padding";
			}
		}

		// Token: 0x06003627 RID: 13863 RVA: 0x0015354C File Offset: 0x0015174C
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			AsymmetricKeyParameter asymmetricKeyParameter;
			if (parameters is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
				this.random = parametersWithRandom.Random;
				asymmetricKeyParameter = (AsymmetricKeyParameter)parametersWithRandom.Parameters;
			}
			else
			{
				this.random = new SecureRandom();
				asymmetricKeyParameter = (AsymmetricKeyParameter)parameters;
			}
			this.engine.Init(forEncryption, parameters);
			this.forPrivateKey = asymmetricKeyParameter.IsPrivate;
			this.forEncryption = forEncryption;
			this.blockBuffer = new byte[this.engine.GetOutputBlockSize()];
			if (this.pLen > 0 && this.fallback == null && this.random == null)
			{
				throw new ArgumentException("encoder requires random");
			}
		}

		// Token: 0x06003628 RID: 13864 RVA: 0x001535F0 File Offset: 0x001517F0
		public int GetInputBlockSize()
		{
			int inputBlockSize = this.engine.GetInputBlockSize();
			if (!this.forEncryption)
			{
				return inputBlockSize;
			}
			return inputBlockSize - 10;
		}

		// Token: 0x06003629 RID: 13865 RVA: 0x00153618 File Offset: 0x00151818
		public int GetOutputBlockSize()
		{
			int outputBlockSize = this.engine.GetOutputBlockSize();
			if (!this.forEncryption)
			{
				return outputBlockSize - 10;
			}
			return outputBlockSize;
		}

		// Token: 0x0600362A RID: 13866 RVA: 0x0015363F File Offset: 0x0015183F
		public byte[] ProcessBlock(byte[] input, int inOff, int length)
		{
			if (!this.forEncryption)
			{
				return this.DecodeBlock(input, inOff, length);
			}
			return this.EncodeBlock(input, inOff, length);
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x0015365C File Offset: 0x0015185C
		private byte[] EncodeBlock(byte[] input, int inOff, int inLen)
		{
			if (inLen > this.GetInputBlockSize())
			{
				throw new ArgumentException("input data too large", "inLen");
			}
			byte[] array = new byte[this.engine.GetInputBlockSize()];
			if (this.forPrivateKey)
			{
				array[0] = 1;
				for (int num = 1; num != array.Length - inLen - 1; num++)
				{
					array[num] = byte.MaxValue;
				}
			}
			else
			{
				this.random.NextBytes(array);
				array[0] = 2;
				for (int num2 = 1; num2 != array.Length - inLen - 1; num2++)
				{
					while (array[num2] == 0)
					{
						array[num2] = (byte)this.random.NextInt();
					}
				}
			}
			array[array.Length - inLen - 1] = 0;
			Array.Copy(input, inOff, array, array.Length - inLen, inLen);
			return this.engine.ProcessBlock(array, 0, array.Length);
		}

		// Token: 0x0600362C RID: 13868 RVA: 0x0015371C File Offset: 0x0015191C
		private static int CheckPkcs1Encoding(byte[] encoded, int pLen)
		{
			int num = 0;
			num |= (int)(encoded[0] ^ 2);
			int num2 = encoded.Length - (pLen + 1);
			for (int i = 1; i < num2; i++)
			{
				int num3 = (int)encoded[i];
				num3 |= num3 >> 1;
				num3 |= num3 >> 2;
				num3 |= num3 >> 4;
				num |= (num3 & 1) - 1;
			}
			num |= (int)encoded[encoded.Length - (pLen + 1)];
			num |= num >> 1;
			num |= num >> 2;
			num |= num >> 4;
			return ~((num & 1) - 1);
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x0015378C File Offset: 0x0015198C
		private byte[] DecodeBlockOrRandom(byte[] input, int inOff, int inLen)
		{
			if (!this.forPrivateKey)
			{
				throw new InvalidCipherTextException("sorry, this method is only for decryption, not for signing");
			}
			byte[] array = this.engine.ProcessBlock(input, inOff, inLen);
			byte[] array2;
			if (this.fallback == null)
			{
				array2 = new byte[this.pLen];
				this.random.NextBytes(array2);
			}
			else
			{
				array2 = this.fallback;
			}
			byte[] array3 = (this.useStrictLength & array.Length != this.engine.GetOutputBlockSize()) ? this.blockBuffer : array;
			int num = Pkcs1Encoding.CheckPkcs1Encoding(array3, this.pLen);
			byte[] array4 = new byte[this.pLen];
			for (int i = 0; i < this.pLen; i++)
			{
				array4[i] = (byte)(((int)array3[i + (array3.Length - this.pLen)] & ~num) | ((int)array2[i] & num));
			}
			Arrays.Fill(array3, 0);
			return array4;
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x00153864 File Offset: 0x00151A64
		private byte[] DecodeBlock(byte[] input, int inOff, int inLen)
		{
			if (this.pLen != -1)
			{
				return this.DecodeBlockOrRandom(input, inOff, inLen);
			}
			byte[] array = this.engine.ProcessBlock(input, inOff, inLen);
			bool flag = this.useStrictLength & array.Length != this.engine.GetOutputBlockSize();
			byte[] array2;
			if (array.Length < this.GetOutputBlockSize())
			{
				array2 = this.blockBuffer;
			}
			else
			{
				array2 = array;
			}
			byte b = this.forPrivateKey ? 2 : 1;
			byte b2 = array2[0];
			bool flag2 = b2 != b;
			int num = this.FindStart(b2, array2);
			num++;
			if (flag2 | num < 10)
			{
				Arrays.Fill(array2, 0);
				throw new InvalidCipherTextException("block incorrect");
			}
			if (flag)
			{
				Arrays.Fill(array2, 0);
				throw new InvalidCipherTextException("block incorrect size");
			}
			byte[] array3 = new byte[array2.Length - num];
			Array.Copy(array2, num, array3, 0, array3.Length);
			return array3;
		}

		// Token: 0x0600362F RID: 13871 RVA: 0x0015393C File Offset: 0x00151B3C
		private int FindStart(byte type, byte[] block)
		{
			int num = -1;
			bool flag = false;
			for (int num2 = 1; num2 != block.Length; num2++)
			{
				byte b = block[num2];
				if (b == 0 & num < 0)
				{
					num = num2;
				}
				flag |= (type == 1 & num < 0 & b != byte.MaxValue);
			}
			if (!flag)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x04002270 RID: 8816
		public const string StrictLengthEnabledProperty = "BestHTTP.SecureProtocol.Org.BouncyCastle.Pkcs1.Strict";

		// Token: 0x04002271 RID: 8817
		private const int HeaderLength = 10;

		// Token: 0x04002272 RID: 8818
		private static readonly bool[] strictLengthEnabled;

		// Token: 0x04002273 RID: 8819
		private SecureRandom random;

		// Token: 0x04002274 RID: 8820
		private IAsymmetricBlockCipher engine;

		// Token: 0x04002275 RID: 8821
		private bool forEncryption;

		// Token: 0x04002276 RID: 8822
		private bool forPrivateKey;

		// Token: 0x04002277 RID: 8823
		private bool useStrictLength;

		// Token: 0x04002278 RID: 8824
		private int pLen = -1;

		// Token: 0x04002279 RID: 8825
		private byte[] fallback;

		// Token: 0x0400227A RID: 8826
		private byte[] blockBuffer;
	}
}
