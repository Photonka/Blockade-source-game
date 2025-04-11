using System;
using UnityEngine;

// Token: 0x02000135 RID: 309
[RequireComponent(typeof(Detonator))]
[AddComponentMenu("Detonator/Smoke")]
public class DetonatorSmoke : DetonatorComponent
{
	// Token: 0x06000AB7 RID: 2743 RVA: 0x0008AAC6 File Offset: 0x00088CC6
	public override void Init()
	{
		this.FillMaterials(false);
		this.BuildSmokeA();
		this.BuildSmokeB();
	}

	// Token: 0x06000AB8 RID: 2744 RVA: 0x0008AADC File Offset: 0x00088CDC
	public void FillMaterials(bool wipe)
	{
		if (!this.smokeAMaterial || wipe)
		{
			this.smokeAMaterial = base.MyDetonator().smokeAMaterial;
		}
		if (!this.smokeBMaterial || wipe)
		{
			this.smokeBMaterial = base.MyDetonator().smokeBMaterial;
		}
	}

	// Token: 0x06000AB9 RID: 2745 RVA: 0x0008AB30 File Offset: 0x00088D30
	public void BuildSmokeA()
	{
		this._smokeA = new GameObject("SmokeA");
		this._smokeAEmitter = this._smokeA.AddComponent<DetonatorBurstEmitter>();
		this._smokeA.transform.parent = base.transform;
		this._smokeA.transform.localPosition = this.localPosition;
		this._smokeA.transform.localRotation = Quaternion.identity;
		this._smokeAEmitter.material = this.smokeAMaterial;
		this._smokeAEmitter.exponentialGrowth = false;
		this._smokeAEmitter.sizeGrow = 0.095f;
		this._smokeAEmitter.useWorldSpace = base.MyDetonator().useWorldSpace;
		this._smokeAEmitter.upwardsBias = base.MyDetonator().upwardsBias;
	}

	// Token: 0x06000ABA RID: 2746 RVA: 0x0008ABF8 File Offset: 0x00088DF8
	public void UpdateSmokeA()
	{
		this._smokeA.transform.localPosition = Vector3.Scale(this.localPosition, new Vector3(this.size, this.size, this.size));
		this._smokeA.transform.LookAt(Camera.main.transform);
		this._smokeA.transform.localPosition = -(Vector3.forward * -1.5f);
		this._smokeAEmitter.color = this.color;
		this._smokeAEmitter.duration = this.duration * 0.5f;
		this._smokeAEmitter.durationVariation = 0f;
		this._smokeAEmitter.timeScale = this.timeScale;
		this._smokeAEmitter.count = 4f;
		this._smokeAEmitter.particleSize = 25f;
		this._smokeAEmitter.sizeVariation = 3f;
		this._smokeAEmitter.velocity = this.velocity;
		this._smokeAEmitter.startRadius = 10f;
		this._smokeAEmitter.size = this.size;
		this._smokeAEmitter.useExplicitColorAnimation = true;
		this._smokeAEmitter.explodeDelayMin = this.explodeDelayMin;
		this._smokeAEmitter.explodeDelayMax = this.explodeDelayMax;
		Color color;
		color..ctor(0.2f, 0.2f, 0.2f, 0.4f);
		Color color2;
		color2..ctor(0.2f, 0.2f, 0.2f, 0.7f);
		Color color3;
		color3..ctor(0.2f, 0.2f, 0.2f, 0.4f);
		Color color4;
		color4..ctor(0.2f, 0.2f, 0.2f, 0f);
		this._smokeAEmitter.colorAnimation[0] = color;
		this._smokeAEmitter.colorAnimation[1] = color2;
		this._smokeAEmitter.colorAnimation[2] = color2;
		this._smokeAEmitter.colorAnimation[3] = color3;
		this._smokeAEmitter.colorAnimation[4] = color4;
	}

	// Token: 0x06000ABB RID: 2747 RVA: 0x0008AE14 File Offset: 0x00089014
	public void BuildSmokeB()
	{
		this._smokeB = new GameObject("SmokeB");
		this._smokeBEmitter = this._smokeB.AddComponent<DetonatorBurstEmitter>();
		this._smokeB.transform.parent = base.transform;
		this._smokeB.transform.localPosition = this.localPosition;
		this._smokeB.transform.localRotation = Quaternion.identity;
		this._smokeBEmitter.material = this.smokeBMaterial;
		this._smokeBEmitter.exponentialGrowth = false;
		this._smokeBEmitter.sizeGrow = 0.095f;
		this._smokeBEmitter.useWorldSpace = base.MyDetonator().useWorldSpace;
		this._smokeBEmitter.upwardsBias = base.MyDetonator().upwardsBias;
	}

	// Token: 0x06000ABC RID: 2748 RVA: 0x0008AEDC File Offset: 0x000890DC
	public void UpdateSmokeB()
	{
		this._smokeB.transform.localPosition = Vector3.Scale(this.localPosition, new Vector3(this.size, this.size, this.size));
		this._smokeB.transform.LookAt(Camera.main.transform);
		this._smokeB.transform.localPosition = -(Vector3.forward * -1f);
		this._smokeBEmitter.color = this.color;
		this._smokeBEmitter.duration = this.duration * 0.5f;
		this._smokeBEmitter.durationVariation = 0f;
		this._smokeBEmitter.count = 2f;
		this._smokeBEmitter.particleSize = 25f;
		this._smokeBEmitter.sizeVariation = 3f;
		this._smokeBEmitter.velocity = this.velocity;
		this._smokeBEmitter.startRadius = 10f;
		this._smokeBEmitter.size = this.size;
		this._smokeBEmitter.useExplicitColorAnimation = true;
		this._smokeBEmitter.explodeDelayMin = this.explodeDelayMin;
		this._smokeBEmitter.explodeDelayMax = this.explodeDelayMax;
		Color color;
		color..ctor(0.2f, 0.2f, 0.2f, 0.4f);
		Color color2;
		color2..ctor(0.2f, 0.2f, 0.2f, 0.7f);
		Color color3;
		color3..ctor(0.2f, 0.2f, 0.2f, 0.4f);
		Color color4;
		color4..ctor(0.2f, 0.2f, 0.2f, 0f);
		this._smokeBEmitter.colorAnimation[0] = color;
		this._smokeBEmitter.colorAnimation[1] = color2;
		this._smokeBEmitter.colorAnimation[2] = color2;
		this._smokeBEmitter.colorAnimation[3] = color3;
		this._smokeBEmitter.colorAnimation[4] = color4;
	}

	// Token: 0x06000ABD RID: 2749 RVA: 0x0008B0E8 File Offset: 0x000892E8
	public void Reset()
	{
		this.FillMaterials(true);
		this.on = true;
		this.size = 1f;
		this.duration = 8f;
		this.explodeDelayMin = 0f;
		this.explodeDelayMax = 0f;
		this.color = this._baseColor;
		this.velocity = new Vector3(3f, 3f, 3f);
	}

	// Token: 0x06000ABE RID: 2750 RVA: 0x0008B158 File Offset: 0x00089358
	public override void Explode()
	{
		if (this.detailThreshold > this.detail)
		{
			return;
		}
		if (this.on)
		{
			this.UpdateSmokeA();
			this.UpdateSmokeB();
			if (this.drawSmokeA)
			{
				this._smokeAEmitter.Explode();
			}
			if (this.drawSmokeB)
			{
				this._smokeBEmitter.Explode();
			}
		}
	}

	// Token: 0x04001073 RID: 4211
	private const float _baseSize = 1f;

	// Token: 0x04001074 RID: 4212
	private const float _baseDuration = 8f;

	// Token: 0x04001075 RID: 4213
	private Color _baseColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

	// Token: 0x04001076 RID: 4214
	private const float _baseDamping = 0.1300004f;

	// Token: 0x04001077 RID: 4215
	private float _scaledDuration;

	// Token: 0x04001078 RID: 4216
	private GameObject _smokeA;

	// Token: 0x04001079 RID: 4217
	private DetonatorBurstEmitter _smokeAEmitter;

	// Token: 0x0400107A RID: 4218
	public Material smokeAMaterial;

	// Token: 0x0400107B RID: 4219
	private GameObject _smokeB;

	// Token: 0x0400107C RID: 4220
	private DetonatorBurstEmitter _smokeBEmitter;

	// Token: 0x0400107D RID: 4221
	public Material smokeBMaterial;

	// Token: 0x0400107E RID: 4222
	public bool drawSmokeA = true;

	// Token: 0x0400107F RID: 4223
	public bool drawSmokeB = true;
}
