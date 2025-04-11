using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes.Gcm
{
	// Token: 0x02000515 RID: 1301
	public interface IGcmMultiplier
	{
		// Token: 0x060031BC RID: 12732
		void Init(byte[] H);

		// Token: 0x060031BD RID: 12733
		void MultiplyH(byte[] x);
	}
}
