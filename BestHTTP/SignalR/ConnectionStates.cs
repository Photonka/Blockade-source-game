using System;

namespace BestHTTP.SignalR
{
	// Token: 0x020001F5 RID: 501
	public enum ConnectionStates
	{
		// Token: 0x04001454 RID: 5204
		Initial,
		// Token: 0x04001455 RID: 5205
		Authenticating,
		// Token: 0x04001456 RID: 5206
		Negotiating,
		// Token: 0x04001457 RID: 5207
		Connecting,
		// Token: 0x04001458 RID: 5208
		Connected,
		// Token: 0x04001459 RID: 5209
		Reconnecting,
		// Token: 0x0400145A RID: 5210
		Closed
	}
}
