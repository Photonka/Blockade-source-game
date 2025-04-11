using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x02000325 RID: 805
	public interface ECMultiplier
	{
		// Token: 0x06001FD0 RID: 8144
		ECPoint Multiply(ECPoint p, BigInteger k);
	}
}
