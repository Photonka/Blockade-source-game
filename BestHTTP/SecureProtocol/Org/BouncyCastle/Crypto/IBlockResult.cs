using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003BC RID: 956
	public interface IBlockResult
	{
		// Token: 0x060027B8 RID: 10168
		byte[] Collect();

		// Token: 0x060027B9 RID: 10169
		int Collect(byte[] destination, int offset);
	}
}
