using System;
using UnityEngine;

// Token: 0x02000131 RID: 305
[RequireComponent(typeof(Detonator))]
[AddComponentMenu("Detonator/Glow")]
public class DetonatorGlow : DetonatorComponent
{
	// Token: 0x06000A9E RID: 2718 RVA: 0x00089F27 File Offset: 0x00088127
	public override void Init()
	{
		this.FillMaterials(false);
		this.BuildGlow();
	}

	// Token: 0x06000A9F RID: 2719 RVA: 0x00089F36 File Offset: 0x00088136
	public void FillMaterials(bool wipe)
	{
		if (!this.glowMaterial || wipe)
		{
			this.glowMaterial = base.MyDetonator().glowMaterial;
		}
	}

	// Token: 0x06000AA0 RID: 2720 RVA: 0x00089F5C File Offset: 0x0008815C
	public void BuildGlow()
	{
		this._glow = new GameObject("Glow");
		this._glowEmitter = this._glow.AddComponent<DetonatorBurstEmitter>();
		this._glow.transform.parent = base.transform;
		this._glow.transform.localPosition = this.localPosition;
		this._glowEmitter.material = this.glowMaterial;
		this._glowEmitter.exponentialGrowth = false;
		this._glowEmitter.useExplicitColorAnimation = true;
		this._glowEmitter.useWorldSpace = base.MyDetonator().useWorldSpace;
	}

	// Token: 0x06000AA1 RID: 2721 RVA: 0x00089FF8 File Offset: 0x000881F8
	public void UpdateGlow()
	{
		this._glow.transform.localPosition = Vector3.Scale(this.localPosition, new Vector3(this.size, this.size, this.size));
		this._glowEmitter.color = this.color;
		this._glowEmitter.duration = this.duration;
		this._glowEmitter.timeScale = this.timeScale;
		this._glowEmitter.count = 1f;
		this._glowEmitter.particleSize = 65f;
		this._glowEmitter.randomRotation = false;
		this._glowEmitter.sizeVariation = 0f;
		this._glowEmitter.velocity = new Vector3(0f, 0f, 0f);
		this._glowEmitter.startRadius = 0f;
		this._glowEmitter.sizeGrow = 0f;
		this._glowEmitter.size = this.size;
		this._glowEmitter.explodeDelayMin = this.explodeDelayMin;
		this._glowEmitter.explodeDelayMax = this.explodeDelayMax;
		Color color = Color.Lerp(this.color, new Color(0.5f, 0.1f, 0.1f, 1f), 0.5f);
		color.a = 0.9f;
		Color color2 = Color.Lerp(this.color, new Color(0.6f, 0.3f, 0.3f, 1f), 0.5f);
		color2.a = 0.8f;
		Color color3 = Color.Lerp(this.color, new Color(0.7f, 0.3f, 0.3f, 1f), 0.5f);
		color3.a = 0.5f;
		Color color4 = Color.Lerp(this.color, new Color(0.4f, 0.3f, 0.4f, 1f), 0.5f);
		color4.a = 0.2f;
		Color color5;
		color5..ctor(0.1f, 0.1f, 0.4f, 0f);
		this._glowEmitter.colorAnimation[0] = color;
		this._glowEmitter.colorAnimation[1] = color2;
		this._glowEmitter.colorAnimation[2] = color3;
		this._glowEmitter.colorAnimation[3] = color4;
		this._glowEmitter.colorAnimation[4] = color5;
	}

	// Token: 0x06000AA2 RID: 2722 RVA: 0x00002B75 File Offset: 0x00000D75
	private void Update()
	{
	}

	// Token: 0x06000AA3 RID: 2723 RVA: 0x0008A268 File Offset: 0x00088468
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

	// Token: 0x06000AA4 RID: 2724 RVA: 0x0008A2C9 File Offset: 0x000884C9
	public override void Explode()
	{
		if (this.detailThreshold > this.detail)
		{
			return;
		}
		if (this.on)
		{
			this.UpdateGlow();
			this._glowEmitter.Explode();
		}
	}

	// Token: 0x0400104F RID: 4175
	private float _baseSize = 1f;

	// Token: 0x04001050 RID: 4176
	private float _baseDuration = 1f;

	// Token: 0x04001051 RID: 4177
	private Vector3 _baseVelocity = new Vector3(0f, 0f, 0f);

	// Token: 0x04001052 RID: 4178
	private Color _baseColor = Color.black;

	// Token: 0x04001053 RID: 4179
	private float _scaledDuration;

	// Token: 0x04001054 RID: 4180
	private GameObject _glow;

	// Token: 0x04001055 RID: 4181
	private DetonatorBurstEmitter _glowEmitter;

	// Token: 0x04001056 RID: 4182
	public Material glowMaterial;
}
