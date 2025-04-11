using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003C1 RID: 961
	public interface IDigest
	{
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060027CF RID: 10191
		string AlgorithmName { get; }

		// Token: 0x060027D0 RID: 10192
		int GetDigestSize();

		// Token: 0x060027D1 RID: 10193
		int GetByteLength();

		// Token: 0x060027D2 RID: 10194
		void Update(byte input);

		// Token: 0x060027D3 RID: 10195
		void BlockUpdate(byte[] input, int inOff, int length);

		// Token: 0x060027D4 RID: 10196
		int DoFinal(byte[] output, int outOff);

		// Token: 0x060027D5 RID: 10197
		void Reset();
	}
}
