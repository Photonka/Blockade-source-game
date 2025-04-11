using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Endo
{
	// Token: 0x02000339 RID: 825
	public interface ECEndomorphism
	{
		// Token: 0x170003CC RID: 972
		// (get) Token: 0x0600201D RID: 8221
		ECPointMap PointMap { get; }

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x0600201E RID: 8222
		bool HasEfficientPointMap { get; }
	}
}
