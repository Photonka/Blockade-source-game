using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Date
{
	// Token: 0x0200027E RID: 638
	public class DateTimeUtilities
	{
		// Token: 0x0600178D RID: 6029 RVA: 0x00023EF4 File Offset: 0x000220F4
		private DateTimeUtilities()
		{
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x000BAF28 File Offset: 0x000B9128
		public static long DateTimeToUnixMs(DateTime dateTime)
		{
			if (dateTime.CompareTo(DateTimeUtilities.UnixEpoch) < 0)
			{
				throw new ArgumentException("DateTime value may not be before the epoch", "dateTime");
			}
			return (dateTime.Ticks - DateTimeUtilities.UnixEpoch.Ticks) / 10000L;
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x000BAF70 File Offset: 0x000B9170
		public static DateTime UnixMsToDateTime(long unixMs)
		{
			return new DateTime(unixMs * 10000L + DateTimeUtilities.UnixEpoch.Ticks);
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x000BAF98 File Offset: 0x000B9198
		public static long CurrentUnixMs()
		{
			return DateTimeUtilities.DateTimeToUnixMs(DateTime.UtcNow);
		}

		// Token: 0x040016ED RID: 5869
		public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);
	}
}
