using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003B8 RID: 952
	public interface IAsymmetricBlockCipher
	{
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060027A8 RID: 10152
		string AlgorithmName { get; }

		// Token: 0x060027A9 RID: 10153
		void Init(bool forEncryption, ICipherParameters parameters);

		// Token: 0x060027AA RID: 10154
		int GetInputBlockSize();

		// Token: 0x060027AB RID: 10155
		int GetOutputBlockSize();

		// Token: 0x060027AC RID: 10156
		byte[] ProcessBlock(byte[] inBuf, int inOff, int inLen);
	}
}
