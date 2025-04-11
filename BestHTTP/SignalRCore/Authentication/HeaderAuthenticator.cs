using System;

namespace BestHTTP.SignalRCore.Authentication
{
	// Token: 0x020001E9 RID: 489
	public sealed class HeaderAuthenticator : IAuthenticationProvider
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public bool IsPreAuthRequired
		{
			get
			{
				return false;
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x0600122A RID: 4650 RVA: 0x000A69E4 File Offset: 0x000A4BE4
		// (remove) Token: 0x0600122B RID: 4651 RVA: 0x000A6A1C File Offset: 0x000A4C1C
		public event OnAuthenticationSuccededDelegate OnAuthenticationSucceded;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x0600122C RID: 4652 RVA: 0x000A6A54 File Offset: 0x000A4C54
		// (remove) Token: 0x0600122D RID: 4653 RVA: 0x000A6A8C File Offset: 0x000A4C8C
		public event OnAuthenticationFailedDelegate OnAuthenticationFailed;

		// Token: 0x0600122E RID: 4654 RVA: 0x000A6AC1 File Offset: 0x000A4CC1
		public HeaderAuthenticator(string credentials)
		{
			this._credentials = credentials;
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00002B75 File Offset: 0x00000D75
		public void StartAuthentication()
		{
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x000A6AD0 File Offset: 0x000A4CD0
		public void PrepareRequest(HTTPRequest request)
		{
			request.SetHeader("Authorization", "Bearer " + this._credentials);
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x000A6AED File Offset: 0x000A4CED
		public Uri PrepareUri(Uri uri)
		{
			return uri;
		}

		// Token: 0x0400141B RID: 5147
		private string _credentials;
	}
}
