using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004ED RID: 1261
	public class X25519KeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x06003061 RID: 12385 RVA: 0x00129A28 File Offset: 0x00127C28
		public X25519KeyGenerationParameters(SecureRandom random) : base(random, 255)
		{
		}
	}
}
