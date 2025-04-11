using System;
using UnityEngine;

// Token: 0x020000F1 RID: 241
public class CactusBlock : Block
{
	// Token: 0x060008CC RID: 2252 RVA: 0x0007FCDE File Offset: 0x0007DEDE
	public override void Init(BlockSet blockSet)
	{
		base.Init(blockSet);
		this._side = base.ToTexCoords(this.side);
		this._top = base.ToTexCoords(this.top);
		this._bottom = base.ToTexCoords(this.bottom);
	}

	// Token: 0x060008CD RID: 2253 RVA: 0x0007FD1D File Offset: 0x0007DF1D
	public override Rect GetPreviewFace()
	{
		return base.ToRect(this.side);
	}

	// Token: 0x060008CE RID: 2254 RVA: 0x0007FD2B File Offset: 0x0007DF2B
	public override Rect GetTopFace()
	{
		return base.ToRect(this.top);
	}

	// Token: 0x060008CF RID: 2255 RVA: 0x0007FD3C File Offset: 0x0007DF3C
	public Vector2[] GetFaceUV(CubeFace face)
	{
		switch (face)
		{
		case CubeFace.Front:
			return this._side;
		case CubeFace.Back:
			return this._side;
		case CubeFace.Right:
			return this._side;
		case CubeFace.Left:
			return this._side;
		case CubeFace.Top:
			return this._top;
		case CubeFace.Bottom:
			return this._bottom;
		default:
			return null;
		}
	}

	// Token: 0x060008D0 RID: 2256 RVA: 0x0007FD94 File Offset: 0x0007DF94
	public override void Build(Vector3i localPos, Vector3i worldPos, Map map, MeshBuilder mesh, bool onlyLight)
	{
		CactusBuilder.Build(localPos, worldPos, map, mesh, onlyLight);
	}

	// Token: 0x060008D1 RID: 2257 RVA: 0x0007FDA2 File Offset: 0x0007DFA2
	public override MeshBuilder Build()
	{
		return CactusBuilder.Build(this);
	}

	// Token: 0x060008D2 RID: 2258 RVA: 0x0006CF70 File Offset: 0x0006B170
	public override bool IsSolid()
	{
		return true;
	}

	// Token: 0x04000E57 RID: 3671
	[SerializeField]
	private int side;

	// Token: 0x04000E58 RID: 3672
	[SerializeField]
	private int top;

	// Token: 0x04000E59 RID: 3673
	[SerializeField]
	private int bottom;

	// Token: 0x04000E5A RID: 3674
	private Vector2[] _side;

	// Token: 0x04000E5B RID: 3675
	private Vector2[] _top;

	// Token: 0x04000E5C RID: 3676
	private Vector2[] _bottom;
}
