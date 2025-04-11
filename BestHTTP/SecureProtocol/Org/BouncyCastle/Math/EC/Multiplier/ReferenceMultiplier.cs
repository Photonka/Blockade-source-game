using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x02000330 RID: 816
	public class ReferenceMultiplier : AbstractECMultiplier
	{
		// Token: 0x06001FEC RID: 8172 RVA: 0x000F119A File Offset: 0x000EF39A
		protected override ECPoint MultiplyPositive(ECPoint p, BigInteger k)
		{
			return ECAlgorithms.ReferenceMultiply(p, k);
		}
	}
}
