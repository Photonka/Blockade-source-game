using System;
using System.Collections.Generic;

namespace BestHTTP.SignalR.Messages
{
	// Token: 0x02000207 RID: 519
	public sealed class FailureMessage : IServerMessage, IHubMessage
	{
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x000AA0EC File Offset: 0x000A82EC
		MessageTypes IServerMessage.Type
		{
			get
			{
				return MessageTypes.Failure;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x000AA0EF File Offset: 0x000A82EF
		// (set) Token: 0x06001339 RID: 4921 RVA: 0x000AA0F7 File Offset: 0x000A82F7
		public ulong InvocationId { get; private set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x000AA100 File Offset: 0x000A8300
		// (set) Token: 0x0600133B RID: 4923 RVA: 0x000AA108 File Offset: 0x000A8308
		public bool IsHubError { get; private set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x000AA111 File Offset: 0x000A8311
		// (set) Token: 0x0600133D RID: 4925 RVA: 0x000AA119 File Offset: 0x000A8319
		public string ErrorMessage { get; private set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x000AA122 File Offset: 0x000A8322
		// (set) Token: 0x0600133F RID: 4927 RVA: 0x000AA12A File Offset: 0x000A832A
		public IDictionary<string, object> AdditionalData { get; private set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x000AA133 File Offset: 0x000A8333
		// (set) Token: 0x06001341 RID: 4929 RVA: 0x000AA13B File Offset: 0x000A833B
		public string StackTrace { get; private set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06001342 RID: 4930 RVA: 0x000AA144 File Offset: 0x000A8344
		// (set) Token: 0x06001343 RID: 4931 RVA: 0x000AA14C File Offset: 0x000A834C
		public IDictionary<string, object> State { get; private set; }

		// Token: 0x06001344 RID: 4932 RVA: 0x000AA158 File Offset: 0x000A8358
		void IServerMessage.Parse(object data)
		{
			IDictionary<string, object> dictionary = data as IDictionary<string, object>;
			this.InvocationId = ulong.Parse(dictionary["I"].ToString());
			object obj;
			if (dictionary.TryGetValue("E", out obj))
			{
				this.ErrorMessage = obj.ToString();
			}
			if (dictionary.TryGetValue("H", out obj))
			{
				this.IsHubError = (int.Parse(obj.ToString()) == 1);
			}
			if (dictionary.TryGetValue("D", out obj))
			{
				this.AdditionalData = (obj as IDictionary<string, object>);
			}
			if (dictionary.TryGetValue("T", out obj))
			{
				this.StackTrace = obj.ToString();
			}
			if (dictionary.TryGetValue("S", out obj))
			{
				this.State = (obj as IDictionary<string, object>);
			}
		}
	}
}
