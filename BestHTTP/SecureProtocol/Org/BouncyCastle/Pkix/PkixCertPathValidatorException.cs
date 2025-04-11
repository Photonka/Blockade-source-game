using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Pkix
{
	// Token: 0x020002A4 RID: 676
	[Serializable]
	public class PkixCertPathValidatorException : GeneralSecurityException
	{
		// Token: 0x060018C8 RID: 6344 RVA: 0x000BE7F0 File Offset: 0x000BC9F0
		public PkixCertPathValidatorException()
		{
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x000BE7FF File Offset: 0x000BC9FF
		public PkixCertPathValidatorException(string message) : base(message)
		{
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x000BE80F File Offset: 0x000BCA0F
		public PkixCertPathValidatorException(string message, Exception cause) : base(message)
		{
			this.cause = cause;
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x000BE828 File Offset: 0x000BCA28
		public PkixCertPathValidatorException(string message, Exception cause, PkixCertPath certPath, int index) : base(message)
		{
			if (certPath == null && index != -1)
			{
				throw new ArgumentNullException("certPath = null and index != -1");
			}
			if (index < -1 || (certPath != null && index >= certPath.Certificates.Count))
			{
				throw new IndexOutOfRangeException(" index < -1 or out of bound of certPath.getCertificates()");
			}
			this.cause = cause;
			this.certPath = certPath;
			this.index = index;
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x000BE890 File Offset: 0x000BCA90
		public override string Message
		{
			get
			{
				string message = base.Message;
				if (message != null)
				{
					return message;
				}
				if (this.cause != null)
				{
					return this.cause.Message;
				}
				return null;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x000BE8BE File Offset: 0x000BCABE
		public PkixCertPath CertPath
		{
			get
			{
				return this.certPath;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x000BE8C6 File Offset: 0x000BCAC6
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x0400173A RID: 5946
		private Exception cause;

		// Token: 0x0400173B RID: 5947
		private PkixCertPath certPath;

		// Token: 0x0400173C RID: 5948
		private int index = -1;
	}
}
