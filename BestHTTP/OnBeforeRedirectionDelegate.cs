using System;

namespace BestHTTP
{
	// Token: 0x0200017C RID: 380
	// (Invoke) Token: 0x06000D89 RID: 3465
	public delegate bool OnBeforeRedirectionDelegate(HTTPRequest originalRequest, HTTPResponse response, Uri redirectUri);
}
