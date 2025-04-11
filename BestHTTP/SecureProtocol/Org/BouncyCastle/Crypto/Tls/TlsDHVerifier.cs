using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200044D RID: 1101
	public interface TlsDHVerifier
	{
		// Token: 0x06002B69 RID: 11113
		bool Accept(DHParameters dhParameters);
	}
}
