using System;
using System.Collections;
using System.Collections.Generic;

namespace BestHTTP.SignalR.Messages
{
	// Token: 0x02000205 RID: 517
	public sealed class MethodCallMessage : IServerMessage
	{
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x000A643E File Offset: 0x000A463E
		MessageTypes IServerMessage.Type
		{
			get
			{
				return MessageTypes.MethodCall;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x000A9F4A File Offset: 0x000A814A
		// (set) Token: 0x06001325 RID: 4901 RVA: 0x000A9F52 File Offset: 0x000A8152
		public string Hub { get; private set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x000A9F5B File Offset: 0x000A815B
		// (set) Token: 0x06001327 RID: 4903 RVA: 0x000A9F63 File Offset: 0x000A8163
		public string Method { get; private set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x000A9F6C File Offset: 0x000A816C
		// (set) Token: 0x06001329 RID: 4905 RVA: 0x000A9F74 File Offset: 0x000A8174
		public object[] Arguments { get; private set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x000A9F7D File Offset: 0x000A817D
		// (set) Token: 0x0600132B RID: 4907 RVA: 0x000A9F85 File Offset: 0x000A8185
		public IDictionary<string, object> State { get; private set; }

		// Token: 0x0600132C RID: 4908 RVA: 0x000A9F90 File Offset: 0x000A8190
		void IServerMessage.Parse(object data)
		{
			IDictionary<string, object> dictionary = data as IDictionary<string, object>;
			this.Hub = dictionary["H"].ToString();
			this.Method = dictionary["M"].ToString();
			List<object> list = new List<object>();
			foreach (object item in (dictionary["A"] as IEnumerable))
			{
				list.Add(item);
			}
			this.Arguments = list.ToArray();
			object obj;
			if (dictionary.TryGetValue("S", out obj))
			{
				this.State = (obj as IDictionary<string, object>);
			}
		}
	}
}
