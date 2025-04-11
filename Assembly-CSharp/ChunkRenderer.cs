using System;
using UnityEngine;

// Token: 0x020000FE RID: 254
public class ChunkRenderer : MonoBehaviour
{
	// Token: 0x0600093A RID: 2362 RVA: 0x000812B0 File Offset: 0x0007F4B0
	public static ChunkRenderer CreateChunkRenderer(Vector3i pos, Map map, Chunk chunk)
	{
		GameObject gameObject = new GameObject(string.Concat(new object[]
		{
			"(",
			pos.x,
			" ",
			pos.y,
			" ",
			pos.z,
			")"
		}), new Type[]
		{
			typeof(MeshFilter),
			typeof(MeshRenderer),
			typeof(ChunkRenderer)
		});
		gameObject.transform.parent = map.transform;
		gameObject.transform.localPosition = new Vector3((float)(pos.x * 8), (float)(pos.y * 8), (float)(pos.z * 8));
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.transform.localScale = Vector3.one;
		ChunkRenderer component = gameObject.GetComponent<ChunkRenderer>();
		component.blockSet = map.GetBlockSet();
		component.chunk = chunk;
		component.x = pos.x;
		component.y = pos.y;
		component.z = pos.z;
		component.map = map;
		gameObject.isStatic = true;
		return component;
	}

	// Token: 0x0600093B RID: 2363 RVA: 0x000813EC File Offset: 0x0007F5EC
	private void Awake()
	{
		this.filter = base.GetComponent<MeshFilter>();
		this.coll = base.gameObject.AddComponent<MeshCollider>();
		base.GetComponent<Collider>().enabled = true;
		this.LocalPlayer = GameObject.Find("Player");
	}

	// Token: 0x0600093C RID: 2364 RVA: 0x00081428 File Offset: 0x0007F628
	private void FixedUpdate()
	{
		if (this.colliderDirty)
		{
			this.BuildCollider();
			this.colliderDirty = false;
		}
		if (this.dirty)
		{
			this.colliderDirty = this.BuildMesh();
			this.dirty = (this.lightDirty = false);
		}
		if (this.lightDirty)
		{
			this.BuildLighting();
			this.lightDirty = false;
		}
		if (this.rebuild)
		{
			this.colliderDirty = this.BuildMesh();
			this.rebuild = false;
			this.dirty = (this.lightDirty = false);
		}
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x000814B0 File Offset: 0x0007F6B0
	private void NearLightUpdate(int cx, int cy, int cz)
	{
		Chunk chunk = this.map.GetChunk(new Vector3i(this.x, this.y, this.z));
		if (chunk == null)
		{
			return;
		}
		ChunkRenderer chunkRenderer = chunk.GetChunkRenderer();
		if (chunkRenderer == null)
		{
			return;
		}
		chunkRenderer.SetRebuild();
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x000814FF File Offset: 0x0007F6FF
	public void FastBuild()
	{
		if (this.BuildMesh())
		{
			this.BuildCollider();
		}
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x00081510 File Offset: 0x0007F710
	private bool BuildMesh()
	{
		bool result = ChunkBuilder.BuildChunkPro(this.filter, this.coll, this.chunk);
		if (this.filter.sharedMesh == null)
		{
			Object.Destroy(base.gameObject);
			return false;
		}
		base.GetComponent<Renderer>().sharedMaterials = this.blockSet.GetMaterials(this.filter.sharedMesh.subMeshCount);
		return result;
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x0008157C File Offset: 0x0007F77C
	private void BuildCollider()
	{
		ChunkBuilder.BuildChunkCollider(this.filter, this.coll);
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x00081590 File Offset: 0x0007F790
	public void custom_build()
	{
		if (this.dirty)
		{
			this.in_build = true;
			this.cycle = 0;
		}
		if (!this.in_build)
		{
			return;
		}
		if (this.cycle == 0)
		{
			this.Build_start(this.chunk);
		}
		this.Build_continue(this.chunk, false);
		this.cycle++;
		if (this.cycle >= 8)
		{
			this.Build_end(this.filter.sharedMesh);
			this.dirty = (this.lightDirty = false);
			this.cycle = 0;
			this.in_build = false;
		}
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x00081623 File Offset: 0x0007F823
	private void Build_start(Chunk chunk)
	{
		this.map = chunk.GetMap();
		this.meshData.Clear();
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x0008163C File Offset: 0x0007F83C
	private void Build_continue(Chunk chunk, bool onlyLight)
	{
		int num = this.cycle;
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				Block block = chunk.GetBlock(j, i, num).block;
				if (block != null)
				{
					Vector3i vector3i = new Vector3i(j, i, num);
					Vector3i worldPos = Chunk.ToWorldPosition(chunk.GetPosition(), vector3i);
					block.Build(vector3i, worldPos, this.map, this.meshData, onlyLight);
				}
			}
		}
	}

	// Token: 0x06000944 RID: 2372 RVA: 0x000816AC File Offset: 0x0007F8AC
	private void Build_end(Mesh mesh)
	{
		this.filter.sharedMesh = this.meshData.ToMesh(mesh);
		if (this.filter.sharedMesh == null)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		base.GetComponent<Renderer>().sharedMaterials = this.blockSet.GetMaterials(this.filter.sharedMesh.subMeshCount);
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x00081715 File Offset: 0x0007F915
	private void BuildLighting()
	{
		if (this.filter.sharedMesh != null)
		{
			ChunkBuilder.BuildChunkLighting(this.coll.sharedMesh, this.chunk);
		}
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x00081740 File Offset: 0x0007F940
	public void SetDirty()
	{
		this.dirty = true;
	}

	// Token: 0x06000947 RID: 2375 RVA: 0x00081749 File Offset: 0x0007F949
	public void SetLightDirty()
	{
		this.lightDirty = true;
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x00081752 File Offset: 0x0007F952
	public void SetRebuild()
	{
		this.rebuild = true;
	}

	// Token: 0x04000E89 RID: 3721
	private int x;

	// Token: 0x04000E8A RID: 3722
	private int y;

	// Token: 0x04000E8B RID: 3723
	private int z;

	// Token: 0x04000E8C RID: 3724
	private GameObject LocalPlayer;

	// Token: 0x04000E8D RID: 3725
	private BlockSet blockSet;

	// Token: 0x04000E8E RID: 3726
	private Chunk chunk;

	// Token: 0x04000E8F RID: 3727
	private Map map;

	// Token: 0x04000E90 RID: 3728
	private bool dirty;

	// Token: 0x04000E91 RID: 3729
	private bool lightDirty;

	// Token: 0x04000E92 RID: 3730
	private bool colliderDirty;

	// Token: 0x04000E93 RID: 3731
	private bool rebuild;

	// Token: 0x04000E94 RID: 3732
	private MeshFilter filter;

	// Token: 0x04000E95 RID: 3733
	private MeshCollider coll;

	// Token: 0x04000E96 RID: 3734
	private bool dirtylock;

	// Token: 0x04000E97 RID: 3735
	private int cycle;

	// Token: 0x04000E98 RID: 3736
	private MeshBuilder meshData = new MeshBuilder();

	// Token: 0x04000E99 RID: 3737
	private bool in_build;
}
