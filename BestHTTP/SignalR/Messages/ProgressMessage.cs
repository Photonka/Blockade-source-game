using System;
using System.Collections.Generic;

namespace BestHTTP.SignalR.Messages
{
	// Token: 0x02000208 RID: 520
	public sealed class ProgressMessage : IServerMessage, IHubMessage
	{
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x000A6441 File Offset: 0x000A4641
		MessageTypes IServerMessage.Type
		{
			get
			{
				return MessageTypes.Progress;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x000AA21A File Offset: 0x000A841A
		// (set) Token: 0x06001348 RID: 4936 RVA: 0x000AA222 File Offset: 0x000A8422
		public ulong InvocationId { get; private set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x000AA22B File Offset: 0x000A842B
		// (set) Token: 0x0600134A RID: 4938 RVA: 0x000AA233 File Offset: 0x000A8433
		public double Progress { get; private set; }

		// Token: 0x0600134B RID: 4939 RVA: 0x000AA23C File Offset: 0x000A843C
		void IServerMessage.Parse(object data)
		{
			IDictionary<string, object> dictionary = (data as IDictionary<string, object>)["P"] as IDictionary<string, object>;
			this.InvocationId = ulong.Parse(dictionary["I"].ToString());
			this.Progress = double.Parse(dictionary["D"].ToString());
		}
	}
}
