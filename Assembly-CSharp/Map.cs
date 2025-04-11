using System;
using UnityEngine;

// Token: 0x02000106 RID: 262
[AddComponentMenu("VoxelEngine/Map")]
public class Map : MonoBehaviour
{
	// Token: 0x0600097D RID: 2429 RVA: 0x000824A8 File Offset: 0x000806A8
	private void Awake()
	{
		for (int i = 0; i < 4; i++)
		{
			this.flags[i] = new Flag();
		}
		int tileset = Config.Tileset;
		if (tileset == 0)
		{
			this.blockSet = ContentLoader.LoadBlockSet("Simple/Simple");
		}
		else if (tileset == 1)
		{
			this.blockSet = ContentLoader.LoadBlockSet("Real/Real");
		}
		else if (tileset == 2)
		{
			this.blockSet = ContentLoader.LoadBlockSet("Apocalypse/Apocalypse");
		}
		else if (tileset == 3)
		{
			this.blockSet = ContentLoader.LoadBlockSet("Space/Space");
		}
		else if (tileset == 4)
		{
			this.blockSet = ContentLoader.LoadBlockSet("Steampunk/Steampunk");
		}
		int dlight = Config.Dlight;
		Material[] materials = this.blockSet.GetMaterials(1);
		if (dlight == 1)
		{
			materials[0].shader = Shader.Find("Custom/StandardVertex");
			GameObject gameObject = GameObject.Find("Dlight");
			if (gameObject)
			{
				gameObject.GetComponent<Light>().enabled = true;
				return;
			}
		}
		else
		{
			materials[0].shader = Shader.Find("Custom/StandardVertex");
		}
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x00082598 File Offset: 0x00080798
	public void SetBlockAndRecompute(BlockData block, Vector3i pos)
	{
		this.SetBlock(block, pos);
		Vector3i vector3i = Chunk.ToChunkPosition(pos);
		Vector3i vector3i2 = Chunk.ToLocalPosition(pos);
		this.SetDirty(vector3i);
		if (vector3i2.x == 0)
		{
			this.SetDirty(vector3i - Vector3i.right);
		}
		if (vector3i2.y == 0)
		{
			this.SetDirty(vector3i - Vector3i.up);
		}
		if (vector3i2.z == 0)
		{
			this.SetDirty(vector3i - Vector3i.forward);
		}
		if (vector3i2.x == 7)
		{
			this.SetDirty(vector3i + Vector3i.right);
		}
		if (vector3i2.y == 7)
		{
			this.SetDirty(vector3i + Vector3i.up);
		}
		if (vector3i2.z == 7)
		{
			this.SetDirty(vector3i + Vector3i.forward);
		}
		SunLightComputer.RecomputeLightAtPosition(this, pos);
		LightComputer.RecomputeLightAtPosition(this, pos);
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x00082667 File Offset: 0x00080867
	public void BlockRecompute(Vector3i pos)
	{
		SunLightComputer.RecomputeLightAtPosition(this, pos);
		LightComputer.RecomputeLightAtPosition(this, pos);
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x00082678 File Offset: 0x00080878
	private void SetDirty(Vector3i chunkPos)
	{
		Chunk chunk = this.GetChunk(chunkPos);
		if (chunk != null)
		{
			chunk.GetChunkRendererInstance().SetDirty();
		}
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x0008269B File Offset: 0x0008089B
	public void SetBlock(Block block, Vector3i pos)
	{
		this.SetBlock(new BlockData(block), pos);
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x000826AA File Offset: 0x000808AA
	public void SetBlock(Block block, int x, int y, int z)
	{
		this.SetBlock(new BlockData(block), x, y, z);
	}

	// Token: 0x06000983 RID: 2435 RVA: 0x000826BC File Offset: 0x000808BC
	public void SetBlock(BlockData block, Vector3i pos)
	{
		this.SetBlock(block, pos.x, pos.y, pos.z);
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x000826D8 File Offset: 0x000808D8
	public void SetBlock(BlockData block, int x, int y, int z)
	{
		Chunk chunkInstance = this.GetChunkInstance(Chunk.ToChunkPosition(x, y, z));
		if (chunkInstance != null)
		{
			chunkInstance.SetBlock(block, Chunk.ToLocalPosition(x, y, z));
		}
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x00082708 File Offset: 0x00080908
	public BlockData GetBlock(Vector3i pos)
	{
		return this.GetBlock(pos.x, pos.y, pos.z);
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x00082724 File Offset: 0x00080924
	public BlockData GetBlock(int x, int y, int z)
	{
		Chunk chunk = this.GetChunk(Chunk.ToChunkPosition(x, y, z));
		if (chunk == null)
		{
			return default(BlockData);
		}
		return chunk.GetBlock(Chunk.ToLocalPosition(x, y, z));
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x0008275C File Offset: 0x0008095C
	public int GetMaxY(int x, int z)
	{
		Vector3i vector3i = Chunk.ToChunkPosition(x, 0, z);
		vector3i.y = this.chunks.GetMax().y;
		Vector3i vector3i2 = Chunk.ToLocalPosition(x, 0, z);
		while (vector3i.y >= 0)
		{
			vector3i2.y = 7;
			while (vector3i2.y >= 0)
			{
				Chunk chunk = this.chunks.SafeGet(vector3i);
				if (chunk == null)
				{
					break;
				}
				if (!chunk.GetBlock(vector3i2).IsEmpty())
				{
					return Chunk.ToWorldPosition(vector3i, vector3i2).y;
				}
				vector3i2.y--;
			}
			vector3i.y--;
		}
		return 0;
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x000827F8 File Offset: 0x000809F8
	public Chunk GetChunkInstance(Vector3i chunkPos)
	{
		if (chunkPos.y < 0)
		{
			return null;
		}
		Chunk chunk = this.GetChunk(chunkPos);
		if (chunk == null)
		{
			chunk = new Chunk(this, chunkPos);
			this.chunks.AddOrReplace(chunk, chunkPos);
		}
		return chunk;
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x00082831 File Offset: 0x00080A31
	public Chunk GetChunk(Vector3i chunkPos)
	{
		return this.chunks.SafeGet(chunkPos);
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x0008283F File Offset: 0x00080A3F
	public List3D<Chunk> GetChunks()
	{
		return this.chunks;
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x00082847 File Offset: 0x00080A47
	public SunLightMap GetSunLightmap()
	{
		return this.sunLightmap;
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x0008284F File Offset: 0x00080A4F
	public LightMap GetLightmap()
	{
		return this.lightmap;
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x00082857 File Offset: 0x00080A57
	public void SetBlockSet(BlockSet blockSet)
	{
		this.blockSet = blockSet;
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x00082860 File Offset: 0x00080A60
	public BlockSet GetBlockSet()
	{
		return this.blockSet;
	}

	// Token: 0x04000EAA RID: 3754
	public const int SIZE_X = 32;

	// Token: 0x04000EAB RID: 3755
	public const int SIZE_Y = 8;

	// Token: 0x04000EAC RID: 3756
	public const int SIZE_Z = 32;

	// Token: 0x04000EAD RID: 3757
	public const int MIN_X = 0;

	// Token: 0x04000EAE RID: 3758
	public const int MIN_Y = 0;

	// Token: 0x04000EAF RID: 3759
	public const int MIN_Z = 0;

	// Token: 0x04000EB0 RID: 3760
	public const int MAX_X = 255;

	// Token: 0x04000EB1 RID: 3761
	public const int MAX_Y = 63;

	// Token: 0x04000EB2 RID: 3762
	public const int MAX_Z = 255;

	// Token: 0x04000EB3 RID: 3763
	public Vector2 mlx = new Vector2(0f, 255f);

	// Token: 0x04000EB4 RID: 3764
	public Vector2 mly = new Vector2(0f, 63f);

	// Token: 0x04000EB5 RID: 3765
	public Vector2 mlz = new Vector2(0f, 255f);

	// Token: 0x04000EB6 RID: 3766
	public Flag[] flags = new Flag[4];

	// Token: 0x04000EB7 RID: 3767
	[SerializeField]
	private BlockSet blockSet;

	// Token: 0x04000EB8 RID: 3768
	private List3D<Chunk> chunks = new List3D<Chunk>();

	// Token: 0x04000EB9 RID: 3769
	private SunLightMap sunLightmap = new SunLightMap();

	// Token: 0x04000EBA RID: 3770
	private LightMap lightmap = new LightMap();
}
