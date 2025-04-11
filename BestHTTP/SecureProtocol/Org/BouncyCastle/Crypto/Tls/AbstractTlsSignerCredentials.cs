using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003E4 RID: 996
	public abstract class AbstractTlsSignerCredentials : AbstractTlsCredentials, TlsSignerCredentials, TlsCredentials
	{
		// Token: 0x060028D4 RID: 10452
		public abstract byte[] GenerateCertificateSignature(byte[] hash);

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060028D5 RID: 10453 RVA: 0x0010F472 File Offset: 0x0010D672
		public virtual SignatureAndHashAlgorithm SignatureAndHashAlgorithm
		{
			get
			{
				throw new InvalidOperationException("TlsSignerCredentials implementation does not support (D)TLS 1.2+");
			}
		}
	}
}
