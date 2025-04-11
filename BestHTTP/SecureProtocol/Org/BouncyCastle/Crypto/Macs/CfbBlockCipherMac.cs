using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Paddings;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x0200051B RID: 1307
	public class CfbBlockCipherMac : IMac
	{
		// Token: 0x060031DB RID: 12763 RVA: 0x00130A7C File Offset: 0x0012EC7C
		public CfbBlockCipherMac(IBlockCipher cipher) : this(cipher, 8, cipher.GetBlockSize() * 8 / 2, null)
		{
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x00130A91 File Offset: 0x0012EC91
		public CfbBlockCipherMac(IBlockCipher cipher, IBlockCipherPadding padding) : this(cipher, 8, cipher.GetBlockSize() * 8 / 2, padding)
		{
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x00130AA6 File Offset: 0x0012ECA6
		public CfbBlockCipherMac(IBlockCipher cipher, int cfbBitSize, int macSizeInBits) : this(cipher, cfbBitSize, macSizeInBits, null)
		{
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x00130AB4 File Offset: 0x0012ECB4
		public CfbBlockCipherMac(IBlockCipher cipher, int cfbBitSize, int macSizeInBits, IBlockCipherPadding padding)
		{
			if (macSizeInBits % 8 != 0)
			{
				throw new ArgumentException("MAC size must be multiple of 8");
			}
			this.mac = new byte[cipher.GetBlockSize()];
			this.cipher = new MacCFBBlockCipher(cipher, cfbBitSize);
			this.padding = padding;
			this.macSize = macSizeInBits / 8;
			this.Buffer = new byte[this.cipher.GetBlockSize()];
			this.bufOff = 0;
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x060031DF RID: 12767 RVA: 0x00130B23 File Offset: 0x0012ED23
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName;
			}
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x00130B30 File Offset: 0x0012ED30
		public void Init(ICipherParameters parameters)
		{
			this.Reset();
			this.cipher.Init(true, parameters);
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x00130B45 File Offset: 0x0012ED45
		public int GetMacSize()
		{
			return this.macSize;
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x00130B50 File Offset: 0x0012ED50
		public void Update(byte input)
		{
			if (this.bufOff == this.Buffer.Length)
			{
				this.cipher.ProcessBlock(this.Buffer, 0, this.mac, 0);
				this.bufOff = 0;
			}
			byte[] buffer = this.Buffer;
			int num = this.bufOff;
			this.bufOff = num + 1;
			buffer[num] = input;
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x00130BA8 File Offset: 0x0012EDA8
		public void BlockUpdate(byte[] input, int inOff, int len)
		{
			if (len < 0)
			{
				throw new ArgumentException("Can't have a negative input length!");
			}
			int blockSize = this.cipher.GetBlockSize();
			int num = 0;
			int num2 = blockSize - this.bufOff;
			if (len > num2)
			{
				Array.Copy(input, inOff, this.Buffer, this.bufOff, num2);
				num += this.cipher.ProcessBlock(this.Buffer, 0, this.mac, 0);
				this.bufOff = 0;
				len -= num2;
				inOff += num2;
				while (len > blockSize)
				{
					num += this.cipher.ProcessBlock(input, inOff, this.mac, 0);
					len -= blockSize;
					inOff += blockSize;
				}
			}
			Array.Copy(input, inOff, this.Buffer, this.bufOff, len);
			this.bufOff += len;
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x00130C6C File Offset: 0x0012EE6C
		public int DoFinal(byte[] output, int outOff)
		{
			int blockSize = this.cipher.GetBlockSize();
			if (this.padding == null)
			{
				while (this.bufOff < blockSize)
				{
					byte[] buffer = this.Buffer;
					int num = this.bufOff;
					this.bufOff = num + 1;
					buffer[num] = 0;
				}
			}
			else
			{
				this.padding.AddPadding(this.Buffer, this.bufOff);
			}
			this.cipher.ProcessBlock(this.Buffer, 0, this.mac, 0);
			this.cipher.GetMacBlock(this.mac);
			Array.Copy(this.mac, 0, output, outOff, this.macSize);
			this.Reset();
			return this.macSize;
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x00130D16 File Offset: 0x0012EF16
		public void Reset()
		{
			Array.Clear(this.Buffer, 0, this.Buffer.Length);
			this.bufOff = 0;
			this.cipher.Reset();
		}

		// Token: 0x04001FC1 RID: 8129
		private byte[] mac;

		// Token: 0x04001FC2 RID: 8130
		private byte[] Buffer;

		// Token: 0x04001FC3 RID: 8131
		private int bufOff;

		// Token: 0x04001FC4 RID: 8132
		private MacCFBBlockCipher cipher;

		// Token: 0x04001FC5 RID: 8133
		private IBlockCipherPadding padding;

		// Token: 0x04001FC6 RID: 8134
		private int macSize;
	}
}
