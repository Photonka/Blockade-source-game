using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003CB RID: 971
	public interface ISigner
	{
		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x060027F4 RID: 10228
		string AlgorithmName { get; }

		// Token: 0x060027F5 RID: 10229
		void Init(bool forSigning, ICipherParameters parameters);

		// Token: 0x060027F6 RID: 10230
		void Update(byte input);

		// Token: 0x060027F7 RID: 10231
		void BlockUpdate(byte[] input, int inOff, int length);

		// Token: 0x060027F8 RID: 10232
		byte[] GenerateSignature();

		// Token: 0x060027F9 RID: 10233
		bool VerifySignature(byte[] signature);

		// Token: 0x060027FA RID: 10234
		void Reset();
	}
}
