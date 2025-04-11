using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004C0 RID: 1216
	public class Ed25519KeyGenerationParameters : KeyGenerationParameters
	{
		// Token: 0x06002F8F RID: 12175 RVA: 0x00128200 File Offset: 0x00126400
		public Ed25519KeyGenerationParameters(SecureRandom random) : base(random, 256)
		{
		}
	}
}
