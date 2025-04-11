using System;
using UnityEngine;

// Token: 0x0200000A RID: 10
public class CustomAnimation : MonoBehaviour
{
	// Token: 0x06000026 RID: 38 RVA: 0x00002A3F File Offset: 0x00000C3F
	private void Awake()
	{
		this.rightColtMuzzle.SetActive(false);
		this.leftColtMuzzle.SetActive(false);
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00002A5C File Offset: 0x00000C5C
	public void DrawIn()
	{
		this.rightColtMuzzle.SetActive(false);
		this.leftColtMuzzle.SetActive(false);
		this.rightColt.GetComponent<Animation>().Play("DRAW_IN");
		this.leftColt.GetComponent<Animation>().Play("DRAW_IN");
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00002AAD File Offset: 0x00000CAD
	public void RightColtFire()
	{
		this.rightColt.GetComponent<Animation>().Play("FIRE");
		this.rightColtMuzzle.SetActive(true);
		this.muzzleTime = Time.time + 0.05f;
		this.rightFiring = true;
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00002AE9 File Offset: 0x00000CE9
	public void LeftColtFire()
	{
		this.leftColt.GetComponent<Animation>().Play("FIRE");
		this.leftColtMuzzle.SetActive(true);
		this.muzzleTime = Time.time + 0.05f;
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002B1E File Offset: 0x00000D1E
	private void Update()
	{
		if (this.muzzleTime > 0f && this.muzzleTime < Time.time)
		{
			this.rightColtMuzzle.SetActive(false);
			this.leftColtMuzzle.SetActive(false);
			this.muzzleTime = 0f;
		}
	}

	// Token: 0x0400001E RID: 30
	public GameObject rightColt;

	// Token: 0x0400001F RID: 31
	public GameObject leftColt;

	// Token: 0x04000020 RID: 32
	public GameObject rightColtMuzzle;

	// Token: 0x04000021 RID: 33
	public GameObject leftColtMuzzle;

	// Token: 0x04000022 RID: 34
	private float muzzleTime;

	// Token: 0x04000023 RID: 35
	public bool rightFiring;
}
