using System;
using System.IO;
using BestHTTP.Authentication;

namespace BestHTTP
{
	// Token: 0x02000175 RID: 373
	public abstract class Proxy
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x000950B6 File Offset: 0x000932B6
		// (set) Token: 0x06000D5D RID: 3421 RVA: 0x000950BE File Offset: 0x000932BE
		public Uri Address { get; set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x000950C7 File Offset: 0x000932C7
		// (set) Token: 0x06000D5F RID: 3423 RVA: 0x000950CF File Offset: 0x000932CF
		public Credentials Credentials { get; set; }

		// Token: 0x06000D60 RID: 3424 RVA: 0x000950D8 File Offset: 0x000932D8
		internal Proxy(Uri address, Credentials credentials)
		{
			this.Address = address;
			this.Credentials = credentials;
		}

		// Token: 0x06000D61 RID: 3425
		internal abstract void Connect(Stream stream, HTTPRequest request);

		// Token: 0x06000D62 RID: 3426
		internal abstract string GetRequestPath(Uri uri);
	}
}
