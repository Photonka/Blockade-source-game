using System;
using System.Collections.Generic;

namespace BestHTTP.Extensions
{
	// Token: 0x020007DD RID: 2013
	internal struct BufferStore
	{
		// Token: 0x060047F7 RID: 18423 RVA: 0x0019A710 File Offset: 0x00198910
		public BufferStore(long size)
		{
			this.Size = size;
			this.buffers = new List<BufferDesc>();
		}

		// Token: 0x060047F8 RID: 18424 RVA: 0x0019A724 File Offset: 0x00198924
		public BufferStore(long size, byte[] buffer)
		{
			this = new BufferStore(size);
			this.buffers.Add(new BufferDesc(buffer));
		}

		// Token: 0x04002DD0 RID: 11728
		public readonly long Size;

		// Token: 0x04002DD1 RID: 11729
		public List<BufferDesc> buffers;
	}
}
