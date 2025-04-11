using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO.Pem
{
	// Token: 0x0200026B RID: 619
	[Serializable]
	public class PemGenerationException : Exception
	{
		// Token: 0x0600172E RID: 5934 RVA: 0x0008E219 File Offset: 0x0008C419
		public PemGenerationException()
		{
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x0008E285 File Offset: 0x0008C485
		public PemGenerationException(string message) : base(message)
		{
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x0008E28E File Offset: 0x0008C48E
		public PemGenerationException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
