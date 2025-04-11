using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Security
{
	// Token: 0x020002CB RID: 715
	[Serializable]
	public class KeyException : GeneralSecurityException
	{
		// Token: 0x06001A82 RID: 6786 RVA: 0x000BE0C4 File Offset: 0x000BC2C4
		public KeyException()
		{
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x000BE0CC File Offset: 0x000BC2CC
		public KeyException(string message) : base(message)
		{
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x000BE0D5 File Offset: 0x000BC2D5
		public KeyException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
