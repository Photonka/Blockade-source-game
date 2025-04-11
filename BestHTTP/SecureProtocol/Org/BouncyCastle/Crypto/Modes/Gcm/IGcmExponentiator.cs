using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes.Gcm
{
	// Token: 0x02000514 RID: 1300
	public interface IGcmExponentiator
	{
		// Token: 0x060031BA RID: 12730
		void Init(byte[] x);

		// Token: 0x060031BB RID: 12731
		void ExponentiateX(long pow, byte[] output);
	}
}
