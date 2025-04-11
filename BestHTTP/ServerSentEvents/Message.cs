using System;

namespace BestHTTP.ServerSentEvents
{
	// Token: 0x02000221 RID: 545
	public sealed class Message
	{
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x000ABE94 File Offset: 0x000AA094
		// (set) Token: 0x060013FB RID: 5115 RVA: 0x000ABE9C File Offset: 0x000AA09C
		public string Id { get; internal set; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x000ABEA5 File Offset: 0x000AA0A5
		// (set) Token: 0x060013FD RID: 5117 RVA: 0x000ABEAD File Offset: 0x000AA0AD
		public string Event { get; internal set; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x000ABEB6 File Offset: 0x000AA0B6
		// (set) Token: 0x060013FF RID: 5119 RVA: 0x000ABEBE File Offset: 0x000AA0BE
		public string Data { get; internal set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x000ABEC7 File Offset: 0x000AA0C7
		// (set) Token: 0x06001401 RID: 5121 RVA: 0x000ABECF File Offset: 0x000AA0CF
		public TimeSpan Retry { get; internal set; }

		// Token: 0x06001402 RID: 5122 RVA: 0x000ABED8 File Offset: 0x000AA0D8
		public override string ToString()
		{
			return string.Format("\"{0}\": \"{1}\"", this.Event, this.Data);
		}
	}
}
