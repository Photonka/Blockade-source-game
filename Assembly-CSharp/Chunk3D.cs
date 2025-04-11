using System;

// Token: 0x0200010E RID: 270
public class Chunk3D<I>
{
	// Token: 0x060009CD RID: 2509 RVA: 0x00083218 File Offset: 0x00081418
	public void Set(I val, Vector3i pos)
	{
		this.Set(val, pos.x, pos.y, pos.z);
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x00083233 File Offset: 0x00081433
	public void Set(I val, int x, int y, int z)
	{
		this.chunk[z, y, x] = val;
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x00083245 File Offset: 0x00081445
	public I Get(Vector3i pos)
	{
		return this.Get(pos.x, pos.y, pos.z);
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x0008325F File Offset: 0x0008145F
	public I Get(int x, int y, int z)
	{
		return this.chunk[z, y, x];
	}

	// Token: 0x04000EC6 RID: 3782
	private I[,,] chunk = new I[8, 8, 8];
}
