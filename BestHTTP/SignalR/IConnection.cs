using System;
using BestHTTP.SignalR.JsonEncoders;
using BestHTTP.SignalR.Messages;
using BestHTTP.SignalR.Transports;

namespace BestHTTP.SignalR
{
	// Token: 0x020001F0 RID: 496
	public interface IConnection
	{
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600124A RID: 4682
		ProtocolVersions Protocol { get; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600124B RID: 4683
		NegotiationData NegotiationResult { get; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600124C RID: 4684
		// (set) Token: 0x0600124D RID: 4685
		IJsonEncoder JsonEncoder { get; set; }

		// Token: 0x0600124E RID: 4686
		void OnMessage(IServerMessage msg);

		// Token: 0x0600124F RID: 4687
		void TransportStarted();

		// Token: 0x06001250 RID: 4688
		void TransportReconnected();

		// Token: 0x06001251 RID: 4689
		void TransportAborted();

		// Token: 0x06001252 RID: 4690
		void Error(string reason);

		// Token: 0x06001253 RID: 4691
		Uri BuildUri(RequestTypes type);

		// Token: 0x06001254 RID: 4692
		Uri BuildUri(RequestTypes type, TransportBase transport);

		// Token: 0x06001255 RID: 4693
		HTTPRequest PrepareRequest(HTTPRequest req, RequestTypes type);

		// Token: 0x06001256 RID: 4694
		string ParseResponse(string responseStr);
	}
}
