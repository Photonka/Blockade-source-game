using System;
using System.Collections.Generic;

namespace BestHTTP.SocketIO.Transports
{
	// Token: 0x020001C2 RID: 450
	public interface ITransport
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600110E RID: 4366
		TransportTypes Type { get; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600110F RID: 4367
		TransportStates State { get; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06001110 RID: 4368
		SocketManager Manager { get; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06001111 RID: 4369
		bool IsRequestInProgress { get; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06001112 RID: 4370
		bool IsPollingInProgress { get; }

		// Token: 0x06001113 RID: 4371
		void Open();

		// Token: 0x06001114 RID: 4372
		void Poll();

		// Token: 0x06001115 RID: 4373
		void Send(Packet packet);

		// Token: 0x06001116 RID: 4374
		void Send(List<Packet> packets);

		// Token: 0x06001117 RID: 4375
		void Close();
	}
}
