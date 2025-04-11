using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000054 RID: 84
public class ATMine : MonoBehaviour
{
	// Token: 0x0600029D RID: 669 RVA: 0x00002B75 File Offset: 0x00000D75
	private void Update()
	{
	}

	// Token: 0x0600029E RID: 670 RVA: 0x00038E64 File Offset: 0x00037064
	private void Awake()
	{
		this.entmanager = (EntManager)Object.FindObjectOfType(typeof(EntManager));
		this.obj = GameObject.Find("/" + base.gameObject.name);
		this.myTransform = base.transform;
	}

	// Token: 0x0600029F RID: 671 RVA: 0x00038EB7 File Offset: 0x000370B7
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
		yield return new WaitForSeconds(180f);
		this.KillSelf();
		yield break;
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x00038EC6 File Offset: 0x000370C6
	public void KillSelf()
	{
		if (base.gameObject == null)
		{
			return;
		}
		Object.Destroy(base.gameObject);
		EntManager.DeleteEnt(this.entid);
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x00002B75 File Offset: 0x00000D75
	private void OnCollisionEnter(Collision collision)
	{
	}

	// Token: 0x060002A2 RID: 674 RVA: 0x00038EF0 File Offset: 0x000370F0
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
		if (this.id == this.cscl.myindex)
		{
			this.cscl.send_detonateent(this.uid, base.gameObject.transform.position);
		}
	}

	// Token: 0x0400050A RID: 1290
	public int id;

	// Token: 0x0400050B RID: 1291
	public int uid;

	// Token: 0x0400050C RID: 1292
	public int entid;

	// Token: 0x0400050D RID: 1293
	private Client cscl;

	// Token: 0x0400050E RID: 1294
	private EntManager entmanager;

	// Token: 0x0400050F RID: 1295
	private GameObject obj;

	// Token: 0x04000510 RID: 1296
	private Transform myTransform;

	// Token: 0x04000511 RID: 1297
	private bool exploded;
}
