using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000111 RID: 273
public class MapCulling : MonoBehaviour
{
	// Token: 0x060009EC RID: 2540 RVA: 0x000837D0 File Offset: 0x000819D0
	private void Awake()
	{
		this.map = base.GetComponent<Map>();
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x000837DE File Offset: 0x000819DE
	private void Update()
	{
		if (!this.building)
		{
			base.StartCoroutine(this.Building());
		}
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x000837F5 File Offset: 0x000819F5
	private IEnumerator Building()
	{
		this.building = true;
		Vector3 position = Camera.main.transform.position;
		Vector3i vector3i = Chunk.ToChunkPosition((int)position.x, (int)position.y, (int)position.z);
		Vector3i? closestEmptyColumn = this.columnMap.GetClosestEmptyColumn(vector3i.x, vector3i.z, 4);
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

	// Token: 0x060009EF RID: 2543 RVA: 0x00083804 File Offset: 0x00081A04
	public IEnumerator BuildColumn(int cx, int cz)
	{
		int num;
		for (int cy = 0; cy < 8; cy = num + 1)
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

	// Token: 0x04000EDB RID: 3803
	private Map map;

	// Token: 0x04000EDC RID: 3804
	private ColumnMap columnMap = new ColumnMap();

	// Token: 0x04000EDD RID: 3805
	private bool building;
}
