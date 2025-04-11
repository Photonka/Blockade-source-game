using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200007B RID: 123
public class Crosshit : MonoBehaviour
{
	// Token: 0x06000369 RID: 873 RVA: 0x0003DA98 File Offset: 0x0003BC98
	private void Awake()
	{
		this.texhit = (Resources.Load("GUI/crosshit") as Texture);
		this.SpecDamag = (Resources.Load("GUI/SpecDamage") as Texture);
		this.indicatorGas = (Resources.Load("GUI/gas") as Texture);
		this.indicatorFire = (Resources.Load("GUI/fire") as Texture);
	}

	// Token: 0x0600036A RID: 874 RVA: 0x0003DAFC File Offset: 0x0003BCFC
	private void OnGUI()
	{
		if (this.HitTime > 0f && this.HitTime > Time.time)
		{
			Rect rect;
			rect..ctor(0f, 0f, 32f, 32f);
			rect.center = new Vector2((float)Screen.width, (float)Screen.height) / 2f;
			GUI.depth = -1;
			GUI.DrawTexture(rect, this.texhit);
		}
		if (this.FireHitTime > 0f && this.FireHitTime > Time.time)
		{
			Rect rect2 = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
			GUI.depth = -1;
			this.c = GUI.color;
			this.a = this.FireHitTime - Time.time;
			GUI.color = new Color(1f, 0f, 0f, this.a);
			GUI.DrawTexture(rect2, this.SpecDamag);
			GUI.DrawTexture(new Rect(GUIManager.XRES(512f) + 76f, GUIManager.YRES(768f) - 40f, 32f, 32f), this.indicatorFire);
			GUI.color = this.c;
		}
		if (this.GazHitTime > 0f && this.GazHitTime > Time.time)
		{
			Rect rect3 = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
			GUI.depth = -1;
			this.c = GUI.color;
			this.a = this.GazHitTime - Time.time;
			GUI.color = new Color(0f, 1f, 0f, this.a);
			GUI.DrawTexture(rect3, this.SpecDamag);
			GUI.color = this.c;
			GUI.DrawTexture(new Rect(GUIManager.XRES(512f) + 76f, GUIManager.YRES(768f) - 40f, 32f, 32f), this.indicatorGas);
		}
		if (this.Splashes.Count > 0)
		{
			int num = 0;
			foreach (ScreenSplash screenSplash in this.Splashes)
			{
				if (screenSplash.a > 0f)
				{
					screenSplash.Draw();
				}
				else
				{
					this.SplashesToDelete.Add(num);
				}
				num++;
			}
			if (this.SplashesToDelete.Count > 0)
			{
				foreach (int index in this.SplashesToDelete)
				{
					this.Splashes.RemoveAt(index);
				}
				this.SplashesToDelete.Clear();
			}
		}
	}

	// Token: 0x0600036B RID: 875 RVA: 0x0003DDE8 File Offset: 0x0003BFE8
	public void Hit()
	{
		this.HitTime = Time.time + 0.25f;
	}

	// Token: 0x0600036C RID: 876 RVA: 0x0003DDFB File Offset: 0x0003BFFB
	public void FireHit()
	{
		this.FireHitTime = Time.time + 1f;
	}

	// Token: 0x0600036D RID: 877 RVA: 0x0003DE0E File Offset: 0x0003C00E
	public void GazHit()
	{
		this.GazHitTime = Time.time + 1f;
	}

	// Token: 0x0600036E RID: 878 RVA: 0x0003DE21 File Offset: 0x0003C021
	public void SnowHit()
	{
		this.Splashes.Add(new ScreenSplash());
	}

	// Token: 0x0400064C RID: 1612
	private List<ScreenSplash> Splashes = new List<ScreenSplash>();

	// Token: 0x0400064D RID: 1613
	private List<int> SplashesToDelete = new List<int>();

	// Token: 0x0400064E RID: 1614
	private Texture texhit;

	// Token: 0x0400064F RID: 1615
	private Texture SpecDamag;

	// Token: 0x04000650 RID: 1616
	private float HitTime;

	// Token: 0x04000651 RID: 1617
	private float FireHitTime;

	// Token: 0x04000652 RID: 1618
	private float GazHitTime;

	// Token: 0x04000653 RID: 1619
	private Texture indicatorGas;

	// Token: 0x04000654 RID: 1620
	private Texture indicatorFire;

	// Token: 0x04000655 RID: 1621
	private float a;

	// Token: 0x04000656 RID: 1622
	private Color c;
}
