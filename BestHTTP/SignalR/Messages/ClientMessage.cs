using System;
using BestHTTP.SignalR.Hubs;

namespace BestHTTP.SignalR.Messages
{
	// Token: 0x020001FF RID: 511
	public struct ClientMessage
	{
		// Token: 0x06001308 RID: 4872 RVA: 0x000A9CFD File Offset: 0x000A7EFD
		public ClientMessage(Hub hub, string method, object[] args, ulong callIdx, OnMethodResultDelegate resultCallback, OnMethodFailedDelegate resultErrorCallback, OnMethodProgressDelegate progressCallback)
		{
			this.Hub = hub;
			this.Method = method;
			this.Args = args;
			this.CallIdx = callIdx;
			this.ResultCallback = resultCallback;
			this.ResultErrorCallback = resultErrorCallback;
			this.ProgressCallback = progressCallback;
		}

		// Token: 0x04001487 RID: 5255
		public readonly Hub Hub;

		// Token: 0x04001488 RID: 5256
		public readonly string Method;

		// Token: 0x04001489 RID: 5257
		public readonly object[] Args;

		// Token: 0x0400148A RID: 5258
		public readonly ulong CallIdx;

		// Token: 0x0400148B RID: 5259
		public readonly OnMethodResultDelegate ResultCallback;

		// Token: 0x0400148C RID: 5260
		public readonly OnMethodFailedDelegate ResultErrorCallback;

		// Token: 0x0400148D RID: 5261
		public readonly OnMethodProgressDelegate ProgressCallback;
	}
}
