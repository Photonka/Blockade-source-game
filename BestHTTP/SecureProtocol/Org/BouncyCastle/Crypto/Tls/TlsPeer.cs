using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200045F RID: 1119
	public interface TlsPeer
	{
		// Token: 0x06002C15 RID: 11285
		bool RequiresExtendedMasterSecret();

		// Token: 0x06002C16 RID: 11286
		bool ShouldUseGmtUnixTime();

		// Token: 0x06002C17 RID: 11287
		void NotifySecureRenegotiation(bool secureRenegotiation);

		// Token: 0x06002C18 RID: 11288
		TlsCompression GetCompression();

		// Token: 0x06002C19 RID: 11289
		TlsCipher GetCipher();

		// Token: 0x06002C1A RID: 11290
		void NotifyAlertRaised(byte alertLevel, byte alertDescription, string message, Exception cause);

		// Token: 0x06002C1B RID: 11291
		void NotifyAlertReceived(byte alertLevel, byte alertDescription);

		// Token: 0x06002C1C RID: 11292
		void NotifyHandshakeComplete();
	}
}
