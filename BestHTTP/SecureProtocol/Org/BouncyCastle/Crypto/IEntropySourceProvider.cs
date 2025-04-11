using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003C5 RID: 965
	public interface IEntropySourceProvider
	{
		// Token: 0x060027DE RID: 10206
		IEntropySource Get(int bitsRequired);
	}
}
