using System;

namespace BestHTTP.SignalR
{
	// Token: 0x020001F4 RID: 500
	public enum MessageTypes
	{
		// Token: 0x0400144C RID: 5196
		KeepAlive,
		// Token: 0x0400144D RID: 5197
		Data,
		// Token: 0x0400144E RID: 5198
		Multiple,
		// Token: 0x0400144F RID: 5199
		Result,
		// Token: 0x04001450 RID: 5200
		Failure,
		// Token: 0x04001451 RID: 5201
		MethodCall,
		// Token: 0x04001452 RID: 5202
		Progress
	}
}
