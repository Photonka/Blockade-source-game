using System;
using UnityEngine;

// Token: 0x02000134 RID: 308
[RequireComponent(typeof(Detonator))]
[AddComponentMenu("Detonator/Shockwave")]
public class DetonatorShockwave : DetonatorComponent
{
	// Token: 0x06000AB0 RID: 2736 RVA: 0x0008A7F6 File Offset: 0x000889F6
	public override void Init()
	{
		this.FillMaterials(false);
		this.BuildShockwave();
	}

	// Token: 0x06000AB1 RID: 2737 RVA: 0x0008A805 File Offset: 0x00088A05
	public void FillMaterials(bool wipe)
	{
		if (!this.shockwaveMaterial || wipe)
		{
			this.shockwaveMaterial = base.MyDetonator().shockwaveMaterial;
		}
	}

	// Token: 0x06000AB2 RID: 2738 RVA: 0x0008A82C File Offset: 0x00088A2C
	public void BuildShockwave()
	{
		this._shockwave = new GameObject("Shockwave");
		this._shockwaveEmitter = this._shockwave.AddComponent<DetonatorBurstEmitter>();
		this._shockwave.transform.parent = base.transform;
		this._shockwave.transform.localRotation = Quaternion.identity;
		this._shockwave.transform.localPosition = this.localPosition;
		this._shockwaveEmitter.material = this.shockwaveMaterial;
		this._shockwaveEmitter.exponentialGrowth = false;
		this._shockwaveEmitter.useWorldSpace = base.MyDetonator().useWorldSpace;
	}

	// Token: 0x06000AB3 RID: 2739 RVA: 0x0008A8D0 File Offset: 0x00088AD0
	public void UpdateShockwave()
	{
		this._shockwave.transform.localPosition = Vector3.Scale(this.localPosition, new Vector3(this.size, this.size, this.size));
		this._shockwaveEmitter.color = this.color;
		this._shockwaveEmitter.duration = this.duration;
		this._shockwaveEmitter.durationVariation = this.duration * 0.1f;
		this._shockwaveEmitter.count = 1f;
		this._shockwaveEmitter.detail = 1f;
		this._shockwaveEmitter.particleSize = 25f;
		this._shockwaveEmitter.sizeVariation = 0f;
		this._shockwaveEmitter.velocity = new Vector3(0f, 0f, 0f);
		this._shockwaveEmitter.startRadius = 0f;
		this._shockwaveEmitter.sizeGrow = 202f;
		this._shockwaveEmitter.size = this.size;
		this._shockwaveEmitter.explodeDelayMin = this.explodeDelayMin;
		this._shockwaveEmitter.explodeDelayMax = this.explodeDelayMax;
	}

	// Token: 0x06000AB4 RID: 2740 RVA: 0x0008A9FC File Offset: 0x00088BFC
	public void Reset()
	{
		this.FillMaterials(true);
		this.on = true;
		this.size = this._baseSize;
		this.duration = this._baseDuration;
		this.explodeDelayMin = 0f;
		this.explodeDelayMax = 0f;
		this.color = this._baseColor;
		this.velocity = this._baseVelocity;
	}

	// Token: 0x06000AB5 RID: 2741 RVA: 0x0008AA5D File Offset: 0x00088C5D
	public override void Explode()
	{
		if (this.on)
		{
			this.UpdateShockwave();
			this._shockwaveEmitter.Explode();
		}
	}

	// Token: 0x0400106C RID: 4204
	private float _baseSize = 1f;

	// Token: 0x0400106D RID: 4205
	private float _baseDuration = 0.25f;

	// Token: 0x0400106E RID: 4206
	private Vector3 _baseVelocity = new Vector3(0f, 0f, 0f);

	// Token: 0x0400106F RID: 4207
	private Color _baseColor = Color.white;

	// Token: 0x04001070 RID: 4208
	private GameObject _shockwave;

	// Token: 0x04001071 RID: 4209
	private DetonatorBurstEmitter _shockwaveEmitter;

	// Token: 0x04001072 RID: 4210
	public Material shockwaveMaterial;
}
