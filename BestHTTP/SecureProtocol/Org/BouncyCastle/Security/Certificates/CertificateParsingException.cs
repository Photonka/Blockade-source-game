using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates
{
	// Token: 0x020002DB RID: 731
	[Serializable]
	public class CertificateParsingException : CertificateException
	{
		// Token: 0x06001B03 RID: 6915 RVA: 0x000D19EF File Offset: 0x000CFBEF
		public CertificateParsingException()
		{
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x000D19F7 File Offset: 0x000CFBF7
		public CertificateParsingException(string message) : base(message)
		{
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x000D1A00 File Offset: 0x000CFC00
		public CertificateParsingException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
