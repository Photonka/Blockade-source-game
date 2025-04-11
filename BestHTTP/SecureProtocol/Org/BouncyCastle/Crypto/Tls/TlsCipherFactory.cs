using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000441 RID: 1089
	public interface TlsCipherFactory
	{
		// Token: 0x06002B05 RID: 11013
		TlsCipher CreateCipher(TlsContext context, int encryptionAlgorithm, int macAlgorithm);
	}
}
