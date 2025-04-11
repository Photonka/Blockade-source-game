using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x02000534 RID: 1332
	public class DHKeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x060032CC RID: 13004 RVA: 0x00134F8E File Offset: 0x0013318E
		public virtual void Init(KeyGenerationParameters parameters)
		{
			this.param = (DHKeyGenerationParameters)parameters;
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x00134F9C File Offset: 0x0013319C
		public virtual AsymmetricCipherKeyPair GenerateKeyPair()
		{
			DHKeyGeneratorHelper instance = DHKeyGeneratorHelper.Instance;
			DHParameters parameters = this.param.Parameters;
			BigInteger x = instance.CalculatePrivate(parameters, this.param.Random);
			return new AsymmetricCipherKeyPair(new DHPublicKeyParameters(instance.CalculatePublic(parameters, x), parameters), new DHPrivateKeyParameters(x, parameters));
		}

		// Token: 0x04002056 RID: 8278
		private DHKeyGenerationParameters param;
	}
}
