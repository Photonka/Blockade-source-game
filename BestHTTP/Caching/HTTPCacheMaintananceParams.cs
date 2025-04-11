using System;

namespace BestHTTP.Caching
{
	// Token: 0x020007FF RID: 2047
	public sealed class HTTPCacheMaintananceParams
	{
		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06004956 RID: 18774 RVA: 0x001A4B82 File Offset: 0x001A2D82
		// (set) Token: 0x06004957 RID: 18775 RVA: 0x001A4B8A File Offset: 0x001A2D8A
		public TimeSpan DeleteOlder { get; private set; }

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06004958 RID: 18776 RVA: 0x001A4B93 File Offset: 0x001A2D93
		// (set) Token: 0x06004959 RID: 18777 RVA: 0x001A4B9B File Offset: 0x001A2D9B
		public ulong MaxCacheSize { get; private set; }

		// Token: 0x0600495A RID: 18778 RVA: 0x001A4BA4 File Offset: 0x001A2DA4
		public HTTPCacheMaintananceParams(TimeSpan deleteOlder, ulong maxCacheSize)
		{
			this.DeleteOlder = deleteOlder;
			this.MaxCacheSize = maxCacheSize;
		}
	}
}
