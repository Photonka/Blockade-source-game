using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders
{
	// Token: 0x02000279 RID: 633
	public interface IEncoder
	{
		// Token: 0x0600177A RID: 6010
		int Encode(byte[] data, int off, int length, Stream outStream);

		// Token: 0x0600177B RID: 6011
		int Decode(byte[] data, int off, int length, Stream outStream);

		// Token: 0x0600177C RID: 6012
		int DecodeString(string data, Stream outStream);
	}
}
