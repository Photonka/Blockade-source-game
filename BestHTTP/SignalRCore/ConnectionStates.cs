using System;

namespace BestHTTP.SignalRCore
{
	// Token: 0x020001D0 RID: 464
	public enum ConnectionStates
	{
		// Token: 0x040013CA RID: 5066
		Initial,
		// Token: 0x040013CB RID: 5067
		Authenticating,
		// Token: 0x040013CC RID: 5068
		Negotiating,
		// Token: 0x040013CD RID: 5069
		Redirected,
		// Token: 0x040013CE RID: 5070
		Connected,
		// Token: 0x040013CF RID: 5071
		CloseInitiated,
		// Token: 0x040013D0 RID: 5072
		Closed
	}
}
