using System;
using UnityEngine;

// Token: 0x020000F4 RID: 244
public class CubeBlock : Block
{
	// Token: 0x060008DC RID: 2268 RVA: 0x0007FDF4 File Offset: 0x0007DFF4
	public override void Init(BlockSet blockSet)
	{
		base.Init(blockSet);
		this.texCoords = new Vector2[][]
		{
			base.ToTexCoords(this.front),
			base.ToTexCoords(this.back),
			base.ToTexCoords(this.right),
			base.ToTexCoords(this.left),
			base.ToTexCoords(this.top),
			base.ToTexCoords(this.bottom)
		};
	}

	// Token: 0x060008DD RID: 2269 RVA: 0x0007FE6E File Offset: 0x0007E06E
	public override Rect GetPreviewFace()
	{
		return base.ToRect(this.front);
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x0007FE7C File Offset: 0x0007E07C
	public override Rect GetTopFace()
	{
		return base.ToRect(this.top);
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x0007FE8A File Offset: 0x0007E08A
	public Vector2[] GetFaceUV(CubeFace face, BlockDirection dir)
	{
		face = CubeBlock.TransformFace(face, dir);
		return this.texCoords[(int)face];
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x0007FEA0 File Offset: 0x0007E0A0
	public static CubeFace TransformFace(CubeFace face, BlockDirection dir)
	{
		if (face == CubeFace.Top || face == CubeFace.Bottom)
		{
			return face;
		}
		int num = 0;
		if (face == CubeFace.Right)
		{
			num = 90;
		}
		if (face == CubeFace.Back)
		{
			num = 180;
		}
		if (face == CubeFace.Left)
		{
			num = 270;
		}
		if (dir == BlockDirection.X_MINUS)
		{
			num += 90;
		}
		if (dir == BlockDirection.Z_MINUS)
		{
			num += 180;
		}
		if (dir == BlockDirection.X_PLUS)
		{
			num += 270;
		}
		num %= 360;
		if (num == 0)
		{
			return CubeFace.Front;
		}
		if (num == 90)
		{
			return CubeFace.Right;
		}
		if (num == 180)
		{
			return CubeFace.Back;
		}
		if (num == 270)
		{
			return CubeFace.Left;
		}
		return CubeFace.Front;
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x0007FF1E File Offset: 0x0007E11E
	public override void Build(Vector3i localPos, Vector3i worldPos, Map map, MeshBuilder mesh, bool onlyLight)
	{
		CubeBuilder.Build(localPos, worldPos, map, mesh, onlyLight);
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x0007FF2C File Offset: 0x0007E12C
	public override MeshBuilder Build()
	{
		return CubeBuilder.Build(this);
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x0006CF70 File Offset: 0x0006B170
	public override bool IsSolid()
	{
		return true;
	}

	// Token: 0x04000E66 RID: 3686
	[SerializeField]
	private int front;

	// Token: 0x04000E67 RID: 3687
	[SerializeField]
	private int back;

	// Token: 0x04000E68 RID: 3688
	[SerializeField]
	private int right;

	// Token: 0x04000E69 RID: 3689
	[SerializeField]
	private int left;

	// Token: 0x04000E6A RID: 3690
	[SerializeField]
	private int top;

	// Token: 0x04000E6B RID: 3691
	[SerializeField]
	private int bottom;

	// Token: 0x04000E6C RID: 3692
	private Vector2[][] texCoords;
}
