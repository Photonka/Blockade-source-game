using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000119 RID: 281
[AddComponentMenu("VoxelEngine/WorldGenerator")]
public class WorldGenerator : MonoBehaviour
{
	// Token: 0x06000A19 RID: 2585 RVA: 0x00084CF8 File Offset: 0x00082EF8
	private void Awake()
	{
		this.map = base.GetComponent<Map>();
		this.terrainGenerator = new TerrainGenerator(this.map);
		Block[] blocks = this.map.GetBlockSet().GetBlocks("Wood");
		Block[] blocks2 = this.map.GetBlockSet().GetBlocks("Leaf");
		this.treeGenerator = new TreeGenerator[Math.Max(blocks.Length, blocks2.Length)];
		for (int i = 0; i < this.treeGenerator.Length; i++)
		{
			Block wood = blocks[i % blocks.Length];
			Block leaves = blocks2[i % blocks2.Length];
			this.treeGenerator[i] = new TreeGenerator(this.map, wood, leaves);
		}
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x00084D9E File Offset: 0x00082F9E
	private void Update()
	{
		if (!this.building)
		{
			base.StartCoroutine(this.Building());
		}
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x00084DB5 File Offset: 0x00082FB5
	private IEnumerator Building()
	{
		this.building = true;
		Vector3 position = Camera.main.transform.position;
		Vector3i vector3i = Chunk.ToChunkPosition((int)position.x, (int)position.y, (int)position.z);
		Vector3i? closestEmptyColumn = this.columnMap.GetClosestEmptyColumn(vector3i.x, vector3i.z, 7);
		if (closestEmptyColumn != null)
		{
			int cx = closestEmptyColumn.Value.x;
			int cz = closestEmptyColumn.Value.z;
			this.columnMap.SetBuilt(cx, cz);
			yield return base.StartCoroutine(this.GenerateColumn(cx, cz));
			yield return null;
			ChunkSunLightComputer.ComputeRays(this.map, cx, cz);
			ChunkSunLightComputer.Scatter(this.map, this.columnMap, cx, cz);
			this.terrainGenerator.GeneratePlants(cx, cz);
			yield return base.StartCoroutine(this.BuildColumn(cx, cz));
		}
		this.building = false;
		yield break;
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x00084DC4 File Offset: 0x00082FC4
	private IEnumerator GenerateColumn(int cx, int cz)
	{
		yield return base.StartCoroutine(this.terrainGenerator.Generate(cx, cz));
		yield return null;
		if (this.treeGenerator.Length != 0)
		{
			int x = cx * 8 + 4;
			int z = cz * 8 + 4;
			int y = this.map.GetMaxY(x, z) + 1;
			int num = Random.Range(0, this.treeGenerator.Length);
			this.treeGenerator[num].Generate(x, y, z);
		}
		yield break;
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x00084DE1 File Offset: 0x00082FE1
	public IEnumerator BuildColumn(int cx, int cz)
	{
		List3D<Chunk> chunks = this.map.GetChunks();
		int num;
		for (int cy = chunks.GetMinY(); cy < chunks.GetMaxY(); cy = num + 1)
		{
			Chunk chunk = this.map.GetChunk(new Vector3i(cx, cy, cz));
			if (chunk != null)
			{
				chunk.GetChunkRendererInstance().SetDirty();
			}
			if (chunk != null)
			{
				yield return null;
			}
			num = cy;
		}
		yield break;
	}

	// Token: 0x04000F07 RID: 3847
	private Map map;

	// Token: 0x04000F08 RID: 3848
	private ColumnMap columnMap = new ColumnMap();

	// Token: 0x04000F09 RID: 3849
	private TerrainGenerator terrainGenerator;

	// Token: 0x04000F0A RID: 3850
	private TreeGenerator[] treeGenerator;

	// Token: 0x04000F0B RID: 3851
	private bool building;
}
