using System;

namespace BestHTTP.SocketIO
{
	// Token: 0x020001BB RID: 443
	public interface ISocket
	{
		// Token: 0x06001077 RID: 4215
		void Open();

		// Token: 0x06001078 RID: 4216
		void Disconnect(bool remove);

		// Token: 0x06001079 RID: 4217
		void OnPacket(Packet packet);

		// Token: 0x0600107A RID: 4218
		void EmitEvent(SocketIOEventTypes type, params object[] args);

		// Token: 0x0600107B RID: 4219
		void EmitEvent(string eventName, params object[] args);

		// Token: 0x0600107C RID: 4220
		void EmitError(SocketIOErrors errCode, string msg);
	}
}
