using System;
using UnityEngine;

// Token: 0x020000F0 RID: 240
public class FluidBlock : Block
{
	// Token: 0x060008C4 RID: 2244 RVA: 0x0007FC8C File Offset: 0x0007DE8C
	public override void Init(BlockSet blockSet)
	{
		base.Init(blockSet);
		this.texCoords = base.ToTexCoords(this.face);
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x0007FCA7 File Offset: 0x0007DEA7
	public override Rect GetPreviewFace()
	{
		return base.ToRect(this.face);
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x0007FCA7 File Offset: 0x0007DEA7
	public override Rect GetTopFace()
	{
		return base.ToRect(this.face);
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x0007FCB5 File Offset: 0x0007DEB5
	public Vector2[] GetFaceUV()
	{
		return this.texCoords;
	}

	// Token: 0x060008C8 RID: 2248 RVA: 0x0007FCBD File Offset: 0x0007DEBD
	public override void Build(Vector3i localPos, Vector3i worldPos, Map map, MeshBuilder mesh, bool onlyLight)
	{
		FluidBuilder.Build(localPos, worldPos, map, mesh, onlyLight);
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x0007FCCB File Offset: 0x0007DECB
	public override MeshBuilder Build()
	{
		return FluidBuilder.Build(this);
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x0007FCD3 File Offset: 0x0007DED3
	public override bool IsSolid()
	{
		return false;
	}

	// Token: 0x04000E55 RID: 3669
	[SerializeField]
	private int face;

	// Token: 0x04000E56 RID: 3670
	private Vector2[] texCoords;
}
