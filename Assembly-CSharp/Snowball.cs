using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000070 RID: 112
public class Snowball : MonoBehaviour
{
	// Token: 0x06000336 RID: 822 RVA: 0x0003B728 File Offset: 0x00039928
	private void Awake()
	{
		this.entmanager = (EntManager)Object.FindObjectOfType(typeof(EntManager));
		this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
		this.cspm = (ParticleManager)Object.FindObjectOfType(typeof(ParticleManager));
		this.myColliders = base.GetComponents<Collider>();
		this.myTransform = base.transform;
		this.fx = base.GetComponentInChildren<ParticleSystem>();
		this.myRenderer = base.GetComponent<Renderer>();
		this.myRigidbody = base.GetComponent<Rigidbody>();
		this.startTime = Time.time;
	}

	// Token: 0x06000337 RID: 823 RVA: 0x0003B7CA File Offset: 0x000399CA
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(0.1f);
		base.gameObject.layer = 0;
		yield return new WaitForSeconds(5f);
		base.StartCoroutine(this.KillSelf());
		yield break;
	}

	// Token: 0x06000338 RID: 824 RVA: 0x0003B7D9 File Offset: 0x000399D9
	private void OnTriggerEnter(Collider collision)
	{
		if (Time.time - this.startTime < 0.1f)
		{
			return;
		}
		this.Explode(collision, this.myTransform.position, Vector3.zero);
	}

	// Token: 0x06000339 RID: 825 RVA: 0x0003B808 File Offset: 0x00039A08
	private void OnCollisionEnter(Collision collision)
	{
		if (Time.time - this.startTime < 0.1f)
		{
			return;
		}
		this.Explode(collision.collider, collision.contacts[0].point, collision.contacts[0].normal);
	}

	// Token: 0x0600033A RID: 826 RVA: 0x0003B858 File Offset: 0x00039A58
	public void Explode(Collider col, Vector3 pos, Vector3 rot)
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
		if (this.cspm == null)
		{
			this.cspm = (ParticleManager)Object.FindObjectOfType(typeof(ParticleManager));
		}
		this.exploded = true;
		if (col.gameObject.GetComponent<Data>())
		{
			if (this.cspm != null)
			{
				this.cspm.CreateParticle(this.myTransform.position.x, this.myTransform.position.y, this.myTransform.position.z, 1f, 1f, 1f, 1f);
			}
			if (this.fx != null)
			{
				this.fx.Play();
			}
		}
		else
		{
			if (this.cspm != null)
			{
				this.cspm.CreateParticle(this.myTransform.position.x, this.myTransform.position.y, this.myTransform.position.z, 1f, 1f, 1f, 1f);
			}
			if (this.fx != null)
			{
				this.fx.Play();
			}
			if (col.transform.name[0] == '(' && this.decal != null)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(this.decal);
				gameObject.transform.position = pos;
				gameObject.transform.rotation = Quaternion.LookRotation(rot);
				gameObject.transform.RotateAroundLocal(gameObject.transform.forward, (float)Random.Range(0, 360));
				Object.DestroyObject(gameObject, 5f);
			}
		}
		base.GetComponent<AudioSource>().PlayOneShot(this.soundHit);
		base.StartCoroutine(this.KillSelf());
	}

	// Token: 0x0600033B RID: 827 RVA: 0x0003BA71 File Offset: 0x00039C71
	private IEnumerator KillSelf()
	{
		if (this.myColliders.Length != 0)
		{
			Collider[] array = this.myColliders;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = false;
			}
		}
		this.myRenderer.enabled = false;
		this.myRigidbody.isKinematic = true;
		yield return new WaitForSeconds(2f);
		if (base.gameObject == null)
		{
			yield return null;
		}
		EntManager.DeleteEnt(this.entid);
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x040005C9 RID: 1481
	public int id;

	// Token: 0x040005CA RID: 1482
	public int uid;

	// Token: 0x040005CB RID: 1483
	public int entid;

	// Token: 0x040005CC RID: 1484
	private Client cscl;

	// Token: 0x040005CD RID: 1485
	private Collider[] myColliders;

	// Token: 0x040005CE RID: 1486
	private EntManager entmanager;

	// Token: 0x040005CF RID: 1487
	private ParticleManager cspm;

	// Token: 0x040005D0 RID: 1488
	private Transform myTransform;

	// Token: 0x040005D1 RID: 1489
	private Renderer myRenderer;

	// Token: 0x040005D2 RID: 1490
	private Rigidbody myRigidbody;

	// Token: 0x040005D3 RID: 1491
	public AudioClip soundHit;

	// Token: 0x040005D4 RID: 1492
	private ParticleSystem fx;

	// Token: 0x040005D5 RID: 1493
	public GameObject decal;

	// Token: 0x040005D6 RID: 1494
	private float startTime;

	// Token: 0x040005D7 RID: 1495
	private bool exploded;
}
