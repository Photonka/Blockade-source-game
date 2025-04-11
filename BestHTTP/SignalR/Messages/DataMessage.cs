using System;

namespace BestHTTP.SignalR.Messages
{
	// Token: 0x02000204 RID: 516
	public sealed class DataMessage : IServerMessage
	{
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x0006CF70 File Offset: 0x0006B170
		MessageTypes IServerMessage.Type
		{
			get
			{
				return MessageTypes.Data;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x000A9F30 File Offset: 0x000A8130
		// (set) Token: 0x06001320 RID: 4896 RVA: 0x000A9F38 File Offset: 0x000A8138
		public object Data { get; private set; }

		// Token: 0x06001321 RID: 4897 RVA: 0x000A9F41 File Offset: 0x000A8141
		void IServerMessage.Parse(object data)
		{
			this.Data = data;
		}
	}
}
