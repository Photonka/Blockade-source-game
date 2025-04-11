using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000066 RID: 102
public class Minen : MonoBehaviour
{
	// Token: 0x060002FC RID: 764 RVA: 0x0003A704 File Offset: 0x00038904
	private void Awake()
	{
		this.entmanager = (EntManager)Object.FindObjectOfType(typeof(EntManager));
		this.obj = base.gameObject;
		this.starttime = Time.time;
		this.myTransform = base.transform;
		Sound sound = null;
		if (sound == null)
		{
			sound = (Sound)Object.FindObjectOfType(typeof(Sound));
		}
		base.GetComponent<AudioSource>().clip = sound.GetMineFly();
		base.GetComponent<AudioSource>().Play();
	}

	// Token: 0x060002FD RID: 765 RVA: 0x0003A78C File Offset: 0x0003898C
	private void Update()
	{
		float num = Vector3.Distance(this.targetPosition, this.myTransform.position);
		if (num < 1f)
		{
			this.Explode();
			return;
		}
		this.myTransform.up = Vector3.Lerp(this.myTransform.up, this.targetPosition - this.myTransform.position, Time.deltaTime * (this.BallisticKoef / num));
		this.myTransform.Translate(-this.myTransform.up * this.speed * Time.deltaTime);
	}

	// Token: 0x060002FE RID: 766 RVA: 0x0003A82E File Offset: 0x00038A2E
	private IEnumerator Start()
	{
		if (this.obj == null)
		{
			yield break;
		}
		yield return new WaitForSeconds(15f);
		this.KillSelf();
		yield break;
	}

	// Token: 0x060002FF RID: 767 RVA: 0x0003A83D File Offset: 0x00038A3D
	private void KillSelf()
	{
		if (this.obj == null)
		{
			return;
		}
		EntManager.DeleteEnt(this.entid);
		Object.Destroy(this.obj);
	}

	// Token: 0x06000300 RID: 768 RVA: 0x0003A864 File Offset: 0x00038A64
	private void OnCollisionEnter(Collision collision)
	{
		if (Time.time > this.starttime + 0.5f)
		{
			this.collignore = false;
		}
		if (this.collignore)
		{
			return;
		}
		this.Explode();
	}

	// Token: 0x06000301 RID: 769 RVA: 0x0003A890 File Offset: 0x00038A90
	private void OnTriggerEnter(Collider col)
	{
		if (base.gameObject == null)
		{
			return;
		}
		if (Time.time > this.starttime + 0.5f)
		{
			this.collignore = false;
		}
		if (this.collignore)
		{
			return;
		}
		if (col.GetComponent<Data>() != null)
		{
			this.Explode();
		}
	}

	// Token: 0x06000302 RID: 770 RVA: 0x0003A8E4 File Offset: 0x00038AE4
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
		base.GetComponentInChildren<Renderer>().castShadows = false;
		base.GetComponentInChildren<Renderer>().receiveShadows = false;
		base.GetComponentInChildren<Renderer>().enabled = false;
		base.gameObject.transform.parent = null;
		base.gameObject.GetComponentInChildren<ParticleSystem>().loop = false;
		base.gameObject.GetComponentInChildren<ParticleSystem>().Stop();
		if (this.id == this.cscl.myindex)
		{
			this.cscl.send_detonateent(this.uid, base.gameObject.transform.position);
		}
	}

	// Token: 0x04000580 RID: 1408
	public int id;

	// Token: 0x04000581 RID: 1409
	public int uid;

	// Token: 0x04000582 RID: 1410
	public int entid;

	// Token: 0x04000583 RID: 1411
	private Client cscl;

	// Token: 0x04000584 RID: 1412
	private EntManager entmanager;

	// Token: 0x04000585 RID: 1413
	private GameObject obj;

	// Token: 0x04000586 RID: 1414
	private float starttime;

	// Token: 0x04000587 RID: 1415
	private bool collignore = true;

	// Token: 0x04000588 RID: 1416
	private float BallisticKoef = 10f;

	// Token: 0x04000589 RID: 1417
	public Vector3 targetPosition;

	// Token: 0x0400058A RID: 1418
	private Transform myTransform;

	// Token: 0x0400058B RID: 1419
	private float speed = 30f;

	// Token: 0x0400058C RID: 1420
	private bool exploded;
}
