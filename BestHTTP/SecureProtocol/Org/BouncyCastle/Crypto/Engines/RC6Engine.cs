using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000570 RID: 1392
	public class RC6Engine : IBlockCipher
	{
		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x060034F2 RID: 13554 RVA: 0x00147EBE File Offset: 0x001460BE
		public virtual string AlgorithmName
		{
			get
			{
				return "RC6";
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x060034F3 RID: 13555 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060034F4 RID: 13556 RVA: 0x00147EC5 File Offset: 0x001460C5
		public virtual int GetBlockSize()
		{
			return 4 * RC6Engine.bytesPerWord;
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x00147ED0 File Offset: 0x001460D0
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("invalid parameter passed to RC6 init - " + Platform.GetTypeName(parameters));
			}
			this.forEncryption = forEncryption;
			KeyParameter keyParameter = (KeyParameter)parameters;
			this.SetKey(keyParameter.GetKey());
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x00147F18 File Offset: 0x00146118
		public virtual int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			int blockSize = this.GetBlockSize();
			if (this._S == null)
			{
				throw new InvalidOperationException("RC6 engine not initialised");
			}
			Check.DataLength(input, inOff, blockSize, "input buffer too short");
			Check.OutputLength(output, outOff, blockSize, "output buffer too short");
			if (!this.forEncryption)
			{
				return this.DecryptBlock(input, inOff, output, outOff);
			}
			return this.EncryptBlock(input, inOff, output, outOff);
		}

		// Token: 0x060034F7 RID: 13559 RVA: 0x00002B75 File Offset: 0x00000D75
		public virtual void Reset()
		{
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x00147F7C File Offset: 0x0014617C
		private void SetKey(byte[] key)
		{
			int num = (key.Length + (RC6Engine.bytesPerWord - 1)) / RC6Engine.bytesPerWord;
			int[] array = new int[(key.Length + RC6Engine.bytesPerWord - 1) / RC6Engine.bytesPerWord];
			for (int i = key.Length - 1; i >= 0; i--)
			{
				array[i / RC6Engine.bytesPerWord] = (array[i / RC6Engine.bytesPerWord] << 8) + (int)(key[i] & byte.MaxValue);
			}
			this._S = new int[2 + 2 * RC6Engine._noRounds + 2];
			this._S[0] = RC6Engine.P32;
			for (int j = 1; j < this._S.Length; j++)
			{
				this._S[j] = this._S[j - 1] + RC6Engine.Q32;
			}
			int num2;
			if (array.Length > this._S.Length)
			{
				num2 = 3 * array.Length;
			}
			else
			{
				num2 = 3 * this._S.Length;
			}
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			for (int k = 0; k < num2; k++)
			{
				num3 = (this._S[num5] = this.RotateLeft(this._S[num5] + num3 + num4, 3));
				num4 = (array[num6] = this.RotateLeft(array[num6] + num3 + num4, num3 + num4));
				num5 = (num5 + 1) % this._S.Length;
				num6 = (num6 + 1) % array.Length;
			}
		}

		// Token: 0x060034F9 RID: 13561 RVA: 0x001480CC File Offset: 0x001462CC
		private int EncryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			int num = this.BytesToWord(input, inOff);
			int num2 = this.BytesToWord(input, inOff + RC6Engine.bytesPerWord);
			int num3 = this.BytesToWord(input, inOff + RC6Engine.bytesPerWord * 2);
			int num4 = this.BytesToWord(input, inOff + RC6Engine.bytesPerWord * 3);
			num2 += this._S[0];
			num4 += this._S[1];
			for (int i = 1; i <= RC6Engine._noRounds; i++)
			{
				int num5 = num2 * (2 * num2 + 1);
				num5 = this.RotateLeft(num5, 5);
				int num6 = num4 * (2 * num4 + 1);
				num6 = this.RotateLeft(num6, 5);
				num ^= num5;
				num = this.RotateLeft(num, num6);
				num += this._S[2 * i];
				num3 ^= num6;
				num3 = this.RotateLeft(num3, num5);
				num3 += this._S[2 * i + 1];
				int num7 = num;
				num = num2;
				num2 = num3;
				num3 = num4;
				num4 = num7;
			}
			num += this._S[2 * RC6Engine._noRounds + 2];
			num3 += this._S[2 * RC6Engine._noRounds + 3];
			this.WordToBytes(num, outBytes, outOff);
			this.WordToBytes(num2, outBytes, outOff + RC6Engine.bytesPerWord);
			this.WordToBytes(num3, outBytes, outOff + RC6Engine.bytesPerWord * 2);
			this.WordToBytes(num4, outBytes, outOff + RC6Engine.bytesPerWord * 3);
			return 4 * RC6Engine.bytesPerWord;
		}

		// Token: 0x060034FA RID: 13562 RVA: 0x00148220 File Offset: 0x00146420
		private int DecryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			int num = this.BytesToWord(input, inOff);
			int num2 = this.BytesToWord(input, inOff + RC6Engine.bytesPerWord);
			int num3 = this.BytesToWord(input, inOff + RC6Engine.bytesPerWord * 2);
			int num4 = this.BytesToWord(input, inOff + RC6Engine.bytesPerWord * 3);
			num3 -= this._S[2 * RC6Engine._noRounds + 3];
			num -= this._S[2 * RC6Engine._noRounds + 2];
			for (int i = RC6Engine._noRounds; i >= 1; i--)
			{
				int num5 = num4;
				num4 = num3;
				num3 = num2;
				num2 = num;
				num = num5;
				int num6 = num2 * (2 * num2 + 1);
				num6 = this.RotateLeft(num6, RC6Engine.LGW);
				int num7 = num4 * (2 * num4 + 1);
				num7 = this.RotateLeft(num7, RC6Engine.LGW);
				num3 -= this._S[2 * i + 1];
				num3 = this.RotateRight(num3, num6);
				num3 ^= num7;
				num -= this._S[2 * i];
				num = this.RotateRight(num, num7);
				num ^= num6;
			}
			num4 -= this._S[1];
			num2 -= this._S[0];
			this.WordToBytes(num, outBytes, outOff);
			this.WordToBytes(num2, outBytes, outOff + RC6Engine.bytesPerWord);
			this.WordToBytes(num3, outBytes, outOff + RC6Engine.bytesPerWord * 2);
			this.WordToBytes(num4, outBytes, outOff + RC6Engine.bytesPerWord * 3);
			return 4 * RC6Engine.bytesPerWord;
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x0014837C File Offset: 0x0014657C
		private int RotateLeft(int x, int y)
		{
			return x << (y & RC6Engine.wordSize - 1) | (int)((uint)x >> RC6Engine.wordSize - (y & RC6Engine.wordSize - 1));
		}

		// Token: 0x060034FC RID: 13564 RVA: 0x001483A1 File Offset: 0x001465A1
		private int RotateRight(int x, int y)
		{
			return (int)((uint)x >> (y & RC6Engine.wordSize - 1) | (uint)((uint)x << RC6Engine.wordSize - (y & RC6Engine.wordSize - 1)));
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x001483C8 File Offset: 0x001465C8
		private int BytesToWord(byte[] src, int srcOff)
		{
			int num = 0;
			for (int i = RC6Engine.bytesPerWord - 1; i >= 0; i--)
			{
				num = (num << 8) + (int)(src[i + srcOff] & byte.MaxValue);
			}
			return num;
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x001483FC File Offset: 0x001465FC
		private void WordToBytes(int word, byte[] dst, int dstOff)
		{
			for (int i = 0; i < RC6Engine.bytesPerWord; i++)
			{
				dst[i + dstOff] = (byte)word;
				word = (int)((uint)word >> 8);
			}
		}

		// Token: 0x040021A8 RID: 8616
		private static readonly int wordSize = 32;

		// Token: 0x040021A9 RID: 8617
		private static readonly int bytesPerWord = RC6Engine.wordSize / 8;

		// Token: 0x040021AA RID: 8618
		private static readonly int _noRounds = 20;

		// Token: 0x040021AB RID: 8619
		private int[] _S;

		// Token: 0x040021AC RID: 8620
		private static readonly int P32 = -1209970333;

		// Token: 0x040021AD RID: 8621
		private static readonly int Q32 = -1640531527;

		// Token: 0x040021AE RID: 8622
		private static readonly int LGW = 5;

		// Token: 0x040021AF RID: 8623
		private bool forEncryption;
	}
}
