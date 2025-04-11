using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Ocsp
{
	// Token: 0x020002E7 RID: 743
	public abstract class OcspRespStatus
	{
		// Token: 0x040017CC RID: 6092
		public const int Successful = 0;

		// Token: 0x040017CD RID: 6093
		public const int MalformedRequest = 1;

		// Token: 0x040017CE RID: 6094
		public const int InternalError = 2;

		// Token: 0x040017CF RID: 6095
		public const int TryLater = 3;

		// Token: 0x040017D0 RID: 6096
		public const int SigRequired = 5;

		// Token: 0x040017D1 RID: 6097
		public const int Unauthorized = 6;
	}
}
