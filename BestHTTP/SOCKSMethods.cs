using System;

namespace BestHTTP
{
	// Token: 0x02000184 RID: 388
	internal enum SOCKSMethods : byte
	{
		// Token: 0x0400123C RID: 4668
		NoAuthenticationRequired,
		// Token: 0x0400123D RID: 4669
		GSSAPI,
		// Token: 0x0400123E RID: 4670
		UsernameAndPassword,
		// Token: 0x0400123F RID: 4671
		NoAcceptableMethods = 255
	}
}
