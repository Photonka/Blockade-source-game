using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000056 RID: 86
public class C4 : MonoBehaviour
{
	// Token: 0x060002A6 RID: 678 RVA: 0x00002B75 File Offset: 0x00000D75
	private void Update()
	{
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x00038FEC File Offset: 0x000371EC
	private void Awake()
	{
		this.entmanager = (EntManager)Object.FindObjectOfType(typeof(EntManager));
		this.obj = GameObject.Find("/" + base.gameObject.name);
		this.myTransform = base.transform;
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x0003903F File Offset: 0x0003723F
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

	// Token: 0x060002A9 RID: 681 RVA: 0x0003904E File Offset: 0x0003724E
	public void KillSelf()
	{
		if (base.gameObject == null)
		{
			return;
		}
		Object.Destroy(base.gameObject);
		EntManager.DeleteEnt(this.entid);
	}

	// Token: 0x060002AA RID: 682 RVA: 0x00039078 File Offset: 0x00037278
	private void OnCollisionEnter(Collision collision)
	{
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
			this.cscl.send_new_ent_pos(this.uid, this.myTransform.position);
		}
	}

	// Token: 0x060002AB RID: 683 RVA: 0x000390EC File Offset: 0x000372EC
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

	// Token: 0x04000518 RID: 1304
	public int id;

	// Token: 0x04000519 RID: 1305
	public int uid;

	// Token: 0x0400051A RID: 1306
	public int entid;

	// Token: 0x0400051B RID: 1307
	private Client cscl;

	// Token: 0x0400051C RID: 1308
	private EntManager entmanager;

	// Token: 0x0400051D RID: 1309
	private GameObject obj;

	// Token: 0x0400051E RID: 1310
	private Transform myTransform;

	// Token: 0x0400051F RID: 1311
	private bool exploded;
}
