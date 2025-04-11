using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000471 RID: 1137
	public interface TlsSrpIdentityManager
	{
		// Token: 0x06002CC9 RID: 11465
		TlsSrpLoginParameters GetLoginParameters(byte[] identity);
	}
}
