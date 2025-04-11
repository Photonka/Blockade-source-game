using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x0200050C RID: 1292
	public class KCtrBlockCipher : IStreamCipher, IBlockCipher
	{
		// Token: 0x0600314F RID: 12623 RVA: 0x0012DF80 File Offset: 0x0012C180
		public KCtrBlockCipher(IBlockCipher cipher)
		{
			this.cipher = cipher;
			this.IV = new byte[cipher.GetBlockSize()];
			this.blockSize = cipher.GetBlockSize();
			this.ofbV = new byte[cipher.GetBlockSize()];
			this.ofbOutV = new byte[cipher.GetBlockSize()];
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x0012DFD9 File Offset: 0x0012C1D9
		public IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x06003151 RID: 12625 RVA: 0x0012DFE4 File Offset: 0x0012C1E4
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.initialised = true;
			if (parameters is ParametersWithIV)
			{
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				byte[] iv = parametersWithIV.GetIV();
				int destinationIndex = this.IV.Length - iv.Length;
				Array.Clear(this.IV, 0, this.IV.Length);
				Array.Copy(iv, 0, this.IV, destinationIndex, iv.Length);
				parameters = parametersWithIV.Parameters;
				if (parameters != null)
				{
					this.cipher.Init(true, parameters);
				}
				this.Reset();
				return;
			}
			throw new ArgumentException("Invalid parameter passed");
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06003152 RID: 12626 RVA: 0x0012E069 File Offset: 0x0012C269
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/KCTR";
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06003153 RID: 12627 RVA: 0x0006CF70 File Offset: 0x0006B170
		public bool IsPartialBlockOkay
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x0012E080 File Offset: 0x0012C280
		public int GetBlockSize()
		{
			return this.cipher.GetBlockSize();
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x0012E08D File Offset: 0x0012C28D
		public byte ReturnByte(byte input)
		{
			return this.CalculateByte(input);
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x0012E098 File Offset: 0x0012C298
		public void ProcessBytes(byte[] input, int inOff, int len, byte[] output, int outOff)
		{
			if (outOff + len > output.Length)
			{
				throw new DataLengthException("Output buffer too short");
			}
			if (inOff + len > input.Length)
			{
				throw new DataLengthException("Input buffer too small");
			}
			int i = inOff;
			int num = inOff + len;
			int num2 = outOff;
			while (i < num)
			{
				output[num2++] = this.CalculateByte(input[i++]);
			}
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x0012E0F4 File Offset: 0x0012C2F4
		protected byte CalculateByte(byte b)
		{
			int num;
			if (this.byteCount == 0)
			{
				this.incrementCounterAt(0);
				this.checkCounter();
				this.cipher.ProcessBlock(this.ofbV, 0, this.ofbOutV, 0);
				byte[] array = this.ofbOutV;
				num = this.byteCount;
				this.byteCount = num + 1;
				return array[num] ^ b;
			}
			byte[] array2 = this.ofbOutV;
			num = this.byteCount;
			this.byteCount = num + 1;
			byte result = array2[num] ^ b;
			if (this.byteCount == this.ofbV.Length)
			{
				this.byteCount = 0;
			}
			return result;
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x0012E180 File Offset: 0x0012C380
		public int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (input.Length - inOff < this.GetBlockSize())
			{
				throw new DataLengthException("Input buffer too short");
			}
			if (output.Length - outOff < this.GetBlockSize())
			{
				throw new DataLengthException("Output buffer too short");
			}
			this.ProcessBytes(input, inOff, this.GetBlockSize(), output, outOff);
			return this.GetBlockSize();
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x0012E1D5 File Offset: 0x0012C3D5
		public void Reset()
		{
			if (this.initialised)
			{
				this.cipher.ProcessBlock(this.IV, 0, this.ofbV, 0);
			}
			this.cipher.Reset();
			this.byteCount = 0;
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x0012E20C File Offset: 0x0012C40C
		private void incrementCounterAt(int pos)
		{
			int i = pos;
			while (i < this.ofbV.Length)
			{
				byte[] array = this.ofbV;
				int num = i++;
				byte b = array[num] + 1;
				array[num] = b;
				if (b != 0)
				{
					break;
				}
			}
		}

		// Token: 0x0600315B RID: 12635 RVA: 0x00002B75 File Offset: 0x00000D75
		private void checkCounter()
		{
		}

		// Token: 0x04001F7D RID: 8061
		private byte[] IV;

		// Token: 0x04001F7E RID: 8062
		private byte[] ofbV;

		// Token: 0x04001F7F RID: 8063
		private byte[] ofbOutV;

		// Token: 0x04001F80 RID: 8064
		private bool initialised;

		// Token: 0x04001F81 RID: 8065
		private int byteCount;

		// Token: 0x04001F82 RID: 8066
		private readonly int blockSize;

		// Token: 0x04001F83 RID: 8067
		private readonly IBlockCipher cipher;
	}
}
