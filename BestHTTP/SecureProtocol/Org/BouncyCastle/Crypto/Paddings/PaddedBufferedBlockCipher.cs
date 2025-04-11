using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Paddings
{
	// Token: 0x020004F6 RID: 1270
	public class PaddedBufferedBlockCipher : BufferedBlockCipher
	{
		// Token: 0x0600308B RID: 12427 RVA: 0x00129E80 File Offset: 0x00128080
		public PaddedBufferedBlockCipher(IBlockCipher cipher, IBlockCipherPadding padding)
		{
			this.cipher = cipher;
			this.padding = padding;
			this.buf = new byte[cipher.GetBlockSize()];
			this.bufOff = 0;
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x00129EAE File Offset: 0x001280AE
		public PaddedBufferedBlockCipher(IBlockCipher cipher) : this(cipher, new Pkcs7Padding())
		{
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x00129EBC File Offset: 0x001280BC
		public override void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.forEncryption = forEncryption;
			SecureRandom random = null;
			if (parameters is ParametersWithRandom)
			{
				ParametersWithRandom parametersWithRandom = (ParametersWithRandom)parameters;
				random = parametersWithRandom.Random;
				parameters = parametersWithRandom.Parameters;
			}
			this.Reset();
			this.padding.Init(random);
			this.cipher.Init(forEncryption, parameters);
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x00129F10 File Offset: 0x00128110
		public override int GetOutputSize(int length)
		{
			int num = length + this.bufOff;
			int num2 = num % this.buf.Length;
			if (num2 != 0)
			{
				return num - num2 + this.buf.Length;
			}
			if (this.forEncryption)
			{
				return num + this.buf.Length;
			}
			return num;
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x00129F58 File Offset: 0x00128158
		public override int GetUpdateOutputSize(int length)
		{
			int num = length + this.bufOff;
			int num2 = num % this.buf.Length;
			if (num2 == 0)
			{
				return num - this.buf.Length;
			}
			return num - num2;
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x00129F8C File Offset: 0x0012818C
		public override int ProcessByte(byte input, byte[] output, int outOff)
		{
			int result = 0;
			if (this.bufOff == this.buf.Length)
			{
				result = this.cipher.ProcessBlock(this.buf, 0, output, outOff);
				this.bufOff = 0;
			}
			byte[] buf = this.buf;
			int bufOff = this.bufOff;
			this.bufOff = bufOff + 1;
			buf[bufOff] = input;
			return result;
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x00129FE4 File Offset: 0x001281E4
		public override int ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			if (length < 0)
			{
				throw new ArgumentException("Can't have a negative input length!");
			}
			int blockSize = this.GetBlockSize();
			int updateOutputSize = this.GetUpdateOutputSize(length);
			if (updateOutputSize > 0)
			{
				Check.OutputLength(output, outOff, updateOutputSize, "output buffer too short");
			}
			int num = 0;
			int num2 = this.buf.Length - this.bufOff;
			if (length > num2)
			{
				Array.Copy(input, inOff, this.buf, this.bufOff, num2);
				num += this.cipher.ProcessBlock(this.buf, 0, output, outOff);
				this.bufOff = 0;
				length -= num2;
				inOff += num2;
				while (length > this.buf.Length)
				{
					num += this.cipher.ProcessBlock(input, inOff, output, outOff + num);
					length -= blockSize;
					inOff += blockSize;
				}
			}
			Array.Copy(input, inOff, this.buf, this.bufOff, length);
			this.bufOff += length;
			return num;
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x0012A0C8 File Offset: 0x001282C8
		public override int DoFinal(byte[] output, int outOff)
		{
			int blockSize = this.cipher.GetBlockSize();
			int num = 0;
			if (this.forEncryption)
			{
				if (this.bufOff == blockSize)
				{
					if (outOff + 2 * blockSize > output.Length)
					{
						this.Reset();
						throw new OutputLengthException("output buffer too short");
					}
					num = this.cipher.ProcessBlock(this.buf, 0, output, outOff);
					this.bufOff = 0;
				}
				this.padding.AddPadding(this.buf, this.bufOff);
				num += this.cipher.ProcessBlock(this.buf, 0, output, outOff + num);
				this.Reset();
			}
			else
			{
				if (this.bufOff != blockSize)
				{
					this.Reset();
					throw new DataLengthException("last block incomplete in decryption");
				}
				num = this.cipher.ProcessBlock(this.buf, 0, this.buf, 0);
				this.bufOff = 0;
				try
				{
					num -= this.padding.PadCount(this.buf);
					Array.Copy(this.buf, 0, output, outOff, num);
				}
				finally
				{
					this.Reset();
				}
			}
			return num;
		}

		// Token: 0x04001F13 RID: 7955
		private readonly IBlockCipherPadding padding;
	}
}
