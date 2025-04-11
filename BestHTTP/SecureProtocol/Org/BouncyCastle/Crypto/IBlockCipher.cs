using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003BB RID: 955
	public interface IBlockCipher
	{
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060027B2 RID: 10162
		string AlgorithmName { get; }

		// Token: 0x060027B3 RID: 10163
		void Init(bool forEncryption, ICipherParameters parameters);

		// Token: 0x060027B4 RID: 10164
		int GetBlockSize();

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060027B5 RID: 10165
		bool IsPartialBlockOkay { get; }

		// Token: 0x060027B6 RID: 10166
		int ProcessBlock(byte[] inBuf, int inOff, byte[] outBuf, int outOff);

		// Token: 0x060027B7 RID: 10167
		void Reset();
	}
}
