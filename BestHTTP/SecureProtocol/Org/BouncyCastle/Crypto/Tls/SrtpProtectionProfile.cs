using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000438 RID: 1080
	public abstract class SrtpProtectionProfile
	{
		// Token: 0x04001CBA RID: 7354
		public const int SRTP_AES128_CM_HMAC_SHA1_80 = 1;

		// Token: 0x04001CBB RID: 7355
		public const int SRTP_AES128_CM_HMAC_SHA1_32 = 2;

		// Token: 0x04001CBC RID: 7356
		public const int SRTP_NULL_HMAC_SHA1_80 = 5;

		// Token: 0x04001CBD RID: 7357
		public const int SRTP_NULL_HMAC_SHA1_32 = 6;

		// Token: 0x04001CBE RID: 7358
		public const int SRTP_AEAD_AES_128_GCM = 7;

		// Token: 0x04001CBF RID: 7359
		public const int SRTP_AEAD_AES_256_GCM = 8;
	}
}
