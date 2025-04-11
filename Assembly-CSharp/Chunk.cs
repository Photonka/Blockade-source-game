using System;

// Token: 0x020000FD RID: 253
public class Chunk
{
	// Token: 0x06000929 RID: 2345 RVA: 0x00081033 File Offset: 0x0007F233
	public Chunk(Map map, Vector3i position)
	{
		this.map = map;
		this.position = position;
	}

	// Token: 0x0600092A RID: 2346 RVA: 0x00081057 File Offset: 0x0007F257
	public ChunkRenderer GetChunkRendererInstance()
	{
		if (this.chunkRenderer == null)
		{
			this.chunkRenderer = ChunkRenderer.CreateChunkRenderer(this.position, this.map, this);
		}
		return this.chunkRenderer;
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x00081085 File Offset: 0x0007F285
	public ChunkRenderer GetChunkRenderer()
	{
		return this.chunkRenderer;
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x0008108D File Offset: 0x0007F28D
	public void SetBlock(BlockData block, Vector3i pos)
	{
		this.SetBlock(block, pos.x, pos.y, pos.z);
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x000810A8 File Offset: 0x0007F2A8
	public void SetBlock(BlockData block, int x, int y, int z)
	{
		this.blocks[z, y, x] = block;
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x000810BA File Offset: 0x0007F2BA
	public BlockData GetBlock(Vector3i pos)
	{
		return this.GetBlock(pos.x, pos.y, pos.z);
	}

	// Token: 0x0600092F RID: 2351 RVA: 0x000810D4 File Offset: 0x0007F2D4
	public BlockData GetBlock(int x, int y, int z)
	{
		return this.blocks[z, y, x];
	}

	// Token: 0x06000930 RID: 2352 RVA: 0x000810E4 File Offset: 0x0007F2E4
	public Map GetMap()
	{
		return this.map;
	}

	// Token: 0x06000931 RID: 2353 RVA: 0x000810EC File Offset: 0x0007F2EC
	public Vector3i GetPosition()
	{
		return this.position;
	}

	// Token: 0x06000932 RID: 2354 RVA: 0x000810F4 File Offset: 0x0007F2F4
	public static bool FixCoords(ref Vector3i chunk, ref Vector3i local)
	{
		bool result = false;
		if (local.x < 0)
		{
			chunk.x--;
			local.x += 8;
			result = true;
		}
		if (local.y < 0)
		{
			chunk.y--;
			local.y += 8;
			result = true;
		}
		if (local.z < 0)
		{
			chunk.z--;
			local.z += 8;
			result = true;
		}
		if (local.x >= 8)
		{
			chunk.x++;
			local.x -= 8;
			result = true;
		}
		if (local.y >= 8)
		{
			chunk.y++;
			local.y -= 8;
			result = true;
		}
		if (local.z >= 8)
		{
			chunk.z++;
			local.z -= 8;
			result = true;
		}
		return result;
	}

	// Token: 0x06000933 RID: 2355 RVA: 0x000811CA File Offset: 0x0007F3CA
	public static bool IsCorrectLocalPosition(Vector3i local)
	{
		return Chunk.IsCorrectLocalPosition(local.x, local.y, local.z);
	}

	// Token: 0x06000934 RID: 2356 RVA: 0x000811E3 File Offset: 0x0007F3E3
	public static bool IsCorrectLocalPosition(int x, int y, int z)
	{
		return (x & 7) == x && (y & 7) == y && (z & 7) == z;
	}

	// Token: 0x06000935 RID: 2357 RVA: 0x000811F9 File Offset: 0x0007F3F9
	public static Vector3i ToChunkPosition(Vector3i point)
	{
		return Chunk.ToChunkPosition(point.x, point.y, point.z);
	}

	// Token: 0x06000936 RID: 2358 RVA: 0x00081214 File Offset: 0x0007F414
	public static Vector3i ToChunkPosition(int pointX, int pointY, int pointZ)
	{
		int x = pointX >> 3;
		int y = pointY >> 3;
		int z = pointZ >> 3;
		return new Vector3i(x, y, z);
	}

	// Token: 0x06000937 RID: 2359 RVA: 0x00081233 File Offset: 0x0007F433
	public static Vector3i ToLocalPosition(Vector3i point)
	{
		return Chunk.ToLocalPosition(point.x, point.y, point.z);
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x0008124C File Offset: 0x0007F44C
	public static Vector3i ToLocalPosition(int pointX, int pointY, int pointZ)
	{
		int x = pointX & 7;
		int y = pointY & 7;
		int z = pointZ & 7;
		return new Vector3i(x, y, z);
	}

	// Token: 0x06000939 RID: 2361 RVA: 0x0008126C File Offset: 0x0007F46C
	public static Vector3i ToWorldPosition(Vector3i chunkPosition, Vector3i localPosition)
	{
		int x = (chunkPosition.x << 3) + localPosition.x;
		int y = (chunkPosition.y << 3) + localPosition.y;
		int z = (chunkPosition.z << 3) + localPosition.z;
		return new Vector3i(x, y, z);
	}

	// Token: 0x04000E7F RID: 3711
	public const int SIZE_X_BITS = 3;

	// Token: 0x04000E80 RID: 3712
	public const int SIZE_Y_BITS = 3;

	// Token: 0x04000E81 RID: 3713
	public const int SIZE_Z_BITS = 3;

	// Token: 0x04000E82 RID: 3714
	public const int SIZE_X = 8;

	// Token: 0x04000E83 RID: 3715
	public const int SIZE_Y = 8;

	// Token: 0x04000E84 RID: 3716
	public const int SIZE_Z = 8;

	// Token: 0x04000E85 RID: 3717
	private BlockData[,,] blocks = new BlockData[8, 8, 8];

	// Token: 0x04000E86 RID: 3718
	private Map map;

	// Token: 0x04000E87 RID: 3719
	private Vector3i position;

	// Token: 0x04000E88 RID: 3720
	private ChunkRenderer chunkRenderer;
}
