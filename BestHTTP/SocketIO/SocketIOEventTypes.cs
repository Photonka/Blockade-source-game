using System;

namespace BestHTTP.SocketIO
{
	// Token: 0x020001B6 RID: 438
	public enum SocketIOEventTypes
	{
		// Token: 0x04001350 RID: 4944
		Unknown = -1,
		// Token: 0x04001351 RID: 4945
		Connect,
		// Token: 0x04001352 RID: 4946
		Disconnect,
		// Token: 0x04001353 RID: 4947
		Event,
		// Token: 0x04001354 RID: 4948
		Ack,
		// Token: 0x04001355 RID: 4949
		Error,
		// Token: 0x04001356 RID: 4950
		BinaryEvent,
		// Token: 0x04001357 RID: 4951
		BinaryAck
	}
}
