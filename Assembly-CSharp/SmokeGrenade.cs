using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200006E RID: 110
public class SmokeGrenade : MonoBehaviour
{
	// Token: 0x06000329 RID: 809 RVA: 0x0003B34C File Offset: 0x0003954C
	private void Awake()
	{
		this.entmanager = (EntManager)Object.FindObjectOfType(typeof(EntManager));
		this.s = (Sound)Object.FindObjectOfType(typeof(Sound));
		this.AS = base.gameObject.GetComponent<AudioSource>();
	}

	// Token: 0x0600032A RID: 810 RVA: 0x0003B39E File Offset: 0x0003959E
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(2.5f);
		this.Smoke();
		yield return new WaitForSeconds(20f);
		this.KillSelf();
		yield break;
	}

	// Token: 0x0600032B RID: 811 RVA: 0x0003B3B0 File Offset: 0x000395B0
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
			MonoBehaviour.print("Sended");
		}
	}

	// Token: 0x0600032C RID: 812 RVA: 0x0003B430 File Offset: 0x00039630
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
		if (base.gameObject == null)
		{
			return;
		}
		EntManager.DeleteEnt(this.entid);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0600032D RID: 813 RVA: 0x0003B49C File Offset: 0x0003969C
	private void Smoke()
	{
		ParticleSystem componentInChildren = base.gameObject.GetComponentInChildren<ParticleSystem>();
		if (componentInChildren == null)
		{
			this.KillSelf();
			return;
		}
		this.s.PlaySound_SmokeGrenade(this.AS);
		componentInChildren.Play();
	}

	// Token: 0x040005B8 RID: 1464
	public int id;

	// Token: 0x040005B9 RID: 1465
	public int uid;

	// Token: 0x040005BA RID: 1466
	public int entid;

	// Token: 0x040005BB RID: 1467
	private Client cscl;

	// Token: 0x040005BC RID: 1468
	private EntManager entmanager;

	// Token: 0x040005BD RID: 1469
	private Sound s;

	// Token: 0x040005BE RID: 1470
	private AudioSource AS;
}
