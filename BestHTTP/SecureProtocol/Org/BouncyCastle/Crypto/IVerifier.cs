using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003CF RID: 975
	public interface IVerifier
	{
		// Token: 0x06002805 RID: 10245
		bool IsVerified(byte[] data);

		// Token: 0x06002806 RID: 10246
		bool IsVerified(byte[] source, int off, int length);
	}
}
