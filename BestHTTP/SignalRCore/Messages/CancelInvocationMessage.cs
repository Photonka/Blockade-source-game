using System;

namespace BestHTTP.SignalRCore.Messages
{
	// Token: 0x020001E2 RID: 482
	public struct CancelInvocationMessage
	{
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x000A643E File Offset: 0x000A463E
		public MessageTypes type
		{
			get
			{
				return MessageTypes.CancelInvocation;
			}
		}

		// Token: 0x040013FE RID: 5118
		public string invocationId;
	}
}
