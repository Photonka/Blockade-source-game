﻿using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x0200050F RID: 1295
	public class OpenPgpCfbBlockCipher : IBlockCipher
	{
		// Token: 0x0600317E RID: 12670 RVA: 0x0012F024 File Offset: 0x0012D224
		public OpenPgpCfbBlockCipher(IBlockCipher cipher)
		{
			this.cipher = cipher;
			this.blockSize = cipher.GetBlockSize();
			this.IV = new byte[this.blockSize];
			this.FR = new byte[this.blockSize];
			this.FRE = new byte[this.blockSize];
		}

		// Token: 0x0600317F RID: 12671 RVA: 0x0012F07D File Offset: 0x0012D27D
		public IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06003180 RID: 12672 RVA: 0x0012F085 File Offset: 0x0012D285
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/OpenPGPCFB";
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06003181 RID: 12673 RVA: 0x0006CF70 File Offset: 0x0006B170
		public bool IsPartialBlockOkay
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x0012F09C File Offset: 0x0012D29C
		public int GetBlockSize()
		{
			return this.cipher.GetBlockSize();
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x0012F0A9 File Offset: 0x0012D2A9
		public int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (!this.forEncryption)
			{
				return this.DecryptBlock(input, inOff, output, outOff);
			}
			return this.EncryptBlock(input, inOff, output, outOff);
		}

		// Token: 0x06003184 RID: 12676 RVA: 0x0012F0CA File Offset: 0x0012D2CA
		public void Reset()
		{
			this.count = 0;
			Array.Copy(this.IV, 0, this.FR, 0, this.FR.Length);
			this.cipher.Reset();
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x0012F0FC File Offset: 0x0012D2FC
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.forEncryption = forEncryption;
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
			this.cipher.Init(true, parameters);
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x0012F1A2 File Offset: 0x0012D3A2
		private byte EncryptByte(byte data, int blockOff)
		{
			return this.FRE[blockOff] ^ data;
		}

		// Token: 0x06003187 RID: 12679 RVA: 0x0012F1B0 File Offset: 0x0012D3B0
		private int EncryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			if (inOff + this.blockSize > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			if (outOff + this.blockSize > outBytes.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			if (this.count > this.blockSize)
			{
				this.FR[this.blockSize - 2] = (outBytes[outOff] = this.EncryptByte(input[inOff], this.blockSize - 2));
				this.FR[this.blockSize - 1] = (outBytes[outOff + 1] = this.EncryptByte(input[inOff + 1], this.blockSize - 1));
				this.cipher.ProcessBlock(this.FR, 0, this.FRE, 0);
				for (int i = 2; i < this.blockSize; i++)
				{
					this.FR[i - 2] = (outBytes[outOff + i] = this.EncryptByte(input[inOff + i], i - 2));
				}
			}
			else if (this.count == 0)
			{
				this.cipher.ProcessBlock(this.FR, 0, this.FRE, 0);
				for (int j = 0; j < this.blockSize; j++)
				{
					this.FR[j] = (outBytes[outOff + j] = this.EncryptByte(input[inOff + j], j));
				}
				this.count += this.blockSize;
			}
			else if (this.count == this.blockSize)
			{
				this.cipher.ProcessBlock(this.FR, 0, this.FRE, 0);
				outBytes[outOff] = this.EncryptByte(input[inOff], 0);
				outBytes[outOff + 1] = this.EncryptByte(input[inOff + 1], 1);
				Array.Copy(this.FR, 2, this.FR, 0, this.blockSize - 2);
				Array.Copy(outBytes, outOff, this.FR, this.blockSize - 2, 2);
				this.cipher.ProcessBlock(this.FR, 0, this.FRE, 0);
				for (int k = 2; k < this.blockSize; k++)
				{
					this.FR[k - 2] = (outBytes[outOff + k] = this.EncryptByte(input[inOff + k], k - 2));
				}
				this.count += this.blockSize;
			}
			return this.blockSize;
		}

		// Token: 0x06003188 RID: 12680 RVA: 0x0012F3EC File Offset: 0x0012D5EC
		private int DecryptBlock(byte[] input, int inOff, byte[] outBytes, int outOff)
		{
			if (inOff + this.blockSize > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			if (outOff + this.blockSize > outBytes.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			if (this.count > this.blockSize)
			{
				byte b = input[inOff];
				this.FR[this.blockSize - 2] = b;
				outBytes[outOff] = this.EncryptByte(b, this.blockSize - 2);
				b = input[inOff + 1];
				this.FR[this.blockSize - 1] = b;
				outBytes[outOff + 1] = this.EncryptByte(b, this.blockSize - 1);
				this.cipher.ProcessBlock(this.FR, 0, this.FRE, 0);
				for (int i = 2; i < this.blockSize; i++)
				{
					b = input[inOff + i];
					this.FR[i - 2] = b;
					outBytes[outOff + i] = this.EncryptByte(b, i - 2);
				}
			}
			else if (this.count == 0)
			{
				this.cipher.ProcessBlock(this.FR, 0, this.FRE, 0);
				for (int j = 0; j < this.blockSize; j++)
				{
					this.FR[j] = input[inOff + j];
					outBytes[j] = this.EncryptByte(input[inOff + j], j);
				}
				this.count += this.blockSize;
			}
			else if (this.count == this.blockSize)
			{
				this.cipher.ProcessBlock(this.FR, 0, this.FRE, 0);
				byte b2 = input[inOff];
				byte b3 = input[inOff + 1];
				outBytes[outOff] = this.EncryptByte(b2, 0);
				outBytes[outOff + 1] = this.EncryptByte(b3, 1);
				Array.Copy(this.FR, 2, this.FR, 0, this.blockSize - 2);
				this.FR[this.blockSize - 2] = b2;
				this.FR[this.blockSize - 1] = b3;
				this.cipher.ProcessBlock(this.FR, 0, this.FRE, 0);
				for (int k = 2; k < this.blockSize; k++)
				{
					byte b4 = input[inOff + k];
					this.FR[k - 2] = b4;
					outBytes[outOff + k] = this.EncryptByte(b4, k - 2);
				}
				this.count += this.blockSize;
			}
			return this.blockSize;
		}

		// Token: 0x04001FA0 RID: 8096
		private byte[] IV;

		// Token: 0x04001FA1 RID: 8097
		private byte[] FR;

		// Token: 0x04001FA2 RID: 8098
		private byte[] FRE;

		// Token: 0x04001FA3 RID: 8099
		private readonly IBlockCipher cipher;

		// Token: 0x04001FA4 RID: 8100
		private readonly int blockSize;

		// Token: 0x04001FA5 RID: 8101
		private int count;

		// Token: 0x04001FA6 RID: 8102
		private bool forEncryption;
	}
}
