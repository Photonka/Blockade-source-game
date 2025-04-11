using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Tsp
{
	// Token: 0x02000296 RID: 662
	[Serializable]
	public class TspException : Exception
	{
		// Token: 0x06001882 RID: 6274 RVA: 0x0008E219 File Offset: 0x0008C419
		public TspException()
		{
		}

		// Token: 0x06001883 RID: 6275 RVA: 0x0008E285 File Offset: 0x0008C485
		public TspException(string message) : base(message)
		{
		}

		// Token: 0x06001884 RID: 6276 RVA: 0x0008E28E File Offset: 0x0008C48E
		public TspException(string message, Exception e) : base(message, e)
		{
		}
	}
}
