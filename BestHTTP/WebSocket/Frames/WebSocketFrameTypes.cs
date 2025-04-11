using System;

namespace BestHTTP.WebSocket.Frames
{
	// Token: 0x020001B0 RID: 432
	public enum WebSocketFrameTypes : byte
	{
		// Token: 0x04001324 RID: 4900
		Continuation,
		// Token: 0x04001325 RID: 4901
		Text,
		// Token: 0x04001326 RID: 4902
		Binary,
		// Token: 0x04001327 RID: 4903
		ConnectionClose = 8,
		// Token: 0x04001328 RID: 4904
		Ping,
		// Token: 0x04001329 RID: 4905
		Pong
	}
}
