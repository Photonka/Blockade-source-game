using System;
using UnityEngine;

// Token: 0x020000EC RID: 236
public abstract class Block
{
	// Token: 0x0600089D RID: 2205 RVA: 0x0007E971 File Offset: 0x0007CB71
	public virtual void Init(BlockSet blockSet)
	{
		this._atlas = blockSet.GetAtlas(this.atlas);
		if (this._atlas != null)
		{
			this.alpha = this._atlas.IsAlpha();
		}
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x0007E9A0 File Offset: 0x0007CBA0
	public Rect ToRect(int pos)
	{
		if (this._atlas != null)
		{
			return this._atlas.ToRect(pos);
		}
		return default(Rect);
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x0007E9CC File Offset: 0x0007CBCC
	protected Vector2[] ToTexCoords(int pos)
	{
		Rect rect = this.ToRect(pos);
		return new Vector2[]
		{
			new Vector2(rect.xMax, rect.yMin),
			new Vector2(rect.xMax, rect.yMax),
			new Vector2(rect.xMin, rect.yMax),
			new Vector2(rect.xMin, rect.yMin)
		};
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x0007EA50 File Offset: 0x0007CC50
	public bool DrawPreview(Rect position)
	{
		Texture texture = this.GetTexture();
		if (texture != null)
		{
			GUI.DrawTextureWithTexCoords(position, texture, this.GetPreviewFace());
		}
		return Event.current.type == null && position.Contains(Event.current.mousePosition);
	}

	// Token: 0x060008A1 RID: 2209
	public abstract Rect GetPreviewFace();

	// Token: 0x060008A2 RID: 2210
	public abstract Rect GetTopFace();

	// Token: 0x060008A3 RID: 2211
	public abstract void Build(Vector3i localPos, Vector3i worldPos, Map map, MeshBuilder mesh, bool onlyLight);

	// Token: 0x060008A4 RID: 2212
	public abstract MeshBuilder Build();

	// Token: 0x060008A5 RID: 2213 RVA: 0x0007EA99 File Offset: 0x0007CC99
	public void SetName(string name)
	{
		this.name = name;
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x0007EAA2 File Offset: 0x0007CCA2
	public string GetName()
	{
		return this.name;
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x0007EAAA File Offset: 0x0007CCAA
	public void SetAtlasID(int atlas)
	{
		this.atlas = atlas;
	}

	// Token: 0x060008A8 RID: 2216 RVA: 0x0007EAB3 File Offset: 0x0007CCB3
	public int GetAtlasID()
	{
		return this.atlas;
	}

	// Token: 0x060008A9 RID: 2217 RVA: 0x0007EABB File Offset: 0x0007CCBB
	public Atlas GetAtlas()
	{
		return this._atlas;
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x0007EAC3 File Offset: 0x0007CCC3
	public Texture GetTexture()
	{
		if (this._atlas != null)
		{
			return this._atlas.GetTexture();
		}
		return null;
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x0007EADA File Offset: 0x0007CCDA
	public void SetLight(int light)
	{
		this.light = Mathf.Clamp(light, 0, 15);
	}

	// Token: 0x060008AC RID: 2220 RVA: 0x0007EAEB File Offset: 0x0007CCEB
	public byte GetLight()
	{
		return (byte)this.light;
	}

	// Token: 0x060008AD RID: 2221
	public abstract bool IsSolid();

	// Token: 0x060008AE RID: 2222 RVA: 0x0007EAF4 File Offset: 0x0007CCF4
	public bool IsAlpha()
	{
		return this.alpha;
	}

	// Token: 0x04000E47 RID: 3655
	[SerializeField]
	private string name;

	// Token: 0x04000E48 RID: 3656
	[SerializeField]
	private int atlas;

	// Token: 0x04000E49 RID: 3657
	[SerializeField]
	private int light;

	// Token: 0x04000E4A RID: 3658
	private Atlas _atlas;

	// Token: 0x04000E4B RID: 3659
	private bool alpha;
}
