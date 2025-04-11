using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003DA RID: 986
	public abstract class AbstractTlsAgreementCredentials : AbstractTlsCredentials, TlsAgreementCredentials, TlsCredentials
	{
		// Token: 0x0600285E RID: 10334
		public abstract byte[] GenerateAgreement(AsymmetricKeyParameter peerPublicKey);
	}
}
