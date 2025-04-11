using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200006B RID: 107
public class Rocket : MonoBehaviour
{
	// Token: 0x06000317 RID: 791 RVA: 0x0003AFE0 File Offset: 0x000391E0
	private void Awake()
	{
		this.entmanager = (EntManager)Object.FindObjectOfType(typeof(EntManager));
		this.obj = GameObject.Find("/" + base.gameObject.name + "/rpg");
	}

	// Token: 0x06000318 RID: 792 RVA: 0x0003B02C File Offset: 0x0003922C
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(3.05f);
		this.KillSelf();
		yield break;
	}

	// Token: 0x06000319 RID: 793 RVA: 0x0003B03B File Offset: 0x0003923B
	private void KillSelf()
	{
		if (base.gameObject == null)
		{
			return;
		}
		Object.Destroy(base.gameObject);
		EntManager.DeleteEnt(this.entid);
	}

	// Token: 0x0600031A RID: 794 RVA: 0x0003B062 File Offset: 0x00039262
	private void OnCollisionEnter(Collision collision)
	{
		this.Explode();
	}

	// Token: 0x0600031B RID: 795 RVA: 0x0003B06A File Offset: 0x0003926A
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

	// Token: 0x0600031C RID: 796 RVA: 0x0003B090 File Offset: 0x00039290
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

	// Token: 0x040005A9 RID: 1449
	public int id;

	// Token: 0x040005AA RID: 1450
	public int uid;

	// Token: 0x040005AB RID: 1451
	public int entid;

	// Token: 0x040005AC RID: 1452
	private Client cscl;

	// Token: 0x040005AD RID: 1453
	private EntManager entmanager;

	// Token: 0x040005AE RID: 1454
	private GameObject obj;

	// Token: 0x040005AF RID: 1455
	private bool exploded;
}
