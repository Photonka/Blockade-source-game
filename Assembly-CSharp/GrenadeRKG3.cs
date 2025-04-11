using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000060 RID: 96
public class GrenadeRKG3 : MonoBehaviour
{
	// Token: 0x060002D3 RID: 723 RVA: 0x00039A52 File Offset: 0x00037C52
	private void Awake()
	{
		this.entmanager = (EntManager)Object.FindObjectOfType(typeof(EntManager));
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x00039A6E File Offset: 0x00037C6E
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(3f);
		this.KillSelf();
		yield break;
	}

	// Token: 0x060002D5 RID: 725 RVA: 0x00039A7D File Offset: 0x00037C7D
	private void KillSelf()
	{
		if (base.gameObject == null)
		{
			return;
		}
		Object.Destroy(base.gameObject);
		EntManager.DeleteEnt(this.entid);
	}

	// Token: 0x060002D6 RID: 726 RVA: 0x00039AA4 File Offset: 0x00037CA4
	private void OnCollisionEnter(Collision collision)
	{
		this.Explode(collision.collider);
	}

	// Token: 0x060002D7 RID: 727 RVA: 0x00039AB2 File Offset: 0x00037CB2
	private void OnTriggerEnter(Collider col)
	{
		if (base.gameObject == null)
		{
			return;
		}
		if (col.GetComponent<Data>() != null)
		{
			this.Explode(col);
		}
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x00039AD8 File Offset: 0x00037CD8
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
			if (collision.transform.name[0] == '(')
			{
				this.cscl.send_detonateent(this.uid, base.gameObject.transform.position);
				return;
			}
			this.cscl.send_detonateent(this.uid, collision.transform.position);
		}
	}

	// Token: 0x0400054E RID: 1358
	public int id;

	// Token: 0x0400054F RID: 1359
	public int uid;

	// Token: 0x04000550 RID: 1360
	public int entid;

	// Token: 0x04000551 RID: 1361
	private Client cscl;

	// Token: 0x04000552 RID: 1362
	private EntManager entmanager;

	// Token: 0x04000553 RID: 1363
	private bool exploded;
}
