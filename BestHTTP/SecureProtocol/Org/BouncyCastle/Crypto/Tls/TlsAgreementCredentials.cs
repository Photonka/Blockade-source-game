using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200043D RID: 1085
	public interface TlsAgreementCredentials : TlsCredentials
	{
		// Token: 0x06002AF6 RID: 10998
		byte[] GenerateAgreement(AsymmetricKeyParameter peerPublicKey);
	}
}
