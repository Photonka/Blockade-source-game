using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200015E RID: 350
	public class AlwaysValidVerifyer : ICertificateVerifyer
	{
		// Token: 0x06000C7F RID: 3199 RVA: 0x0006CF70 File Offset: 0x0006B170
		public bool IsValid(Uri targetUri, X509CertificateStructure[] certs)
		{
			return true;
		}
	}
}
