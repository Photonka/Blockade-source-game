using System;
using BestHTTP.SocketIO.Transports;

namespace BestHTTP.SocketIO
{
	// Token: 0x020001BA RID: 442
	public interface IManager
	{
		// Token: 0x0600106B RID: 4203
		void Remove(Socket socket);

		// Token: 0x0600106C RID: 4204
		void Close(bool removeSockets = true);

		// Token: 0x0600106D RID: 4205
		void TryToReconnect();

		// Token: 0x0600106E RID: 4206
		bool OnTransportConnected(ITransport transport);

		// Token: 0x0600106F RID: 4207
		void OnTransportError(ITransport trans, string err);

		// Token: 0x06001070 RID: 4208
		void OnTransportProbed(ITransport trans);

		// Token: 0x06001071 RID: 4209
		void SendPacket(Packet packet);

		// Token: 0x06001072 RID: 4210
		void OnPacket(Packet packet);

		// Token: 0x06001073 RID: 4211
		void EmitEvent(string eventName, params object[] args);

		// Token: 0x06001074 RID: 4212
		void EmitEvent(SocketIOEventTypes type, params object[] args);

		// Token: 0x06001075 RID: 4213
		void EmitError(SocketIOErrors errCode, string msg);

		// Token: 0x06001076 RID: 4214
		void EmitAll(string eventName, params object[] args);
	}
}
