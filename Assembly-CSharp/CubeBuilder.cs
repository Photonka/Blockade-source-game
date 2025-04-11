using System;
using UnityEngine;

// Token: 0x020000EF RID: 239
public class CubeBuilder
{
	// Token: 0x060008BD RID: 2237 RVA: 0x0007F5FC File Offset: 0x0007D7FC
	public static void Build(Vector3i localPos, Vector3i worldPos, Map map, MeshBuilder mesh, bool onlyLight)
	{
		BlockData block = map.GetBlock(worldPos);
		CubeBlock cube = (CubeBlock)block.block;
		BlockDirection direction = block.GetDirection();
		for (int i = 0; i < 6; i++)
		{
			CubeFace face = CubeBuilder.faces[i];
			Vector3i b = CubeBuilder.directions[i];
			Vector3i nearPos = worldPos + b;
			if (CubeBuilder.IsFaceVisible(map, nearPos))
			{
				if (!onlyLight)
				{
					CubeBuilder.BuildFace(face, cube, direction, localPos, mesh);
				}
				CubeBuilder.BuildFaceLight(face, map, worldPos, mesh);
			}
		}
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x0007F67C File Offset: 0x0007D87C
	private static bool IsFaceVisible(Map map, Vector3i nearPos)
	{
		if (nearPos.x > 255 || nearPos.x < 0)
		{
			return false;
		}
		if (nearPos.y > 63 || nearPos.y < 0)
		{
			return false;
		}
		if (nearPos.z > 255 || nearPos.z < 0)
		{
			return false;
		}
		Block block = map.GetBlock(nearPos).block;
		return !(block is CubeBlock) || block.IsAlpha();
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x0007F6EC File Offset: 0x0007D8EC
	private static void BuildFace(CubeFace face, CubeBlock cube, BlockDirection direction, Vector3 localPos, MeshBuilder mesh)
	{
		mesh.AddFaceIndices(cube.GetAtlasID());
		mesh.AddVertices(CubeBuilder.vertices[(int)face], localPos);
		mesh.AddNormals(CubeBuilder.normals[(int)face]);
		mesh.AddTexCoords(cube.GetFaceUV(face, direction));
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x0007F734 File Offset: 0x0007D934
	private static void BuildFaceLight(CubeFace face, Map map, Vector3i pos, MeshBuilder mesh)
	{
		foreach (Vector3 vertex in CubeBuilder.vertices[(int)face])
		{
			Color smoothVertexLight = BuildUtils.GetSmoothVertexLight(map, pos, vertex, face);
			mesh.AddColor(smoothVertexLight);
		}
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x0007F770 File Offset: 0x0007D970
	public static MeshBuilder Build(CubeBlock cube)
	{
		MeshBuilder meshBuilder = new MeshBuilder();
		for (int i = 0; i < CubeBuilder.vertices.Length; i++)
		{
			meshBuilder.AddFaceIndices(0);
			meshBuilder.AddVertices(CubeBuilder.vertices[i], Vector3.zero);
			meshBuilder.AddNormals(CubeBuilder.normals[i]);
			Vector2[] faceUV = cube.GetFaceUV((CubeFace)i, BlockDirection.Z_PLUS);
			meshBuilder.AddTexCoords(faceUV);
			meshBuilder.AddFaceColor(new Color(0f, 0f, 0f, 1f));
		}
		return meshBuilder;
	}

	// Token: 0x04000E51 RID: 3665
	public static CubeFace[] faces = new CubeFace[]
	{
		CubeFace.Front,
		CubeFace.Back,
		CubeFace.Right,
		CubeFace.Left,
		CubeFace.Top,
		CubeFace.Bottom
	};

	// Token: 0x04000E52 RID: 3666
	public static Vector3i[] directions = new Vector3i[]
	{
		Vector3i.forward,
		Vector3i.back,
		Vector3i.right,
		Vector3i.left,
		Vector3i.up,
		Vector3i.down
	};

	// Token: 0x04000E53 RID: 3667
	public static Vector3[][] vertices = new Vector3[][]
	{
		new Vector3[]
		{
			new Vector3(-0.5f, -0.5f, 0.5f),
			new Vector3(-0.5f, 0.5f, 0.5f),
			new Vector3(0.5f, 0.5f, 0.5f),
			new Vector3(0.5f, -0.5f, 0.5f)
		},
		new Vector3[]
		{
			new Vector3(0.5f, -0.5f, -0.5f),
			new Vector3(0.5f, 0.5f, -0.5f),
			new Vector3(-0.5f, 0.5f, -0.5f),
			new Vector3(-0.5f, -0.5f, -0.5f)
		},
		new Vector3[]
		{
			new Vector3(0.5f, -0.5f, 0.5f),
			new Vector3(0.5f, 0.5f, 0.5f),
			new Vector3(0.5f, 0.5f, -0.5f),
			new Vector3(0.5f, -0.5f, -0.5f)
		},
		new Vector3[]
		{
			new Vector3(-0.5f, -0.5f, -0.5f),
			new Vector3(-0.5f, 0.5f, -0.5f),
			new Vector3(-0.5f, 0.5f, 0.5f),
			new Vector3(-0.5f, -0.5f, 0.5f)
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

	// Token: 0x04000E54 RID: 3668
	public static Vector3[][] normals = new Vector3[][]
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
