using System;

namespace BestHTTP.SignalR
{
	// Token: 0x020001F7 RID: 503
	public enum TransportStates
	{
		// Token: 0x04001465 RID: 5221
		Initial,
		// Token: 0x04001466 RID: 5222
		Connecting,
		// Token: 0x04001467 RID: 5223
		Reconnecting,
		// Token: 0x04001468 RID: 5224
		Starting,
		// Token: 0x04001469 RID: 5225
		Started,
		// Token: 0x0400146A RID: 5226
		Closing,
		// Token: 0x0400146B RID: 5227
		Closed
	}
}
