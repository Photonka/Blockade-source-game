using System;
using UnityEngine;

// Token: 0x02000082 RID: 130
public class MapSize : MonoBehaviour
{
	// Token: 0x060003A6 RID: 934 RVA: 0x0004778D File Offset: 0x0004598D
	private void Awake()
	{
		this.radar = base.gameObject.GetComponent<Radar>();
		this.map = (Map)Object.FindObjectOfType(typeof(Map));
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x000477BC File Offset: 0x000459BC
	public void OnActive()
	{
		MainGUI.ForceCursor = true;
		this.x_min = (int)this.map.mlx.x;
		this.x_max = (int)this.map.mlx.y;
		this.y_min = (int)this.map.mly.x;
		this.y_max = (int)this.map.mly.y;
		this.z_min = (int)this.map.mlz.x;
		this.z_max = (int)this.map.mlz.y;
		this.r_window = new Rect(0f, 0f, 600f, 400f);
		this.r_window.center = new Vector2((float)Screen.width, (float)Screen.height) / 2f;
		this.radar.GenerateSideTexture();
		this.radar.ForceUpdateRadar();
		this.show = true;
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x000478BB File Offset: 0x00045ABB
	private void OnGUI()
	{
		if (!this.show)
		{
			return;
		}
		GUI.Window(0, this.r_window, new GUI.WindowFunction(this.DrawWindow), "", GUIManager.gs_empty);
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x000478EC File Offset: 0x00045AEC
	private void DrawWindow(int wid)
	{
		Vector2 mpos;
		mpos..ctor(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y);
		mpos.x -= this.r_window.x;
		mpos.y -= this.r_window.y;
		GUI.color = new Color(1f, 1f, 1f, 0.8f);
		GUI.DrawTexture(new Rect(0f, 0f, 600f, 32f), GUIManager.tex_black);
		GUI.color = new Color(1f, 1f, 1f, 1f);
		GUI.Label(new Rect(0f, 0f, 600f, 32f), Lang.GetLabel(129), GUIManager.gs_style1);
		GUI.color = new Color(1f, 1f, 1f, 0.6f);
		GUI.DrawTexture(new Rect(0f, 34f, 600f, 366f), GUIManager.tex_black);
		GUI.color = new Color(1f, 1f, 1f, 1f);
		this.DrawMapTop(0, 32);
		this.DrawMapSide(280, 32);
		this.DrawSize(288, 120);
		if (GUIManager.DrawButton(new Rect(8f, 358f, 256f, 32f), mpos, new Color(0f, 1f, 0f, 1f), Lang.GetLabel(123)))
		{
			this.show = false;
			MainGUI.ForceCursor = this.show;
		}
		if (GUIManager.DrawButton(new Rect(336f, 358f, 256f, 32f), mpos, new Color(1f, 0f, 0f, 1f), Lang.GetLabel(130)))
		{
			MainGUI.ForceCursor = this.show;
			this.map.mlx = new Vector2((float)this.x_min, (float)this.x_max);
			this.map.mly = new Vector2((float)this.y_min, (float)this.y_max);
			this.map.mlz = new Vector2((float)this.z_min, (float)this.z_max);
			if (this.cscl == null)
			{
				this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
			}
			this.cscl.send_private_command(4, this.cscl.myindex, 0, (byte)this.x_min, (byte)this.x_max, (byte)this.y_min, (byte)this.y_max, (byte)this.z_min, (byte)this.z_max);
		}
	}

	// Token: 0x060003AA RID: 938 RVA: 0x00047BC0 File Offset: 0x00045DC0
	private void DrawMapTop(int x, int y)
	{
		float num = GUI.HorizontalSlider(new Rect((float)(x + 8), (float)(y + 8), 128f, 32f), (float)this.x_min, 0f, 127f);
		float num2 = GUI.HorizontalSlider(new Rect((float)(x + 128 + 8), (float)(y + 8), 128f, 32f), (float)this.x_max, 128f, 255f);
		float num3 = GUI.VerticalSlider(new Rect((float)(x + 256 + 4 + 8), (float)(y + 16 + 8), 32f, 128f), (float)this.z_max, 255f, 128f);
		float num4 = GUI.VerticalSlider(new Rect((float)(x + 256 + 4 + 8), (float)(y + 16 + 128 + 8), 32f, 128f), (float)this.z_min, 127f, 0f);
		GUI.DrawTexture(new Rect((float)(x + 8), (float)(y + 8 + 16), 256f, 256f), this.radar.RadarTexture);
		if (this.x_min != (int)num || this.x_max != (int)num2 || this.z_min != (int)num4 || this.z_max != (int)num3)
		{
			if ((int)num > this.x_min)
			{
				this.x_min++;
			}
			else if ((int)num < this.x_min)
			{
				this.x_min--;
			}
			if ((int)num2 > this.x_max)
			{
				this.x_max++;
			}
			else if ((int)num2 < this.x_max)
			{
				this.x_max--;
			}
			if ((int)num4 > this.z_min)
			{
				this.z_min++;
			}
			else if ((int)num4 < this.z_min)
			{
				this.z_min--;
			}
			if ((int)num3 > this.z_max)
			{
				this.z_max++;
			}
			else if ((int)num3 < this.z_max)
			{
				this.z_max--;
			}
		}
		if (this.x_min > 0)
		{
			GUI.DrawTexture(new Rect((float)(x + 8), (float)(y + 8 + 16), (float)(this.x_min + 1), 256f), GUIManager.tex_red);
		}
		if (this.x_max < 255)
		{
			GUI.DrawTexture(new Rect((float)(x + 8 + this.x_max), (float)(y + 8 + 16), (float)(256 - this.x_max), 256f), GUIManager.tex_red);
		}
		if (this.z_max < 255)
		{
			GUI.DrawTexture(new Rect((float)(x + 8), (float)(y + 8 + 16), 256f, (float)(256 - this.z_max)), GUIManager.tex_red);
		}
		if (this.z_min > 0)
		{
			GUI.DrawTexture(new Rect((float)(x + 8), (float)(y + 8 + 16 + (256 - this.z_min)), 256f, (float)this.z_min), GUIManager.tex_red);
		}
	}

	// Token: 0x060003AB RID: 939 RVA: 0x00047EAC File Offset: 0x000460AC
	private void DrawMapSide(int x, int y)
	{
		GUI.DrawTexture(new Rect((float)(x + 8), (float)(y + 8), 256f, 64f), this.radar.RadarSideTexture);
		float num = GUI.VerticalSlider(new Rect((float)(x + 256 + 4 + 8), (float)(y + 8), 32f, 64f), (float)this.y_max, 63f, 0f);
		if (this.y_max != (int)num)
		{
			this.y_max = (int)num;
		}
		if (this.y_max < 63)
		{
			GUI.DrawTexture(new Rect((float)(x + 8), (float)(y + 8), 256f, (float)(64 - this.y_max)), GUIManager.tex_red);
		}
	}

	// Token: 0x060003AC RID: 940 RVA: 0x00047F58 File Offset: 0x00046158
	private void DrawSize(int x, int y)
	{
		GUIManager.DrawText(new Rect((float)x, (float)y, 80f, 20f), Lang.GetLabel(131) + this.x_min.ToString() + " - " + this.x_max.ToString(), 16, 3, 8);
		GUIManager.DrawText(new Rect((float)x, (float)(y + 20), 80f, 20f), Lang.GetLabel(132) + this.y_min.ToString() + " - " + this.y_max.ToString(), 16, 3, 8);
		GUIManager.DrawText(new Rect((float)x, (float)(y + 40), 80f, 20f), Lang.GetLabel(133) + this.z_min.ToString() + " - " + this.z_max.ToString(), 16, 3, 8);
	}

	// Token: 0x040007C8 RID: 1992
	private Radar radar;

	// Token: 0x040007C9 RID: 1993
	private Map map;

	// Token: 0x040007CA RID: 1994
	private Client cscl;

	// Token: 0x040007CB RID: 1995
	private int x_min;

	// Token: 0x040007CC RID: 1996
	private int x_max = 255;

	// Token: 0x040007CD RID: 1997
	private int y_min;

	// Token: 0x040007CE RID: 1998
	private int y_max = 63;

	// Token: 0x040007CF RID: 1999
	private int z_min;

	// Token: 0x040007D0 RID: 2000
	private int z_max = 255;

	// Token: 0x040007D1 RID: 2001
	private bool show;

	// Token: 0x040007D2 RID: 2002
	private Rect r_window;
}
