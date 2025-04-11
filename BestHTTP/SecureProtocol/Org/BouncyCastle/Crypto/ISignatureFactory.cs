using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003CA RID: 970
	public interface ISignatureFactory
	{
		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060027F2 RID: 10226
		object AlgorithmDetails { get; }

		// Token: 0x060027F3 RID: 10227
		IStreamCalculator CreateCalculator();
	}
}
