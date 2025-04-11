using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Security
{
	// Token: 0x020002C7 RID: 711
	[Serializable]
	public class GeneralSecurityException : Exception
	{
		// Token: 0x06001A6A RID: 6762 RVA: 0x0008E219 File Offset: 0x0008C419
		public GeneralSecurityException()
		{
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x0008E285 File Offset: 0x0008C485
		public GeneralSecurityException(string message) : base(message)
		{
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x0008E28E File Offset: 0x0008C48E
		public GeneralSecurityException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
