using System;
using System.Collections;

// Token: 0x02000117 RID: 279
public class TerrainGenerator
{
	// Token: 0x06000A0A RID: 2570 RVA: 0x0008460C File Offset: 0x0008280C
	public TerrainGenerator(Map map)
	{
		this.map = map;
		BlockSet blockSet = map.GetBlockSet();
		this.water = blockSet.GetBlock("Water");
		this.grass = blockSet.GetBlock("Grass");
		this.dirt = blockSet.GetBlock("Dirt");
		this.sand = blockSet.GetBlock("Sand");
		this.stone = blockSet.GetBlock("Grass");
		this.snow = blockSet.GetBlock("Snow");
		this.ice = blockSet.GetBlock("Snow");
		this.stoneend = blockSet.GetBlock("Stoneend");
	}

	// Token: 0x06000A0B RID: 2571 RVA: 0x00084711 File Offset: 0x00082911
	public IEnumerator Generate(int cx, int cz)
	{
		if (cx < 0 || cz < 0)
		{
			yield return null;
		}
		if (cx > 32 || cz > 32)
		{
			yield return null;
		}
		this.terrainNoise.GenerateNoise(cx * 8, cz * 8);
		this.islandNoise.GenerateNoise(cx * 8, cz * 8);
		int cy = 0;
		for (;;)
		{
			Vector3i offset = Chunk.ToWorldPosition(new Vector3i(cx, cy, cz), Vector3i.zero);
			this.terrainNoise3D.GenerateNoise(offset);
			this.islandNoise3D.GenerateNoise(offset);
			this.caveNoise3D.GenerateNoise(offset);
			if (!this.GenerateChunk(new Vector3i(cx, cy, cz)))
			{
				break;
			}
			yield return null;
			int num = cy;
			cy = num + 1;
		}
		yield break;
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x00084730 File Offset: 0x00082930
	private bool GenerateChunk(Vector3i chunkPos)
	{
		bool result = false;
		for (int i = -1; i < 9; i++)
		{
			for (int j = -1; j < 9; j++)
			{
				for (int k = 0; k < 8; k++)
				{
					Vector3i vector3i = Chunk.ToWorldPosition(chunkPos, new Vector3i(j, k, i));
					if (vector3i.y <= 2)
					{
						this.map.SetBlock(this.stoneend, vector3i);
						result = true;
					}
					if (vector3i.y <= 6)
					{
						this.map.SetBlock(this.dirt, vector3i);
						result = true;
					}
					int terrainHeight = this.GetTerrainHeight(vector3i.x, vector3i.z);
					if (vector3i.y <= terrainHeight)
					{
						this.GenerateBlockForBaseTerrain(vector3i);
						result = true;
					}
					else
					{
						int islandHeight = this.GetIslandHeight(vector3i.x, vector3i.z);
						if (vector3i.y <= islandHeight)
						{
							this.GenerateBlockForIsland(vector3i, islandHeight - vector3i.y, islandHeight);
							result = true;
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x0008482C File Offset: 0x00082A2C
	public void GeneratePlants(int cx, int cz)
	{
		for (int i = -1; i < 9; i++)
		{
			for (int j = -1; j < 9; j++)
			{
				Vector3i vector3i = new Vector3i(cx * 8 + j, 0, cz * 8 + i);
				vector3i.y = this.map.GetMaxY(vector3i.x, vector3i.z);
				while (vector3i.y >= 5)
				{
					if (this.map.GetBlock(vector3i).block == this.dirt && this.map.GetSunLightmap().GetLight(vector3i + Vector3i.up) > 5)
					{
						this.map.SetBlock(this.grass, vector3i);
					}
					vector3i.y--;
				}
			}
		}
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x000848EF File Offset: 0x00082AEF
	private int GetTerrainHeight(int x, int z)
	{
		return (int)(this.terrainNoise.GetNoise(x, z) * 10f);
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x00084905 File Offset: 0x00082B05
	private int GetIslandHeight(int x, int z)
	{
		return (int)(this.islandNoise.GetNoise(x, z) * 50f);
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x0008491C File Offset: 0x00082B1C
	private void GenerateBlockForBaseTerrain(Vector3i worldPos)
	{
		float noise = this.terrainNoise3D.GetNoise(worldPos.x, worldPos.y, worldPos.z);
		Block block = null;
		if (TerrainGenerator.IsInRange(noise, 0f, 0.2f))
		{
			block = this.sand;
		}
		if (TerrainGenerator.IsInRange(noise, 0.2f, 0.6f))
		{
			block = this.dirt;
		}
		if (TerrainGenerator.IsInRange(noise, 0.6f, 1f))
		{
			block = this.stone;
		}
		if (block != null)
		{
			this.map.SetBlock(block, worldPos);
		}
	}

	// Token: 0x06000A11 RID: 2577 RVA: 0x000849A4 File Offset: 0x00082BA4
	private void GenerateBlockForIsland(Vector3i worldPos, int deep, int height)
	{
		if (this.caveNoise3D.GetNoise(worldPos.x, worldPos.y, worldPos.z) > 0.7f)
		{
			return;
		}
		float noise = this.islandNoise3D.GetNoise(worldPos.x, worldPos.y, worldPos.z);
		Block block = null;
		if (TerrainGenerator.IsInRange(noise, 0f, 0.2f))
		{
			block = this.sand;
		}
		if (TerrainGenerator.IsInRange(noise, 0.2f, 0.6f))
		{
			block = this.dirt;
		}
		if (TerrainGenerator.IsInRange(noise, 0.6f, 1f))
		{
			block = this.stone;
		}
		if (block != null)
		{
			this.map.SetBlock(block, worldPos);
		}
	}

	// Token: 0x06000A12 RID: 2578 RVA: 0x00084A4F File Offset: 0x00082C4F
	private static bool IsInRange(float val, float min, float max)
	{
		return val >= min && val <= max;
	}

	// Token: 0x04000EF5 RID: 3829
	private const int WATER_LEVEL = 5;

	// Token: 0x04000EF6 RID: 3830
	private NoiseArray2D terrainNoise = new NoiseArray2D(0.02f).SetOctaves(1);

	// Token: 0x04000EF7 RID: 3831
	private NoiseArray3D terrainNoise3D = new NoiseArray3D(0.033333335f);

	// Token: 0x04000EF8 RID: 3832
	private NoiseArray2D islandNoise = new NoiseArray2D(0.006666667f).SetOctaves(3);

	// Token: 0x04000EF9 RID: 3833
	private NoiseArray3D islandNoise3D = new NoiseArray3D(0.033333335f);

	// Token: 0x04000EFA RID: 3834
	private NoiseArray3D caveNoise3D = new NoiseArray3D(1f);

	// Token: 0x04000EFB RID: 3835
	private Map map;

	// Token: 0x04000EFC RID: 3836
	private Block water;

	// Token: 0x04000EFD RID: 3837
	private Block grass;

	// Token: 0x04000EFE RID: 3838
	private Block dirt;

	// Token: 0x04000EFF RID: 3839
	private Block sand;

	// Token: 0x04000F00 RID: 3840
	private Block stone;

	// Token: 0x04000F01 RID: 3841
	private Block snow;

	// Token: 0x04000F02 RID: 3842
	private Block ice;

	// Token: 0x04000F03 RID: 3843
	private Block stoneend;
}
