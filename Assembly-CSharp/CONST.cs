using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

// Token: 0x020000AB RID: 171
public class CONST
{
	// Token: 0x0600057E RID: 1406 RVA: 0x000694C6 File Offset: 0x000676C6
	public static string GET_CONTENT_URL()
	{
		return "file://" + Directory.GetCurrentDirectory() + "\\AssetBundles\\";
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x000694DC File Offset: 0x000676DC
	public static string MD5(string strToEncrypt)
	{
		byte[] bytes = new UTF8Encoding().GetBytes(strToEncrypt);
		byte[] array = new MD5CryptoServiceProvider().ComputeHash(bytes);
		string text = "";
		for (int i = 0; i < array.Length; i++)
		{
			text += Convert.ToString(array[i], 16).PadLeft(2, '0');
		}
		return text.PadLeft(32, '0');
	}

	// Token: 0x04000B19 RID: 2841
	public static SteamHandler STEAM_HANDLER;

	// Token: 0x04000B1A RID: 2842
	public static string HANDLER_SERVER = "https://blockade3d.com/api_classic/handler.php?NETWORK=" + (int)PlayerProfile.network + "&CMD=";

	// Token: 0x04000B1B RID: 2843
	public static string HANDLER_SERVER_LIST = "http://31.131.253.109/api_classic/handler.php?NETWORK=" + (int)PlayerProfile.network + "&CMD=";

	// Token: 0x04000B1C RID: 2844
	public const string SIMPLE = "a891a7d64f3f57354b6d93a89aac29ae";

	// Token: 0x0200087B RID: 2171
	public class VEHICLES
	{
		// Token: 0x04003235 RID: 12853
		public static int NONE = 0;

		// Token: 0x04003236 RID: 12854
		public static int TANKS = 1;

		// Token: 0x04003237 RID: 12855
		public static int JEEP = 2;

		// Token: 0x04003238 RID: 12856
		public static int POSITION_NONE = 200;

		// Token: 0x04003239 RID: 12857
		public static int POSITION_JEEP_DRIVER = 0;

		// Token: 0x0400323A RID: 12858
		public static int POSITION_JEEP_GUNNER = 1;

		// Token: 0x0400323B RID: 12859
		public static int POSITION_JEEP_PASS = 2;

		// Token: 0x0400323C RID: 12860
		public static int VEHICLE_TANK_LIGHT = 200;

		// Token: 0x0400323D RID: 12861
		public static int VEHICLE_TANK_MEDIUM = 201;

		// Token: 0x0400323E RID: 12862
		public static int VEHICLE_TANK_HEAVY = 202;

		// Token: 0x0400323F RID: 12863
		public static int VEHICLE_JEEP = 203;

		// Token: 0x04003240 RID: 12864
		public static int VEHICLE_MODUL_TANK_MG = 220;

		// Token: 0x04003241 RID: 12865
		public static int VEHICLE_MODUL_REPAIR_KIT = 221;

		// Token: 0x04003242 RID: 12866
		public static int VEHICLE_MODUL_ANTI_MISSLE = 222;

		// Token: 0x04003243 RID: 12867
		public static int VEHICLE_MODUL_SMOKE = 223;
	}

	// Token: 0x0200087C RID: 2172
	public class ENTS
	{
		// Token: 0x04003244 RID: 12868
		public static int MAX_ENTS = 512;

		// Token: 0x04003245 RID: 12869
		public static int ENT_GRENADE = 1;

		// Token: 0x04003246 RID: 12870
		public static int ENT_SHMEL = 2;

		// Token: 0x04003247 RID: 12871
		public static int ENT_ZOMBIE = 3;

		// Token: 0x04003248 RID: 12872
		public static int ENT_GP = 4;

		// Token: 0x04003249 RID: 12873
		public static int ENT_BOAT = 5;

		// Token: 0x0400324A RID: 12874
		public static int ENT_SHTURM_MINEN = 6;

		// Token: 0x0400324B RID: 12875
		public static int ENT_TURRETS = 7;

		// Token: 0x0400324C RID: 12876
		public static int ENT_TNT_PLACE = 8;

		// Token: 0x0400324D RID: 12877
		public static int ENT_FENCE = 9;

		// Token: 0x0400324E RID: 12878
		public static int ENT_ZOMBIE2 = 10;

		// Token: 0x0400324F RID: 12879
		public static int ENT_ZOMBIE_BOSS = 11;

		// Token: 0x04003250 RID: 12880
		public static int ENT_EJ = 12;

		// Token: 0x04003251 RID: 12881
		public static int ENT_TANK = 13;

		// Token: 0x04003252 RID: 12882
		public static int ENT_TANK_SNARYAD = 14;

		// Token: 0x04003253 RID: 12883
		public static int ENT_RPG = 15;

		// Token: 0x04003254 RID: 12884
		public static int ENT_TANK_LIGHT = 16;

		// Token: 0x04003255 RID: 12885
		public static int ENT_TANK_MEDIUM = 17;

		// Token: 0x04003256 RID: 12886
		public static int ENT_TANK_HEAVY = 18;

		// Token: 0x04003257 RID: 12887
		public static int ENT_ZBK18M = 19;

		// Token: 0x04003258 RID: 12888
		public static int ENT_ZOF26 = 20;

		// Token: 0x04003259 RID: 12889
		public static int ENT_MINEFLY = 21;

		// Token: 0x0400325A RID: 12890
		public static int ENT_JAVELIN = 22;

		// Token: 0x0400325B RID: 12891
		public static int ENT_ARROW = 23;

		// Token: 0x0400325C RID: 12892
		public static int ENT_SMOKE_GRENADE = 24;

		// Token: 0x0400325D RID: 12893
		public static int ENT_HE_GRENADE = 25;

		// Token: 0x0400325E RID: 12894
		public static int ENT_RKG3 = 26;

		// Token: 0x0400325F RID: 12895
		public static int ENT_MINE = 27;

		// Token: 0x04003260 RID: 12896
		public static int ENT_C4 = 28;

		// Token: 0x04003261 RID: 12897
		public static int ENT_JEEP = 29;

		// Token: 0x04003262 RID: 12898
		public static int ENT_ANTI_MISSLE = 30;

		// Token: 0x04003263 RID: 12899
		public static int ENT_SMOKE = 31;

		// Token: 0x04003264 RID: 12900
		public static int ENT_AT_MINE = 32;

		// Token: 0x04003265 RID: 12901
		public static int ENT_MOLOTOV = 33;

		// Token: 0x04003266 RID: 12902
		public static int ENT_M202 = 34;

		// Token: 0x04003267 RID: 12903
		public static int ENT_GAZ_GRENADE = 35;

		// Token: 0x04003268 RID: 12904
		public static int ENT_SNOWBALL = 36;

		// Token: 0x04003269 RID: 12905
		public static int ENT_GHOST = 37;

		// Token: 0x0400326A RID: 12906
		public static int ENT_GHOST_BOSS = 38;
	}

	// Token: 0x0200087D RID: 2173
	public class SKINS
	{
	}

	// Token: 0x0200087E RID: 2174
	public class TEAMS
	{
		// Token: 0x0400326B RID: 12907
		public static int TEAM_BLUE = 0;

		// Token: 0x0400326C RID: 12908
		public static int TEAM_RED = 1;

		// Token: 0x0400326D RID: 12909
		public static int TEAM_GREEN = 2;

		// Token: 0x0400326E RID: 12910
		public static int TEAM_YELLOW = 3;
	}

	// Token: 0x0200087F RID: 2175
	public class CFG
	{
		// Token: 0x0400326F RID: 12911
		public static global::Version VERSION = global::Version.RELEASE;

		// Token: 0x04003270 RID: 12912
		public static int SERVER_UPDATE_TIMEOUT = 15;

		// Token: 0x04003271 RID: 12913
		public static int BATTLE_MODE = 0;

		// Token: 0x04003272 RID: 12914
		public static int SNOWBALLS_MODE = 1;

		// Token: 0x04003273 RID: 12915
		public static int BUILD_MODE = 2;

		// Token: 0x04003274 RID: 12916
		public static int ZOMBIE_MODE = 3;

		// Token: 0x04003275 RID: 12917
		public static int CAPTURE_MODE = 4;

		// Token: 0x04003276 RID: 12918
		public static int CONTRA_MODE = 5;

		// Token: 0x04003277 RID: 12919
		public static int MELEE_MODE = 6;

		// Token: 0x04003278 RID: 12920
		public static int SURVIVAL_MODE = 7;

		// Token: 0x04003279 RID: 12921
		public static int CLASSIC_MODE = 8;

		// Token: 0x0400327A RID: 12922
		public static int PRORIV_MODE = 9;

		// Token: 0x0400327B RID: 12923
		public static int CLEAR_MODE = 10;

		// Token: 0x0400327C RID: 12924
		public static int TANK_MODE = 11;

		// Token: 0x0400327D RID: 12925
		public static string[] SOCIAL_POSTFIX = new string[]
		{
			"_vk",
			"_ok",
			"_fb",
			"_mm",
			"_kg",
			"_steam"
		};

		// Token: 0x0400327E RID: 12926
		public static int[] GAME_PORTS_OFFSET = new int[]
		{
			40000,
			50000,
			50000,
			30000,
			1050,
			50000
		};
	}

	// Token: 0x02000880 RID: 2176
	public class EXT
	{
		// Token: 0x0400327F RID: 12927
		public static DateTime TIME_START = new DateTime(2018, 10, 31, 0, 0, 0);

		// Token: 0x04003280 RID: 12928
		public static DateTime TIME_END = new DateTime(2018, 11, 10, 0, 0, 0);
	}
}
