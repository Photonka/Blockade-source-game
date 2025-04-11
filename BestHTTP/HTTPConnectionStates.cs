using System;

namespace BestHTTP
{
	// Token: 0x02000170 RID: 368
	internal enum HTTPConnectionStates
	{
		// Token: 0x0400118F RID: 4495
		Initial,
		// Token: 0x04001190 RID: 4496
		Processing,
		// Token: 0x04001191 RID: 4497
		Redirected,
		// Token: 0x04001192 RID: 4498
		Upgraded,
		// Token: 0x04001193 RID: 4499
		WaitForProtocolShutdown,
		// Token: 0x04001194 RID: 4500
		WaitForRecycle,
		// Token: 0x04001195 RID: 4501
		Free,
		// Token: 0x04001196 RID: 4502
		AbortRequested,
		// Token: 0x04001197 RID: 4503
		TimedOut,
		// Token: 0x04001198 RID: 4504
		Closed
	}
}
