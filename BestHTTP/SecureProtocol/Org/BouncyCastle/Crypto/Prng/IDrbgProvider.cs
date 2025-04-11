using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng.Drbg;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x0200049B RID: 1179
	internal interface IDrbgProvider
	{
		// Token: 0x06002E91 RID: 11921
		ISP80090Drbg Get(IEntropySource entropySource);
	}
}
