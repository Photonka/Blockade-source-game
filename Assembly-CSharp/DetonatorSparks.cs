using System;
using UnityEngine;

// Token: 0x02000137 RID: 311
[RequireComponent(typeof(Detonator))]
[AddComponentMenu("Detonator/Sparks")]
public class DetonatorSparks : DetonatorComponent
{
	// Token: 0x06000AC5 RID: 2757 RVA: 0x0008B37B File Offset: 0x0008957B
	public override void Init()
	{
		this.FillMaterials(false);
		this.BuildSparks();
	}

	// Token: 0x06000AC6 RID: 2758 RVA: 0x0008B38A File Offset: 0x0008958A
	public void FillMaterials(bool wipe)
	{
		if (!this.sparksMaterial || wipe)
		{
			this.sparksMaterial = base.MyDetonator().sparksMaterial;
		}
	}

	// Token: 0x06000AC7 RID: 2759 RVA: 0x0008B3B0 File Offset: 0x000895B0
	public void BuildSparks()
	{
		this._sparks = new GameObject("Sparks");
		this._sparksEmitter = this._sparks.AddComponent<DetonatorBurstEmitter>();
		this._sparks.transform.parent = base.transform;
		this._sparks.transform.localPosition = this.localPosition;
		this._sparks.transform.localRotation = Quaternion.identity;
		this._sparksEmitter.material = this.sparksMaterial;
		this._sparksEmitter.force = Physics.gravity / 3f;
		this._sparksEmitter.useExplicitColorAnimation = false;
		this._sparksEmitter.useWorldSpace = base.MyDetonator().useWorldSpace;
		this._sparksEmitter.upwardsBias = base.MyDetonator().upwardsBias;
	}

	// Token: 0x06000AC8 RID: 2760 RVA: 0x0008B484 File Offset: 0x00089684
	public void UpdateSparks()
	{
		this._scaledDuration = this.duration * this.timeScale;
		this._sparksEmitter.color = this.color;
		this._sparksEmitter.duration = this._scaledDuration / 4f;
		this._sparksEmitter.durationVariation = this._scaledDuration;
		this._sparksEmitter.count = (float)((int)(this.detail * 50f));
		this._sparksEmitter.particleSize = 0.5f;
		this._sparksEmitter.sizeVariation = 0.25f;
		if (this._sparksEmitter.upwardsBias > 0f)
		{
			this._sparksEmitter.velocity = new Vector3(this.velocity.x / Mathf.Log(this._sparksEmitter.upwardsBias), this.velocity.y * Mathf.Log(this._sparksEmitter.upwardsBias), this.velocity.z / Mathf.Log(this._sparksEmitter.upwardsBias));
		}
		else
		{
			this._sparksEmitter.velocity = this.velocity;
		}
		this._sparksEmitter.startRadius = 0f;
		this._sparksEmitter.size = this.size;
		this._sparksEmitter.explodeDelayMin = this.explodeDelayMin;
		this._sparksEmitter.explodeDelayMax = this.explodeDelayMax;
	}

	// Token: 0x06000AC9 RID: 2761 RVA: 0x0008B5E4 File Offset: 0x000897E4
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
		this.force = this._baseForce;
	}

	// Token: 0x06000ACA RID: 2762 RVA: 0x0008B651 File Offset: 0x00089851
	public override void Explode()
	{
		if (this.on)
		{
			this.UpdateSparks();
			this._sparksEmitter.Explode();
		}
	}

	// Token: 0x0400108A RID: 4234
	private float _baseSize = 1f;

	// Token: 0x0400108B RID: 4235
	private float _baseDuration = 4f;

	// Token: 0x0400108C RID: 4236
	private Vector3 _baseVelocity = new Vector3(400f, 400f, 400f);

	// Token: 0x0400108D RID: 4237
	private Color _baseColor = Color.white;

	// Token: 0x0400108E RID: 4238
	private Vector3 _baseForce = Physics.gravity;

	// Token: 0x0400108F RID: 4239
	private float _scaledDuration;

	// Token: 0x04001090 RID: 4240
	private GameObject _sparks;

	// Token: 0x04001091 RID: 4241
	private DetonatorBurstEmitter _sparksEmitter;

	// Token: 0x04001092 RID: 4242
	public Material sparksMaterial;
}
