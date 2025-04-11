using System;
using UnityEngine;

// Token: 0x0200008C RID: 140
public class GUI3
{
	// Token: 0x0600040E RID: 1038 RVA: 0x00051AB4 File Offset: 0x0004FCB4
	public static void Init()
	{
		GUI3.bar[0] = (Resources.Load("NewMenu/E_Menu_Slider_Top") as Texture2D);
		GUI3.bar[1] = (Resources.Load("NewMenu/E_Menu_Slider_Middle") as Texture2D);
		GUI3.bar[2] = (Resources.Load("NewMenu/E_Menu_Slider_Bottom") as Texture2D);
		GUI3.blackTex = GUI3.GetTexture2D(Color.black, 100f);
		GUI3.whiteTex = GUI3.GetTexture2D(Color.white, 100f);
	}

	// Token: 0x0600040F RID: 1039 RVA: 0x00051B2C File Offset: 0x0004FD2C
	public static void HSlider()
	{
		GUI.skin.horizontalSliderThumb.normal.background = GUI3.bar[3];
		GUI.skin.horizontalSliderThumb.hover.background = GUI3.bar[4];
		GUI.skin.horizontalSliderThumb.active.background = GUI3.bar[4];
		GUI.skin.horizontalSlider.normal.background = GUI3.bar[5];
		GUI.skin.horizontalSlider.fixedHeight = 12f;
	}

	// Token: 0x06000410 RID: 1040 RVA: 0x00051BBC File Offset: 0x0004FDBC
	public static void Toggle()
	{
		GUI.skin.toggle.normal.background = GUI3.bar[6];
		GUI.skin.toggle.onNormal.background = GUI3.bar[7];
		GUI.skin.toggle.hover.background = GUI3.bar[6];
		GUI.skin.toggle.onHover.background = GUI3.bar[7];
		GUI.skin.toggle.active.background = GUI3.bar[7];
		GUI.skin.toggle.onActive.background = GUI3.bar[7];
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x00051C6C File Offset: 0x0004FE6C
	public static Vector2 BeginScrollView(Rect viewzone, Vector2 scrollViewVector, Rect scrollzone, bool debug = false)
	{
		GUI.skin.verticalScrollbar.normal.background = null;
		GUI.skin.verticalScrollbarThumb.normal.background = null;
		GUI.skin.horizontalScrollbar.normal.background = null;
		GUI.skin.horizontalScrollbarThumb.normal.background = null;
		scrollViewVector = GUI.BeginScrollView(viewzone, scrollViewVector, scrollzone);
		float num = viewzone.height / scrollzone.height * viewzone.height;
		float num2 = scrollViewVector.y / scrollzone.height * viewzone.height;
		if (debug)
		{
			Debug.Log(string.Concat(new object[]
			{
				num,
				" ",
				viewzone.height,
				" ",
				scrollzone.height,
				" y=",
				scrollViewVector.y,
				" "
			}));
		}
		if (num - 28f < 0f)
		{
			num = 28f;
		}
		if (scrollzone.height <= viewzone.height)
		{
			num = 0f;
		}
		GUI3.rbar = new Rect(viewzone.x + viewzone.width - 15f, viewzone.y + num2, 14f, num);
		return scrollViewVector;
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x00051DC7 File Offset: 0x0004FFC7
	public static void EndScrollView()
	{
		GUI.EndScrollView();
		GUI3.DrawBar(GUI3.rbar);
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x00051DD8 File Offset: 0x0004FFD8
	public static void DrawBar(Rect r)
	{
		if (r.height == 0f)
		{
			return;
		}
		GUI.DrawTexture(new Rect(r.x, r.y, r.width, 14f), GUI3.bar[0]);
		if (r.height - 28f > 0f)
		{
			GUI.DrawTexture(new Rect(r.x, r.y + 14f, r.width, r.height - 28f), GUI3.bar[1]);
		}
		GUI.DrawTexture(new Rect(r.x, r.y + r.height - 14f, r.width, 14f), GUI3.bar[2]);
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x00051EA8 File Offset: 0x000500A8
	public static Texture2D GetTexture2D(Color _color, float _alpha)
	{
		Texture2D texture2D = new Texture2D(1, 1, 5, false);
		texture2D.SetPixel(0, 0, new Color(_color.r, _color.g, _color.b, _alpha / 100f));
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x04000873 RID: 2163
	private static Texture2D[] bar = new Texture2D[10];

	// Token: 0x04000874 RID: 2164
	public static Texture2D blackTex;

	// Token: 0x04000875 RID: 2165
	public static Texture2D whiteTex;

	// Token: 0x04000876 RID: 2166
	private static Rect rbar;
}
