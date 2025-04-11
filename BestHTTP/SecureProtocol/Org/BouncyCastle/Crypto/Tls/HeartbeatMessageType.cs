using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200041E RID: 1054
	public abstract class HeartbeatMessageType
	{
		// Token: 0x06002A3F RID: 10815 RVA: 0x001148B1 File Offset: 0x00112AB1
		public static bool IsValid(byte heartbeatMessageType)
		{
			return heartbeatMessageType >= 1 && heartbeatMessageType <= 2;
		}

		// Token: 0x04001C18 RID: 7192
		public const byte heartbeat_request = 1;

		// Token: 0x04001C19 RID: 7193
		public const byte heartbeat_response = 2;
	}
}
