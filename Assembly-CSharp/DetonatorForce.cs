using System;
using UnityEngine;

// Token: 0x02000130 RID: 304
[RequireComponent(typeof(Detonator))]
[AddComponentMenu("Detonator/Force")]
public class DetonatorForce : DetonatorComponent
{
	// Token: 0x06000A99 RID: 2713 RVA: 0x00002B75 File Offset: 0x00000D75
	public override void Init()
	{
	}

	// Token: 0x06000A9A RID: 2714 RVA: 0x00089CF7 File Offset: 0x00087EF7
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
	}

	// Token: 0x06000A9B RID: 2715 RVA: 0x00089D28 File Offset: 0x00087F28
	public override void Explode()
	{
		if (!this.on)
		{
			return;
		}
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
			this._explosionPosition = base.transform.position;
			this._colliders = Physics.OverlapSphere(this._explosionPosition, this.radius);
			foreach (Collider collider in this._colliders)
			{
				if (collider && collider.GetComponent<Rigidbody>())
				{
					collider.GetComponent<Rigidbody>().AddExplosionForce(this.power * this.size, this._explosionPosition, this.radius * this.size, 4f * base.MyDetonator().upwardsBias * this.size);
					collider.gameObject.SendMessage("OnDetonatorForceHit", null, 1);
					if (this.fireObject)
					{
						if (collider.transform.Find(this.fireObject.name + "(Clone)"))
						{
							return;
						}
						this._tempFireObject = Object.Instantiate<GameObject>(this.fireObject, base.transform.position, base.transform.rotation);
						this._tempFireObject.transform.parent = collider.transform;
						this._tempFireObject.transform.localPosition = new Vector3(0f, 0f, 0f);
					}
				}
			}
			this._delayedExplosionStarted = false;
			this._explodeDelay = 0f;
			return;
		}
		this._delayedExplosionStarted = true;
	}

	// Token: 0x06000A9C RID: 2716 RVA: 0x00089EEF File Offset: 0x000880EF
	public void Reset()
	{
		this.radius = this._baseRadius;
		this.power = this._basePower;
	}

	// Token: 0x04001042 RID: 4162
	private float _baseRadius = 50f;

	// Token: 0x04001043 RID: 4163
	private float _basePower = 4000f;

	// Token: 0x04001044 RID: 4164
	private float _scaledRange;

	// Token: 0x04001045 RID: 4165
	private float _scaledIntensity;

	// Token: 0x04001046 RID: 4166
	private bool _delayedExplosionStarted;

	// Token: 0x04001047 RID: 4167
	private float _explodeDelay;

	// Token: 0x04001048 RID: 4168
	public float radius;

	// Token: 0x04001049 RID: 4169
	public float power;

	// Token: 0x0400104A RID: 4170
	public GameObject fireObject;

	// Token: 0x0400104B RID: 4171
	public float fireObjectLife;

	// Token: 0x0400104C RID: 4172
	private Collider[] _colliders;

	// Token: 0x0400104D RID: 4173
	private GameObject _tempFireObject;

	// Token: 0x0400104E RID: 4174
	private Vector3 _explosionPosition;
}
