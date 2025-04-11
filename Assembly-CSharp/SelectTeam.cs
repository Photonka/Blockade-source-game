using System;
using UnityEngine;

// Token: 0x02000088 RID: 136
public class SelectTeam : MonoBehaviour
{
	// Token: 0x060003D2 RID: 978 RVA: 0x0004B1DC File Offset: 0x000493DC
	public void OpenMenu()
	{
		if (PlayerControl.GetGameMode() == 7 || PlayerControl.GetGameMode() == 10)
		{
			return;
		}
		this.SortPlayers();
		this.show = true;
		this.showtime = Time.time;
		this.show_teamselect = true;
		if (this.csws == null)
		{
			this.csws = (WeaponSystem)Object.FindObjectOfType(typeof(WeaponSystem));
		}
		MainGUI.ForceCursor = true;
		this.csws.HideWeapons(true);
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x0004B254 File Offset: 0x00049454
	public void CloseMenu()
	{
		this.show = false;
		MainGUI.ForceCursor = this.show;
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x0004B268 File Offset: 0x00049468
	private void Awake()
	{
		this.Map = GameObject.Find("Map");
		this.gui_style = new GUIStyle();
		this.gui_style.font = FontManager.font[2];
		this.gui_style.fontSize = 16;
		this.gui_style.normal.textColor = new Color(255f, 255f, 255f, 255f);
		this.tex_tabmenu = (Resources.Load("GUI/tabmenu") as Texture2D);
		this.tex_tabmenu2 = (Resources.Load("GUI/tabmenu2") as Texture2D);
		this.tex_tabmenu3 = (Resources.Load("GUI/tabmenuz") as Texture2D);
		this.tex_tabmenu2team = (Resources.Load("GUI/tabmenucs") as Texture2D);
		if (Lang.current == 1)
		{
			this.tex_class_mp5 = (Resources.Load("GUI/eng/assault") as Texture2D);
			this.tex_class_m3 = (Resources.Load("GUI/eng/support") as Texture2D);
			this.tex_class_m14 = (Resources.Load("GUI/eng/sniper") as Texture2D);
		}
		else
		{
			this.tex_class_mp5 = (Resources.Load("GUI/rus/assault") as Texture2D);
			this.tex_class_m3 = (Resources.Load("GUI/rus/support") as Texture2D);
			this.tex_class_m14 = (Resources.Load("GUI/rus/sniper") as Texture2D);
		}
		this.tex_flags[0] = (Resources.Load("GUI/flags/xx") as Texture2D);
		this.tex_flags[1] = (Resources.Load("GUI/flags/__ru") as Texture2D);
		this.tex_flags[2] = (Resources.Load("GUI/flags/__ua") as Texture2D);
		this.tex_flags[3] = (Resources.Load("GUI/flags/__by") as Texture2D);
		this.tex_flags[4] = (Resources.Load("GUI/flags/__kz") as Texture2D);
		this.tex_flags[5] = (Resources.Load("GUI/flags/__md") as Texture2D);
		this.tex_flags[6] = (Resources.Load("GUI/flags/__ee") as Texture2D);
		this.tex_flags[7] = (Resources.Load("GUI/flags/__lv") as Texture2D);
		this.tex_flags[8] = (Resources.Load("GUI/flags/__de") as Texture2D);
		this.tex_flags[9] = (Resources.Load("GUI/flags/__am") as Texture2D);
		this.tex_flags[10] = (Resources.Load("GUI/flags/__us") as Texture2D);
		this.tex_flags[11] = (Resources.Load("GUI/flags/ad") as Texture2D);
		this.tex_flags[12] = (Resources.Load("GUI/flags/ae") as Texture2D);
		this.tex_flags[13] = (Resources.Load("GUI/flags/af") as Texture2D);
		this.tex_flags[14] = (Resources.Load("GUI/flags/ag") as Texture2D);
		this.tex_flags[15] = (Resources.Load("GUI/flags/ai") as Texture2D);
		this.tex_flags[16] = (Resources.Load("GUI/flags/al") as Texture2D);
		this.tex_flags[17] = (Resources.Load("GUI/flags/an") as Texture2D);
		this.tex_flags[18] = (Resources.Load("GUI/flags/ao") as Texture2D);
		this.tex_flags[19] = (Resources.Load("GUI/flags/ar") as Texture2D);
		this.tex_flags[20] = (Resources.Load("GUI/flags/as") as Texture2D);
		this.tex_flags[21] = (Resources.Load("GUI/flags/at") as Texture2D);
		this.tex_flags[22] = (Resources.Load("GUI/flags/au") as Texture2D);
		this.tex_flags[23] = (Resources.Load("GUI/flags/aw") as Texture2D);
		this.tex_flags[24] = (Resources.Load("GUI/flags/ax") as Texture2D);
		this.tex_flags[25] = (Resources.Load("GUI/flags/az") as Texture2D);
		this.tex_flags[26] = (Resources.Load("GUI/flags/ba") as Texture2D);
		this.tex_flags[27] = (Resources.Load("GUI/flags/bb") as Texture2D);
		this.tex_flags[28] = (Resources.Load("GUI/flags/bd") as Texture2D);
		this.tex_flags[29] = (Resources.Load("GUI/flags/be") as Texture2D);
		this.tex_flags[30] = (Resources.Load("GUI/flags/bf") as Texture2D);
		this.tex_flags[31] = (Resources.Load("GUI/flags/bg") as Texture2D);
		this.tex_flags[32] = (Resources.Load("GUI/flags/bh") as Texture2D);
		this.tex_flags[33] = (Resources.Load("GUI/flags/bi") as Texture2D);
		this.tex_flags[34] = (Resources.Load("GUI/flags/bj") as Texture2D);
		this.tex_flags[35] = (Resources.Load("GUI/flags/bm") as Texture2D);
		this.tex_flags[36] = (Resources.Load("GUI/flags/bn") as Texture2D);
		this.tex_flags[37] = (Resources.Load("GUI/flags/bo") as Texture2D);
		this.tex_flags[38] = (Resources.Load("GUI/flags/br") as Texture2D);
		this.tex_flags[39] = (Resources.Load("GUI/flags/bs") as Texture2D);
		this.tex_flags[40] = (Resources.Load("GUI/flags/bt") as Texture2D);
		this.tex_flags[41] = (Resources.Load("GUI/flags/bv") as Texture2D);
		this.tex_flags[42] = (Resources.Load("GUI/flags/bw") as Texture2D);
		this.tex_flags[43] = (Resources.Load("GUI/flags/bz") as Texture2D);
		this.tex_flags[44] = (Resources.Load("GUI/flags/ca") as Texture2D);
		this.tex_flags[45] = (Resources.Load("GUI/flags/cc") as Texture2D);
		this.tex_flags[46] = (Resources.Load("GUI/flags/cd") as Texture2D);
		this.tex_flags[47] = (Resources.Load("GUI/flags/cf") as Texture2D);
		this.tex_flags[48] = (Resources.Load("GUI/flags/cg") as Texture2D);
		this.tex_flags[49] = (Resources.Load("GUI/flags/ch") as Texture2D);
		this.tex_flags[50] = (Resources.Load("GUI/flags/ci") as Texture2D);
		this.tex_flags[51] = (Resources.Load("GUI/flags/ck") as Texture2D);
		this.tex_flags[52] = (Resources.Load("GUI/flags/cl") as Texture2D);
		this.tex_flags[53] = (Resources.Load("GUI/flags/cm") as Texture2D);
		this.tex_flags[54] = (Resources.Load("GUI/flags/cn") as Texture2D);
		this.tex_flags[55] = (Resources.Load("GUI/flags/co") as Texture2D);
		this.tex_flags[56] = (Resources.Load("GUI/flags/cr") as Texture2D);
		this.tex_flags[57] = (Resources.Load("GUI/flags/cs") as Texture2D);
		this.tex_flags[58] = (Resources.Load("GUI/flags/cu") as Texture2D);
		this.tex_flags[59] = (Resources.Load("GUI/flags/cv") as Texture2D);
		this.tex_flags[60] = (Resources.Load("GUI/flags/cx") as Texture2D);
		this.tex_flags[61] = (Resources.Load("GUI/flags/cy") as Texture2D);
		this.tex_flags[62] = (Resources.Load("GUI/flags/cz") as Texture2D);
		this.tex_flags[63] = (Resources.Load("GUI/flags/dj") as Texture2D);
		this.tex_flags[64] = (Resources.Load("GUI/flags/dk") as Texture2D);
		this.tex_flags[65] = (Resources.Load("GUI/flags/dm") as Texture2D);
		this.tex_flags[66] = (Resources.Load("GUI/flags/do") as Texture2D);
		this.tex_flags[67] = (Resources.Load("GUI/flags/dz") as Texture2D);
		this.tex_flags[68] = (Resources.Load("GUI/flags/ec") as Texture2D);
		this.tex_flags[69] = (Resources.Load("GUI/flags/eg") as Texture2D);
		this.tex_flags[70] = (Resources.Load("GUI/flags/eh") as Texture2D);
		this.tex_flags[71] = (Resources.Load("GUI/flags/er") as Texture2D);
		this.tex_flags[72] = (Resources.Load("GUI/flags/es") as Texture2D);
		this.tex_flags[73] = (Resources.Load("GUI/flags/et") as Texture2D);
		this.tex_flags[74] = (Resources.Load("GUI/flags/fi") as Texture2D);
		this.tex_flags[75] = (Resources.Load("GUI/flags/fj") as Texture2D);
		this.tex_flags[76] = (Resources.Load("GUI/flags/fk") as Texture2D);
		this.tex_flags[77] = (Resources.Load("GUI/flags/fm") as Texture2D);
		this.tex_flags[78] = (Resources.Load("GUI/flags/fo") as Texture2D);
		this.tex_flags[79] = (Resources.Load("GUI/flags/fr") as Texture2D);
		this.tex_flags[80] = (Resources.Load("GUI/flags/ga") as Texture2D);
		this.tex_flags[81] = (Resources.Load("GUI/flags/gb") as Texture2D);
		this.tex_flags[82] = (Resources.Load("GUI/flags/gd") as Texture2D);
		this.tex_flags[83] = (Resources.Load("GUI/flags/ge") as Texture2D);
		this.tex_flags[84] = (Resources.Load("GUI/flags/gf") as Texture2D);
		this.tex_flags[85] = (Resources.Load("GUI/flags/gh") as Texture2D);
		this.tex_flags[86] = (Resources.Load("GUI/flags/gi") as Texture2D);
		this.tex_flags[87] = (Resources.Load("GUI/flags/gl") as Texture2D);
		this.tex_flags[88] = (Resources.Load("GUI/flags/gm") as Texture2D);
		this.tex_flags[89] = (Resources.Load("GUI/flags/gn") as Texture2D);
		this.tex_flags[90] = (Resources.Load("GUI/flags/gp") as Texture2D);
		this.tex_flags[91] = (Resources.Load("GUI/flags/gq") as Texture2D);
		this.tex_flags[92] = (Resources.Load("GUI/flags/gr") as Texture2D);
		this.tex_flags[93] = (Resources.Load("GUI/flags/gs") as Texture2D);
		this.tex_flags[94] = (Resources.Load("GUI/flags/gt") as Texture2D);
		this.tex_flags[95] = (Resources.Load("GUI/flags/gu") as Texture2D);
		this.tex_flags[96] = (Resources.Load("GUI/flags/gw") as Texture2D);
		this.tex_flags[97] = (Resources.Load("GUI/flags/gy") as Texture2D);
		this.tex_flags[98] = (Resources.Load("GUI/flags/hk") as Texture2D);
		this.tex_flags[99] = (Resources.Load("GUI/flags/hm") as Texture2D);
		this.tex_flags[100] = (Resources.Load("GUI/flags/hn") as Texture2D);
		this.tex_flags[101] = (Resources.Load("GUI/flags/hr") as Texture2D);
		this.tex_flags[102] = (Resources.Load("GUI/flags/ht") as Texture2D);
		this.tex_flags[103] = (Resources.Load("GUI/flags/hu") as Texture2D);
		this.tex_flags[104] = (Resources.Load("GUI/flags/id") as Texture2D);
		this.tex_flags[105] = (Resources.Load("GUI/flags/ie") as Texture2D);
		this.tex_flags[106] = (Resources.Load("GUI/flags/il") as Texture2D);
		this.tex_flags[107] = (Resources.Load("GUI/flags/in") as Texture2D);
		this.tex_flags[108] = (Resources.Load("GUI/flags/io") as Texture2D);
		this.tex_flags[109] = (Resources.Load("GUI/flags/iq") as Texture2D);
		this.tex_flags[110] = (Resources.Load("GUI/flags/ir") as Texture2D);
		this.tex_flags[111] = (Resources.Load("GUI/flags/is") as Texture2D);
		this.tex_flags[112] = (Resources.Load("GUI/flags/it") as Texture2D);
		this.tex_flags[113] = (Resources.Load("GUI/flags/jm") as Texture2D);
		this.tex_flags[114] = (Resources.Load("GUI/flags/jo") as Texture2D);
		this.tex_flags[115] = (Resources.Load("GUI/flags/jp") as Texture2D);
		this.tex_flags[116] = (Resources.Load("GUI/flags/ke") as Texture2D);
		this.tex_flags[117] = (Resources.Load("GUI/flags/kg") as Texture2D);
		this.tex_flags[118] = (Resources.Load("GUI/flags/kh") as Texture2D);
		this.tex_flags[119] = (Resources.Load("GUI/flags/ki") as Texture2D);
		this.tex_flags[120] = (Resources.Load("GUI/flags/km") as Texture2D);
		this.tex_flags[121] = (Resources.Load("GUI/flags/kn") as Texture2D);
		this.tex_flags[122] = (Resources.Load("GUI/flags/kp") as Texture2D);
		this.tex_flags[123] = (Resources.Load("GUI/flags/kr") as Texture2D);
		this.tex_flags[124] = (Resources.Load("GUI/flags/kw") as Texture2D);
		this.tex_flags[125] = (Resources.Load("GUI/flags/ky") as Texture2D);
		this.tex_flags[126] = (Resources.Load("GUI/flags/la") as Texture2D);
		this.tex_flags[127] = (Resources.Load("GUI/flags/lb") as Texture2D);
		this.tex_flags[128] = (Resources.Load("GUI/flags/lc") as Texture2D);
		this.tex_flags[129] = (Resources.Load("GUI/flags/li") as Texture2D);
		this.tex_flags[130] = (Resources.Load("GUI/flags/lk") as Texture2D);
		this.tex_flags[131] = (Resources.Load("GUI/flags/lr") as Texture2D);
		this.tex_flags[132] = (Resources.Load("GUI/flags/ls") as Texture2D);
		this.tex_flags[133] = (Resources.Load("GUI/flags/lt") as Texture2D);
		this.tex_flags[134] = (Resources.Load("GUI/flags/lu") as Texture2D);
		this.tex_flags[135] = (Resources.Load("GUI/flags/ly") as Texture2D);
		this.tex_flags[136] = (Resources.Load("GUI/flags/ma") as Texture2D);
		this.tex_flags[137] = (Resources.Load("GUI/flags/mc") as Texture2D);
		this.tex_flags[138] = (Resources.Load("GUI/flags/me") as Texture2D);
		this.tex_flags[139] = (Resources.Load("GUI/flags/mg") as Texture2D);
		this.tex_flags[140] = (Resources.Load("GUI/flags/mh") as Texture2D);
		this.tex_flags[141] = (Resources.Load("GUI/flags/mk") as Texture2D);
		this.tex_flags[142] = (Resources.Load("GUI/flags/ml") as Texture2D);
		this.tex_flags[143] = (Resources.Load("GUI/flags/mm") as Texture2D);
		this.tex_flags[144] = (Resources.Load("GUI/flags/mn") as Texture2D);
		this.tex_flags[145] = (Resources.Load("GUI/flags/mo") as Texture2D);
		this.tex_flags[146] = (Resources.Load("GUI/flags/mp") as Texture2D);
		this.tex_flags[147] = (Resources.Load("GUI/flags/mq") as Texture2D);
		this.tex_flags[148] = (Resources.Load("GUI/flags/mr") as Texture2D);
		this.tex_flags[149] = (Resources.Load("GUI/flags/ms") as Texture2D);
		this.tex_flags[150] = (Resources.Load("GUI/flags/mt") as Texture2D);
		this.tex_flags[151] = (Resources.Load("GUI/flags/mu") as Texture2D);
		this.tex_flags[152] = (Resources.Load("GUI/flags/mv") as Texture2D);
		this.tex_flags[153] = (Resources.Load("GUI/flags/mw") as Texture2D);
		this.tex_flags[154] = (Resources.Load("GUI/flags/mx") as Texture2D);
		this.tex_flags[155] = (Resources.Load("GUI/flags/my") as Texture2D);
		this.tex_flags[156] = (Resources.Load("GUI/flags/mz") as Texture2D);
		this.tex_flags[157] = (Resources.Load("GUI/flags/na") as Texture2D);
		this.tex_flags[158] = (Resources.Load("GUI/flags/nc") as Texture2D);
		this.tex_flags[159] = (Resources.Load("GUI/flags/ne") as Texture2D);
		this.tex_flags[160] = (Resources.Load("GUI/flags/nf") as Texture2D);
		this.tex_flags[161] = (Resources.Load("GUI/flags/ng") as Texture2D);
		this.tex_flags[162] = (Resources.Load("GUI/flags/ni") as Texture2D);
		this.tex_flags[163] = (Resources.Load("GUI/flags/nl") as Texture2D);
		this.tex_flags[164] = (Resources.Load("GUI/flags/no") as Texture2D);
		this.tex_flags[165] = (Resources.Load("GUI/flags/np") as Texture2D);
		this.tex_flags[166] = (Resources.Load("GUI/flags/nr") as Texture2D);
		this.tex_flags[167] = (Resources.Load("GUI/flags/nu") as Texture2D);
		this.tex_flags[168] = (Resources.Load("GUI/flags/nz") as Texture2D);
		this.tex_flags[169] = (Resources.Load("GUI/flags/om") as Texture2D);
		this.tex_flags[170] = (Resources.Load("GUI/flags/pa") as Texture2D);
		this.tex_flags[171] = (Resources.Load("GUI/flags/pe") as Texture2D);
		this.tex_flags[172] = (Resources.Load("GUI/flags/pf") as Texture2D);
		this.tex_flags[173] = (Resources.Load("GUI/flags/pg") as Texture2D);
		this.tex_flags[174] = (Resources.Load("GUI/flags/ph") as Texture2D);
		this.tex_flags[175] = (Resources.Load("GUI/flags/pk") as Texture2D);
		this.tex_flags[176] = (Resources.Load("GUI/flags/pl") as Texture2D);
		this.tex_flags[177] = (Resources.Load("GUI/flags/pm") as Texture2D);
		this.tex_flags[178] = (Resources.Load("GUI/flags/pn") as Texture2D);
		this.tex_flags[179] = (Resources.Load("GUI/flags/pr") as Texture2D);
		this.tex_flags[180] = (Resources.Load("GUI/flags/ps") as Texture2D);
		this.tex_flags[181] = (Resources.Load("GUI/flags/pt") as Texture2D);
		this.tex_flags[182] = (Resources.Load("GUI/flags/pw") as Texture2D);
		this.tex_flags[183] = (Resources.Load("GUI/flags/py") as Texture2D);
		this.tex_flags[184] = (Resources.Load("GUI/flags/qa") as Texture2D);
		this.tex_flags[185] = (Resources.Load("GUI/flags/re") as Texture2D);
		this.tex_flags[186] = (Resources.Load("GUI/flags/ro") as Texture2D);
		this.tex_flags[187] = (Resources.Load("GUI/flags/rs") as Texture2D);
		this.tex_flags[188] = (Resources.Load("GUI/flags/rw") as Texture2D);
		this.tex_flags[189] = (Resources.Load("GUI/flags/sa") as Texture2D);
		this.tex_flags[190] = (Resources.Load("GUI/flags/sb") as Texture2D);
		this.tex_flags[191] = (Resources.Load("GUI/flags/sc") as Texture2D);
		this.tex_flags[192] = (Resources.Load("GUI/flags/sd") as Texture2D);
		this.tex_flags[193] = (Resources.Load("GUI/flags/se") as Texture2D);
		this.tex_flags[194] = (Resources.Load("GUI/flags/sg") as Texture2D);
		this.tex_flags[195] = (Resources.Load("GUI/flags/sh") as Texture2D);
		this.tex_flags[196] = (Resources.Load("GUI/flags/si") as Texture2D);
		this.tex_flags[197] = (Resources.Load("GUI/flags/sj") as Texture2D);
		this.tex_flags[198] = (Resources.Load("GUI/flags/sk") as Texture2D);
		this.tex_flags[199] = (Resources.Load("GUI/flags/sl") as Texture2D);
		this.tex_flags[200] = (Resources.Load("GUI/flags/sm") as Texture2D);
		this.tex_flags[201] = (Resources.Load("GUI/flags/sn") as Texture2D);
		this.tex_flags[202] = (Resources.Load("GUI/flags/so") as Texture2D);
		this.tex_flags[203] = (Resources.Load("GUI/flags/sr") as Texture2D);
		this.tex_flags[204] = (Resources.Load("GUI/flags/st") as Texture2D);
		this.tex_flags[205] = (Resources.Load("GUI/flags/sv") as Texture2D);
		this.tex_flags[206] = (Resources.Load("GUI/flags/sy") as Texture2D);
		this.tex_flags[207] = (Resources.Load("GUI/flags/sz") as Texture2D);
		this.tex_flags[208] = (Resources.Load("GUI/flags/tc") as Texture2D);
		this.tex_flags[209] = (Resources.Load("GUI/flags/td") as Texture2D);
		this.tex_flags[210] = (Resources.Load("GUI/flags/tf") as Texture2D);
		this.tex_flags[211] = (Resources.Load("GUI/flags/tg") as Texture2D);
		this.tex_flags[212] = (Resources.Load("GUI/flags/th") as Texture2D);
		this.tex_flags[213] = (Resources.Load("GUI/flags/tj") as Texture2D);
		this.tex_flags[214] = (Resources.Load("GUI/flags/tk") as Texture2D);
		this.tex_flags[215] = (Resources.Load("GUI/flags/tl") as Texture2D);
		this.tex_flags[216] = (Resources.Load("GUI/flags/tm") as Texture2D);
		this.tex_flags[217] = (Resources.Load("GUI/flags/tn") as Texture2D);
		this.tex_flags[218] = (Resources.Load("GUI/flags/to") as Texture2D);
		this.tex_flags[219] = (Resources.Load("GUI/flags/tr") as Texture2D);
		this.tex_flags[220] = (Resources.Load("GUI/flags/tt") as Texture2D);
		this.tex_flags[221] = (Resources.Load("GUI/flags/tv") as Texture2D);
		this.tex_flags[222] = (Resources.Load("GUI/flags/tw") as Texture2D);
		this.tex_flags[223] = (Resources.Load("GUI/flags/tz") as Texture2D);
		this.tex_flags[224] = (Resources.Load("GUI/flags/ug") as Texture2D);
		this.tex_flags[225] = (Resources.Load("GUI/flags/um") as Texture2D);
		this.tex_flags[226] = (Resources.Load("GUI/flags/uy") as Texture2D);
		this.tex_flags[227] = (Resources.Load("GUI/flags/uz") as Texture2D);
		this.tex_flags[228] = (Resources.Load("GUI/flags/va") as Texture2D);
		this.tex_flags[229] = (Resources.Load("GUI/flags/vc") as Texture2D);
		this.tex_flags[230] = (Resources.Load("GUI/flags/ve") as Texture2D);
		this.tex_flags[231] = (Resources.Load("GUI/flags/vg") as Texture2D);
		this.tex_flags[232] = (Resources.Load("GUI/flags/vi") as Texture2D);
		this.tex_flags[233] = (Resources.Load("GUI/flags/vn") as Texture2D);
		this.tex_flags[234] = (Resources.Load("GUI/flags/vu") as Texture2D);
		this.tex_flags[235] = (Resources.Load("GUI/flags/wf") as Texture2D);
		this.tex_flags[236] = (Resources.Load("GUI/flags/ws") as Texture2D);
		this.tex_flags[237] = (Resources.Load("GUI/flags/ye") as Texture2D);
		this.tex_flags[238] = (Resources.Load("GUI/flags/yt") as Texture2D);
		this.tex_flags[239] = (Resources.Load("GUI/flags/za") as Texture2D);
		this.tex_flags[240] = (Resources.Load("GUI/flags/zm") as Texture2D);
		this.tex_flags[241] = (Resources.Load("GUI/flags/zw") as Texture2D);
		this.blueTexture = new Texture2D(1, 1);
		this.blueTexture.SetPixel(0, 0, new Color(0.03f, 0.23f, 0.76f, 1f));
		this.blueTexture.Apply();
		this.redTexture = new Texture2D(1, 1);
		this.redTexture.SetPixel(0, 0, new Color(0.65f, 0.18f, 0.14f, 1f));
		this.redTexture.Apply();
		this.greenTexture = new Texture2D(1, 1);
		this.greenTexture.SetPixel(0, 0, new Color(0.27f, 0.78f, 0.09f, 1f));
		this.greenTexture.Apply();
		this.yellowTexture = new Texture2D(1, 1);
		this.yellowTexture.SetPixel(0, 0, new Color(0.87f, 0.85f, 0f, 1f));
		this.yellowTexture.Apply();
		this.tex_premium = (Resources.Load("GUI/premium_game") as Texture2D);
		this.sorted = new SelectTeam.CSortedPlayer[32];
		for (int i = 0; i < 32; i++)
		{
			this.sorted[i] = new SelectTeam.CSortedPlayer();
		}
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x0004CCE8 File Offset: 0x0004AEE8
	private void Update()
	{
		if (this.tc == null)
		{
			this.tc = (TankController)Object.FindObjectOfType(typeof(TankController));
		}
		if (this.cc == null)
		{
			this.cc = (CarController)Object.FindObjectOfType(typeof(CarController));
		}
		if (this.tc != null && this.cc != null)
		{
			if (this.tc.enabled || this.cc.enabled)
			{
				this.can_team_select = false;
			}
			else
			{
				this.can_team_select = true;
			}
		}
		if (Input.GetKeyDown(9))
		{
			this.SortPlayers();
			this.show = true;
			this.show_teamselect = false;
		}
		if (Input.GetKeyUp(9))
		{
			this.show = false;
		}
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x0004CDB8 File Offset: 0x0004AFB8
	private void OnGUI()
	{
		if (this.show)
		{
			if (this.cspc == null)
			{
				this.cspc = (PlayerControl)Object.FindObjectOfType(typeof(PlayerControl));
			}
			if (this.sortedCount == 0)
			{
				this.SortPlayers();
			}
			if (Time.time > this.showtime + 2f)
			{
				this.showtime = Time.time;
				this.SortPlayers();
			}
			if (PlayerControl.GetGameMode() == 0 || PlayerControl.GetGameMode() == 4 || PlayerControl.GetGameMode() == 8)
			{
				this.r = new Rect(0f, 0f, 640f, 420f);
				this.r.center = new Vector2((float)Screen.width, (float)Screen.height) / 2f;
				GUI.Window(900, this.r, new GUI.WindowFunction(this.TabBattle), "", this.gui_style);
				return;
			}
			if (PlayerControl.GetGameMode() == CONST.CFG.BUILD_MODE || PlayerControl.GetGameMode() == 7 || PlayerControl.GetGameMode() == 10)
			{
				this.r = new Rect(0f, 0f, 320f, 671f);
				this.r.center = new Vector2((float)Screen.width, (float)Screen.height) / 2f;
				GUI.Window(900, this.r, new GUI.WindowFunction(this.TabBuild), "", this.gui_style);
				return;
			}
			if (PlayerControl.GetGameMode() == 3)
			{
				this.r = new Rect(0f, 0f, 640f, 620f);
				this.r.center = new Vector2((float)Screen.width, (float)Screen.height) / 2f;
				GUI.Window(900, this.r, new GUI.WindowFunction(this.TabZombie), "", this.gui_style);
				return;
			}
			if (PlayerControl.GetGameMode() == 5 || PlayerControl.GetGameMode() == 1)
			{
				this.r = new Rect(0f, 0f, 640f, 420f);
				this.r.center = new Vector2((float)Screen.width, (float)Screen.height) / 2f;
				GUI.Window(900, this.r, new GUI.WindowFunction(this.TabContra), "", this.gui_style);
				return;
			}
			if (PlayerControl.GetGameMode() == 6)
			{
				this.r = new Rect(0f, 0f, 640f, 420f);
				this.r.center = new Vector2((float)Screen.width, (float)Screen.height) / 2f;
				GUI.Window(900, this.r, new GUI.WindowFunction(this.TabContra), "", this.gui_style);
				return;
			}
			if (PlayerControl.GetGameMode() == 9)
			{
				this.r = new Rect(0f, 0f, 640f, 420f);
				this.r.center = new Vector2((float)Screen.width, (float)Screen.height) / 2f;
				GUI.Window(900, this.r, new GUI.WindowFunction(this.TabContra), "", this.gui_style);
				return;
			}
			if (PlayerControl.GetGameMode() == 11)
			{
				this.r = new Rect(0f, 0f, 640f, 420f);
				this.r.center = new Vector2((float)Screen.width, (float)Screen.height) / 2f;
				GUI.Window(900, this.r, new GUI.WindowFunction(this.TabContra), "", this.gui_style);
			}
		}
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x0004D194 File Offset: 0x0004B394
	private void TabBattle(int windowID)
	{
		GUI.DrawTexture(new Rect(0f, 0f, 640f, 420f), this.tex_tabmenu);
		GUIManager.DrawText(new Rect(0f, 0f, 600f, 28f), ConnectionInfo.HOSTNAME, 16, 4, 8);
		this.DrawSquad(1, 30, Lang.GetLabel(146), 0, false);
		this.DrawSquad(320, 30, Lang.GetLabel(147), 1, false);
		this.DrawSquad(1, 224, Lang.GetLabel(148), 2, false);
		this.DrawSquad(320, 224, Lang.GetLabel(149), 3, false);
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x0004D250 File Offset: 0x0004B450
	private void TabBuild(int windowID)
	{
		GUI.DrawTexture(new Rect(0f, 0f, 320f, 600f), this.tex_tabmenu2);
		GUIManager.DrawText(new Rect(0f, 0f, 320f, 28f), ConnectionInfo.HOSTNAME, 16, 4, 8);
		this.DrawPlayers(1, 30, 0, false, 20);
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x0004D2B8 File Offset: 0x0004B4B8
	private void TabZombie(int windowID)
	{
		GUI.DrawTexture(new Rect(0f, 0f, 640f, 620f), this.tex_tabmenu3);
		GUIManager.DrawText(new Rect(0f, 0f, 640f, 28f), ConnectionInfo.HOSTNAME, 16, 4, 8);
		GUIManager.DrawText(new Rect(1f, 30f, 318f, 22f), Lang.GetLabel(150), 16, 4, 8);
		GUIManager.DrawText(new Rect(320f, 30f, 318f, 22f), Lang.GetLabel(50), 16, 4, 8);
		this.DrawPlayers(1, 58, 0, false, 17);
		this.DrawPlayers(320, 58, 1, false, 17);
	}

	// Token: 0x060003DA RID: 986 RVA: 0x0004D384 File Offset: 0x0004B584
	private void TabContra(int windowID)
	{
		GUI.DrawTexture(new Rect(0f, 0f, 640f, 420f), this.tex_tabmenu2team);
		GUIManager.DrawText(new Rect(0f, 0f, 600f, 28f), ConnectionInfo.HOSTNAME, 16, 4, 8);
		this.DrawSquad(1, 30, Lang.GetLabel(146), 0, false);
		this.DrawSquad(320, 30, Lang.GetLabel(147), 1, false);
	}

	// Token: 0x060003DB RID: 987 RVA: 0x0004D40C File Offset: 0x0004B60C
	private void DrawSquad(int x, int y, string text, byte squadid, bool by_admin = false)
	{
		this.x_pos = x;
		this.y_pos = y;
		GUIManager.DrawText(new Rect((float)x, (float)y, 318f, 22f), text, 16, 4, 8);
		this.y_pos += 28;
		this.DrawPlayers(this.x_pos, this.y_pos, (int)squadid, by_admin, 20);
		if (this.show_teamselect)
		{
			this.DrawClasses(squadid);
		}
	}

	// Token: 0x060003DC RID: 988 RVA: 0x0004D47C File Offset: 0x0004B67C
	private void DrawClasses(byte squadid)
	{
		Texture2D texture2D;
		if (squadid == 0)
		{
			texture2D = this.blueTexture;
		}
		else if (squadid == 1)
		{
			texture2D = this.redTexture;
		}
		else if (squadid == 2)
		{
			texture2D = this.greenTexture;
		}
		else if (squadid == 3)
		{
			texture2D = this.yellowTexture;
		}
		else
		{
			texture2D = this.blueTexture;
		}
		int num = 29;
		int num2 = 12;
		float num3 = Input.mousePosition.x;
		float num4 = (float)Screen.height - Input.mousePosition.y;
		num3 -= (float)(Screen.width - 600) / 2f;
		num4 -= (float)(Screen.height - 400) / 2f;
		Rect rect;
		rect..ctor((float)(this.x_pos + num), (float)(this.y_pos + num2), 84f, 120f);
		Rect rect2;
		rect2..ctor((float)(this.x_pos + num + 84 + 2), (float)(this.y_pos + num2), 84f, 120f);
		Rect rect3;
		rect3..ctor((float)(this.x_pos + num + 168 + 4), (float)(this.y_pos + num2), 84f, 120f);
		Rect rect4;
		rect4..ctor((float)(this.x_pos + num), (float)(this.y_pos + num2 + 120 + 4), 256f, 18f);
		Rect rect5;
		rect5..ctor((float)(this.x_pos + num + 1), (float)(this.y_pos + num2 + 1), 82f, 18f);
		Rect rect6;
		rect6..ctor((float)(this.x_pos + num + 84 + 2 + 1), (float)(this.y_pos + num2 + 1), 82f, 18f);
		Rect rect7;
		rect7..ctor((float)(this.x_pos + num + 168 + 4 + 1), (float)(this.y_pos + num2 + 1), 82f, 18f);
		Rect rect8 = new Rect((float)(this.x_pos + num + 1), (float)(this.y_pos + num2 + 120 + 4 + 1), 254f, 16f);
		if (rect.Contains(new Vector2(num3, num4)))
		{
			if (!this.hover[0])
			{
				this.hover[0] = true;
			}
		}
		else if (this.hover[0])
		{
			this.hover[0] = false;
		}
		if (rect2.Contains(new Vector2(num3, num4)))
		{
			if (!this.hover[1])
			{
				this.hover[1] = true;
			}
		}
		else if (this.hover[1])
		{
			this.hover[1] = false;
		}
		if (rect3.Contains(new Vector2(num3, num4)))
		{
			if (!this.hover[2])
			{
				this.hover[2] = true;
			}
		}
		else if (this.hover[2])
		{
			this.hover[2] = false;
		}
		if (rect4.Contains(new Vector2(num3, num4)))
		{
			if (!this.hover[3])
			{
				this.hover[3] = true;
			}
		}
		else if (this.hover[3])
		{
			this.hover[3] = false;
		}
		GUI.DrawTexture(rect5, texture2D);
		GUI.DrawTexture(rect6, texture2D);
		GUI.DrawTexture(rect7, texture2D);
		GUI.DrawTexture(rect8, texture2D);
		if (this.hover[0])
		{
			GUI.DrawTexture(rect, texture2D);
		}
		else if (this.hover[1])
		{
			GUI.DrawTexture(rect2, texture2D);
		}
		else if (this.hover[2])
		{
			GUI.DrawTexture(rect3, texture2D);
		}
		else if (this.hover[3])
		{
			GUI.DrawTexture(rect4, texture2D);
		}
		GUI.DrawTexture(new Rect((float)(this.x_pos + num), (float)(this.y_pos + num2), 128f, 128f), this.tex_class_mp5);
		GUI.DrawTexture(new Rect((float)(this.x_pos + num + 84 + 2), (float)(this.y_pos + num2), 128f, 128f), this.tex_class_m3);
		GUI.DrawTexture(new Rect((float)(this.x_pos + num + 168 + 4), (float)(this.y_pos + num2), 128f, 128f), this.tex_class_m14);
		if (this.cscl == null)
		{
			this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
		}
		if (GUI.Button(rect, "", this.gui_style))
		{
			this.cscl.send_jointeamclass(squadid, 0);
		}
		if (GUI.Button(rect2, "", this.gui_style))
		{
			this.cscl.send_jointeamclass(squadid, 1);
		}
		if (GUI.Button(rect3, "", this.gui_style))
		{
			this.cscl.send_jointeamclass(squadid, 2);
		}
		this.gui_style.alignment = 1;
		if (GUI.Button(rect4, Lang.GetLabel(221), this.gui_style))
		{
			this.cscl.send_last_config(squadid);
		}
	}

	// Token: 0x060003DD RID: 989 RVA: 0x0004D8FC File Offset: 0x0004BAFC
	private void DrawPlayers(int x, int y, int squadid, bool by_admin = false, int offset = 20)
	{
		if (this.cscl == null)
		{
			this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
		}
		for (int i = 0; i < this.sortedCount; i++)
		{
			int id = this.sorted[i].id;
			if (ConnectionInfo.mode == CONST.CFG.BUILD_MODE || (int)BotsController.Instance.Bots[id].Team == squadid)
			{
				float num = (float)(offset - 16) / 2f;
				if (BotsController.Instance.Bots[id].Item[6] == 1)
				{
					GUI.color = new Color(1f, 1f, 1f, 0.1f);
					GUI.DrawTexture(new Rect((float)x, (float)y, 318f, (float)offset), GUIManager.tex_yellow);
					GUI.color = new Color(1f, 1f, 1f, 1f);
					GUI.DrawTexture(new Rect((float)(x + 160), (float)y + num, 16f, 16f), this.tex_premium);
				}
				if (id == this.cscl.myindex)
				{
					if (BotsController.Instance.Bots[id].Item[6] == 0)
					{
						GUI.color = new Color(1f, 0f, 0f, 0.25f);
						GUI.DrawTexture(new Rect((float)x, (float)y, 318f, (float)offset), GUIManager.tex_yellow);
						GUI.color = new Color(1f, 1f, 1f, 1f);
					}
					Color gray;
					gray..ctor(1f, 0.7f, 0f, 1f);
					if (BotsController.Instance.Bots[id].Dead > 0)
					{
						gray = Color.gray;
					}
					GUIManager.DrawText2(new Rect((float)(x + 4 + 20), (float)y, 200f, (float)offset), BotsController.Instance.Bots[id].Name, 14, 3, gray);
				}
				else
				{
					Color gray2;
					gray2..ctor(1f, 1f, 1f, 1f);
					if (BotsController.Instance.Bots[id].Dead > 0)
					{
						gray2 = Color.gray;
					}
					GUIManager.DrawText2(new Rect((float)(x + 4 + 20), (float)y, 200f, (float)offset), BotsController.Instance.Bots[id].Name, 14, 3, gray2);
				}
				GUIManager.DrawText2(new Rect((float)(x + 180), (float)y, 200f, (float)offset), BotsController.Instance.Bots[id].ClanName, 14, 3, new Color(1f, 1f, 0f, 1f));
				GUIManager.DrawText2(new Rect((float)(x + 235), (float)y, 60f, (float)offset), BotsController.Instance.Bots[id].Stats_Kills.ToString(), 14, 4, new Color(0.49f, 0.74f, 1f, 1f));
				GUIManager.DrawText2(new Rect((float)(x + 265), (float)y, 60f, (float)offset), BotsController.Instance.Bots[id].Stats_Deads.ToString(), 14, 4, new Color(0.56f, 0.56f, 0.56f, 1f));
				y += offset;
				GUIManager.gs_style2.normal.textColor = new Color(1f, 1f, 1f, 1f);
			}
		}
	}

	// Token: 0x060003DE RID: 990 RVA: 0x0004DC78 File Offset: 0x0004BE78
	private void SortPlayers()
	{
		this.sortedCount = 0;
		for (int i = 0; i < 32; i++)
		{
			if (BotsController.Instance.Bots[i].Active)
			{
				this.sorted[this.sortedCount].id = i;
				this.sorted[this.sortedCount].frags = BotsController.Instance.Bots[i].Stats_Kills;
				this.sortedCount++;
			}
		}
		if (this.sortedCount <= 1)
		{
			return;
		}
		bool flag = false;
		while (!flag)
		{
			flag = true;
			for (int j = 1; j < this.sortedCount; j++)
			{
				if (this.sorted[j].frags > this.sorted[j - 1].frags)
				{
					int id = this.sorted[j - 1].id;
					int frags = this.sorted[j - 1].frags;
					this.sorted[j - 1].id = this.sorted[j].id;
					this.sorted[j - 1].frags = this.sorted[j].frags;
					this.sorted[j].id = id;
					this.sorted[j].frags = frags;
					flag = false;
				}
			}
		}
	}

	// Token: 0x04000806 RID: 2054
	private bool show;

	// Token: 0x04000807 RID: 2055
	private bool show_teamselect;

	// Token: 0x04000808 RID: 2056
	private WeaponSystem csws;

	// Token: 0x04000809 RID: 2057
	private PlayerControl cspc;

	// Token: 0x0400080A RID: 2058
	private Client cscl;

	// Token: 0x0400080B RID: 2059
	private GameObject Map;

	// Token: 0x0400080C RID: 2060
	private Rect r;

	// Token: 0x0400080D RID: 2061
	private Texture2D tex_tabmenu;

	// Token: 0x0400080E RID: 2062
	private Texture2D tex_tabmenu2;

	// Token: 0x0400080F RID: 2063
	private Texture2D tex_tabmenu3;

	// Token: 0x04000810 RID: 2064
	private Texture2D tex_class_mp5;

	// Token: 0x04000811 RID: 2065
	private Texture2D tex_class_m3;

	// Token: 0x04000812 RID: 2066
	private Texture2D tex_class_m14;

	// Token: 0x04000813 RID: 2067
	private Texture2D tex_tabmenu2team;

	// Token: 0x04000814 RID: 2068
	private Texture2D tex_premium;

	// Token: 0x04000815 RID: 2069
	private Texture2D[] tex_flags = new Texture2D[245];

	// Token: 0x04000816 RID: 2070
	private Texture2D tex_border;

	// Token: 0x04000817 RID: 2071
	private GUIStyle gui_style;

	// Token: 0x04000818 RID: 2072
	private int y_pos;

	// Token: 0x04000819 RID: 2073
	private int x_pos;

	// Token: 0x0400081A RID: 2074
	private bool[] hover = new bool[4];

	// Token: 0x0400081B RID: 2075
	private Texture2D redTexture;

	// Token: 0x0400081C RID: 2076
	private Texture2D blueTexture;

	// Token: 0x0400081D RID: 2077
	private Texture2D greenTexture;

	// Token: 0x0400081E RID: 2078
	private Texture2D yellowTexture;

	// Token: 0x0400081F RID: 2079
	private SelectTeam.CSortedPlayer[] sorted;

	// Token: 0x04000820 RID: 2080
	private int sortedCount;

	// Token: 0x04000821 RID: 2081
	private float showtime;

	// Token: 0x04000822 RID: 2082
	private TankController tc;

	// Token: 0x04000823 RID: 2083
	private CarController cc;

	// Token: 0x04000824 RID: 2084
	private bool can_team_select;

	// Token: 0x02000858 RID: 2136
	public class CSortedPlayer
	{
		// Token: 0x040031C9 RID: 12745
		public int id;

		// Token: 0x040031CA RID: 12746
		public int frags;
	}
}
