using System;
using UnityEngine;

// Token: 0x02000007 RID: 7
public class Ghost : MonoBehaviour
{
	// Token: 0x0600001B RID: 27 RVA: 0x00002540 File Offset: 0x00000740
	private void Start()
	{
		this.myRenderer = base.GetComponentInChildren<SkinnedMeshRenderer>();
		this.mat = this.myRenderer.material;
		this.cr = 0.65f;
		this.cg = 0.75f;
		this.cb = 0.9f;
		this.er = 0.25f;
		this.eg = 0.4f;
		this.eb = 0.65f;
		this.mat.SetColor("_Color", new Color(this.cr, this.cg, this.cb, 0.7f));
		this.mat.SetColor("_Emission", new Color(this.er, this.eg, this.eb, 0.7f));
		this.last_update = Time.time + 2f;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00002618 File Offset: 0x00000818
	private void Update()
	{
		if (this.last_update < Time.time)
		{
			return;
		}
		if (this.mat != null)
		{
			this.mat.SetColor("_Color", new Color(Mathf.Lerp(this.mat.GetColor("_Color").r, this.cr, this.FadeSpeed), Mathf.Lerp(this.mat.GetColor("_Color").g, this.cg, this.FadeSpeed), Mathf.Lerp(this.mat.GetColor("_Color").b, this.cb, this.FadeSpeed), this.mat.GetColor("_Color").a));
			this.mat.SetColor("_Emission", new Color(Mathf.Lerp(this.mat.GetColor("_Emission").r, this.er, this.FadeSpeed), Mathf.Lerp(this.mat.GetColor("_Emission").g, this.eg, this.FadeSpeed), Mathf.Lerp(this.mat.GetColor("_Emission").b, this.eb, this.FadeSpeed), this.mat.GetColor("_Emission").a));
		}
	}

	// Token: 0x0600001D RID: 29 RVA: 0x0000277C File Offset: 0x0000097C
	public void RecvDamage()
	{
		this.mat.SetColor("_Color", Color.red);
		this.mat.SetColor("_Emission", Color.red);
		this.last_update = Time.time + 2f;
	}

	// Token: 0x0400000C RID: 12
	private float cr;

	// Token: 0x0400000D RID: 13
	private float cg;

	// Token: 0x0400000E RID: 14
	private float cb;

	// Token: 0x0400000F RID: 15
	private float er;

	// Token: 0x04000010 RID: 16
	private float eg;

	// Token: 0x04000011 RID: 17
	private float eb;

	// Token: 0x04000012 RID: 18
	private float last_update;

	// Token: 0x04000013 RID: 19
	public Renderer myRenderer;

	// Token: 0x04000014 RID: 20
	public Material mat;

	// Token: 0x04000015 RID: 21
	public float FadeSpeed = 2f;

	// Token: 0x04000016 RID: 22
	public Vector3 LastPos = Vector3.zero;
}
