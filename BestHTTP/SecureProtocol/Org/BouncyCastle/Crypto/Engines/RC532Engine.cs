using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200056E RID: 1390
	public class RC532Engine : IBlockCipher
	{
		// Token: 0x060034D3 RID: 13523 RVA: 0x0014771B File Offset: 0x0014591B
		public RC532Engine()
		{
			this._noRounds = 12;
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x060034D4 RID: 13524 RVA: 0x0014772B File Offset: 0x0014592B
		public virtual string AlgorithmName
		{
			get
			{
				return "RC5-32";
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x060034D5 RID: 13525 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060034D6 RID: 13526 RVA: 0x000FE681 File Offset: 0x000FC881
		public virtual int GetBlockSize()
		{
			return 8;
		}

		// Token: 0x060034D7 RID: 13527 RVA: 0x00147734 File Offset: 0x00145934
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (typeof(RC5Parameters).IsInstanceOfType(parameters))
			{
				RC5Parameters rc5Parameters = (RC5Parameters)parameters;
				this._noRounds = rc5Parameters.Rounds;
				this.SetKey(rc5Parameters.GetKey());
			}
			else
			{
				if (!typeof(KeyParameter).IsInstanceOfType(parameters))
				{
					throw new ArgumentException("invalid parameter passed to RC532 init - " + Platform.GetTypeName(parameters));
				}
				KeyParameter keyParameter = (KeyParameter)parameters;
				this.SetKey(keyParameter.GetKey());
			}
			this.forEncryption = forEncryption;
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x001477B8 File Offset: 0x001459B8
		public virtual int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (!this.forEncryption)
			{
				return this.DecryptBlock(input, inOff, output, outOff);
			}
			return this.EncryptBlock(input, inOff, output, outOff);
		}

		// Token: 0x060034D9 RID: 13529 RVA: 0x00002B75 File Offset: 0x00000D75
		public virtual void Reset()
		{
		}

		// Token: 0x060034DA RID: 13530 RVA: 0x001477DC File Offset: 0x001459DC
		private void SetKey(byte[] key)
		{
			int[] array = new int[(key.Length + 3) / 4];
			for (int num = 0; num != key.Length; num++)
			{
				array[num / 4] += (int)(key[num] & byte.MaxValue) << 8 * (num % 4);
			}
			this._S = new int[2 * (this._noRounds + 1)];
			this._S[0] = RC532Engine.P32;
			for (int i = 1; i < this._S.Length; i++)
			{
				this._S[i] = this._S[i - 1] + RC532Engine.Q32;
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
			for (int j = 0; j < num2; j++)
			{
				num3 = (this._S[num5] = this.RotateLeft(this._S[num5] + num3 + num4, 3));
				num4 = (array[num6] = this.RotateLeft(array[num6] + num3 + num4, num3 + num4));
				num5 = (num5 + 1) % this._S.Length;
				num6 = (num6 + 1) % array.Length;
			}
		}

		// Token: 0x060034DB RID: 13531 RVA: 0x00147910 File Offset: 0x00145B10
		private int EncryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			int num = this.BytesToWord(input, inOff) + this._S[0];
			int num2 = this.BytesToWord(input, inOff + 4) + this._S[1];
			for (int i = 1; i <= this._noRounds; i++)
			{
				num = this.RotateLeft(num ^ num2, num2) + this._S[2 * i];
				num2 = this.RotateLeft(num2 ^ num, num) + this._S[2 * i + 1];
			}
			this.WordToBytes(num, outBytes, outOff);
			this.WordToBytes(num2, outBytes, outOff + 4);
			return 8;
		}

		// Token: 0x060034DC RID: 13532 RVA: 0x0014799C File Offset: 0x00145B9C
		private int DecryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			int num = this.BytesToWord(input, inOff);
			int num2 = this.BytesToWord(input, inOff + 4);
			for (int i = this._noRounds; i >= 1; i--)
			{
				num2 = (this.RotateRight(num2 - this._S[2 * i + 1], num) ^ num);
				num = (this.RotateRight(num - this._S[2 * i], num2) ^ num2);
			}
			this.WordToBytes(num - this._S[0], outBytes, outOff);
			this.WordToBytes(num2 - this._S[1], outBytes, outOff + 4);
			return 8;
		}

		// Token: 0x060034DD RID: 13533 RVA: 0x00147A25 File Offset: 0x00145C25
		private int RotateLeft(int x, int y)
		{
			return x << y | (int)((uint)x >> 32 - (y & 31));
		}

		// Token: 0x060034DE RID: 13534 RVA: 0x00147A3D File Offset: 0x00145C3D
		private int RotateRight(int x, int y)
		{
			return (int)((uint)x >> y | (uint)((uint)x << 32 - (y & 31)));
		}

		// Token: 0x060034DF RID: 13535 RVA: 0x00147A55 File Offset: 0x00145C55
		private int BytesToWord(byte[] src, int srcOff)
		{
			return (int)(src[srcOff] & byte.MaxValue) | (int)(src[srcOff + 1] & byte.MaxValue) << 8 | (int)(src[srcOff + 2] & byte.MaxValue) << 16 | (int)(src[srcOff + 3] & byte.MaxValue) << 24;
		}

		// Token: 0x060034E0 RID: 13536 RVA: 0x00147A8C File Offset: 0x00145C8C
		private void WordToBytes(int word, byte[] dst, int dstOff)
		{
			dst[dstOff] = (byte)word;
			dst[dstOff + 1] = (byte)(word >> 8);
			dst[dstOff + 2] = (byte)(word >> 16);
			dst[dstOff + 3] = (byte)(word >> 24);
		}

		// Token: 0x0400219C RID: 8604
		private int _noRounds;

		// Token: 0x0400219D RID: 8605
		private int[] _S;

		// Token: 0x0400219E RID: 8606
		private static readonly int P32 = -1209970333;

		// Token: 0x0400219F RID: 8607
		private static readonly int Q32 = -1640531527;

		// Token: 0x040021A0 RID: 8608
		private bool forEncryption;
	}
}
