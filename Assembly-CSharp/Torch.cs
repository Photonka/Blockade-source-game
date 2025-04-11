using System;
using UnityEngine;

// Token: 0x02000009 RID: 9
public class Torch : MonoBehaviour
{
	// Token: 0x06000021 RID: 33 RVA: 0x000028A8 File Offset: 0x00000AA8
	private void Start()
	{
		this.deadtime = Time.time + (float)Random.Range(10, 15);
		this.myRigidBody = base.gameObject.GetComponent<Rigidbody>();
		this.myCollider = base.gameObject.GetComponent<Collider>();
		this.myTransform = base.transform;
	}

	// Token: 0x06000022 RID: 34 RVA: 0x000028FC File Offset: 0x00000AFC
	private void Update()
	{
		if (base.gameObject == null)
		{
			return;
		}
		if (this.deadtime == 0f)
		{
			return;
		}
		if (Time.time < this.deadtime)
		{
			return;
		}
		Object.Destroy(base.gameObject);
		this.deadtime = 0f;
	}

	// Token: 0x06000023 RID: 35 RVA: 0x0000294C File Offset: 0x00000B4C
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
		if (col.transform.name.Contains("Torch"))
		{
			return;
		}
		this.myRigidBody.isKinematic = true;
		Object.Destroy(this.myRigidBody);
		Object.Destroy(this.myCollider);
		this.myTransform.parent = col.collider.transform;
		this.coll = true;
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000029C8 File Offset: 0x00000BC8
	private void OnTriggerEnter(Collider col)
	{
		if (base.gameObject == null)
		{
			return;
		}
		if (this.coll)
		{
			return;
		}
		if (col.transform.name.Contains("Torch"))
		{
			return;
		}
		this.myRigidBody.isKinematic = true;
		Object.Destroy(this.myRigidBody);
		Object.Destroy(this.myCollider);
		this.myTransform.parent = col.transform;
		this.coll = true;
	}

	// Token: 0x04000019 RID: 25
	private Rigidbody myRigidBody;

	// Token: 0x0400001A RID: 26
	private Collider myCollider;

	// Token: 0x0400001B RID: 27
	private Transform myTransform;

	// Token: 0x0400001C RID: 28
	private float deadtime;

	// Token: 0x0400001D RID: 29
	private bool coll;
}
