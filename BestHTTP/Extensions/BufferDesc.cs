using System;

namespace BestHTTP.Extensions
{
	// Token: 0x020007DE RID: 2014
	internal struct BufferDesc
	{
		// Token: 0x060047F9 RID: 18425 RVA: 0x0019A73E File Offset: 0x0019893E
		public BufferDesc(byte[] buff)
		{
			this.buffer = buff;
			this.released = DateTime.UtcNow;
		}

		// Token: 0x04002DD2 RID: 11730
		public static readonly BufferDesc Empty = new BufferDesc(null);

		// Token: 0x04002DD3 RID: 11731
		public byte[] buffer;

		// Token: 0x04002DD4 RID: 11732
		public DateTime released;
	}
}
