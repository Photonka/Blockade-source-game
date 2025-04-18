﻿using System;
using UnityEngine;

// Token: 0x0200008F RID: 143
public class New_Slots : MonoBehaviour
{
	// Token: 0x06000438 RID: 1080 RVA: 0x00056200 File Offset: 0x00054400
	private void Start()
	{
		this.EM = base.GetComponent<E_Menu>();
		this.MGUI = (MainGUI)Object.FindObjectOfType(typeof(MainGUI));
		this.Background = GUI3.GetTexture2D(Color.black, 60f);
		this.Name_Background = GUI3.GetTexture2D(Color.black, 35f);
		this.Background_Active = GUI3.GetTexture2D(new Color(1f, 0.4f, 0f), 100f);
		this.Active = new bool[10];
		this.Hover = new bool[10];
		this.Names = new string[10];
		this.Active[0] = true;
		this.Active[1] = true;
		this.Active[2] = true;
		this.Active[3] = true;
		this.Active[4] = false;
		this.Active[5] = true;
		this.Active[6] = true;
		this.Active[7] = true;
		this.Active[8] = true;
		this.Active[9] = true;
		this.Hover[0] = false;
		this.Hover[1] = false;
		this.Hover[2] = false;
		this.Hover[3] = false;
		this.Hover[4] = false;
		this.Hover[5] = false;
		this.Hover[6] = false;
		this.Hover[7] = false;
		this.Hover[8] = false;
		this.Hover[9] = false;
		this.Names[0] = "1";
		this.Names[1] = "2";
		this.Names[2] = "3";
		this.Names[3] = "4";
		this.Names[4] = "5";
		this.Names[5] = "";
		this.Names[6] = "Q";
		this.Names[7] = "";
		this.Names[8] = "G";
		this.Names[9] = "H";
		this.gui_style = new GUIStyle();
		this.gui_style.font = (Resources.Load("NewMenu/E_Menu_Font4") as Font);
		this.gui_style.normal.textColor = Color.white;
		Map map = (Map)Object.FindObjectOfType(typeof(Map));
		this.blockSet = map.GetBlockSet();
		this.teamblock[0] = this.blockSet.GetBlock("Brick_blue");
		this.teamblock[1] = this.blockSet.GetBlock("Brick_red");
		this.teamblock[2] = this.blockSet.GetBlock("Brick_green");
		this.teamblock[3] = this.blockSet.GetBlock("Brick_yellow");
		this.teamblock[4] = this.blockSet.GetBlock("ArmoredBrickBlue");
		this.teamblock[5] = this.blockSet.GetBlock("ArmoredBrickRed");
		this.teamblock[6] = this.blockSet.GetBlock("ArmoredBrickGreen");
		this.teamblock[7] = this.blockSet.GetBlock("ArmoredBrickYellow");
	}

	// Token: 0x06000439 RID: 1081 RVA: 0x00002B75 File Offset: 0x00000D75
	private void Update()
	{
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x000564FC File Offset: 0x000546FC
	public void Draw_New_Slots(int _active_slot)
	{
		if (PlayerControl.GetGameMode() == CONST.CFG.BUILD_MODE)
		{
			return;
		}
		if (BotsController.Instance.Bots[PlayerProfile.myindex].Team > 3 && !MainGUI.e_menu)
		{
			return;
		}
		if ((PlayerControl.GetGameMode() == 3 || PlayerControl.GetGameMode() == 7) && BotsController.Instance.Bots[PlayerProfile.myindex].Team > 0 && !MainGUI.e_menu)
		{
			return;
		}
		if ((!BotsController.Instance.Bots[PlayerProfile.myindex].Active || BotsController.Instance.Bots[PlayerProfile.myindex].Dead == 1) && !MainGUI.e_menu)
		{
			return;
		}
		int num = Screen.width / 2;
		int num2 = Screen.height - 116;
		int num3 = 0;
		bool[] active = this.Active;
		for (int i = 0; i < active.Length; i++)
		{
			if (active[i])
			{
				num3++;
			}
		}
		if (num3 > 0)
		{
			num -= 75 * num3 / 2;
		}
		this.Rect = new Rect((float)num, (float)num2, (float)(75 * num3), 70f);
		this.x_pos = num;
		this.y_pos = num2;
		for (int j = 0; j < this.Active.Length; j++)
		{
			if (this.Active[j])
			{
				bool slitno = false;
				bool active2 = false;
				if (j == 5 || j == 6)
				{
					slitno = true;
				}
				if (j == _active_slot)
				{
					active2 = true;
				}
				int num4 = this.x_pos;
				this.Draw_Slot(j, active2, slitno);
				if (j == 5)
				{
					this.EM.Draw_Borders(new Rect((float)num4, (float)this.y_pos, 72f, 70f), true, true, false, true);
				}
				else if (j == 6)
				{
					this.EM.Draw_Borders(new Rect((float)(num4 - 3), (float)this.y_pos, 78f, 70f), false, true, false, true);
				}
				else if (j == 7)
				{
					this.EM.Draw_Borders(new Rect((float)num4, (float)this.y_pos, 72f, 70f), false, true, true, true);
				}
				else
				{
					this.EM.Draw_Borders(new Rect((float)num4, (float)this.y_pos, 72f, 70f), true, true, true, true);
				}
			}
		}
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x0005671C File Offset: 0x0005491C
	private void Draw_Slot(int index, bool _active, bool _slitno)
	{
		int num = index - 1;
		int num2 = num - 4;
		if (num2 < 0)
		{
			num2 = 0;
		}
		if (num < 0)
		{
			num = 0;
		}
		if (num > 4)
		{
			num = 4;
		}
		int num3 = 72;
		int num4 = 0;
		if (_slitno)
		{
			num4 = 3;
		}
		if (_slitno)
		{
			num3 += 3;
		}
		GUI.DrawTexture(new Rect((float)this.x_pos, (float)this.y_pos, (float)num3, 70f), this.Background);
		if (_active)
		{
			GUI.DrawTexture(new Rect((float)this.x_pos, (float)this.y_pos, (float)(num3 - num4), 70f), this.Background_Active);
		}
		GUI.DrawTexture(new Rect((float)this.x_pos, (float)this.y_pos, (float)num3, 18f), this.Name_Background);
		this.gui_style.fontSize = 14;
		this.gui_style.alignment = 1;
		this.gui_style.normal.textColor = Color.black;
		this.SetPadding(2, 0, 0, 0);
		GUI.Label(new Rect((float)(this.x_pos + 1), (float)(this.y_pos + 1), (float)num3, 18f), this.Names[index], this.gui_style);
		this.gui_style.normal.textColor = Color.white;
		GUI.Label(new Rect((float)this.x_pos, (float)this.y_pos, (float)num3, 18f), this.Names[index], this.gui_style);
		this.SetPadding(0, 0, 0, 0);
		if (index > 0)
		{
			if (this.EM.SelectedItem[num][num2] != 0 && ItemsDB.CheckItem(this.EM.SelectedItem[num][num2]))
			{
				GUI.DrawTexture(new Rect((float)(this.x_pos + 3), (float)(this.y_pos + 18), (float)(num3 - num4 - 6), (float)((num3 - num4 - 6) / 2)), ItemsDB.Items[this.EM.SelectedItem[num][num2]].icon);
				this.gui_style.fontSize = 9;
				this.gui_style.alignment = 7;
				this.gui_style.normal.textColor = Color.black;
				this.SetPadding(0, 0, 3, -1);
				Rect rect = new Rect((float)(this.x_pos + 1), (float)(this.y_pos + 1), (float)num3, 70f);
				ITEM item = (ITEM)this.EM.SelectedItem[num][num2];
				GUI.Label(rect, item.ToString(), this.gui_style);
				this.gui_style.normal.textColor = Color.white;
				Rect rect2 = new Rect((float)this.x_pos, (float)this.y_pos, (float)num3, 70f);
				item = (ITEM)this.EM.SelectedItem[num][num2];
				GUI.Label(rect2, item.ToString(), this.gui_style);
				this.SetPadding(0, 0, 0, 0);
			}
		}
		else
		{
			int num5 = 0;
			if (BotsController.Instance.Bots[PlayerProfile.myindex].Item[198] > 0)
			{
				num5 = 4;
			}
			if ((int)BotsController.Instance.Bots[PlayerProfile.myindex].Team < CONST.TEAMS.TEAM_YELLOW + 1)
			{
				this.teamblock[(int)BotsController.Instance.Bots[PlayerProfile.myindex].Team + num5].DrawPreview(new Rect((float)(this.x_pos + 3 + (num3 - num4 - 6) / 2 / 2), (float)(this.y_pos + 18), (float)((num3 - num4 - 6) / 2), (float)((num3 - num4 - 6) / 2)));
			}
			this.gui_style.fontSize = 9;
			this.gui_style.alignment = 7;
			this.gui_style.normal.textColor = Color.black;
			this.SetPadding(0, 0, 3, -1);
			GUI.Label(new Rect((float)(this.x_pos + 1), (float)(this.y_pos + 1), (float)num3, 70f), "БЛОК", this.gui_style);
			this.gui_style.normal.textColor = Color.white;
			GUI.Label(new Rect((float)this.x_pos, (float)this.y_pos, (float)num3, 70f), "БЛОК", this.gui_style);
			this.SetPadding(0, 0, 0, 0);
		}
		this.x_pos += 75;
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x00056B48 File Offset: 0x00054D48
	private void SetPadding(int _top, int _right, int _bottom, int _left)
	{
		this.gui_style.padding.top = _top;
		this.gui_style.padding.left = _left;
		this.gui_style.padding.bottom = _bottom;
		this.gui_style.padding.right = _right;
	}

	// Token: 0x040008B6 RID: 2230
	private E_Menu EM;

	// Token: 0x040008B7 RID: 2231
	private GUIStyle gui_style;

	// Token: 0x040008B8 RID: 2232
	public Texture Background;

	// Token: 0x040008B9 RID: 2233
	public Texture Background_Hover;

	// Token: 0x040008BA RID: 2234
	public Texture Background_Active;

	// Token: 0x040008BB RID: 2235
	public Texture Name_Background;

	// Token: 0x040008BC RID: 2236
	public Texture2D Border;

	// Token: 0x040008BD RID: 2237
	public Rect Rect;

	// Token: 0x040008BE RID: 2238
	public bool[] Active;

	// Token: 0x040008BF RID: 2239
	public bool[] Hover;

	// Token: 0x040008C0 RID: 2240
	public string[] Names;

	// Token: 0x040008C1 RID: 2241
	private int x = Screen.width / 2 - 300;

	// Token: 0x040008C2 RID: 2242
	private int y = Screen.height / 2 - 190;

	// Token: 0x040008C3 RID: 2243
	private int Inactive_Tabs_Space_X;

	// Token: 0x040008C4 RID: 2244
	private int E_Menu_Active_Tab_Index;

	// Token: 0x040008C5 RID: 2245
	private int E_Menu_Active_Item_PodIndex;

	// Token: 0x040008C6 RID: 2246
	private MainGUI MGUI;

	// Token: 0x040008C7 RID: 2247
	private int x_pos;

	// Token: 0x040008C8 RID: 2248
	private int y_pos;

	// Token: 0x040008C9 RID: 2249
	private BlockSet blockSet;

	// Token: 0x040008CA RID: 2250
	private Block[] teamblock = new Block[8];

	// Token: 0x040008CB RID: 2251
	private Vector2 mousePos;
}
