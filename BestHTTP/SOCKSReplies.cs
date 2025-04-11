using System;

namespace BestHTTP
{
	// Token: 0x02000185 RID: 389
	internal enum SOCKSReplies : byte
	{
		// Token: 0x04001241 RID: 4673
		Succeeded,
		// Token: 0x04001242 RID: 4674
		GeneralSOCKSServerFailure,
		// Token: 0x04001243 RID: 4675
		ConnectionNotAllowedByRuleset,
		// Token: 0x04001244 RID: 4676
		NetworkUnreachable,
		// Token: 0x04001245 RID: 4677
		HostUnreachable,
		// Token: 0x04001246 RID: 4678
		ConnectionRefused,
		// Token: 0x04001247 RID: 4679
		TTLExpired,
		// Token: 0x04001248 RID: 4680
		CommandNotSupported,
		// Token: 0x04001249 RID: 4681
		AddressTypeNotSupported
	}
}
