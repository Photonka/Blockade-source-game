using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000446 RID: 1094
	public interface TlsCompression
	{
		// Token: 0x06002B2B RID: 11051
		Stream Compress(Stream output);

		// Token: 0x06002B2C RID: 11052
		Stream Decompress(Stream output);
	}
}
