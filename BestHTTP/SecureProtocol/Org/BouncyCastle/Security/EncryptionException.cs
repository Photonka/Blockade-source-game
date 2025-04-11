using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Security
{
	// Token: 0x020002C1 RID: 705
	[Serializable]
	public class EncryptionException : IOException
	{
		// Token: 0x06001A30 RID: 6704 RVA: 0x000B97A2 File Offset: 0x000B79A2
		public EncryptionException(string message) : base(message)
		{
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x000B97AB File Offset: 0x000B79AB
		public EncryptionException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
