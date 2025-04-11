using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000053 RID: 83
public class Arrow : MonoBehaviour
{
	// Token: 0x06000297 RID: 663 RVA: 0x000389E0 File Offset: 0x00036BE0
	private void Start()
	{
		this.newPos = base.transform.position;
		this.oldPos = this.newPos;
		this.velocity = this.speed * base.transform.forward;
		this.myTransform = base.transform;
		this.cspm = (ParticleManager)Object.FindObjectOfType(typeof(ParticleManager));
		base.StartCoroutine(this.Start2());
	}

	// Token: 0x06000298 RID: 664 RVA: 0x00038A5C File Offset: 0x00036C5C
	private void Update()
	{
		if (this.hasHit)
		{
			return;
		}
		this.newPos += (this.velocity + this.direction) * Time.deltaTime;
		Vector3 vector = this.newPos - this.oldPos;
		float magnitude = vector.magnitude;
		vector /= magnitude;
		if (magnitude > 0f)
		{
			Debug.DrawLine(this.oldPos, vector, Color.red);
			if (Physics.Raycast(this.oldPos, vector, ref this.hit, magnitude, this.layerMask))
			{
				this.newPos = this.hit.point;
				if (this.hit.collider)
				{
					this.myTransform.parent = this.hit.transform;
					this.hasHit = true;
					if (this.hit.rigidbody)
					{
						this.hit.rigidbody.AddForceAtPosition(this.forceToApply * vector, this.hit.point);
					}
					this.Explode(this.hit.collider);
				}
			}
		}
		this.oldPos = base.transform.position;
		base.transform.position = this.newPos;
		this.velocity.y = this.velocity.y - this.arrowGravity * Time.deltaTime;
		base.transform.rotation = Quaternion.LookRotation(vector);
	}

	// Token: 0x06000299 RID: 665 RVA: 0x00038BD8 File Offset: 0x00036DD8
	public void Explode(Collider col)
	{
		if (this.exploded)
		{
			return;
		}
		if (this.cscl == null)
		{
			this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
		}
		if (this.cscl == null)
		{
			return;
		}
		this.exploded = true;
		Data component = col.gameObject.GetComponent<Data>();
		if (component)
		{
			this.cspm.CreateHit(this.myTransform, component.hitzone, this.myTransform.position.x, this.myTransform.position.y, this.myTransform.position.z);
			this.cspm.CreateParticle(this.myTransform.position.x, this.myTransform.position.y, this.myTransform.position.z, 1f, 0f, 0f, 1f);
			if (this.id == this.cscl.myindex)
			{
				this.cscl.send_damage(161, component.index, component.hitzone, Time.time, this.cscl.transform.position.x, this.cscl.transform.position.y, this.cscl.transform.position.z, col.transform.position.x, col.transform.position.y, col.transform.position.z, this.cscl.transform.position.x, this.cscl.transform.position.y, this.cscl.transform.position.z, col.transform.position.x, col.transform.position.y, col.transform.position.z);
			}
		}
		base.GetComponent<AudioSource>().PlayOneShot(this.soundHit, 0.7f);
	}

	// Token: 0x0600029A RID: 666 RVA: 0x00038E04 File Offset: 0x00037004
	private IEnumerator Start2()
	{
		yield return new WaitForSeconds(10.5f);
		this.KillSelf();
		yield break;
	}

	// Token: 0x0600029B RID: 667 RVA: 0x00038E13 File Offset: 0x00037013
	public void KillSelf()
	{
		if (base.gameObject == null)
		{
			return;
		}
		Object.Destroy(base.gameObject);
		EntManager.DeleteEnt(this.entid);
	}

	// Token: 0x040004F7 RID: 1271
	public int id;

	// Token: 0x040004F8 RID: 1272
	public int uid;

	// Token: 0x040004F9 RID: 1273
	public int entid;

	// Token: 0x040004FA RID: 1274
	public LayerMask layerMask;

	// Token: 0x040004FB RID: 1275
	public AudioClip soundHit;

	// Token: 0x040004FC RID: 1276
	private Vector3 velocity = Vector3.zero;

	// Token: 0x040004FD RID: 1277
	private Vector3 newPos = Vector3.zero;

	// Token: 0x040004FE RID: 1278
	private Vector3 oldPos = Vector3.zero;

	// Token: 0x040004FF RID: 1279
	private bool hasHit;

	// Token: 0x04000500 RID: 1280
	private Vector3 direction;

	// Token: 0x04000501 RID: 1281
	private RaycastHit hit;

	// Token: 0x04000502 RID: 1282
	public float speed;

	// Token: 0x04000503 RID: 1283
	private Transform myTransform;

	// Token: 0x04000504 RID: 1284
	public float forceToApply;

	// Token: 0x04000505 RID: 1285
	public float arrowGravity;

	// Token: 0x04000506 RID: 1286
	private GameObject follow;

	// Token: 0x04000507 RID: 1287
	private bool exploded;

	// Token: 0x04000508 RID: 1288
	private Client cscl;

	// Token: 0x04000509 RID: 1289
	private ParticleManager cspm;
}
