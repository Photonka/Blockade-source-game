using System;
using UnityEngine;

// Token: 0x02000132 RID: 306
[RequireComponent(typeof(Detonator))]
[AddComponentMenu("Detonator/Heatwave (Pro Only)")]
public class DetonatorHeatwave : DetonatorComponent
{
	// Token: 0x06000AA6 RID: 2726 RVA: 0x00002B75 File Offset: 0x00000D75
	public override void Init()
	{
	}

	// Token: 0x06000AA7 RID: 2727 RVA: 0x0008A344 File Offset: 0x00088544
	private void Update()
	{
		if (this._delayedExplosionStarted)
		{
			this._explodeDelay -= Time.deltaTime;
			if (this._explodeDelay <= 0f)
			{
				this.Explode();
			}
		}
		if (this._heatwave)
		{
			this._heatwave.transform.rotation = Quaternion.FromToRotation(Vector3.up, Camera.main.transform.position - this._heatwave.transform.position);
			this._heatwave.transform.localPosition = this.localPosition + Vector3.forward * this.zOffset;
			this._elapsedTime += Time.deltaTime;
			this._normalizedTime = this._elapsedTime / this.duration;
			this.s = Mathf.Lerp(this._startSize, this._maxSize, this._normalizedTime);
			this._heatwave.GetComponent<Renderer>().material.SetFloat("_BumpAmt", (1f - this._normalizedTime) * this.distortion);
			this._heatwave.gameObject.transform.localScale = new Vector3(this.s, this.s, this.s);
			if (this._elapsedTime > this.duration)
			{
				Object.Destroy(this._heatwave.gameObject);
			}
		}
	}

	// Token: 0x06000AA8 RID: 2728 RVA: 0x0008A4B0 File Offset: 0x000886B0
	public override void Explode()
	{
		if (SystemInfo.supportsImageEffects)
		{
			if (this.detailThreshold > this.detail || !this.on)
			{
				return;
			}
			if (!this._delayedExplosionStarted)
			{
				this._explodeDelay = this.explodeDelayMin + Random.value * (this.explodeDelayMax - this.explodeDelayMin);
			}
			if (this._explodeDelay <= 0f)
			{
				this._startSize = 0f;
				this._maxSize = this.size * 10f;
				this._material = new Material(Shader.Find("HeatDistort"));
				this._heatwave = GameObject.CreatePrimitive(4);
				this._heatwave.name = "Heatwave";
				this._heatwave.transform.parent = base.transform;
				Object.Destroy(this._heatwave.GetComponent(typeof(MeshCollider)));
				if (!this.heatwaveMaterial)
				{
					this.heatwaveMaterial = base.MyDetonator().heatwaveMaterial;
				}
				this._material.CopyPropertiesFromMaterial(this.heatwaveMaterial);
				this._heatwave.GetComponent<Renderer>().material = this._material;
				this._heatwave.transform.parent = base.transform;
				this._delayedExplosionStarted = false;
				this._explodeDelay = 0f;
				return;
			}
			this._delayedExplosionStarted = true;
		}
	}

	// Token: 0x06000AA9 RID: 2729 RVA: 0x0008A609 File Offset: 0x00088809
	public void Reset()
	{
		this.duration = this._baseDuration;
	}

	// Token: 0x04001057 RID: 4183
	private GameObject _heatwave;

	// Token: 0x04001058 RID: 4184
	private float s;

	// Token: 0x04001059 RID: 4185
	private float _startSize;

	// Token: 0x0400105A RID: 4186
	private float _maxSize;

	// Token: 0x0400105B RID: 4187
	private float _baseDuration = 0.25f;

	// Token: 0x0400105C RID: 4188
	private bool _delayedExplosionStarted;

	// Token: 0x0400105D RID: 4189
	private float _explodeDelay;

	// Token: 0x0400105E RID: 4190
	public float zOffset = 0.5f;

	// Token: 0x0400105F RID: 4191
	public float distortion = 64f;

	// Token: 0x04001060 RID: 4192
	private float _elapsedTime;

	// Token: 0x04001061 RID: 4193
	private float _normalizedTime;

	// Token: 0x04001062 RID: 4194
	public Material heatwaveMaterial;

	// Token: 0x04001063 RID: 4195
	private Material _material;
}
