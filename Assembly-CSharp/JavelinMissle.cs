using System;
using UnityEngine;

// Token: 0x02000061 RID: 97
public class JavelinMissle : MonoBehaviour
{
	// Token: 0x060002DA RID: 730 RVA: 0x00039BD7 File Offset: 0x00037DD7
	public void Awake()
	{
		this._thisTransform = base.transform;
		this.explosionPrefab = base.gameObject;
		this.entmanager = (EntManager)Object.FindObjectOfType(typeof(EntManager));
	}

	// Token: 0x060002DB RID: 731 RVA: 0x00039C0C File Offset: 0x00037E0C
	public void Update()
	{
		this.explosionTime -= Time.deltaTime;
		if (this.explosionTime <= 0f)
		{
			this.Explode();
			return;
		}
		Vector3 vector = this._thisTransform.forward * this.speed * Time.deltaTime;
		float num = 0f;
		Vector3 vector2;
		if (this.target != null && this.timeTarget == null)
		{
			if (Vector3.Distance(this.target.position, this._thisTransform.position) > 15f)
			{
				num = Vector3.Distance(this.target.position, this._thisTransform.position) / 2f;
			}
			vector2 = this.target.position - this._thisTransform.position + new Vector3(0f, num + 1.75f, 0f);
		}
		else if (this.timeTarget != null)
		{
			if (Vector3.Distance(this.timeTarget.position, this._thisTransform.position) > 15f)
			{
				num = Vector3.Distance(this.timeTarget.position, this._thisTransform.position) / 2f;
			}
			vector2 = this.timeTarget.position - this._thisTransform.position + new Vector3(0f, num + 1.75f, 0f);
		}
		else
		{
			if (Vector3.Distance(new Vector3(128f, 64f, 128f), this._thisTransform.position) > 15f)
			{
				num = Vector3.Distance(new Vector3(128f, 64f, 128f), this._thisTransform.position) / 2f;
			}
			vector2 = new Vector3(128f, 64f, 128f) - this._thisTransform.position + new Vector3(0f, num + 1.75f, 0f);
		}
		float num2 = this.turnSpeed * Time.deltaTime;
		float num3 = Vector3.Angle(this._thisTransform.forward, vector2);
		if (num3 <= num2)
		{
			this._thisTransform.forward = vector2.normalized;
		}
		else
		{
			this._thisTransform.forward = Vector3.Slerp(this._thisTransform.forward, vector2.normalized, num2 / num3);
		}
		if (vector2.magnitude < vector.magnitude)
		{
			this.Explode();
			return;
		}
		this._thisTransform.position += vector;
	}

	// Token: 0x060002DC RID: 732 RVA: 0x00039EBC File Offset: 0x000380BC
	public void Explode()
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
		base.GetComponent<Renderer>().castShadows = false;
		base.GetComponent<Renderer>().receiveShadows = false;
		base.GetComponent<Renderer>().enabled = false;
		base.gameObject.transform.parent = null;
		base.gameObject.GetComponentInChildren<ParticleSystem>().loop = false;
		base.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
		if (this.timeTarget == null && this.id == this.cscl.myindex)
		{
			this.cscl.send_detonateent(this.uid, this._thisTransform.position);
		}
		if (this.targetPlayerID == this.cscl.myindex)
		{
			if (this.targetEntClassID != CONST.ENTS.ENT_JEEP)
			{
				TankController tankController = (TankController)Object.FindObjectOfType(typeof(TankController));
				if (tankController.enabled)
				{
					tankController.javelinAIM(3);
					tankController.missle = null;
				}
			}
			else
			{
				CarController carController = (CarController)Object.FindObjectOfType(typeof(CarController));
				if (carController.enabled)
				{
					carController.javelinAIM(3);
					carController.missle = null;
				}
			}
		}
		this.KillSelf();
	}

	// Token: 0x060002DD RID: 733 RVA: 0x0003A021 File Offset: 0x00038221
	private void KillSelf()
	{
		if (base.gameObject == null)
		{
			return;
		}
		Object.Destroy(base.gameObject);
		EntManager.DeleteEnt(this.entid);
	}

	// Token: 0x060002DE RID: 734 RVA: 0x0003A048 File Offset: 0x00038248
	private void OnCollisionEnter(Collision collision)
	{
		this.Explode();
	}

	// Token: 0x060002DF RID: 735 RVA: 0x0003A050 File Offset: 0x00038250
	private void OnTriggerEnter(Collider col)
	{
		if (base.gameObject == null)
		{
			return;
		}
		if (col.GetComponent<Data>() != null)
		{
			this.Explode();
		}
	}

	// Token: 0x04000554 RID: 1364
	public Transform target;

	// Token: 0x04000555 RID: 1365
	public Transform timeTarget;

	// Token: 0x04000556 RID: 1366
	public GameObject explosionPrefab;

	// Token: 0x04000557 RID: 1367
	public float speed = 15f;

	// Token: 0x04000558 RID: 1368
	public float turnSpeed = 100f;

	// Token: 0x04000559 RID: 1369
	public float explosionTime = 10f;

	// Token: 0x0400055A RID: 1370
	public int id;

	// Token: 0x0400055B RID: 1371
	public int uid;

	// Token: 0x0400055C RID: 1372
	public int entid;

	// Token: 0x0400055D RID: 1373
	public int targetPlayerID;

	// Token: 0x0400055E RID: 1374
	public int targetEntClassID;

	// Token: 0x0400055F RID: 1375
	private Client cscl;

	// Token: 0x04000560 RID: 1376
	private EntManager entmanager;

	// Token: 0x04000561 RID: 1377
	private Transform _thisTransform;

	// Token: 0x04000562 RID: 1378
	private bool exploded;
}
