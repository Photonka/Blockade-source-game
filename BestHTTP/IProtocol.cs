using System;

namespace BestHTTP
{
	// Token: 0x02000180 RID: 384
	public interface IProtocol
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000E29 RID: 3625
		bool IsClosed { get; }

		// Token: 0x06000E2A RID: 3626
		void HandleEvents();
	}
}
