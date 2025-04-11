using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000160 RID: 352
	public interface IClientCredentialsProvider
	{
		// Token: 0x06000C82 RID: 3202
		TlsCredentials GetClientCredentials(TlsContext context, CertificateRequest certificateRequest);
	}
}
