using System;
using UnityEngine;

// Token: 0x020000EE RID: 238
public class CrossBuilder
{
	// Token: 0x060008B7 RID: 2231 RVA: 0x0007F0DF File Offset: 0x0007D2DF
	public static void Build(Vector3i localPos, Vector3i worldPos, Map map, MeshBuilder mesh, bool onlyLight)
	{
		if (!onlyLight)
		{
			CrossBuilder.BuildCross(localPos, worldPos, map, mesh);
		}
		CrossBuilder.BuildCrossLight(map, worldPos, mesh);
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x0007F0FC File Offset: 0x0007D2FC
	private static void BuildCross(Vector3 localPos, Vector3i worldPos, Map map, MeshBuilder mesh)
	{
		CrossBlock crossBlock = (CrossBlock)map.GetBlock(worldPos).block;
		mesh.AddIndices(crossBlock.GetAtlasID(), CrossBuilder.indices);
		mesh.AddVertices(CrossBuilder.vertices, localPos);
		mesh.AddNormals(CrossBuilder.normals);
		mesh.AddTexCoords(crossBlock.GetFaceUV());
		mesh.AddTexCoords(crossBlock.GetFaceUV());
		mesh.AddTexCoords(crossBlock.GetFaceUV());
		mesh.AddTexCoords(crossBlock.GetFaceUV());
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x0007F174 File Offset: 0x0007D374
	private static void BuildCrossLight(Map map, Vector3i pos, MeshBuilder mesh)
	{
		Color blockLight = BuildUtils.GetBlockLight(map, pos);
		mesh.AddColors(blockLight, CrossBuilder.vertices.Length);
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x0007F198 File Offset: 0x0007D398
	public static MeshBuilder Build(CrossBlock cross)
	{
		MeshBuilder meshBuilder = new MeshBuilder();
		meshBuilder.AddIndices(0, CrossBuilder.indices);
		meshBuilder.AddVertices(CrossBuilder.vertices, Vector3.zero);
		meshBuilder.AddNormals(CrossBuilder.normals);
		meshBuilder.AddTexCoords(cross.GetFaceUV());
		meshBuilder.AddTexCoords(cross.GetFaceUV());
		meshBuilder.AddTexCoords(cross.GetFaceUV());
		meshBuilder.AddTexCoords(cross.GetFaceUV());
		meshBuilder.AddColors(new Color(0f, 0f, 0f, 1f), CrossBuilder.vertices.Length);
		return meshBuilder;
	}

	// Token: 0x04000E4E RID: 3662
	private static Vector3[] vertices = new Vector3[]
	{
		new Vector3(-0.5f, -0.5f, -0.5f),
		new Vector3(-0.5f, 0.5f, -0.5f),
		new Vector3(0.5f, 0.5f, 0.5f),
		new Vector3(0.5f, -0.5f, 0.5f),
		new Vector3(-0.5f, -0.5f, -0.5f),
		new Vector3(-0.5f, 0.5f, -0.5f),
		new Vector3(0.5f, 0.5f, 0.5f),
		new Vector3(0.5f, -0.5f, 0.5f),
		new Vector3(-0.5f, -0.5f, 0.5f),
		new Vector3(-0.5f, 0.5f, 0.5f),
		new Vector3(0.5f, 0.5f, -0.5f),
		new Vector3(0.5f, -0.5f, -0.5f),
		new Vector3(-0.5f, -0.5f, 0.5f),
		new Vector3(-0.5f, 0.5f, 0.5f),
		new Vector3(0.5f, 0.5f, -0.5f),
		new Vector3(0.5f, -0.5f, -0.5f)
	};

	// Token: 0x04000E4F RID: 3663
	private static Vector3[] normals = new Vector3[]
	{
		new Vector3(-0.7f, 0f, 0.7f),
		new Vector3(-0.7f, 0f, 0.7f),
		new Vector3(-0.7f, 0f, 0.7f),
		new Vector3(-0.7f, 0f, 0.7f),
		-new Vector3(-0.7f, 0f, 0.7f),
		-new Vector3(-0.7f, 0f, 0.7f),
		-new Vector3(-0.7f, 0f, 0.7f),
		-new Vector3(-0.7f, 0f, 0.7f),
		new Vector3(0.7f, 0f, 0.7f),
		new Vector3(0.7f, 0f, 0.7f),
		new Vector3(0.7f, 0f, 0.7f),
		new Vector3(0.7f, 0f, 0.7f),
		-new Vector3(0.7f, 0f, 0.7f),
		-new Vector3(0.7f, 0f, 0.7f),
		-new Vector3(0.7f, 0f, 0.7f),
		-new Vector3(0.7f, 0f, 0.7f)
	};

	// Token: 0x04000E50 RID: 3664
	private static int[] indices = new int[]
	{
		2,
		1,
		0,
		3,
		2,
		0,
		4,
		6,
		7,
		4,
		5,
		6,
		10,
		9,
		8,
		11,
		10,
		8,
		12,
		14,
		15,
		12,
		13,
		14
	};
}
