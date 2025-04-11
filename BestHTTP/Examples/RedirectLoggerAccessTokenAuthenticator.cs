using System;
using BestHTTP.SignalRCore;

namespace BestHTTP.Examples
{
	// Token: 0x0200019D RID: 413
	public sealed class RedirectLoggerAccessTokenAuthenticator : IAuthenticationProvider
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public bool IsPreAuthRequired
		{
			get
			{
				return false;
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000F61 RID: 3937 RVA: 0x0009CC84 File Offset: 0x0009AE84
		// (remove) Token: 0x06000F62 RID: 3938 RVA: 0x0009CCBC File Offset: 0x0009AEBC
		public event OnAuthenticationSuccededDelegate OnAuthenticationSucceded;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000F63 RID: 3939 RVA: 0x0009CCF4 File Offset: 0x0009AEF4
		// (remove) Token: 0x06000F64 RID: 3940 RVA: 0x0009CD2C File Offset: 0x0009AF2C
		public event OnAuthenticationFailedDelegate OnAuthenticationFailed;

		// Token: 0x06000F65 RID: 3941 RVA: 0x0009CD61 File Offset: 0x0009AF61
		public RedirectLoggerAccessTokenAuthenticator(HubConnection connection)
		{
			this._connection = connection;
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x00002B75 File Offset: 0x00000D75
		public void StartAuthentication()
		{
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0009CD70 File Offset: 0x0009AF70
		public void PrepareRequest(HTTPRequest request)
		{
			request.SetHeader("x-redirect-count", this._connection.RedirectCount.ToString());
			if (HTTPProtocolFactory.GetProtocolFromUri(request.CurrentUri) == SupportedProtocols.HTTP)
			{
				request.Uri = this.PrepareUri(request.Uri);
			}
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0009CDBC File Offset: 0x0009AFBC
		public Uri PrepareUri(Uri uri)
		{
			if (this._connection.NegotiationResult != null && !string.IsNullOrEmpty(this._connection.NegotiationResult.AccessToken))
			{
				string str = string.IsNullOrEmpty(uri.Query) ? "?" : (uri.Query + "&");
				return new UriBuilder(uri.Scheme, uri.Host, uri.Port, uri.AbsolutePath, str + "access_token=" + this._connection.NegotiationResult.AccessToken).Uri;
			}
			return uri;
		}

		// Token: 0x040012BA RID: 4794
		private HubConnection _connection;
	}
}
