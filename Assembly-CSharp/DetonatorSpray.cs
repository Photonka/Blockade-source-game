using System;
using UnityEngine;

// Token: 0x02000138 RID: 312
[RequireComponent(typeof(Detonator))]
[AddComponentMenu("Detonator/Object Spray")]
public class DetonatorSpray : DetonatorComponent
{
	// Token: 0x06000ACC RID: 2764 RVA: 0x00002B75 File Offset: 0x00000D75
	public override void Init()
	{
	}

	// Token: 0x06000ACD RID: 2765 RVA: 0x0008B6C5 File Offset: 0x000898C5
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

	// Token: 0x06000ACE RID: 2766 RVA: 0x0008B6F4 File Offset: 0x000898F4
	public override void Explode()
	{
		if (!this._delayedExplosionStarted)
		{
			this._explodeDelay = this.explodeDelayMin + Random.value * (this.explodeDelayMax - this.explodeDelayMin);
		}
		if (this._explodeDelay <= 0f)
		{
			int num = (int)(this.detail * (float)this.count);
			for (int i = 0; i < num; i++)
			{
				Vector3 vector = Random.onUnitSphere * (this.startingRadius * this.size);
				Vector3 vector2;
				vector2..ctor(this.velocity.x * this.size, this.velocity.y * this.size, this.velocity.z * this.size);
				GameObject gameObject = Object.Instantiate<GameObject>(this.sprayObject, base.transform.position + vector, base.transform.rotation);
				gameObject.transform.parent = base.transform;
				this._tmpScale = this.minScale + Random.value * (this.maxScale - this.minScale);
				this._tmpScale *= this.size;
				gameObject.transform.localScale = new Vector3(this._tmpScale, this._tmpScale, this._tmpScale);
				if (base.MyDetonator().upwardsBias > 0f)
				{
					vector2..ctor(vector2.x / Mathf.Log(base.MyDetonator().upwardsBias), vector2.y * Mathf.Log(base.MyDetonator().upwardsBias), vector2.z / Mathf.Log(base.MyDetonator().upwardsBias));
				}
				gameObject.GetComponent<Rigidbody>().velocity = Vector3.Scale(vector.normalized, vector2);
				gameObject.GetComponent<Rigidbody>().velocity = Vector3.Scale(vector.normalized, vector2);
				Object.Destroy(gameObject, this.duration * this.timeScale);
				this._delayedExplosionStarted = false;
				this._explodeDelay = 0f;
			}
			return;
		}
		this._delayedExplosionStarted = true;
	}

	// Token: 0x06000ACF RID: 2767 RVA: 0x0008B8F7 File Offset: 0x00089AF7
	public void Reset()
	{
		this.velocity = new Vector3(15f, 15f, 15f);
	}

	// Token: 0x04001093 RID: 4243
	public GameObject sprayObject;

	// Token: 0x04001094 RID: 4244
	public int count = 10;

	// Token: 0x04001095 RID: 4245
	public float startingRadius;

	// Token: 0x04001096 RID: 4246
	public float minScale = 1f;

	// Token: 0x04001097 RID: 4247
	public float maxScale = 1f;

	// Token: 0x04001098 RID: 4248
	private bool _delayedExplosionStarted;

	// Token: 0x04001099 RID: 4249
	private float _explodeDelay;

	// Token: 0x0400109A RID: 4250
	private Vector3 _explosionPosition;

	// Token: 0x0400109B RID: 4251
	private float _tmpScale;
}
