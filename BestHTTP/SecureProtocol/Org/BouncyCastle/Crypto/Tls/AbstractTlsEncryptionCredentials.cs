using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003DF RID: 991
	public abstract class AbstractTlsEncryptionCredentials : AbstractTlsCredentials, TlsEncryptionCredentials, TlsCredentials
	{
		// Token: 0x0600288F RID: 10383
		public abstract byte[] DecryptPreMasterSecret(byte[] encryptedPreMasterSecret);
	}
}
