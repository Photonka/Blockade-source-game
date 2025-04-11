using System;
using UnityEngine;

// Token: 0x020000F2 RID: 242
public class CrossBlock : Block
{
	// Token: 0x060008D4 RID: 2260 RVA: 0x0007FDAA File Offset: 0x0007DFAA
	public override void Init(BlockSet blockSet)
	{
		base.Init(blockSet);
		this._face = base.ToTexCoords(this.face);
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x0007FDC5 File Offset: 0x0007DFC5
	public override Rect GetPreviewFace()
	{
		return base.ToRect(this.face);
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x0007FDC5 File Offset: 0x0007DFC5
	public override Rect GetTopFace()
	{
		return base.ToRect(this.face);
	}

	// Token: 0x060008D7 RID: 2263 RVA: 0x0007FDD3 File Offset: 0x0007DFD3
	public Vector2[] GetFaceUV()
	{
		return this._face;
	}

	// Token: 0x060008D8 RID: 2264 RVA: 0x0007FDDB File Offset: 0x0007DFDB
	public override void Build(Vector3i localPos, Vector3i worldPos, Map map, MeshBuilder mesh, bool onlyLight)
	{
		CrossBuilder.Build(localPos, worldPos, map, mesh, onlyLight);
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x0007FDE9 File Offset: 0x0007DFE9
	public override MeshBuilder Build()
	{
		return CrossBuilder.Build(this);
	}

	// Token: 0x060008DA RID: 2266 RVA: 0x0007FCD3 File Offset: 0x0007DED3
	public override bool IsSolid()
	{
		return false;
	}

	// Token: 0x04000E5D RID: 3677
	[SerializeField]
	private int face;

	// Token: 0x04000E5E RID: 3678
	private Vector2[] _face;
}
