using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000418 RID: 1048
	public abstract class ExtensionType
	{
		// Token: 0x04001BDB RID: 7131
		public const int server_name = 0;

		// Token: 0x04001BDC RID: 7132
		public const int max_fragment_length = 1;

		// Token: 0x04001BDD RID: 7133
		public const int client_certificate_url = 2;

		// Token: 0x04001BDE RID: 7134
		public const int trusted_ca_keys = 3;

		// Token: 0x04001BDF RID: 7135
		public const int truncated_hmac = 4;

		// Token: 0x04001BE0 RID: 7136
		public const int status_request = 5;

		// Token: 0x04001BE1 RID: 7137
		public const int user_mapping = 6;

		// Token: 0x04001BE2 RID: 7138
		public const int client_authz = 7;

		// Token: 0x04001BE3 RID: 7139
		public const int server_authz = 8;

		// Token: 0x04001BE4 RID: 7140
		public const int cert_type = 9;

		// Token: 0x04001BE5 RID: 7141
		public const int supported_groups = 10;

		// Token: 0x04001BE6 RID: 7142
		[Obsolete("Use 'supported_groups' instead")]
		public const int elliptic_curves = 10;

		// Token: 0x04001BE7 RID: 7143
		public const int ec_point_formats = 11;

		// Token: 0x04001BE8 RID: 7144
		public const int srp = 12;

		// Token: 0x04001BE9 RID: 7145
		public const int signature_algorithms = 13;

		// Token: 0x04001BEA RID: 7146
		public const int use_srtp = 14;

		// Token: 0x04001BEB RID: 7147
		public const int heartbeat = 15;

		// Token: 0x04001BEC RID: 7148
		public const int application_layer_protocol_negotiation = 16;

		// Token: 0x04001BED RID: 7149
		public const int status_request_v2 = 17;

		// Token: 0x04001BEE RID: 7150
		public const int signed_certificate_timestamp = 18;

		// Token: 0x04001BEF RID: 7151
		public const int client_certificate_type = 19;

		// Token: 0x04001BF0 RID: 7152
		public const int server_certificate_type = 20;

		// Token: 0x04001BF1 RID: 7153
		public const int padding = 21;

		// Token: 0x04001BF2 RID: 7154
		public const int encrypt_then_mac = 22;

		// Token: 0x04001BF3 RID: 7155
		public const int extended_master_secret = 23;

		// Token: 0x04001BF4 RID: 7156
		public static readonly int DRAFT_token_binding = 24;

		// Token: 0x04001BF5 RID: 7157
		public const int cached_info = 25;

		// Token: 0x04001BF6 RID: 7158
		public const int session_ticket = 35;

		// Token: 0x04001BF7 RID: 7159
		public static readonly int negotiated_ff_dhe_groups = 101;

		// Token: 0x04001BF8 RID: 7160
		public const int renegotiation_info = 65281;
	}
}
