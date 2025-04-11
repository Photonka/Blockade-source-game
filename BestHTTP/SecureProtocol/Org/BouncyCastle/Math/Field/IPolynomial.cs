using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Field
{
	// Token: 0x02000303 RID: 771
	public interface IPolynomial
	{
		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06001D8D RID: 7565
		int Degree { get; }

		// Token: 0x06001D8E RID: 7566
		int[] GetExponentsPresent();
	}
}
