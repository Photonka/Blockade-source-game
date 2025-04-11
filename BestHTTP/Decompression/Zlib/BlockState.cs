using System;

namespace BestHTTP.Decompression.Zlib
{
	// Token: 0x020007E2 RID: 2018
	internal enum BlockState
	{
		// Token: 0x04002DE8 RID: 11752
		NeedMore,
		// Token: 0x04002DE9 RID: 11753
		BlockDone,
		// Token: 0x04002DEA RID: 11754
		FinishStarted,
		// Token: 0x04002DEB RID: 11755
		FinishDone
	}
}
