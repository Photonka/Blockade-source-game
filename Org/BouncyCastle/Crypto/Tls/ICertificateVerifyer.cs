using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200015F RID: 351
	public interface ICertificateVerifyer
	{
		// Token: 0x06000C81 RID: 3201
		bool IsValid(Uri targetUri, X509CertificateStructure[] certs);
	}
}
