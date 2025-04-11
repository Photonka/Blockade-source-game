using System;

namespace BestHTTP.SignalRCore.Messages
{
	// Token: 0x020001E3 RID: 483
	public struct PingMessage
	{
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x000A6441 File Offset: 0x000A4641
		public MessageTypes type
		{
			get
			{
				return MessageTypes.Ping;
			}
		}
	}
}
