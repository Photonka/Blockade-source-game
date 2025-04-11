using System;
using System.Collections.Generic;

// Token: 0x02000100 RID: 256
public class LightComputer
{
	// Token: 0x0600094F RID: 2383 RVA: 0x0008184C File Offset: 0x0007FA4C
	public static void RecomputeLightAtPosition(Map map, Vector3i pos)
	{
		int light = (int)map.GetLightmap().GetLight(pos);
		int light2 = (int)map.GetBlock(pos).GetLight();
		if (light > light2)
		{
			LightComputer.RemoveLight(map, pos);
		}
		if (light2 > 5)
		{
			LightComputer.Scatter(map, pos);
		}
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x0008188A File Offset: 0x0007FA8A
	private static void Scatter(Map map, Vector3i pos)
	{
		LightComputer.list.Clear();
		LightComputer.list.Add(pos);
		LightComputer.Scatter(map, LightComputer.list);
	}

	// Token: 0x06000951 RID: 2385 RVA: 0x000818AC File Offset: 0x0007FAAC
	private static void Scatter(Map map, List<Vector3i> list)
	{
		LightMap lightmap = map.GetLightmap();
		foreach (Vector3i pos in list)
		{
			byte light = map.GetBlock(pos).GetLight();
			if (light > 5)
			{
				lightmap.SetMaxLight(light, pos);
			}
		}
		for (int i = 0; i < list.Count; i++)
		{
			Vector3i vector3i = list[i];
			if (vector3i.y >= 0)
			{
				BlockData block = map.GetBlock(vector3i);
				int num = (int)lightmap.GetLight(vector3i) - LightComputerUtils.GetLightStep(block);
				if (num > 5)
				{
					foreach (Vector3i b in Vector3i.directions)
					{
						Vector3i vector3i2 = vector3i + b;
						block = map.GetBlock(vector3i2);
						if (block.IsAlpha() && lightmap.SetMaxLight((byte)num, vector3i2))
						{
							list.Add(vector3i2);
						}
						if (!block.IsEmpty())
						{
							LightComputerUtils.SetLightDirty(map, vector3i2);
						}
					}
				}
			}
		}
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x000819D4 File Offset: 0x0007FBD4
	private static void RemoveLight(Map map, Vector3i pos)
	{
		LightComputer.list.Clear();
		LightComputer.list.Add(pos);
		LightComputer.RemoveLight(map, LightComputer.list);
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x000819F8 File Offset: 0x0007FBF8
	private static void RemoveLight(Map map, List<Vector3i> list)
	{
		LightMap lightmap = map.GetLightmap();
		foreach (Vector3i pos in list)
		{
			lightmap.SetLight(15, pos);
		}
		List<Vector3i> list2 = new List<Vector3i>();
		for (int i = 0; i < list.Count; i++)
		{
			Vector3i vector3i = list[i];
			if (vector3i.y >= 0)
			{
				int num = (int)(lightmap.GetLight(vector3i) - 1);
				lightmap.SetLight(5, vector3i);
				if (num > 5)
				{
					foreach (Vector3i b in Vector3i.directions)
					{
						Vector3i vector3i2 = vector3i + b;
						BlockData block = map.GetBlock(vector3i2);
						if (block.IsAlpha())
						{
							if ((int)lightmap.GetLight(vector3i2) <= num)
							{
								list.Add(vector3i2);
							}
							else
							{
								list2.Add(vector3i2);
							}
						}
						if (block.GetLight() > 5)
						{
							list2.Add(vector3i2);
						}
						if (!block.IsEmpty())
						{
							LightComputerUtils.SetLightDirty(map, vector3i2);
						}
					}
				}
			}
		}
		LightComputer.Scatter(map, list2);
	}

	// Token: 0x04000E9B RID: 3739
	public const byte MIN_LIGHT = 5;

	// Token: 0x04000E9C RID: 3740
	public const byte MAX_LIGHT = 15;

	// Token: 0x04000E9D RID: 3741
	public const byte STEP_LIGHT = 1;

	// Token: 0x04000E9E RID: 3742
	private static List<Vector3i> list = new List<Vector3i>();
}
