using System;
using System.Collections.Generic;

namespace BestHTTP.SignalR.Messages
{
	// Token: 0x02000206 RID: 518
	public sealed class ResultMessage : IServerMessage, IHubMessage
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x000AA054 File Offset: 0x000A8254
		MessageTypes IServerMessage.Type
		{
			get
			{
				return MessageTypes.Result;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x0600132F RID: 4911 RVA: 0x000AA057 File Offset: 0x000A8257
		// (set) Token: 0x06001330 RID: 4912 RVA: 0x000AA05F File Offset: 0x000A825F
		public ulong InvocationId { get; private set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x000AA068 File Offset: 0x000A8268
		// (set) Token: 0x06001332 RID: 4914 RVA: 0x000AA070 File Offset: 0x000A8270
		public object ReturnValue { get; private set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x000AA079 File Offset: 0x000A8279
		// (set) Token: 0x06001334 RID: 4916 RVA: 0x000AA081 File Offset: 0x000A8281
		public IDictionary<string, object> State { get; private set; }

		// Token: 0x06001335 RID: 4917 RVA: 0x000AA08C File Offset: 0x000A828C
		void IServerMessage.Parse(object data)
		{
			IDictionary<string, object> dictionary = data as IDictionary<string, object>;
			this.InvocationId = ulong.Parse(dictionary["I"].ToString());
			object obj;
			if (dictionary.TryGetValue("R", out obj))
			{
				this.ReturnValue = obj;
			}
			if (dictionary.TryGetValue("S", out obj))
			{
				this.State = (obj as IDictionary<string, object>);
			}
		}
	}
}
