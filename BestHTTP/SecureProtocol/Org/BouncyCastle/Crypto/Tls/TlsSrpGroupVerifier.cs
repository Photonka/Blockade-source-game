using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000470 RID: 1136
	public interface TlsSrpGroupVerifier
	{
		// Token: 0x06002CC8 RID: 11464
		bool Accept(Srp6GroupParameters group);
	}
}
