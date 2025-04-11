using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003F7 RID: 1015
	public abstract class ClientAuthenticationType
	{
		// Token: 0x04001B52 RID: 6994
		public const byte anonymous = 0;

		// Token: 0x04001B53 RID: 6995
		public const byte certificate_based = 1;

		// Token: 0x04001B54 RID: 6996
		public const byte psk = 2;
	}
}
