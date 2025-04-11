using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x02000505 RID: 1285
	public class CfbBlockCipher : IBlockCipher
	{
		// Token: 0x060030E5 RID: 12517 RVA: 0x0012B69C File Offset: 0x0012989C
		public CfbBlockCipher(IBlockCipher cipher, int bitBlockSize)
		{
			this.cipher = cipher;
			this.blockSize = bitBlockSize / 8;
			this.IV = new byte[cipher.GetBlockSize()];
			this.cfbV = new byte[cipher.GetBlockSize()];
			this.cfbOutV = new byte[cipher.GetBlockSize()];
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x0012B6F2 File Offset: 0x001298F2
		public IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x0012B6FC File Offset: 0x001298FC
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.encrypting = forEncryption;
			if (parameters is ParametersWithIV)
			{
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				byte[] iv = parametersWithIV.GetIV();
				int num = this.IV.Length - iv.Length;
				Array.Copy(iv, 0, this.IV, num, iv.Length);
				Array.Clear(this.IV, 0, num);
				parameters = parametersWithIV.Parameters;
			}
			this.Reset();
			if (parameters != null)
			{
				this.cipher.Init(true, parameters);
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060030E8 RID: 12520 RVA: 0x0012B76D File Offset: 0x0012996D
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/CFB" + this.blockSize * 8;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x060030E9 RID: 12521 RVA: 0x0006CF70 File Offset: 0x0006B170
		public bool IsPartialBlockOkay
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x0012B791 File Offset: 0x00129991
		public int GetBlockSize()
		{
			return this.blockSize;
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x0012B799 File Offset: 0x00129999
		public int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (!this.encrypting)
			{
				return this.DecryptBlock(input, inOff, output, outOff);
			}
			return this.EncryptBlock(input, inOff, output, outOff);
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x0012B7BC File Offset: 0x001299BC
		public int EncryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
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

		// Token: 0x060030ED RID: 12525 RVA: 0x0012B88C File Offset: 0x00129A8C
		public int DecryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
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
			Array.Copy(this.cfbV, this.blockSize, this.cfbV, 0, this.cfbV.Length - this.blockSize);
			Array.Copy(input, inOff, this.cfbV, this.cfbV.Length - this.blockSize, this.blockSize);
			for (int i = 0; i < this.blockSize; i++)
			{
				outBytes[outOff + i] = (this.cfbOutV[i] ^ input[inOff + i]);
			}
			return this.blockSize;
		}

		// Token: 0x060030EE RID: 12526 RVA: 0x0012B959 File Offset: 0x00129B59
		public void Reset()
		{
			Array.Copy(this.IV, 0, this.cfbV, 0, this.IV.Length);
			this.cipher.Reset();
		}

		// Token: 0x04001F34 RID: 7988
		private byte[] IV;

		// Token: 0x04001F35 RID: 7989
		private byte[] cfbV;

		// Token: 0x04001F36 RID: 7990
		private byte[] cfbOutV;

		// Token: 0x04001F37 RID: 7991
		private bool encrypting;

		// Token: 0x04001F38 RID: 7992
		private readonly int blockSize;

		// Token: 0x04001F39 RID: 7993
		private readonly IBlockCipher cipher;
	}
}
