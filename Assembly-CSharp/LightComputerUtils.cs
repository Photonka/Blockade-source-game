using System;

// Token: 0x02000101 RID: 257
internal class LightComputerUtils
{
	// Token: 0x06000956 RID: 2390 RVA: 0x00081B3C File Offset: 0x0007FD3C
	public static void SetLightDirty(Map map, Vector3i pos)
	{
		Vector3i vector3i = Chunk.ToChunkPosition(pos);
		Chunk.ToLocalPosition(pos);
		LightComputerUtils.SetChunkLightDirty(map, vector3i);
		LightComputerUtils.SetChunkLightDirty(map, vector3i - Vector3i.right);
		LightComputerUtils.SetChunkLightDirty(map, vector3i - Vector3i.up);
		LightComputerUtils.SetChunkLightDirty(map, vector3i - Vector3i.forward);
		LightComputerUtils.SetChunkLightDirty(map, vector3i + Vector3i.right);
		LightComputerUtils.SetChunkLightDirty(map, vector3i + Vector3i.up);
		LightComputerUtils.SetChunkLightDirty(map, vector3i + Vector3i.forward);
	}

	// Token: 0x06000957 RID: 2391 RVA: 0x00081BC4 File Offset: 0x0007FDC4
	private static void SetChunkLightDirty(Map map, Vector3i chunkPos)
	{
		Chunk chunk = map.GetChunk(chunkPos);
		if (chunk == null)
		{
			return;
		}
		ChunkRenderer chunkRenderer = chunk.GetChunkRenderer();
		if (chunkRenderer == null)
		{
			return;
		}
		chunkRenderer.SetLightDirty();
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x00081BF4 File Offset: 0x0007FDF4
	public static int GetLightStep(BlockData block)
	{
		if (block.IsEmpty())
		{
			return 1;
		}
		return 2;
	}
}
