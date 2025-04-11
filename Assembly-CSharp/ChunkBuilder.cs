using System;
using UnityEngine;

// Token: 0x020000F9 RID: 249
internal class ChunkBuilder
{
	// Token: 0x0600090A RID: 2314 RVA: 0x0008099A File Offset: 0x0007EB9A
	public static Mesh BuildChunk(Mesh mesh, Chunk chunk)
	{
		ChunkBuilder.Build(chunk, false);
		return ChunkBuilder.meshData.ToMesh(mesh);
	}

	// Token: 0x0600090B RID: 2315 RVA: 0x000809B0 File Offset: 0x0007EBB0
	public static void BuildChunkLighting(Mesh mesh, Chunk chunk)
	{
		ChunkBuilder.Build(chunk, true);
		if (ChunkBuilder.meshData.GetColors() == null)
		{
			return;
		}
		if (mesh == null)
		{
			return;
		}
		if (mesh.colors.Length == ChunkBuilder.meshData.GetColors().ToArray().Length)
		{
			mesh.colors = ChunkBuilder.meshData.GetColors().ToArray();
		}
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x00080A0C File Offset: 0x0007EC0C
	private static void Build(Chunk chunk, bool onlyLight)
	{
		Map map = chunk.GetMap();
		ChunkBuilder.meshData.Clear();
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				for (int k = 0; k < 8; k++)
				{
					Block block = chunk.GetBlock(k, j, i).block;
					if (block != null)
					{
						Vector3i vector3i = new Vector3i(k, j, i);
						Vector3i worldPos = Chunk.ToWorldPosition(chunk.GetPosition(), vector3i);
						block.Build(vector3i, worldPos, map, ChunkBuilder.meshData, onlyLight);
					}
				}
			}
		}
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x00080A90 File Offset: 0x0007EC90
	public static bool BuildChunkPro(MeshFilter filter, MeshCollider collider, Chunk chunk)
	{
		ChunkBuilder.lastupdate = Time.time;
		if (ChunkBuilder.Build(chunk, false, false))
		{
			filter.sharedMesh = ChunkBuilder.meshData.ToMesh(null);
			if (filter.sharedMesh != null)
			{
				ChunkBuilder.Build(chunk, false, true);
				collider.sharedMesh = ChunkBuilder.meshData.ToMesh(null);
			}
			else
			{
				collider.sharedMesh = null;
			}
			return false;
		}
		filter.sharedMesh = ChunkBuilder.meshData.ToMesh(null);
		return true;
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x00080B07 File Offset: 0x0007ED07
	public static void BuildChunkCollider(MeshFilter filter, MeshCollider collider)
	{
		collider.sharedMesh = null;
		if (filter.sharedMesh == null)
		{
			return;
		}
		collider.sharedMesh = filter.sharedMesh;
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x00080B2C File Offset: 0x0007ED2C
	private static bool Build(Chunk chunk, bool onlyLight, bool solidignore)
	{
		bool result = false;
		Map map = chunk.GetMap();
		ChunkBuilder.meshData.Clear();
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				for (int k = 0; k < 8; k++)
				{
					Block block = chunk.GetBlock(k, j, i).block;
					if (block != null)
					{
						if (block.GetName()[0] == '!')
						{
							result = true;
							if (solidignore)
							{
								goto IL_77;
							}
						}
						Vector3i vector3i = new Vector3i(k, j, i);
						Vector3i worldPos = Chunk.ToWorldPosition(chunk.GetPosition(), vector3i);
						block.Build(vector3i, worldPos, map, ChunkBuilder.meshData, onlyLight);
					}
					IL_77:;
				}
			}
		}
		return result;
	}

	// Token: 0x04000E70 RID: 3696
	private static MeshBuilder meshData = new MeshBuilder();

	// Token: 0x04000E71 RID: 3697
	public static float lastupdate = 0f;
}
