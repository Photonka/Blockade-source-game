using System;
using UnityEngine;

// Token: 0x02000136 RID: 310
[RequireComponent(typeof(Detonator))]
[AddComponentMenu("Detonator/Sound")]
public class DetonatorSound : DetonatorComponent
{
	// Token: 0x06000AC0 RID: 2752 RVA: 0x0008B1E3 File Offset: 0x000893E3
	public override void Init()
	{
		this._soundComponent = base.gameObject.AddComponent<AudioSource>();
	}

	// Token: 0x06000AC1 RID: 2753 RVA: 0x0008B1F8 File Offset: 0x000893F8
	private void Update()
	{
		if (this._soundComponent == null)
		{
			return;
		}
		this._soundComponent.pitch = Time.timeScale;
		if (this._delayedExplosionStarted)
		{
			this._explodeDelay -= Time.deltaTime;
			if (this._explodeDelay <= 0f)
			{
				this.Explode();
			}
		}
	}

	// Token: 0x06000AC2 RID: 2754 RVA: 0x0008B254 File Offset: 0x00089454
	public override void Explode()
	{
		if (this.detailThreshold > this.detail)
		{
			return;
		}
		if (!this._delayedExplosionStarted)
		{
			this._explodeDelay = this.explodeDelayMin + Random.value * (this.explodeDelayMax - this.explodeDelayMin);
		}
		if (this._explodeDelay <= 0f)
		{
			if (Vector3.Distance(Camera.main.transform.position, base.transform.position) < this.distanceThreshold)
			{
				this._idx = (int)(Random.value * (float)this.nearSounds.Length);
				this._soundComponent.PlayOneShot(this.nearSounds[this._idx]);
			}
			else
			{
				this._idx = (int)(Random.value * (float)this.farSounds.Length);
				this._soundComponent.PlayOneShot(this.farSounds[this._idx]);
			}
			this._delayedExplosionStarted = false;
			this._explodeDelay = 0f;
			return;
		}
		this._delayedExplosionStarted = true;
	}

	// Token: 0x06000AC3 RID: 2755 RVA: 0x00002B75 File Offset: 0x00000D75
	public void Reset()
	{
	}

	// Token: 0x04001080 RID: 4224
	public AudioClip[] nearSounds;

	// Token: 0x04001081 RID: 4225
	public AudioClip[] farSounds;

	// Token: 0x04001082 RID: 4226
	public float distanceThreshold = 50f;

	// Token: 0x04001083 RID: 4227
	public float minVolume = 0.4f;

	// Token: 0x04001084 RID: 4228
	public float maxVolume = 1f;

	// Token: 0x04001085 RID: 4229
	public float rolloffFactor = 0.5f;

	// Token: 0x04001086 RID: 4230
	private AudioSource _soundComponent;

	// Token: 0x04001087 RID: 4231
	private bool _delayedExplosionStarted;

	// Token: 0x04001088 RID: 4232
	private float _explodeDelay;

	// Token: 0x04001089 RID: 4233
	private int _idx;
}
