using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200006C RID: 108
public class RPG7 : MonoBehaviour
{
	// Token: 0x0600031E RID: 798 RVA: 0x0003B17E File Offset: 0x0003937E
	private void Awake()
	{
		this.entmanager = (EntManager)Object.FindObjectOfType(typeof(EntManager));
		this.obj = base.gameObject;
	}

	// Token: 0x0600031F RID: 799 RVA: 0x0003B1A6 File Offset: 0x000393A6
	private IEnumerator Start()
	{
		if (this.obj == null)
		{
			yield break;
		}
		this.obj.GetComponent<Renderer>().enabled = false;
		this.obj.GetComponent<Renderer>().castShadows = false;
		this.obj.GetComponent<Renderer>().receiveShadows = true;
		yield return new WaitForSeconds(0.05f);
		if (this.obj.GetComponent<Renderer>().receiveShadows)
		{
			this.obj.GetComponent<Renderer>().enabled = true;
			this.obj.GetComponent<Renderer>().castShadows = true;
		}
		yield return new WaitForSeconds(2f);
		this.KillSelf();
		yield break;
	}

	// Token: 0x06000320 RID: 800 RVA: 0x0003B1B5 File Offset: 0x000393B5
	private void KillSelf()
	{
		if (base.gameObject == null)
		{
			return;
		}
		Object.Destroy(base.gameObject);
		EntManager.DeleteEnt(this.entid);
	}

	// Token: 0x06000321 RID: 801 RVA: 0x0003B1DC File Offset: 0x000393DC
	private void OnCollisionEnter(Collision collision)
	{
		this.Explode();
	}

	// Token: 0x06000322 RID: 802 RVA: 0x0003B1E4 File Offset: 0x000393E4
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

	// Token: 0x06000323 RID: 803 RVA: 0x0003B20C File Offset: 0x0003940C
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
		base.gameObject.GetComponent<Rigidbody>().Sleep();
		base.gameObject.GetComponentInChildren<ParticleSystem>().loop = false;
		base.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
		if (this.id == this.cscl.myindex)
		{
			this.cscl.send_detonateent(this.uid, base.gameObject.transform.position);
		}
	}

	// Token: 0x040005B0 RID: 1456
	public int id;

	// Token: 0x040005B1 RID: 1457
	public int uid;

	// Token: 0x040005B2 RID: 1458
	public int entid;

	// Token: 0x040005B3 RID: 1459
	private Client cscl;

	// Token: 0x040005B4 RID: 1460
	private EntManager entmanager;

	// Token: 0x040005B5 RID: 1461
	private GameObject obj;

	// Token: 0x040005B6 RID: 1462
	private bool exploded;
}
