using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC
{
	// Token: 0x02000311 RID: 785
	public interface ECLookupTable
	{
		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001E5F RID: 7775
		int Size { get; }

		// Token: 0x06001E60 RID: 7776
		ECPoint Lookup(int index);
	}
}
