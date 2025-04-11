using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000062 RID: 98
public class M202Rocket : MonoBehaviour
{
	// Token: 0x060002E1 RID: 737 RVA: 0x0003A09E File Offset: 0x0003829E
	private void Awake()
	{
		this.entmanager = (EntManager)Object.FindObjectOfType(typeof(EntManager));
		if (this.obj == null)
		{
			this.obj = base.gameObject;
		}
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x0003A0D4 File Offset: 0x000382D4
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
		yield return new WaitForSeconds(3f);
		this.KillSelf();
		yield break;
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x0003A0E3 File Offset: 0x000382E3
	private void KillSelf()
	{
		if (base.gameObject == null)
		{
			return;
		}
		Object.Destroy(base.gameObject);
		EntManager.DeleteEnt(this.entid);
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x0003A10A File Offset: 0x0003830A
	private void OnCollisionEnter(Collision collision)
	{
		this.Explode();
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x0003A112 File Offset: 0x00038312
	private void OnTriggerEnter(Collider col)
	{
		if (col.GetComponent<Data>() != null)
		{
			this.Explode();
		}
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x0003A128 File Offset: 0x00038328
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

	// Token: 0x04000563 RID: 1379
	public int id;

	// Token: 0x04000564 RID: 1380
	public int uid;

	// Token: 0x04000565 RID: 1381
	public int entid;

	// Token: 0x04000566 RID: 1382
	private Client cscl;

	// Token: 0x04000567 RID: 1383
	private EntManager entmanager;

	// Token: 0x04000568 RID: 1384
	private GameObject obj;

	// Token: 0x04000569 RID: 1385
	private bool exploded;
}
