using System;
using UnityEngine;

// Token: 0x02000067 RID: 103
public class Minigun : MonoBehaviour
{
	// Token: 0x06000304 RID: 772 RVA: 0x0003A9F4 File Offset: 0x00038BF4
	private void Update()
	{
		this.cannon.Rotate(Vector3.forward * (float)this.speed * Time.deltaTime);
		if (this.S == null)
		{
			this.S = GameObject.Find("Player").GetComponent<Sound>();
		}
		if (this.AS == null)
		{
			this.AS = GameObject.Find("Player/MainCamera/TMPAudio").GetComponent<AudioSource>();
		}
		if (this.speed > 0)
		{
			float num = (float)(this.speed - 50) / 800f;
			if (num < 0f)
			{
				num = 0f;
			}
			this.S.PlaySound_Pich(num, this.AS);
			if (this.speedUp)
			{
				this.S.PlaySound_MinigunMotor(this.AS);
			}
		}
		if (!this.speedUp && this.speed > 0)
		{
			this.speed -= this.speed / 100 + 1;
			if (this.speed <= 0)
			{
				this.speed = 0;
			}
			if (this.speed <= 100)
			{
				this.S.PlaySound_Stop(this.AS);
			}
		}
	}

	// Token: 0x0400058D RID: 1421
	public Transform cannon;

	// Token: 0x0400058E RID: 1422
	public int speed;

	// Token: 0x0400058F RID: 1423
	public bool speedUp;

	// Token: 0x04000590 RID: 1424
	private AudioSource AS;

	// Token: 0x04000591 RID: 1425
	private Sound S;
}
