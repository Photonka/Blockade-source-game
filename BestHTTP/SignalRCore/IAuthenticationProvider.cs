using System;

namespace BestHTTP.SignalRCore
{
	// Token: 0x020001DA RID: 474
	public interface IAuthenticationProvider
	{
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060011C4 RID: 4548
		bool IsPreAuthRequired { get; }

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060011C5 RID: 4549
		// (remove) Token: 0x060011C6 RID: 4550
		event OnAuthenticationSuccededDelegate OnAuthenticationSucceded;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060011C7 RID: 4551
		// (remove) Token: 0x060011C8 RID: 4552
		event OnAuthenticationFailedDelegate OnAuthenticationFailed;

		// Token: 0x060011C9 RID: 4553
		void StartAuthentication();

		// Token: 0x060011CA RID: 4554
		void PrepareRequest(HTTPRequest request);

		// Token: 0x060011CB RID: 4555
		Uri PrepareUri(Uri uri);
	}
}
