using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000074 RID: 116
public class VehicleSmoke : MonoBehaviour
{
	// Token: 0x06000347 RID: 839 RVA: 0x0003BD06 File Offset: 0x00039F06
	private void Awake()
	{
		this.s = (Sound)Object.FindObjectOfType(typeof(Sound));
		this.AS = base.gameObject.GetComponent<AudioSource>();
	}

	// Token: 0x06000348 RID: 840 RVA: 0x0003BD33 File Offset: 0x00039F33
	private IEnumerator Start()
	{
		this.Smoke();
		yield return new WaitForSeconds(20f);
		this.KillSelf();
		yield break;
	}

	// Token: 0x06000349 RID: 841 RVA: 0x0003BD42 File Offset: 0x00039F42
	private void KillSelf()
	{
		EntManager.DeleteEnt(this.entid);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0600034A RID: 842 RVA: 0x0003BD5A File Offset: 0x00039F5A
	private void Smoke()
	{
		this.s.PlaySound_SmokeGrenade(this.AS);
		this.Smoke1.Play();
		this.Smoke2.Play();
		this.Smoke3.Play();
		this.Smoke4.Play();
	}

	// Token: 0x040005EB RID: 1515
	public int id;

	// Token: 0x040005EC RID: 1516
	public int uid;

	// Token: 0x040005ED RID: 1517
	public int entid;

	// Token: 0x040005EE RID: 1518
	private Client cscl;

	// Token: 0x040005EF RID: 1519
	private EntManager entmanager;

	// Token: 0x040005F0 RID: 1520
	public ParticleSystem Smoke1;

	// Token: 0x040005F1 RID: 1521
	public ParticleSystem Smoke2;

	// Token: 0x040005F2 RID: 1522
	public ParticleSystem Smoke3;

	// Token: 0x040005F3 RID: 1523
	public ParticleSystem Smoke4;

	// Token: 0x040005F4 RID: 1524
	private Sound s;

	// Token: 0x040005F5 RID: 1525
	private AudioSource AS;
}
