using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004C3 RID: 1219
	public class Ed448KeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x06002F9D RID: 12189 RVA: 0x0012844D File Offset: 0x0012664D
		public Ed448KeyGenerationParameters(SecureRandom random) : base(random, 448)
		{
		}
	}
}
