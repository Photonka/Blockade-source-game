using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng
{
	// Token: 0x0200049C RID: 1180
	public interface IRandomGenerator
	{
		// Token: 0x06002E92 RID: 11922
		void AddSeedMaterial(byte[] seed);

		// Token: 0x06002E93 RID: 11923
		void AddSeedMaterial(long seed);

		// Token: 0x06002E94 RID: 11924
		void NextBytes(byte[] bytes);

		// Token: 0x06002E95 RID: 11925
		void NextBytes(byte[] bytes, int start, int len);
	}
}
