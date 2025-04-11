using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Paddings;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x0200051C RID: 1308
	public class CMac : IMac
	{
		// Token: 0x060031E6 RID: 12774 RVA: 0x00130D3E File Offset: 0x0012EF3E
		public CMac(IBlockCipher cipher) : this(cipher, cipher.GetBlockSize() * 8)
		{
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x00130D50 File Offset: 0x0012EF50
		public CMac(IBlockCipher cipher, int macSizeInBits)
		{
			if (macSizeInBits % 8 != 0)
			{
				throw new ArgumentException("MAC size must be multiple of 8");
			}
			if (macSizeInBits > cipher.GetBlockSize() * 8)
			{
				throw new ArgumentException("MAC size must be less or equal to " + cipher.GetBlockSize() * 8);
			}
			if (cipher.GetBlockSize() != 8 && cipher.GetBlockSize() != 16)
			{
				throw new ArgumentException("Block size must be either 64 or 128 bits");
			}
			this.cipher = new CbcBlockCipher(cipher);
			this.macSize = macSizeInBits / 8;
			this.mac = new byte[cipher.GetBlockSize()];
			this.buf = new byte[cipher.GetBlockSize()];
			this.ZEROES = new byte[cipher.GetBlockSize()];
			this.bufOff = 0;
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x060031E8 RID: 12776 RVA: 0x00130E08 File Offset: 0x0012F008
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName;
			}
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x00130E18 File Offset: 0x0012F018
		private static int ShiftLeft(byte[] block, byte[] output)
		{
			int num = block.Length;
			uint num2 = 0U;
			while (--num >= 0)
			{
				uint num3 = (uint)block[num];
				output[num] = (byte)(num3 << 1 | num2);
				num2 = (num3 >> 7 & 1U);
			}
			return (int)num2;
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x00130E4C File Offset: 0x0012F04C
		private static byte[] DoubleLu(byte[] input)
		{
			byte[] array = new byte[input.Length];
			int num = CMac.ShiftLeft(input, array);
			int num2 = (input.Length == 16) ? 135 : 27;
			byte[] array2 = array;
			int num3 = input.Length - 1;
			array2[num3] ^= (byte)(num2 >> (1 - num << 3));
			return array;
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x00130E98 File Offset: 0x0012F098
		public void Init(ICipherParameters parameters)
		{
			if (parameters is KeyParameter)
			{
				this.cipher.Init(true, parameters);
				this.L = new byte[this.ZEROES.Length];
				this.cipher.ProcessBlock(this.ZEROES, 0, this.L, 0);
				this.Lu = CMac.DoubleLu(this.L);
				this.Lu2 = CMac.DoubleLu(this.Lu);
			}
			else if (parameters != null)
			{
				throw new ArgumentException("CMac mode only permits key to be set.", "parameters");
			}
			this.Reset();
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x00130F24 File Offset: 0x0012F124
		public int GetMacSize()
		{
			return this.macSize;
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x00130F2C File Offset: 0x0012F12C
		public void Update(byte input)
		{
			if (this.bufOff == this.buf.Length)
			{
				this.cipher.ProcessBlock(this.buf, 0, this.mac, 0);
				this.bufOff = 0;
			}
			byte[] array = this.buf;
			int num = this.bufOff;
			this.bufOff = num + 1;
			array[num] = input;
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x00130F84 File Offset: 0x0012F184
		public void BlockUpdate(byte[] inBytes, int inOff, int len)
		{
			if (len < 0)
			{
				throw new ArgumentException("Can't have a negative input length!");
			}
			int blockSize = this.cipher.GetBlockSize();
			int num = blockSize - this.bufOff;
			if (len > num)
			{
				Array.Copy(inBytes, inOff, this.buf, this.bufOff, num);
				this.cipher.ProcessBlock(this.buf, 0, this.mac, 0);
				this.bufOff = 0;
				len -= num;
				inOff += num;
				while (len > blockSize)
				{
					this.cipher.ProcessBlock(inBytes, inOff, this.mac, 0);
					len -= blockSize;
					inOff += blockSize;
				}
			}
			Array.Copy(inBytes, inOff, this.buf, this.bufOff, len);
			this.bufOff += len;
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x00131040 File Offset: 0x0012F240
		public int DoFinal(byte[] outBytes, int outOff)
		{
			int blockSize = this.cipher.GetBlockSize();
			byte[] array;
			if (this.bufOff == blockSize)
			{
				array = this.Lu;
			}
			else
			{
				new ISO7816d4Padding().AddPadding(this.buf, this.bufOff);
				array = this.Lu2;
			}
			for (int i = 0; i < this.mac.Length; i++)
			{
				byte[] array2 = this.buf;
				int num = i;
				array2[num] ^= array[i];
			}
			this.cipher.ProcessBlock(this.buf, 0, this.mac, 0);
			Array.Copy(this.mac, 0, outBytes, outOff, this.macSize);
			this.Reset();
			return this.macSize;
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x001310EA File Offset: 0x0012F2EA
		public void Reset()
		{
			Array.Clear(this.buf, 0, this.buf.Length);
			this.bufOff = 0;
			this.cipher.Reset();
		}

		// Token: 0x04001FC7 RID: 8135
		private const byte CONSTANT_128 = 135;

		// Token: 0x04001FC8 RID: 8136
		private const byte CONSTANT_64 = 27;

		// Token: 0x04001FC9 RID: 8137
		private byte[] ZEROES;

		// Token: 0x04001FCA RID: 8138
		private byte[] mac;

		// Token: 0x04001FCB RID: 8139
		private byte[] buf;

		// Token: 0x04001FCC RID: 8140
		private int bufOff;

		// Token: 0x04001FCD RID: 8141
		private IBlockCipher cipher;

		// Token: 0x04001FCE RID: 8142
		private int macSize;

		// Token: 0x04001FCF RID: 8143
		private byte[] L;

		// Token: 0x04001FD0 RID: 8144
		private byte[] Lu;

		// Token: 0x04001FD1 RID: 8145
		private byte[] Lu2;
	}
}
