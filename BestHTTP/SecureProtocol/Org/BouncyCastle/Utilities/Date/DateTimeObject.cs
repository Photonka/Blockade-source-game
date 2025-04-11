using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Date
{
	// Token: 0x0200027D RID: 637
	public sealed class DateTimeObject
	{
		// Token: 0x0600178A RID: 6026 RVA: 0x000BAEF2 File Offset: 0x000B90F2
		public DateTimeObject(DateTime dt)
		{
			this.dt = dt;
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x000BAF01 File Offset: 0x000B9101
		public DateTime Value
		{
			get
			{
				return this.dt;
			}
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x000BAF0C File Offset: 0x000B910C
		public override string ToString()
		{
			return this.dt.ToString();
		}

		// Token: 0x040016EC RID: 5868
		private readonly DateTime dt;
	}
}
