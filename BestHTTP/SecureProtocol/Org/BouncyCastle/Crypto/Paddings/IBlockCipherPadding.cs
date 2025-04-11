using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Paddings
{
	// Token: 0x020004F3 RID: 1267
	public interface IBlockCipherPadding
	{
		// Token: 0x0600307D RID: 12413
		void Init(SecureRandom random);

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x0600307E RID: 12414
		string PaddingName { get; }

		// Token: 0x0600307F RID: 12415
		int AddPadding(byte[] input, int inOff);

		// Token: 0x06003080 RID: 12416
		int PadCount(byte[] input);
	}
}
