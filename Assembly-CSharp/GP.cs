using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200005F RID: 95
public class GP : MonoBehaviour
{
	// Token: 0x060002CD RID: 717 RVA: 0x00039900 File Offset: 0x00037B00
	private void Awake()
	{
		this.entmanager = (EntManager)Object.FindObjectOfType(typeof(EntManager));
		this.obj = GameObject.Find("/" + base.gameObject.name);
		this.starttime = Time.time;
	}

	// Token: 0x060002CE RID: 718 RVA: 0x00039952 File Offset: 0x00037B52
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

	// Token: 0x060002CF RID: 719 RVA: 0x00039964 File Offset: 0x00037B64
	private void KillSelf()
	{
		if (base.gameObject == null)
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
		if (this.id == this.cscl.myindex)
		{
			this.cscl.send_detonateent(this.uid, this.obj.transform.position);
		}
		EntManager.DeleteEnt(this.entid);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x00039A01 File Offset: 0x00037C01
	private void OnCollisionEnter(Collision collision)
	{
		if (this.collignore)
		{
			return;
		}
		if (Time.time < this.starttime + 0.2f)
		{
			this.collignore = true;
			return;
		}
		this.KillSelf();
	}

	// Token: 0x060002D1 RID: 721 RVA: 0x00039A2D File Offset: 0x00037C2D
	private void OnTriggerEnter(Collider col)
	{
		if (base.gameObject == null)
		{
			return;
		}
		if (col.GetComponent<Data>() != null)
		{
			this.KillSelf();
		}
	}

	// Token: 0x04000546 RID: 1350
	public int id;

	// Token: 0x04000547 RID: 1351
	public int uid;

	// Token: 0x04000548 RID: 1352
	public int entid;

	// Token: 0x04000549 RID: 1353
	private Client cscl;

	// Token: 0x0400054A RID: 1354
	private EntManager entmanager;

	// Token: 0x0400054B RID: 1355
	private GameObject obj;

	// Token: 0x0400054C RID: 1356
	private float starttime;

	// Token: 0x0400054D RID: 1357
	private bool collignore;
}
