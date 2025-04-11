using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Paddings;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x02000519 RID: 1305
	public class CbcBlockCipherMac : IMac
	{
		// Token: 0x060031C8 RID: 12744 RVA: 0x001305BC File Offset: 0x0012E7BC
		public CbcBlockCipherMac(IBlockCipher cipher) : this(cipher, cipher.GetBlockSize() * 8 / 2, null)
		{
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x001305D0 File Offset: 0x0012E7D0
		public CbcBlockCipherMac(IBlockCipher cipher, IBlockCipherPadding padding) : this(cipher, cipher.GetBlockSize() * 8 / 2, padding)
		{
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x001305E4 File Offset: 0x0012E7E4
		public CbcBlockCipherMac(IBlockCipher cipher, int macSizeInBits) : this(cipher, macSizeInBits, null)
		{
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x001305F0 File Offset: 0x0012E7F0
		public CbcBlockCipherMac(IBlockCipher cipher, int macSizeInBits, IBlockCipherPadding padding)
		{
			if (macSizeInBits % 8 != 0)
			{
				throw new ArgumentException("MAC size must be multiple of 8");
			}
			this.cipher = new CbcBlockCipher(cipher);
			this.padding = padding;
			this.macSize = macSizeInBits / 8;
			this.buf = new byte[cipher.GetBlockSize()];
			this.bufOff = 0;
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060031CC RID: 12748 RVA: 0x00130647 File Offset: 0x0012E847
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName;
			}
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x00130654 File Offset: 0x0012E854
		public void Init(ICipherParameters parameters)
		{
			this.Reset();
			this.cipher.Init(true, parameters);
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x00130669 File Offset: 0x0012E869
		public int GetMacSize()
		{
			return this.macSize;
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x00130674 File Offset: 0x0012E874
		public void Update(byte input)
		{
			if (this.bufOff == this.buf.Length)
			{
				this.cipher.ProcessBlock(this.buf, 0, this.buf, 0);
				this.bufOff = 0;
			}
			byte[] array = this.buf;
			int num = this.bufOff;
			this.bufOff = num + 1;
			array[num] = input;
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x001306CC File Offset: 0x0012E8CC
		public void BlockUpdate(byte[] input, int inOff, int len)
		{
			if (len < 0)
			{
				throw new ArgumentException("Can't have a negative input length!");
			}
			int blockSize = this.cipher.GetBlockSize();
			int num = blockSize - this.bufOff;
			if (len > num)
			{
				Array.Copy(input, inOff, this.buf, this.bufOff, num);
				this.cipher.ProcessBlock(this.buf, 0, this.buf, 0);
				this.bufOff = 0;
				len -= num;
				inOff += num;
				while (len > blockSize)
				{
					this.cipher.ProcessBlock(input, inOff, this.buf, 0);
					len -= blockSize;
					inOff += blockSize;
				}
			}
			Array.Copy(input, inOff, this.buf, this.bufOff, len);
			this.bufOff += len;
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x00130788 File Offset: 0x0012E988
		public int DoFinal(byte[] output, int outOff)
		{
			int blockSize = this.cipher.GetBlockSize();
			if (this.padding == null)
			{
				while (this.bufOff < blockSize)
				{
					byte[] array = this.buf;
					int num = this.bufOff;
					this.bufOff = num + 1;
					array[num] = 0;
				}
			}
			else
			{
				if (this.bufOff == blockSize)
				{
					this.cipher.ProcessBlock(this.buf, 0, this.buf, 0);
					this.bufOff = 0;
				}
				this.padding.AddPadding(this.buf, this.bufOff);
			}
			this.cipher.ProcessBlock(this.buf, 0, this.buf, 0);
			Array.Copy(this.buf, 0, output, outOff, this.macSize);
			this.Reset();
			return this.macSize;
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x0013084B File Offset: 0x0012EA4B
		public void Reset()
		{
			Array.Clear(this.buf, 0, this.buf.Length);
			this.bufOff = 0;
			this.cipher.Reset();
		}

		// Token: 0x04001FB7 RID: 8119
		private byte[] buf;

		// Token: 0x04001FB8 RID: 8120
		private int bufOff;

		// Token: 0x04001FB9 RID: 8121
		private IBlockCipher cipher;

		// Token: 0x04001FBA RID: 8122
		private IBlockCipherPadding padding;

		// Token: 0x04001FBB RID: 8123
		private int macSize;
	}
}
