using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200043E RID: 1086
	public interface TlsAuthentication
	{
		// Token: 0x06002AF7 RID: 10999
		void NotifyServerCertificate(Certificate serverCertificate);

		// Token: 0x06002AF8 RID: 11000
		TlsCredentials GetClientCredentials(TlsContext context, CertificateRequest certificateRequest);
	}
}
