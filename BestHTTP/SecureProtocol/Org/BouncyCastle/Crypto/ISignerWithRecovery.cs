using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003CC RID: 972
	public interface ISignerWithRecovery : ISigner
	{
		// Token: 0x060027FB RID: 10235
		bool HasFullMessage();

		// Token: 0x060027FC RID: 10236
		byte[] GetRecoveredMessage();

		// Token: 0x060027FD RID: 10237
		void UpdateWithRecoveredMessage(byte[] signature);
	}
}
