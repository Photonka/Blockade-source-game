using System;

namespace BestHTTP.SignalRCore
{
	// Token: 0x020001D1 RID: 465
	public interface ITransport
	{
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06001165 RID: 4453
		TransferModes TransferMode { get; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06001166 RID: 4454
		TransportTypes TransportType { get; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06001167 RID: 4455
		TransportStates State { get; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06001168 RID: 4456
		string ErrorReason { get; }

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06001169 RID: 4457
		// (remove) Token: 0x0600116A RID: 4458
		event Action<TransportStates, TransportStates> OnStateChanged;

		// Token: 0x0600116B RID: 4459
		void StartConnect();

		// Token: 0x0600116C RID: 4460
		void StartClose();

		// Token: 0x0600116D RID: 4461
		void Send(byte[] msg);
	}
}
