using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003D3 RID: 979
	public interface IXof : IDigest
	{
		// Token: 0x0600280E RID: 10254
		int DoFinal(byte[] output, int outOff, int outLen);

		// Token: 0x0600280F RID: 10255
		int DoOutput(byte[] output, int outOff, int outLen);
	}
}
