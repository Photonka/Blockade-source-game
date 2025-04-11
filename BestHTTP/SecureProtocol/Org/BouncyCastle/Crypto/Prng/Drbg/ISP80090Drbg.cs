using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Prng.Drbg
{
	// Token: 0x020004A9 RID: 1193
	public interface ISP80090Drbg
	{
		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06002EEF RID: 12015
		int BlockSize { get; }

		// Token: 0x06002EF0 RID: 12016
		int Generate(byte[] output, byte[] additionalInput, bool predictionResistant);

		// Token: 0x06002EF1 RID: 12017
		void Reseed(byte[] additionalInput);
	}
}
