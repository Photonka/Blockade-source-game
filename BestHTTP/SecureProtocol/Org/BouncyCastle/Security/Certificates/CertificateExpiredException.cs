using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates
{
	// Token: 0x020002D9 RID: 729
	[Serializable]
	public class CertificateExpiredException : CertificateException
	{
		// Token: 0x06001AFD RID: 6909 RVA: 0x000D19EF File Offset: 0x000CFBEF
		public CertificateExpiredException()
		{
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x000D19F7 File Offset: 0x000CFBF7
		public CertificateExpiredException(string message) : base(message)
		{
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x000D1A00 File Offset: 0x000CFC00
		public CertificateExpiredException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
