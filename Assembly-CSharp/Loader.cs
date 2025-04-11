using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000023 RID: 35
public class Loader : MonoBehaviour
{
	// Token: 0x06000116 RID: 278 RVA: 0x00017C90 File Offset: 0x00015E90
	private void Awake()
	{
		this.map = base.GetComponent<Map>();
		this.blockSet = this.map.GetBlockSet();
		this.dirt = this.blockSet.GetBlock("Dirt");
		this.Generate();
		this.Visual();
	}

	// Token: 0x06000117 RID: 279 RVA: 0x00017CDC File Offset: 0x00015EDC
	private void Generate()
	{
		this.map.SetBlock(this.dirt, new Vector3i(1, 1, 1));
		this.map.SetBlock(this.dirt, new Vector3i(2, 1, 1));
	}

	// Token: 0x06000118 RID: 280 RVA: 0x00017D10 File Offset: 0x00015F10
	private void Visual()
	{
		for (int i = 0; i < 32; i++)
		{
			for (int j = 0; j < 32; j++)
			{
				for (int k = 0; k < 8; k++)
				{
					Chunk chunk = this.map.GetChunk(new Vector3i(i, k, j));
					if (chunk != null)
					{
						chunk.GetChunkRendererInstance().FastBuild();
						ChunkSunLightComputer.ComputeRays(this.map, i, j);
						chunk.GetChunkRenderer().SetLightDirty();
						this.Stats(chunk);
					}
				}
			}
		}
	}

	// Token: 0x06000119 RID: 281 RVA: 0x00017D84 File Offset: 0x00015F84
	private void Stats(Chunk chunk)
	{
		MeshFilter component = chunk.GetChunkRenderer().GetComponent<Renderer>().gameObject.GetComponent<MeshFilter>();
		Mesh sharedMesh = component.sharedMesh;
		Vector3[] vertices = sharedMesh.vertices;
		int[] triangles = sharedMesh.triangles;
		List<float> list = new List<float>();
		for (int i = 0; i < vertices.Length; i++)
		{
			list.Add(vertices[i].y);
		}
		List<float> list2 = list.Distinct<float>().ToList<float>();
		for (int j = 0; j < list2.Count; j++)
		{
			MonoBehaviour.print("List Y: " + list2[j].ToString());
		}
		float num = list2[1];
		int num2 = 0;
		for (int k = 0; k < triangles.Length; k += 3)
		{
			Vector3i vector3i = new Vector3i(triangles[k], triangles[k + 1], triangles[k + 2]);
			if (vertices[vector3i.x].y == num && vertices[vector3i.y].y == num)
			{
				float y = vertices[vector3i.z].y;
			}
			num2++;
		}
		component.sharedMesh.vertices = vertices;
		component.sharedMesh.triangles = triangles;
	}

	// Token: 0x04000138 RID: 312
	private Map map;

	// Token: 0x04000139 RID: 313
	private BlockSet blockSet;

	// Token: 0x0400013A RID: 314
	private Block dirt;
}
