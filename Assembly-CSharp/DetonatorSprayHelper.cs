using System;
using UnityEngine;

// Token: 0x0200012A RID: 298
public class DetonatorSprayHelper : MonoBehaviour
{
	// Token: 0x06000A66 RID: 2662 RVA: 0x00087D58 File Offset: 0x00085F58
	private void Start()
	{
		this.startTime = Random.value * (this.startTimeMax - this.startTimeMin) + this.startTimeMin + Time.time;
		this.stopTime = Random.value * (this.stopTimeMax - this.stopTimeMin) + this.stopTimeMin + Time.time;
		base.GetComponent<Renderer>().material = ((Random.value > 0.5f) ? this.firstMaterial : this.secondMaterial);
	}

	// Token: 0x06000A67 RID: 2663 RVA: 0x00087DD6 File Offset: 0x00085FD6
	private void FixedUpdate()
	{
		float time = Time.time;
		float num = this.startTime;
		float time2 = Time.time;
		float num2 = this.stopTime;
	}

	// Token: 0x04000FB4 RID: 4020
	public float startTimeMin;

	// Token: 0x04000FB5 RID: 4021
	public float startTimeMax;

	// Token: 0x04000FB6 RID: 4022
	public float stopTimeMin = 10f;

	// Token: 0x04000FB7 RID: 4023
	public float stopTimeMax = 10f;

	// Token: 0x04000FB8 RID: 4024
	public Material firstMaterial;

	// Token: 0x04000FB9 RID: 4025
	public Material secondMaterial;

	// Token: 0x04000FBA RID: 4026
	private float startTime;

	// Token: 0x04000FBB RID: 4027
	private float stopTime;

	// Token: 0x04000FBC RID: 4028
	private bool isReallyOn;
}
