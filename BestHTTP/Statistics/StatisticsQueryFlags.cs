using System;

namespace BestHTTP.Statistics
{
	// Token: 0x020001B3 RID: 435
	[Flags]
	public enum StatisticsQueryFlags : byte
	{
		// Token: 0x04001338 RID: 4920
		Connections = 1,
		// Token: 0x04001339 RID: 4921
		Cache = 2,
		// Token: 0x0400133A RID: 4922
		Cookies = 4,
		// Token: 0x0400133B RID: 4923
		All = 255
	}
}
