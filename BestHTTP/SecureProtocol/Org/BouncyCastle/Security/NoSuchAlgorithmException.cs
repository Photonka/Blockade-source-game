using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Security
{
	// Token: 0x020002CD RID: 717
	[Obsolete("Never thrown")]
	[Serializable]
	public class NoSuchAlgorithmException : GeneralSecurityException
	{
		// Token: 0x06001A8D RID: 6797 RVA: 0x000BE0C4 File Offset: 0x000BC2C4
		public NoSuchAlgorithmException()
		{
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x000BE0CC File Offset: 0x000BC2CC
		public NoSuchAlgorithmException(string message) : base(message)
		{
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x000BE0D5 File Offset: 0x000BC2D5
		public NoSuchAlgorithmException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
