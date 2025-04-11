using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003F8 RID: 1016
	public abstract class ClientCertificateType
	{
		// Token: 0x04001B55 RID: 6997
		public const byte rsa_sign = 1;

		// Token: 0x04001B56 RID: 6998
		public const byte dss_sign = 2;

		// Token: 0x04001B57 RID: 6999
		public const byte rsa_fixed_dh = 3;

		// Token: 0x04001B58 RID: 7000
		public const byte dss_fixed_dh = 4;

		// Token: 0x04001B59 RID: 7001
		public const byte rsa_ephemeral_dh_RESERVED = 5;

		// Token: 0x04001B5A RID: 7002
		public const byte dss_ephemeral_dh_RESERVED = 6;

		// Token: 0x04001B5B RID: 7003
		public const byte fortezza_dms_RESERVED = 20;

		// Token: 0x04001B5C RID: 7004
		public const byte ecdsa_sign = 64;

		// Token: 0x04001B5D RID: 7005
		public const byte rsa_fixed_ecdh = 65;

		// Token: 0x04001B5E RID: 7006
		public const byte ecdsa_fixed_ecdh = 66;
	}
}
