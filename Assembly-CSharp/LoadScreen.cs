using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000080 RID: 128
public class LoadScreen : MonoBehaviour
{
	// Token: 0x0600039C RID: 924 RVA: 0x00044B6C File Offset: 0x00042D6C
	private void Awake()
	{
		LoadScreen.THIS = this;
		GUIManager.Init(false);
		this.LocalPlayer = GameObject.Find("Player");
		this.gui_style = new GUIStyle();
		this.gui_style.font = FontManager.font[0];
		this.gui_style.fontSize = 20;
		this.gui_style.normal.textColor = new Color(0f, 0f, 0f, 1f);
		this.gui_style.alignment = 4;
		this.black = new Texture2D(1, 1);
		this.black.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.5f));
		this.black.Apply();
		this.tmpmsg = new string[4];
		this.tmpmsg[0] = Lang.GetLabel(225);
		this.tmpmsg[1] = Lang.GetLabel(226);
		this.tmpmsg[2] = Lang.GetLabel(348);
		this.tmpmsg[3] = Lang.GetLabel(349);
		this.msg[0] = this.tmpmsg;
		this.tmpmsg = new string[0];
		this.msg[1] = this.tmpmsg;
		this.tmpmsg = new string[4];
		this.tmpmsg[0] = Lang.GetLabel(225);
		this.tmpmsg[1] = Lang.GetLabel(226);
		this.tmpmsg[2] = Lang.GetLabel(350);
		this.tmpmsg[3] = Lang.GetLabel(351);
		this.msg[3] = this.tmpmsg;
		this.tmpmsg = new string[5];
		this.tmpmsg[0] = Lang.GetLabel(225);
		this.tmpmsg[1] = Lang.GetLabel(226);
		this.tmpmsg[2] = Lang.GetLabel(352);
		this.tmpmsg[3] = Lang.GetLabel(353);
		this.tmpmsg[4] = Lang.GetLabel(354);
		this.msg[4] = this.tmpmsg;
		this.tmpmsg = new string[4];
		this.tmpmsg[0] = Lang.GetLabel(225);
		this.tmpmsg[1] = Lang.GetLabel(226);
		this.tmpmsg[2] = Lang.GetLabel(355);
		this.tmpmsg[3] = Lang.GetLabel(356);
		this.msg[5] = this.tmpmsg;
		this.tmpmsg = new string[4];
		this.tmpmsg[0] = Lang.GetLabel(225);
		this.tmpmsg[1] = Lang.GetLabel(226);
		this.tmpmsg[2] = Lang.GetLabel(357);
		this.tmpmsg[3] = Lang.GetLabel(358);
		this.msg[6] = this.tmpmsg;
		this.tmpmsg = new string[4];
		this.tmpmsg[0] = Lang.GetLabel(225);
		this.tmpmsg[1] = Lang.GetLabel(226);
		this.tmpmsg[2] = Lang.GetLabel(359);
		this.tmpmsg[3] = Lang.GetLabel(360);
		this.msg[7] = this.tmpmsg;
		this.tmpmsg = new string[4];
		this.tmpmsg[0] = Lang.GetLabel(225);
		this.tmpmsg[1] = Lang.GetLabel(226);
		this.tmpmsg[2] = Lang.GetLabel(361);
		this.tmpmsg[3] = Lang.GetLabel(362);
		this.msg[11] = this.tmpmsg;
		this.tmpmsg = new string[2];
		this.tmpmsg[0] = Lang.GetLabel(225);
		this.tmpmsg[1] = Lang.GetLabel(226);
		this.msg[100] = this.tmpmsg;
		this.msg[9] = this.tmpmsg;
		this.initialized = false;
	}

	// Token: 0x0600039D RID: 925 RVA: 0x00044F8C File Offset: 0x0004318C
	private void OnGUI()
	{
		if (!this.initialized)
		{
			if (GM.currMainState != GAME_STATES.CONNECTING)
			{
				GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), GUI3.blackTex);
				return;
			}
			this.Init();
		}
		if (this.need_rename)
		{
			if (this.progress == 0)
			{
				this.loadtext = Lang.GetLabel(152) + Lang.GetLabel(490);
			}
			else if (this.progress == 1)
			{
				this.loadtext = Lang.GetLabel(152) + Lang.GetLabel(491);
			}
			else if (this.progress == 2)
			{
				this.loadtext = Lang.GetLabel(152) + Lang.GetLabel(492);
			}
			else if (this.progress == 3)
			{
				this.loadtext = Lang.GetLabel(152) + Lang.GetLabel(493);
			}
			else if (this.progress == 4)
			{
				this.loadtext = Lang.GetLabel(152) + Lang.GetLabel(494);
			}
			else if (this.progress == 5)
			{
				this.loadtext = Lang.GetLabel(152);
			}
		}
		GUI.depth = -2;
		Rect rect;
		rect..ctor(0f, 0f, (float)(Screen.height * 2), (float)Screen.height);
		rect.center = new Vector2((float)Screen.width, (float)Screen.height) / 2f;
		Rect rect2;
		rect2..ctor(this.x1, 0f, 1f, 1f);
		Rect rect3;
		rect3..ctor(this.x2, 0f, 1f, 1f);
		GUI.DrawTexture(rect, this.background[0]);
		GUI.DrawTextureWithTexCoords(rect, this.background[1], rect2);
		GUI.DrawTexture(rect, this.background[2]);
		GUI.DrawTextureWithTexCoords(rect, this.background[3], rect3);
		this.x1 += 0.02f * Time.deltaTime;
		this.x2 -= 0.015f * Time.deltaTime;
		GUI.Label(new Rect((float)Screen.width / 2f - 100f, (float)Screen.height * 0.8f - 30f, 200f, 32f), this.loadtext, this.gui_style);
		int num = (int)((float)Screen.width / 2f - 86f);
		int num2 = (int)((float)Screen.height * 0.8f);
		GUI.DrawTexture(new Rect((float)num, (float)num2, 172f, 20f), this.black);
		num += 2;
		num2 += 2;
		for (int i = 0; i < this.progress; i++)
		{
			GUI.DrawTexture(new Rect((float)num, (float)num2, 32f, 16f), this.bar);
			num += 34;
		}
		if (this.last_mode != ConnectionInfo.mode)
		{
			this.last_mode = ConnectionInfo.mode;
			if (ConnectionInfo.mode != CONST.CFG.BUILD_MODE && ConnectionInfo.mode != CONST.CFG.CLEAR_MODE)
			{
				this.msg_type = Random.Range(0, this.msg[ConnectionInfo.mode].Length - 1);
			}
		}
		if (ConnectionInfo.mode == CONST.CFG.SNOWBALLS_MODE)
		{
			return;
		}
		num = 40;
		num2 = Screen.height - 120;
		if ((float)num2 < (float)Screen.height * 0.8f + 20f)
		{
			return;
		}
		if (ConnectionInfo.mode != CONST.CFG.BUILD_MODE && ConnectionInfo.mode != CONST.CFG.CLEAR_MODE)
		{
			GUI.DrawTexture(new Rect((float)num, (float)num2, (float)(Screen.width - 80), 80f), this.black);
			GUIManager.DrawText2(new Rect((float)(num + 4), (float)num2, (float)(Screen.width - 80), 80f), Lang.GetLabel(224), 18, 0, Color.white);
			GUIManager.DrawText2(new Rect((float)num, (float)num2, (float)(Screen.width - 80), 80f), this.msg[ConnectionInfo.mode][this.msg_type], 18, 4, Color.yellow);
		}
	}

	// Token: 0x0600039E RID: 926 RVA: 0x000453B4 File Offset: 0x000435B4
	private void Init()
	{
		this.background[0] = ContentLoader.LoadTexture("layer_wall");
		this.background[1] = ContentLoader.LoadTexture("layer_back");
		if (Lang.current == 0)
		{
			this.background[2] = ContentLoader.LoadTexture("layer_load_rus");
		}
		else
		{
			this.background[2] = ContentLoader.LoadTexture("layer_load_eng");
		}
		this.background[3] = ContentLoader.LoadTexture("layer_front");
		this.initialized = true;
	}

	// Token: 0x040007AD RID: 1965
	public static LoadScreen THIS;

	// Token: 0x040007AE RID: 1966
	private bool Active;

	// Token: 0x040007AF RID: 1967
	public Texture2D[] background = new Texture2D[4];

	// Token: 0x040007B0 RID: 1968
	public Texture2D bar;

	// Token: 0x040007B1 RID: 1969
	public int progress;

	// Token: 0x040007B2 RID: 1970
	public string loadtext = "";

	// Token: 0x040007B3 RID: 1971
	private Texture2D black;

	// Token: 0x040007B4 RID: 1972
	private GUIStyle gui_style;

	// Token: 0x040007B5 RID: 1973
	private GameObject LocalPlayer;

	// Token: 0x040007B6 RID: 1974
	private Rect r;

	// Token: 0x040007B7 RID: 1975
	private float x1;

	// Token: 0x040007B8 RID: 1976
	private float x2;

	// Token: 0x040007B9 RID: 1977
	private float y1;

	// Token: 0x040007BA RID: 1978
	private float y2;

	// Token: 0x040007BB RID: 1979
	private Dictionary<int, string[]> msg = new Dictionary<int, string[]>();

	// Token: 0x040007BC RID: 1980
	private string[] tmpmsg;

	// Token: 0x040007BD RID: 1981
	private int msg_type;

	// Token: 0x040007BE RID: 1982
	private int last_mode = 50;

	// Token: 0x040007BF RID: 1983
	public bool need_rename = true;

	// Token: 0x040007C0 RID: 1984
	private bool initialized;
}
