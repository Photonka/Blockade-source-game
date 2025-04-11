using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003D0 RID: 976
	public interface IVerifierFactory
	{
		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06002807 RID: 10247
		object AlgorithmDetails { get; }

		// Token: 0x06002808 RID: 10248
		IStreamCalculator CreateCalculator();
	}
}
