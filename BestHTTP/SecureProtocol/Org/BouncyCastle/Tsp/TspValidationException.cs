using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Tsp
{
	// Token: 0x02000298 RID: 664
	[Serializable]
	public class TspValidationException : TspException
	{
		// Token: 0x0600188F RID: 6287 RVA: 0x000BD11C File Offset: 0x000BB31C
		public TspValidationException(string message) : base(message)
		{
			this.failureCode = -1;
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x000BD12C File Offset: 0x000BB32C
		public TspValidationException(string message, int failureCode) : base(message)
		{
			this.failureCode = failureCode;
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x000BD13C File Offset: 0x000BB33C
		public int FailureCode
		{
			get
			{
				return this.failureCode;
			}
		}

		// Token: 0x0400172E RID: 5934
		private int failureCode;
	}
}
