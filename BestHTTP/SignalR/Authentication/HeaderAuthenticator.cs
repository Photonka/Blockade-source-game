using System;

namespace BestHTTP.SignalR.Authentication
{
	// Token: 0x02000217 RID: 535
	internal class HeaderAuthenticator : IAuthenticationProvider
	{
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x000AADAE File Offset: 0x000A8FAE
		// (set) Token: 0x060013A9 RID: 5033 RVA: 0x000AADB6 File Offset: 0x000A8FB6
		public string User { get; private set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x000AADBF File Offset: 0x000A8FBF
		// (set) Token: 0x060013AB RID: 5035 RVA: 0x000AADC7 File Offset: 0x000A8FC7
		public string Roles { get; private set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public bool IsPreAuthRequired
		{
			get
			{
				return false;
			}
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x060013AD RID: 5037 RVA: 0x000AADD0 File Offset: 0x000A8FD0
		// (remove) Token: 0x060013AE RID: 5038 RVA: 0x000AAE08 File Offset: 0x000A9008
		public event OnAuthenticationSuccededDelegate OnAuthenticationSucceded;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x060013AF RID: 5039 RVA: 0x000AAE40 File Offset: 0x000A9040
		// (remove) Token: 0x060013B0 RID: 5040 RVA: 0x000AAE78 File Offset: 0x000A9078
		public event OnAuthenticationFailedDelegate OnAuthenticationFailed;

		// Token: 0x060013B1 RID: 5041 RVA: 0x000AAEAD File Offset: 0x000A90AD
		public HeaderAuthenticator(string user, string roles)
		{
			this.User = user;
			this.Roles = roles;
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x00002B75 File Offset: 0x00000D75
		public void StartAuthentication()
		{
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x000AAEC3 File Offset: 0x000A90C3
		public void PrepareRequest(HTTPRequest request, RequestTypes type)
		{
			request.SetHeader("username", this.User);
			request.SetHeader("roles", this.Roles);
		}
	}
}
