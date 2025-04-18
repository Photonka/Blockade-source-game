﻿using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200056F RID: 1391
	public class RC564Engine : IBlockCipher
	{
		// Token: 0x060034E2 RID: 13538 RVA: 0x00147AC6 File Offset: 0x00145CC6
		public RC564Engine()
		{
			this._noRounds = 12;
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x060034E3 RID: 13539 RVA: 0x00147AD6 File Offset: 0x00145CD6
		public virtual string AlgorithmName
		{
			get
			{
				return "RC5-64";
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x060034E4 RID: 13540 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060034E5 RID: 13541 RVA: 0x00147ADD File Offset: 0x00145CDD
		public virtual int GetBlockSize()
		{
			return 2 * RC564Engine.bytesPerWord;
		}

		// Token: 0x060034E6 RID: 13542 RVA: 0x00147AE8 File Offset: 0x00145CE8
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!typeof(RC5Parameters).IsInstanceOfType(parameters))
			{
				throw new ArgumentException("invalid parameter passed to RC564 init - " + Platform.GetTypeName(parameters));
			}
			RC5Parameters rc5Parameters = (RC5Parameters)parameters;
			this.forEncryption = forEncryption;
			this._noRounds = rc5Parameters.Rounds;
			this.SetKey(rc5Parameters.GetKey());
		}

		// Token: 0x060034E7 RID: 13543 RVA: 0x00147B43 File Offset: 0x00145D43
		public virtual int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (!this.forEncryption)
			{
				return this.DecryptBlock(input, inOff, output, outOff);
			}
			return this.EncryptBlock(input, inOff, output, outOff);
		}

		// Token: 0x060034E8 RID: 13544 RVA: 0x00002B75 File Offset: 0x00000D75
		public virtual void Reset()
		{
		}

		// Token: 0x060034E9 RID: 13545 RVA: 0x00147B64 File Offset: 0x00145D64
		private void SetKey(byte[] key)
		{
			long[] array = new long[(key.Length + (RC564Engine.bytesPerWord - 1)) / RC564Engine.bytesPerWord];
			for (int num = 0; num != key.Length; num++)
			{
				array[num / RC564Engine.bytesPerWord] += (long)(key[num] & byte.MaxValue) << 8 * (num % RC564Engine.bytesPerWord);
			}
			this._S = new long[2 * (this._noRounds + 1)];
			this._S[0] = RC564Engine.P64;
			for (int i = 1; i < this._S.Length; i++)
			{
				this._S[i] = this._S[i - 1] + RC564Engine.Q64;
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
			long num3 = 0L;
			long num4 = 0L;
			int num5 = 0;
			int num6 = 0;
			for (int j = 0; j < num2; j++)
			{
				num3 = (this._S[num5] = this.RotateLeft(this._S[num5] + num3 + num4, 3L));
				num4 = (array[num6] = this.RotateLeft(array[num6] + num3 + num4, num3 + num4));
				num5 = (num5 + 1) % this._S.Length;
				num6 = (num6 + 1) % array.Length;
			}
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x00147CAC File Offset: 0x00145EAC
		private int EncryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			long num = this.BytesToWord(input, inOff) + this._S[0];
			long num2 = this.BytesToWord(input, inOff + RC564Engine.bytesPerWord) + this._S[1];
			for (int i = 1; i <= this._noRounds; i++)
			{
				num = this.RotateLeft(num ^ num2, num2) + this._S[2 * i];
				num2 = this.RotateLeft(num2 ^ num, num) + this._S[2 * i + 1];
			}
			this.WordToBytes(num, outBytes, outOff);
			this.WordToBytes(num2, outBytes, outOff + RC564Engine.bytesPerWord);
			return 2 * RC564Engine.bytesPerWord;
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x00147D44 File Offset: 0x00145F44
		private int DecryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			long num = this.BytesToWord(input, inOff);
			long num2 = this.BytesToWord(input, inOff + RC564Engine.bytesPerWord);
			for (int i = this._noRounds; i >= 1; i--)
			{
				num2 = (this.RotateRight(num2 - this._S[2 * i + 1], num) ^ num);
				num = (this.RotateRight(num - this._S[2 * i], num2) ^ num2);
			}
			this.WordToBytes(num - this._S[0], outBytes, outOff);
			this.WordToBytes(num2 - this._S[1], outBytes, outOff + RC564Engine.bytesPerWord);
			return 2 * RC564Engine.bytesPerWord;
		}

		// Token: 0x060034EC RID: 13548 RVA: 0x00147DDB File Offset: 0x00145FDB
		private long RotateLeft(long x, long y)
		{
			return x << (int)(y & (long)(RC564Engine.wordSize - 1)) | (long)((ulong)x >> (int)((long)RC564Engine.wordSize - (y & (long)(RC564Engine.wordSize - 1))));
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x00147E05 File Offset: 0x00146005
		private long RotateRight(long x, long y)
		{
			return (long)((ulong)x >> (int)(y & (long)(RC564Engine.wordSize - 1)) | (ulong)((ulong)x << (int)((long)RC564Engine.wordSize - (y & (long)(RC564Engine.wordSize - 1)))));
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x00147E30 File Offset: 0x00146030
		private long BytesToWord(byte[] src, int srcOff)
		{
			long num = 0L;
			for (int i = RC564Engine.bytesPerWord - 1; i >= 0; i--)
			{
				num = (num << 8) + (long)(src[i + srcOff] & byte.MaxValue);
			}
			return num;
		}

		// Token: 0x060034EF RID: 13551 RVA: 0x00147E64 File Offset: 0x00146064
		private void WordToBytes(long word, byte[] dst, int dstOff)
		{
			for (int i = 0; i < RC564Engine.bytesPerWord; i++)
			{
				dst[i + dstOff] = (byte)word;
				word = (long)((ulong)word >> 8);
			}
		}

		// Token: 0x040021A1 RID: 8609
		private static readonly int wordSize = 64;

		// Token: 0x040021A2 RID: 8610
		private static readonly int bytesPerWord = RC564Engine.wordSize / 8;

		// Token: 0x040021A3 RID: 8611
		private int _noRounds;

		// Token: 0x040021A4 RID: 8612
		private long[] _S;

		// Token: 0x040021A5 RID: 8613
		private static readonly long P64 = -5196783011329398165L;

		// Token: 0x040021A6 RID: 8614
		private static readonly long Q64 = -7046029254386353131L;

		// Token: 0x040021A7 RID: 8615
		private bool forEncryption;
	}
}
