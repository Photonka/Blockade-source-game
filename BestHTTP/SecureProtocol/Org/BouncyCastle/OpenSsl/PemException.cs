using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.OpenSsl
{
	// Token: 0x020002BC RID: 700
	[Serializable]
	public class PemException : IOException
	{
		// Token: 0x06001A12 RID: 6674 RVA: 0x000B97A2 File Offset: 0x000B79A2
		public PemException(string message) : base(message)
		{
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x000B97AB File Offset: 0x000B79AB
		public PemException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
