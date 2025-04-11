using System;
using System.Collections;
using System.Collections.Generic;

namespace BestHTTP.SignalR.Messages
{
	// Token: 0x02000203 RID: 515
	public sealed class MultiMessage : IServerMessage
	{
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600130F RID: 4879 RVA: 0x000A8A48 File Offset: 0x000A6C48
		MessageTypes IServerMessage.Type
		{
			get
			{
				return MessageTypes.Multiple;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x000A9D34 File Offset: 0x000A7F34
		// (set) Token: 0x06001311 RID: 4881 RVA: 0x000A9D3C File Offset: 0x000A7F3C
		public string MessageId { get; private set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x000A9D45 File Offset: 0x000A7F45
		// (set) Token: 0x06001313 RID: 4883 RVA: 0x000A9D4D File Offset: 0x000A7F4D
		public bool IsInitialization { get; private set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x000A9D56 File Offset: 0x000A7F56
		// (set) Token: 0x06001315 RID: 4885 RVA: 0x000A9D5E File Offset: 0x000A7F5E
		public string GroupsToken { get; private set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x000A9D67 File Offset: 0x000A7F67
		// (set) Token: 0x06001317 RID: 4887 RVA: 0x000A9D6F File Offset: 0x000A7F6F
		public bool ShouldReconnect { get; private set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x000A9D78 File Offset: 0x000A7F78
		// (set) Token: 0x06001319 RID: 4889 RVA: 0x000A9D80 File Offset: 0x000A7F80
		public TimeSpan? PollDelay { get; private set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x000A9D89 File Offset: 0x000A7F89
		// (set) Token: 0x0600131B RID: 4891 RVA: 0x000A9D91 File Offset: 0x000A7F91
		public List<IServerMessage> Data { get; private set; }

		// Token: 0x0600131C RID: 4892 RVA: 0x000A9D9C File Offset: 0x000A7F9C
		void IServerMessage.Parse(object data)
		{
			IDictionary<string, object> dictionary = data as IDictionary<string, object>;
			this.MessageId = dictionary["C"].ToString();
			object obj;
			if (dictionary.TryGetValue("S", out obj))
			{
				this.IsInitialization = (int.Parse(obj.ToString()) == 1);
			}
			else
			{
				this.IsInitialization = false;
			}
			if (dictionary.TryGetValue("G", out obj))
			{
				this.GroupsToken = obj.ToString();
			}
			if (dictionary.TryGetValue("T", out obj))
			{
				this.ShouldReconnect = (int.Parse(obj.ToString()) == 1);
			}
			else
			{
				this.ShouldReconnect = false;
			}
			if (dictionary.TryGetValue("L", out obj))
			{
				this.PollDelay = new TimeSpan?(TimeSpan.FromMilliseconds(double.Parse(obj.ToString())));
			}
			IEnumerable enumerable = dictionary["M"] as IEnumerable;
			if (enumerable != null)
			{
				this.Data = new List<IServerMessage>();
				foreach (object obj2 in enumerable)
				{
					IDictionary<string, object> dictionary2 = obj2 as IDictionary<string, object>;
					IServerMessage serverMessage;
					if (dictionary2 != null)
					{
						if (dictionary2.ContainsKey("H"))
						{
							serverMessage = new MethodCallMessage();
						}
						else if (dictionary2.ContainsKey("I"))
						{
							serverMessage = new ProgressMessage();
						}
						else
						{
							serverMessage = new DataMessage();
						}
					}
					else
					{
						serverMessage = new DataMessage();
					}
					serverMessage.Parse(obj2);
					this.Data.Add(serverMessage);
				}
			}
		}
	}
}
