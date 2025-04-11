using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003C4 RID: 964
	public interface IEntropySource
	{
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x060027DB RID: 10203
		bool IsPredictionResistant { get; }

		// Token: 0x060027DC RID: 10204
		byte[] GetEntropy();

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x060027DD RID: 10205
		int EntropySize { get; }
	}
}
