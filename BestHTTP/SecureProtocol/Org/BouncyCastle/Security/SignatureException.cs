using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Security
{
	// Token: 0x020002D4 RID: 724
	[Serializable]
	public class SignatureException : GeneralSecurityException
	{
		// Token: 0x06001AE3 RID: 6883 RVA: 0x000BE0C4 File Offset: 0x000BC2C4
		public SignatureException()
		{
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x000BE0CC File Offset: 0x000BC2CC
		public SignatureException(string message) : base(message)
		{
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x000BE0D5 File Offset: 0x000BC2D5
		public SignatureException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
