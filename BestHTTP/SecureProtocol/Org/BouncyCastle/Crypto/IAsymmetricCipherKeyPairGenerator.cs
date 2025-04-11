using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003B9 RID: 953
	public interface IAsymmetricCipherKeyPairGenerator
	{
		// Token: 0x060027AD RID: 10157
		void Init(KeyGenerationParameters parameters);

		// Token: 0x060027AE RID: 10158
		AsymmetricCipherKeyPair GenerateKeyPair();
	}
}
