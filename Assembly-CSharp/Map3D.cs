using System;

// Token: 0x0200010D RID: 269
public class Map3D<I>
{
	// Token: 0x060009C3 RID: 2499 RVA: 0x00083104 File Offset: 0x00081304
	public Map3D()
	{
		this.defaultValue = default(I);
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x00083123 File Offset: 0x00081323
	public Map3D(I defaultValue)
	{
		this.defaultValue = defaultValue;
	}

	// Token: 0x060009C5 RID: 2501 RVA: 0x0008313D File Offset: 0x0008133D
	public void Set(I val, Vector3i pos)
	{
		this.Set(val, pos.x, pos.y, pos.z);
	}

	// Token: 0x060009C6 RID: 2502 RVA: 0x00083158 File Offset: 0x00081358
	public void Set(I val, int x, int y, int z)
	{
		Vector3i pos = Chunk.ToChunkPosition(x, y, z);
		Vector3i pos2 = Chunk.ToLocalPosition(x, y, z);
		this.GetChunkInstance(pos).Set(val, pos2);
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x00083187 File Offset: 0x00081387
	public I Get(Vector3i pos)
	{
		return this.Get(pos.x, pos.y, pos.z);
	}

	// Token: 0x060009C8 RID: 2504 RVA: 0x000831A4 File Offset: 0x000813A4
	public I Get(int x, int y, int z)
	{
		Vector3i pos = Chunk.ToChunkPosition(x, y, z);
		Vector3i pos2 = Chunk.ToLocalPosition(x, y, z);
		Chunk3D<I> chunk = this.GetChunk(pos);
		if (chunk != null)
		{
			return chunk.Get(pos2);
		}
		return this.defaultValue;
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x000831DC File Offset: 0x000813DC
	public Chunk3D<I> GetChunkInstance(Vector3i pos)
	{
		return this.chunks.GetInstance(pos);
	}

	// Token: 0x060009CA RID: 2506 RVA: 0x000831EA File Offset: 0x000813EA
	public Chunk3D<I> GetChunkInstance(int x, int y, int z)
	{
		return this.chunks.GetInstance(x, y, z);
	}

	// Token: 0x060009CB RID: 2507 RVA: 0x000831FA File Offset: 0x000813FA
	public Chunk3D<I> GetChunk(Vector3i pos)
	{
		return this.chunks.SafeGet(pos);
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x00083208 File Offset: 0x00081408
	public Chunk3D<I> GetChunk(int x, int y, int z)
	{
		return this.chunks.SafeGet(x, y, z);
	}

	// Token: 0x04000EC4 RID: 3780
	private List3D<Chunk3D<I>> chunks = new List3D<Chunk3D<I>>();

	// Token: 0x04000EC5 RID: 3781
	private I defaultValue;
}
