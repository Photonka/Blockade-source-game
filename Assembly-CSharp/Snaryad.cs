using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200006F RID: 111
public class Snaryad : MonoBehaviour
{
	// Token: 0x0600032F RID: 815 RVA: 0x0003B4DC File Offset: 0x000396DC
	private void Awake()
	{
		this.entmanager = (EntManager)Object.FindObjectOfType(typeof(EntManager));
		this.obj = GameObject.Find("/" + base.gameObject.name);
		this.starttime = Time.time;
	}

	// Token: 0x06000330 RID: 816 RVA: 0x0003B52E File Offset: 0x0003972E
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

	// Token: 0x06000331 RID: 817 RVA: 0x0003B53D File Offset: 0x0003973D
	private void KillSelf()
	{
		if (base.gameObject == null)
		{
			return;
		}
		Object.Destroy(base.gameObject);
		EntManager.DeleteEnt(this.entid);
	}

	// Token: 0x06000332 RID: 818 RVA: 0x0003B564 File Offset: 0x00039764
	private void OnCollisionEnter(Collision collision)
	{
		if (Time.time > this.starttime + 0.01f)
		{
			this.collignore = false;
		}
		if (this.collignore)
		{
			return;
		}
		this.Explode(collision.collider);
	}

	// Token: 0x06000333 RID: 819 RVA: 0x0003B598 File Offset: 0x00039798
	private void OnTriggerEnter(Collider col)
	{
		if (Time.time > this.starttime + 0.01f)
		{
			this.collignore = false;
		}
		if (this.collignore)
		{
			return;
		}
		if (base.gameObject == null)
		{
			return;
		}
		if (col.GetComponent<Data>() != null)
		{
			this.Explode(col);
		}
	}

	// Token: 0x06000334 RID: 820 RVA: 0x0003B5EC File Offset: 0x000397EC
	public void Explode(Collider collision)
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
		if (this.id == this.cscl.myindex)
		{
			if (this.classid == 19)
			{
				if (collision.transform.name[0] == '(')
				{
					this.cscl.send_detonateent(this.uid, base.gameObject.transform.position);
					return;
				}
				this.cscl.send_detonateent(this.uid, collision.transform.position);
				return;
			}
			else
			{
				this.cscl.send_detonateent(this.uid, base.gameObject.transform.position);
			}
		}
	}

	// Token: 0x040005BF RID: 1471
	public int id;

	// Token: 0x040005C0 RID: 1472
	public int uid;

	// Token: 0x040005C1 RID: 1473
	public int entid;

	// Token: 0x040005C2 RID: 1474
	public int classid;

	// Token: 0x040005C3 RID: 1475
	private Client cscl;

	// Token: 0x040005C4 RID: 1476
	private EntManager entmanager;

	// Token: 0x040005C5 RID: 1477
	private GameObject obj;

	// Token: 0x040005C6 RID: 1478
	private float starttime;

	// Token: 0x040005C7 RID: 1479
	private bool collignore = true;

	// Token: 0x040005C8 RID: 1480
	private bool exploded;
}
