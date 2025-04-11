using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x0200053B RID: 1339
	public class Ed448KeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x060032F5 RID: 13045 RVA: 0x00135D11 File Offset: 0x00133F11
		public virtual void Init(KeyGenerationParameters parameters)
		{
			this.random = parameters.Random;
		}

		// Token: 0x060032F6 RID: 13046 RVA: 0x00135D20 File Offset: 0x00133F20
		public virtual AsymmetricCipherKeyPair GenerateKeyPair()
		{
			Ed448PrivateKeyParameters ed448PrivateKeyParameters = new Ed448PrivateKeyParameters(this.random);
			return new AsymmetricCipherKeyPair(ed448PrivateKeyParameters.GeneratePublicKey(), ed448PrivateKeyParameters);
		}

		// Token: 0x0400206C RID: 8300
		private SecureRandom random;
	}
}
