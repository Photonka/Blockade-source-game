using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200011B RID: 283
[AddComponentMenu("VoxelEngine/WorldGenerator")]
public class WorldClear : MonoBehaviour
{
	// Token: 0x06000A21 RID: 2593 RVA: 0x00084F94 File Offset: 0x00083194
	private void Awake()
	{
		this.map = base.GetComponent<Map>();
		this.terrainGenerator = new TerrainClear(this.map);
		for (int i = 0; i < 32; i++)
		{
			for (int j = 0; j < 32; j++)
			{
				this.terrainGenerator.GenerateChunk(i, 0, j);
			}
		}
	}

	// Token: 0x06000A22 RID: 2594 RVA: 0x00084FE6 File Offset: 0x000831E6
	private void Update()
	{
		if (!this.building)
		{
			base.StartCoroutine(this.Building());
		}
	}

	// Token: 0x06000A23 RID: 2595 RVA: 0x00084FFD File Offset: 0x000831FD
	private IEnumerator Building()
	{
		this.building = true;
		Vector3 position = Camera.main.transform.position;
		Vector3i vector3i = Chunk.ToChunkPosition((int)position.x, (int)position.y, (int)position.z);
		Vector3i? closestEmptyColumn = this.columnMap.GetClosestEmptyColumn(vector3i.x, vector3i.z, 40);
		if (closestEmptyColumn != null)
		{
			string[] array = new string[6];
			array[0] = "Building( ) column: ";
			int num = 1;
			Vector3i value = closestEmptyColumn.Value;
			array[num] = value.x.ToString();
			array[2] = " ";
			int num2 = 3;
			value = closestEmptyColumn.Value;
			array[num2] = value.y.ToString();
			array[4] = " ";
			int num3 = 5;
			value = closestEmptyColumn.Value;
			array[num3] = value.z.ToString();
			MonoBehaviour.print(string.Concat(array));
		}
		if (closestEmptyColumn != null)
		{
			int x = closestEmptyColumn.Value.x;
			int z = closestEmptyColumn.Value.z;
			this.columnMap.SetBuilt(x, z);
			ChunkSunLightComputer.ComputeRays(this.map, x, z);
			ChunkSunLightComputer.Scatter(this.map, this.columnMap, x, z);
			yield return base.StartCoroutine(this.BuildColumn(x, z));
		}
		this.building = false;
		yield break;
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x0008500C File Offset: 0x0008320C
	private IEnumerator GenerateColumn(int cx, int cz)
	{
		yield return null;
		yield break;
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x00085014 File Offset: 0x00083214
	public IEnumerator BuildColumn(int cx, int cz)
	{
		int num;
		for (int cy = 0; cy < 4; cy = num + 1)
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

	// Token: 0x06000A26 RID: 2598 RVA: 0x00085034 File Offset: 0x00083234
	private void BuildColumn_(int cx, int cz)
	{
		for (int i = 0; i < 4; i++)
		{
			Chunk chunk = this.map.GetChunk(new Vector3i(cx, i, cz));
			if (chunk != null)
			{
				chunk.GetChunkRendererInstance().SetDirty();
			}
		}
	}

	// Token: 0x04000F1A RID: 3866
	private Map map;

	// Token: 0x04000F1B RID: 3867
	private ColumnMap columnMap = new ColumnMap();

	// Token: 0x04000F1C RID: 3868
	private TerrainClear terrainGenerator;

	// Token: 0x04000F1D RID: 3869
	private bool building;
}
