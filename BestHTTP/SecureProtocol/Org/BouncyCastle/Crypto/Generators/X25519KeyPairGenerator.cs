using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x0200054E RID: 1358
	public class X25519KeyPairGenerator : IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x0600335E RID: 13150 RVA: 0x00138A2F File Offset: 0x00136C2F
		public virtual void Init(KeyGenerationParameters parameters)
		{
			this.random = parameters.Random;
		}

		// Token: 0x0600335F RID: 13151 RVA: 0x00138A40 File Offset: 0x00136C40
		public virtual AsymmetricCipherKeyPair GenerateKeyPair()
		{
			X25519PrivateKeyParameters x25519PrivateKeyParameters = new X25519PrivateKeyParameters(this.random);
			return new AsymmetricCipherKeyPair(x25519PrivateKeyParameters.GeneratePublicKey(), x25519PrivateKeyParameters);
		}

		// Token: 0x04002098 RID: 8344
		private SecureRandom random;
	}
}
