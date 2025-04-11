using System;

namespace BestHTTP.Extensions
{
	// Token: 0x020007D6 RID: 2006
	public static class ExceptionHelper
	{
		// Token: 0x060047CE RID: 18382 RVA: 0x00199F45 File Offset: 0x00198145
		public static Exception ServerClosedTCPStream()
		{
			return new Exception("TCP Stream closed unexpectedly by the remote server");
		}
	}
}
