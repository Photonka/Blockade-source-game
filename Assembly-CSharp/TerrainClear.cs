using System;

// Token: 0x0200011A RID: 282
public class TerrainClear
{
	// Token: 0x06000A1F RID: 2591 RVA: 0x00084E14 File Offset: 0x00083014
	public TerrainClear(Map map)
	{
		this.map = map;
		BlockSet blockSet = map.GetBlockSet();
		this.water = blockSet.GetBlock("Water");
		this.grass = blockSet.GetBlock("Grass");
		this.dirt = blockSet.GetBlock("Dirt");
		this.sand = blockSet.GetBlock("Sand");
		this.stoneend = blockSet.GetBlock("Stoneend");
		this.snow = blockSet.GetBlock("Snow");
		this.ice = blockSet.GetBlock("Ice");
	}

	// Token: 0x06000A20 RID: 2592 RVA: 0x00084F08 File Offset: 0x00083108
	public void GenerateChunk(int cx, int cy, int cz)
	{
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				for (int k = 0; k < 7; k++)
				{
					Vector3i pos = Chunk.ToWorldPosition(new Vector3i(cx, cy, cz), new Vector3i(j, k, i));
					if (k < 2)
					{
						this.map.SetBlock(this.stoneend, pos);
					}
					else if (k < 6)
					{
						this.map.SetBlock(this.dirt, pos);
					}
					else
					{
						this.map.SetBlock(this.grass, pos);
					}
				}
			}
		}
	}

	// Token: 0x04000F0C RID: 3852
	private const int WATER_LEVEL = -999;

	// Token: 0x04000F0D RID: 3853
	private NoiseArray2D terrainNoise = new NoiseArray2D(0.02f).SetOctaves(1);

	// Token: 0x04000F0E RID: 3854
	private NoiseArray3D terrainNoise3D = new NoiseArray3D(0.033333335f);

	// Token: 0x04000F0F RID: 3855
	private NoiseArray2D islandNoise = new NoiseArray2D(0.008333334f).SetOctaves(3);

	// Token: 0x04000F10 RID: 3856
	private NoiseArray3D islandNoise3D = new NoiseArray3D(0.025f);

	// Token: 0x04000F11 RID: 3857
	private NoiseArray3D caveNoise3D = new NoiseArray3D(0.02f);

	// Token: 0x04000F12 RID: 3858
	private Map map;

	// Token: 0x04000F13 RID: 3859
	private Block water;

	// Token: 0x04000F14 RID: 3860
	private Block grass;

	// Token: 0x04000F15 RID: 3861
	private Block dirt;

	// Token: 0x04000F16 RID: 3862
	private Block sand;

	// Token: 0x04000F17 RID: 3863
	private Block stoneend;

	// Token: 0x04000F18 RID: 3864
	private Block snow;

	// Token: 0x04000F19 RID: 3865
	private Block ice;
}
