using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000459 RID: 1113
	public interface TlsHandshakeHash : IDigest
	{
		// Token: 0x06002BEF RID: 11247
		void Init(TlsContext context);

		// Token: 0x06002BF0 RID: 11248
		TlsHandshakeHash NotifyPrfDetermined();

		// Token: 0x06002BF1 RID: 11249
		void TrackHashAlgorithm(byte hashAlgorithm);

		// Token: 0x06002BF2 RID: 11250
		void SealHashAlgorithms();

		// Token: 0x06002BF3 RID: 11251
		TlsHandshakeHash StopTracking();

		// Token: 0x06002BF4 RID: 11252
		IDigest ForkPrfHash();

		// Token: 0x06002BF5 RID: 11253
		byte[] GetFinalHash(byte hashAlgorithm);
	}
}
