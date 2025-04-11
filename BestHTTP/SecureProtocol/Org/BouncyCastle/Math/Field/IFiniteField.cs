using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Field
{
	// Token: 0x02000302 RID: 770
	public interface IFiniteField
	{
		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001D8B RID: 7563
		BigInteger Characteristic { get; }

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001D8C RID: 7564
		int Dimension { get; }
	}
}
