using System;
using UnityEngine;

// Token: 0x0200007A RID: 122
public class ScreenSplash
{
	// Token: 0x06000367 RID: 871 RVA: 0x0003D944 File Offset: 0x0003BB44
	public ScreenSplash()
	{
		this.splash = Resources.Load<Texture2D>("GUI/snowball_screen");
		float num = (float)Random.Range(Screen.height / 2, Screen.height);
		this.drawRect = new Rect(Random.Range(-1f * num / 2f, (float)Screen.width - num / 2f), Random.Range(-1f * num / 2f, (float)Screen.height - num / 2f), num, num);
		this.pivot = this.drawRect.center;
		this.rotateAngle = (float)Random.Range(0, 360);
		this.splashTime = Time.time + this.splashTimer;
	}

	// Token: 0x06000368 RID: 872 RVA: 0x0003DA14 File Offset: 0x0003BC14
	public void Draw()
	{
		int depth = GUI.depth;
		Color color = GUI.color;
		Matrix4x4 matrix = GUI.matrix;
		GUI.depth = -1;
		this.a = this.splashTime - Time.time;
		GUI.color = new Color(1f, 1f, 1f, this.a);
		GUIUtility.RotateAroundPivot(this.rotateAngle, this.pivot);
		GUI.DrawTexture(this.drawRect, this.splash);
		GUI.matrix = matrix;
		GUI.color = color;
		GUI.depth = depth;
	}

	// Token: 0x04000645 RID: 1605
	private Texture2D splash;

	// Token: 0x04000646 RID: 1606
	private Rect drawRect;

	// Token: 0x04000647 RID: 1607
	private Vector2 pivot;

	// Token: 0x04000648 RID: 1608
	private float rotateAngle;

	// Token: 0x04000649 RID: 1609
	public float a = 1f;

	// Token: 0x0400064A RID: 1610
	private float splashTime;

	// Token: 0x0400064B RID: 1611
	private float splashTimer = 5f;
}
