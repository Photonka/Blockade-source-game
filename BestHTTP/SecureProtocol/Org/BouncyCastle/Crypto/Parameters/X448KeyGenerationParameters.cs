using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004F0 RID: 1264
	public class X448KeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x0600306F RID: 12399 RVA: 0x0012844D File Offset: 0x0012664D
		public X448KeyGenerationParameters(SecureRandom random) : base(random, 448)
		{
		}
	}
}
