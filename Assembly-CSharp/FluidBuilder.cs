using System;
using UnityEngine;

// Token: 0x020000F5 RID: 245
public class FluidBuilder
{
	// Token: 0x060008E5 RID: 2277 RVA: 0x0007FF34 File Offset: 0x0007E134
	public static void Build(Vector3i localPos, Vector3i worldPos, Map map, MeshBuilder mesh, bool onlyLight)
	{
		FluidBlock fluid = (FluidBlock)map.GetBlock(worldPos).block;
		for (int i = 0; i < 6; i++)
		{
			CubeFace face = CubeBuilder.faces[i];
			Vector3i b = CubeBuilder.directions[i];
			Vector3i nearPos = worldPos + b;
			if (FluidBuilder.IsFaceVisible(map, nearPos, face))
			{
				if (!onlyLight)
				{
					FluidBuilder.BuildFace(face, fluid, localPos, mesh);
				}
				FluidBuilder.BuildFaceLight(face, map, worldPos, mesh);
			}
		}
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x0007FFA4 File Offset: 0x0007E1A4
	private static bool IsFaceVisible(Map map, Vector3i nearPos, CubeFace face)
	{
		if (face == CubeFace.Top)
		{
			BlockData block = map.GetBlock(nearPos);
			return block.IsEmpty() || !block.IsFluid();
		}
		return map.GetBlock(nearPos).IsEmpty();
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x0007FFE4 File Offset: 0x0007E1E4
	private static void BuildFace(CubeFace face, FluidBlock fluid, Vector3 localPos, MeshBuilder mesh)
	{
		mesh.AddFaceIndices(fluid.GetAtlasID());
		mesh.AddVertices(CubeBuilder.vertices[(int)face], localPos);
		mesh.AddNormals(CubeBuilder.normals[(int)face]);
		mesh.AddTexCoords(fluid.GetFaceUV());
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x00080028 File Offset: 0x0007E228
	private static void BuildFaceLight(CubeFace face, Map map, Vector3i pos, MeshBuilder mesh)
	{
		Vector3i b = CubeBuilder.directions[(int)face];
		Color blockLight = BuildUtils.GetBlockLight(map, pos + b);
		mesh.AddFaceColor(blockLight);
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x00080058 File Offset: 0x0007E258
	public static MeshBuilder Build(FluidBlock fluid)
	{
		MeshBuilder meshBuilder = new MeshBuilder();
		for (int i = 0; i < CubeBuilder.vertices.Length; i++)
		{
			meshBuilder.AddFaceIndices(0);
			meshBuilder.AddVertices(CubeBuilder.vertices[i], Vector3.zero);
			meshBuilder.AddNormals(CubeBuilder.normals[i]);
			meshBuilder.AddTexCoords(fluid.GetFaceUV());
			meshBuilder.AddFaceColor(new Color(0f, 0f, 0f, 1f));
		}
		return meshBuilder;
	}
}
