using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x0200050E RID: 1294
	public class OfbBlockCipher : IBlockCipher
	{
		// Token: 0x06003176 RID: 12662 RVA: 0x0012EDF8 File Offset: 0x0012CFF8
		public OfbBlockCipher(IBlockCipher cipher, int blockSize)
		{
			this.cipher = cipher;
			this.blockSize = blockSize / 8;
			this.IV = new byte[cipher.GetBlockSize()];
			this.ofbV = new byte[cipher.GetBlockSize()];
			this.ofbOutV = new byte[cipher.GetBlockSize()];
		}

		// Token: 0x06003177 RID: 12663 RVA: 0x0012EE4E File Offset: 0x0012D04E
		public IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x06003178 RID: 12664 RVA: 0x0012EE58 File Offset: 0x0012D058
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (parameters is ParametersWithIV)
			{
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				byte[] iv = parametersWithIV.GetIV();
				if (iv.Length < this.IV.Length)
				{
					Array.Copy(iv, 0, this.IV, this.IV.Length - iv.Length, iv.Length);
					for (int i = 0; i < this.IV.Length - iv.Length; i++)
					{
						this.IV[i] = 0;
					}
				}
				else
				{
					Array.Copy(iv, 0, this.IV, 0, this.IV.Length);
				}
				parameters = parametersWithIV.Parameters;
			}
			this.Reset();
			if (parameters != null)
			{
				this.cipher.Init(true, parameters);
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06003179 RID: 12665 RVA: 0x0012EEFA File Offset: 0x0012D0FA
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/OFB" + this.blockSize * 8;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x0600317A RID: 12666 RVA: 0x0006CF70 File Offset: 0x0006B170
		public bool IsPartialBlockOkay
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x0012EF1E File Offset: 0x0012D11E
		public int GetBlockSize()
		{
			return this.blockSize;
		}

		// Token: 0x0600317C RID: 12668 RVA: 0x0012EF28 File Offset: 0x0012D128
		public int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (inOff + this.blockSize > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			if (outOff + this.blockSize > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			this.cipher.ProcessBlock(this.ofbV, 0, this.ofbOutV, 0);
			for (int i = 0; i < this.blockSize; i++)
			{
				output[outOff + i] = (this.ofbOutV[i] ^ input[inOff + i]);
			}
			Array.Copy(this.ofbV, this.blockSize, this.ofbV, 0, this.ofbV.Length - this.blockSize);
			Array.Copy(this.ofbOutV, 0, this.ofbV, this.ofbV.Length - this.blockSize, this.blockSize);
			return this.blockSize;
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x0012EFFA File Offset: 0x0012D1FA
		public void Reset()
		{
			Array.Copy(this.IV, 0, this.ofbV, 0, this.IV.Length);
			this.cipher.Reset();
		}

		// Token: 0x04001F9B RID: 8091
		private byte[] IV;

		// Token: 0x04001F9C RID: 8092
		private byte[] ofbV;

		// Token: 0x04001F9D RID: 8093
		private byte[] ofbOutV;

		// Token: 0x04001F9E RID: 8094
		private readonly int blockSize;

		// Token: 0x04001F9F RID: 8095
		private readonly IBlockCipher cipher;
	}
}
