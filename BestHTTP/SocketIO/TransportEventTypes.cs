using System;

namespace BestHTTP.SocketIO
{
	// Token: 0x020001B5 RID: 437
	public enum TransportEventTypes
	{
		// Token: 0x04001347 RID: 4935
		Unknown = -1,
		// Token: 0x04001348 RID: 4936
		Open,
		// Token: 0x04001349 RID: 4937
		Close,
		// Token: 0x0400134A RID: 4938
		Ping,
		// Token: 0x0400134B RID: 4939
		Pong,
		// Token: 0x0400134C RID: 4940
		Message,
		// Token: 0x0400134D RID: 4941
		Upgrade,
		// Token: 0x0400134E RID: 4942
		Noop
	}
}
