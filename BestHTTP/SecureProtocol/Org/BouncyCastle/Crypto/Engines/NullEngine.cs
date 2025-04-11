using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200056A RID: 1386
	public class NullEngine : IBlockCipher
	{
		// Token: 0x060034B1 RID: 13489 RVA: 0x00146678 File Offset: 0x00144878
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.initialised = true;
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x060034B2 RID: 13490 RVA: 0x00146681 File Offset: 0x00144881
		public virtual string AlgorithmName
		{
			get
			{
				return "Null";
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x060034B3 RID: 13491 RVA: 0x0006CF70 File Offset: 0x0006B170
		public virtual bool IsPartialBlockOkay
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060034B4 RID: 13492 RVA: 0x0006CF70 File Offset: 0x0006B170
		public virtual int GetBlockSize()
		{
			return 1;
		}

		// Token: 0x060034B5 RID: 13493 RVA: 0x00146688 File Offset: 0x00144888
		public virtual int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (!this.initialised)
			{
				throw new InvalidOperationException("Null engine not initialised");
			}
			Check.DataLength(input, inOff, 1, "input buffer too short");
			Check.OutputLength(output, outOff, 1, "output buffer too short");
			for (int i = 0; i < 1; i++)
			{
				output[outOff + i] = input[inOff + i];
			}
			return 1;
		}

		// Token: 0x060034B6 RID: 13494 RVA: 0x00002B75 File Offset: 0x00000D75
		public virtual void Reset()
		{
		}

		// Token: 0x04002188 RID: 8584
		private bool initialised;

		// Token: 0x04002189 RID: 8585
		private const int BlockSize = 1;
	}
}
