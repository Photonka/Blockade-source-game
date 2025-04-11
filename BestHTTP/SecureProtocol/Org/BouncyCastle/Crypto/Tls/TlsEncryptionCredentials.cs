using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000454 RID: 1108
	public interface TlsEncryptionCredentials : TlsCredentials
	{
		// Token: 0x06002BB8 RID: 11192
		byte[] DecryptPreMasterSecret(byte[] encryptedPreMasterSecret);
	}
}
