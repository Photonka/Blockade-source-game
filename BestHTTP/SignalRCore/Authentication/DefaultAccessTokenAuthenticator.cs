using System;

namespace BestHTTP.SignalRCore.Authentication
{
	// Token: 0x020001E8 RID: 488
	public sealed class DefaultAccessTokenAuthenticator : IAuthenticationProvider
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public bool IsPreAuthRequired
		{
			get
			{
				return false;
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06001221 RID: 4641 RVA: 0x000A683C File Offset: 0x000A4A3C
		// (remove) Token: 0x06001222 RID: 4642 RVA: 0x000A6874 File Offset: 0x000A4A74
		public event OnAuthenticationSuccededDelegate OnAuthenticationSucceded;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06001223 RID: 4643 RVA: 0x000A68AC File Offset: 0x000A4AAC
		// (remove) Token: 0x06001224 RID: 4644 RVA: 0x000A68E4 File Offset: 0x000A4AE4
		public event OnAuthenticationFailedDelegate OnAuthenticationFailed;

		// Token: 0x06001225 RID: 4645 RVA: 0x000A6919 File Offset: 0x000A4B19
		public DefaultAccessTokenAuthenticator(HubConnection connection)
		{
			this._connection = connection;
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00002B75 File Offset: 0x00000D75
		public void StartAuthentication()
		{
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x000A6928 File Offset: 0x000A4B28
		public void PrepareRequest(HTTPRequest request)
		{
			if (HTTPProtocolFactory.GetProtocolFromUri(request.CurrentUri) == SupportedProtocols.HTTP)
			{
				request.Uri = this.PrepareUri(request.Uri);
			}
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x000A694C File Offset: 0x000A4B4C
		public Uri PrepareUri(Uri uri)
		{
			if (this._connection.NegotiationResult != null && !string.IsNullOrEmpty(this._connection.NegotiationResult.AccessToken))
			{
				string str = string.IsNullOrEmpty(uri.Query) ? "?" : (uri.Query + "&");
				return new UriBuilder(uri.Scheme, uri.Host, uri.Port, uri.AbsolutePath, str + "access_token=" + this._connection.NegotiationResult.AccessToken).Uri;
			}
			return uri;
		}

		// Token: 0x04001418 RID: 5144
		private HubConnection _connection;
	}
}
