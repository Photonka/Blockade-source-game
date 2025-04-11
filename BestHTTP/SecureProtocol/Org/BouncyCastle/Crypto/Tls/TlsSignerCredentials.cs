using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200046F RID: 1135
	public interface TlsSignerCredentials : TlsCredentials
	{
		// Token: 0x06002CC6 RID: 11462
		byte[] GenerateCertificateSignature(byte[] hash);

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06002CC7 RID: 11463
		SignatureAndHashAlgorithm SignatureAndHashAlgorithm { get; }
	}
}
