using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003DB RID: 987
	public class AbstractTlsCipherFactory : TlsCipherFactory
	{
		// Token: 0x06002860 RID: 10336 RVA: 0x0010EA5F File Offset: 0x0010CC5F
		public virtual TlsCipher CreateCipher(TlsContext context, int encryptionAlgorithm, int macAlgorithm)
		{
			throw new TlsFatalAlert(80);
		}
	}
}
