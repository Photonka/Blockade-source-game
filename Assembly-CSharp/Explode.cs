using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200005B RID: 91
public class Explode : MonoBehaviour
{
	// Token: 0x060002BC RID: 700 RVA: 0x00039475 File Offset: 0x00037675
	private void Awake()
	{
		this.explode = ContentLoader.LoadSound("grenade");
		this.fx = (Resources.Load("Prefab/explode") as GameObject);
	}

	// Token: 0x060002BD RID: 701 RVA: 0x0003949C File Offset: 0x0003769C
	private IEnumerator Start()
	{
		AudioSource audioSource = base.gameObject.AddComponent<AudioSource>();
		audioSource.maxDistance = 35f;
		audioSource.spatialBlend = 1f;
		audioSource.playOnAwake = false;
		audioSource.PlayOneShot(this.explode, AudioListener.volume);
		Object.Instantiate<GameObject>(this.fx).transform.position = base.gameObject.transform.position;
		yield return new WaitForSeconds(1f);
		this.KillSelf();
		yield break;
	}

	// Token: 0x060002BE RID: 702 RVA: 0x00017ECE File Offset: 0x000160CE
	private void KillSelf()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x0400052F RID: 1327
	private AudioClip explode;

	// Token: 0x04000530 RID: 1328
	private GameObject fx;
}
