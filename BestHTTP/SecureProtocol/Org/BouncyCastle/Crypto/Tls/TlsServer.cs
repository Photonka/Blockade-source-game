using System;
using System.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000468 RID: 1128
	public interface TlsServer : TlsPeer
	{
		// Token: 0x06002C89 RID: 11401
		void Init(TlsServerContext context);

		// Token: 0x06002C8A RID: 11402
		void NotifyClientVersion(ProtocolVersion clientVersion);

		// Token: 0x06002C8B RID: 11403
		void NotifyFallback(bool isFallback);

		// Token: 0x06002C8C RID: 11404
		void NotifyOfferedCipherSuites(int[] offeredCipherSuites);

		// Token: 0x06002C8D RID: 11405
		void NotifyOfferedCompressionMethods(byte[] offeredCompressionMethods);

		// Token: 0x06002C8E RID: 11406
		void ProcessClientExtensions(IDictionary clientExtensions);

		// Token: 0x06002C8F RID: 11407
		ProtocolVersion GetServerVersion();

		// Token: 0x06002C90 RID: 11408
		int GetSelectedCipherSuite();

		// Token: 0x06002C91 RID: 11409
		byte GetSelectedCompressionMethod();

		// Token: 0x06002C92 RID: 11410
		IDictionary GetServerExtensions();

		// Token: 0x06002C93 RID: 11411
		IList GetServerSupplementalData();

		// Token: 0x06002C94 RID: 11412
		TlsCredentials GetCredentials();

		// Token: 0x06002C95 RID: 11413
		CertificateStatus GetCertificateStatus();

		// Token: 0x06002C96 RID: 11414
		TlsKeyExchange GetKeyExchange();

		// Token: 0x06002C97 RID: 11415
		CertificateRequest GetCertificateRequest();

		// Token: 0x06002C98 RID: 11416
		void ProcessClientSupplementalData(IList clientSupplementalData);

		// Token: 0x06002C99 RID: 11417
		void NotifyClientCertificate(Certificate clientCertificate);

		// Token: 0x06002C9A RID: 11418
		NewSessionTicket GetNewSessionTicket();
	}
}
