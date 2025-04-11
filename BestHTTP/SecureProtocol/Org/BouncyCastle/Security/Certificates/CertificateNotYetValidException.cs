using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates
{
	// Token: 0x020002DA RID: 730
	[Serializable]
	public class CertificateNotYetValidException : CertificateException
	{
		// Token: 0x06001B00 RID: 6912 RVA: 0x000D19EF File Offset: 0x000CFBEF
		public CertificateNotYetValidException()
		{
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x000D19F7 File Offset: 0x000CFBF7
		public CertificateNotYetValidException(string message) : base(message)
		{
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x000D1A00 File Offset: 0x000CFC00
		public CertificateNotYetValidException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
