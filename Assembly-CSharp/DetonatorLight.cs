using System;
using UnityEngine;

// Token: 0x02000133 RID: 307
[RequireComponent(typeof(Detonator))]
[AddComponentMenu("Detonator/Light")]
public class DetonatorLight : DetonatorComponent
{
	// Token: 0x06000AAB RID: 2731 RVA: 0x0008A640 File Offset: 0x00088840
	public override void Init()
	{
		this._light = new GameObject("Light");
		this._light.transform.parent = base.transform;
		this._light.transform.localPosition = this.localPosition;
		this._lightComponent = this._light.AddComponent<Light>();
		this._lightComponent.type = 2;
		this._lightComponent.enabled = false;
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x0008A6B4 File Offset: 0x000888B4
	private void Update()
	{
		if (this._explodeTime + this._scaledDuration > Time.time && this._lightComponent.intensity > 0f)
		{
			this._reduceAmount = this.intensity * (Time.deltaTime / this._scaledDuration);
			this._lightComponent.intensity -= this._reduceAmount;
			return;
		}
		if (this._lightComponent)
		{
			this._lightComponent.enabled = false;
		}
	}

	// Token: 0x06000AAD RID: 2733 RVA: 0x0008A734 File Offset: 0x00088934
	public override void Explode()
	{
		if (this.detailThreshold > this.detail)
		{
			return;
		}
		this._lightComponent.color = this.color;
		this._lightComponent.range = this.size * 50f;
		this._scaledDuration = this.duration * this.timeScale;
		this._lightComponent.enabled = true;
		this._lightComponent.intensity = this.intensity;
		this._explodeTime = Time.time;
	}

	// Token: 0x06000AAE RID: 2734 RVA: 0x0008A7B3 File Offset: 0x000889B3
	public void Reset()
	{
		this.color = this._baseColor;
		this.intensity = this._baseIntensity;
	}

	// Token: 0x04001064 RID: 4196
	private float _baseIntensity = 1f;

	// Token: 0x04001065 RID: 4197
	private Color _baseColor = Color.white;

	// Token: 0x04001066 RID: 4198
	private float _scaledDuration;

	// Token: 0x04001067 RID: 4199
	private float _explodeTime = -1000f;

	// Token: 0x04001068 RID: 4200
	private GameObject _light;

	// Token: 0x04001069 RID: 4201
	private Light _lightComponent;

	// Token: 0x0400106A RID: 4202
	public float intensity;

	// Token: 0x0400106B RID: 4203
	private float _reduceAmount;
}
