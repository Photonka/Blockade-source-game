using System;
using UnityEngine;

// Token: 0x020000A1 RID: 161
public class Config
{
	// Token: 0x06000577 RID: 1399 RVA: 0x00069100 File Offset: 0x00067300
	public static void Init()
	{
		Application.runInBackground = true;
		if (PlayerPrefs.HasKey(CONST.MD5("Sensitivity")))
		{
			Config.Sensitivity = PlayerPrefs.GetFloat(CONST.MD5("Sensitivity"));
		}
		else
		{
			Config.Sensitivity = 3f;
		}
		if (PlayerPrefs.HasKey(CONST.MD5("Tileset")))
		{
			Config.Tileset = PlayerPrefs.GetInt(CONST.MD5("Tileset"));
		}
		else
		{
			Config.Tileset = 0;
		}
		if (PlayerPrefs.HasKey(CONST.MD5("Dlight")))
		{
			Config.Dlight = PlayerPrefs.GetInt(CONST.MD5("Dlight"));
		}
		else
		{
			Config.Dlight = 0;
		}
		if (PlayerPrefs.HasKey(CONST.MD5("Res")))
		{
			Config.respos = PlayerPrefs.GetInt(CONST.MD5("Res"));
		}
		else
		{
			Config.respos = 0;
		}
		if (PlayerPrefs.HasKey(CONST.MD5("MenuVolume")))
		{
			Config.menuvolume = PlayerPrefs.GetFloat(CONST.MD5("MenuVolume"));
		}
		else
		{
			Config.menuvolume = 1f;
		}
		if (PlayerPrefs.HasKey(CONST.MD5("GameVolume")))
		{
			Config.gamevolume = PlayerPrefs.GetFloat(CONST.MD5("GameVolume"));
		}
		else
		{
			Config.gamevolume = 1f;
		}
		if (PlayerPrefs.HasKey(CONST.MD5("Dist")))
		{
			Config.distpos = PlayerPrefs.GetInt(CONST.MD5("Dist"));
		}
		else
		{
			Config.distpos = 2;
		}
		if (PlayerPrefs.HasKey(CONST.MD5("CrosshairDot")))
		{
			Config.dot = PlayerPrefs.GetInt(CONST.MD5("CrosshairDot"));
		}
		else
		{
			Config.dot = 0;
		}
		if (PlayerPrefs.HasKey(CONST.MD5("CrosshairID")))
		{
			Config.cross = PlayerPrefs.GetInt(CONST.MD5("CrosshairID"));
		}
		else
		{
			Config.cross = 0;
		}
		if (PlayerPrefs.HasKey(CONST.MD5("CrosshairR")))
		{
			Config.crossR = PlayerPrefs.GetFloat(CONST.MD5("CrosshairR"));
		}
		else
		{
			Config.crossR = 1f;
		}
		if (PlayerPrefs.HasKey(CONST.MD5("CrosshairG")))
		{
			Config.crossG = PlayerPrefs.GetFloat(CONST.MD5("CrosshairG"));
		}
		else
		{
			Config.crossG = 1f;
		}
		if (PlayerPrefs.HasKey(CONST.MD5("CrosshairB")))
		{
			Config.crossB = PlayerPrefs.GetFloat(CONST.MD5("CrosshairB"));
		}
		else
		{
			Config.crossB = 1f;
		}
		if (PlayerPrefs.HasKey(CONST.MD5("Fscreen")))
		{
			Config.Fscreen = PlayerPrefs.GetInt(CONST.MD5("Fscreen"));
		}
		else
		{
			Config.Fscreen = 0;
		}
		bool flag = Config.Fscreen > 0;
		if (Screen.fullScreen != flag)
		{
			Config.ChangeMode();
		}
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x0006938A File Offset: 0x0006758A
	public static void SaveChatFilter()
	{
		if (Config.chat_filter)
		{
			PlayerPrefs.SetInt("ChatFilter", 1);
			PlayerPrefs.Save();
			return;
		}
		PlayerPrefs.SetInt("ChatFilter", 0);
		PlayerPrefs.Save();
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x000693B4 File Offset: 0x000675B4
	public static void ChangeMode()
	{
		bool flag = !Screen.fullScreen;
		if (flag)
		{
			int num = Config.respos;
			if (num < 0)
			{
				Screen.fullScreen = !Screen.fullScreen;
			}
			else
			{
				int num2 = Screen.resolutions[num].width;
				int num3 = Screen.resolutions[num].height;
				if (num2 < 1024)
				{
					num2 = 1024;
					num3 = 768;
				}
				Screen.SetResolution(num2, num3, true);
			}
		}
		else
		{
			Screen.fullScreen = !Screen.fullScreen;
		}
		if (flag)
		{
			PlayerPrefs.SetInt(CONST.MD5("Fscreen"), 1);
			Config.Fscreen = 1;
			return;
		}
		PlayerPrefs.SetInt(CONST.MD5("Fscreen"), 0);
		Config.Fscreen = 0;
	}

	// Token: 0x040009A9 RID: 2473
	public static float Sensitivity = 3f;

	// Token: 0x040009AA RID: 2474
	public static int Dlight = 0;

	// Token: 0x040009AB RID: 2475
	public static int Fscreen = 0;

	// Token: 0x040009AC RID: 2476
	public static int Tileset = 0;

	// Token: 0x040009AD RID: 2477
	public static int respos;

	// Token: 0x040009AE RID: 2478
	public static int distpos;

	// Token: 0x040009AF RID: 2479
	public static float menuvolume;

	// Token: 0x040009B0 RID: 2480
	public static float gamevolume;

	// Token: 0x040009B1 RID: 2481
	public static bool chat_filter;

	// Token: 0x040009B2 RID: 2482
	public static int dot = 0;

	// Token: 0x040009B3 RID: 2483
	public static int cross = 0;

	// Token: 0x040009B4 RID: 2484
	public static float crossR = 1f;

	// Token: 0x040009B5 RID: 2485
	public static float crossG = 1f;

	// Token: 0x040009B6 RID: 2486
	public static float crossB = 1f;
}
