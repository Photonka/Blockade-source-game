using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000462 RID: 1122
	public interface TlsPskIdentity
	{
		// Token: 0x06002C5F RID: 11359
		void SkipIdentityHint();

		// Token: 0x06002C60 RID: 11360
		void NotifyIdentityHint(byte[] psk_identity_hint);

		// Token: 0x06002C61 RID: 11361
		byte[] GetPskIdentity();

		// Token: 0x06002C62 RID: 11362
		byte[] GetPsk();
	}
}
