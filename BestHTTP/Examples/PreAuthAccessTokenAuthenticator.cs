using System;
using BestHTTP.SignalRCore;

namespace BestHTTP.Examples
{
	// Token: 0x0200019B RID: 411
	public sealed class PreAuthAccessTokenAuthenticator : IAuthenticationProvider
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x0006CF70 File Offset: 0x0006B170
		public bool IsPreAuthRequired
		{
			get
			{
				return true;
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000F49 RID: 3913 RVA: 0x0009C7B4 File Offset: 0x0009A9B4
		// (remove) Token: 0x06000F4A RID: 3914 RVA: 0x0009C7EC File Offset: 0x0009A9EC
		public event OnAuthenticationSuccededDelegate OnAuthenticationSucceded;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000F4B RID: 3915 RVA: 0x0009C824 File Offset: 0x0009AA24
		// (remove) Token: 0x06000F4C RID: 3916 RVA: 0x0009C85C File Offset: 0x0009AA5C
		public event OnAuthenticationFailedDelegate OnAuthenticationFailed;

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x0009C891 File Offset: 0x0009AA91
		// (set) Token: 0x06000F4E RID: 3918 RVA: 0x0009C899 File Offset: 0x0009AA99
		public string Token { get; private set; }

		// Token: 0x06000F4F RID: 3919 RVA: 0x0009C8A2 File Offset: 0x0009AAA2
		public PreAuthAccessTokenAuthenticator(Uri authUri)
		{
			this.authenticationUri = authUri;
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0009C8B1 File Offset: 0x0009AAB1
		public void StartAuthentication()
		{
			new HTTPRequest(this.authenticationUri, new OnRequestFinishedDelegate(this.OnAuthenticationRequestFinished)).Send();
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0009C8D0 File Offset: 0x0009AAD0
		private void OnAuthenticationRequestFinished(HTTPRequest req, HTTPResponse resp)
		{
			switch (req.State)
			{
			case HTTPRequestStates.Finished:
				if (!resp.IsSuccess)
				{
					this.AuthenticationFailed(string.Format("Request Finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", resp.StatusCode, resp.Message, resp.DataAsText));
					return;
				}
				this.Token = resp.DataAsText;
				if (this.OnAuthenticationSucceded != null)
				{
					this.OnAuthenticationSucceded(this);
					return;
				}
				break;
			case HTTPRequestStates.Error:
				this.AuthenticationFailed("Request Finished with Error! " + ((req.Exception != null) ? (req.Exception.Message + "\n" + req.Exception.StackTrace) : "No Exception"));
				return;
			case HTTPRequestStates.Aborted:
				this.AuthenticationFailed("Request Aborted!");
				return;
			case HTTPRequestStates.ConnectionTimedOut:
				this.AuthenticationFailed("Connection Timed Out!");
				return;
			case HTTPRequestStates.TimedOut:
				this.AuthenticationFailed("Processing the request Timed Out!");
				break;
			default:
				return;
			}
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0009C9B8 File Offset: 0x0009ABB8
		private void AuthenticationFailed(string reason)
		{
			if (this.OnAuthenticationFailed != null)
			{
				this.OnAuthenticationFailed(this, reason);
			}
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0009C9CF File Offset: 0x0009ABCF
		public void PrepareRequest(HTTPRequest request)
		{
			if (HTTPProtocolFactory.GetProtocolFromUri(request.CurrentUri) == SupportedProtocols.HTTP)
			{
				request.Uri = this.PrepareUri(request.Uri);
			}
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0009C9F4 File Offset: 0x0009ABF4
		public Uri PrepareUri(Uri uri)
		{
			if (!string.IsNullOrEmpty(this.Token))
			{
				string str = string.IsNullOrEmpty(uri.Query) ? "?" : (uri.Query + "&");
				return new UriBuilder(uri.Scheme, uri.Host, uri.Port, uri.AbsolutePath, str + "access_token=" + this.Token).Uri;
			}
			return uri;
		}

		// Token: 0x040012B2 RID: 4786
		private Uri authenticationUri;
	}
}
