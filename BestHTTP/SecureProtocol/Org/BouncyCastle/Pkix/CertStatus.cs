using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Date;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Pkix
{
	// Token: 0x02000299 RID: 665
	public class CertStatus
	{
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x000BD144 File Offset: 0x000BB344
		// (set) Token: 0x06001893 RID: 6291 RVA: 0x000BD14C File Offset: 0x000BB34C
		public DateTimeObject RevocationDate
		{
			get
			{
				return this.revocationDate;
			}
			set
			{
				this.revocationDate = value;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x000BD155 File Offset: 0x000BB355
		// (set) Token: 0x06001895 RID: 6293 RVA: 0x000BD15D File Offset: 0x000BB35D
		public int Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		// Token: 0x0400172F RID: 5935
		public const int Unrevoked = 11;

		// Token: 0x04001730 RID: 5936
		public const int Undetermined = 12;

		// Token: 0x04001731 RID: 5937
		private int status = 11;

		// Token: 0x04001732 RID: 5938
		private DateTimeObject revocationDate;
	}
}
