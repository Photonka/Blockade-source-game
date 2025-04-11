using System;
using UnityEngine;

// Token: 0x020000EB RID: 235
[Serializable]
public class Atlas
{
	// Token: 0x06000891 RID: 2193 RVA: 0x0007E890 File Offset: 0x0007CA90
	public void SetMaterial(Material material)
	{
		this.material = material;
	}

	// Token: 0x06000892 RID: 2194 RVA: 0x0007E899 File Offset: 0x0007CA99
	public Material GetMaterial()
	{
		return this.material;
	}

	// Token: 0x06000893 RID: 2195 RVA: 0x0007E8A1 File Offset: 0x0007CAA1
	public void SetWidth(int width)
	{
		this.width = width;
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x0007E8AA File Offset: 0x0007CAAA
	public int GetWidth()
	{
		return this.width;
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x0007E8B2 File Offset: 0x0007CAB2
	public void SetHeight(int height)
	{
		this.height = height;
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x0007E8BB File Offset: 0x0007CABB
	public int GetHeight()
	{
		return this.height;
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x0007E8C3 File Offset: 0x0007CAC3
	public void SetAlpha(bool alpha)
	{
		this.alpha = alpha;
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x0007E8CC File Offset: 0x0007CACC
	public bool IsAlpha()
	{
		return this.alpha;
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x0007E8D4 File Offset: 0x0007CAD4
	public Texture GetTexture()
	{
		if (this.material)
		{
			return this.material.mainTexture;
		}
		return null;
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x0007E8F0 File Offset: 0x0007CAF0
	public Rect ToRect(int pos)
	{
		float num = (float)(pos % this.width);
		int num2 = pos / this.width;
		float num3 = 1f / (float)this.width;
		float num4 = 1f / (float)this.height;
		return new Rect(num * num3, (float)num2 * num4, num3, num4);
	}

	// Token: 0x0600089B RID: 2203 RVA: 0x0007E938 File Offset: 0x0007CB38
	public override string ToString()
	{
		if (this.material != null)
		{
			return this.material.name;
		}
		return "Null";
	}

	// Token: 0x04000E43 RID: 3651
	[SerializeField]
	private Material material;

	// Token: 0x04000E44 RID: 3652
	[SerializeField]
	private int width = 16;

	// Token: 0x04000E45 RID: 3653
	[SerializeField]
	private int height = 16;

	// Token: 0x04000E46 RID: 3654
	[SerializeField]
	private bool alpha;
}
