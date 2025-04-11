using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000104 RID: 260
public class ChunkSunLightComputer
{
	// Token: 0x0600096A RID: 2410 RVA: 0x000820C0 File Offset: 0x000802C0
	public static void ComputeRays(Map map, int cx, int cz)
	{
		int num = cx * 8 - 8;
		int num2 = cz * 8 - 8;
		int num3 = num + 24;
		int num4 = num2 + 24;
		for (int i = num2; i < num4; i++)
		{
			for (int j = num; j < num3; j++)
			{
				SunLightComputer.ComputeRayAtPosition(map, j, i);
			}
		}
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x00082108 File Offset: 0x00080308
	public static void Scatter(Map map, ColumnMap columnMap, int cx, int cz)
	{
		int num = cx * 8;
		int num2 = cz * 8;
		int num3 = num + 8;
		int num4 = num2 + 8;
		SunLightMap sunLightmap = map.GetSunLightmap();
		ChunkSunLightComputer.list.Clear();
		for (int i = num; i < num3; i++)
		{
			for (int j = num2; j < num4; j++)
			{
				int num5 = ChunkSunLightComputer.ComputeMaxY(sunLightmap, i, j) + 1;
				for (int k = 0; k < num5; k++)
				{
					if (sunLightmap.GetLight(i, k, j) > 5)
					{
						ChunkSunLightComputer.list.Add(new Vector3i(i, k, j));
					}
				}
			}
		}
		ChunkSunLightComputer.Scatter(map, columnMap, ChunkSunLightComputer.list);
	}

	// Token: 0x0600096C RID: 2412 RVA: 0x000821A4 File Offset: 0x000803A4
	private static void Scatter(Map map, ColumnMap columnMap, List<Vector3i> list)
	{
		SunLightMap sunLightmap = map.GetSunLightmap();
		for (int i = 0; i < list.Count; i++)
		{
			Vector3i vector3i = list[i];
			if (vector3i.y >= 0)
			{
				BlockData block = map.GetBlock(vector3i);
				int num = (int)sunLightmap.GetLight(vector3i) - LightComputerUtils.GetLightStep(block);
				if (num > 5)
				{
					Vector3i vector3i2 = Chunk.ToChunkPosition(vector3i);
					if (columnMap.IsBuilt(vector3i2.x, vector3i2.z))
					{
						foreach (Vector3i b in Vector3i.directions)
						{
							Vector3i vector3i3 = vector3i + b;
							block = map.GetBlock(vector3i3);
							if (block.IsAlpha() && sunLightmap.SetMaxLight((byte)num, vector3i3))
							{
								list.Add(vector3i3);
							}
							if (!block.IsEmpty())
							{
								LightComputerUtils.SetLightDirty(map, vector3i3);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600096D RID: 2413 RVA: 0x00082288 File Offset: 0x00080488
	private static int ComputeMaxY(SunLightMap lightmap, int x, int z)
	{
		return Mathf.Max(Mathf.Max(Mathf.Max(Mathf.Max(lightmap.GetSunHeight(x, z), lightmap.GetSunHeight(x - 1, z)), lightmap.GetSunHeight(x + 1, z)), lightmap.GetSunHeight(x, z - 1)), lightmap.GetSunHeight(x, z + 1));
	}

	// Token: 0x04000EA4 RID: 3748
	private const byte MIN_LIGHT = 5;

	// Token: 0x04000EA5 RID: 3749
	private const byte MAX_LIGHT = 15;

	// Token: 0x04000EA6 RID: 3750
	private const byte STEP_LIGHT = 1;

	// Token: 0x04000EA7 RID: 3751
	private static List<Vector3i> list = new List<Vector3i>();
}
