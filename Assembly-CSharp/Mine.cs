using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000064 RID: 100
public class Mine : MonoBehaviour
{
	// Token: 0x060002ED RID: 749 RVA: 0x00002B75 File Offset: 0x00000D75
	private void Update()
	{
	}

	// Token: 0x060002EE RID: 750 RVA: 0x0003A34C File Offset: 0x0003854C
	private void Awake()
	{
		this.entmanager = (EntManager)Object.FindObjectOfType(typeof(EntManager));
		this.obj = GameObject.Find("/" + base.gameObject.name);
		this.myTransform = base.transform;
	}

	// Token: 0x060002EF RID: 751 RVA: 0x0003A39F File Offset: 0x0003859F
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

	// Token: 0x060002F0 RID: 752 RVA: 0x0003A3AE File Offset: 0x000385AE
	public void KillSelf()
	{
		if (base.gameObject == null)
		{
			return;
		}
		Object.Destroy(base.gameObject);
		EntManager.DeleteEnt(this.entid);
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x00002B75 File Offset: 0x00000D75
	private void OnCollisionEnter(Collision collision)
	{
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x0003A3D8 File Offset: 0x000385D8
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

	// Token: 0x0400056F RID: 1391
	public int id;

	// Token: 0x04000570 RID: 1392
	public int uid;

	// Token: 0x04000571 RID: 1393
	public int entid;

	// Token: 0x04000572 RID: 1394
	private Client cscl;

	// Token: 0x04000573 RID: 1395
	private EntManager entmanager;

	// Token: 0x04000574 RID: 1396
	private GameObject obj;

	// Token: 0x04000575 RID: 1397
	private Transform myTransform;

	// Token: 0x04000576 RID: 1398
	private bool exploded;
}
