using System;
using UnityEngine;

// Token: 0x0200007D RID: 125
public class GUIManager
{
	// Token: 0x06000374 RID: 884 RVA: 0x000413EB File Offset: 0x0003F5EB
	public static float XRES(float x)
	{
		return x * ((float)Screen.width / 1024f) + 0.5f;
	}

	// Token: 0x06000375 RID: 885 RVA: 0x00041401 File Offset: 0x0003F601
	public static float YRES(float y)
	{
		return y * ((float)Screen.height / 768f) + 0.5f;
	}

	// Token: 0x06000376 RID: 886 RVA: 0x00041417 File Offset: 0x0003F617
	public static float ASPECTRATIO()
	{
		return (float)(Screen.width / Screen.height);
	}

	// Token: 0x06000377 RID: 887 RVA: 0x00041428 File Offset: 0x0003F628
	public static void Init(bool _only_localize = false)
	{
		GUIManager.logo = ContentLoader.LoadTexture("logo" + Lang.current.ToString());
		GUIManager.prazd_logo = ContentLoader.LoadTexture("winter_logo_eng" + Lang.current.ToString());
		GUIManager.tex_sound_off = ContentLoader.LoadTexture("sound_off" + Lang.current.ToString());
		GUIManager.tex_sound_on = ContentLoader.LoadTexture("sound_on" + Lang.current.ToString());
		GUIManager.tex_exit = ContentLoader.LoadTexture("exit" + Lang.current.ToString());
		GUIManager.tex_help = ContentLoader.LoadTexture("help" + Lang.current.ToString());
		GUIManager.tex_fullscreen = ContentLoader.LoadTexture("fullscreen" + Lang.current.ToString());
		GUIManager.tex_menu_start = ContentLoader.LoadTexture("menu_start" + Lang.current.ToString());
		GUIManager.tex_menu_option = ContentLoader.LoadTexture("menu_options" + Lang.current.ToString());
		GUIManager.tex_menu_profile = ContentLoader.LoadTexture("menu_profle" + Lang.current.ToString());
		GUIManager.tex_menu_shop = ContentLoader.LoadTexture("menu_shop" + Lang.current.ToString());
		GUIManager.tex_menu_playerstats = ContentLoader.LoadTexture("menu_stats" + Lang.current.ToString());
		GUIManager.tex_menu_inv = ContentLoader.LoadTexture("menu_inv" + Lang.current.ToString());
		GUIManager.tex_menu_clan = ContentLoader.LoadTexture("menu_clan" + Lang.current.ToString());
		GUIManager.tex_menu_master = ContentLoader.LoadTexture("menu_master" + Lang.current.ToString());
		GUIManager.tex_icon_premium = ContentLoader.LoadTexture("premium_icon" + Lang.current.ToString());
		GUIManager.tex_loading = ContentLoader.LoadTexture("loading" + Lang.current.ToString());
		if (DateTime.UtcNow >= CONST.EXT.TIME_START && DateTime.UtcNow < CONST.EXT.TIME_END)
		{
			GUIManager.tex_top_plus = ContentLoader.LoadTexture("action_coin_menu_plus" + Lang.current.ToString());
			GUIManager.tex_top_plus2 = ContentLoader.LoadTexture("action_coin_menu_plus2" + Lang.current.ToString());
		}
		else
		{
			GUIManager.tex_top_plus = ContentLoader.LoadTexture("coin_menu_plus" + Lang.current.ToString());
			GUIManager.tex_top_plus2 = ContentLoader.LoadTexture("coin_menu_plus2" + Lang.current.ToString());
		}
		GUIManager.tex_menu_zadanie = ContentLoader.LoadTexture("menu_zadanie" + Lang.current.ToString());
		GUIManager.tex_item_select = ContentLoader.LoadTexture("select" + Lang.current.ToString());
		GUIManager.tex_item_open = ContentLoader.LoadTexture("open" + Lang.current.ToString());
		GUIManager.tex_premium_big = ContentLoader.LoadTexture("newprem" + Lang.current.ToString());
		GUIManager.tex_megapack = ContentLoader.LoadTexture("megapack" + Lang.current.ToString());
		GUIManager.tex_clan_manage = ContentLoader.LoadTexture("manage" + Lang.current.ToString());
		GUIManager.tex_clan_delete = ContentLoader.LoadTexture("delete" + Lang.current.ToString());
		GUIManager.tex_clan_accept = ContentLoader.LoadTexture("accept" + Lang.current.ToString());
		GUIManager.tex_clan_decline = ContentLoader.LoadTexture("decline" + Lang.current.ToString());
		GUIManager.tex_clan_invite = ContentLoader.LoadTexture("invite" + Lang.current.ToString());
		GUIManager.tex_upgrade[1] = ContentLoader.LoadTexture("button_damage" + Lang.current.ToString());
		GUIManager.tex_upgrade[2] = ContentLoader.LoadTexture("button_clip" + Lang.current.ToString());
		GUIManager.tex_upgrade[3] = ContentLoader.LoadTexture("button_backpack" + Lang.current.ToString());
		GUIManager.tex_upgrade[4] = ContentLoader.LoadTexture("button_zombie" + Lang.current.ToString());
		GUIManager.tex_upgrade_vehicle[1] = ContentLoader.LoadTexture("button_life" + Lang.current.ToString());
		GUIManager.tex_upgrade_vehicle[2] = ContentLoader.LoadTexture("button_armor" + Lang.current.ToString());
		GUIManager.tex_upgrade_vehicle[3] = ContentLoader.LoadTexture("button_speed" + Lang.current.ToString());
		GUIManager.tex_upgrade_vehicle[4] = ContentLoader.LoadTexture("button_tank_reloadspeed" + Lang.current.ToString());
		GUIManager.tex_upgrade_vehicle[5] = ContentLoader.LoadTexture("button_turret" + Lang.current.ToString());
		GUIManager.tex_upgrade_active[1] = ContentLoader.LoadTexture("button_damage_buy" + Lang.current.ToString());
		GUIManager.tex_upgrade_active[2] = ContentLoader.LoadTexture("button_clip_buy" + Lang.current.ToString());
		GUIManager.tex_upgrade_active[3] = ContentLoader.LoadTexture("button_backpack_buy" + Lang.current.ToString());
		GUIManager.tex_upgrade_active[4] = ContentLoader.LoadTexture("button_zombie_buy" + Lang.current.ToString());
		GUIManager.tex_upgrade_vehicle_active[1] = ContentLoader.LoadTexture("button_tank_health_buy" + Lang.current.ToString());
		GUIManager.tex_upgrade_vehicle_active[2] = ContentLoader.LoadTexture("button_tank_shield_buy" + Lang.current.ToString());
		GUIManager.tex_upgrade_vehicle_active[3] = ContentLoader.LoadTexture("button_tank_speed_buy" + Lang.current.ToString());
		GUIManager.tex_upgrade_vehicle_active[4] = ContentLoader.LoadTexture("button_tank_reloadspeed_buy" + Lang.current.ToString());
		GUIManager.tex_upgrade_vehicle_active[5] = ContentLoader.LoadTexture("button_tank_aimspeed_buy" + Lang.current.ToString());
		GUIManager.tex_bonus = ContentLoader.LoadTexture("bonus" + Lang.current);
		GUIManager.tex_discount = ContentLoader.LoadTexture("discount" + Lang.current);
		if (!_only_localize)
		{
			GUIManager.tex_button = ContentLoader.LoadTexture("ingame_disconnect");
			GUIManager.tex_button_hover = ContentLoader.LoadTexture("ingame");
			GUIManager.goPlayer = GameObject.Find("Player");
			GUIManager.goMainCamera = GameObject.Find("Main Camera");
			GUIManager.gs_empty = new GUIStyle();
			GUIManager.gs_style1 = new GUIStyle();
			GUIManager.gs_style2 = new GUIStyle();
			GUIManager.gs_style3 = new GUIStyle();
			GUIManager.gs_style1.font = FontManager.font[2];
			GUIManager.gs_style1.fontSize = 16;
			GUIManager.gs_style1.normal.textColor = new Color(1f, 1f, 1f, 1f);
			GUIManager.gs_style1.alignment = 4;
			GUIManager.gs_style1.richText = false;
			GUIManager.gs_style2.font = FontManager.font[3];
			GUIManager.gs_style2.fontSize = 14;
			GUIManager.gs_style2.normal.textColor = new Color(1f, 1f, 1f, 1f);
			GUIManager.gs_style2.alignment = 3;
			GUIManager.gs_style2.richText = false;
			GUIManager.gs_style3.font = FontManager.font[0];
			GUIManager.gs_style3.fontSize = 16;
			GUIManager.gs_style3.normal.textColor = new Color(1f, 1f, 1f, 1f);
			GUIManager.gs_style3.alignment = 4;
			GUIManager.gs_style3.richText = false;
			GUIManager.c[0] = new Color(0f, 0f, 1f, 1f);
			GUIManager.c[1] = new Color(1f, 0f, 0f, 1f);
			GUIManager.c[2] = new Color(0f, 1f, 0f, 1f);
			GUIManager.c[3] = new Color(1f, 1f, 0f, 1f);
			GUIManager.c[4] = new Color(1f, 1f, 1f, 1f);
			GUIManager.c[5] = new Color(1f, 1f, 1f, 1f);
			GUIManager.c[6] = new Color(1f, 1f, 1f, 1f);
			GUIManager.c[7] = new Color(1f, 1f, 1f, 1f);
			GUIManager.c[8] = new Color(1f, 1f, 1f, 1f);
			GUIManager.c[9] = new Color(0f, 0f, 0f, 1f);
			GUIManager.tex_black = new Texture2D(1, 1);
			GUIManager.tex_black.SetPixel(0, 0, new Color(0f, 0f, 0f, 1f));
			GUIManager.tex_black.Apply();
			GUIManager.tex_half_black = new Texture2D(1, 1);
			GUIManager.tex_half_black.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.5f));
			GUIManager.tex_half_black.Apply();
			GUIManager.tex_blue = new Texture2D(1, 1);
			GUIManager.tex_blue.SetPixel(0, 0, new Color(0f, 0f, 1f, 1f));
			GUIManager.tex_blue.Apply();
			GUIManager.tex_green = new Texture2D(1, 1);
			GUIManager.tex_green.SetPixel(0, 0, new Color(0f, 1f, 0f, 1f));
			GUIManager.tex_green.Apply();
			GUIManager.tex_red = new Texture2D(1, 1);
			GUIManager.tex_red.SetPixel(0, 0, new Color(1f, 0f, 0f, 1f));
			GUIManager.tex_red.Apply();
			GUIManager.tex_yellow = new Texture2D(1, 1);
			GUIManager.tex_yellow.SetPixel(0, 0, new Color(1f, 1f, 0f, 1f));
			GUIManager.tex_yellow.Apply();
			GUIManager.tex_half_yellow = new Texture2D(1, 1);
			GUIManager.tex_half_yellow.SetPixel(0, 0, new Color(1f, 1f, 0f, 0.5f));
			GUIManager.tex_half_yellow.Apply();
			GUIManager.tex_menu_social = ContentLoader.LoadTexture("menu_social");
			GUIManager.tex_face_icon = ContentLoader.LoadTexture("face_icon");
			GUIManager.tex_edit_icon = ContentLoader.LoadTexture("edit");
			GUIManager.tex_save_icon = ContentLoader.LoadTexture("save");
			GUIManager.tex_icon_load = ContentLoader.LoadTexture("icon_load");
			GUIManager.tex_menu_back = ContentLoader.LoadTexture("menu_back");
			GUIManager.tex_button256 = ContentLoader.LoadTexture("button256");
			GUIManager.tex_button128 = ContentLoader.LoadTexture("button128");
			GUIManager.tex_button96 = ContentLoader.LoadTexture("button96");
			GUIManager.tex_button22 = ContentLoader.LoadTexture("button22");
			GUIManager.tex_edit96 = ContentLoader.LoadTexture("edit96");
			GUIManager.tex_button_blue = ContentLoader.LoadTexture("button_blue");
			GUIManager.tex_button_green = ContentLoader.LoadTexture("button_green");
			GUIManager.tex_bar_blue = ContentLoader.LoadTexture("barb");
			GUIManager.tex_bar_yellow = ContentLoader.LoadTexture("bary");
			GUIManager.tex_panel = ContentLoader.LoadTexture("menu_panel");
			GUIManager.tex_select = ContentLoader.LoadTexture("menu_select");
			GUIManager.tex_hover = ContentLoader.LoadTexture("menu_hover");
			GUIManager.tex_server = ContentLoader.LoadTexture("server2");
			GUIManager.tex_server_hover = ContentLoader.LoadTexture("server_hover");
			GUIManager.tex_hint = ContentLoader.LoadTexture("hint");
			GUIManager.gm0 = ContentLoader.LoadTexture("gm0");
			GUIManager.gm1 = ContentLoader.LoadTexture("gm1");
			GUIManager.gm2 = ContentLoader.LoadTexture("gm2");
			GUIManager.gm3 = ContentLoader.LoadTexture("gm3");
			GUIManager.gm4 = ContentLoader.LoadTexture("gm4");
			GUIManager.gm5 = ContentLoader.LoadTexture("gm5");
			GUIManager.gm6 = ContentLoader.LoadTexture("gm6");
			GUIManager.gm7 = ContentLoader.LoadTexture("gm7");
			GUIManager.gm8 = ContentLoader.LoadTexture("gm8");
			GUIManager.gm9 = ContentLoader.LoadTexture("gm9");
			GUIManager.gm10 = ContentLoader.LoadTexture("gm10");
			GUIManager.gm11 = ContentLoader.LoadTexture("gm11");
			GUIManager.pro = ContentLoader.LoadTexture("pro" + Lang.current.ToString());
			GUIManager.select_glow = ContentLoader.LoadTexture("select_glow");
			GUIManager.hover_glow = ContentLoader.LoadTexture("hover_glow");
			GUIManager.hover_part_glow = ContentLoader.LoadTexture("select_glow_part");
			GUIManager.tex_fb = ContentLoader.LoadTexture("net_fb");
			GUIManager.tex_kg = ContentLoader.LoadTexture("net_kg");
			GUIManager.tex_mm = ContentLoader.LoadTexture("net_mm");
			GUIManager.tex_nl = ContentLoader.LoadTexture("net_nl");
			GUIManager.tex_ok = ContentLoader.LoadTexture("net_ok");
			GUIManager.tex_st = ContentLoader.LoadTexture("net_st");
			GUIManager.tex_vk = ContentLoader.LoadTexture("net_vk");
			GUIManager.tex_flags_filter[0] = ContentLoader.LoadTexture("RUSSIA");
			GUIManager.tex_flags_filter[1] = ContentLoader.LoadTexture("EUROPE");
			GUIManager.tex_flags_filter[2] = ContentLoader.LoadTexture("USA");
			GUIManager.tex_flags[0] = ContentLoader.LoadTexture("flag_xx");
			GUIManager.tex_flags[1] = ContentLoader.LoadTexture("flag__ru");
			GUIManager.tex_flags[2] = ContentLoader.LoadTexture("flag_europeanunion");
			GUIManager.tex_flags[3] = ContentLoader.LoadTexture("flag__us");
			GUIManager.tex_button_mode = ContentLoader.LoadTexture("button_mode");
			GUIManager.tex_menubar = ContentLoader.LoadTexture("menubar");
			GUIManager.tex_warning = ContentLoader.LoadTexture("warningbar");
			GUIManager.tex_warning2 = ContentLoader.LoadTexture("warningbar2");
			GUIManager.tex_arrow = ContentLoader.LoadTexture("arrow");
			GUIManager.tex_coin = ContentLoader.LoadTexture("coin");
			GUIManager.tex_back_discount = ContentLoader.LoadTexture("discount_30");
			GUIManager.tex_bars = ContentLoader.LoadTexture("bars");
			GUIManager.tex_category = ContentLoader.LoadTexture("bar_category");
			GUIManager.tex_prazd_ZM = ContentLoader.LoadTexture("bar_category");
			GUIManager.tex_prazd_NY = ContentLoader.LoadTexture("bar_categoryNY");
			GUIManager.tex_prazd_HL = ContentLoader.LoadTexture("bar_categoryHL");
			GUIManager.tex_prazd_WWII = ContentLoader.LoadTexture("bar_categoryWWII");
			GUIManager.tex_prazd_LADY = ContentLoader.LoadTexture("bar_category");
			GUIManager.tex_buy_active = ContentLoader.LoadTexture("buy_active");
			GUIManager.tex_buy_blocked = ContentLoader.LoadTexture("buy_blocked");
			GUIManager.tex_weaponback = ContentLoader.LoadTexture("weaponback");
			GUIManager.tex_item_back = ContentLoader.LoadTexture("itemback");
			GUIManager.tex_item_back_ZM = ContentLoader.LoadTexture("itemback");
			GUIManager.tex_item_back_HL = ContentLoader.LoadTexture("itembackHL");
			GUIManager.tex_item_back_WWII = ContentLoader.LoadTexture("itembackWWII");
			GUIManager.tex_item_back_LADY = ContentLoader.LoadTexture("itemback");
			GUIManager.tex_item_back_NY = ContentLoader.LoadTexture("itembackNY");
			GUIManager.tex_item_back_discount = ContentLoader.LoadTexture("itemback_30");
			GUIManager.tex_item_back_new = ContentLoader.LoadTexture("itembackNEW");
			GUIManager.tex_item_back_lvl = ContentLoader.LoadTexture("itembackLVL");
			GUIManager.tex_playerback = ContentLoader.LoadTexture("playerback");
			GUIManager.tex_clock = ContentLoader.LoadTexture("clock");
			GUIManager.tex_atlas1 = ContentLoader.LoadTexture("atlas1");
			GUIManager.tex_atlas2 = ContentLoader.LoadTexture("atlas2");
			GUIManager.tex_atlas3 = ContentLoader.LoadTexture("atlas3");
			GUIManager.tex_atlas4 = ContentLoader.LoadTexture("atlas4");
			GUIManager.tex_atlas5 = ContentLoader.LoadTexture("atlas5");
			GUIManager.tex_proceed = ContentLoader.LoadTexture("proceedbar");
			GUIManager.tex_crossPrevBackground = ContentLoader.LoadTexture("background");
			GUIManager.tex_crossPalette = ContentLoader.LoadTexture("palette");
			GUIManager.NY2017REWARD = ContentLoader.LoadTexture("NY2017REWARD");
			GUIManager.VD2017REWARD = ContentLoader.LoadTexture("VD2017REWARD");
			GUIManager.NY2018REWARD = ContentLoader.LoadTexture("NY2018REWARD");
			GUIManager.tex_clan_exit = ContentLoader.LoadTexture("exit");
			GUIManager.tex_clan_find = ContentLoader.LoadTexture("buy_valid");
			GUIManager.tex_upgrade_bars = ContentLoader.LoadTexture("upgrade_bar");
			GUIManager.bar[0] = ContentLoader.LoadTexture("bar_top");
			GUIManager.bar[1] = ContentLoader.LoadTexture("bar_middle");
			GUIManager.bar[2] = ContentLoader.LoadTexture("bar_bottom");
			GUIManager.bar[3] = ContentLoader.LoadTexture("slider_normal");
			GUIManager.bar[4] = ContentLoader.LoadTexture("slider_active");
			GUIManager.bar[5] = ContentLoader.LoadTexture("slider_back");
			GUIManager.bar[6] = ContentLoader.LoadTexture("toggle_normal");
			GUIManager.bar[7] = ContentLoader.LoadTexture("toggle_active");
			GUIManager.tex_social = ContentLoader.LoadTexture("button_social");
			GUIManager.tex_crossline = ContentLoader.LoadTexture("crossline");
			GUIManager.tex_soundbar = ContentLoader.LoadTexture("soundbar");
			GUIManager.tex_sensbar = ContentLoader.LoadTexture("sensbar");
			GUIManager.loadingRoll = new TweenButton(GUIGS.NULL, new Vector2(0f, 0f), OFFSET_TYPE.FROM_CENTER, ContentLoader.LoadTexture("Loading"), null, null, null, null, null, null, 0f, 20f, 50, 64f, 64f, 0f);
			GUIManager.loadingRoll.tt.Add(TWEEN_TYPE.ROTATE);
			GUIManager.IsReady = true;
		}
	}

	// Token: 0x06000378 RID: 888 RVA: 0x000425A9 File Offset: 0x000407A9
	public static void DrawLoading()
	{
		GUIManager.loadingRoll.DrawButton(Vector2.zero);
	}

	// Token: 0x06000379 RID: 889 RVA: 0x000425BC File Offset: 0x000407BC
	public static bool DrawButton(Rect r, Vector2 mpos, Color c, string label)
	{
		Texture2D texture2D = null;
		if (r.width == 256f)
		{
			texture2D = GUIManager.tex_button256;
		}
		else if (r.width == 128f)
		{
			texture2D = GUIManager.tex_button128;
		}
		else if (r.width == 96f)
		{
			texture2D = GUIManager.tex_button96;
		}
		else if (r.width == 22f || r.width == 20f)
		{
			texture2D = GUIManager.tex_button22;
		}
		Color color;
		color..ctor(1f, 1f, 1f, 1f);
		bool result = false;
		bool flag = false;
		if (r.Contains(mpos))
		{
			flag = true;
		}
		if (!flag)
		{
			c *= 0.8f;
			color *= 0.8f;
		}
		GUI.color = c;
		GUI.DrawTexture(r, texture2D);
		if (GUI.Button(r, "", GUIManager.gs_empty))
		{
			result = true;
		}
		GUI.color = new Color(0f, 0f, 0f, 1f);
		Rect rect = r;
		float num = rect.x;
		rect.x = num + 1f;
		num = rect.y;
		rect.y = num + 1f;
		GUI.Label(rect, label, GUIManager.gs_style1);
		GUI.color = color;
		GUI.Label(r, label, GUIManager.gs_style1);
		GUI.color = new Color(1f, 1f, 1f, 1f);
		return result;
	}

	// Token: 0x0600037A RID: 890 RVA: 0x00042724 File Offset: 0x00040924
	public static string DrawEdit(Rect r, Vector2 mpos, Color c, string label, int limit)
	{
		Texture2D texture2D = null;
		if (r.width == 96f)
		{
			texture2D = GUIManager.tex_edit96;
		}
		GUI.DrawTexture(r, texture2D);
		return GUI.TextField(r, label, limit, GUIManager.gs_style1);
	}

	// Token: 0x0600037B RID: 891 RVA: 0x0004275C File Offset: 0x0004095C
	public static float DrawColorText(float x, float y, string text, TextAnchor align)
	{
		if (text == null)
		{
			return 0f;
		}
		if (text == "")
		{
			return 0f;
		}
		GUIManager.gs_style2.alignment = align;
		string[] array = text.Split(new char[]
		{
			'^'
		});
		float num = 0f;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != null && !(array[i] == ""))
			{
				int num2;
				if (int.TryParse(array[i][0].ToString(), out num2))
				{
					if (num2 < 0 || num2 > 9)
					{
						num2 = 8;
					}
					array[i] = array[i].Substring(1, array[i].Length - 1);
				}
				else
				{
					num2 = 8;
				}
				Vector2 vector = GUIManager.gs_style2.CalcSize(new GUIContent(array[i]));
				GUIManager.gs_style2.normal.textColor = GUIManager.c[9];
				GUI.Label(new Rect(x + 1f, y + 1f, 256f, 20f), array[i], GUIManager.gs_style2);
				GUIManager.gs_style2.normal.textColor = GUIManager.c[num2];
				GUI.Label(new Rect(x, y, 256f, 20f), array[i], GUIManager.gs_style2);
				x += vector.x;
				num += vector.x;
			}
		}
		return num;
	}

	// Token: 0x0600037C RID: 892 RVA: 0x000428C0 File Offset: 0x00040AC0
	public static void DrawText(Rect r, string text, int size, TextAnchor align, int color = 8)
	{
		TextAnchor alignment = GUIManager.gs_style3.alignment;
		int fontSize = GUIManager.gs_style3.fontSize;
		GUIManager.gs_style3.alignment = align;
		GUIManager.gs_style3.fontSize = size;
		GUIManager.gs_style3.normal.textColor = GUIManager.c[9];
		GUI.Label(new Rect(r.x + 1f, r.y + 1f, r.width, r.height), text, GUIManager.gs_style3);
		GUIManager.gs_style3.normal.textColor = GUIManager.c[color];
		GUI.Label(r, text, GUIManager.gs_style3);
		GUIManager.gs_style3.alignment = alignment;
		GUIManager.gs_style3.fontSize = fontSize;
	}

	// Token: 0x0600037D RID: 893 RVA: 0x0004298C File Offset: 0x00040B8C
	public static void DrawText2(Rect r, string text, int size, TextAnchor align, Color _c)
	{
		TextAnchor alignment = GUIManager.gs_style2.alignment;
		int fontSize = GUIManager.gs_style2.fontSize;
		GUIManager.gs_style2.alignment = align;
		GUIManager.gs_style2.fontSize = size;
		GUIManager.gs_style2.normal.textColor = GUIManager.c[9];
		GUI.Label(new Rect(r.x + 1f, r.y + 1f, r.width, r.height), text, GUIManager.gs_style2);
		GUIManager.gs_style2.normal.textColor = _c;
		GUI.Label(r, text, GUIManager.gs_style2);
		GUIManager.gs_style2.alignment = alignment;
		GUIManager.gs_style2.fontSize = fontSize;
	}

	// Token: 0x0600037E RID: 894 RVA: 0x00042A4C File Offset: 0x00040C4C
	public static bool DrawButton2(Vector2 pos, Vector2 mpos, string label, int type)
	{
		Texture2D texture2D = null;
		if (type == 0)
		{
			texture2D = GUIManager.tex_button_blue;
		}
		else if (type == 1)
		{
			texture2D = GUIManager.tex_button_green;
		}
		bool result = false;
		Rect rect = new Rect(pos.x, pos.y, 192f, 32f);
		GUI.DrawTexture(rect, texture2D);
		if (GUI.Button(rect, "", GUIManager.gs_empty))
		{
			result = true;
		}
		GUIManager.DrawText(rect, label, 22, 4, 8);
		return result;
	}

	// Token: 0x0600037F RID: 895 RVA: 0x00042AB4 File Offset: 0x00040CB4
	public static void HSlider()
	{
		GUI.skin.horizontalSliderThumb.normal.background = GUIManager.bar[3];
		GUI.skin.horizontalSliderThumb.hover.background = GUIManager.bar[4];
		GUI.skin.horizontalSliderThumb.active.background = GUIManager.bar[4];
		GUI.skin.horizontalSlider.normal.background = GUIManager.bar[5];
		GUI.skin.horizontalSlider.fixedHeight = 12f;
	}

	// Token: 0x06000380 RID: 896 RVA: 0x00042B44 File Offset: 0x00040D44
	public static void Toggle()
	{
		GUI.skin.toggle.normal.background = GUIManager.bar[6];
		GUI.skin.toggle.onNormal.background = GUIManager.bar[7];
		GUI.skin.toggle.hover.background = GUIManager.bar[6];
		GUI.skin.toggle.onHover.background = GUIManager.bar[7];
		GUI.skin.toggle.active.background = GUIManager.bar[7];
		GUI.skin.toggle.onActive.background = GUIManager.bar[7];
	}

	// Token: 0x06000381 RID: 897 RVA: 0x00042BF4 File Offset: 0x00040DF4
	public static Vector2 BeginScrollView(Rect viewzone, Vector2 scrollViewVector, Rect scrollzone)
	{
		GUI.skin.verticalScrollbar.normal.background = null;
		GUI.skin.verticalScrollbarThumb.normal.background = null;
		scrollViewVector = GUI.BeginScrollView(viewzone, scrollViewVector, scrollzone);
		float num = viewzone.height / scrollzone.height * viewzone.height;
		float num2 = scrollViewVector.y / scrollzone.height * viewzone.height;
		if (scrollzone.height <= viewzone.height)
		{
			GUIManager.rbar.height = 0f;
		}
		else
		{
			GUIManager.rbar = new Rect(viewzone.x + viewzone.width - 16f, viewzone.y + num2, 16f, num);
		}
		return scrollViewVector;
	}

	// Token: 0x06000382 RID: 898 RVA: 0x00042CB4 File Offset: 0x00040EB4
	public static void EndScrollView()
	{
		GUI.EndScrollView();
		GUIManager.DrawBar(GUIManager.rbar);
	}

	// Token: 0x06000383 RID: 899 RVA: 0x00042CC8 File Offset: 0x00040EC8
	public static void DrawBar(Rect r)
	{
		if (r.height == 0f)
		{
			return;
		}
		GUI.DrawTexture(new Rect(r.x, r.y, r.width, 4f), GUIManager.bar[0]);
		if (r.height - 8f > 0f)
		{
			GUI.DrawTexture(new Rect(r.x, r.y + 4f, r.width, r.height - 8f), GUIManager.bar[1]);
		}
		GUI.DrawTexture(new Rect(r.x, r.y + r.height - 4f, r.width, 4f), GUIManager.bar[2]);
	}

	// Token: 0x040006A6 RID: 1702
	public static bool IsReady = false;

	// Token: 0x040006A7 RID: 1703
	public static Texture2D tex_black;

	// Token: 0x040006A8 RID: 1704
	public static Texture2D tex_half_black;

	// Token: 0x040006A9 RID: 1705
	public static Texture2D tex_red;

	// Token: 0x040006AA RID: 1706
	public static Texture2D tex_green;

	// Token: 0x040006AB RID: 1707
	public static Texture2D tex_blue;

	// Token: 0x040006AC RID: 1708
	public static Texture2D tex_yellow;

	// Token: 0x040006AD RID: 1709
	public static Texture2D tex_half_yellow;

	// Token: 0x040006AE RID: 1710
	public static Texture2D tex_button256;

	// Token: 0x040006AF RID: 1711
	public static Texture2D tex_button128;

	// Token: 0x040006B0 RID: 1712
	public static Texture2D tex_button96;

	// Token: 0x040006B1 RID: 1713
	public static Texture2D tex_button22;

	// Token: 0x040006B2 RID: 1714
	public static Texture2D tex_edit96;

	// Token: 0x040006B3 RID: 1715
	public static Texture2D tex_bar_blue;

	// Token: 0x040006B4 RID: 1716
	public static Texture2D tex_bar_yellow;

	// Token: 0x040006B5 RID: 1717
	public static Texture2D tex_face_icon;

	// Token: 0x040006B6 RID: 1718
	public static Texture2D tex_edit_icon;

	// Token: 0x040006B7 RID: 1719
	public static Texture2D tex_save_icon;

	// Token: 0x040006B8 RID: 1720
	public static Texture2D tex_icon_load;

	// Token: 0x040006B9 RID: 1721
	public static Texture2D tex_icon_premium;

	// Token: 0x040006BA RID: 1722
	public static Texture2D tex_menu_back;

	// Token: 0x040006BB RID: 1723
	public static Texture2D logo;

	// Token: 0x040006BC RID: 1724
	public static Texture2D prazd_logo;

	// Token: 0x040006BD RID: 1725
	public static Texture2D under_logo;

	// Token: 0x040006BE RID: 1726
	public static Texture2D tex_sound_off;

	// Token: 0x040006BF RID: 1727
	public static Texture2D tex_sound_on;

	// Token: 0x040006C0 RID: 1728
	public static Texture2D tex_exit;

	// Token: 0x040006C1 RID: 1729
	public static Texture2D tex_help;

	// Token: 0x040006C2 RID: 1730
	public static Texture2D tex_fullscreen;

	// Token: 0x040006C3 RID: 1731
	public static Texture2D tex_menu_start;

	// Token: 0x040006C4 RID: 1732
	public static Texture2D tex_menu_option;

	// Token: 0x040006C5 RID: 1733
	public static Texture2D tex_menu_profile;

	// Token: 0x040006C6 RID: 1734
	public static Texture2D tex_menu_shop;

	// Token: 0x040006C7 RID: 1735
	public static Texture2D tex_menu_playerstats;

	// Token: 0x040006C8 RID: 1736
	public static Texture2D tex_menu_inv;

	// Token: 0x040006C9 RID: 1737
	public static Texture2D tex_menu_clan;

	// Token: 0x040006CA RID: 1738
	public static Texture2D tex_menu_master;

	// Token: 0x040006CB RID: 1739
	public static Texture2D tex_button;

	// Token: 0x040006CC RID: 1740
	public static Texture2D tex_button_hover;

	// Token: 0x040006CD RID: 1741
	public static Texture2D tex_loading;

	// Token: 0x040006CE RID: 1742
	public static Texture2D tex_menu_social;

	// Token: 0x040006CF RID: 1743
	public static Texture2D tex_menu_zadanie;

	// Token: 0x040006D0 RID: 1744
	public static Texture2D tex_top_plus;

	// Token: 0x040006D1 RID: 1745
	public static Texture2D tex_top_plus2;

	// Token: 0x040006D2 RID: 1746
	public static Texture2D tex_panel;

	// Token: 0x040006D3 RID: 1747
	public static Texture2D tex_select;

	// Token: 0x040006D4 RID: 1748
	public static Texture2D tex_hover;

	// Token: 0x040006D5 RID: 1749
	public static Texture2D tex_server;

	// Token: 0x040006D6 RID: 1750
	public static Texture2D tex_server_hover;

	// Token: 0x040006D7 RID: 1751
	public static Texture2D tex_hint;

	// Token: 0x040006D8 RID: 1752
	public static Texture2D gm0;

	// Token: 0x040006D9 RID: 1753
	public static Texture2D gm1;

	// Token: 0x040006DA RID: 1754
	public static Texture2D gm2;

	// Token: 0x040006DB RID: 1755
	public static Texture2D gm3;

	// Token: 0x040006DC RID: 1756
	public static Texture2D gm4;

	// Token: 0x040006DD RID: 1757
	public static Texture2D gm5;

	// Token: 0x040006DE RID: 1758
	public static Texture2D gm6;

	// Token: 0x040006DF RID: 1759
	public static Texture2D gm7;

	// Token: 0x040006E0 RID: 1760
	public static Texture2D gm8;

	// Token: 0x040006E1 RID: 1761
	public static Texture2D gm9;

	// Token: 0x040006E2 RID: 1762
	public static Texture2D gm10;

	// Token: 0x040006E3 RID: 1763
	public static Texture2D gm11;

	// Token: 0x040006E4 RID: 1764
	public static Texture2D tex_warning;

	// Token: 0x040006E5 RID: 1765
	public static Texture2D tex_warning2;

	// Token: 0x040006E6 RID: 1766
	public static Texture2D pro;

	// Token: 0x040006E7 RID: 1767
	public static Texture2D select_glow;

	// Token: 0x040006E8 RID: 1768
	public static Texture2D hover_glow;

	// Token: 0x040006E9 RID: 1769
	public static Texture2D hover_part_glow;

	// Token: 0x040006EA RID: 1770
	public static Texture2D[] tex_flags = new Texture2D[245];

	// Token: 0x040006EB RID: 1771
	public static Texture2D[] tex_flags_filter = new Texture2D[3];

	// Token: 0x040006EC RID: 1772
	public static Texture2D tex_button_mode;

	// Token: 0x040006ED RID: 1773
	public static Texture2D tex_menubar;

	// Token: 0x040006EE RID: 1774
	public static Texture2D tex_arrow;

	// Token: 0x040006EF RID: 1775
	public static Texture2D tex_coin;

	// Token: 0x040006F0 RID: 1776
	public static Texture2D tex_premium_big;

	// Token: 0x040006F1 RID: 1777
	public static Texture2D tex_category;

	// Token: 0x040006F2 RID: 1778
	public static Texture2D tex_prazd_ZM;

	// Token: 0x040006F3 RID: 1779
	public static Texture2D tex_prazd_NY;

	// Token: 0x040006F4 RID: 1780
	public static Texture2D tex_prazd_HL;

	// Token: 0x040006F5 RID: 1781
	public static Texture2D tex_prazd_WWII;

	// Token: 0x040006F6 RID: 1782
	public static Texture2D tex_prazd_LADY;

	// Token: 0x040006F7 RID: 1783
	public static Texture2D tex_buy_active;

	// Token: 0x040006F8 RID: 1784
	public static Texture2D tex_buy_blocked;

	// Token: 0x040006F9 RID: 1785
	public static Texture2D tex_back_discount;

	// Token: 0x040006FA RID: 1786
	public static Texture2D tex_megapack;

	// Token: 0x040006FB RID: 1787
	public static Texture2D tex_weaponback;

	// Token: 0x040006FC RID: 1788
	public static Texture2D tex_bars;

	// Token: 0x040006FD RID: 1789
	public static Texture2D tex_item_back;

	// Token: 0x040006FE RID: 1790
	public static Texture2D tex_item_back_ZM;

	// Token: 0x040006FF RID: 1791
	public static Texture2D tex_item_back_NY;

	// Token: 0x04000700 RID: 1792
	public static Texture2D tex_item_back_HL;

	// Token: 0x04000701 RID: 1793
	public static Texture2D tex_item_back_WWII;

	// Token: 0x04000702 RID: 1794
	public static Texture2D tex_item_back_LADY;

	// Token: 0x04000703 RID: 1795
	public static Texture2D tex_item_back_discount;

	// Token: 0x04000704 RID: 1796
	public static Texture2D tex_item_back_new;

	// Token: 0x04000705 RID: 1797
	public static Texture2D tex_item_back_lvl;

	// Token: 0x04000706 RID: 1798
	public static Texture2D tex_item_select;

	// Token: 0x04000707 RID: 1799
	public static Texture2D tex_item_open;

	// Token: 0x04000708 RID: 1800
	public static Texture2D tex_playerback;

	// Token: 0x04000709 RID: 1801
	public static Texture2D tex_clock;

	// Token: 0x0400070A RID: 1802
	public static Texture2D tex_atlas1;

	// Token: 0x0400070B RID: 1803
	public static Texture2D tex_atlas2;

	// Token: 0x0400070C RID: 1804
	public static Texture2D tex_atlas3;

	// Token: 0x0400070D RID: 1805
	public static Texture2D tex_atlas4;

	// Token: 0x0400070E RID: 1806
	public static Texture2D tex_atlas5;

	// Token: 0x0400070F RID: 1807
	public static Texture2D tex_proceed;

	// Token: 0x04000710 RID: 1808
	public static Texture2D tex_crossPrevBackground;

	// Token: 0x04000711 RID: 1809
	public static Texture2D tex_crossPalette;

	// Token: 0x04000712 RID: 1810
	public static Texture2D NY2017REWARD;

	// Token: 0x04000713 RID: 1811
	public static Texture2D VD2017REWARD;

	// Token: 0x04000714 RID: 1812
	public static Texture2D NY2018REWARD;

	// Token: 0x04000715 RID: 1813
	public static Texture2D tex_clan_manage;

	// Token: 0x04000716 RID: 1814
	public static Texture2D tex_clan_exit;

	// Token: 0x04000717 RID: 1815
	public static Texture2D tex_clan_accept;

	// Token: 0x04000718 RID: 1816
	public static Texture2D tex_clan_decline;

	// Token: 0x04000719 RID: 1817
	public static Texture2D tex_clan_delete;

	// Token: 0x0400071A RID: 1818
	public static Texture2D tex_clan_invite;

	// Token: 0x0400071B RID: 1819
	public static Texture2D tex_clan_find;

	// Token: 0x0400071C RID: 1820
	public static Texture2D[] tex_upgrade = new Texture2D[5];

	// Token: 0x0400071D RID: 1821
	public static Texture2D[] tex_upgrade_active = new Texture2D[5];

	// Token: 0x0400071E RID: 1822
	public static Texture2D[] tex_upgrade_vehicle = new Texture2D[6];

	// Token: 0x0400071F RID: 1823
	public static Texture2D[] tex_upgrade_vehicle_active = new Texture2D[6];

	// Token: 0x04000720 RID: 1824
	public static Texture2D tex_upgrade_bars;

	// Token: 0x04000721 RID: 1825
	public static Texture2D tex_social;

	// Token: 0x04000722 RID: 1826
	public static Texture2D tex_bonus;

	// Token: 0x04000723 RID: 1827
	public static Texture2D tex_discount;

	// Token: 0x04000724 RID: 1828
	public static Texture2D tex_crossline;

	// Token: 0x04000725 RID: 1829
	public static Texture2D tex_soundbar;

	// Token: 0x04000726 RID: 1830
	public static Texture2D tex_sensbar;

	// Token: 0x04000727 RID: 1831
	public static Texture2D tex_fb;

	// Token: 0x04000728 RID: 1832
	public static Texture2D tex_kg;

	// Token: 0x04000729 RID: 1833
	public static Texture2D tex_mm;

	// Token: 0x0400072A RID: 1834
	public static Texture2D tex_nl;

	// Token: 0x0400072B RID: 1835
	public static Texture2D tex_ok;

	// Token: 0x0400072C RID: 1836
	public static Texture2D tex_st;

	// Token: 0x0400072D RID: 1837
	public static Texture2D tex_vk;

	// Token: 0x0400072E RID: 1838
	public static GUIStyle gs_empty;

	// Token: 0x0400072F RID: 1839
	public static GUIStyle gs_style1;

	// Token: 0x04000730 RID: 1840
	public static GUIStyle gs_style2;

	// Token: 0x04000731 RID: 1841
	public static GUIStyle gs_style3;

	// Token: 0x04000732 RID: 1842
	public static Color[] c = new Color[10];

	// Token: 0x04000733 RID: 1843
	public static Texture2D tex_button_blue;

	// Token: 0x04000734 RID: 1844
	public static Texture2D tex_button_green;

	// Token: 0x04000735 RID: 1845
	private static Texture2D[] bar = new Texture2D[10];

	// Token: 0x04000736 RID: 1846
	private static Rect rbar;

	// Token: 0x04000737 RID: 1847
	private static GameObject goPlayer;

	// Token: 0x04000738 RID: 1848
	private static GameObject goMainCamera;

	// Token: 0x04000739 RID: 1849
	private static float lastwidth = 0f;

	// Token: 0x0400073A RID: 1850
	private static TweenButton loadingRoll;
}
