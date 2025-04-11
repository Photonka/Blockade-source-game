using System;

namespace BestHTTP.Extensions
{
	// Token: 0x020007D9 RID: 2009
	public interface IHeartbeat
	{
		// Token: 0x060047DE RID: 18398
		void OnHeartbeatUpdate(TimeSpan dif);
	}
}
