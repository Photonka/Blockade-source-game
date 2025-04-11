using System;

namespace BestHTTP.SignalR.Messages
{
	// Token: 0x02000200 RID: 512
	public interface IServerMessage
	{
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06001309 RID: 4873
		MessageTypes Type { get; }

		// Token: 0x0600130A RID: 4874
		void Parse(object data);
	}
}
