using System;

// Token: 0x0200010B RID: 267
public class Map2D<I>
{
	// Token: 0x060009BA RID: 2490 RVA: 0x00082FE7 File Offset: 0x000811E7
	public Map2D()
	{
		this.defaultValue = default(I);
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x00083006 File Offset: 0x00081206
	public Map2D(I defaultValue)
	{
		this.defaultValue = defaultValue;
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x00083020 File Offset: 0x00081220
	public void Set(I val, int x, int z)
	{
		Vector3i vector3i = Chunk.ToChunkPosition(x, 0, z);
		Vector3i vector3i2 = Chunk.ToLocalPosition(x, 0, z);
		this.GetChunkInstance(vector3i.x, vector3i.z).Set(val, vector3i2.x, vector3i2.z);
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x00083064 File Offset: 0x00081264
	public I Get(int x, int z)
	{
		Vector3i vector3i = Chunk.ToChunkPosition(x, 0, z);
		Vector3i vector3i2 = Chunk.ToLocalPosition(x, 0, z);
		Chunk2D<I> chunk = this.GetChunk(vector3i.x, vector3i.z);
		if (chunk != null)
		{
			return chunk.Get(vector3i2.x, vector3i2.z);
		}
		return this.defaultValue;
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x000830B2 File Offset: 0x000812B2
	public Chunk2D<I> GetChunkInstance(int x, int z)
	{
		return this.chunks.GetInstance(x, z);
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x000830C1 File Offset: 0x000812C1
	public Chunk2D<I> GetChunk(int x, int z)
	{
		return this.chunks.SafeGet(x, z);
	}

	// Token: 0x04000EC1 RID: 3777
	private List2D<Chunk2D<I>> chunks = new List2D<Chunk2D<I>>();

	// Token: 0x04000EC2 RID: 3778
	private I defaultValue;
}
