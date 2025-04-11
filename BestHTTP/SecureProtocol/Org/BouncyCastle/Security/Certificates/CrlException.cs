using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates
{
	// Token: 0x020002DC RID: 732
	[Serializable]
	public class CrlException : GeneralSecurityException
	{
		// Token: 0x06001B06 RID: 6918 RVA: 0x000BE0C4 File Offset: 0x000BC2C4
		public CrlException()
		{
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x000BE0CC File Offset: 0x000BC2CC
		public CrlException(string msg) : base(msg)
		{
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x000BE0D5 File Offset: 0x000BC2D5
		public CrlException(string msg, Exception e) : base(msg, e)
		{
		}
	}
}
