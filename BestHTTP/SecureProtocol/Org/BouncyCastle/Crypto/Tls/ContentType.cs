using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003FC RID: 1020
	public abstract class ContentType
	{
		// Token: 0x04001B66 RID: 7014
		public const byte change_cipher_spec = 20;

		// Token: 0x04001B67 RID: 7015
		public const byte alert = 21;

		// Token: 0x04001B68 RID: 7016
		public const byte handshake = 22;

		// Token: 0x04001B69 RID: 7017
		public const byte application_data = 23;

		// Token: 0x04001B6A RID: 7018
		public const byte heartbeat = 24;
	}
}
