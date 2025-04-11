using System;
using BestHTTP.Cookies;

namespace BestHTTP.SignalR.Authentication
{
	// Token: 0x02000216 RID: 534
	public sealed class SampleCookieAuthentication : IAuthenticationProvider
	{
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x000AAA0C File Offset: 0x000A8C0C
		// (set) Token: 0x06001397 RID: 5015 RVA: 0x000AAA14 File Offset: 0x000A8C14
		public Uri AuthUri { get; private set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06001398 RID: 5016 RVA: 0x000AAA1D File Offset: 0x000A8C1D
		// (set) Token: 0x06001399 RID: 5017 RVA: 0x000AAA25 File Offset: 0x000A8C25
		public string UserName { get; private set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x000AAA2E File Offset: 0x000A8C2E
		// (set) Token: 0x0600139B RID: 5019 RVA: 0x000AAA36 File Offset: 0x000A8C36
		public string Password { get; private set; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x000AAA3F File Offset: 0x000A8C3F
		// (set) Token: 0x0600139D RID: 5021 RVA: 0x000AAA47 File Offset: 0x000A8C47
		public string UserRoles { get; private set; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x000AAA50 File Offset: 0x000A8C50
		// (set) Token: 0x0600139F RID: 5023 RVA: 0x000AAA58 File Offset: 0x000A8C58
		public bool IsPreAuthRequired { get; private set; }

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x060013A0 RID: 5024 RVA: 0x000AAA64 File Offset: 0x000A8C64
		// (remove) Token: 0x060013A1 RID: 5025 RVA: 0x000AAA9C File Offset: 0x000A8C9C
		public event OnAuthenticationSuccededDelegate OnAuthenticationSucceded;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x060013A2 RID: 5026 RVA: 0x000AAAD4 File Offset: 0x000A8CD4
		// (remove) Token: 0x060013A3 RID: 5027 RVA: 0x000AAB0C File Offset: 0x000A8D0C
		public event OnAuthenticationFailedDelegate OnAuthenticationFailed;

		// Token: 0x060013A4 RID: 5028 RVA: 0x000AAB41 File Offset: 0x000A8D41
		public SampleCookieAuthentication(Uri authUri, string user, string passwd, string roles)
		{
			this.AuthUri = authUri;
			this.UserName = user;
			this.Password = passwd;
			this.UserRoles = roles;
			this.IsPreAuthRequired = true;
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x000AAB70 File Offset: 0x000A8D70
		public void StartAuthentication()
		{
			this.AuthRequest = new HTTPRequest(this.AuthUri, HTTPMethods.Post, new OnRequestFinishedDelegate(this.OnAuthRequestFinished));
			this.AuthRequest.AddField("userName", this.UserName);
			this.AuthRequest.AddField("Password", this.Password);
			this.AuthRequest.AddField("roles", this.UserRoles);
			this.AuthRequest.Send();
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x000AABE9 File Offset: 0x000A8DE9
		public void PrepareRequest(HTTPRequest request, RequestTypes type)
		{
			request.Cookies.Add(this.Cookie);
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x000AABFC File Offset: 0x000A8DFC
		private void OnAuthRequestFinished(HTTPRequest req, HTTPResponse resp)
		{
			this.AuthRequest = null;
			string reason = string.Empty;
			switch (req.State)
			{
			case HTTPRequestStates.Finished:
				if (resp.IsSuccess)
				{
					Cookie cookie;
					if (resp.Cookies == null)
					{
						cookie = null;
					}
					else
					{
						cookie = resp.Cookies.Find((Cookie c) => c.Name.Equals(".ASPXAUTH"));
					}
					this.Cookie = cookie;
					if (this.Cookie != null)
					{
						HTTPManager.Logger.Information("CookieAuthentication", "Auth. Cookie found!");
						if (this.OnAuthenticationSucceded != null)
						{
							this.OnAuthenticationSucceded(this);
						}
						return;
					}
					HTTPManager.Logger.Warning("CookieAuthentication", reason = "Auth. Cookie NOT found!");
				}
				else
				{
					HTTPManager.Logger.Warning("CookieAuthentication", reason = string.Format("Request Finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", resp.StatusCode, resp.Message, resp.DataAsText));
				}
				break;
			case HTTPRequestStates.Error:
				HTTPManager.Logger.Warning("CookieAuthentication", reason = "Request Finished with Error! " + ((req.Exception != null) ? (req.Exception.Message + "\n" + req.Exception.StackTrace) : "No Exception"));
				break;
			case HTTPRequestStates.Aborted:
				HTTPManager.Logger.Warning("CookieAuthentication", reason = "Request Aborted!");
				break;
			case HTTPRequestStates.ConnectionTimedOut:
				HTTPManager.Logger.Error("CookieAuthentication", reason = "Connection Timed Out!");
				break;
			case HTTPRequestStates.TimedOut:
				HTTPManager.Logger.Error("CookieAuthentication", reason = "Processing the request Timed Out!");
				break;
			}
			if (this.OnAuthenticationFailed != null)
			{
				this.OnAuthenticationFailed(this, reason);
			}
		}

		// Token: 0x040014B2 RID: 5298
		private HTTPRequest AuthRequest;

		// Token: 0x040014B3 RID: 5299
		private Cookie Cookie;
	}
}
