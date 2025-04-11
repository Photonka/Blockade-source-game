using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000581 RID: 1409
	public class ThreefishEngine : IBlockCipher
	{
		// Token: 0x060035BE RID: 13758 RVA: 0x0014E848 File Offset: 0x0014CA48
		static ThreefishEngine()
		{
			for (int i = 0; i < ThreefishEngine.MOD9.Length; i++)
			{
				ThreefishEngine.MOD17[i] = i % 17;
				ThreefishEngine.MOD9[i] = i % 9;
				ThreefishEngine.MOD5[i] = i % 5;
				ThreefishEngine.MOD3[i] = i % 3;
			}
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x0014E8D0 File Offset: 0x0014CAD0
		public ThreefishEngine(int blocksizeBits)
		{
			this.blocksizeBytes = blocksizeBits / 8;
			this.blocksizeWords = this.blocksizeBytes / 8;
			this.currentBlock = new ulong[this.blocksizeWords];
			this.kw = new ulong[2 * this.blocksizeWords + 1];
			if (blocksizeBits == 256)
			{
				this.cipher = new ThreefishEngine.Threefish256Cipher(this.kw, this.t);
				return;
			}
			if (blocksizeBits == 512)
			{
				this.cipher = new ThreefishEngine.Threefish512Cipher(this.kw, this.t);
				return;
			}
			if (blocksizeBits != 1024)
			{
				throw new ArgumentException("Invalid blocksize - Threefish is defined with block size of 256, 512, or 1024 bits");
			}
			this.cipher = new ThreefishEngine.Threefish1024Cipher(this.kw, this.t);
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x0014E998 File Offset: 0x0014CB98
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			byte[] key;
			byte[] array;
			if (parameters is TweakableBlockCipherParameters)
			{
				TweakableBlockCipherParameters tweakableBlockCipherParameters = (TweakableBlockCipherParameters)parameters;
				key = tweakableBlockCipherParameters.Key.GetKey();
				array = tweakableBlockCipherParameters.Tweak;
			}
			else
			{
				if (!(parameters is KeyParameter))
				{
					throw new ArgumentException("Invalid parameter passed to Threefish init - " + Platform.GetTypeName(parameters));
				}
				key = ((KeyParameter)parameters).GetKey();
				array = null;
			}
			ulong[] array2 = null;
			ulong[] tweak = null;
			if (key != null)
			{
				if (key.Length != this.blocksizeBytes)
				{
					throw new ArgumentException("Threefish key must be same size as block (" + this.blocksizeBytes + " bytes)");
				}
				array2 = new ulong[this.blocksizeWords];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = ThreefishEngine.BytesToWord(key, i * 8);
				}
			}
			if (array != null)
			{
				if (array.Length != 16)
				{
					throw new ArgumentException("Threefish tweak must be " + 16 + " bytes");
				}
				tweak = new ulong[]
				{
					ThreefishEngine.BytesToWord(array, 0),
					ThreefishEngine.BytesToWord(array, 8)
				};
			}
			this.Init(forEncryption, array2, tweak);
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x0014EA9D File Offset: 0x0014CC9D
		internal void Init(bool forEncryption, ulong[] key, ulong[] tweak)
		{
			this.forEncryption = forEncryption;
			if (key != null)
			{
				this.SetKey(key);
			}
			if (tweak != null)
			{
				this.SetTweak(tweak);
			}
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x0014EABC File Offset: 0x0014CCBC
		private void SetKey(ulong[] key)
		{
			if (key.Length != this.blocksizeWords)
			{
				throw new ArgumentException("Threefish key must be same size as block (" + this.blocksizeWords + " words)");
			}
			ulong num = 2004413935125273122UL;
			for (int i = 0; i < this.blocksizeWords; i++)
			{
				this.kw[i] = key[i];
				num ^= this.kw[i];
			}
			this.kw[this.blocksizeWords] = num;
			Array.Copy(this.kw, 0, this.kw, this.blocksizeWords + 1, this.blocksizeWords);
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x0014EB54 File Offset: 0x0014CD54
		private void SetTweak(ulong[] tweak)
		{
			if (tweak.Length != 2)
			{
				throw new ArgumentException("Tweak must be " + 2 + " words.");
			}
			this.t[0] = tweak[0];
			this.t[1] = tweak[1];
			this.t[2] = (this.t[0] ^ this.t[1]);
			this.t[3] = this.t[0];
			this.t[4] = this.t[1];
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x060035C4 RID: 13764 RVA: 0x0014EBD1 File Offset: 0x0014CDD1
		public virtual string AlgorithmName
		{
			get
			{
				return "Threefish-" + this.blocksizeBytes * 8;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x060035C5 RID: 13765 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x0014EBEA File Offset: 0x0014CDEA
		public virtual int GetBlockSize()
		{
			return this.blocksizeBytes;
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x00002B75 File Offset: 0x00000D75
		public virtual void Reset()
		{
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x0014EBF4 File Offset: 0x0014CDF4
		public virtual int ProcessBlock(byte[] inBytes, int inOff, byte[] outBytes, int outOff)
		{
			if (outOff + this.blocksizeBytes > outBytes.Length)
			{
				throw new DataLengthException("Output buffer too short");
			}
			if (inOff + this.blocksizeBytes > inBytes.Length)
			{
				throw new DataLengthException("Input buffer too short");
			}
			for (int i = 0; i < this.blocksizeBytes; i += 8)
			{
				this.currentBlock[i >> 3] = ThreefishEngine.BytesToWord(inBytes, inOff + i);
			}
			this.ProcessBlock(this.currentBlock, this.currentBlock);
			for (int j = 0; j < this.blocksizeBytes; j += 8)
			{
				ThreefishEngine.WordToBytes(this.currentBlock[j >> 3], outBytes, outOff + j);
			}
			return this.blocksizeBytes;
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x0014EC94 File Offset: 0x0014CE94
		internal int ProcessBlock(ulong[] inWords, ulong[] outWords)
		{
			if (this.kw[this.blocksizeWords] == 0UL)
			{
				throw new InvalidOperationException("Threefish engine not initialised");
			}
			if (inWords.Length != this.blocksizeWords)
			{
				throw new DataLengthException("Input buffer too short");
			}
			if (outWords.Length != this.blocksizeWords)
			{
				throw new DataLengthException("Output buffer too short");
			}
			if (this.forEncryption)
			{
				this.cipher.EncryptBlock(inWords, outWords);
			}
			else
			{
				this.cipher.DecryptBlock(inWords, outWords);
			}
			return this.blocksizeWords;
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x0014ED14 File Offset: 0x0014CF14
		internal static ulong BytesToWord(byte[] bytes, int off)
		{
			if (off + 8 > bytes.Length)
			{
				throw new ArgumentException();
			}
			int num = off + 1;
			return ((ulong)bytes[off] & 255UL) | ((ulong)bytes[num++] & 255UL) << 8 | ((ulong)bytes[num++] & 255UL) << 16 | ((ulong)bytes[num++] & 255UL) << 24 | ((ulong)bytes[num++] & 255UL) << 32 | ((ulong)bytes[num++] & 255UL) << 40 | ((ulong)bytes[num++] & 255UL) << 48 | ((ulong)bytes[num++] & 255UL) << 56;
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x0014EDC4 File Offset: 0x0014CFC4
		internal static void WordToBytes(ulong word, byte[] bytes, int off)
		{
			if (off + 8 > bytes.Length)
			{
				throw new ArgumentException();
			}
			int num = off + 1;
			bytes[off] = (byte)word;
			bytes[num++] = (byte)(word >> 8);
			bytes[num++] = (byte)(word >> 16);
			bytes[num++] = (byte)(word >> 24);
			bytes[num++] = (byte)(word >> 32);
			bytes[num++] = (byte)(word >> 40);
			bytes[num++] = (byte)(word >> 48);
			bytes[num++] = (byte)(word >> 56);
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x0014EE3D File Offset: 0x0014D03D
		private static ulong RotlXor(ulong x, int n, ulong xor)
		{
			return (x << n | x >> 64 - n) ^ xor;
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x0014EE54 File Offset: 0x0014D054
		private static ulong XorRotr(ulong x, int n, ulong xor)
		{
			ulong num = x ^ xor;
			return num >> n | num << 64 - n;
		}

		// Token: 0x04002213 RID: 8723
		public const int BLOCKSIZE_256 = 256;

		// Token: 0x04002214 RID: 8724
		public const int BLOCKSIZE_512 = 512;

		// Token: 0x04002215 RID: 8725
		public const int BLOCKSIZE_1024 = 1024;

		// Token: 0x04002216 RID: 8726
		private const int TWEAK_SIZE_BYTES = 16;

		// Token: 0x04002217 RID: 8727
		private const int TWEAK_SIZE_WORDS = 2;

		// Token: 0x04002218 RID: 8728
		private const int ROUNDS_256 = 72;

		// Token: 0x04002219 RID: 8729
		private const int ROUNDS_512 = 72;

		// Token: 0x0400221A RID: 8730
		private const int ROUNDS_1024 = 80;

		// Token: 0x0400221B RID: 8731
		private const int MAX_ROUNDS = 80;

		// Token: 0x0400221C RID: 8732
		private const ulong C_240 = 2004413935125273122UL;

		// Token: 0x0400221D RID: 8733
		private static readonly int[] MOD9 = new int[80];

		// Token: 0x0400221E RID: 8734
		private static readonly int[] MOD17 = new int[ThreefishEngine.MOD9.Length];

		// Token: 0x0400221F RID: 8735
		private static readonly int[] MOD5 = new int[ThreefishEngine.MOD9.Length];

		// Token: 0x04002220 RID: 8736
		private static readonly int[] MOD3 = new int[ThreefishEngine.MOD9.Length];

		// Token: 0x04002221 RID: 8737
		private readonly int blocksizeBytes;

		// Token: 0x04002222 RID: 8738
		private readonly int blocksizeWords;

		// Token: 0x04002223 RID: 8739
		private readonly ulong[] currentBlock;

		// Token: 0x04002224 RID: 8740
		private readonly ulong[] t = new ulong[5];

		// Token: 0x04002225 RID: 8741
		private readonly ulong[] kw;

		// Token: 0x04002226 RID: 8742
		private readonly ThreefishEngine.ThreefishCipher cipher;

		// Token: 0x04002227 RID: 8743
		private bool forEncryption;

		// Token: 0x02000930 RID: 2352
		private abstract class ThreefishCipher
		{
			// Token: 0x06004E5B RID: 20059 RVA: 0x001B3B36 File Offset: 0x001B1D36
			protected ThreefishCipher(ulong[] kw, ulong[] t)
			{
				this.kw = kw;
				this.t = t;
			}

			// Token: 0x06004E5C RID: 20060
			internal abstract void EncryptBlock(ulong[] block, ulong[] outWords);

			// Token: 0x06004E5D RID: 20061
			internal abstract void DecryptBlock(ulong[] block, ulong[] outWords);

			// Token: 0x0400350B RID: 13579
			protected readonly ulong[] t;

			// Token: 0x0400350C RID: 13580
			protected readonly ulong[] kw;
		}

		// Token: 0x02000931 RID: 2353
		private sealed class Threefish256Cipher : ThreefishEngine.ThreefishCipher
		{
			// Token: 0x06004E5E RID: 20062 RVA: 0x001B3B4C File Offset: 0x001B1D4C
			public Threefish256Cipher(ulong[] kw, ulong[] t) : base(kw, t)
			{
			}

			// Token: 0x06004E5F RID: 20063 RVA: 0x001B3B58 File Offset: 0x001B1D58
			internal override void EncryptBlock(ulong[] block, ulong[] outWords)
			{
				ulong[] kw = this.kw;
				ulong[] t = this.t;
				int[] mod = ThreefishEngine.MOD5;
				int[] mod2 = ThreefishEngine.MOD3;
				if (kw.Length != 9)
				{
					throw new ArgumentException();
				}
				if (t.Length != 5)
				{
					throw new ArgumentException();
				}
				ulong num = block[0];
				ulong num2 = block[1];
				ulong num3 = block[2];
				ulong num4 = block[3];
				num += kw[0];
				num2 += kw[1] + t[0];
				num3 += kw[2] + t[1];
				num4 += kw[3];
				for (int i = 1; i < 18; i += 2)
				{
					int num5 = mod[i];
					int num6 = mod2[i];
					num2 = ThreefishEngine.RotlXor(num2, 14, num += num2);
					num4 = ThreefishEngine.RotlXor(num4, 16, num3 += num4);
					num4 = ThreefishEngine.RotlXor(num4, 52, num += num4);
					num2 = ThreefishEngine.RotlXor(num2, 57, num3 += num2);
					num2 = ThreefishEngine.RotlXor(num2, 23, num += num2);
					num4 = ThreefishEngine.RotlXor(num4, 40, num3 += num4);
					num4 = ThreefishEngine.RotlXor(num4, 5, num += num4);
					num2 = ThreefishEngine.RotlXor(num2, 37, num3 += num2);
					num += kw[num5];
					num2 += kw[num5 + 1] + t[num6];
					num3 += kw[num5 + 2] + t[num6 + 1];
					num4 += kw[num5 + 3] + (ulong)i;
					num2 = ThreefishEngine.RotlXor(num2, 25, num += num2);
					num4 = ThreefishEngine.RotlXor(num4, 33, num3 += num4);
					num4 = ThreefishEngine.RotlXor(num4, 46, num += num4);
					num2 = ThreefishEngine.RotlXor(num2, 12, num3 += num2);
					num2 = ThreefishEngine.RotlXor(num2, 58, num += num2);
					num4 = ThreefishEngine.RotlXor(num4, 22, num3 += num4);
					num4 = ThreefishEngine.RotlXor(num4, 32, num += num4);
					num2 = ThreefishEngine.RotlXor(num2, 32, num3 += num2);
					num += kw[num5 + 1];
					num2 += kw[num5 + 2] + t[num6 + 1];
					num3 += kw[num5 + 3] + t[num6 + 2];
					num4 += kw[num5 + 4] + (ulong)i + 1UL;
				}
				outWords[0] = num;
				outWords[1] = num2;
				outWords[2] = num3;
				outWords[3] = num4;
			}

			// Token: 0x06004E60 RID: 20064 RVA: 0x001B3DB8 File Offset: 0x001B1FB8
			internal override void DecryptBlock(ulong[] block, ulong[] state)
			{
				ulong[] kw = this.kw;
				ulong[] t = this.t;
				int[] mod = ThreefishEngine.MOD5;
				int[] mod2 = ThreefishEngine.MOD3;
				if (kw.Length != 9)
				{
					throw new ArgumentException();
				}
				if (t.Length != 5)
				{
					throw new ArgumentException();
				}
				ulong num = block[0];
				ulong num2 = block[1];
				ulong num3 = block[2];
				ulong num4 = block[3];
				for (int i = 17; i >= 1; i -= 2)
				{
					int num5 = mod[i];
					int num6 = mod2[i];
					num -= kw[num5 + 1];
					num2 -= kw[num5 + 2] + t[num6 + 1];
					num3 -= kw[num5 + 3] + t[num6 + 2];
					num4 -= kw[num5 + 4] + (ulong)i + 1UL;
					num4 = ThreefishEngine.XorRotr(num4, 32, num);
					num -= num4;
					num2 = ThreefishEngine.XorRotr(num2, 32, num3);
					num3 -= num2;
					num2 = ThreefishEngine.XorRotr(num2, 58, num);
					num -= num2;
					num4 = ThreefishEngine.XorRotr(num4, 22, num3);
					num3 -= num4;
					num4 = ThreefishEngine.XorRotr(num4, 46, num);
					num -= num4;
					num2 = ThreefishEngine.XorRotr(num2, 12, num3);
					num3 -= num2;
					num2 = ThreefishEngine.XorRotr(num2, 25, num);
					num -= num2;
					num4 = ThreefishEngine.XorRotr(num4, 33, num3);
					num3 -= num4;
					num -= kw[num5];
					num2 -= kw[num5 + 1] + t[num6];
					num3 -= kw[num5 + 2] + t[num6 + 1];
					num4 -= kw[num5 + 3] + (ulong)i;
					num4 = ThreefishEngine.XorRotr(num4, 5, num);
					num -= num4;
					num2 = ThreefishEngine.XorRotr(num2, 37, num3);
					num3 -= num2;
					num2 = ThreefishEngine.XorRotr(num2, 23, num);
					num -= num2;
					num4 = ThreefishEngine.XorRotr(num4, 40, num3);
					num3 -= num4;
					num4 = ThreefishEngine.XorRotr(num4, 52, num);
					num -= num4;
					num2 = ThreefishEngine.XorRotr(num2, 57, num3);
					num3 -= num2;
					num2 = ThreefishEngine.XorRotr(num2, 14, num);
					num -= num2;
					num4 = ThreefishEngine.XorRotr(num4, 16, num3);
					num3 -= num4;
				}
				num -= kw[0];
				num2 -= kw[1] + t[0];
				num3 -= kw[2] + t[1];
				num4 -= kw[3];
				state[0] = num;
				state[1] = num2;
				state[2] = num3;
				state[3] = num4;
			}

			// Token: 0x0400350D RID: 13581
			private const int ROTATION_0_0 = 14;

			// Token: 0x0400350E RID: 13582
			private const int ROTATION_0_1 = 16;

			// Token: 0x0400350F RID: 13583
			private const int ROTATION_1_0 = 52;

			// Token: 0x04003510 RID: 13584
			private const int ROTATION_1_1 = 57;

			// Token: 0x04003511 RID: 13585
			private const int ROTATION_2_0 = 23;

			// Token: 0x04003512 RID: 13586
			private const int ROTATION_2_1 = 40;

			// Token: 0x04003513 RID: 13587
			private const int ROTATION_3_0 = 5;

			// Token: 0x04003514 RID: 13588
			private const int ROTATION_3_1 = 37;

			// Token: 0x04003515 RID: 13589
			private const int ROTATION_4_0 = 25;

			// Token: 0x04003516 RID: 13590
			private const int ROTATION_4_1 = 33;

			// Token: 0x04003517 RID: 13591
			private const int ROTATION_5_0 = 46;

			// Token: 0x04003518 RID: 13592
			private const int ROTATION_5_1 = 12;

			// Token: 0x04003519 RID: 13593
			private const int ROTATION_6_0 = 58;

			// Token: 0x0400351A RID: 13594
			private const int ROTATION_6_1 = 22;

			// Token: 0x0400351B RID: 13595
			private const int ROTATION_7_0 = 32;

			// Token: 0x0400351C RID: 13596
			private const int ROTATION_7_1 = 32;
		}

		// Token: 0x02000932 RID: 2354
		private sealed class Threefish512Cipher : ThreefishEngine.ThreefishCipher
		{
			// Token: 0x06004E61 RID: 20065 RVA: 0x001B3B4C File Offset: 0x001B1D4C
			internal Threefish512Cipher(ulong[] kw, ulong[] t) : base(kw, t)
			{
			}

			// Token: 0x06004E62 RID: 20066 RVA: 0x001B4028 File Offset: 0x001B2228
			internal override void EncryptBlock(ulong[] block, ulong[] outWords)
			{
				ulong[] kw = this.kw;
				ulong[] t = this.t;
				int[] mod = ThreefishEngine.MOD9;
				int[] mod2 = ThreefishEngine.MOD3;
				if (kw.Length != 17)
				{
					throw new ArgumentException();
				}
				if (t.Length != 5)
				{
					throw new ArgumentException();
				}
				ulong num = block[0];
				ulong num2 = block[1];
				ulong num3 = block[2];
				ulong num4 = block[3];
				ulong num5 = block[4];
				ulong num6 = block[5];
				ulong num7 = block[6];
				ulong num8 = block[7];
				num += kw[0];
				num2 += kw[1];
				num3 += kw[2];
				num4 += kw[3];
				num5 += kw[4];
				num6 += kw[5] + t[0];
				num7 += kw[6] + t[1];
				num8 += kw[7];
				for (int i = 1; i < 18; i += 2)
				{
					int num9 = mod[i];
					int num10 = mod2[i];
					num2 = ThreefishEngine.RotlXor(num2, 46, num += num2);
					num4 = ThreefishEngine.RotlXor(num4, 36, num3 += num4);
					num6 = ThreefishEngine.RotlXor(num6, 19, num5 += num6);
					num8 = ThreefishEngine.RotlXor(num8, 37, num7 += num8);
					num2 = ThreefishEngine.RotlXor(num2, 33, num3 += num2);
					num8 = ThreefishEngine.RotlXor(num8, 27, num5 += num8);
					num6 = ThreefishEngine.RotlXor(num6, 14, num7 += num6);
					num4 = ThreefishEngine.RotlXor(num4, 42, num += num4);
					num2 = ThreefishEngine.RotlXor(num2, 17, num5 += num2);
					num4 = ThreefishEngine.RotlXor(num4, 49, num7 += num4);
					num6 = ThreefishEngine.RotlXor(num6, 36, num += num6);
					num8 = ThreefishEngine.RotlXor(num8, 39, num3 += num8);
					num2 = ThreefishEngine.RotlXor(num2, 44, num7 += num2);
					num8 = ThreefishEngine.RotlXor(num8, 9, num += num8);
					num6 = ThreefishEngine.RotlXor(num6, 54, num3 += num6);
					num4 = ThreefishEngine.RotlXor(num4, 56, num5 += num4);
					num += kw[num9];
					num2 += kw[num9 + 1];
					num3 += kw[num9 + 2];
					num4 += kw[num9 + 3];
					num5 += kw[num9 + 4];
					num6 += kw[num9 + 5] + t[num10];
					num7 += kw[num9 + 6] + t[num10 + 1];
					num8 += kw[num9 + 7] + (ulong)i;
					num2 = ThreefishEngine.RotlXor(num2, 39, num += num2);
					num4 = ThreefishEngine.RotlXor(num4, 30, num3 += num4);
					num6 = ThreefishEngine.RotlXor(num6, 34, num5 += num6);
					num8 = ThreefishEngine.RotlXor(num8, 24, num7 += num8);
					num2 = ThreefishEngine.RotlXor(num2, 13, num3 += num2);
					num8 = ThreefishEngine.RotlXor(num8, 50, num5 += num8);
					num6 = ThreefishEngine.RotlXor(num6, 10, num7 += num6);
					num4 = ThreefishEngine.RotlXor(num4, 17, num += num4);
					num2 = ThreefishEngine.RotlXor(num2, 25, num5 += num2);
					num4 = ThreefishEngine.RotlXor(num4, 29, num7 += num4);
					num6 = ThreefishEngine.RotlXor(num6, 39, num += num6);
					num8 = ThreefishEngine.RotlXor(num8, 43, num3 += num8);
					num2 = ThreefishEngine.RotlXor(num2, 8, num7 += num2);
					num8 = ThreefishEngine.RotlXor(num8, 35, num += num8);
					num6 = ThreefishEngine.RotlXor(num6, 56, num3 += num6);
					num4 = ThreefishEngine.RotlXor(num4, 22, num5 += num4);
					num += kw[num9 + 1];
					num2 += kw[num9 + 2];
					num3 += kw[num9 + 3];
					num4 += kw[num9 + 4];
					num5 += kw[num9 + 5];
					num6 += kw[num9 + 6] + t[num10 + 1];
					num7 += kw[num9 + 7] + t[num10 + 2];
					num8 += kw[num9 + 8] + (ulong)i + 1UL;
				}
				outWords[0] = num;
				outWords[1] = num2;
				outWords[2] = num3;
				outWords[3] = num4;
				outWords[4] = num5;
				outWords[5] = num6;
				outWords[6] = num7;
				outWords[7] = num8;
			}

			// Token: 0x06004E63 RID: 20067 RVA: 0x001B4458 File Offset: 0x001B2658
			internal override void DecryptBlock(ulong[] block, ulong[] state)
			{
				ulong[] kw = this.kw;
				ulong[] t = this.t;
				int[] mod = ThreefishEngine.MOD9;
				int[] mod2 = ThreefishEngine.MOD3;
				if (kw.Length != 17)
				{
					throw new ArgumentException();
				}
				if (t.Length != 5)
				{
					throw new ArgumentException();
				}
				ulong num = block[0];
				ulong num2 = block[1];
				ulong num3 = block[2];
				ulong num4 = block[3];
				ulong num5 = block[4];
				ulong num6 = block[5];
				ulong num7 = block[6];
				ulong num8 = block[7];
				for (int i = 17; i >= 1; i -= 2)
				{
					int num9 = mod[i];
					int num10 = mod2[i];
					num -= kw[num9 + 1];
					num2 -= kw[num9 + 2];
					num3 -= kw[num9 + 3];
					num4 -= kw[num9 + 4];
					num5 -= kw[num9 + 5];
					num6 -= kw[num9 + 6] + t[num10 + 1];
					num7 -= kw[num9 + 7] + t[num10 + 2];
					num8 -= kw[num9 + 8] + (ulong)i + 1UL;
					num2 = ThreefishEngine.XorRotr(num2, 8, num7);
					num7 -= num2;
					num8 = ThreefishEngine.XorRotr(num8, 35, num);
					num -= num8;
					num6 = ThreefishEngine.XorRotr(num6, 56, num3);
					num3 -= num6;
					num4 = ThreefishEngine.XorRotr(num4, 22, num5);
					num5 -= num4;
					num2 = ThreefishEngine.XorRotr(num2, 25, num5);
					num5 -= num2;
					num4 = ThreefishEngine.XorRotr(num4, 29, num7);
					num7 -= num4;
					num6 = ThreefishEngine.XorRotr(num6, 39, num);
					num -= num6;
					num8 = ThreefishEngine.XorRotr(num8, 43, num3);
					num3 -= num8;
					num2 = ThreefishEngine.XorRotr(num2, 13, num3);
					num3 -= num2;
					num8 = ThreefishEngine.XorRotr(num8, 50, num5);
					num5 -= num8;
					num6 = ThreefishEngine.XorRotr(num6, 10, num7);
					num7 -= num6;
					num4 = ThreefishEngine.XorRotr(num4, 17, num);
					num -= num4;
					num2 = ThreefishEngine.XorRotr(num2, 39, num);
					num -= num2;
					num4 = ThreefishEngine.XorRotr(num4, 30, num3);
					num3 -= num4;
					num6 = ThreefishEngine.XorRotr(num6, 34, num5);
					num5 -= num6;
					num8 = ThreefishEngine.XorRotr(num8, 24, num7);
					num7 -= num8;
					num -= kw[num9];
					num2 -= kw[num9 + 1];
					num3 -= kw[num9 + 2];
					num4 -= kw[num9 + 3];
					num5 -= kw[num9 + 4];
					num6 -= kw[num9 + 5] + t[num10];
					num7 -= kw[num9 + 6] + t[num10 + 1];
					num8 -= kw[num9 + 7] + (ulong)i;
					num2 = ThreefishEngine.XorRotr(num2, 44, num7);
					num7 -= num2;
					num8 = ThreefishEngine.XorRotr(num8, 9, num);
					num -= num8;
					num6 = ThreefishEngine.XorRotr(num6, 54, num3);
					num3 -= num6;
					num4 = ThreefishEngine.XorRotr(num4, 56, num5);
					num5 -= num4;
					num2 = ThreefishEngine.XorRotr(num2, 17, num5);
					num5 -= num2;
					num4 = ThreefishEngine.XorRotr(num4, 49, num7);
					num7 -= num4;
					num6 = ThreefishEngine.XorRotr(num6, 36, num);
					num -= num6;
					num8 = ThreefishEngine.XorRotr(num8, 39, num3);
					num3 -= num8;
					num2 = ThreefishEngine.XorRotr(num2, 33, num3);
					num3 -= num2;
					num8 = ThreefishEngine.XorRotr(num8, 27, num5);
					num5 -= num8;
					num6 = ThreefishEngine.XorRotr(num6, 14, num7);
					num7 -= num6;
					num4 = ThreefishEngine.XorRotr(num4, 42, num);
					num -= num4;
					num2 = ThreefishEngine.XorRotr(num2, 46, num);
					num -= num2;
					num4 = ThreefishEngine.XorRotr(num4, 36, num3);
					num3 -= num4;
					num6 = ThreefishEngine.XorRotr(num6, 19, num5);
					num5 -= num6;
					num8 = ThreefishEngine.XorRotr(num8, 37, num7);
					num7 -= num8;
				}
				num -= kw[0];
				num2 -= kw[1];
				num3 -= kw[2];
				num4 -= kw[3];
				num5 -= kw[4];
				num6 -= kw[5] + t[0];
				num7 -= kw[6] + t[1];
				num8 -= kw[7];
				state[0] = num;
				state[1] = num2;
				state[2] = num3;
				state[3] = num4;
				state[4] = num5;
				state[5] = num6;
				state[6] = num7;
				state[7] = num8;
			}

			// Token: 0x0400351D RID: 13597
			private const int ROTATION_0_0 = 46;

			// Token: 0x0400351E RID: 13598
			private const int ROTATION_0_1 = 36;

			// Token: 0x0400351F RID: 13599
			private const int ROTATION_0_2 = 19;

			// Token: 0x04003520 RID: 13600
			private const int ROTATION_0_3 = 37;

			// Token: 0x04003521 RID: 13601
			private const int ROTATION_1_0 = 33;

			// Token: 0x04003522 RID: 13602
			private const int ROTATION_1_1 = 27;

			// Token: 0x04003523 RID: 13603
			private const int ROTATION_1_2 = 14;

			// Token: 0x04003524 RID: 13604
			private const int ROTATION_1_3 = 42;

			// Token: 0x04003525 RID: 13605
			private const int ROTATION_2_0 = 17;

			// Token: 0x04003526 RID: 13606
			private const int ROTATION_2_1 = 49;

			// Token: 0x04003527 RID: 13607
			private const int ROTATION_2_2 = 36;

			// Token: 0x04003528 RID: 13608
			private const int ROTATION_2_3 = 39;

			// Token: 0x04003529 RID: 13609
			private const int ROTATION_3_0 = 44;

			// Token: 0x0400352A RID: 13610
			private const int ROTATION_3_1 = 9;

			// Token: 0x0400352B RID: 13611
			private const int ROTATION_3_2 = 54;

			// Token: 0x0400352C RID: 13612
			private const int ROTATION_3_3 = 56;

			// Token: 0x0400352D RID: 13613
			private const int ROTATION_4_0 = 39;

			// Token: 0x0400352E RID: 13614
			private const int ROTATION_4_1 = 30;

			// Token: 0x0400352F RID: 13615
			private const int ROTATION_4_2 = 34;

			// Token: 0x04003530 RID: 13616
			private const int ROTATION_4_3 = 24;

			// Token: 0x04003531 RID: 13617
			private const int ROTATION_5_0 = 13;

			// Token: 0x04003532 RID: 13618
			private const int ROTATION_5_1 = 50;

			// Token: 0x04003533 RID: 13619
			private const int ROTATION_5_2 = 10;

			// Token: 0x04003534 RID: 13620
			private const int ROTATION_5_3 = 17;

			// Token: 0x04003535 RID: 13621
			private const int ROTATION_6_0 = 25;

			// Token: 0x04003536 RID: 13622
			private const int ROTATION_6_1 = 29;

			// Token: 0x04003537 RID: 13623
			private const int ROTATION_6_2 = 39;

			// Token: 0x04003538 RID: 13624
			private const int ROTATION_6_3 = 43;

			// Token: 0x04003539 RID: 13625
			private const int ROTATION_7_0 = 8;

			// Token: 0x0400353A RID: 13626
			private const int ROTATION_7_1 = 35;

			// Token: 0x0400353B RID: 13627
			private const int ROTATION_7_2 = 56;

			// Token: 0x0400353C RID: 13628
			private const int ROTATION_7_3 = 22;
		}

		// Token: 0x02000933 RID: 2355
		private sealed class Threefish1024Cipher : ThreefishEngine.ThreefishCipher
		{
			// Token: 0x06004E64 RID: 20068 RVA: 0x001B3B4C File Offset: 0x001B1D4C
			public Threefish1024Cipher(ulong[] kw, ulong[] t) : base(kw, t)
			{
			}

			// Token: 0x06004E65 RID: 20069 RVA: 0x001B48A8 File Offset: 0x001B2AA8
			internal override void EncryptBlock(ulong[] block, ulong[] outWords)
			{
				ulong[] kw = this.kw;
				ulong[] t = this.t;
				int[] mod = ThreefishEngine.MOD17;
				int[] mod2 = ThreefishEngine.MOD3;
				if (kw.Length != 33)
				{
					throw new ArgumentException();
				}
				if (t.Length != 5)
				{
					throw new ArgumentException();
				}
				ulong num = block[0];
				ulong num2 = block[1];
				ulong num3 = block[2];
				ulong num4 = block[3];
				ulong num5 = block[4];
				ulong num6 = block[5];
				ulong num7 = block[6];
				ulong num8 = block[7];
				ulong num9 = block[8];
				ulong num10 = block[9];
				ulong num11 = block[10];
				ulong num12 = block[11];
				ulong num13 = block[12];
				ulong num14 = block[13];
				ulong num15 = block[14];
				ulong num16 = block[15];
				num += kw[0];
				num2 += kw[1];
				num3 += kw[2];
				num4 += kw[3];
				num5 += kw[4];
				num6 += kw[5];
				num7 += kw[6];
				num8 += kw[7];
				num9 += kw[8];
				num10 += kw[9];
				num11 += kw[10];
				num12 += kw[11];
				num13 += kw[12];
				num14 += kw[13] + t[0];
				num15 += kw[14] + t[1];
				num16 += kw[15];
				for (int i = 1; i < 20; i += 2)
				{
					int num17 = mod[i];
					int num18 = mod2[i];
					num2 = ThreefishEngine.RotlXor(num2, 24, num += num2);
					num4 = ThreefishEngine.RotlXor(num4, 13, num3 += num4);
					num6 = ThreefishEngine.RotlXor(num6, 8, num5 += num6);
					num8 = ThreefishEngine.RotlXor(num8, 47, num7 += num8);
					num10 = ThreefishEngine.RotlXor(num10, 8, num9 += num10);
					num12 = ThreefishEngine.RotlXor(num12, 17, num11 += num12);
					num14 = ThreefishEngine.RotlXor(num14, 22, num13 += num14);
					num16 = ThreefishEngine.RotlXor(num16, 37, num15 += num16);
					num10 = ThreefishEngine.RotlXor(num10, 38, num += num10);
					num14 = ThreefishEngine.RotlXor(num14, 19, num3 += num14);
					num12 = ThreefishEngine.RotlXor(num12, 10, num7 += num12);
					num16 = ThreefishEngine.RotlXor(num16, 55, num5 += num16);
					num8 = ThreefishEngine.RotlXor(num8, 49, num11 += num8);
					num4 = ThreefishEngine.RotlXor(num4, 18, num13 += num4);
					num6 = ThreefishEngine.RotlXor(num6, 23, num15 += num6);
					num2 = ThreefishEngine.RotlXor(num2, 52, num9 += num2);
					num8 = ThreefishEngine.RotlXor(num8, 33, num += num8);
					num6 = ThreefishEngine.RotlXor(num6, 4, num3 += num6);
					num4 = ThreefishEngine.RotlXor(num4, 51, num5 += num4);
					num2 = ThreefishEngine.RotlXor(num2, 13, num7 += num2);
					num16 = ThreefishEngine.RotlXor(num16, 34, num13 += num16);
					num14 = ThreefishEngine.RotlXor(num14, 41, num15 += num14);
					num12 = ThreefishEngine.RotlXor(num12, 59, num9 += num12);
					num10 = ThreefishEngine.RotlXor(num10, 17, num11 += num10);
					num16 = ThreefishEngine.RotlXor(num16, 5, num += num16);
					num12 = ThreefishEngine.RotlXor(num12, 20, num3 += num12);
					num14 = ThreefishEngine.RotlXor(num14, 48, num7 += num14);
					num10 = ThreefishEngine.RotlXor(num10, 41, num5 += num10);
					num2 = ThreefishEngine.RotlXor(num2, 47, num15 += num2);
					num6 = ThreefishEngine.RotlXor(num6, 28, num9 += num6);
					num4 = ThreefishEngine.RotlXor(num4, 16, num11 += num4);
					num8 = ThreefishEngine.RotlXor(num8, 25, num13 += num8);
					num += kw[num17];
					num2 += kw[num17 + 1];
					num3 += kw[num17 + 2];
					num4 += kw[num17 + 3];
					num5 += kw[num17 + 4];
					num6 += kw[num17 + 5];
					num7 += kw[num17 + 6];
					num8 += kw[num17 + 7];
					num9 += kw[num17 + 8];
					num10 += kw[num17 + 9];
					num11 += kw[num17 + 10];
					num12 += kw[num17 + 11];
					num13 += kw[num17 + 12];
					num14 += kw[num17 + 13] + t[num18];
					num15 += kw[num17 + 14] + t[num18 + 1];
					num16 += kw[num17 + 15] + (ulong)i;
					num2 = ThreefishEngine.RotlXor(num2, 41, num += num2);
					num4 = ThreefishEngine.RotlXor(num4, 9, num3 += num4);
					num6 = ThreefishEngine.RotlXor(num6, 37, num5 += num6);
					num8 = ThreefishEngine.RotlXor(num8, 31, num7 += num8);
					num10 = ThreefishEngine.RotlXor(num10, 12, num9 += num10);
					num12 = ThreefishEngine.RotlXor(num12, 47, num11 += num12);
					num14 = ThreefishEngine.RotlXor(num14, 44, num13 += num14);
					num16 = ThreefishEngine.RotlXor(num16, 30, num15 += num16);
					num10 = ThreefishEngine.RotlXor(num10, 16, num += num10);
					num14 = ThreefishEngine.RotlXor(num14, 34, num3 += num14);
					num12 = ThreefishEngine.RotlXor(num12, 56, num7 += num12);
					num16 = ThreefishEngine.RotlXor(num16, 51, num5 += num16);
					num8 = ThreefishEngine.RotlXor(num8, 4, num11 += num8);
					num4 = ThreefishEngine.RotlXor(num4, 53, num13 += num4);
					num6 = ThreefishEngine.RotlXor(num6, 42, num15 += num6);
					num2 = ThreefishEngine.RotlXor(num2, 41, num9 += num2);
					num8 = ThreefishEngine.RotlXor(num8, 31, num += num8);
					num6 = ThreefishEngine.RotlXor(num6, 44, num3 += num6);
					num4 = ThreefishEngine.RotlXor(num4, 47, num5 += num4);
					num2 = ThreefishEngine.RotlXor(num2, 46, num7 += num2);
					num16 = ThreefishEngine.RotlXor(num16, 19, num13 += num16);
					num14 = ThreefishEngine.RotlXor(num14, 42, num15 += num14);
					num12 = ThreefishEngine.RotlXor(num12, 44, num9 += num12);
					num10 = ThreefishEngine.RotlXor(num10, 25, num11 += num10);
					num16 = ThreefishEngine.RotlXor(num16, 9, num += num16);
					num12 = ThreefishEngine.RotlXor(num12, 48, num3 += num12);
					num14 = ThreefishEngine.RotlXor(num14, 35, num7 += num14);
					num10 = ThreefishEngine.RotlXor(num10, 52, num5 += num10);
					num2 = ThreefishEngine.RotlXor(num2, 23, num15 += num2);
					num6 = ThreefishEngine.RotlXor(num6, 31, num9 += num6);
					num4 = ThreefishEngine.RotlXor(num4, 37, num11 += num4);
					num8 = ThreefishEngine.RotlXor(num8, 20, num13 += num8);
					num += kw[num17 + 1];
					num2 += kw[num17 + 2];
					num3 += kw[num17 + 3];
					num4 += kw[num17 + 4];
					num5 += kw[num17 + 5];
					num6 += kw[num17 + 6];
					num7 += kw[num17 + 7];
					num8 += kw[num17 + 8];
					num9 += kw[num17 + 9];
					num10 += kw[num17 + 10];
					num11 += kw[num17 + 11];
					num12 += kw[num17 + 12];
					num13 += kw[num17 + 13];
					num14 += kw[num17 + 14] + t[num18 + 1];
					num15 += kw[num17 + 15] + t[num18 + 2];
					num16 += kw[num17 + 16] + (ulong)i + 1UL;
				}
				outWords[0] = num;
				outWords[1] = num2;
				outWords[2] = num3;
				outWords[3] = num4;
				outWords[4] = num5;
				outWords[5] = num6;
				outWords[6] = num7;
				outWords[7] = num8;
				outWords[8] = num9;
				outWords[9] = num10;
				outWords[10] = num11;
				outWords[11] = num12;
				outWords[12] = num13;
				outWords[13] = num14;
				outWords[14] = num15;
				outWords[15] = num16;
			}

			// Token: 0x06004E66 RID: 20070 RVA: 0x001B5098 File Offset: 0x001B3298
			internal override void DecryptBlock(ulong[] block, ulong[] state)
			{
				ulong[] kw = this.kw;
				ulong[] t = this.t;
				int[] mod = ThreefishEngine.MOD17;
				int[] mod2 = ThreefishEngine.MOD3;
				if (kw.Length != 33)
				{
					throw new ArgumentException();
				}
				if (t.Length != 5)
				{
					throw new ArgumentException();
				}
				ulong num = block[0];
				ulong num2 = block[1];
				ulong num3 = block[2];
				ulong num4 = block[3];
				ulong num5 = block[4];
				ulong num6 = block[5];
				ulong num7 = block[6];
				ulong num8 = block[7];
				ulong num9 = block[8];
				ulong num10 = block[9];
				ulong num11 = block[10];
				ulong num12 = block[11];
				ulong num13 = block[12];
				ulong num14 = block[13];
				ulong num15 = block[14];
				ulong num16 = block[15];
				for (int i = 19; i >= 1; i -= 2)
				{
					int num17 = mod[i];
					int num18 = mod2[i];
					num -= kw[num17 + 1];
					num2 -= kw[num17 + 2];
					num3 -= kw[num17 + 3];
					num4 -= kw[num17 + 4];
					num5 -= kw[num17 + 5];
					num6 -= kw[num17 + 6];
					num7 -= kw[num17 + 7];
					num8 -= kw[num17 + 8];
					num9 -= kw[num17 + 9];
					num10 -= kw[num17 + 10];
					num11 -= kw[num17 + 11];
					num12 -= kw[num17 + 12];
					num13 -= kw[num17 + 13];
					num14 -= kw[num17 + 14] + t[num18 + 1];
					num15 -= kw[num17 + 15] + t[num18 + 2];
					num16 -= kw[num17 + 16] + (ulong)i + 1UL;
					num16 = ThreefishEngine.XorRotr(num16, 9, num);
					num -= num16;
					num12 = ThreefishEngine.XorRotr(num12, 48, num3);
					num3 -= num12;
					num14 = ThreefishEngine.XorRotr(num14, 35, num7);
					num7 -= num14;
					num10 = ThreefishEngine.XorRotr(num10, 52, num5);
					num5 -= num10;
					num2 = ThreefishEngine.XorRotr(num2, 23, num15);
					num15 -= num2;
					num6 = ThreefishEngine.XorRotr(num6, 31, num9);
					num9 -= num6;
					num4 = ThreefishEngine.XorRotr(num4, 37, num11);
					num11 -= num4;
					num8 = ThreefishEngine.XorRotr(num8, 20, num13);
					num13 -= num8;
					num8 = ThreefishEngine.XorRotr(num8, 31, num);
					num -= num8;
					num6 = ThreefishEngine.XorRotr(num6, 44, num3);
					num3 -= num6;
					num4 = ThreefishEngine.XorRotr(num4, 47, num5);
					num5 -= num4;
					num2 = ThreefishEngine.XorRotr(num2, 46, num7);
					num7 -= num2;
					num16 = ThreefishEngine.XorRotr(num16, 19, num13);
					num13 -= num16;
					num14 = ThreefishEngine.XorRotr(num14, 42, num15);
					num15 -= num14;
					num12 = ThreefishEngine.XorRotr(num12, 44, num9);
					num9 -= num12;
					num10 = ThreefishEngine.XorRotr(num10, 25, num11);
					num11 -= num10;
					num10 = ThreefishEngine.XorRotr(num10, 16, num);
					num -= num10;
					num14 = ThreefishEngine.XorRotr(num14, 34, num3);
					num3 -= num14;
					num12 = ThreefishEngine.XorRotr(num12, 56, num7);
					num7 -= num12;
					num16 = ThreefishEngine.XorRotr(num16, 51, num5);
					num5 -= num16;
					num8 = ThreefishEngine.XorRotr(num8, 4, num11);
					num11 -= num8;
					num4 = ThreefishEngine.XorRotr(num4, 53, num13);
					num13 -= num4;
					num6 = ThreefishEngine.XorRotr(num6, 42, num15);
					num15 -= num6;
					num2 = ThreefishEngine.XorRotr(num2, 41, num9);
					num9 -= num2;
					num2 = ThreefishEngine.XorRotr(num2, 41, num);
					num -= num2;
					num4 = ThreefishEngine.XorRotr(num4, 9, num3);
					num3 -= num4;
					num6 = ThreefishEngine.XorRotr(num6, 37, num5);
					num5 -= num6;
					num8 = ThreefishEngine.XorRotr(num8, 31, num7);
					num7 -= num8;
					num10 = ThreefishEngine.XorRotr(num10, 12, num9);
					num9 -= num10;
					num12 = ThreefishEngine.XorRotr(num12, 47, num11);
					num11 -= num12;
					num14 = ThreefishEngine.XorRotr(num14, 44, num13);
					num13 -= num14;
					num16 = ThreefishEngine.XorRotr(num16, 30, num15);
					num15 -= num16;
					num -= kw[num17];
					num2 -= kw[num17 + 1];
					num3 -= kw[num17 + 2];
					num4 -= kw[num17 + 3];
					num5 -= kw[num17 + 4];
					num6 -= kw[num17 + 5];
					num7 -= kw[num17 + 6];
					num8 -= kw[num17 + 7];
					num9 -= kw[num17 + 8];
					num10 -= kw[num17 + 9];
					num11 -= kw[num17 + 10];
					num12 -= kw[num17 + 11];
					num13 -= kw[num17 + 12];
					num14 -= kw[num17 + 13] + t[num18];
					num15 -= kw[num17 + 14] + t[num18 + 1];
					num16 -= kw[num17 + 15] + (ulong)i;
					num16 = ThreefishEngine.XorRotr(num16, 5, num);
					num -= num16;
					num12 = ThreefishEngine.XorRotr(num12, 20, num3);
					num3 -= num12;
					num14 = ThreefishEngine.XorRotr(num14, 48, num7);
					num7 -= num14;
					num10 = ThreefishEngine.XorRotr(num10, 41, num5);
					num5 -= num10;
					num2 = ThreefishEngine.XorRotr(num2, 47, num15);
					num15 -= num2;
					num6 = ThreefishEngine.XorRotr(num6, 28, num9);
					num9 -= num6;
					num4 = ThreefishEngine.XorRotr(num4, 16, num11);
					num11 -= num4;
					num8 = ThreefishEngine.XorRotr(num8, 25, num13);
					num13 -= num8;
					num8 = ThreefishEngine.XorRotr(num8, 33, num);
					num -= num8;
					num6 = ThreefishEngine.XorRotr(num6, 4, num3);
					num3 -= num6;
					num4 = ThreefishEngine.XorRotr(num4, 51, num5);
					num5 -= num4;
					num2 = ThreefishEngine.XorRotr(num2, 13, num7);
					num7 -= num2;
					num16 = ThreefishEngine.XorRotr(num16, 34, num13);
					num13 -= num16;
					num14 = ThreefishEngine.XorRotr(num14, 41, num15);
					num15 -= num14;
					num12 = ThreefishEngine.XorRotr(num12, 59, num9);
					num9 -= num12;
					num10 = ThreefishEngine.XorRotr(num10, 17, num11);
					num11 -= num10;
					num10 = ThreefishEngine.XorRotr(num10, 38, num);
					num -= num10;
					num14 = ThreefishEngine.XorRotr(num14, 19, num3);
					num3 -= num14;
					num12 = ThreefishEngine.XorRotr(num12, 10, num7);
					num7 -= num12;
					num16 = ThreefishEngine.XorRotr(num16, 55, num5);
					num5 -= num16;
					num8 = ThreefishEngine.XorRotr(num8, 49, num11);
					num11 -= num8;
					num4 = ThreefishEngine.XorRotr(num4, 18, num13);
					num13 -= num4;
					num6 = ThreefishEngine.XorRotr(num6, 23, num15);
					num15 -= num6;
					num2 = ThreefishEngine.XorRotr(num2, 52, num9);
					num9 -= num2;
					num2 = ThreefishEngine.XorRotr(num2, 24, num);
					num -= num2;
					num4 = ThreefishEngine.XorRotr(num4, 13, num3);
					num3 -= num4;
					num6 = ThreefishEngine.XorRotr(num6, 8, num5);
					num5 -= num6;
					num8 = ThreefishEngine.XorRotr(num8, 47, num7);
					num7 -= num8;
					num10 = ThreefishEngine.XorRotr(num10, 8, num9);
					num9 -= num10;
					num12 = ThreefishEngine.XorRotr(num12, 17, num11);
					num11 -= num12;
					num14 = ThreefishEngine.XorRotr(num14, 22, num13);
					num13 -= num14;
					num16 = ThreefishEngine.XorRotr(num16, 37, num15);
					num15 -= num16;
				}
				num -= kw[0];
				num2 -= kw[1];
				num3 -= kw[2];
				num4 -= kw[3];
				num5 -= kw[4];
				num6 -= kw[5];
				num7 -= kw[6];
				num8 -= kw[7];
				num9 -= kw[8];
				num10 -= kw[9];
				num11 -= kw[10];
				num12 -= kw[11];
				num13 -= kw[12];
				num14 -= kw[13] + t[0];
				num15 -= kw[14] + t[1];
				num16 -= kw[15];
				state[0] = num;
				state[1] = num2;
				state[2] = num3;
				state[3] = num4;
				state[4] = num5;
				state[5] = num6;
				state[6] = num7;
				state[7] = num8;
				state[8] = num9;
				state[9] = num10;
				state[10] = num11;
				state[11] = num12;
				state[12] = num13;
				state[13] = num14;
				state[14] = num15;
				state[15] = num16;
			}

			// Token: 0x0400353D RID: 13629
			private const int ROTATION_0_0 = 24;

			// Token: 0x0400353E RID: 13630
			private const int ROTATION_0_1 = 13;

			// Token: 0x0400353F RID: 13631
			private const int ROTATION_0_2 = 8;

			// Token: 0x04003540 RID: 13632
			private const int ROTATION_0_3 = 47;

			// Token: 0x04003541 RID: 13633
			private const int ROTATION_0_4 = 8;

			// Token: 0x04003542 RID: 13634
			private const int ROTATION_0_5 = 17;

			// Token: 0x04003543 RID: 13635
			private const int ROTATION_0_6 = 22;

			// Token: 0x04003544 RID: 13636
			private const int ROTATION_0_7 = 37;

			// Token: 0x04003545 RID: 13637
			private const int ROTATION_1_0 = 38;

			// Token: 0x04003546 RID: 13638
			private const int ROTATION_1_1 = 19;

			// Token: 0x04003547 RID: 13639
			private const int ROTATION_1_2 = 10;

			// Token: 0x04003548 RID: 13640
			private const int ROTATION_1_3 = 55;

			// Token: 0x04003549 RID: 13641
			private const int ROTATION_1_4 = 49;

			// Token: 0x0400354A RID: 13642
			private const int ROTATION_1_5 = 18;

			// Token: 0x0400354B RID: 13643
			private const int ROTATION_1_6 = 23;

			// Token: 0x0400354C RID: 13644
			private const int ROTATION_1_7 = 52;

			// Token: 0x0400354D RID: 13645
			private const int ROTATION_2_0 = 33;

			// Token: 0x0400354E RID: 13646
			private const int ROTATION_2_1 = 4;

			// Token: 0x0400354F RID: 13647
			private const int ROTATION_2_2 = 51;

			// Token: 0x04003550 RID: 13648
			private const int ROTATION_2_3 = 13;

			// Token: 0x04003551 RID: 13649
			private const int ROTATION_2_4 = 34;

			// Token: 0x04003552 RID: 13650
			private const int ROTATION_2_5 = 41;

			// Token: 0x04003553 RID: 13651
			private const int ROTATION_2_6 = 59;

			// Token: 0x04003554 RID: 13652
			private const int ROTATION_2_7 = 17;

			// Token: 0x04003555 RID: 13653
			private const int ROTATION_3_0 = 5;

			// Token: 0x04003556 RID: 13654
			private const int ROTATION_3_1 = 20;

			// Token: 0x04003557 RID: 13655
			private const int ROTATION_3_2 = 48;

			// Token: 0x04003558 RID: 13656
			private const int ROTATION_3_3 = 41;

			// Token: 0x04003559 RID: 13657
			private const int ROTATION_3_4 = 47;

			// Token: 0x0400355A RID: 13658
			private const int ROTATION_3_5 = 28;

			// Token: 0x0400355B RID: 13659
			private const int ROTATION_3_6 = 16;

			// Token: 0x0400355C RID: 13660
			private const int ROTATION_3_7 = 25;

			// Token: 0x0400355D RID: 13661
			private const int ROTATION_4_0 = 41;

			// Token: 0x0400355E RID: 13662
			private const int ROTATION_4_1 = 9;

			// Token: 0x0400355F RID: 13663
			private const int ROTATION_4_2 = 37;

			// Token: 0x04003560 RID: 13664
			private const int ROTATION_4_3 = 31;

			// Token: 0x04003561 RID: 13665
			private const int ROTATION_4_4 = 12;

			// Token: 0x04003562 RID: 13666
			private const int ROTATION_4_5 = 47;

			// Token: 0x04003563 RID: 13667
			private const int ROTATION_4_6 = 44;

			// Token: 0x04003564 RID: 13668
			private const int ROTATION_4_7 = 30;

			// Token: 0x04003565 RID: 13669
			private const int ROTATION_5_0 = 16;

			// Token: 0x04003566 RID: 13670
			private const int ROTATION_5_1 = 34;

			// Token: 0x04003567 RID: 13671
			private const int ROTATION_5_2 = 56;

			// Token: 0x04003568 RID: 13672
			private const int ROTATION_5_3 = 51;

			// Token: 0x04003569 RID: 13673
			private const int ROTATION_5_4 = 4;

			// Token: 0x0400356A RID: 13674
			private const int ROTATION_5_5 = 53;

			// Token: 0x0400356B RID: 13675
			private const int ROTATION_5_6 = 42;

			// Token: 0x0400356C RID: 13676
			private const int ROTATION_5_7 = 41;

			// Token: 0x0400356D RID: 13677
			private const int ROTATION_6_0 = 31;

			// Token: 0x0400356E RID: 13678
			private const int ROTATION_6_1 = 44;

			// Token: 0x0400356F RID: 13679
			private const int ROTATION_6_2 = 47;

			// Token: 0x04003570 RID: 13680
			private const int ROTATION_6_3 = 46;

			// Token: 0x04003571 RID: 13681
			private const int ROTATION_6_4 = 19;

			// Token: 0x04003572 RID: 13682
			private const int ROTATION_6_5 = 42;

			// Token: 0x04003573 RID: 13683
			private const int ROTATION_6_6 = 44;

			// Token: 0x04003574 RID: 13684
			private const int ROTATION_6_7 = 25;

			// Token: 0x04003575 RID: 13685
			private const int ROTATION_7_0 = 9;

			// Token: 0x04003576 RID: 13686
			private const int ROTATION_7_1 = 48;

			// Token: 0x04003577 RID: 13687
			private const int ROTATION_7_2 = 35;

			// Token: 0x04003578 RID: 13688
			private const int ROTATION_7_3 = 52;

			// Token: 0x04003579 RID: 13689
			private const int ROTATION_7_4 = 23;

			// Token: 0x0400357A RID: 13690
			private const int ROTATION_7_5 = 31;

			// Token: 0x0400357B RID: 13691
			private const int ROTATION_7_6 = 37;

			// Token: 0x0400357C RID: 13692
			private const int ROTATION_7_7 = 20;
		}
	}
}
