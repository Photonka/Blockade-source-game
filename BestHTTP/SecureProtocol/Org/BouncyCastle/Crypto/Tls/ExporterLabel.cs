using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000417 RID: 1047
	public abstract class ExporterLabel
	{
		// Token: 0x04001BD2 RID: 7122
		public const string client_finished = "client finished";

		// Token: 0x04001BD3 RID: 7123
		public const string server_finished = "server finished";

		// Token: 0x04001BD4 RID: 7124
		public const string master_secret = "master secret";

		// Token: 0x04001BD5 RID: 7125
		public const string key_expansion = "key expansion";

		// Token: 0x04001BD6 RID: 7126
		public const string client_EAP_encryption = "client EAP encryption";

		// Token: 0x04001BD7 RID: 7127
		public const string ttls_keying_material = "ttls keying material";

		// Token: 0x04001BD8 RID: 7128
		public const string ttls_challenge = "ttls challenge";

		// Token: 0x04001BD9 RID: 7129
		public const string dtls_srtp = "EXTRACTOR-dtls_srtp";

		// Token: 0x04001BDA RID: 7130
		public static readonly string extended_master_secret = "extended master secret";
	}
}
