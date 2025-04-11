using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003BF RID: 959
	public interface IDerivationFunction
	{
		// Token: 0x060027CC RID: 10188
		void Init(IDerivationParameters parameters);

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060027CD RID: 10189
		IDigest Digest { get; }

		// Token: 0x060027CE RID: 10190
		int GenerateBytes(byte[] output, int outOff, int length);
	}
}
