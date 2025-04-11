using System;

namespace BestHTTP.SignalR.Messages
{
	// Token: 0x02000202 RID: 514
	public sealed class KeepAliveMessage : IServerMessage
	{
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		MessageTypes IServerMessage.Type
		{
			get
			{
				return MessageTypes.KeepAlive;
			}
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00002B75 File Offset: 0x00000D75
		void IServerMessage.Parse(object data)
		{
		}
	}
}
