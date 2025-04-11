using System;
using UnityEngine;

// Token: 0x0200012F RID: 303
[RequireComponent(typeof(Detonator))]
[AddComponentMenu("Detonator/Fireball")]
public class DetonatorFireball : DetonatorComponent
{
	// Token: 0x06000A8E RID: 2702 RVA: 0x00089396 File Offset: 0x00087596
	public override void Init()
	{
		this.FillMaterials(false);
		this.BuildFireballA();
		this.BuildFireballB();
		this.BuildFireShadow();
	}

	// Token: 0x06000A8F RID: 2703 RVA: 0x000893B4 File Offset: 0x000875B4
	public void FillMaterials(bool wipe)
	{
		if (!this.fireballAMaterial || wipe)
		{
			this.fireballAMaterial = base.MyDetonator().fireballAMaterial;
		}
		if (!this.fireballBMaterial || wipe)
		{
			this.fireballBMaterial = base.MyDetonator().fireballBMaterial;
		}
		if (!this.fireShadowMaterial || wipe)
		{
			if ((double)Random.value > 0.5)
			{
				this.fireShadowMaterial = base.MyDetonator().smokeAMaterial;
				return;
			}
			this.fireShadowMaterial = base.MyDetonator().smokeBMaterial;
		}
	}

	// Token: 0x06000A90 RID: 2704 RVA: 0x00089450 File Offset: 0x00087650
	public void BuildFireballA()
	{
		this._fireballA = new GameObject("FireballA");
		this._fireballAEmitter = this._fireballA.AddComponent<DetonatorBurstEmitter>();
		this._fireballA.transform.parent = base.transform;
		this._fireballA.transform.localRotation = Quaternion.identity;
		this._fireballAEmitter.material = this.fireballAMaterial;
		this._fireballAEmitter.useWorldSpace = base.MyDetonator().useWorldSpace;
		this._fireballAEmitter.upwardsBias = base.MyDetonator().upwardsBias;
	}

	// Token: 0x06000A91 RID: 2705 RVA: 0x000894E8 File Offset: 0x000876E8
	public void UpdateFireballA()
	{
		this._fireballA.transform.localPosition = Vector3.Scale(this.localPosition, new Vector3(this.size, this.size, this.size));
		this._fireballAEmitter.color = this.color;
		this._fireballAEmitter.duration = this.duration * 0.5f;
		this._fireballAEmitter.durationVariation = this.duration * 0.5f;
		this._fireballAEmitter.count = 2f;
		this._fireballAEmitter.timeScale = this.timeScale;
		this._fireballAEmitter.detail = this.detail;
		this._fireballAEmitter.particleSize = 14f;
		this._fireballAEmitter.sizeVariation = 3f;
		this._fireballAEmitter.velocity = this.velocity;
		this._fireballAEmitter.startRadius = 4f;
		this._fireballAEmitter.size = this.size;
		this._fireballAEmitter.useExplicitColorAnimation = true;
		Color color;
		color..ctor(1f, 1f, 1f, 0.5f);
		Color color2;
		color2..ctor(0.6f, 0.15f, 0.15f, 0.3f);
		Color color3;
		color3..ctor(0.1f, 0.2f, 0.45f, 0f);
		this._fireballAEmitter.colorAnimation[0] = Color.Lerp(this.color, color, 0.8f);
		this._fireballAEmitter.colorAnimation[1] = Color.Lerp(this.color, color, 0.5f);
		this._fireballAEmitter.colorAnimation[2] = this.color;
		this._fireballAEmitter.colorAnimation[3] = Color.Lerp(this.color, color2, 0.7f);
		this._fireballAEmitter.colorAnimation[4] = color3;
		this._fireballAEmitter.explodeDelayMin = this.explodeDelayMin;
		this._fireballAEmitter.explodeDelayMax = this.explodeDelayMax;
	}

	// Token: 0x06000A92 RID: 2706 RVA: 0x000896F8 File Offset: 0x000878F8
	public void BuildFireballB()
	{
		this._fireballB = new GameObject("FireballB");
		this._fireballBEmitter = this._fireballB.AddComponent<DetonatorBurstEmitter>();
		this._fireballB.transform.parent = base.transform;
		this._fireballB.transform.localRotation = Quaternion.identity;
		this._fireballBEmitter.material = this.fireballBMaterial;
		this._fireballBEmitter.useWorldSpace = base.MyDetonator().useWorldSpace;
		this._fireballBEmitter.upwardsBias = base.MyDetonator().upwardsBias;
	}

	// Token: 0x06000A93 RID: 2707 RVA: 0x00089790 File Offset: 0x00087990
	public void UpdateFireballB()
	{
		this._fireballB.transform.localPosition = Vector3.Scale(this.localPosition, new Vector3(this.size, this.size, this.size));
		this._fireballBEmitter.color = this.color;
		this._fireballBEmitter.duration = this.duration * 0.5f;
		this._fireballBEmitter.durationVariation = this.duration * 0.5f;
		this._fireballBEmitter.count = 2f;
		this._fireballBEmitter.timeScale = this.timeScale;
		this._fireballBEmitter.detail = this.detail;
		this._fireballBEmitter.particleSize = 10f;
		this._fireballBEmitter.sizeVariation = 6f;
		this._fireballBEmitter.velocity = this.velocity;
		this._fireballBEmitter.startRadius = 4f;
		this._fireballBEmitter.size = this.size;
		this._fireballBEmitter.useExplicitColorAnimation = true;
		Color color;
		color..ctor(1f, 1f, 1f, 0.5f);
		Color color2;
		color2..ctor(0.6f, 0.15f, 0.15f, 0.3f);
		Color color3;
		color3..ctor(0.1f, 0.2f, 0.45f, 0f);
		this._fireballBEmitter.colorAnimation[0] = Color.Lerp(this.color, color, 0.8f);
		this._fireballBEmitter.colorAnimation[1] = Color.Lerp(this.color, color, 0.5f);
		this._fireballBEmitter.colorAnimation[2] = this.color;
		this._fireballBEmitter.colorAnimation[3] = Color.Lerp(this.color, color2, 0.7f);
		this._fireballBEmitter.colorAnimation[4] = color3;
		this._fireballBEmitter.explodeDelayMin = this.explodeDelayMin;
		this._fireballBEmitter.explodeDelayMax = this.explodeDelayMax;
	}

	// Token: 0x06000A94 RID: 2708 RVA: 0x000899A0 File Offset: 0x00087BA0
	public void BuildFireShadow()
	{
		this._fireShadow = new GameObject("FireShadow");
		this._fireShadowEmitter = this._fireShadow.AddComponent<DetonatorBurstEmitter>();
		this._fireShadow.transform.parent = base.transform;
		this._fireShadow.transform.localRotation = Quaternion.identity;
		this._fireShadowEmitter.material = this.fireShadowMaterial;
		this._fireShadowEmitter.useWorldSpace = base.MyDetonator().useWorldSpace;
		this._fireShadowEmitter.upwardsBias = base.MyDetonator().upwardsBias;
	}

	// Token: 0x06000A95 RID: 2709 RVA: 0x00089A38 File Offset: 0x00087C38
	public void UpdateFireShadow()
	{
		this._fireShadow.transform.localPosition = Vector3.Scale(this.localPosition, new Vector3(this.size, this.size, this.size));
		this._fireShadow.transform.LookAt(Camera.main.transform);
		this._fireShadow.transform.localPosition = -(Vector3.forward * 1f);
		this._fireShadowEmitter.color = new Color(0.1f, 0.1f, 0.1f, 0.6f);
		this._fireShadowEmitter.duration = this.duration * 0.5f;
		this._fireShadowEmitter.durationVariation = this.duration * 0.5f;
		this._fireShadowEmitter.timeScale = this.timeScale;
		this._fireShadowEmitter.detail = 1f;
		this._fireShadowEmitter.particleSize = 13f;
		this._fireShadowEmitter.velocity = this.velocity;
		this._fireShadowEmitter.sizeVariation = 1f;
		this._fireShadowEmitter.count = 4f;
		this._fireShadowEmitter.startRadius = 6f;
		this._fireShadowEmitter.size = this.size;
		this._fireShadowEmitter.explodeDelayMin = this.explodeDelayMin;
		this._fireShadowEmitter.explodeDelayMax = this.explodeDelayMax;
	}

	// Token: 0x06000A96 RID: 2710 RVA: 0x00089BAC File Offset: 0x00087DAC
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

	// Token: 0x06000A97 RID: 2711 RVA: 0x00089C10 File Offset: 0x00087E10
	public override void Explode()
	{
		if (this.detailThreshold > this.detail)
		{
			return;
		}
		if (this.on)
		{
			this.UpdateFireballA();
			this.UpdateFireballB();
			this.UpdateFireShadow();
			if (this.drawFireballA)
			{
				this._fireballAEmitter.Explode();
			}
			if (this.drawFireballB)
			{
				this._fireballBEmitter.Explode();
			}
			if (this.drawFireShadow)
			{
				this._fireShadowEmitter.Explode();
			}
		}
	}

	// Token: 0x04001030 RID: 4144
	private float _baseSize = 1f;

	// Token: 0x04001031 RID: 4145
	private float _baseDuration = 3f;

	// Token: 0x04001032 RID: 4146
	private Color _baseColor = new Color(1f, 0.423f, 0f, 0.5f);

	// Token: 0x04001033 RID: 4147
	private Vector3 _baseVelocity = new Vector3(60f, 60f, 60f);

	// Token: 0x04001034 RID: 4148
	private float _scaledDuration;

	// Token: 0x04001035 RID: 4149
	private GameObject _fireballA;

	// Token: 0x04001036 RID: 4150
	private DetonatorBurstEmitter _fireballAEmitter;

	// Token: 0x04001037 RID: 4151
	public Material fireballAMaterial;

	// Token: 0x04001038 RID: 4152
	private GameObject _fireballB;

	// Token: 0x04001039 RID: 4153
	private DetonatorBurstEmitter _fireballBEmitter;

	// Token: 0x0400103A RID: 4154
	public Material fireballBMaterial;

	// Token: 0x0400103B RID: 4155
	private GameObject _fireShadow;

	// Token: 0x0400103C RID: 4156
	private DetonatorBurstEmitter _fireShadowEmitter;

	// Token: 0x0400103D RID: 4157
	public Material fireShadowMaterial;

	// Token: 0x0400103E RID: 4158
	public bool drawFireballA = true;

	// Token: 0x0400103F RID: 4159
	public bool drawFireballB = true;

	// Token: 0x04001040 RID: 4160
	public bool drawFireShadow = true;

	// Token: 0x04001041 RID: 4161
	private Color _detailAdjustedColor;
}
