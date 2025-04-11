using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002F RID: 47
public class Batch : MonoBehaviour
{
	// Token: 0x06000189 RID: 393 RVA: 0x00023F10 File Offset: 0x00022110
	public void Combine()
	{
		if (this.map == null)
		{
			this.map = base.GetComponent<Map>();
		}
		if (this.map == null)
		{
			return;
		}
		this.gochunk.Clear();
		for (int i = 0; i < 32; i++)
		{
			for (int j = 0; j < 32; j++)
			{
				for (int k = 0; k < 8; k++)
				{
					Chunk chunk = this.map.GetChunk(new Vector3i(i, k, j));
					if (chunk != null)
					{
						ChunkRenderer chunkRenderer = chunk.GetChunkRenderer();
						if (!(chunkRenderer == null))
						{
							this.gochunk.Add(chunkRenderer.gameObject);
						}
					}
				}
			}
		}
		StaticBatchingUtility.Combine(this.gochunk.ToArray(), base.gameObject);
	}

	// Token: 0x0400016A RID: 362
	private Map map;

	// Token: 0x0400016B RID: 363
	private List<GameObject> gochunk = new List<GameObject>();
}
