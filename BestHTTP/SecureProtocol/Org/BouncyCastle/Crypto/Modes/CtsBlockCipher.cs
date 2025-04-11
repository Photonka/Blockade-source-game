using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x02000506 RID: 1286
	public class CtsBlockCipher : BufferedBlockCipher
	{
		// Token: 0x060030EF RID: 12527 RVA: 0x0012B984 File Offset: 0x00129B84
		public CtsBlockCipher(IBlockCipher cipher)
		{
			if (cipher is OfbBlockCipher || cipher is CfbBlockCipher)
			{
				throw new ArgumentException("CtsBlockCipher can only accept ECB, or CBC ciphers");
			}
			this.cipher = cipher;
			this.blockSize = cipher.GetBlockSize();
			this.buf = new byte[this.blockSize * 2];
			this.bufOff = 0;
		}

		// Token: 0x060030F0 RID: 12528 RVA: 0x0012B9E0 File Offset: 0x00129BE0
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

		// Token: 0x060030F1 RID: 12529 RVA: 0x0010DA3E File Offset: 0x0010BC3E
		public override int GetOutputSize(int length)
		{
			return length + this.bufOff;
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x0012BA14 File Offset: 0x00129C14
		public override int ProcessByte(byte input, byte[] output, int outOff)
		{
			int result = 0;
			if (this.bufOff == this.buf.Length)
			{
				result = this.cipher.ProcessBlock(this.buf, 0, output, outOff);
				Array.Copy(this.buf, this.blockSize, this.buf, 0, this.blockSize);
				this.bufOff = this.blockSize;
			}
			byte[] buf = this.buf;
			int bufOff = this.bufOff;
			this.bufOff = bufOff + 1;
			buf[bufOff] = input;
			return result;
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x0012BA8C File Offset: 0x00129C8C
		public override int ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			if (length < 0)
			{
				throw new ArgumentException("Can't have a negative input outLength!");
			}
			int num = this.GetBlockSize();
			int updateOutputSize = this.GetUpdateOutputSize(length);
			if (updateOutputSize > 0 && outOff + updateOutputSize > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			int num2 = 0;
			int num3 = this.buf.Length - this.bufOff;
			if (length > num3)
			{
				Array.Copy(input, inOff, this.buf, this.bufOff, num3);
				num2 += this.cipher.ProcessBlock(this.buf, 0, output, outOff);
				Array.Copy(this.buf, num, this.buf, 0, num);
				this.bufOff = num;
				length -= num3;
				inOff += num3;
				while (length > num)
				{
					Array.Copy(input, inOff, this.buf, this.bufOff, num);
					num2 += this.cipher.ProcessBlock(this.buf, 0, output, outOff + num2);
					Array.Copy(this.buf, num, this.buf, 0, num);
					length -= num;
					inOff += num;
				}
			}
			Array.Copy(input, inOff, this.buf, this.bufOff, length);
			this.bufOff += length;
			return num2;
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x0012BBB4 File Offset: 0x00129DB4
		public override int DoFinal(byte[] output, int outOff)
		{
			if (this.bufOff + outOff > output.Length)
			{
				throw new DataLengthException("output buffer too small in doFinal");
			}
			int num = this.cipher.GetBlockSize();
			int length = this.bufOff - num;
			byte[] array = new byte[num];
			if (this.forEncryption)
			{
				this.cipher.ProcessBlock(this.buf, 0, array, 0);
				if (this.bufOff < num)
				{
					throw new DataLengthException("need at least one block of input for CTS");
				}
				for (int num2 = this.bufOff; num2 != this.buf.Length; num2++)
				{
					this.buf[num2] = array[num2 - num];
				}
				for (int num3 = num; num3 != this.bufOff; num3++)
				{
					byte[] buf = this.buf;
					int num4 = num3;
					buf[num4] ^= array[num3 - num];
				}
				((this.cipher is CbcBlockCipher) ? ((CbcBlockCipher)this.cipher).GetUnderlyingCipher() : this.cipher).ProcessBlock(this.buf, num, output, outOff);
				Array.Copy(array, 0, output, outOff + num, length);
			}
			else
			{
				byte[] array2 = new byte[num];
				((this.cipher is CbcBlockCipher) ? ((CbcBlockCipher)this.cipher).GetUnderlyingCipher() : this.cipher).ProcessBlock(this.buf, 0, array, 0);
				for (int num5 = num; num5 != this.bufOff; num5++)
				{
					array2[num5 - num] = (array[num5 - num] ^ this.buf[num5]);
				}
				Array.Copy(this.buf, num, array, 0, length);
				this.cipher.ProcessBlock(array, 0, output, outOff);
				Array.Copy(array2, 0, output, outOff + num, length);
			}
			int bufOff = this.bufOff;
			this.Reset();
			return bufOff;
		}

		// Token: 0x04001F3A RID: 7994
		private readonly int blockSize;
	}
}
