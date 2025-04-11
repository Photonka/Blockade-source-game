using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003C3 RID: 963
	public interface IDsaExt : IDsa
	{
		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x060027DA RID: 10202
		BigInteger Order { get; }
	}
}
