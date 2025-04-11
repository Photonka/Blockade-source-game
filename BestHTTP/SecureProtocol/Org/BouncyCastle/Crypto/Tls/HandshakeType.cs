using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200041A RID: 1050
	public abstract class HandshakeType
	{
		// Token: 0x04001BFE RID: 7166
		public const byte hello_request = 0;

		// Token: 0x04001BFF RID: 7167
		public const byte client_hello = 1;

		// Token: 0x04001C00 RID: 7168
		public const byte server_hello = 2;

		// Token: 0x04001C01 RID: 7169
		public const byte certificate = 11;

		// Token: 0x04001C02 RID: 7170
		public const byte server_key_exchange = 12;

		// Token: 0x04001C03 RID: 7171
		public const byte certificate_request = 13;

		// Token: 0x04001C04 RID: 7172
		public const byte server_hello_done = 14;

		// Token: 0x04001C05 RID: 7173
		public const byte certificate_verify = 15;

		// Token: 0x04001C06 RID: 7174
		public const byte client_key_exchange = 16;

		// Token: 0x04001C07 RID: 7175
		public const byte finished = 20;

		// Token: 0x04001C08 RID: 7176
		public const byte certificate_url = 21;

		// Token: 0x04001C09 RID: 7177
		public const byte certificate_status = 22;

		// Token: 0x04001C0A RID: 7178
		public const byte hello_verify_request = 3;

		// Token: 0x04001C0B RID: 7179
		public const byte supplemental_data = 23;

		// Token: 0x04001C0C RID: 7180
		public const byte session_ticket = 4;
	}
}
