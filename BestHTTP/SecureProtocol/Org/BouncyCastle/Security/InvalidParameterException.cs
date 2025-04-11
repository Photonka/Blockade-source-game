using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Security
{
	// Token: 0x020002CA RID: 714
	[Serializable]
	public class InvalidParameterException : KeyException
	{
		// Token: 0x06001A7F RID: 6783 RVA: 0x000CC74B File Offset: 0x000CA94B
		public InvalidParameterException()
		{
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x000CC753 File Offset: 0x000CA953
		public InvalidParameterException(string message) : base(message)
		{
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x000CC75C File Offset: 0x000CA95C
		public InvalidParameterException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
