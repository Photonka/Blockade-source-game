using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Ocsp
{
	// Token: 0x020002E1 RID: 737
	[Serializable]
	public class OcspException : Exception
	{
		// Token: 0x06001B36 RID: 6966 RVA: 0x0008E219 File Offset: 0x0008C419
		public OcspException()
		{
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x0008E285 File Offset: 0x0008C485
		public OcspException(string message) : base(message)
		{
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x0008E28E File Offset: 0x0008C48E
		public OcspException(string message, Exception e) : base(message, e)
		{
		}
	}
}
