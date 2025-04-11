using System;
using UnityEngine;

// Token: 0x020000ED RID: 237
public class CactusBuilder
{
	// Token: 0x060008B0 RID: 2224 RVA: 0x0007EAFC File Offset: 0x0007CCFC
	public static void Build(Vector3i localPos, Vector3i worldPos, Map map, MeshBuilder mesh, bool onlyLight)
	{
		CactusBlock cactus = (CactusBlock)map.GetBlock(worldPos).block;
		for (int i = 0; i < 6; i++)
		{
			CubeFace face = CubeBuilder.faces[i];
			Vector3i b = CubeBuilder.directions[i];
			Vector3i nearPos = worldPos + b;
			if (CactusBuilder.IsFaceVisible(map, face, nearPos))
			{
				if (!onlyLight)
				{
					CactusBuilder.BuildFace(face, cactus, localPos, mesh);
				}
				CactusBuilder.BuildFaceLight(face, map, worldPos, mesh);
			}
		}
	}

	// Token: 0x060008B1 RID: 2225 RVA: 0x0007EB6C File Offset: 0x0007CD6C
	private static bool IsFaceVisible(Map map, CubeFace face, Vector3i nearPos)
	{
		if (face == CubeFace.Bottom || face == CubeFace.Top)
		{
			Block block = map.GetBlock(nearPos).block;
			if (block is CubeBlock && !block.IsAlpha())
			{
				return false;
			}
			if (block is CactusBlock)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x0007EBAC File Offset: 0x0007CDAC
	private static void BuildFace(CubeFace face, CactusBlock cactus, Vector3 localPos, MeshBuilder mesh)
	{
		mesh.AddFaceIndices(cactus.GetAtlasID());
		mesh.AddVertices(CactusBuilder.vertices[(int)face], localPos);
		mesh.AddNormals(CactusBuilder.normals[(int)face]);
		mesh.AddTexCoords(cactus.GetFaceUV(face));
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x0007EBF0 File Offset: 0x0007CDF0
	private static void BuildFaceLight(CubeFace face, Map map, Vector3i pos, MeshBuilder mesh)
	{
		foreach (Vector3 vertex in CactusBuilder.vertices[(int)face])
		{
			Color smoothVertexLight = BuildUtils.GetSmoothVertexLight(map, pos, vertex, face);
			mesh.AddColor(smoothVertexLight);
		}
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x0007EC2C File Offset: 0x0007CE2C
	public static MeshBuilder Build(CactusBlock cactus)
	{
		MeshBuilder meshBuilder = new MeshBuilder();
		for (int i = 0; i < CactusBuilder.vertices.Length; i++)
		{
			meshBuilder.AddFaceIndices(0);
			meshBuilder.AddVertices(CactusBuilder.vertices[i], Vector3.zero);
			meshBuilder.AddNormals(CactusBuilder.normals[i]);
			Vector2[] faceUV = cactus.GetFaceUV((CubeFace)i);
			meshBuilder.AddTexCoords(faceUV);
			meshBuilder.AddFaceColor(new Color(0f, 0f, 0f, 1f));
		}
		return meshBuilder;
	}

	// Token: 0x04000E4C RID: 3660
	private static Vector3[][] vertices = new Vector3[][]
	{
		new Vector3[]
		{
			new Vector3(-0.5f, -0.5f, 0.4375f),
			new Vector3(-0.5f, 0.5f, 0.4375f),
			new Vector3(0.5f, 0.5f, 0.4375f),
			new Vector3(0.5f, -0.5f, 0.4375f)
		},
		new Vector3[]
		{
			new Vector3(0.5f, -0.5f, -0.4375f),
			new Vector3(0.5f, 0.5f, -0.4375f),
			new Vector3(-0.5f, 0.5f, -0.4375f),
			new Vector3(-0.5f, -0.5f, -0.4375f)
		},
		new Vector3[]
		{
			new Vector3(0.4375f, -0.5f, 0.5f),
			new Vector3(0.4375f, 0.5f, 0.5f),
			new Vector3(0.4375f, 0.5f, -0.5f),
			new Vector3(0.4375f, -0.5f, -0.5f)
		},
		new Vector3[]
		{
			new Vector3(-0.4375f, -0.5f, -0.5f),
			new Vector3(-0.4375f, 0.5f, -0.5f),
			new Vector3(-0.4375f, 0.5f, 0.5f),
			new Vector3(-0.4375f, -0.5f, 0.5f)
		},
		new Vector3[]
		{
			new Vector3(0.5f, 0.5f, -0.5f),
			new Vector3(0.5f, 0.5f, 0.5f),
			new Vector3(-0.5f, 0.5f, 0.5f),
			new Vector3(-0.5f, 0.5f, -0.5f)
		},
		new Vector3[]
		{
			new Vector3(-0.5f, -0.5f, -0.5f),
			new Vector3(-0.5f, -0.5f, 0.5f),
			new Vector3(0.5f, -0.5f, 0.5f),
			new Vector3(0.5f, -0.5f, -0.5f)
		}
	};

	// Token: 0x04000E4D RID: 3661
	private static Vector3[][] normals = new Vector3[][]
	{
		new Vector3[]
		{
			Vector3.forward,
			Vector3.forward,
			Vector3.forward,
			Vector3.forward
		},
		new Vector3[]
		{
			Vector3.back,
			Vector3.back,
			Vector3.back,
			Vector3.back
		},
		new Vector3[]
		{
			Vector3.right,
			Vector3.right,
			Vector3.right,
			Vector3.right
		},
		new Vector3[]
		{
			Vector3.left,
			Vector3.left,
			Vector3.left,
			Vector3.left
		},
		new Vector3[]
		{
			Vector3.up,
			Vector3.up,
			Vector3.up,
			Vector3.up
		},
		new Vector3[]
		{
			Vector3.down,
			Vector3.down,
			Vector3.down,
			Vector3.down
		}
	};
}
