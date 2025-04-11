using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200045E RID: 1118
	public class TlsNullCompression : TlsCompression
	{
		// Token: 0x06002C12 RID: 11282 RVA: 0x000A6AED File Offset: 0x000A4CED
		public virtual Stream Compress(Stream output)
		{
			return output;
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x000A6AED File Offset: 0x000A4CED
		public virtual Stream Decompress(Stream output)
		{
			return output;
		}
	}
}
