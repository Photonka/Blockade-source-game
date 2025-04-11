using System;
using UnityEngine;

// Token: 0x02000079 RID: 121
public class Crosshair : MonoBehaviour
{
	// Token: 0x06000362 RID: 866 RVA: 0x00002B75 File Offset: 0x00000D75
	private void Awake()
	{
	}

	// Token: 0x06000363 RID: 867 RVA: 0x0003D848 File Offset: 0x0003BA48
	private void OnGUI()
	{
		if (!this.active || MainGUI.ForceCursor)
		{
			return;
		}
		float num = this.ShootTime - Time.time;
		if (num <= 0f)
		{
			num = 0f;
		}
		num *= 1.2f;
		Rect rect;
		rect..ctor(0f, 0f, 32f + 32f * num, 32f + 32f * num);
		rect.center = new Vector2((float)Screen.width, (float)Screen.height) / 2f;
		GUI.color = PlayerProfile.crossColor;
		GUI.DrawTexture(rect, PlayerProfile.crossList[Config.cross]);
		if (Config.dot > 0)
		{
			GUI.DrawTexture(rect, PlayerProfile.crossDot[Config.dot]);
		}
		GUI.color = Color.white;
	}

	// Token: 0x06000364 RID: 868 RVA: 0x0003D91B File Offset: 0x0003BB1B
	public void Shoot(float interval)
	{
		this.ShootTime = Time.time + interval;
	}

	// Token: 0x06000365 RID: 869 RVA: 0x0003D92A File Offset: 0x0003BB2A
	public void SetActive(bool val)
	{
		this.active = val;
	}

	// Token: 0x04000643 RID: 1603
	private float ShootTime;

	// Token: 0x04000644 RID: 1604
	public bool active = true;
}
