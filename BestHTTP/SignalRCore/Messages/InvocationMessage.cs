using System;

namespace BestHTTP.SignalRCore.Messages
{
	// Token: 0x020001E1 RID: 481
	public struct InvocationMessage
	{
		// Token: 0x040013F9 RID: 5113
		public MessageTypes type;

		// Token: 0x040013FA RID: 5114
		public string invocationId;

		// Token: 0x040013FB RID: 5115
		public bool nonblocking;

		// Token: 0x040013FC RID: 5116
		public string target;

		// Token: 0x040013FD RID: 5117
		public object[] arguments;
	}
}
