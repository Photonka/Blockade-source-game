using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates
{
	// Token: 0x020002D8 RID: 728
	[Serializable]
	public class CertificateException : GeneralSecurityException
	{
		// Token: 0x06001AFA RID: 6906 RVA: 0x000BE0C4 File Offset: 0x000BC2C4
		public CertificateException()
		{
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x000BE0CC File Offset: 0x000BC2CC
		public CertificateException(string message) : base(message)
		{
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x000BE0D5 File Offset: 0x000BC2D5
		public CertificateException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
