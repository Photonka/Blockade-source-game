using System;

namespace BestHTTP.SignalRCore.Messages
{
	// Token: 0x020001E4 RID: 484
	public enum MessageTypes
	{
		// Token: 0x04001400 RID: 5120
		Handshake,
		// Token: 0x04001401 RID: 5121
		Invocation,
		// Token: 0x04001402 RID: 5122
		StreamItem,
		// Token: 0x04001403 RID: 5123
		Completion,
		// Token: 0x04001404 RID: 5124
		StreamInvocation,
		// Token: 0x04001405 RID: 5125
		CancelInvocation,
		// Token: 0x04001406 RID: 5126
		Ping,
		// Token: 0x04001407 RID: 5127
		Close
	}
}
