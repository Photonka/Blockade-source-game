using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000065 RID: 101
public class Minefly : MonoBehaviour
{
	// Token: 0x060002F4 RID: 756 RVA: 0x0003A498 File Offset: 0x00038698
	private void Update()
	{
		if (this.chekTime < Time.time)
		{
			this.chekTime = Time.time + 0.2f;
			this.myTransform.LookAt(this.myTransform.position + base.gameObject.GetComponent<Rigidbody>().velocity);
			this.myTransform.eulerAngles += new Vector3(90f, 0f, 90f);
		}
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x0003A518 File Offset: 0x00038718
	private void Awake()
	{
		this.entmanager = (EntManager)Object.FindObjectOfType(typeof(EntManager));
		this.obj = GameObject.Find("/" + base.gameObject.name);
		this.chekTime = Time.time;
		this.myTransform = base.transform;
		Sound sound = null;
		if (sound == null)
		{
			sound = (Sound)Object.FindObjectOfType(typeof(Sound));
		}
		base.GetComponent<AudioSource>().clip = sound.GetMineFly();
		base.GetComponent<AudioSource>().Play();
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x0003A5B2 File Offset: 0x000387B2
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
		yield return new WaitForSeconds(10f);
		this.KillSelf();
		yield break;
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x0003A5C1 File Offset: 0x000387C1
	private void KillSelf()
	{
		if (base.gameObject == null)
		{
			return;
		}
		Object.Destroy(base.gameObject);
		EntManager.DeleteEnt(this.entid);
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x0003A5E8 File Offset: 0x000387E8
	private void OnCollisionEnter(Collision collision)
	{
		this.Explode();
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x0003A5F0 File Offset: 0x000387F0
	private void OnTriggerEnter(Collider col)
	{
		if (col.GetComponent<Data>() != null)
		{
			this.Explode();
		}
	}

	// Token: 0x060002FA RID: 762 RVA: 0x0003A608 File Offset: 0x00038808
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
		base.GetComponent<AudioSource>().Stop();
		this.exploded = true;
		base.GetComponent<Renderer>().castShadows = false;
		base.GetComponent<Renderer>().receiveShadows = false;
		base.GetComponent<Renderer>().enabled = false;
		base.gameObject.transform.parent = null;
		base.gameObject.GetComponent<Rigidbody>().Sleep();
		base.gameObject.GetComponentInChildren<ParticleSystem>().loop = false;
		base.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
		if (this.id == this.cscl.myindex)
		{
			this.cscl.send_detonateent(this.uid, base.gameObject.transform.position);
		}
	}

	// Token: 0x04000577 RID: 1399
	public int id;

	// Token: 0x04000578 RID: 1400
	public int uid;

	// Token: 0x04000579 RID: 1401
	public int entid;

	// Token: 0x0400057A RID: 1402
	private Client cscl;

	// Token: 0x0400057B RID: 1403
	private EntManager entmanager;

	// Token: 0x0400057C RID: 1404
	private GameObject obj;

	// Token: 0x0400057D RID: 1405
	private Transform myTransform;

	// Token: 0x0400057E RID: 1406
	private float chekTime;

	// Token: 0x0400057F RID: 1407
	private bool exploded;
}
