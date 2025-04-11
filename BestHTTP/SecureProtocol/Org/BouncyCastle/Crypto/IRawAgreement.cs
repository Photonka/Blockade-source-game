using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003C8 RID: 968
	public interface IRawAgreement
	{
		// Token: 0x060027E9 RID: 10217
		void Init(ICipherParameters parameters);

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x060027EA RID: 10218
		int AgreementSize { get; }

		// Token: 0x060027EB RID: 10219
		void CalculateAgreement(ICipherParameters publicKey, byte[] buf, int off);
	}
}
