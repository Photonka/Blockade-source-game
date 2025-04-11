using System;

// Token: 0x0200010C RID: 268
public class Chunk2D<I>
{
	// Token: 0x060009C0 RID: 2496 RVA: 0x000830D0 File Offset: 0x000812D0
	public void Set(I val, int x, int z)
	{
		this.chunk[z, x] = val;
	}

	// Token: 0x060009C1 RID: 2497 RVA: 0x000830E0 File Offset: 0x000812E0
	public I Get(int x, int z)
	{
		return this.chunk[z, x];
	}

	// Token: 0x04000EC3 RID: 3779
	private I[,] chunk = new I[8, 8];
}
