using System;
using UnityEngine;

// Token: 0x02000127 RID: 295
public class HeadBob : MonoBehaviour
{
	// Token: 0x06000A4F RID: 2639 RVA: 0x000879A0 File Offset: 0x00085BA0
	private void Update()
	{
		if (!this.Active)
		{
			return;
		}
		int tickCount = Environment.TickCount;
		if (tickCount < this.oldtime + 10)
		{
			return;
		}
		float num = (float)(tickCount - this.oldtime);
		this.oldtime = tickCount;
		float num2 = 0f;
		float axis = Input.GetAxis("Horizontal");
		float axis2 = Input.GetAxis("Vertical");
		if (Mathf.Abs(axis) == 0f && Mathf.Abs(axis2) == 0f)
		{
			this.timer = 0f;
		}
		else
		{
			num2 = Mathf.Sin(this.timer);
			this.timer += this.bobbingSpeed * num * 0.1f;
			if (this.timer > 6.2831855f)
			{
				this.timer -= 6.2831855f;
			}
		}
		if (num2 != 0f)
		{
			float num3 = num2 * this.bobbingAmount;
			num3 = Mathf.Clamp(Mathf.Abs(axis) + Mathf.Abs(axis2), 0f, 1f) * num3;
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, this.midpoint + num3, base.transform.localPosition.z);
			return;
		}
		base.transform.localPosition = new Vector3(base.transform.localPosition.x, this.midpoint, base.transform.localPosition.z);
	}

	// Token: 0x04000F9F RID: 3999
	public bool Active;

	// Token: 0x04000FA0 RID: 4000
	private float timer;

	// Token: 0x04000FA1 RID: 4001
	private float bobbingSpeed = 0.075f;

	// Token: 0x04000FA2 RID: 4002
	private float bobbingAmount = 0.07f;

	// Token: 0x04000FA3 RID: 4003
	private float midpoint = 1.75f;

	// Token: 0x04000FA4 RID: 4004
	private int oldtime;
}
