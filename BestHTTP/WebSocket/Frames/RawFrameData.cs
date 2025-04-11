using System;
using BestHTTP.Extensions;

namespace BestHTTP.WebSocket.Frames
{
	// Token: 0x020001AD RID: 429
	public struct RawFrameData : IDisposable
	{
		// Token: 0x06001016 RID: 4118 RVA: 0x000A0108 File Offset: 0x0009E308
		public RawFrameData(byte[] data, int length)
		{
			this.Data = data;
			this.Length = length;
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x000A0118 File Offset: 0x0009E318
		public void Dispose()
		{
			VariableSizedBufferPool.Release(this.Data);
			this.Data = null;
		}

		// Token: 0x04001314 RID: 4884
		public byte[] Data;

		// Token: 0x04001315 RID: 4885
		public int Length;
	}
}
