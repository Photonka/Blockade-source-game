using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x0200053A RID: 1338
	public class Ed25519KeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x060032F2 RID: 13042 RVA: 0x00135CDB File Offset: 0x00133EDB
		public virtual void Init(KeyGenerationParameters parameters)
		{
			this.random = parameters.Random;
		}

		// Token: 0x060032F3 RID: 13043 RVA: 0x00135CEC File Offset: 0x00133EEC
		public virtual AsymmetricCipherKeyPair GenerateKeyPair()
		{
			Ed25519PrivateKeyParameters ed25519PrivateKeyParameters = new Ed25519PrivateKeyParameters(this.random);
			return new AsymmetricCipherKeyPair(ed25519PrivateKeyParameters.GeneratePublicKey(), ed25519PrivateKeyParameters);
		}

		// Token: 0x0400206B RID: 8299
		private SecureRandom random;
	}
}
