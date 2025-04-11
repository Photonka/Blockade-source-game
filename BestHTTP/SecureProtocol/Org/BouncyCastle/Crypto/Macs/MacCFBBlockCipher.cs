using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x0200051A RID: 1306
	internal class MacCFBBlockCipher : IBlockCipher
	{
		// Token: 0x060031D3 RID: 12755 RVA: 0x00130874 File Offset: 0x0012EA74
		public MacCFBBlockCipher(IBlockCipher cipher, int bitBlockSize)
		{
			this.cipher = cipher;
			this.blockSize = bitBlockSize / 8;
			this.IV = new byte[cipher.GetBlockSize()];
			this.cfbV = new byte[cipher.GetBlockSize()];
			this.cfbOutV = new byte[cipher.GetBlockSize()];
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x001308CC File Offset: 0x0012EACC
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (parameters is ParametersWithIV)
			{
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				byte[] iv = parametersWithIV.GetIV();
				if (iv.Length < this.IV.Length)
				{
					Array.Copy(iv, 0, this.IV, this.IV.Length - iv.Length, iv.Length);
				}
				else
				{
					Array.Copy(iv, 0, this.IV, 0, this.IV.Length);
				}
				parameters = parametersWithIV.Parameters;
			}
			this.Reset();
			this.cipher.Init(true, parameters);
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060031D5 RID: 12757 RVA: 0x00130949 File Offset: 0x0012EB49
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/CFB" + this.blockSize * 8;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x060031D6 RID: 12758 RVA: 0x0006CF70 File Offset: 0x0006B170
		public bool IsPartialBlockOkay
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x0013096D File Offset: 0x0012EB6D
		public int GetBlockSize()
		{
			return this.blockSize;
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x00130978 File Offset: 0x0012EB78
		public int ProcessBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			if (inOff + this.blockSize > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			if (outOff + this.blockSize > outBytes.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			this.cipher.ProcessBlock(this.cfbV, 0, this.cfbOutV, 0);
			for (int i = 0; i < this.blockSize; i++)
			{
				outBytes[outOff + i] = (this.cfbOutV[i] ^ input[inOff + i]);
			}
			Array.Copy(this.cfbV, this.blockSize, this.cfbV, 0, this.cfbV.Length - this.blockSize);
			Array.Copy(outBytes, outOff, this.cfbV, this.cfbV.Length - this.blockSize, this.blockSize);
			return this.blockSize;
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x00130A46 File Offset: 0x0012EC46
		public void Reset()
		{
			this.IV.CopyTo(this.cfbV, 0);
			this.cipher.Reset();
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x00130A65 File Offset: 0x0012EC65
		public void GetMacBlock(byte[] mac)
		{
			this.cipher.ProcessBlock(this.cfbV, 0, mac, 0);
		}

		// Token: 0x04001FBC RID: 8124
		private byte[] IV;

		// Token: 0x04001FBD RID: 8125
		private byte[] cfbV;

		// Token: 0x04001FBE RID: 8126
		private byte[] cfbOutV;

		// Token: 0x04001FBF RID: 8127
		private readonly int blockSize;

		// Token: 0x04001FC0 RID: 8128
		private readonly IBlockCipher cipher;
	}
}
