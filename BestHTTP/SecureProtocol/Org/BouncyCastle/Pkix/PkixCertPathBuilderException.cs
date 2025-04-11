using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Pkix
{
	// Token: 0x020002A0 RID: 672
	[Serializable]
	public class PkixCertPathBuilderException : GeneralSecurityException
	{
		// Token: 0x060018B9 RID: 6329 RVA: 0x000BE0C4 File Offset: 0x000BC2C4
		public PkixCertPathBuilderException()
		{
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x000BE0CC File Offset: 0x000BC2CC
		public PkixCertPathBuilderException(string message) : base(message)
		{
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x000BE0D5 File Offset: 0x000BC2D5
		public PkixCertPathBuilderException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
