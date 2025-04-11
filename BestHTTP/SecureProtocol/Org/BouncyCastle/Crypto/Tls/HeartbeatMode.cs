using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200041F RID: 1055
	public abstract class HeartbeatMode
	{
		// Token: 0x06002A41 RID: 10817 RVA: 0x001148B1 File Offset: 0x00112AB1
		public static bool IsValid(byte heartbeatMode)
		{
			return heartbeatMode >= 1 && heartbeatMode <= 2;
		}

		// Token: 0x04001C1A RID: 7194
		public const byte peer_allowed_to_send = 1;

		// Token: 0x04001C1B RID: 7195
		public const byte peer_not_allowed_to_send = 2;
	}
}
