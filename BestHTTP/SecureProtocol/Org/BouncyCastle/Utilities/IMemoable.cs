using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities
{
	// Token: 0x0200024D RID: 589
	public interface IMemoable
	{
		// Token: 0x060015F6 RID: 5622
		IMemoable Copy();

		// Token: 0x060015F7 RID: 5623
		void Reset(IMemoable other);
	}
}
