using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Security
{
	// Token: 0x020002C2 RID: 706
	[Serializable]
	public class PasswordException : IOException
	{
		// Token: 0x06001A32 RID: 6706 RVA: 0x000B97A2 File Offset: 0x000B79A2
		public PasswordException(string message) : base(message)
		{
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x000B97AB File Offset: 0x000B79AB
		public PasswordException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
