using System;

namespace BestHTTP.ServerSentEvents
{
	// Token: 0x02000218 RID: 536
	public enum States
	{
		// Token: 0x040014B9 RID: 5305
		Initial,
		// Token: 0x040014BA RID: 5306
		Connecting,
		// Token: 0x040014BB RID: 5307
		Open,
		// Token: 0x040014BC RID: 5308
		Retrying,
		// Token: 0x040014BD RID: 5309
		Closing,
		// Token: 0x040014BE RID: 5310
		Closed
	}
}
