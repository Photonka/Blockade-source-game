using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x02000503 RID: 1283
	public sealed class CbcBlockCipher : IBlockCipher
	{
		// Token: 0x060030C7 RID: 12487 RVA: 0x0012ACC8 File Offset: 0x00128EC8
		public CbcBlockCipher(IBlockCipher cipher)
		{
			this.cipher = cipher;
			this.blockSize = cipher.GetBlockSize();
			this.IV = new byte[this.blockSize];
			this.cbcV = new byte[this.blockSize];
			this.cbcNextV = new byte[this.blockSize];
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x0012AD21 File Offset: 0x00128F21
		public IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x0012AD2C File Offset: 0x00128F2C
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			bool flag = this.encrypting;
			this.encrypting = forEncryption;
			if (parameters is ParametersWithIV)
			{
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				byte[] iv = parametersWithIV.GetIV();
				if (iv.Length != this.blockSize)
				{
					throw new ArgumentException("initialisation vector must be the same length as block size");
				}
				Array.Copy(iv, 0, this.IV, 0, iv.Length);
				parameters = parametersWithIV.Parameters;
			}
			this.Reset();
			if (parameters != null)
			{
				this.cipher.Init(this.encrypting, parameters);
				return;
			}
			if (flag != this.encrypting)
			{
				throw new ArgumentException("cannot change encrypting state without providing key.");
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x060030CA RID: 12490 RVA: 0x0012ADBA File Offset: 0x00128FBA
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/CBC";
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x060030CB RID: 12491 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x0012ADD1 File Offset: 0x00128FD1
		public int GetBlockSize()
		{
			return this.cipher.GetBlockSize();
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x0012ADDE File Offset: 0x00128FDE
		public int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (!this.encrypting)
			{
				return this.DecryptBlock(input, inOff, output, outOff);
			}
			return this.EncryptBlock(input, inOff, output, outOff);
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x0012ADFF File Offset: 0x00128FFF
		public void Reset()
		{
			Array.Copy(this.IV, 0, this.cbcV, 0, this.IV.Length);
			Array.Clear(this.cbcNextV, 0, this.cbcNextV.Length);
			this.cipher.Reset();
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x0012AE3C File Offset: 0x0012903C
		private int EncryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			if (inOff + this.blockSize > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			for (int i = 0; i < this.blockSize; i++)
			{
				byte[] array = this.cbcV;
				int num = i;
				array[num] ^= input[inOff + i];
			}
			int result = this.cipher.ProcessBlock(this.cbcV, 0, outBytes, outOff);
			Array.Copy(outBytes, outOff, this.cbcV, 0, this.cbcV.Length);
			return result;
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x0012AEB4 File Offset: 0x001290B4
		private int DecryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			if (inOff + this.blockSize > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			Array.Copy(input, inOff, this.cbcNextV, 0, this.blockSize);
			int result = this.cipher.ProcessBlock(input, inOff, outBytes, outOff);
			for (int i = 0; i < this.blockSize; i++)
			{
				int num = outOff + i;
				outBytes[num] ^= this.cbcV[i];
			}
			byte[] array = this.cbcV;
			this.cbcV = this.cbcNextV;
			this.cbcNextV = array;
			return result;
		}

		// Token: 0x04001F24 RID: 7972
		private byte[] IV;

		// Token: 0x04001F25 RID: 7973
		private byte[] cbcV;

		// Token: 0x04001F26 RID: 7974
		private byte[] cbcNextV;

		// Token: 0x04001F27 RID: 7975
		private int blockSize;

		// Token: 0x04001F28 RID: 7976
		private IBlockCipher cipher;

		// Token: 0x04001F29 RID: 7977
		private bool encrypting;
	}
}
