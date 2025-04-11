using System;

namespace BestHTTP.SignalRCore
{
	// Token: 0x020001D6 RID: 470
	public sealed class HubOptions
	{
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x000A4910 File Offset: 0x000A2B10
		// (set) Token: 0x0600117F RID: 4479 RVA: 0x000A4918 File Offset: 0x000A2B18
		public bool SkipNegotiation { get; set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06001180 RID: 4480 RVA: 0x000A4921 File Offset: 0x000A2B21
		// (set) Token: 0x06001181 RID: 4481 RVA: 0x000A4929 File Offset: 0x000A2B29
		public TransportTypes PreferedTransport { get; set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x000A4932 File Offset: 0x000A2B32
		// (set) Token: 0x06001183 RID: 4483 RVA: 0x000A493A File Offset: 0x000A2B3A
		public TimeSpan PingInterval { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x000A4943 File Offset: 0x000A2B43
		// (set) Token: 0x06001185 RID: 4485 RVA: 0x000A494B File Offset: 0x000A2B4B
		public int MaxRedirects { get; set; }

		// Token: 0x06001186 RID: 4486 RVA: 0x000A4954 File Offset: 0x000A2B54
		public HubOptions()
		{
			this.SkipNegotiation = false;
			this.PreferedTransport = TransportTypes.WebSocket;
			this.PingInterval = TimeSpan.FromSeconds(15.0);
			this.MaxRedirects = 100;
		}
	}
}
