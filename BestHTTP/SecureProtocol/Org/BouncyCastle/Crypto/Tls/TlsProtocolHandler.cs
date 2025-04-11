using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000461 RID: 1121
	[Obsolete("Use 'TlsClientProtocol' instead")]
	public class TlsProtocolHandler : TlsClientProtocol
	{
		// Token: 0x06002C5D RID: 11357 RVA: 0x0011B456 File Offset: 0x00119656
		public TlsProtocolHandler(Stream stream, SecureRandom secureRandom) : base(stream, stream, secureRandom)
		{
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x0011B461 File Offset: 0x00119661
		public TlsProtocolHandler(Stream input, Stream output, SecureRandom secureRandom) : base(input, output, secureRandom)
		{
		}
	}
}
