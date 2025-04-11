using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Security
{
	// Token: 0x020002D3 RID: 723
	[Serializable]
	public class SecurityUtilityException : Exception
	{
		// Token: 0x06001AE0 RID: 6880 RVA: 0x0008E219 File Offset: 0x0008C419
		public SecurityUtilityException()
		{
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x0008E285 File Offset: 0x0008C485
		public SecurityUtilityException(string message) : base(message)
		{
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x0008E28E File Offset: 0x0008C48E
		public SecurityUtilityException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
