using System;
using BestHTTP.SignalR.Messages;

namespace BestHTTP.SignalR.Hubs
{
	// Token: 0x02000212 RID: 530
	public interface IHub
	{
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06001380 RID: 4992
		// (set) Token: 0x06001381 RID: 4993
		Connection Connection { get; set; }

		// Token: 0x06001382 RID: 4994
		bool Call(ClientMessage msg);

		// Token: 0x06001383 RID: 4995
		bool HasSentMessageId(ulong id);

		// Token: 0x06001384 RID: 4996
		void Close();

		// Token: 0x06001385 RID: 4997
		void OnMethod(MethodCallMessage msg);

		// Token: 0x06001386 RID: 4998
		void OnMessage(IServerMessage msg);
	}
}
