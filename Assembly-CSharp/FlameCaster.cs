using System;
using UnityEngine;

// Token: 0x0200000D RID: 13
public class FlameCaster : MonoBehaviour
{
	// Token: 0x06000030 RID: 48 RVA: 0x00002B77 File Offset: 0x00000D77
	private void Start()
	{
		this.myTransform = base.transform;
		this.myPS = base.GetComponent<ParticleSystem>();
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00002B94 File Offset: 0x00000D94
	private void Update()
	{
		if (!this.myPS.isPlaying)
		{
			return;
		}
		if (this.timer > Time.time)
		{
			return;
		}
		this.timer = Time.time + 0.1f;
		Ray ray;
		ray..ctor(this.myTransform.position, this.myTransform.forward);
		ray.direction += new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
		RaycastHit raycastHit;
		if (!Physics.Raycast(ray, ref raycastHit, 20f))
		{
			return;
		}
		if (ParticleManager.THIS == null)
		{
			return;
		}
		ParticleManager.THIS.CreateFlame(raycastHit.point, raycastHit.transform);
	}

	// Token: 0x04000025 RID: 37
	private ParticleSystem myPS;

	// Token: 0x04000026 RID: 38
	private Transform myTransform;

	// Token: 0x04000027 RID: 39
	private float timer;
}
