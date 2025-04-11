using System;
using System.Collections.Generic;
using BestHTTP.Extensions;

namespace BestHTTP
{
	// Token: 0x0200016D RID: 365
	internal sealed class KeepAliveHeader
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00092F98 File Offset: 0x00091198
		// (set) Token: 0x06000D0D RID: 3341 RVA: 0x00092FA0 File Offset: 0x000911A0
		public TimeSpan TimeOut { get; private set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x00092FA9 File Offset: 0x000911A9
		// (set) Token: 0x06000D0F RID: 3343 RVA: 0x00092FB1 File Offset: 0x000911B1
		public int MaxRequests { get; private set; }

		// Token: 0x06000D10 RID: 3344 RVA: 0x00092FBC File Offset: 0x000911BC
		public void Parse(List<string> headerValues)
		{
			HeaderParser headerParser = new HeaderParser(headerValues[0]);
			HeaderValue headerValue;
			if (headerParser.TryGet("timeout", out headerValue) && headerValue.HasValue)
			{
				int num = 0;
				if (int.TryParse(headerValue.Value, out num))
				{
					this.TimeOut = TimeSpan.FromSeconds((double)num);
				}
				else
				{
					this.TimeOut = TimeSpan.MaxValue;
				}
			}
			if (headerParser.TryGet("max", out headerValue) && headerValue.HasValue)
			{
				int maxRequests = 0;
				if (int.TryParse("max", out maxRequests))
				{
					this.MaxRequests = maxRequests;
					return;
				}
				this.MaxRequests = int.MaxValue;
			}
		}
	}
}
