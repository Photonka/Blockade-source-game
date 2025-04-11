using System;
using UnityEngine;

// Token: 0x0200008E RID: 142
public class New_Select_Team : MonoBehaviour
{
	// Token: 0x0600042A RID: 1066 RVA: 0x00002B75 File Offset: 0x00000D75
	public void OpenMenu()
	{
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x00002B75 File Offset: 0x00000D75
	public void CloseMenu()
	{
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x0005349C File Offset: 0x0005169C
	private void Awake()
	{
		this.EM = base.GetComponent<E_Menu>();
		this.New_Select_Teams_Icons = new Texture[5];
		this.New_Select_Teams_Icons_Background = new Texture[4];
		this.New_Select_Teams_Icons_Tab = new Texture[4];
		this.New_Select_Teams_Names = new string[4];
		this.Title_Background = GUI3.GetTexture2D(Color.black, 100f);
		this.Icon_Background = GUI3.GetTexture2D(Color.black, 60f);
		this.List_Background = GUI3.GetTexture2D(Color.black, 75f);
		this.New_Select_Teams_Icons_Background[0] = GUI3.GetTexture2D(Color.blue, 60f);
		this.New_Select_Teams_Icons_Background[1] = GUI3.GetTexture2D(Color.red, 60f);
		this.New_Select_Teams_Icons_Background[2] = GUI3.GetTexture2D(Color.green, 60f);
		this.New_Select_Teams_Icons_Background[3] = GUI3.GetTexture2D(Color.yellow, 60f);
		this.yellow_tex = GUI3.GetTexture2D(Color.yellow, 50f);
		this.New_Select_Teams_Icons[0] = (Resources.Load("NewMenu/TeamSelect/team_blue") as Texture);
		this.New_Select_Teams_Icons[1] = (Resources.Load("NewMenu/TeamSelect/team_red") as Texture);
		this.New_Select_Teams_Icons[2] = (Resources.Load("NewMenu/TeamSelect/team_green") as Texture);
		this.New_Select_Teams_Icons[3] = (Resources.Load("NewMenu/TeamSelect/team_yellow") as Texture);
		this.New_Select_Teams_Icons[4] = (Resources.Load("NewMenu/TeamSelect/team_zombie") as Texture);
		this.New_Select_Teams_Icons_Tab[0] = GUI3.GetTexture2D(Color.blue, 100f);
		this.New_Select_Teams_Icons_Tab[1] = GUI3.GetTexture2D(Color.red, 100f);
		this.New_Select_Teams_Icons_Tab[2] = GUI3.GetTexture2D(Color.green, 100f);
		this.New_Select_Teams_Icons_Tab[3] = GUI3.GetTexture2D(Color.yellow, 100f);
		this.New_Select_Teams_Names[0] = Lang.GetLabel(400);
		this.New_Select_Teams_Names[1] = Lang.GetLabel(401);
		this.New_Select_Teams_Names[2] = Lang.GetLabel(402);
		this.New_Select_Teams_Names[3] = Lang.GetLabel(403);
		this.Play_Button_Normal = (Resources.Load("NewMenu/Play_Button_Normal") as Texture);
		this.Play_Button_Hover = (Resources.Load("NewMenu/Play_Button_Hover") as Texture);
		this.New_Select_Teams = new Rect[4];
		this.New_Select_Teams_List = new Rect[4];
		this.Map = GameObject.Find("Map");
		this.gui_style = new GUIStyle();
		this.gui_style.font = (Resources.Load("NewMenu/E_Menu_Font4") as Font);
		this.gui_style.normal.textColor = Color.white;
		this.MGUI = (MainGUI)Object.FindObjectOfType(typeof(MainGUI));
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
		this.tex_premium = (Resources.Load("GUI/premium_game") as Texture2D);
		this.sorted = new New_Select_Team.CSortedPlayer[32];
		for (int i = 0; i < 32; i++)
		{
			this.sorted[i] = new New_Select_Team.CSortedPlayer();
		}
	}

	// Token: 0x0600042D RID: 1069 RVA: 0x00002B75 File Offset: 0x00000D75
	private void Update()
	{
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x00054F8C File Offset: 0x0005318C
	public void Draw_New_Select_Team()
	{
		if (this.tc == null)
		{
			this.tc = (TankController)Object.FindObjectOfType(typeof(TankController));
		}
		if (this.tc != null && this.tc.enabled)
		{
			return;
		}
		if (this.cc == null)
		{
			this.cc = (CarController)Object.FindObjectOfType(typeof(CarController));
		}
		if (this.cc != null && this.cc.enabled)
		{
			return;
		}
		if (PlayerControl.GetGameMode() == CONST.CFG.BUILD_MODE || PlayerControl.GetGameMode() == 10)
		{
			return;
		}
		int num;
		if (PlayerControl.GetGameMode() == 7 || PlayerControl.GetGameMode() == 3)
		{
			num = 2;
		}
		else if (PlayerControl.GetGameMode() == 0 || PlayerControl.GetGameMode() == 4)
		{
			num = 0;
		}
		else
		{
			num = 1;
		}
		this.x = Screen.width / 2 - 300;
		this.y = Screen.height / 2 - (Screen.height - 220) / 2;
		this.Play_Button_Rect = new Rect((float)(Screen.width / 2 - 86), 50f, 172f, 42f);
		if (num == 2)
		{
			this.Title_Rect = new Rect((float)this.x, (float)(this.y - 42), 600f, 22f);
		}
		else
		{
			this.Title_Rect = new Rect((float)this.x, (float)this.y, 600f, 22f);
		}
		this.mousePos = new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y);
		if (this.Play_Button_Rect.Contains(this.mousePos))
		{
			this.In_Play_Button_Rect = true;
		}
		else
		{
			this.In_Play_Button_Rect = false;
		}
		if (num != 2)
		{
			this.Draw_Play_Button();
		}
		GUI.DrawTexture(this.Title_Rect, this.Title_Background);
		string label = Lang.GetLabel(404);
		if (num == 2)
		{
			label = Lang.GetLabel(405);
		}
		this.gui_style.fontSize = 18;
		this.gui_style.alignment = 4;
		this.gui_style.normal.textColor = Color.black;
		this.gui_style.padding.bottom = 0;
		GUI.Label(new Rect(this.Title_Rect.x + 1f, this.Title_Rect.y + 1f, this.Title_Rect.width, 22f), label, this.gui_style);
		this.gui_style.normal.textColor = Color.yellow;
		GUI.Label(this.Title_Rect, label, this.gui_style);
		if (num == 0)
		{
			this.Draw_Battle();
		}
		else if (num == 1)
		{
			this.Draw_Contra();
		}
		else if (num == 2)
		{
			this.Draw_Zombie();
		}
		if (this.last_time < Time.time)
		{
			this.SortPlayers();
			this.last_time += 1f;
		}
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x00055270 File Offset: 0x00053470
	private void Draw_New_Select_Zombie_Tab(int i)
	{
		GUI.DrawTexture(this.New_Select_Teams[i], this.New_Select_Teams_Icons_Background[i]);
		if (i == 0)
		{
			GUI.DrawTexture(this.New_Select_Teams[i], this.New_Select_Teams_Icons[i]);
		}
		else
		{
			GUI.DrawTexture(this.New_Select_Teams[i], this.New_Select_Teams_Icons[4]);
		}
		GUI.DrawTexture(new Rect(this.New_Select_Teams[i].x, this.New_Select_Teams[i].yMax - 20f, this.New_Select_Teams[i].width, 20f), this.New_Select_Teams_Icons_Tab[i]);
		GUI.DrawTexture(this.New_Select_Teams_List[i], this.List_Background);
		this.gui_style.fontSize = 14;
		this.gui_style.alignment = 4;
		this.gui_style.normal.textColor = Color.black;
		this.gui_style.padding.bottom = 0;
		this.New_Select_Teams_Names[0] = Lang.GetLabel(406);
		if (PlayerControl.GetGameMode() == 7)
		{
			this.New_Select_Teams_Names[1] = Lang.GetLabel(407);
		}
		else
		{
			this.New_Select_Teams_Names[1] = Lang.GetLabel(408);
		}
		GUI.Label(new Rect(this.New_Select_Teams[i].x + 1f, this.New_Select_Teams[i].yMax - 20f + 1f, this.New_Select_Teams[i].width, 20f), this.New_Select_Teams_Names[i], this.gui_style);
		this.gui_style.normal.textColor = Color.white;
		GUI.Label(new Rect(this.New_Select_Teams[i].x, this.New_Select_Teams[i].yMax - 20f, this.New_Select_Teams[i].width, 20f), this.New_Select_Teams_Names[i], this.gui_style);
		int num = (int)this.New_Select_Teams_List[i].x + 5;
		int num2 = (int)this.New_Select_Teams_List[i].y + 5;
		this.gui_style.alignment = 3;
		this.DrawPlayers(num, num2, i, 20);
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x000554C4 File Offset: 0x000536C4
	private void Draw_Tab(int i)
	{
		if (this.New_Select_Teams[i].Contains(this.mousePos) || this.New_Select_Teams_List[i].Contains(this.mousePos))
		{
			GUI.DrawTexture(this.New_Select_Teams[i], this.New_Select_Teams_Icons_Background[i]);
		}
		else
		{
			GUI.DrawTexture(this.New_Select_Teams[i], this.Icon_Background);
		}
		GUI.DrawTexture(this.New_Select_Teams[i], this.New_Select_Teams_Icons[i]);
		GUI.DrawTexture(new Rect(this.New_Select_Teams[i].x, this.New_Select_Teams[i].yMax - 20f, this.New_Select_Teams[i].width, 20f), this.New_Select_Teams_Icons_Tab[i]);
		GUI.DrawTexture(this.New_Select_Teams_List[i], this.List_Background);
		this.gui_style.fontSize = 14;
		this.gui_style.alignment = 4;
		this.gui_style.normal.textColor = Color.black;
		this.gui_style.padding.bottom = 0;
		GUI.Label(new Rect(this.New_Select_Teams[i].x + 1f, this.New_Select_Teams[i].yMax - 20f + 1f, this.New_Select_Teams[i].width, 20f), this.New_Select_Teams_Names[i], this.gui_style);
		this.gui_style.normal.textColor = Color.white;
		GUI.Label(new Rect(this.New_Select_Teams[i].x, this.New_Select_Teams[i].yMax - 20f, this.New_Select_Teams[i].width, 20f), this.New_Select_Teams_Names[i], this.gui_style);
		int num = (int)this.New_Select_Teams_List[i].x + 5;
		int num2 = (int)this.New_Select_Teams_List[i].y + 5;
		this.gui_style.alignment = 3;
		this.DrawPlayers(num, num2, i, 20);
		if (GUI.Button(this.New_Select_Teams[i], "", this.gui_style) || GUI.Button(this.New_Select_Teams_List[i], "", this.gui_style))
		{
			if (this.cscl == null)
			{
				this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
			}
			this.cscl.send_jointeamclass((byte)i, 0);
		}
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x00055774 File Offset: 0x00053974
	private void Draw_Battle()
	{
		this.New_Select_Teams[0] = new Rect((float)this.x, (float)(this.y + 22), 150f, 170f);
		this.New_Select_Teams[1] = new Rect((float)(this.x + 150), (float)(this.y + 22), 150f, 170f);
		this.New_Select_Teams[2] = new Rect((float)(this.x + 300), (float)(this.y + 22), 150f, 170f);
		this.New_Select_Teams[3] = new Rect((float)(this.x + 450), (float)(this.y + 22), 150f, 170f);
		this.New_Select_Teams_List[0] = new Rect((float)this.x, (float)(this.y + 22 + 170), 150f, 170f);
		this.New_Select_Teams_List[1] = new Rect((float)(this.x + 150), (float)(this.y + 22 + 170), 150f, 170f);
		this.New_Select_Teams_List[2] = new Rect((float)(this.x + 300), (float)(this.y + 22 + 170), 150f, 170f);
		this.New_Select_Teams_List[3] = new Rect((float)(this.x + 450), (float)(this.y + 22 + 170), 150f, 170f);
		for (int i = 0; i < 4; i++)
		{
			this.Draw_Tab(i);
			if (i == 0 || i == 1 || i == 2)
			{
				this.EM.Draw_Borders(this.New_Select_Teams[i], true, false, false, true);
				this.EM.Draw_Borders(this.New_Select_Teams_List[i], true, false, false, true);
			}
			if (i == 3)
			{
				this.EM.Draw_Borders(this.New_Select_Teams[i], true, false, true, true);
				this.EM.Draw_Borders(this.New_Select_Teams_List[i], true, false, false, true);
			}
		}
	}

	// Token: 0x06000432 RID: 1074 RVA: 0x000559B4 File Offset: 0x00053BB4
	private void Draw_Contra()
	{
		this.New_Select_Teams[0] = new Rect((float)this.x, (float)(this.y + 22), 150f, 170f);
		this.New_Select_Teams[1] = new Rect((float)(this.x + 450), (float)(this.y + 22), 150f, 170f);
		this.New_Select_Teams[2] = new Rect(0f, 0f, 0f, 0f);
		this.New_Select_Teams[3] = new Rect(0f, 0f, 0f, 0f);
		this.New_Select_Teams_List[0] = new Rect((float)(this.x + 150), (float)(this.y + 22), 150f, 330f);
		this.New_Select_Teams_List[1] = new Rect((float)(this.x + 300), (float)(this.y + 22), 150f, 330f);
		this.New_Select_Teams_List[2] = new Rect(0f, 0f, 0f, 0f);
		this.New_Select_Teams_List[3] = new Rect(0f, 0f, 0f, 0f);
		for (int i = 0; i < 2; i++)
		{
			this.Draw_Tab(i);
			if (i == 0)
			{
				this.EM.Draw_Borders(this.New_Select_Teams[i], true, false, false, true);
				this.EM.Draw_Borders(this.New_Select_Teams_List[i], true, false, false, true);
			}
			if (i == 1)
			{
				this.EM.Draw_Borders(this.New_Select_Teams[i], false, false, true, true);
				this.EM.Draw_Borders(this.New_Select_Teams_List[i], true, false, true, true);
			}
		}
	}

	// Token: 0x06000433 RID: 1075 RVA: 0x00055BA0 File Offset: 0x00053DA0
	private void Draw_Zombie()
	{
		this.New_Select_Teams[0] = new Rect((float)this.x, (float)(this.y + 22 - 42), 150f, 170f);
		this.New_Select_Teams[1] = new Rect((float)(this.x + 450), (float)(this.y + 22 - 42), 150f, 170f);
		this.New_Select_Teams[2] = new Rect(0f, 0f, 0f, 0f);
		this.New_Select_Teams[3] = new Rect(0f, 0f, 0f, 0f);
		this.New_Select_Teams_List[0] = new Rect((float)(this.x + 150), (float)(this.y + 22 - 42), 150f, 650f);
		this.New_Select_Teams_List[1] = new Rect((float)(this.x + 300), (float)(this.y + 22 - 42), 150f, 650f);
		this.New_Select_Teams_List[2] = new Rect(0f, 0f, 0f, 0f);
		this.New_Select_Teams_List[3] = new Rect(0f, 0f, 0f, 0f);
		this.Draw_New_Select_Zombie_Tab(0);
		this.Draw_New_Select_Zombie_Tab(1);
		this.EM.Draw_Borders(this.New_Select_Teams[0], true, false, false, true);
		this.EM.Draw_Borders(this.New_Select_Teams_List[0], true, false, false, true);
		this.EM.Draw_Borders(this.New_Select_Teams[1], false, false, true, true);
		this.EM.Draw_Borders(this.New_Select_Teams_List[1], true, false, true, true);
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x00055D8C File Offset: 0x00053F8C
	public void Draw_Play_Button()
	{
		if (this.In_Play_Button_Rect)
		{
			GUI.DrawTexture(this.Play_Button_Rect, this.Play_Button_Hover);
		}
		else
		{
			GUI.DrawTexture(this.Play_Button_Rect, this.Play_Button_Normal);
		}
		this.gui_style.fontSize = 22;
		this.gui_style.alignment = 4;
		this.gui_style.normal.textColor = Color.black;
		this.gui_style.padding.bottom = 0;
		GUI.Label(new Rect(this.Play_Button_Rect.x + 2f, this.Play_Button_Rect.y + 2f, this.Play_Button_Rect.width, this.Play_Button_Rect.height), Lang.GetLabel(409), this.gui_style);
		this.gui_style.normal.textColor = Color.white;
		GUI.Label(this.Play_Button_Rect, Lang.GetLabel(409), this.gui_style);
		if (GUI.Button(this.Play_Button_Rect, "", this.gui_style))
		{
			if (this.cscl == null)
			{
				this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
			}
			this.cscl.send_auto_jointeamclass();
		}
	}

	// Token: 0x06000435 RID: 1077 RVA: 0x00055ED4 File Offset: 0x000540D4
	private void DrawPlayers(int x, int y, int squadid, int offset = 20)
	{
		if (this.cscl == null)
		{
			this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
		}
		for (int i = 0; i < this.sortedCount; i++)
		{
			int id = this.sorted[i].id;
			if ((int)BotsController.Instance.Bots[id].Team == squadid)
			{
				this.gui_style.fontSize = 12;
				this.gui_style.alignment = 3;
				if (BotsController.Instance.Bots[id].Dead > 0)
				{
					this.gui_style.normal.textColor = Color.gray;
				}
				else
				{
					this.gui_style.normal.textColor = Color.white;
				}
				if (BotsController.Instance.Bots[id].Item[6] == 1)
				{
					GUI.DrawTexture(new Rect((float)(x - 5), (float)y, 150f, (float)offset), this.yellow_tex);
					GUI.DrawTexture(new Rect((float)(x + 140 - 16), (float)y, 16f, 16f), this.tex_premium);
				}
				string text = BotsController.Instance.Bots[id].Name;
				if (text.Length > 12)
				{
					text = text.Substring(0, 12);
				}
				if (id == this.cscl.myindex)
				{
					if (BotsController.Instance.Bots[id].Item[6] == 0)
					{
						GUI.DrawTexture(new Rect((float)(x - 5), (float)y, 150f, (float)offset), this.yellow_tex);
					}
					GUI.Label(new Rect((float)(x + 18), (float)y, 102f, (float)offset), text, this.gui_style);
				}
				else
				{
					GUI.Label(new Rect((float)(x + 18), (float)y, 102f, (float)offset), text, this.gui_style);
				}
				y += offset;
			}
		}
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x000560A8 File Offset: 0x000542A8
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

	// Token: 0x04000892 RID: 2194
	private E_Menu EM;

	// Token: 0x04000893 RID: 2195
	public Texture yellow_tex;

	// Token: 0x04000894 RID: 2196
	public Texture Title_Background;

	// Token: 0x04000895 RID: 2197
	public Texture Icon_Background;

	// Token: 0x04000896 RID: 2198
	public Texture List_Background;

	// Token: 0x04000897 RID: 2199
	public Texture[] New_Select_Teams_Icons_Background;

	// Token: 0x04000898 RID: 2200
	public Texture[] New_Select_Teams_Icons;

	// Token: 0x04000899 RID: 2201
	public Texture[] New_Select_Teams_Icons_Tab;

	// Token: 0x0400089A RID: 2202
	public string[] New_Select_Teams_Names;

	// Token: 0x0400089B RID: 2203
	public Rect Title_Rect;

	// Token: 0x0400089C RID: 2204
	public Rect Icon_Rect;

	// Token: 0x0400089D RID: 2205
	public Rect[] New_Select_Teams;

	// Token: 0x0400089E RID: 2206
	public Rect[] New_Select_Teams_List;

	// Token: 0x0400089F RID: 2207
	public Texture Play_Button_Normal;

	// Token: 0x040008A0 RID: 2208
	public Texture Play_Button_Hover;

	// Token: 0x040008A1 RID: 2209
	public Texture2D Border;

	// Token: 0x040008A2 RID: 2210
	public Rect Play_Button_Rect;

	// Token: 0x040008A3 RID: 2211
	private WeaponSystem csws;

	// Token: 0x040008A4 RID: 2212
	private PlayerControl cspc;

	// Token: 0x040008A5 RID: 2213
	private Client cscl;

	// Token: 0x040008A6 RID: 2214
	private GameObject Map;

	// Token: 0x040008A7 RID: 2215
	private MainGUI MGUI;

	// Token: 0x040008A8 RID: 2216
	private Texture2D tex_premium;

	// Token: 0x040008A9 RID: 2217
	private Texture2D[] tex_flags = new Texture2D[245];

	// Token: 0x040008AA RID: 2218
	private GUIStyle gui_style;

	// Token: 0x040008AB RID: 2219
	private New_Select_Team.CSortedPlayer[] sorted;

	// Token: 0x040008AC RID: 2220
	private int sortedCount;

	// Token: 0x040008AD RID: 2221
	private float showtime;

	// Token: 0x040008AE RID: 2222
	private TankController tc;

	// Token: 0x040008AF RID: 2223
	private CarController cc;

	// Token: 0x040008B0 RID: 2224
	private bool can_team_select;

	// Token: 0x040008B1 RID: 2225
	private int x;

	// Token: 0x040008B2 RID: 2226
	private int y;

	// Token: 0x040008B3 RID: 2227
	private float last_time;

	// Token: 0x040008B4 RID: 2228
	private bool In_Play_Button_Rect;

	// Token: 0x040008B5 RID: 2229
	private Vector2 mousePos;

	// Token: 0x02000859 RID: 2137
	public class CSortedPlayer
	{
		// Token: 0x040031CB RID: 12747
		public int id;

		// Token: 0x040031CC RID: 12748
		public int frags;
	}
}
