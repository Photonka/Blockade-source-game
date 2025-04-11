using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000068 RID: 104
public class Molotov : MonoBehaviour
{
	// Token: 0x06000306 RID: 774 RVA: 0x0003AB14 File Offset: 0x00038D14
	private void Update()
	{
		if (this.chekTime < Time.time)
		{
			this.chekTime = Time.time + 0.2f;
			this.myTransform.LookAt(this.myTransform.position + base.gameObject.GetComponent<Rigidbody>().velocity);
			this.myTransform.eulerAngles += new Vector3(90f, 0f, 90f);
		}
	}

	// Token: 0x06000307 RID: 775 RVA: 0x0003AB94 File Offset: 0x00038D94
	private void Awake()
	{
		this.entmanager = (EntManager)Object.FindObjectOfType(typeof(EntManager));
		this.chekTime = Time.time;
		this.myTransform = base.transform;
		if (this.sound == null)
		{
			this.sound = (Sound)Object.FindObjectOfType(typeof(Sound));
		}
		base.GetComponent<AudioSource>().clip = this.sound.GetMolotovFly();
		base.GetComponent<AudioSource>().Play();
	}

	// Token: 0x06000308 RID: 776 RVA: 0x0003AC1B File Offset: 0x00038E1B
	private IEnumerator Start()
	{
		if (this.obj == null)
		{
			yield break;
		}
		this.obj.GetComponent<Renderer>().enabled = false;
		this.obj.GetComponent<Renderer>().castShadows = false;
		this.obj.GetComponent<Renderer>().receiveShadows = true;
		yield return new WaitForSeconds(0.1f);
		this.spec_active = true;
		if (this.obj.GetComponent<Renderer>().receiveShadows)
		{
			this.obj.GetComponent<Renderer>().enabled = true;
			this.obj.GetComponent<Renderer>().castShadows = true;
		}
		yield break;
	}

	// Token: 0x06000309 RID: 777 RVA: 0x0003AC2A File Offset: 0x00038E2A
	private void KillSelf()
	{
		if (base.gameObject == null)
		{
			return;
		}
		Object.Destroy(base.gameObject);
		EntManager.DeleteEnt(this.entid);
	}

	// Token: 0x0600030A RID: 778 RVA: 0x0003AC54 File Offset: 0x00038E54
	private void OnCollisionEnter(Collision collision)
	{
		if (!this.spec_active)
		{
			return;
		}
		if (base.gameObject == null)
		{
			return;
		}
		if (this.coll)
		{
			return;
		}
		this.coll = true;
		base.StartCoroutine(this.Explode());
		for (int i = 0; i < 10; i++)
		{
			Object.Instantiate<GameObject>(this.torch, base.gameObject.transform.position + Vector3.up * (float)i * 0.05f, this.torch.transform.rotation).GetComponent<Rigidbody>().AddForce(Vector3.up * 20f + Vector3.right * (float)Random.Range(-30, 30) + Vector3.forward * (float)Random.Range(-30, 30));
		}
	}

	// Token: 0x0600030B RID: 779 RVA: 0x0003AD3C File Offset: 0x00038F3C
	private void OnTriggerEnter(Collider col)
	{
		if (!this.spec_active)
		{
			return;
		}
		if (base.gameObject == null)
		{
			return;
		}
		if (this.coll)
		{
			return;
		}
		if (col.GetComponent<Data>() != null)
		{
			this.coll = true;
			base.StartCoroutine(this.Explode());
			for (int i = 0; i < 10; i++)
			{
				Object.Instantiate<GameObject>(this.torch, base.gameObject.transform.position + Vector3.up * (float)i * 0.05f, this.torch.transform.rotation).GetComponent<Rigidbody>().AddForce(Vector3.up * 20f + Vector3.right * (float)Random.Range(-30, 30) + Vector3.forward * (float)Random.Range(-30, 30));
			}
		}
	}

	// Token: 0x0600030C RID: 780 RVA: 0x0003AE33 File Offset: 0x00039033
	private IEnumerator Explode()
	{
		if (this.exploded)
		{
			yield break;
		}
		if (this.obj == null)
		{
			yield break;
		}
		this.flare.GetComponent<AudioSource>().PlayOneShot(this.sound.GetMolotovExplosion(), 1f);
		if (this.cscl == null)
		{
			this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
		}
		if (this.cscl == null)
		{
			yield break;
		}
		if (this.id == this.cscl.myindex)
		{
			this.cscl.send_new_ent_pos(this.uid, this.myTransform.position);
		}
		this.exploded = true;
		base.gameObject.GetComponent<Rigidbody>().Sleep();
		Object.Destroy(this.obj);
		base.GetComponent<AudioSource>().Stop();
		base.GetComponent<AudioSource>().clip = this.sound.GetMolotovBurn();
		base.GetComponent<AudioSource>().Play();
		yield return new WaitForSeconds(11f);
		this.KillSelf();
		yield break;
	}

	// Token: 0x04000592 RID: 1426
	public int id;

	// Token: 0x04000593 RID: 1427
	public int uid;

	// Token: 0x04000594 RID: 1428
	public int entid;

	// Token: 0x04000595 RID: 1429
	private Client cscl;

	// Token: 0x04000596 RID: 1430
	private EntManager entmanager;

	// Token: 0x04000597 RID: 1431
	public GameObject obj;

	// Token: 0x04000598 RID: 1432
	public GameObject flare;

	// Token: 0x04000599 RID: 1433
	private Transform myTransform;

	// Token: 0x0400059A RID: 1434
	public GameObject torch;

	// Token: 0x0400059B RID: 1435
	private bool coll;

	// Token: 0x0400059C RID: 1436
	private float chekTime;

	// Token: 0x0400059D RID: 1437
	private Sound sound;

	// Token: 0x0400059E RID: 1438
	private bool spec_active;

	// Token: 0x0400059F RID: 1439
	private bool exploded;
}
