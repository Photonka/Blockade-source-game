using System;

namespace BestHTTP.SignalR.Authentication
{
	// Token: 0x02000215 RID: 533
	public interface IAuthenticationProvider
	{
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600138F RID: 5007
		bool IsPreAuthRequired { get; }

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06001390 RID: 5008
		// (remove) Token: 0x06001391 RID: 5009
		event OnAuthenticationSuccededDelegate OnAuthenticationSucceded;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06001392 RID: 5010
		// (remove) Token: 0x06001393 RID: 5011
		event OnAuthenticationFailedDelegate OnAuthenticationFailed;

		// Token: 0x06001394 RID: 5012
		void StartAuthentication();

		// Token: 0x06001395 RID: 5013
		void PrepareRequest(HTTPRequest request, RequestTypes type);
	}
}
