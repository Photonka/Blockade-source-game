using System;
using UnityEngine;

// Token: 0x0200012D RID: 301
[RequireComponent(typeof(Detonator))]
public class DetonatorCloudRing : DetonatorComponent
{
	// Token: 0x06000A82 RID: 2690 RVA: 0x00088ED3 File Offset: 0x000870D3
	public override void Init()
	{
		this.FillMaterials(false);
		this.BuildCloudRing();
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x00088EE2 File Offset: 0x000870E2
	public void FillMaterials(bool wipe)
	{
		if (!this.cloudRingMaterial || wipe)
		{
			this.cloudRingMaterial = base.MyDetonator().smokeBMaterial;
		}
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x00088F08 File Offset: 0x00087108
	public void BuildCloudRing()
	{
		this._cloudRing = new GameObject("CloudRing");
		this._cloudRingEmitter = this._cloudRing.AddComponent<DetonatorBurstEmitter>();
		this._cloudRing.transform.parent = base.transform;
		this._cloudRing.transform.localPosition = this.localPosition;
		this._cloudRingEmitter.material = this.cloudRingMaterial;
		this._cloudRingEmitter.useExplicitColorAnimation = true;
	}

	// Token: 0x06000A85 RID: 2693 RVA: 0x00088F80 File Offset: 0x00087180
	public void UpdateCloudRing()
	{
		this._cloudRing.transform.localPosition = Vector3.Scale(this.localPosition, new Vector3(this.size, this.size, this.size));
		this._cloudRingEmitter.color = this.color;
		this._cloudRingEmitter.duration = this.duration;
		this._cloudRingEmitter.durationVariation = this.duration / 4f;
		this._cloudRingEmitter.count = (float)((int)(this.detail * 50f));
		this._cloudRingEmitter.particleSize = 10f;
		this._cloudRingEmitter.sizeVariation = 2f;
		this._cloudRingEmitter.velocity = this.velocity;
		this._cloudRingEmitter.startRadius = 3f;
		this._cloudRingEmitter.size = this.size;
		this._cloudRingEmitter.force = this.force;
		this._cloudRingEmitter.explodeDelayMin = this.explodeDelayMin;
		this._cloudRingEmitter.explodeDelayMax = this.explodeDelayMax;
		Color color = Color.Lerp(this.color, new Color(0.2f, 0.2f, 0.2f, 0.6f), 0.5f);
		Color color2;
		color2..ctor(0.2f, 0.2f, 0.2f, 0.5f);
		Color color3;
		color3..ctor(0.2f, 0.2f, 0.2f, 0.3f);
		Color color4;
		color4..ctor(0.2f, 0.2f, 0.2f, 0f);
		this._cloudRingEmitter.colorAnimation[0] = color;
		this._cloudRingEmitter.colorAnimation[1] = color2;
		this._cloudRingEmitter.colorAnimation[2] = color2;
		this._cloudRingEmitter.colorAnimation[3] = color3;
		this._cloudRingEmitter.colorAnimation[4] = color4;
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x0008916C File Offset: 0x0008736C
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

	// Token: 0x06000A87 RID: 2695 RVA: 0x000891D9 File Offset: 0x000873D9
	public override void Explode()
	{
		if (this.on)
		{
			this.UpdateCloudRing();
			this._cloudRingEmitter.Explode();
		}
	}

	// Token: 0x04001014 RID: 4116
	private float _baseSize = 1f;

	// Token: 0x04001015 RID: 4117
	private float _baseDuration = 5f;

	// Token: 0x04001016 RID: 4118
	private Vector3 _baseVelocity = new Vector3(155f, 5f, 155f);

	// Token: 0x04001017 RID: 4119
	private Color _baseColor = Color.white;

	// Token: 0x04001018 RID: 4120
	private Vector3 _baseForce = new Vector3(0.162f, 2.56f, 0f);

	// Token: 0x04001019 RID: 4121
	private GameObject _cloudRing;

	// Token: 0x0400101A RID: 4122
	private DetonatorBurstEmitter _cloudRingEmitter;

	// Token: 0x0400101B RID: 4123
	public Material cloudRingMaterial;
}
