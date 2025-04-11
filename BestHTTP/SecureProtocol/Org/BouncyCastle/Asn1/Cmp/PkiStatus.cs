using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007AE RID: 1966
	public enum PkiStatus
	{
		// Token: 0x04002D02 RID: 11522
		Granted,
		// Token: 0x04002D03 RID: 11523
		GrantedWithMods,
		// Token: 0x04002D04 RID: 11524
		Rejection,
		// Token: 0x04002D05 RID: 11525
		Waiting,
		// Token: 0x04002D06 RID: 11526
		RevocationWarning,
		// Token: 0x04002D07 RID: 11527
		RevocationNotification,
		// Token: 0x04002D08 RID: 11528
		KeyUpdateWarning
	}
}
