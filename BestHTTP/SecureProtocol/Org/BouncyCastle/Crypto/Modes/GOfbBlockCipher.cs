using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x02000509 RID: 1289
	public class GOfbBlockCipher : IBlockCipher
	{
		// Token: 0x06003121 RID: 12577 RVA: 0x0012D0F8 File Offset: 0x0012B2F8
		public GOfbBlockCipher(IBlockCipher cipher)
		{
			this.cipher = cipher;
			this.blockSize = cipher.GetBlockSize();
			if (this.blockSize != 8)
			{
				throw new ArgumentException("GCTR only for 64 bit block ciphers");
			}
			this.IV = new byte[cipher.GetBlockSize()];
			this.ofbV = new byte[cipher.GetBlockSize()];
			this.ofbOutV = new byte[cipher.GetBlockSize()];
		}

		// Token: 0x06003122 RID: 12578 RVA: 0x0012D16C File Offset: 0x0012B36C
		public IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x0012D174 File Offset: 0x0012B374
		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.firstStep = true;
			this.N3 = 0;
			this.N4 = 0;
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

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06003124 RID: 12580 RVA: 0x0012D22B File Offset: 0x0012B42B
		public string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/GCTR";
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06003125 RID: 12581 RVA: 0x0006CF70 File Offset: 0x0006B170
		public bool IsPartialBlockOkay
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x0012D242 File Offset: 0x0012B442
		public int GetBlockSize()
		{
			return this.blockSize;
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x0012D24C File Offset: 0x0012B44C
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
			if (this.firstStep)
			{
				this.firstStep = false;
				this.cipher.ProcessBlock(this.ofbV, 0, this.ofbOutV, 0);
				this.N3 = this.bytesToint(this.ofbOutV, 0);
				this.N4 = this.bytesToint(this.ofbOutV, 4);
			}
			this.N3 += 16843009;
			this.N4 += 16843012;
			if (this.N4 < 16843012 && this.N4 > 0)
			{
				this.N4++;
			}
			this.intTobytes(this.N3, this.ofbV, 0);
			this.intTobytes(this.N4, this.ofbV, 4);
			this.cipher.ProcessBlock(this.ofbV, 0, this.ofbOutV, 0);
			for (int i = 0; i < this.blockSize; i++)
			{
				output[outOff + i] = (this.ofbOutV[i] ^ input[inOff + i]);
			}
			Array.Copy(this.ofbV, this.blockSize, this.ofbV, 0, this.ofbV.Length - this.blockSize);
			Array.Copy(this.ofbOutV, 0, this.ofbV, this.ofbV.Length - this.blockSize, this.blockSize);
			return this.blockSize;
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x0012D3DB File Offset: 0x0012B5DB
		public void Reset()
		{
			Array.Copy(this.IV, 0, this.ofbV, 0, this.IV.Length);
			this.cipher.Reset();
		}

		// Token: 0x06003129 RID: 12585 RVA: 0x0012D403 File Offset: 0x0012B603
		private int bytesToint(byte[] inBytes, int inOff)
		{
			return (int)((long)((long)inBytes[inOff + 3] << 24) & (long)((ulong)-16777216)) + ((int)inBytes[inOff + 2] << 16 & 16711680) + ((int)inBytes[inOff + 1] << 8 & 65280) + (int)(inBytes[inOff] & byte.MaxValue);
		}

		// Token: 0x0600312A RID: 12586 RVA: 0x0012D43D File Offset: 0x0012B63D
		private void intTobytes(int num, byte[] outBytes, int outOff)
		{
			outBytes[outOff + 3] = (byte)(num >> 24);
			outBytes[outOff + 2] = (byte)(num >> 16);
			outBytes[outOff + 1] = (byte)(num >> 8);
			outBytes[outOff] = (byte)num;
		}

		// Token: 0x04001F61 RID: 8033
		private byte[] IV;

		// Token: 0x04001F62 RID: 8034
		private byte[] ofbV;

		// Token: 0x04001F63 RID: 8035
		private byte[] ofbOutV;

		// Token: 0x04001F64 RID: 8036
		private readonly int blockSize;

		// Token: 0x04001F65 RID: 8037
		private readonly IBlockCipher cipher;

		// Token: 0x04001F66 RID: 8038
		private bool firstStep = true;

		// Token: 0x04001F67 RID: 8039
		private int N3;

		// Token: 0x04001F68 RID: 8040
		private int N4;

		// Token: 0x04001F69 RID: 8041
		private const int C1 = 16843012;

		// Token: 0x04001F6A RID: 8042
		private const int C2 = 16843009;
	}
}
