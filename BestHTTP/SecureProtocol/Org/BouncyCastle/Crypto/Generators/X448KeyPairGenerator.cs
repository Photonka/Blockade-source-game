using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x0200054F RID: 1359
	public class X448KeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x06003361 RID: 13153 RVA: 0x00138A65 File Offset: 0x00136C65
		public virtual void Init(KeyGenerationParameters parameters)
		{
			this.random = parameters.Random;
		}

		// Token: 0x06003362 RID: 13154 RVA: 0x00138A74 File Offset: 0x00136C74
		public virtual AsymmetricCipherKeyPair GenerateKeyPair()
		{
			X448PrivateKeyParameters x448PrivateKeyParameters = new X448PrivateKeyParameters(this.random);
			return new AsymmetricCipherKeyPair(x448PrivateKeyParameters.GeneratePublicKey(), x448PrivateKeyParameters);
		}

		// Token: 0x04002099 RID: 8345
		private SecureRandom random;
	}
}
