using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200046C RID: 1132
	public interface TlsSession
	{
		// Token: 0x06002CB3 RID: 11443
		SessionParameters ExportSessionParameters();

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06002CB4 RID: 11444
		byte[] SessionID { get; }

		// Token: 0x06002CB5 RID: 11445
		void Invalidate();

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06002CB6 RID: 11446
		bool IsResumable { get; }
	}
}
