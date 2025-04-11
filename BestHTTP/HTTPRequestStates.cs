using System;

namespace BestHTTP
{
	// Token: 0x02000178 RID: 376
	public enum HTTPRequestStates
	{
		// Token: 0x040011CF RID: 4559
		Initial,
		// Token: 0x040011D0 RID: 4560
		Queued,
		// Token: 0x040011D1 RID: 4561
		Processing,
		// Token: 0x040011D2 RID: 4562
		Finished,
		// Token: 0x040011D3 RID: 4563
		Error,
		// Token: 0x040011D4 RID: 4564
		Aborted,
		// Token: 0x040011D5 RID: 4565
		ConnectionTimedOut,
		// Token: 0x040011D6 RID: 4566
		TimedOut
	}
}
