using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200057A RID: 1402
	public class SeedWrapEngine : Rfc3394WrapEngine
	{
		// Token: 0x0600356A RID: 13674 RVA: 0x0014AB0C File Offset: 0x00148D0C
		public SeedWrapEngine() : base(new SeedEngine())
		{
		}
	}
}
