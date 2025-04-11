using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000420 RID: 1056
	public abstract class KeyExchangeAlgorithm
	{
		// Token: 0x04001C1C RID: 7196
		public const int NULL = 0;

		// Token: 0x04001C1D RID: 7197
		public const int RSA = 1;

		// Token: 0x04001C1E RID: 7198
		public const int RSA_EXPORT = 2;

		// Token: 0x04001C1F RID: 7199
		public const int DHE_DSS = 3;

		// Token: 0x04001C20 RID: 7200
		public const int DHE_DSS_EXPORT = 4;

		// Token: 0x04001C21 RID: 7201
		public const int DHE_RSA = 5;

		// Token: 0x04001C22 RID: 7202
		public const int DHE_RSA_EXPORT = 6;

		// Token: 0x04001C23 RID: 7203
		public const int DH_DSS = 7;

		// Token: 0x04001C24 RID: 7204
		public const int DH_DSS_EXPORT = 8;

		// Token: 0x04001C25 RID: 7205
		public const int DH_RSA = 9;

		// Token: 0x04001C26 RID: 7206
		public const int DH_RSA_EXPORT = 10;

		// Token: 0x04001C27 RID: 7207
		public const int DH_anon = 11;

		// Token: 0x04001C28 RID: 7208
		public const int DH_anon_EXPORT = 12;

		// Token: 0x04001C29 RID: 7209
		public const int PSK = 13;

		// Token: 0x04001C2A RID: 7210
		public const int DHE_PSK = 14;

		// Token: 0x04001C2B RID: 7211
		public const int RSA_PSK = 15;

		// Token: 0x04001C2C RID: 7212
		public const int ECDH_ECDSA = 16;

		// Token: 0x04001C2D RID: 7213
		public const int ECDHE_ECDSA = 17;

		// Token: 0x04001C2E RID: 7214
		public const int ECDH_RSA = 18;

		// Token: 0x04001C2F RID: 7215
		public const int ECDHE_RSA = 19;

		// Token: 0x04001C30 RID: 7216
		public const int ECDH_anon = 20;

		// Token: 0x04001C31 RID: 7217
		public const int SRP = 21;

		// Token: 0x04001C32 RID: 7218
		public const int SRP_DSS = 22;

		// Token: 0x04001C33 RID: 7219
		public const int SRP_RSA = 23;

		// Token: 0x04001C34 RID: 7220
		public const int ECDHE_PSK = 24;
	}
}
