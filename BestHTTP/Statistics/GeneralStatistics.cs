using System;

namespace BestHTTP.Statistics
{
	// Token: 0x020001B4 RID: 436
	public struct GeneralStatistics
	{
		// Token: 0x0400133C RID: 4924
		public StatisticsQueryFlags QueryFlags;

		// Token: 0x0400133D RID: 4925
		public int Connections;

		// Token: 0x0400133E RID: 4926
		public int ActiveConnections;

		// Token: 0x0400133F RID: 4927
		public int FreeConnections;

		// Token: 0x04001340 RID: 4928
		public int RecycledConnections;

		// Token: 0x04001341 RID: 4929
		public int RequestsInQueue;

		// Token: 0x04001342 RID: 4930
		public int CacheEntityCount;

		// Token: 0x04001343 RID: 4931
		public ulong CacheSize;

		// Token: 0x04001344 RID: 4932
		public int CookieCount;

		// Token: 0x04001345 RID: 4933
		public uint CookieJarSize;
	}
}
