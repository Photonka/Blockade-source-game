using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Endo
{
	// Token: 0x0200033A RID: 826
	public interface GlvEndomorphism : ECEndomorphism
	{
		// Token: 0x0600201F RID: 8223
		BigInteger[] DecomposeScalar(BigInteger k);
	}
}
