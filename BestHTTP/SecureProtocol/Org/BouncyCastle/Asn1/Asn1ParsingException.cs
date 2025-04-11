using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000616 RID: 1558
	[Serializable]
	public class Asn1ParsingException : InvalidOperationException
	{
		// Token: 0x06003B0D RID: 15117 RVA: 0x00170348 File Offset: 0x0016E548
		public Asn1ParsingException()
		{
		}

		// Token: 0x06003B0E RID: 15118 RVA: 0x00170350 File Offset: 0x0016E550
		public Asn1ParsingException(string message) : base(message)
		{
		}

		// Token: 0x06003B0F RID: 15119 RVA: 0x00170359 File Offset: 0x0016E559
		public Asn1ParsingException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
