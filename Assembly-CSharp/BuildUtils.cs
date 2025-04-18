﻿using System;
using UnityEngine;

// Token: 0x020000F8 RID: 248
internal class BuildUtils
{
	// Token: 0x06000906 RID: 2310 RVA: 0x00080700 File Offset: 0x0007E900
	private static Color ComputeSmoothLight(Map map, Vector3i a, Vector3i b, Vector3i c, Vector3i d)
	{
		if (map.GetBlock(b).IsAlpha() || map.GetBlock(c).IsAlpha())
		{
			Color blockLight = BuildUtils.GetBlockLight(map, a);
			Color blockLight2 = BuildUtils.GetBlockLight(map, b);
			Color blockLight3 = BuildUtils.GetBlockLight(map, c);
			Color blockLight4 = BuildUtils.GetBlockLight(map, d);
			return (blockLight + blockLight2 + blockLight3 + blockLight4) / 4f;
		}
		Color blockLight5 = BuildUtils.GetBlockLight(map, a);
		Color blockLight6 = BuildUtils.GetBlockLight(map, b);
		Color blockLight7 = BuildUtils.GetBlockLight(map, c);
		return (blockLight5 + blockLight6 + blockLight7) / 3f;
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x000807A0 File Offset: 0x0007E9A0
	public static Color GetSmoothVertexLight(Map map, Vector3i pos, Vector3 vertex, CubeFace face)
	{
		int x = (int)Mathf.Sign(vertex.x);
		int y = (int)Mathf.Sign(vertex.y);
		int z = (int)Mathf.Sign(vertex.z);
		Vector3i pos2;
		Vector3i pos3;
		Vector3i pos4;
		Vector3i pos5;
		if (face == CubeFace.Left || face == CubeFace.Right)
		{
			pos2 = pos + new Vector3i(x, 0, 0);
			pos3 = pos + new Vector3i(x, y, 0);
			pos4 = pos + new Vector3i(x, 0, z);
			pos5 = pos + new Vector3i(x, y, z);
		}
		else if (face == CubeFace.Bottom || face == CubeFace.Top)
		{
			pos2 = pos + new Vector3i(0, y, 0);
			pos3 = pos + new Vector3i(x, y, 0);
			pos4 = pos + new Vector3i(0, y, z);
			pos5 = pos + new Vector3i(x, y, z);
		}
		else
		{
			pos2 = pos + new Vector3i(0, 0, z);
			pos3 = pos + new Vector3i(x, 0, z);
			pos4 = pos + new Vector3i(0, y, z);
			pos5 = pos + new Vector3i(x, y, z);
		}
		if (map.GetBlock(pos3).IsAlpha() || map.GetBlock(pos4).IsAlpha())
		{
			Color blockLight = BuildUtils.GetBlockLight(map, pos2);
			Color blockLight2 = BuildUtils.GetBlockLight(map, pos3);
			Color blockLight3 = BuildUtils.GetBlockLight(map, pos4);
			Color blockLight4 = BuildUtils.GetBlockLight(map, pos5);
			return (blockLight + blockLight2 + blockLight3 + blockLight4) / 4f;
		}
		Color blockLight5 = BuildUtils.GetBlockLight(map, pos2);
		Color blockLight6 = BuildUtils.GetBlockLight(map, pos3);
		Color blockLight7 = BuildUtils.GetBlockLight(map, pos4);
		return (blockLight5 + blockLight6 + blockLight7) / 3f;
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x00080948 File Offset: 0x0007EB48
	public static Color GetBlockLight(Map map, Vector3i pos)
	{
		Vector3i chunkPos = Chunk.ToChunkPosition(pos);
		Vector3i localPos = Chunk.ToLocalPosition(pos);
		float num = (float)map.GetLightmap().GetLight(chunkPos, localPos) / 15f;
		float num2 = (float)map.GetSunLightmap().GetLight(chunkPos, localPos, pos.y) / 15f;
		return new Color(num, num, num, num2);
	}
}
