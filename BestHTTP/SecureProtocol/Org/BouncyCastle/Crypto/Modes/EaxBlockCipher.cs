using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Macs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x02000507 RID: 1287
	public class EaxBlockCipher : IAeadBlockCipher
	{
		// Token: 0x060030F5 RID: 12533 RVA: 0x0012BD64 File Offset: 0x00129F64
		public EaxBlockCipher(IBlockCipher cipher)
		{
			this.blockSize = cipher.GetBlockSize();
			this.mac = new CMac(cipher);
			this.macBlock = new byte[this.blockSize];
			this.associatedTextMac = new byte[this.mac.GetMacSize()];
			this.nonceMac = new byte[this.mac.GetMacSize()];
			this.cipher = new SicBlockCipher(cipher);
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x060030F6 RID: 12534 RVA: 0x0012BDD8 File Offset: 0x00129FD8
		public virtual string AlgorithmName
		{
			get
			{
				return this.cipher.GetUnderlyingCipher().AlgorithmName + "/EAX";
			}
		}

		// Token: 0x060030F7 RID: 12535 RVA: 0x0012BDF4 File Offset: 0x00129FF4
		public virtual IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x060030F8 RID: 12536 RVA: 0x0012BDFC File Offset: 0x00129FFC
		public virtual int GetBlockSize()
		{
			return this.cipher.GetBlockSize();
		}

		// Token: 0x060030F9 RID: 12537 RVA: 0x0012BE0C File Offset: 0x0012A00C
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.forEncryption = forEncryption;
			byte[] array;
			ICipherParameters parameters2;
			if (parameters is AeadParameters)
			{
				AeadParameters aeadParameters = (AeadParameters)parameters;
				array = aeadParameters.GetNonce();
				this.initialAssociatedText = aeadParameters.GetAssociatedText();
				this.macSize = aeadParameters.MacSize / 8;
				parameters2 = aeadParameters.Key;
			}
			else
			{
				if (!(parameters is ParametersWithIV))
				{
					throw new ArgumentException("invalid parameters passed to EAX");
				}
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				array = parametersWithIV.GetIV();
				this.initialAssociatedText = null;
				this.macSize = this.mac.GetMacSize() / 2;
				parameters2 = parametersWithIV.Parameters;
			}
			this.bufBlock = new byte[forEncryption ? this.blockSize : (this.blockSize + this.macSize)];
			byte[] array2 = new byte[this.blockSize];
			this.mac.Init(parameters2);
			array2[this.blockSize - 1] = 0;
			this.mac.BlockUpdate(array2, 0, this.blockSize);
			this.mac.BlockUpdate(array, 0, array.Length);
			this.mac.DoFinal(this.nonceMac, 0);
			this.cipher.Init(true, new ParametersWithIV(null, this.nonceMac));
			this.Reset();
		}

		// Token: 0x060030FA RID: 12538 RVA: 0x0012BF38 File Offset: 0x0012A138
		private void InitCipher()
		{
			if (this.cipherInitialized)
			{
				return;
			}
			this.cipherInitialized = true;
			this.mac.DoFinal(this.associatedTextMac, 0);
			byte[] array = new byte[this.blockSize];
			array[this.blockSize - 1] = 2;
			this.mac.BlockUpdate(array, 0, this.blockSize);
		}

		// Token: 0x060030FB RID: 12539 RVA: 0x0012BF94 File Offset: 0x0012A194
		private void CalculateMac()
		{
			byte[] array = new byte[this.blockSize];
			this.mac.DoFinal(array, 0);
			for (int i = 0; i < this.macBlock.Length; i++)
			{
				this.macBlock[i] = (this.nonceMac[i] ^ this.associatedTextMac[i] ^ array[i]);
			}
		}

		// Token: 0x060030FC RID: 12540 RVA: 0x0012BFEC File Offset: 0x0012A1EC
		public virtual void Reset()
		{
			this.Reset(true);
		}

		// Token: 0x060030FD RID: 12541 RVA: 0x0012BFF8 File Offset: 0x0012A1F8
		private void Reset(bool clearMac)
		{
			this.cipher.Reset();
			this.mac.Reset();
			this.bufOff = 0;
			Array.Clear(this.bufBlock, 0, this.bufBlock.Length);
			if (clearMac)
			{
				Array.Clear(this.macBlock, 0, this.macBlock.Length);
			}
			byte[] array = new byte[this.blockSize];
			array[this.blockSize - 1] = 1;
			this.mac.BlockUpdate(array, 0, this.blockSize);
			this.cipherInitialized = false;
			if (this.initialAssociatedText != null)
			{
				this.ProcessAadBytes(this.initialAssociatedText, 0, this.initialAssociatedText.Length);
			}
		}

		// Token: 0x060030FE RID: 12542 RVA: 0x0012C09B File Offset: 0x0012A29B
		public virtual void ProcessAadByte(byte input)
		{
			if (this.cipherInitialized)
			{
				throw new InvalidOperationException("AAD data cannot be added after encryption/decryption processing has begun.");
			}
			this.mac.Update(input);
		}

		// Token: 0x060030FF RID: 12543 RVA: 0x0012C0BC File Offset: 0x0012A2BC
		public virtual void ProcessAadBytes(byte[] inBytes, int inOff, int len)
		{
			if (this.cipherInitialized)
			{
				throw new InvalidOperationException("AAD data cannot be added after encryption/decryption processing has begun.");
			}
			this.mac.BlockUpdate(inBytes, inOff, len);
		}

		// Token: 0x06003100 RID: 12544 RVA: 0x0012C0DF File Offset: 0x0012A2DF
		public virtual int ProcessByte(byte input, byte[] outBytes, int outOff)
		{
			this.InitCipher();
			return this.Process(input, outBytes, outOff);
		}

		// Token: 0x06003101 RID: 12545 RVA: 0x0012C0F0 File Offset: 0x0012A2F0
		public virtual int ProcessBytes(byte[] inBytes, int inOff, int len, byte[] outBytes, int outOff)
		{
			this.InitCipher();
			int num = 0;
			for (int num2 = 0; num2 != len; num2++)
			{
				num += this.Process(inBytes[inOff + num2], outBytes, outOff + num);
			}
			return num;
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x0012C128 File Offset: 0x0012A328
		public virtual int DoFinal(byte[] outBytes, int outOff)
		{
			this.InitCipher();
			int num = this.bufOff;
			byte[] array = new byte[this.bufBlock.Length];
			this.bufOff = 0;
			if (this.forEncryption)
			{
				Check.OutputLength(outBytes, outOff, num + this.macSize, "Output buffer too short");
				this.cipher.ProcessBlock(this.bufBlock, 0, array, 0);
				Array.Copy(array, 0, outBytes, outOff, num);
				this.mac.BlockUpdate(array, 0, num);
				this.CalculateMac();
				Array.Copy(this.macBlock, 0, outBytes, outOff + num, this.macSize);
				this.Reset(false);
				return num + this.macSize;
			}
			if (num < this.macSize)
			{
				throw new InvalidCipherTextException("data too short");
			}
			Check.OutputLength(outBytes, outOff, num - this.macSize, "Output buffer too short");
			if (num > this.macSize)
			{
				this.mac.BlockUpdate(this.bufBlock, 0, num - this.macSize);
				this.cipher.ProcessBlock(this.bufBlock, 0, array, 0);
				Array.Copy(array, 0, outBytes, outOff, num - this.macSize);
			}
			this.CalculateMac();
			if (!this.VerifyMac(this.bufBlock, num - this.macSize))
			{
				throw new InvalidCipherTextException("mac check in EAX failed");
			}
			this.Reset(false);
			return num - this.macSize;
		}

		// Token: 0x06003103 RID: 12547 RVA: 0x0012C274 File Offset: 0x0012A474
		public virtual byte[] GetMac()
		{
			byte[] array = new byte[this.macSize];
			Array.Copy(this.macBlock, 0, array, 0, this.macSize);
			return array;
		}

		// Token: 0x06003104 RID: 12548 RVA: 0x0012C2A4 File Offset: 0x0012A4A4
		public virtual int GetUpdateOutputSize(int len)
		{
			int num = len + this.bufOff;
			if (!this.forEncryption)
			{
				if (num < this.macSize)
				{
					return 0;
				}
				num -= this.macSize;
			}
			return num - num % this.blockSize;
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x0012C2E0 File Offset: 0x0012A4E0
		public virtual int GetOutputSize(int len)
		{
			int num = len + this.bufOff;
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

		// Token: 0x06003106 RID: 12550 RVA: 0x0012C31C File Offset: 0x0012A51C
		private int Process(byte b, byte[] outBytes, int outOff)
		{
			byte[] array = this.bufBlock;
			int num = this.bufOff;
			this.bufOff = num + 1;
			array[num] = b;
			if (this.bufOff == this.bufBlock.Length)
			{
				Check.OutputLength(outBytes, outOff, this.blockSize, "Output buffer is too short");
				int result;
				if (this.forEncryption)
				{
					result = this.cipher.ProcessBlock(this.bufBlock, 0, outBytes, outOff);
					this.mac.BlockUpdate(outBytes, outOff, this.blockSize);
				}
				else
				{
					this.mac.BlockUpdate(this.bufBlock, 0, this.blockSize);
					result = this.cipher.ProcessBlock(this.bufBlock, 0, outBytes, outOff);
				}
				this.bufOff = 0;
				if (!this.forEncryption)
				{
					Array.Copy(this.bufBlock, this.blockSize, this.bufBlock, 0, this.macSize);
					this.bufOff = this.macSize;
				}
				return result;
			}
			return 0;
		}

		// Token: 0x06003107 RID: 12551 RVA: 0x0012C404 File Offset: 0x0012A604
		private bool VerifyMac(byte[] mac, int off)
		{
			int num = 0;
			for (int i = 0; i < this.macSize; i++)
			{
				num |= (int)(this.macBlock[i] ^ mac[off + i]);
			}
			return num == 0;
		}

		// Token: 0x04001F3B RID: 7995
		private SicBlockCipher cipher;

		// Token: 0x04001F3C RID: 7996
		private bool forEncryption;

		// Token: 0x04001F3D RID: 7997
		private int blockSize;

		// Token: 0x04001F3E RID: 7998
		private IMac mac;

		// Token: 0x04001F3F RID: 7999
		private byte[] nonceMac;

		// Token: 0x04001F40 RID: 8000
		private byte[] associatedTextMac;

		// Token: 0x04001F41 RID: 8001
		private byte[] macBlock;

		// Token: 0x04001F42 RID: 8002
		private int macSize;

		// Token: 0x04001F43 RID: 8003
		private byte[] bufBlock;

		// Token: 0x04001F44 RID: 8004
		private int bufOff;

		// Token: 0x04001F45 RID: 8005
		private bool cipherInitialized;

		// Token: 0x04001F46 RID: 8006
		private byte[] initialAssociatedText;

		// Token: 0x0200092F RID: 2351
		private enum Tag : byte
		{
			// Token: 0x04003508 RID: 13576
			N,
			// Token: 0x04003509 RID: 13577
			H,
			// Token: 0x0400350A RID: 13578
			C
		}
	}
}
