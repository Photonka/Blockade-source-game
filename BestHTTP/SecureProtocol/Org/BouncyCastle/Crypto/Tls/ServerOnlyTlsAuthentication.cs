using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200042F RID: 1071
	public abstract class ServerOnlyTlsAuthentication : TlsAuthentication
	{
		// Token: 0x06002AAA RID: 10922
		public abstract void NotifyServerCertificate(Certificate serverCertificate);

		// Token: 0x06002AAB RID: 10923 RVA: 0x0008F86E File Offset: 0x0008DA6E
		public TlsCredentials GetClientCredentials(TlsContext context, CertificateRequest certificateRequest)
		{
			return null;
		}
	}
}
