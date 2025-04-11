using System;
using System.IO;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x0200050B RID: 1291
	public class KCcmBlockCipher : IAeadBlockCipher
	{
		// Token: 0x06003138 RID: 12600 RVA: 0x0012D461 File Offset: 0x0012B661
		private void setNb(int Nb)
		{
			if (Nb == 4 || Nb == 6 || Nb == 8)
			{
				this.Nb_ = Nb;
				return;
			}
			throw new ArgumentException("Nb = 4 is recommended by DSTU7624 but can be changed to only 6 or 8 in this implementation");
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x0012D481 File Offset: 0x0012B681
		public KCcmBlockCipher(IBlockCipher engine) : this(engine, 4)
		{
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x0012D48C File Offset: 0x0012B68C
		public KCcmBlockCipher(IBlockCipher engine, int Nb)
		{
			this.engine = engine;
			this.macSize = engine.GetBlockSize();
			this.nonce = new byte[engine.GetBlockSize()];
			this.initialAssociatedText = new byte[engine.GetBlockSize()];
			this.mac = new byte[engine.GetBlockSize()];
			this.macBlock = new byte[engine.GetBlockSize()];
			this.G1 = new byte[engine.GetBlockSize()];
			this.buffer = new byte[engine.GetBlockSize()];
			this.s = new byte[engine.GetBlockSize()];
			this.counter = new byte[engine.GetBlockSize()];
			this.setNb(Nb);
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x0012D560 File Offset: 0x0012B760
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			ICipherParameters parameters2;
			if (parameters is AeadParameters)
			{
				AeadParameters aeadParameters = (AeadParameters)parameters;
				if (aeadParameters.MacSize > KCcmBlockCipher.MAX_MAC_BIT_LENGTH || aeadParameters.MacSize < KCcmBlockCipher.MIN_MAC_BIT_LENGTH || aeadParameters.MacSize % 8 != 0)
				{
					throw new ArgumentException("Invalid mac size specified");
				}
				this.nonce = aeadParameters.GetNonce();
				this.macSize = aeadParameters.MacSize / KCcmBlockCipher.BITS_IN_BYTE;
				this.initialAssociatedText = aeadParameters.GetAssociatedText();
				parameters2 = aeadParameters.Key;
			}
			else
			{
				if (!(parameters is ParametersWithIV))
				{
					throw new ArgumentException("Invalid parameters specified");
				}
				this.nonce = ((ParametersWithIV)parameters).GetIV();
				this.macSize = this.engine.GetBlockSize();
				this.initialAssociatedText = null;
				parameters2 = ((ParametersWithIV)parameters).Parameters;
			}
			this.mac = new byte[this.macSize];
			this.forEncryption = forEncryption;
			this.engine.Init(true, parameters2);
			this.counter[0] = 1;
			if (this.initialAssociatedText != null)
			{
				this.ProcessAadBytes(this.initialAssociatedText, 0, this.initialAssociatedText.Length);
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x0600313C RID: 12604 RVA: 0x0012D673 File Offset: 0x0012B873
		public virtual string AlgorithmName
		{
			get
			{
				return this.engine.AlgorithmName + "/KCCM";
			}
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x0012D68A File Offset: 0x0012B88A
		public virtual int GetBlockSize()
		{
			return this.engine.GetBlockSize();
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x0012D697 File Offset: 0x0012B897
		public virtual IBlockCipher GetUnderlyingCipher()
		{
			return this.engine;
		}

		// Token: 0x0600313F RID: 12607 RVA: 0x0012D69F File Offset: 0x0012B89F
		public virtual void ProcessAadByte(byte input)
		{
			this.associatedText.WriteByte(input);
		}

		// Token: 0x06003140 RID: 12608 RVA: 0x0012D6AD File Offset: 0x0012B8AD
		public virtual void ProcessAadBytes(byte[] input, int inOff, int len)
		{
			this.associatedText.Write(input, inOff, len);
		}

		// Token: 0x06003141 RID: 12609 RVA: 0x0012D6C0 File Offset: 0x0012B8C0
		private void ProcessAAD(byte[] assocText, int assocOff, int assocLen, int dataLen)
		{
			if (assocLen - assocOff < this.engine.GetBlockSize())
			{
				throw new ArgumentException("authText buffer too short");
			}
			if (assocLen % this.engine.GetBlockSize() != 0)
			{
				throw new ArgumentException("padding not supported");
			}
			Array.Copy(this.nonce, 0, this.G1, 0, this.nonce.Length - this.Nb_ - 1);
			this.intToBytes(dataLen, this.buffer, 0);
			Array.Copy(this.buffer, 0, this.G1, this.nonce.Length - this.Nb_ - 1, KCcmBlockCipher.BYTES_IN_INT);
			this.G1[this.G1.Length - 1] = this.getFlag(true, this.macSize);
			this.engine.ProcessBlock(this.G1, 0, this.macBlock, 0);
			this.intToBytes(assocLen, this.buffer, 0);
			if (assocLen <= this.engine.GetBlockSize() - this.Nb_)
			{
				for (int i = 0; i < assocLen; i++)
				{
					byte[] array = this.buffer;
					int num = i + this.Nb_;
					array[num] ^= assocText[assocOff + i];
				}
				for (int j = 0; j < this.engine.GetBlockSize(); j++)
				{
					byte[] array2 = this.macBlock;
					int num2 = j;
					array2[num2] ^= this.buffer[j];
				}
				this.engine.ProcessBlock(this.macBlock, 0, this.macBlock, 0);
				return;
			}
			for (int k = 0; k < this.engine.GetBlockSize(); k++)
			{
				byte[] array3 = this.macBlock;
				int num3 = k;
				array3[num3] ^= this.buffer[k];
			}
			this.engine.ProcessBlock(this.macBlock, 0, this.macBlock, 0);
			for (int num4 = assocLen; num4 != 0; num4 -= this.engine.GetBlockSize())
			{
				for (int l = 0; l < this.engine.GetBlockSize(); l++)
				{
					byte[] array4 = this.macBlock;
					int num5 = l;
					array4[num5] ^= assocText[l + assocOff];
				}
				this.engine.ProcessBlock(this.macBlock, 0, this.macBlock, 0);
				assocOff += this.engine.GetBlockSize();
			}
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x0012D8E4 File Offset: 0x0012BAE4
		public virtual int ProcessByte(byte input, byte[] output, int outOff)
		{
			this.data.WriteByte(input);
			return 0;
		}

		// Token: 0x06003143 RID: 12611 RVA: 0x0012D8F3 File Offset: 0x0012BAF3
		public virtual int ProcessBytes(byte[] input, int inOff, int inLen, byte[] output, int outOff)
		{
			Check.DataLength(input, inOff, inLen, "input buffer too short");
			this.data.Write(input, inOff, inLen);
			return 0;
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x0012D914 File Offset: 0x0012BB14
		public int ProcessPacket(byte[] input, int inOff, int len, byte[] output, int outOff)
		{
			Check.DataLength(input, inOff, len, "input buffer too short");
			Check.OutputLength(output, outOff, len, "output buffer too short");
			if (this.associatedText.Length > 0L)
			{
				byte[] assocText = this.associatedText.GetBuffer();
				int assocLen = (int)this.associatedText.Length;
				int dataLen = this.forEncryption ? ((int)this.data.Length) : ((int)this.data.Length - this.macSize);
				this.ProcessAAD(assocText, 0, assocLen, dataLen);
			}
			if (this.forEncryption)
			{
				Check.DataLength(len % this.engine.GetBlockSize() != 0, "partial blocks not supported");
				this.CalculateMac(input, inOff, len);
				this.engine.ProcessBlock(this.nonce, 0, this.s, 0);
				int i = len;
				while (i > 0)
				{
					this.ProcessBlock(input, inOff, len, output, outOff);
					i -= this.engine.GetBlockSize();
					inOff += this.engine.GetBlockSize();
					outOff += this.engine.GetBlockSize();
				}
				for (int j = 0; j < this.counter.Length; j++)
				{
					byte[] array = this.s;
					int num = j;
					array[num] += this.counter[j];
				}
				this.engine.ProcessBlock(this.s, 0, this.buffer, 0);
				for (int k = 0; k < this.macSize; k++)
				{
					output[outOff + k] = (this.buffer[k] ^ this.macBlock[k]);
				}
				Array.Copy(this.macBlock, 0, this.mac, 0, this.macSize);
				this.Reset();
				return len + this.macSize;
			}
			Check.DataLength((len - this.macSize) % this.engine.GetBlockSize() != 0, "partial blocks not supported");
			this.engine.ProcessBlock(this.nonce, 0, this.s, 0);
			int num2 = len / this.engine.GetBlockSize();
			for (int l = 0; l < num2; l++)
			{
				this.ProcessBlock(input, inOff, len, output, outOff);
				inOff += this.engine.GetBlockSize();
				outOff += this.engine.GetBlockSize();
			}
			if (len > inOff)
			{
				for (int m = 0; m < this.counter.Length; m++)
				{
					byte[] array2 = this.s;
					int num3 = m;
					array2[num3] += this.counter[m];
				}
				this.engine.ProcessBlock(this.s, 0, this.buffer, 0);
				for (int n = 0; n < this.macSize; n++)
				{
					output[outOff + n] = (this.buffer[n] ^ input[inOff + n]);
				}
				outOff += this.macSize;
			}
			for (int num4 = 0; num4 < this.counter.Length; num4++)
			{
				byte[] array3 = this.s;
				int num5 = num4;
				array3[num5] += this.counter[num4];
			}
			this.engine.ProcessBlock(this.s, 0, this.buffer, 0);
			Array.Copy(output, outOff - this.macSize, this.buffer, 0, this.macSize);
			this.CalculateMac(output, 0, outOff - this.macSize);
			Array.Copy(this.macBlock, 0, this.mac, 0, this.macSize);
			byte[] array4 = new byte[this.macSize];
			Array.Copy(this.buffer, 0, array4, 0, this.macSize);
			if (!Arrays.ConstantTimeAreEqual(this.mac, array4))
			{
				throw new InvalidCipherTextException("mac check failed");
			}
			this.Reset();
			return len - this.macSize;
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x0012DCBC File Offset: 0x0012BEBC
		private void ProcessBlock(byte[] input, int inOff, int len, byte[] output, int outOff)
		{
			for (int i = 0; i < this.counter.Length; i++)
			{
				byte[] array = this.s;
				int num = i;
				array[num] += this.counter[i];
			}
			this.engine.ProcessBlock(this.s, 0, this.buffer, 0);
			for (int j = 0; j < this.engine.GetBlockSize(); j++)
			{
				output[outOff + j] = (this.buffer[j] ^ input[inOff + j]);
			}
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x0012DD3C File Offset: 0x0012BF3C
		private void CalculateMac(byte[] authText, int authOff, int len)
		{
			int i = len;
			while (i > 0)
			{
				for (int j = 0; j < this.engine.GetBlockSize(); j++)
				{
					byte[] array = this.macBlock;
					int num = j;
					array[num] ^= authText[authOff + j];
				}
				this.engine.ProcessBlock(this.macBlock, 0, this.macBlock, 0);
				i -= this.engine.GetBlockSize();
				authOff += this.engine.GetBlockSize();
			}
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x0012DDB4 File Offset: 0x0012BFB4
		public virtual int DoFinal(byte[] output, int outOff)
		{
			byte[] input = this.data.GetBuffer();
			int len = (int)this.data.Length;
			int result = this.ProcessPacket(input, 0, len, output, outOff);
			this.Reset();
			return result;
		}

		// Token: 0x06003148 RID: 12616 RVA: 0x0012DDEB File Offset: 0x0012BFEB
		public virtual byte[] GetMac()
		{
			return Arrays.Clone(this.mac);
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x000A6AED File Offset: 0x000A4CED
		public virtual int GetUpdateOutputSize(int len)
		{
			return len;
		}

		// Token: 0x0600314A RID: 12618 RVA: 0x0012DDF8 File Offset: 0x0012BFF8
		public virtual int GetOutputSize(int len)
		{
			return len + this.macSize;
		}

		// Token: 0x0600314B RID: 12619 RVA: 0x0012DE04 File Offset: 0x0012C004
		public virtual void Reset()
		{
			Arrays.Fill(this.G1, 0);
			Arrays.Fill(this.buffer, 0);
			Arrays.Fill(this.counter, 0);
			Arrays.Fill(this.macBlock, 0);
			this.counter[0] = 1;
			this.data.SetLength(0L);
			this.associatedText.SetLength(0L);
			if (this.initialAssociatedText != null)
			{
				this.ProcessAadBytes(this.initialAssociatedText, 0, this.initialAssociatedText.Length);
			}
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x0012D43D File Offset: 0x0012B63D
		private void intToBytes(int num, byte[] outBytes, int outOff)
		{
			outBytes[outOff + 3] = (byte)(num >> 24);
			outBytes[outOff + 2] = (byte)(num >> 16);
			outBytes[outOff + 1] = (byte)(num >> 8);
			outBytes[outOff] = (byte)num;
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x0012DE84 File Offset: 0x0012C084
		private byte getFlag(bool authTextPresents, int macSize)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (authTextPresents)
			{
				stringBuilder.Append("1");
			}
			else
			{
				stringBuilder.Append("0");
			}
			if (macSize <= 16)
			{
				if (macSize != 8)
				{
					if (macSize == 16)
					{
						stringBuilder.Append("011");
					}
				}
				else
				{
					stringBuilder.Append("010");
				}
			}
			else if (macSize != 32)
			{
				if (macSize != 48)
				{
					if (macSize == 64)
					{
						stringBuilder.Append("110");
					}
				}
				else
				{
					stringBuilder.Append("101");
				}
			}
			else
			{
				stringBuilder.Append("100");
			}
			string text = Convert.ToString(this.Nb_ - 1, 2);
			while (text.Length < 4)
			{
				text = new StringBuilder(text).Insert(0, "0").ToString();
			}
			stringBuilder.Append(text);
			return (byte)Convert.ToInt32(stringBuilder.ToString(), 2);
		}

		// Token: 0x04001F6B RID: 8043
		private static readonly int BYTES_IN_INT = 4;

		// Token: 0x04001F6C RID: 8044
		private static readonly int BITS_IN_BYTE = 8;

		// Token: 0x04001F6D RID: 8045
		private static readonly int MAX_MAC_BIT_LENGTH = 512;

		// Token: 0x04001F6E RID: 8046
		private static readonly int MIN_MAC_BIT_LENGTH = 64;

		// Token: 0x04001F6F RID: 8047
		private IBlockCipher engine;

		// Token: 0x04001F70 RID: 8048
		private int macSize;

		// Token: 0x04001F71 RID: 8049
		private bool forEncryption;

		// Token: 0x04001F72 RID: 8050
		private byte[] initialAssociatedText;

		// Token: 0x04001F73 RID: 8051
		private byte[] mac;

		// Token: 0x04001F74 RID: 8052
		private byte[] macBlock;

		// Token: 0x04001F75 RID: 8053
		private byte[] nonce;

		// Token: 0x04001F76 RID: 8054
		private byte[] G1;

		// Token: 0x04001F77 RID: 8055
		private byte[] buffer;

		// Token: 0x04001F78 RID: 8056
		private byte[] s;

		// Token: 0x04001F79 RID: 8057
		private byte[] counter;

		// Token: 0x04001F7A RID: 8058
		private readonly MemoryStream associatedText = new MemoryStream();

		// Token: 0x04001F7B RID: 8059
		private readonly MemoryStream data = new MemoryStream();

		// Token: 0x04001F7C RID: 8060
		private int Nb_ = 4;
	}
}
