using System;
using BestHTTP.WebSocket.Frames;

namespace BestHTTP.WebSocket.Extensions
{
	// Token: 0x020001B1 RID: 433
	public interface IExtension
	{
		// Token: 0x0600103C RID: 4156
		void AddNegotiation(HTTPRequest request);

		// Token: 0x0600103D RID: 4157
		bool ParseNegotiation(WebSocketResponse resp);

		// Token: 0x0600103E RID: 4158
		byte GetFrameHeader(WebSocketFrame writer, byte inFlag);

		// Token: 0x0600103F RID: 4159
		byte[] Encode(WebSocketFrame writer);

		// Token: 0x06001040 RID: 4160
		byte[] Decode(byte header, byte[] data, int length);
	}
}
