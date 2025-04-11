using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes
{
	// Token: 0x02000510 RID: 1296
	public class SicBlockCipher : IBlockCipher
	{
		// Token: 0x06003189 RID: 12681 RVA: 0x0012F644 File Offset: 0x0012D844
		public SicBlockCipher(IBlockCipher cipher)
		{
			this.cipher = cipher;
			this.blockSize = cipher.GetBlockSize();
			this.counter = new byte[this.blockSize];
			this.counterOut = new byte[this.blockSize];
			this.IV = new byte[this.blockSize];
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x0012F69D File Offset: 0x0012D89D
		public virtual IBlockCipher GetUnderlyingCipher()
		{
			return this.cipher;
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x0012F6A8 File Offset: 0x0012D8A8
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			ParametersWithIV parametersWithIV = parameters as ParametersWithIV;
			if (parametersWithIV == null)
			{
				throw new ArgumentException("CTR/SIC mode requires ParametersWithIV", "parameters");
			}
			this.IV = Arrays.Clone(parametersWithIV.GetIV());
			if (this.blockSize < this.IV.Length)
			{
				throw new ArgumentException("CTR/SIC mode requires IV no greater than: " + this.blockSize + " bytes.");
			}
			int num = Math.Min(8, this.blockSize / 2);
			if (this.blockSize - this.IV.Length > num)
			{
				throw new ArgumentException("CTR/SIC mode requires IV of at least: " + (this.blockSize - num) + " bytes.");
			}
			if (parametersWithIV.Parameters != null)
			{
				this.cipher.Init(true, parametersWithIV.Parameters);
			}
			this.Reset();
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x0600318C RID: 12684 RVA: 0x0012F773 File Offset: 0x0012D973
		public virtual string AlgorithmName
		{
			get
			{
				return this.cipher.AlgorithmName + "/SIC";
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x0600318D RID: 12685 RVA: 0x0006CF70 File Offset: 0x0006B170
		public virtual bool IsPartialBlockOkay
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600318E RID: 12686 RVA: 0x0012F78A File Offset: 0x0012D98A
		public virtual int GetBlockSize()
		{
			return this.cipher.GetBlockSize();
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x0012F798 File Offset: 0x0012D998
		public virtual int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			this.cipher.ProcessBlock(this.counter, 0, this.counterOut, 0);
			for (int i = 0; i < this.counterOut.Length; i++)
			{
				output[outOff + i] = (this.counterOut[i] ^ input[inOff + i]);
			}
			int num = this.counter.Length;
			while (--num >= 0)
			{
				byte[] array = this.counter;
				int num2 = num;
				byte b = array[num2] + 1;
				array[num2] = b;
				if (b != 0)
				{
					break;
				}
			}
			return this.counter.Length;
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x0012F817 File Offset: 0x0012DA17
		public virtual void Reset()
		{
			Arrays.Fill(this.counter, 0);
			Array.Copy(this.IV, 0, this.counter, 0, this.IV.Length);
			this.cipher.Reset();
		}

		// Token: 0x04001FA7 RID: 8103
		private readonly IBlockCipher cipher;

		// Token: 0x04001FA8 RID: 8104
		private readonly int blockSize;

		// Token: 0x04001FA9 RID: 8105
		private readonly byte[] counter;

		// Token: 0x04001FAA RID: 8106
		private readonly byte[] counterOut;

		// Token: 0x04001FAB RID: 8107
		private byte[] IV;
	}
}
