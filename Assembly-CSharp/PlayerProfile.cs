using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AC RID: 172
public class PlayerProfile
{
	// Token: 0x04000B1D RID: 2845
	public static string PlayerName = "Player";

	// Token: 0x04000B1E RID: 2846
	public static string id = "0";

	// Token: 0x04000B1F RID: 2847
	public static string authkey = "nokey";

	// Token: 0x04000B20 RID: 2848
	public static string session = "nosession";

	// Token: 0x04000B21 RID: 2849
	public static string gameSession = "nosession";

	// Token: 0x04000B22 RID: 2850
	public static int country = 0;

	// Token: 0x04000B23 RID: 2851
	public static string countrySTR = "";

	// Token: 0x04000B24 RID: 2852
	public static int admin = 0;

	// Token: 0x04000B25 RID: 2853
	public static byte loh = 0;

	// Token: 0x04000B26 RID: 2854
	public static bool NY2017Q = false;

	// Token: 0x04000B27 RID: 2855
	public static bool NY2018Q = false;

	// Token: 0x04000B28 RID: 2856
	public static bool VD2017Q = false;

	// Token: 0x04000B29 RID: 2857
	public static int currentMission = 0;

	// Token: 0x04000B2A RID: 2858
	public static string screenShotURL;

	// Token: 0x04000B2B RID: 2859
	public static string myInventory = "";

	// Token: 0x04000B2C RID: 2860
	public static bool load_player = false;

	// Token: 0x04000B2D RID: 2861
	public static bool get_bonus_day = false;

	// Token: 0x04000B2E RID: 2862
	public static bool get_player_stats = false;

	// Token: 0x04000B2F RID: 2863
	public static int money = 0;

	// Token: 0x04000B30 RID: 2864
	public static int moneypay = 0;

	// Token: 0x04000B31 RID: 2865
	public static int premium = 0;

	// Token: 0x04000B32 RID: 2866
	public static int exp = 0;

	// Token: 0x04000B33 RID: 2867
	public static int tykva = 0;

	// Token: 0x04000B34 RID: 2868
	public static int kolpak = 0;

	// Token: 0x04000B35 RID: 2869
	public static int roga = 0;

	// Token: 0x04000B36 RID: 2870
	public static int mask_bear = 0;

	// Token: 0x04000B37 RID: 2871
	public static int mask_fox = 0;

	// Token: 0x04000B38 RID: 2872
	public static int mask_rabbit = 0;

	// Token: 0x04000B39 RID: 2873
	public static int level = 0;

	// Token: 0x04000B3A RID: 2874
	public static int skin = 0;

	// Token: 0x04000B3B RID: 2875
	public static NETWORK network = NETWORK.ST;

	// Token: 0x04000B3C RID: 2876
	public static string friends = "";

	// Token: 0x04000B3D RID: 2877
	public static string friendServers = "";

	// Token: 0x04000B3E RID: 2878
	public static Dictionary<string, string> friendsOnline = new Dictionary<string, string>();

	// Token: 0x04000B3F RID: 2879
	public static string[] friendsOnlineServers;

	// Token: 0x04000B40 RID: 2880
	public static string[] friendsRating;

	// Token: 0x04000B41 RID: 2881
	public static int myindex = 0;

	// Token: 0x04000B42 RID: 2882
	public static int myteam = -1;

	// Token: 0x04000B43 RID: 2883
	public static Dictionary<int, Texture2D> crossList = new Dictionary<int, Texture2D>();

	// Token: 0x04000B44 RID: 2884
	public static Dictionary<int, Texture2D> crossDot = new Dictionary<int, Texture2D>();

	// Token: 0x04000B45 RID: 2885
	public static Color crossColor;
}
