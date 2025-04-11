using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003C6 RID: 966
	public interface IMac
	{
		// Token: 0x060027DF RID: 10207
		void Init(ICipherParameters parameters);

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x060027E0 RID: 10208
		string AlgorithmName { get; }

		// Token: 0x060027E1 RID: 10209
		int GetMacSize();

		// Token: 0x060027E2 RID: 10210
		void Update(byte input);

		// Token: 0x060027E3 RID: 10211
		void BlockUpdate(byte[] input, int inOff, int len);

		// Token: 0x060027E4 RID: 10212
		int DoFinal(byte[] output, int outOff);

		// Token: 0x060027E5 RID: 10213
		void Reset();
	}
}
