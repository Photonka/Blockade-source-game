using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities
{
	// Token: 0x02000252 RID: 594
	public sealed class Times
	{
		// Token: 0x0600161F RID: 5663 RVA: 0x000B25FC File Offset: 0x000B07FC
		public static long NanoTime()
		{
			return DateTime.UtcNow.Ticks * Times.NanosecondsPerTick;
		}

		// Token: 0x0400154C RID: 5452
		private static long NanosecondsPerTick = 100L;
	}
}
