using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x02000532 RID: 1330
	public class DHBasicKeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x060032C5 RID: 12997 RVA: 0x00134E78 File Offset: 0x00133078
		public virtual void Init(KeyGenerationParameters parameters)
		{
			this.param = (DHKeyGenerationParameters)parameters;
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x00134E88 File Offset: 0x00133088
		public virtual AsymmetricCipherKeyPair GenerateKeyPair()
		{
			DHKeyGeneratorHelper instance = DHKeyGeneratorHelper.Instance;
			DHParameters parameters = this.param.Parameters;
			BigInteger x = instance.CalculatePrivate(parameters, this.param.Random);
			return new AsymmetricCipherKeyPair(new DHPublicKeyParameters(instance.CalculatePublic(parameters, x), parameters), new DHPrivateKeyParameters(x, parameters));
		}

		// Token: 0x04002054 RID: 8276
		private DHKeyGenerationParameters param;
	}
}
