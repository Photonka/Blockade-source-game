using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000063 RID: 99
public class M61 : MonoBehaviour
{
	// Token: 0x060002E8 RID: 744 RVA: 0x0003A216 File Offset: 0x00038416
	private void Awake()
	{
		this.entmanager = (EntManager)Object.FindObjectOfType(typeof(EntManager));
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x0003A232 File Offset: 0x00038432
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(2.5f);
		this.KillSelf();
		yield break;
	}

	// Token: 0x060002EA RID: 746 RVA: 0x0003A241 File Offset: 0x00038441
	private void OnTriggerEnter(Collider col)
	{
		if (base.gameObject == null)
		{
			return;
		}
		if (col.GetComponent<Data>() != null && col.GetComponent<Data>().isGost)
		{
			this.KillSelf();
		}
	}

	// Token: 0x060002EB RID: 747 RVA: 0x0003A274 File Offset: 0x00038474
	private void KillSelf()
	{
		if (this.cscl == null)
		{
			this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
		}
		if (this.cscl == null)
		{
			return;
		}
		GameObject gameObject = GameObject.Find("/" + base.gameObject.name + "/m61");
		if (gameObject == null)
		{
			gameObject = GameObject.Find("/" + base.gameObject.name);
			if (gameObject == null)
			{
				return;
			}
		}
		if (this.id == this.cscl.myindex)
		{
			this.cscl.send_detonateent(this.uid, gameObject.transform.position);
		}
		EntManager.DeleteEnt(this.entid);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0400056A RID: 1386
	public int id;

	// Token: 0x0400056B RID: 1387
	public int uid;

	// Token: 0x0400056C RID: 1388
	public int entid;

	// Token: 0x0400056D RID: 1389
	private Client cscl;

	// Token: 0x0400056E RID: 1390
	private EntManager entmanager;
}
