using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200046E RID: 1134
	public interface TlsSigner
	{
		// Token: 0x06002CBC RID: 11452
		void Init(TlsContext context);

		// Token: 0x06002CBD RID: 11453
		byte[] GenerateRawSignature(AsymmetricKeyParameter privateKey, byte[] md5AndSha1);

		// Token: 0x06002CBE RID: 11454
		byte[] GenerateRawSignature(SignatureAndHashAlgorithm algorithm, AsymmetricKeyParameter privateKey, byte[] hash);

		// Token: 0x06002CBF RID: 11455
		bool VerifyRawSignature(byte[] sigBytes, AsymmetricKeyParameter publicKey, byte[] md5AndSha1);

		// Token: 0x06002CC0 RID: 11456
		bool VerifyRawSignature(SignatureAndHashAlgorithm algorithm, byte[] sigBytes, AsymmetricKeyParameter publicKey, byte[] hash);

		// Token: 0x06002CC1 RID: 11457
		ISigner CreateSigner(AsymmetricKeyParameter privateKey);

		// Token: 0x06002CC2 RID: 11458
		ISigner CreateSigner(SignatureAndHashAlgorithm algorithm, AsymmetricKeyParameter privateKey);

		// Token: 0x06002CC3 RID: 11459
		ISigner CreateVerifyer(AsymmetricKeyParameter publicKey);

		// Token: 0x06002CC4 RID: 11460
		ISigner CreateVerifyer(SignatureAndHashAlgorithm algorithm, AsymmetricKeyParameter publicKey);

		// Token: 0x06002CC5 RID: 11461
		bool IsValidPublicKey(AsymmetricKeyParameter publicKey);
	}
}
