using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x0200053C RID: 1340
	public class ElGamalKeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x060032F8 RID: 13048 RVA: 0x00135D45 File Offset: 0x00133F45
		public void Init(KeyGenerationParameters parameters)
		{
			this.param = (ElGamalKeyGenerationParameters)parameters;
		}

		// Token: 0x060032F9 RID: 13049 RVA: 0x00135D54 File Offset: 0x00133F54
		public AsymmetricCipherKeyPair GenerateKeyPair()
		{
			DHKeyGeneratorHelper instance = DHKeyGeneratorHelper.Instance;
			ElGamalParameters parameters = this.param.Parameters;
			DHParameters dhParams = new DHParameters(parameters.P, parameters.G, null, 0, parameters.L);
			BigInteger x = instance.CalculatePrivate(dhParams, this.param.Random);
			return new AsymmetricCipherKeyPair(new ElGamalPublicKeyParameters(instance.CalculatePublic(dhParams, x), parameters), new ElGamalPrivateKeyParameters(x, parameters));
		}

		// Token: 0x0400206D RID: 8301
		private ElGamalKeyGenerationParameters param;
	}
}
