using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200045A RID: 1114
	public interface TlsKeyExchange
	{
		// Token: 0x06002BF6 RID: 11254
		void Init(TlsContext context);

		// Token: 0x06002BF7 RID: 11255
		void SkipServerCredentials();

		// Token: 0x06002BF8 RID: 11256
		void ProcessServerCredentials(TlsCredentials serverCredentials);

		// Token: 0x06002BF9 RID: 11257
		void ProcessServerCertificate(Certificate serverCertificate);

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06002BFA RID: 11258
		bool RequiresServerKeyExchange { get; }

		// Token: 0x06002BFB RID: 11259
		byte[] GenerateServerKeyExchange();

		// Token: 0x06002BFC RID: 11260
		void SkipServerKeyExchange();

		// Token: 0x06002BFD RID: 11261
		void ProcessServerKeyExchange(Stream input);

		// Token: 0x06002BFE RID: 11262
		void ValidateCertificateRequest(CertificateRequest certificateRequest);

		// Token: 0x06002BFF RID: 11263
		void SkipClientCredentials();

		// Token: 0x06002C00 RID: 11264
		void ProcessClientCredentials(TlsCredentials clientCredentials);

		// Token: 0x06002C01 RID: 11265
		void ProcessClientCertificate(Certificate clientCertificate);

		// Token: 0x06002C02 RID: 11266
		void GenerateClientKeyExchange(Stream output);

		// Token: 0x06002C03 RID: 11267
		void ProcessClientKeyExchange(Stream input);

		// Token: 0x06002C04 RID: 11268
		byte[] GeneratePremasterSecret();
	}
}
