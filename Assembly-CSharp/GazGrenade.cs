using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200005E RID: 94
public class GazGrenade : MonoBehaviour
{
	// Token: 0x060002C7 RID: 711 RVA: 0x00039700 File Offset: 0x00037900
	private void Awake()
	{
		this.entmanager = (EntManager)Object.FindObjectOfType(typeof(EntManager));
		this.s = (Sound)Object.FindObjectOfType(typeof(Sound));
		this.AS = base.gameObject.GetComponent<AudioSource>();
	}

	// Token: 0x060002C8 RID: 712 RVA: 0x00039752 File Offset: 0x00037952
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(2.5f);
		this.Smoke();
		yield return new WaitForSeconds(20f);
		this.KillSelf();
		yield break;
	}

	// Token: 0x060002C9 RID: 713 RVA: 0x00039764 File Offset: 0x00037964
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
		if (GameObject.Find("/" + base.gameObject.name) == null)
		{
			return;
		}
		EntManager.DeleteEnt(this.entid);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060002CA RID: 714 RVA: 0x000397E4 File Offset: 0x000379E4
	private void Smoke()
	{
		ParticleSystem componentInChildren = base.gameObject.GetComponentInChildren<ParticleSystem>();
		if (componentInChildren == null)
		{
			this.KillSelf();
			return;
		}
		if (this.cscl == null)
		{
			this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
		}
		if (this.cscl != null && this.id == this.cscl.myindex)
		{
			this.cscl.send_new_ent_pos(this.uid, base.transform.position);
		}
		this.s.PlaySound_SmokeGrenade(this.AS);
		componentInChildren.Play();
	}

	// Token: 0x060002CB RID: 715 RVA: 0x0003988C File Offset: 0x00037A8C
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
			this.cscl.send_new_ent_pos(this.uid, base.transform.position);
		}
	}

	// Token: 0x0400053F RID: 1343
	public int id;

	// Token: 0x04000540 RID: 1344
	public int uid;

	// Token: 0x04000541 RID: 1345
	public int entid;

	// Token: 0x04000542 RID: 1346
	private Client cscl;

	// Token: 0x04000543 RID: 1347
	private EntManager entmanager;

	// Token: 0x04000544 RID: 1348
	private Sound s;

	// Token: 0x04000545 RID: 1349
	private AudioSource AS;
}
