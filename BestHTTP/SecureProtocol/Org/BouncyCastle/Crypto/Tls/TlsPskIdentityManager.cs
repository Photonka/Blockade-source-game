using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000463 RID: 1123
	public interface TlsPskIdentityManager
	{
		// Token: 0x06002C63 RID: 11363
		byte[] GetHint();

		// Token: 0x06002C64 RID: 11364
		byte[] GetPsk(byte[] identity);
	}
}
