using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x0200050D RID: 1293
	public class OcbBlockCipher : IAeadBlockCipher
	{
		// Token: 0x0600315C RID: 12636 RVA: 0x0012E244 File Offset: 0x0012C444
		public OcbBlockCipher(IBlockCipher hashCipher, IBlockCipher mainCipher)
		{
			if (hashCipher == null)
			{
				throw new ArgumentNullException("hashCipher");
			}
			if (hashCipher.GetBlockSize() != 16)
			{
				throw new ArgumentException("must have a block size of " + 16, "hashCipher");
			}
			if (mainCipher == null)
			{
				throw new ArgumentNullException("mainCipher");
			}
			if (mainCipher.GetBlockSize() != 16)
			{
				throw new ArgumentException("must have a block size of " + 16, "mainCipher");
			}
			if (!hashCipher.AlgorithmName.Equals(mainCipher.AlgorithmName))
			{
				throw new ArgumentException("'hashCipher' and 'mainCipher' must be the same algorithm");
			}
			this.hashCipher = hashCipher;
			this.mainCipher = mainCipher;
		}

		// Token: 0x0600315D RID: 12637 RVA: 0x0012E312 File Offset: 0x0012C512
		public virtual IBlockCipher GetUnderlyingCipher()
		{
			return this.mainCipher;
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x0600315E RID: 12638 RVA: 0x0012E31A File Offset: 0x0012C51A
		public virtual string AlgorithmName
		{
			get
			{
				return this.mainCipher.AlgorithmName + "/OCB";
			}
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x0012E334 File Offset: 0x0012C534
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			bool flag = this.forEncryption;
			this.forEncryption = forEncryption;
			this.macBlock = null;
			byte[] array;
			KeyParameter keyParameter;
			if (parameters is AeadParameters)
			{
				AeadParameters aeadParameters = (AeadParameters)parameters;
				array = aeadParameters.GetNonce();
				this.initialAssociatedText = aeadParameters.GetAssociatedText();
				int num = aeadParameters.MacSize;
				if (num < 64 || num > 128 || num % 8 != 0)
				{
					throw new ArgumentException("Invalid value for MAC size: " + num);
				}
				this.macSize = num / 8;
				keyParameter = aeadParameters.Key;
			}
			else
			{
				if (!(parameters is ParametersWithIV))
				{
					throw new ArgumentException("invalid parameters passed to OCB");
				}
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				array = parametersWithIV.GetIV();
				this.initialAssociatedText = null;
				this.macSize = 16;
				keyParameter = (KeyParameter)parametersWithIV.Parameters;
			}
			this.hashBlock = new byte[16];
			this.mainBlock = new byte[forEncryption ? 16 : (16 + this.macSize)];
			if (array == null)
			{
				array = new byte[0];
			}
			if (array.Length > 15)
			{
				throw new ArgumentException("IV must be no more than 15 bytes");
			}
			if (keyParameter != null)
			{
				this.hashCipher.Init(true, keyParameter);
				this.mainCipher.Init(forEncryption, keyParameter);
				this.KtopInput = null;
			}
			else if (flag != forEncryption)
			{
				throw new ArgumentException("cannot change encrypting state without providing key.");
			}
			this.L_Asterisk = new byte[16];
			this.hashCipher.ProcessBlock(this.L_Asterisk, 0, this.L_Asterisk, 0);
			this.L_Dollar = OcbBlockCipher.OCB_double(this.L_Asterisk);
			this.L = Platform.CreateArrayList();
			this.L.Add(OcbBlockCipher.OCB_double(this.L_Dollar));
			int num2 = this.ProcessNonce(array);
			int num3 = num2 % 8;
			int num4 = num2 / 8;
			if (num3 == 0)
			{
				Array.Copy(this.Stretch, num4, this.OffsetMAIN_0, 0, 16);
			}
			else
			{
				for (int i = 0; i < 16; i++)
				{
					uint num5 = (uint)this.Stretch[num4];
					uint num6 = (uint)this.Stretch[++num4];
					this.OffsetMAIN_0[i] = (byte)(num5 << num3 | num6 >> 8 - num3);
				}
			}
			this.hashBlockPos = 0;
			this.mainBlockPos = 0;
			this.hashBlockCount = 0L;
			this.mainBlockCount = 0L;
			this.OffsetHASH = new byte[16];
			this.Sum = new byte[16];
			Array.Copy(this.OffsetMAIN_0, 0, this.OffsetMAIN, 0, 16);
			this.Checksum = new byte[16];
			if (this.initialAssociatedText != null)
			{
				this.ProcessAadBytes(this.initialAssociatedText, 0, this.initialAssociatedText.Length);
			}
		}

		// Token: 0x06003160 RID: 12640 RVA: 0x0012E5BC File Offset: 0x0012C7BC
		protected virtual int ProcessNonce(byte[] N)
		{
			byte[] array = new byte[16];
			Array.Copy(N, 0, array, array.Length - N.Length, N.Length);
			array[0] = (byte)(this.macSize << 4);
			byte[] array2 = array;
			int num = 15 - N.Length;
			array2[num] |= 1;
			int result = (int)(array[15] & 63);
			byte[] array3 = array;
			int num2 = 15;
			array3[num2] &= 192;
			if (this.KtopInput == null || !Arrays.AreEqual(array, this.KtopInput))
			{
				byte[] array4 = new byte[16];
				this.KtopInput = array;
				this.hashCipher.ProcessBlock(this.KtopInput, 0, array4, 0);
				Array.Copy(array4, 0, this.Stretch, 0, 16);
				for (int i = 0; i < 8; i++)
				{
					this.Stretch[16 + i] = (array4[i] ^ array4[i + 1]);
				}
			}
			return result;
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x0012C4C1 File Offset: 0x0012A6C1
		public virtual int GetBlockSize()
		{
			return 16;
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x0012E687 File Offset: 0x0012C887
		public virtual byte[] GetMac()
		{
			if (this.macBlock != null)
			{
				return Arrays.Clone(this.macBlock);
			}
			return new byte[this.macSize];
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x0012E6A8 File Offset: 0x0012C8A8
		public virtual int GetOutputSize(int len)
		{
			int num = len + this.mainBlockPos;
			if (this.forEncryption)
			{
				return num + this.macSize;
			}
			if (num >= this.macSize)
			{
				return num - this.macSize;
			}
			return 0;
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x0012E6E4 File Offset: 0x0012C8E4
		public virtual int GetUpdateOutputSize(int len)
		{
			int num = len + this.mainBlockPos;
			if (!this.forEncryption)
			{
				if (num < this.macSize)
				{
					return 0;
				}
				num -= this.macSize;
			}
			return num - num % 16;
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x0012E71C File Offset: 0x0012C91C
		public virtual void ProcessAadByte(byte input)
		{
			this.hashBlock[this.hashBlockPos] = input;
			int num = this.hashBlockPos + 1;
			this.hashBlockPos = num;
			if (num == this.hashBlock.Length)
			{
				this.ProcessHashBlock();
			}
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x0012E758 File Offset: 0x0012C958
		public virtual void ProcessAadBytes(byte[] input, int off, int len)
		{
			for (int i = 0; i < len; i++)
			{
				this.hashBlock[this.hashBlockPos] = input[off + i];
				int num = this.hashBlockPos + 1;
				this.hashBlockPos = num;
				if (num == this.hashBlock.Length)
				{
					this.ProcessHashBlock();
				}
			}
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x0012E7A4 File Offset: 0x0012C9A4
		public virtual int ProcessByte(byte input, byte[] output, int outOff)
		{
			this.mainBlock[this.mainBlockPos] = input;
			int num = this.mainBlockPos + 1;
			this.mainBlockPos = num;
			if (num == this.mainBlock.Length)
			{
				this.ProcessMainBlock(output, outOff);
				return 16;
			}
			return 0;
		}

		// Token: 0x06003168 RID: 12648 RVA: 0x0012E7E8 File Offset: 0x0012C9E8
		public virtual int ProcessBytes(byte[] input, int inOff, int len, byte[] output, int outOff)
		{
			int num = 0;
			for (int i = 0; i < len; i++)
			{
				this.mainBlock[this.mainBlockPos] = input[inOff + i];
				int num2 = this.mainBlockPos + 1;
				this.mainBlockPos = num2;
				if (num2 == this.mainBlock.Length)
				{
					this.ProcessMainBlock(output, outOff + num);
					num += 16;
				}
			}
			return num;
		}

		// Token: 0x06003169 RID: 12649 RVA: 0x0012E844 File Offset: 0x0012CA44
		public virtual int DoFinal(byte[] output, int outOff)
		{
			byte[] array = null;
			if (!this.forEncryption)
			{
				if (this.mainBlockPos < this.macSize)
				{
					throw new InvalidCipherTextException("data too short");
				}
				this.mainBlockPos -= this.macSize;
				array = new byte[this.macSize];
				Array.Copy(this.mainBlock, this.mainBlockPos, array, 0, this.macSize);
			}
			if (this.hashBlockPos > 0)
			{
				OcbBlockCipher.OCB_extend(this.hashBlock, this.hashBlockPos);
				this.UpdateHASH(this.L_Asterisk);
			}
			if (this.mainBlockPos > 0)
			{
				if (this.forEncryption)
				{
					OcbBlockCipher.OCB_extend(this.mainBlock, this.mainBlockPos);
					OcbBlockCipher.Xor(this.Checksum, this.mainBlock);
				}
				OcbBlockCipher.Xor(this.OffsetMAIN, this.L_Asterisk);
				byte[] array2 = new byte[16];
				this.hashCipher.ProcessBlock(this.OffsetMAIN, 0, array2, 0);
				OcbBlockCipher.Xor(this.mainBlock, array2);
				Check.OutputLength(output, outOff, this.mainBlockPos, "Output buffer too short");
				Array.Copy(this.mainBlock, 0, output, outOff, this.mainBlockPos);
				if (!this.forEncryption)
				{
					OcbBlockCipher.OCB_extend(this.mainBlock, this.mainBlockPos);
					OcbBlockCipher.Xor(this.Checksum, this.mainBlock);
				}
			}
			OcbBlockCipher.Xor(this.Checksum, this.OffsetMAIN);
			OcbBlockCipher.Xor(this.Checksum, this.L_Dollar);
			this.hashCipher.ProcessBlock(this.Checksum, 0, this.Checksum, 0);
			OcbBlockCipher.Xor(this.Checksum, this.Sum);
			this.macBlock = new byte[this.macSize];
			Array.Copy(this.Checksum, 0, this.macBlock, 0, this.macSize);
			int num = this.mainBlockPos;
			if (this.forEncryption)
			{
				Check.OutputLength(output, outOff, num + this.macSize, "Output buffer too short");
				Array.Copy(this.macBlock, 0, output, outOff + num, this.macSize);
				num += this.macSize;
			}
			else if (!Arrays.ConstantTimeAreEqual(this.macBlock, array))
			{
				throw new InvalidCipherTextException("mac check in OCB failed");
			}
			this.Reset(false);
			return num;
		}

		// Token: 0x0600316A RID: 12650 RVA: 0x0012EA6E File Offset: 0x0012CC6E
		public virtual void Reset()
		{
			this.Reset(true);
		}

		// Token: 0x0600316B RID: 12651 RVA: 0x0012EA77 File Offset: 0x0012CC77
		protected virtual void Clear(byte[] bs)
		{
			if (bs != null)
			{
				Array.Clear(bs, 0, bs.Length);
			}
		}

		// Token: 0x0600316C RID: 12652 RVA: 0x0012EA88 File Offset: 0x0012CC88
		protected virtual byte[] GetLSub(int n)
		{
			while (n >= this.L.Count)
			{
				this.L.Add(OcbBlockCipher.OCB_double((byte[])this.L[this.L.Count - 1]));
			}
			return (byte[])this.L[n];
		}

		// Token: 0x0600316D RID: 12653 RVA: 0x0012EAE4 File Offset: 0x0012CCE4
		protected virtual void ProcessHashBlock()
		{
			long x = this.hashBlockCount + 1L;
			this.hashBlockCount = x;
			this.UpdateHASH(this.GetLSub(OcbBlockCipher.OCB_ntz(x)));
			this.hashBlockPos = 0;
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x0012EB1C File Offset: 0x0012CD1C
		protected virtual void ProcessMainBlock(byte[] output, int outOff)
		{
			Check.DataLength(output, outOff, 16, "Output buffer too short");
			if (this.forEncryption)
			{
				OcbBlockCipher.Xor(this.Checksum, this.mainBlock);
				this.mainBlockPos = 0;
			}
			byte[] offsetMAIN = this.OffsetMAIN;
			long x = this.mainBlockCount + 1L;
			this.mainBlockCount = x;
			OcbBlockCipher.Xor(offsetMAIN, this.GetLSub(OcbBlockCipher.OCB_ntz(x)));
			OcbBlockCipher.Xor(this.mainBlock, this.OffsetMAIN);
			this.mainCipher.ProcessBlock(this.mainBlock, 0, this.mainBlock, 0);
			OcbBlockCipher.Xor(this.mainBlock, this.OffsetMAIN);
			Array.Copy(this.mainBlock, 0, output, outOff, 16);
			if (!this.forEncryption)
			{
				OcbBlockCipher.Xor(this.Checksum, this.mainBlock);
				Array.Copy(this.mainBlock, 16, this.mainBlock, 0, this.macSize);
				this.mainBlockPos = this.macSize;
			}
		}

		// Token: 0x0600316F RID: 12655 RVA: 0x0012EC0C File Offset: 0x0012CE0C
		protected virtual void Reset(bool clearMac)
		{
			this.hashCipher.Reset();
			this.mainCipher.Reset();
			this.Clear(this.hashBlock);
			this.Clear(this.mainBlock);
			this.hashBlockPos = 0;
			this.mainBlockPos = 0;
			this.hashBlockCount = 0L;
			this.mainBlockCount = 0L;
			this.Clear(this.OffsetHASH);
			this.Clear(this.Sum);
			Array.Copy(this.OffsetMAIN_0, 0, this.OffsetMAIN, 0, 16);
			this.Clear(this.Checksum);
			if (clearMac)
			{
				this.macBlock = null;
			}
			if (this.initialAssociatedText != null)
			{
				this.ProcessAadBytes(this.initialAssociatedText, 0, this.initialAssociatedText.Length);
			}
		}

		// Token: 0x06003170 RID: 12656 RVA: 0x0012ECC8 File Offset: 0x0012CEC8
		protected virtual void UpdateHASH(byte[] LSub)
		{
			OcbBlockCipher.Xor(this.OffsetHASH, LSub);
			OcbBlockCipher.Xor(this.hashBlock, this.OffsetHASH);
			this.hashCipher.ProcessBlock(this.hashBlock, 0, this.hashBlock, 0);
			OcbBlockCipher.Xor(this.Sum, this.hashBlock);
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x0012ED20 File Offset: 0x0012CF20
		protected static byte[] OCB_double(byte[] block)
		{
			byte[] array = new byte[16];
			int num = OcbBlockCipher.ShiftLeft(block, array);
			byte[] array2 = array;
			int num2 = 15;
			array2[num2] ^= (byte)(135 >> (1 - num << 3));
			return array;
		}

		// Token: 0x06003172 RID: 12658 RVA: 0x0012ED5A File Offset: 0x0012CF5A
		protected static void OCB_extend(byte[] block, int pos)
		{
			block[pos] = 128;
			while (++pos < 16)
			{
				block[pos] = 0;
			}
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x0012ED74 File Offset: 0x0012CF74
		protected static int OCB_ntz(long x)
		{
			if (x == 0L)
			{
				return 64;
			}
			int num = 0;
			ulong num2 = (ulong)x;
			while ((num2 & 1UL) == 0UL)
			{
				num++;
				num2 >>= 1;
			}
			return num;
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x0012ED9C File Offset: 0x0012CF9C
		protected static int ShiftLeft(byte[] block, byte[] output)
		{
			int num = 16;
			uint num2 = 0U;
			while (--num >= 0)
			{
				uint num3 = (uint)block[num];
				output[num] = (byte)(num3 << 1 | num2);
				num2 = (num3 >> 7 & 1U);
			}
			return (int)num2;
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x0012EDCC File Offset: 0x0012CFCC
		protected static void Xor(byte[] block, byte[] val)
		{
			for (int i = 15; i >= 0; i--)
			{
				int num = i;
				block[num] ^= val[i];
			}
		}

		// Token: 0x04001F84 RID: 8068
		private const int BLOCK_SIZE = 16;

		// Token: 0x04001F85 RID: 8069
		private readonly IBlockCipher hashCipher;

		// Token: 0x04001F86 RID: 8070
		private readonly IBlockCipher mainCipher;

		// Token: 0x04001F87 RID: 8071
		private bool forEncryption;

		// Token: 0x04001F88 RID: 8072
		private int macSize;

		// Token: 0x04001F89 RID: 8073
		private byte[] initialAssociatedText;

		// Token: 0x04001F8A RID: 8074
		private IList L;

		// Token: 0x04001F8B RID: 8075
		private byte[] L_Asterisk;

		// Token: 0x04001F8C RID: 8076
		private byte[] L_Dollar;

		// Token: 0x04001F8D RID: 8077
		private byte[] KtopInput;

		// Token: 0x04001F8E RID: 8078
		private byte[] Stretch = new byte[24];

		// Token: 0x04001F8F RID: 8079
		private byte[] OffsetMAIN_0 = new byte[16];

		// Token: 0x04001F90 RID: 8080
		private byte[] hashBlock;

		// Token: 0x04001F91 RID: 8081
		private byte[] mainBlock;

		// Token: 0x04001F92 RID: 8082
		private int hashBlockPos;

		// Token: 0x04001F93 RID: 8083
		private int mainBlockPos;

		// Token: 0x04001F94 RID: 8084
		private long hashBlockCount;

		// Token: 0x04001F95 RID: 8085
		private long mainBlockCount;

		// Token: 0x04001F96 RID: 8086
		private byte[] OffsetHASH;

		// Token: 0x04001F97 RID: 8087
		private byte[] Sum;

		// Token: 0x04001F98 RID: 8088
		private byte[] OffsetMAIN = new byte[16];

		// Token: 0x04001F99 RID: 8089
		private byte[] Checksum;

		// Token: 0x04001F9A RID: 8090
		private byte[] macBlock;
	}
}
