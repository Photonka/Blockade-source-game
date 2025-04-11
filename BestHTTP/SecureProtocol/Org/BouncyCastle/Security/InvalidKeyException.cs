using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Security
{
	// Token: 0x020002C9 RID: 713
	[Serializable]
	public class InvalidKeyException : KeyException
	{
		// Token: 0x06001A7C RID: 6780 RVA: 0x000CC74B File Offset: 0x000CA94B
		public InvalidKeyException()
		{
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x000CC753 File Offset: 0x000CA953
		public InvalidKeyException(string message) : base(message)
		{
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x000CC75C File Offset: 0x000CA95C
		public InvalidKeyException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
