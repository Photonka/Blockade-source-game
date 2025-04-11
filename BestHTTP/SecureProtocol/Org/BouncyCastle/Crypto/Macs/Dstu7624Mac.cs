using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x0200051E RID: 1310
	public class Dstu7624Mac : IMac
	{
		// Token: 0x060031FB RID: 12795 RVA: 0x001313F4 File Offset: 0x0012F5F4
		public Dstu7624Mac(int blockSizeBits, int q)
		{
			this.engine = new Dstu7624Engine(blockSizeBits);
			this.blockSize = blockSizeBits / 8;
			this.macSize = q / 8;
			this.c = new byte[this.blockSize];
			this.cTemp = new byte[this.blockSize];
			this.kDelta = new byte[this.blockSize];
			this.buf = new byte[this.blockSize];
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x0013146C File Offset: 0x0012F66C
		public void Init(ICipherParameters parameters)
		{
			if (parameters is KeyParameter)
			{
				this.engine.Init(true, (KeyParameter)parameters);
				this.engine.ProcessBlock(this.kDelta, 0, this.kDelta, 0);
				return;
			}
			throw new ArgumentException("invalid parameter passed to Dstu7624Mac init - " + Platform.GetTypeName(parameters));
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x060031FD RID: 12797 RVA: 0x001314C3 File Offset: 0x0012F6C3
		public string AlgorithmName
		{
			get
			{
				return "Dstu7624Mac";
			}
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x001314CA File Offset: 0x0012F6CA
		public int GetMacSize()
		{
			return this.macSize;
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x001314D4 File Offset: 0x0012F6D4
		public void Update(byte input)
		{
			if (this.bufOff == this.buf.Length)
			{
				this.processBlock(this.buf, 0);
				this.bufOff = 0;
			}
			byte[] array = this.buf;
			int num = this.bufOff;
			this.bufOff = num + 1;
			array[num] = input;
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x00131520 File Offset: 0x0012F720
		public void BlockUpdate(byte[] input, int inOff, int len)
		{
			if (len < 0)
			{
				throw new ArgumentException("Can't have a negative input length!");
			}
			int num = this.engine.GetBlockSize();
			int num2 = num - this.bufOff;
			if (len > num2)
			{
				Array.Copy(input, inOff, this.buf, this.bufOff, num2);
				this.processBlock(this.buf, 0);
				this.bufOff = 0;
				len -= num2;
				inOff += num2;
				while (len > num)
				{
					this.processBlock(input, inOff);
					len -= num;
					inOff += num;
				}
			}
			Array.Copy(input, inOff, this.buf, this.bufOff, len);
			this.bufOff += len;
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x001315C1 File Offset: 0x0012F7C1
		private void processBlock(byte[] input, int inOff)
		{
			this.Xor(this.c, 0, input, inOff, this.cTemp);
			this.engine.ProcessBlock(this.cTemp, 0, this.c, 0);
		}

		// Token: 0x06003202 RID: 12802 RVA: 0x001315F4 File Offset: 0x0012F7F4
		private void Xor(byte[] c, int cOff, byte[] input, int inOff, byte[] xorResult)
		{
			for (int i = 0; i < this.blockSize; i++)
			{
				xorResult[i] = (c[i + cOff] ^ input[i + inOff]);
			}
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x00131624 File Offset: 0x0012F824
		public int DoFinal(byte[] output, int outOff)
		{
			if (this.bufOff % this.buf.Length != 0)
			{
				throw new DataLengthException("Input must be a multiple of blocksize");
			}
			this.Xor(this.c, 0, this.buf, 0, this.cTemp);
			this.Xor(this.cTemp, 0, this.kDelta, 0, this.c);
			this.engine.ProcessBlock(this.c, 0, this.c, 0);
			if (this.macSize + outOff > output.Length)
			{
				throw new DataLengthException("Output buffer too short");
			}
			Array.Copy(this.c, 0, output, outOff, this.macSize);
			return this.macSize;
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x001316D0 File Offset: 0x0012F8D0
		public void Reset()
		{
			Arrays.Fill(this.c, 0);
			Arrays.Fill(this.cTemp, 0);
			Arrays.Fill(this.kDelta, 0);
			Arrays.Fill(this.buf, 0);
			this.engine.Reset();
			this.engine.ProcessBlock(this.kDelta, 0, this.kDelta, 0);
			this.bufOff = 0;
		}

		// Token: 0x04001FD7 RID: 8151
		private int macSize;

		// Token: 0x04001FD8 RID: 8152
		private Dstu7624Engine engine;

		// Token: 0x04001FD9 RID: 8153
		private int blockSize;

		// Token: 0x04001FDA RID: 8154
		private byte[] c;

		// Token: 0x04001FDB RID: 8155
		private byte[] cTemp;

		// Token: 0x04001FDC RID: 8156
		private byte[] kDelta;

		// Token: 0x04001FDD RID: 8157
		private byte[] buf;

		// Token: 0x04001FDE RID: 8158
		private int bufOff;
	}
}
