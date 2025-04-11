using System;

namespace BestHTTP.WebSocket
{
	// Token: 0x020001AC RID: 428
	public enum WebSocketStausCodes : ushort
	{
		// Token: 0x04001307 RID: 4871
		NormalClosure = 1000,
		// Token: 0x04001308 RID: 4872
		GoingAway,
		// Token: 0x04001309 RID: 4873
		ProtocolError,
		// Token: 0x0400130A RID: 4874
		WrongDataType,
		// Token: 0x0400130B RID: 4875
		Reserved,
		// Token: 0x0400130C RID: 4876
		NoStatusCode,
		// Token: 0x0400130D RID: 4877
		ClosedAbnormally,
		// Token: 0x0400130E RID: 4878
		DataError,
		// Token: 0x0400130F RID: 4879
		PolicyError,
		// Token: 0x04001310 RID: 4880
		TooBigMessage,
		// Token: 0x04001311 RID: 4881
		ExtensionExpected,
		// Token: 0x04001312 RID: 4882
		WrongRequest,
		// Token: 0x04001313 RID: 4883
		TLSHandshakeError = 1015
	}
}
