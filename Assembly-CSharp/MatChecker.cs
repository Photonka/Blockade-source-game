using System;
using UnityEngine;

// Token: 0x0200000F RID: 15
public class MatChecker : MonoBehaviour
{
	// Token: 0x06000034 RID: 52 RVA: 0x00002C68 File Offset: 0x00000E68
	private void Start()
	{
		this.myRenderer = base.GetComponent<Renderer>();
		if (this.myRenderer == null)
		{
			return;
		}
		this.myMat = this.myRenderer.material;
		if (this.myMat == null)
		{
			return;
		}
		this.myShader = this.myMat.shader;
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00002CC4 File Offset: 0x00000EC4
	private void FixedUpdate()
	{
		if (this.myRenderer == null)
		{
			return;
		}
		if (this.myMat == null)
		{
			return;
		}
		if (this.myMat.shader == this.myShader)
		{
			return;
		}
		this.myMat.shader = this.myShader;
	}

	// Token: 0x04000028 RID: 40
	private Renderer myRenderer;

	// Token: 0x04000029 RID: 41
	private Material myMat;

	// Token: 0x0400002A RID: 42
	private Shader myShader;
}
