using System;
using UnityEngine;

// Token: 0x02000008 RID: 8
public class Molotov2 : MonoBehaviour
{
	// Token: 0x0600001F RID: 31 RVA: 0x000027D8 File Offset: 0x000009D8
	private void OnCollisionEnter(Collision col)
	{
		if (base.gameObject == null)
		{
			return;
		}
		if (this.coll)
		{
			return;
		}
		this.coll = true;
		for (int i = 0; i < 10; i++)
		{
			Object.Instantiate<GameObject>(this.torch, base.gameObject.transform.position + Vector3.up * (float)i * 0.05f, this.torch.transform.rotation).GetComponent<Rigidbody>().AddForce(Vector3.up * 20f + Vector3.right * (float)Random.Range(-30, 30) + Vector3.forward * (float)Random.Range(-30, 30));
		}
	}

	// Token: 0x04000017 RID: 23
	public GameObject torch;

	// Token: 0x04000018 RID: 24
	private bool coll;
}
