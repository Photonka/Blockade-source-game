using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200008B RID: 139
public class E_Menu : MonoBehaviour
{
	// Token: 0x060003F4 RID: 1012 RVA: 0x0004F404 File Offset: 0x0004D604
	public void Start()
	{
		this.Tabs = new Texture[6];
		this.Tabs_Rects = new Rect[6];
		this.Title_Background = GUI3.GetTexture2D(Color.black, 100f);
		this.Tab_Background = GUI3.GetTexture2D(Color.black, 75f);
		this.Active_Tab_Background = GUI3.GetTexture2D(Color.black, 60f);
		this.Tab_Background_Hover = GUI3.GetTexture2D(new Color(1f, 0.4f, 0f), 100f);
		this.Tab_Background_Active = GUI3.GetTexture2D(new Color(0.38f, 0.38f, 0.38f), 75f);
		this.Item_Background = GUI3.GetTexture2D(Color.gray, 35f);
		this.Item_Name_Background = GUI3.GetTexture2D(Color.black, 35f);
		this.Category_Background = GUI3.GetTexture2D(new Color(0.38f, 0.38f, 0.38f), 75f);
		this.Item_Background_Hover = GUI3.GetTexture2D(new Color(1f, 0.4f, 0f), 100f);
		this.Play_Button_Normal = (Resources.Load("NewMenu/Play_Button_Normal") as Texture);
		this.Play_Button_Hover = (Resources.Load("NewMenu/Play_Button_Hover") as Texture);
		this.Item_Bar_Red = (Resources.Load("NewMenu/Item_Bar_Red") as Texture);
		this.Item_Bar_Blue = (Resources.Load("NewMenu/Item_Bar_Blue") as Texture);
		this.Item_Bar_Sharp = (Resources.Load("NewMenu/Item_Bar_Sharp") as Texture);
		this.Tabs[0] = (Resources.Load("NewMenu/E_Menu_Tab_Melee") as Texture);
		this.Tabs[1] = (Resources.Load("NewMenu/E_Menu_Tab_Primary") as Texture);
		this.Tabs[2] = (Resources.Load("NewMenu/E_Menu_Tab_Secondary") as Texture);
		this.Tabs[3] = (Resources.Load("NewMenu/E_Menu_Tab_Equip") as Texture);
		this.Tabs[4] = (Resources.Load("NewMenu/E_Menu_Tab_Equip") as Texture);
		this.Tabs[5] = (Resources.Load("NewMenu/E_Menu_Tab_Vehicles") as Texture);
		this.Border = GUI3.GetTexture2D(Color.black, 100f);
		this.TabsActive = new bool[6];
		this.TabsHover = new bool[6];
		this.SelectedItem.Add(0, new int[1]);
		this.SelectedItem.Add(1, new int[1]);
		this.SelectedItem.Add(2, new int[1]);
		this.SelectedItem.Add(3, new int[1]);
		this.SelectedItem.Add(4, new int[5]);
		this.SelectedItem.Add(5, new int[1]);
		this.TabsActive[0] = true;
		this.TabsActive[1] = true;
		this.TabsActive[2] = true;
		this.TabsActive[3] = false;
		this.TabsActive[4] = true;
		if (PlayerControl.GetGameMode() == 11)
		{
			this.TabsActive[5] = true;
		}
		else
		{
			this.TabsActive[5] = false;
		}
		this.TabsHover[0] = false;
		this.TabsHover[1] = false;
		this.TabsHover[2] = false;
		this.TabsHover[3] = false;
		this.TabsHover[4] = false;
		this.TabsHover[5] = false;
		this.Active_Item_PodIndex = 0;
		this.SelectedItem[0][0] = 33;
		this.SelectedItem[1][0] = 44;
		this.SelectedItem[2][0] = 46;
		this.SelectedItem[3][0] = 0;
		this.SelectedItem[4][0] = 0;
		this.SelectedItem[4][1] = 0;
		this.SelectedItem[4][2] = 0;
		this.SelectedItem[4][3] = 0;
		this.SelectedItem[4][4] = 0;
		this.SelectedItem[5][0] = 0;
		this.Inactive_Tabs_Space_X = this.x;
		this.TabsNames = new string[6];
		this.WeaponBarsNames = new string[6];
		this.VehicleBarsNames = new string[5];
		this.WeaponsCategoryNames = new string[14];
		this.TabsNames[0] = Lang.GetLabel(410);
		this.TabsNames[1] = Lang.GetLabel(411);
		this.TabsNames[2] = Lang.GetLabel(412);
		this.TabsNames[3] = Lang.GetLabel(413);
		this.TabsNames[4] = Lang.GetLabel(413);
		this.TabsNames[5] = Lang.GetLabel(414);
		this.WeaponBarsNames[1] = Lang.GetLabel(415);
		this.WeaponBarsNames[2] = Lang.GetLabel(416);
		this.WeaponBarsNames[3] = Lang.GetLabel(417);
		this.WeaponBarsNames[4] = Lang.GetLabel(418);
		this.WeaponBarsNames[5] = Lang.GetLabel(419);
		this.VehicleBarsNames[0] = Lang.GetLabel(420);
		this.VehicleBarsNames[1] = Lang.GetLabel(421);
		this.VehicleBarsNames[2] = Lang.GetLabel(422);
		this.VehicleBarsNames[3] = Lang.GetLabel(423);
		this.VehicleBarsNames[4] = Lang.GetLabel(424);
		for (int i = 1; i < 14; i++)
		{
			this.WeaponsCategoryNames[i] = Lang.GetLabel(575 + i);
		}
		this.gui_style = new GUIStyle();
		this.gui_style.font = ContentLoader.LoadFont("E_Menu_Font4");
		this.gui_style.normal.textColor = Color.white;
		this.Init();
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x0004F988 File Offset: 0x0004DB88
	public void Init()
	{
		if (PlayerPrefs.HasKey("PWID"))
		{
			this.SelectedItem[1][0] = this.GetWeaponByID(PlayerPrefs.GetInt("PWID"), this.SelectedItem[1][0]);
		}
		if (PlayerPrefs.HasKey("SWID"))
		{
			this.SelectedItem[2][0] = this.GetWeaponByID(PlayerPrefs.GetInt("SWID"), this.SelectedItem[2][0]);
		}
		if (PlayerPrefs.HasKey("MWID"))
		{
			this.SelectedItem[0][0] = this.GetWeaponByID(PlayerPrefs.GetInt("MWID"), this.SelectedItem[0][0]);
		}
		if (PlayerPrefs.HasKey("A1WID"))
		{
			this.SelectedItem[4][0] = this.GetWeaponByID(PlayerPrefs.GetInt("A1WID"), this.SelectedItem[4][0]);
		}
		if (PlayerPrefs.HasKey("A2WID"))
		{
			this.SelectedItem[4][1] = this.GetWeaponByID(PlayerPrefs.GetInt("A2WID"), this.SelectedItem[4][1]);
		}
		if (PlayerPrefs.HasKey("A3WID"))
		{
			this.SelectedItem[4][2] = this.GetWeaponByID(PlayerPrefs.GetInt("A3WID"), this.SelectedItem[4][2]);
		}
		if (PlayerPrefs.HasKey("G1WID"))
		{
			this.SelectedItem[4][3] = this.GetWeaponByID(PlayerPrefs.GetInt("G1WID"), this.SelectedItem[4][3]);
		}
		if (PlayerPrefs.HasKey("G2WID"))
		{
			this.SelectedItem[4][4] = this.GetWeaponByID(PlayerPrefs.GetInt("G2WID"), this.SelectedItem[4][4]);
		}
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x0004FB58 File Offset: 0x0004DD58
	public void Draw_E_Menu()
	{
		this.TabsActive[0] = true;
		this.TabsActive[1] = true;
		this.TabsActive[2] = true;
		this.TabsActive[3] = false;
		this.TabsActive[4] = true;
		if (PlayerControl.GetGameMode() == 11)
		{
			this.TabsActive[5] = true;
		}
		else
		{
			this.TabsActive[5] = false;
		}
		this.Play_Button_Rect = new Rect((float)(Screen.width / 2 - 86), 50f, 172f, 42f);
		this.x = Screen.width / 2 - 300;
		this.y = Screen.height / 2 - (Screen.height - 220) / 2;
		int num = Screen.height - 390;
		if (num < 60)
		{
			num = 60;
		}
		this.Title_Background_Rect = new Rect((float)this.x, (float)this.y, 600f, 22f);
		this.Tabs_Rect = new Rect((float)this.x, (float)(this.y + 22), 600f, 50f);
		this.Active_Tab_Rect = new Rect((float)this.x, (float)(this.y + 72), 600f, (float)num);
		this.Active_Item_Rect = new Rect((float)this.x, (float)(this.y + num + 72), 600f, 80f);
		this.Active_Item_Rect1 = new Rect((float)(this.x + 3), this.Active_Item_Rect.y + 3f, 194f, 74f);
		if (this.Active_Tab_Index == 1 || this.Active_Tab_Index == 2 || this.Active_Tab_Index == 5)
		{
			this.Active_Item_Rect2 = new Rect((float)(this.x + 200), this.Active_Item_Rect.y, 200f, 80f);
			this.Active_Item_Rect3 = new Rect((float)(this.x + 400), this.Active_Item_Rect.y, 200f, 80f);
		}
		else
		{
			this.Active_Item_Rect2 = new Rect(0f, 0f, 0f, 0f);
			this.Active_Item_Rect3 = new Rect((float)(this.x + 200), this.Active_Item_Rect.y, 400f, 80f);
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
		this.Tabs_Rects = new Rect[6];
		int num2 = this.x;
		int num3 = this.y + 22;
		int num4 = 120;
		int num5 = 600;
		for (int i = 0; i < this.TabsActive.Length; i++)
		{
			if (this.TabsActive[i])
			{
				this.Tabs_Rects[i] = new Rect((float)num2, (float)num3, (float)num4, (float)this.Tabs[i].height);
				if (this.Tabs_Rects[i].Contains(this.mousePos))
				{
					this.TabsHover[i] = true;
				}
				else
				{
					this.TabsHover[i] = false;
				}
				num2 += num4;
				num5 -= num4;
			}
		}
		this.Draw_Background();
		this.Draw_Play_Button();
		this.Draw_Tabs();
		this.Draw_Active_Tab();
		if (this.Active_Tab_Index == 5)
		{
			this.Draw_Active_Vehicle();
		}
		else
		{
			this.Draw_Active_Item();
		}
		this.Draw_Borders(this.Title_Background_Rect, true, true, true, true);
		this.Draw_Borders(this.Tabs_Rect, true, false, true, false);
		this.Draw_Borders(this.Active_Tab_Rect, true, false, true, false);
		this.Draw_Borders(this.Active_Item_Rect, true, true, true, true);
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x0004FF00 File Offset: 0x0004E100
	public void Draw_Background()
	{
		GUI.DrawTexture(this.Title_Background_Rect, this.Title_Background);
		GUI.DrawTexture(this.Tabs_Rect, this.Tab_Background);
		GUI.DrawTexture(this.Active_Tab_Rect, this.Active_Tab_Background);
		GUI.DrawTexture(this.Active_Item_Rect, this.Tab_Background);
		this.gui_style.fontSize = 18;
		this.gui_style.alignment = 4;
		this.gui_style.normal.textColor = Color.black;
		this.gui_style.padding.bottom = 0;
		GUI.Label(new Rect(this.Title_Background_Rect.x + 1f, this.Title_Background_Rect.y + 1f, this.Title_Background_Rect.width, 22f), Lang.GetLabel(428), this.gui_style);
		this.gui_style.normal.textColor = Color.yellow;
		GUI.Label(this.Title_Background_Rect, Lang.GetLabel(428), this.gui_style);
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x0005000C File Offset: 0x0004E20C
	public void Draw_Borders(Rect _rect, bool left, bool top, bool right, bool bottom)
	{
		if (left)
		{
			GUI.DrawTexture(new Rect(_rect.x, _rect.y, 2f, _rect.height), this.Border);
		}
		if (top)
		{
			GUI.DrawTexture(new Rect(_rect.x, _rect.y, _rect.width, 2f), this.Border);
		}
		if (right)
		{
			GUI.DrawTexture(new Rect(_rect.xMax - 2f, _rect.y, 2f, _rect.height), this.Border);
		}
		if (bottom)
		{
			GUI.DrawTexture(new Rect(_rect.x, _rect.yMax - 2f, _rect.width, 2f), this.Border);
		}
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x000500DC File Offset: 0x0004E2DC
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
		GUI.Label(new Rect(this.Play_Button_Rect.x + 2f, this.Play_Button_Rect.y + 2f, this.Play_Button_Rect.width, this.Play_Button_Rect.height), Lang.GetLabel(429), this.gui_style);
		this.gui_style.normal.textColor = Color.white;
		GUI.Label(this.Play_Button_Rect, Lang.GetLabel(429), this.gui_style);
		if (GUI.Button(this.Play_Button_Rect, "", this.gui_style))
		{
			if (this.cscl == null)
			{
				this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
			}
			this.cscl.send_new_config(this.SelectedItem[0][0], this.SelectedItem[1][0], this.SelectedItem[2][0], this.SelectedItem[4][0], this.SelectedItem[4][1], this.SelectedItem[4][2], this.SelectedItem[4][3], this.SelectedItem[4][4]);
			if (this.SelectedItem[5][0] > 0)
			{
				this.cscl.send_spawn_my_vehicle(this.SelectedItem[5][0]);
				this.SelectedItem[5][0] = 0;
			}
		}
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x000502D0 File Offset: 0x0004E4D0
	public void Draw_Tabs()
	{
		for (int i = 0; i < this.TabsActive.Length; i++)
		{
			if (this.TabsActive[i])
			{
				if (this.Active_Tab_Index == i)
				{
					GUI.DrawTexture(this.Tabs_Rects[i], this.Tab_Background_Active);
				}
				else if (this.TabsHover[i])
				{
					GUI.DrawTexture(this.Tabs_Rects[i], this.Tab_Background_Hover);
					if (GUI.Button(this.Tabs_Rects[i], "", this.gui_style))
					{
						this.Active_Tab_Index = i;
						this.Active_Item_PodIndex = 0;
					}
				}
				GUI.DrawTexture(this.Tabs_Rects[i], this.Tabs[i]);
				this.gui_style.fontSize = 12;
				this.gui_style.alignment = 7;
				this.gui_style.padding.bottom = 3;
				this.gui_style.normal.textColor = Color.black;
				GUI.Label(new Rect(this.Tabs_Rects[i].x + 1f, this.Tabs_Rects[i].y + 1f, this.Tabs_Rects[i].width, this.Tabs_Rects[i].height), this.TabsNames[i], this.gui_style);
				this.gui_style.normal.textColor = Color.white;
				GUI.Label(this.Tabs_Rects[i], this.TabsNames[i], this.gui_style);
			}
			if (this.Active_Tab_Index == i)
			{
				this.Draw_Borders(this.Tabs_Rects[i], true, false, true, false);
			}
			else
			{
				this.Draw_Borders(this.Tabs_Rects[i], false, false, false, true);
			}
		}
		GUI.DrawTexture(this.Inactive_Space_Rect, this.Tab_Background);
		this.Draw_Borders(this.Inactive_Space_Rect, false, false, false, true);
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x000504C0 File Offset: 0x0004E6C0
	public void Draw_Active_Tab()
	{
		this.ResetPos();
		this.scrollViewVector = GUI3.BeginScrollView(this.Active_Tab_Rect, this.scrollViewVector, new Rect(0f, 0f, 0f, this.sh), false);
		switch (this.Active_Tab_Index)
		{
		case 0:
			this.Draw_Melee();
			goto IL_8D;
		case 1:
			this.Draw_Primary();
			goto IL_8D;
		case 2:
			this.Draw_Secondary();
			goto IL_8D;
		case 4:
			this.Draw_Ammunition();
			goto IL_8D;
		case 5:
			this.Draw_Vehicles();
			goto IL_8D;
		}
		this.Draw_Melee();
		IL_8D:
		GUI3.EndScrollView();
		this.sh = (float)this.y_pos;
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x0005056C File Offset: 0x0004E76C
	public void Draw_Melee()
	{
		if (ItemsDB.Items.Length == 0)
		{
			return;
		}
		this.DrawCategory(Lang.GetLabel(524), 5, true);
		for (int i = 0; i < 500; i++)
		{
			if (ItemsDB.CheckItem(i) && ItemsDB.Items[i].Category == 7 && ItemsDB.Items[i].ItemID != 35 && (i <= 0 || BotsController.Instance.Bots[PlayerProfile.myindex].Item[i] != 0))
			{
				this.DrawItem(i, 0);
				this.NextPos(false);
			}
		}
		this.NextPos(true);
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x00050600 File Offset: 0x0004E800
	public void Draw_Primary()
	{
		if (ItemsDB.Items.Length == 0)
		{
			return;
		}
		for (int i = 2; i < 9; i++)
		{
			if (i != 7)
			{
				this.DrawCategory(this.WeaponsCategoryNames[i], 5, true);
				for (int j = 0; j < 500; j++)
				{
					if (ItemsDB.CheckItem(j) && ItemsDB.Items[j].Category == i && BotsController.Instance.Bots[PlayerProfile.myindex].Item[j] != 0)
					{
						this.DrawItem(j, 0);
						this.NextPos(false);
					}
				}
				this.NextPos(true);
			}
		}
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x00050690 File Offset: 0x0004E890
	public void Draw_Secondary()
	{
		if (ItemsDB.Items.Length == 0)
		{
			return;
		}
		this.DrawCategory(Lang.GetLabel(525), 5, true);
		for (int i = -10; i < 500; i++)
		{
			if (ItemsDB.CheckItem(i) && ItemsDB.Items[i].Category == 1 && (i <= 0 || BotsController.Instance.Bots[PlayerProfile.myindex].Item[i] != 0))
			{
				this.DrawItem(i, 0);
				this.NextPos(false);
			}
		}
		this.NextPos(true);
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x00050714 File Offset: 0x0004E914
	public void Draw_Ammunition()
	{
		if (ItemsDB.Items.Length == 0)
		{
			return;
		}
		this.DrawCategory(Lang.GetLabel(430), 1, false);
		this.DrawCategory(Lang.GetLabel(431), 1, false);
		this.DrawCategory(Lang.GetLabel(432), 1, false);
		this.DrawCategory(Lang.GetLabel(433), 2, true);
		int val = this.y_pos;
		if (BotsController.Instance.Bots[PlayerProfile.myindex].Item[62] != 0)
		{
			this.DrawItem(62, 0);
			this.NextYPos(false);
			val = Math.Max(val, this.y_pos);
		}
		if (BotsController.Instance.Bots[PlayerProfile.myindex].Item[10] != 0)
		{
			this.DrawItem(10, 0);
			this.NextYPos(false);
			val = Math.Max(val, this.y_pos);
		}
		if (BotsController.Instance.Bots[PlayerProfile.myindex].Item[185] != 0)
		{
			this.DrawItem(185, 0);
			this.NextYPos(false);
			val = Math.Max(val, this.y_pos);
		}
		this.NextYPos(true);
		if (BotsController.Instance.Bots[PlayerProfile.myindex].Item[55] != 0)
		{
			this.DrawItem(55, 1);
			this.NextYPos(false);
			val = Math.Max(val, this.y_pos);
		}
		if (BotsController.Instance.Bots[PlayerProfile.myindex].Item[171] != 0)
		{
			this.DrawItem(171, 1);
			this.NextYPos(false);
			val = Math.Max(val, this.y_pos);
		}
		if (BotsController.Instance.Bots[PlayerProfile.myindex].Item[172] != 0)
		{
			this.DrawItem(172, 1);
			this.NextYPos(false);
			val = Math.Max(val, this.y_pos);
		}
		if (BotsController.Instance.Bots[PlayerProfile.myindex].Item[183] != 0)
		{
			this.DrawItem(183, 1);
			this.NextYPos(false);
			val = Math.Max(val, this.y_pos);
		}
		this.NextYPos(true);
		if (BotsController.Instance.Bots[PlayerProfile.myindex].Item[138] != 0)
		{
			this.DrawItem(138, 2);
			this.NextYPos(false);
			val = Math.Max(val, this.y_pos);
		}
		if (BotsController.Instance.Bots[PlayerProfile.myindex].Item[100] != 0)
		{
			this.DrawItem(100, 2);
			this.NextYPos(false);
			val = Math.Max(val, this.y_pos);
		}
		this.NextYPos(true);
		if (BotsController.Instance.Bots[PlayerProfile.myindex].Item[7] != 0)
		{
			this.DrawItem(7, 3);
			this.NextYPos(false);
			val = Math.Max(val, this.y_pos);
		}
		if (BotsController.Instance.Bots[PlayerProfile.myindex].Item[168] != 0)
		{
			this.DrawItem(168, 3);
			this.NextYPos(false);
			val = Math.Max(val, this.y_pos);
		}
		if (BotsController.Instance.Bots[PlayerProfile.myindex].Item[170] != 0)
		{
			this.DrawItem(170, 3);
			this.NextYPos(false);
			val = Math.Max(val, this.y_pos);
		}
		this.NextYPos(true);
		if (BotsController.Instance.Bots[PlayerProfile.myindex].Item[169] != 0)
		{
			this.DrawItem(169, 4);
			this.NextYPos(false);
			val = Math.Max(val, this.y_pos);
		}
		if (BotsController.Instance.Bots[PlayerProfile.myindex].Item[184] != 0)
		{
			this.DrawItem(184, 4);
			this.NextYPos(false);
			val = Math.Max(val, this.y_pos);
		}
		if (BotsController.Instance.Bots[PlayerProfile.myindex].Item[186] != 0)
		{
			this.DrawItem(186, 4);
			this.NextYPos(false);
			val = Math.Max(val, this.y_pos);
		}
		this.NextYPos(true);
		this.y_pos = val;
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x00050B18 File Offset: 0x0004ED18
	public void Draw_Vehicles()
	{
		this.DrawCategory(Lang.GetLabel(434), 5, true);
		for (int i = 200; i < 204; i++)
		{
			this.DrawVehicle(i, 0);
			this.NextPos(false);
		}
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x00050B5C File Offset: 0x0004ED5C
	private void NextPos(bool end = false)
	{
		if (end)
		{
			if (this.icount != 0 && this.icount != 5)
			{
				this.y_pos += 59;
			}
			this.x_pos = 2;
			return;
		}
		this.x_pos += 117;
		this.icount++;
		if (this.icount == 5)
		{
			this.icount = 0;
			this.x_pos = 2;
			this.y_pos += 59;
		}
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x00050BD7 File Offset: 0x0004EDD7
	private void NextYPos(bool end = false)
	{
		if (end)
		{
			this.x_pos += 117;
			this.y_pos = 17;
			return;
		}
		this.y_pos += 59;
	}

	// Token: 0x06000403 RID: 1027 RVA: 0x00050C03 File Offset: 0x0004EE03
	private void ResetPos()
	{
		this.x_pos = 2;
		this.y_pos = 0;
		this.icount = 0;
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x00050C1C File Offset: 0x0004EE1C
	private void DrawCategory(string _text, int _cols, bool _ending)
	{
		GUI.DrawTexture(new Rect((float)this.x_pos, (float)this.y_pos, (float)(116 * _cols + _cols - 1), 16f), this.Category_Background);
		this.gui_style.fontSize = 12;
		this.gui_style.alignment = 4;
		this.gui_style.normal.textColor = Color.black;
		this.gui_style.padding.bottom = 0;
		GUI.Label(new Rect((float)this.x_pos, (float)(this.y_pos + 1), (float)(116 * _cols + _cols - 1), 16f), _text, this.gui_style);
		this.gui_style.normal.textColor = Color.white;
		GUI.Label(new Rect((float)this.x_pos, (float)this.y_pos, (float)(116 * _cols + _cols - 1), 16f), _text, this.gui_style);
		this.x_pos += 117 * _cols;
		if (_ending)
		{
			this.y_pos += 17;
			this.x_pos = 2;
			this.icount = 0;
		}
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x00050D38 File Offset: 0x0004EF38
	private void DrawItem(int index, int _Active_Item_PodIndex)
	{
		Rect rect;
		rect..ctor((float)this.x_pos, (float)this.y_pos, 116f, 58f);
		Rect rect2 = new Rect((float)this.x_pos, (float)this.y_pos, 116f, 15f);
		if (this.SelectedItem[this.Active_Tab_Index][_Active_Item_PodIndex] == index || (rect.Contains(Event.current.mousePosition) && this.Active_Tab_Rect.Contains(this.mousePos)))
		{
			GUI.DrawTexture(rect, this.Item_Background_Hover);
		}
		else
		{
			GUI.DrawTexture(rect, this.Item_Background);
		}
		GUI.DrawTexture(rect2, this.Item_Name_Background);
		GUI.DrawTexture(rect, ItemsDB.Items[index].icon);
		this.gui_style.fontSize = 10;
		this.gui_style.alignment = 0;
		this.gui_style.normal.textColor = Color.black;
		this.SetPadding(2, 0, 0, 2);
		Rect rect3 = new Rect(rect.x + 1f, rect.y + 1f, rect.width, rect.height);
		ITEM item = (ITEM)index;
		GUI.Label(rect3, item.ToString(), this.gui_style);
		this.gui_style.normal.textColor = Color.white;
		Rect rect4 = rect;
		item = (ITEM)index;
		GUI.Label(rect4, item.ToString(), this.gui_style);
		this.SetPadding(0, 0, 0, 0);
		if (GUI.Button(rect, "", this.gui_style))
		{
			this.SelectedItem[this.Active_Tab_Index][_Active_Item_PodIndex] = index;
			this.Active_Item_PodIndex = _Active_Item_PodIndex;
		}
		if (this.Active_Tab_Index == 3)
		{
			this.gui_style.fontSize = 10;
			this.gui_style.alignment = 8;
			this.gui_style.normal.textColor = Color.black;
			this.SetPadding(2, 5, 2, 0);
			GUI.Label(new Rect(rect.x + 1f, rect.y + 1f, rect.width, rect.height), BotsController.Instance.Bots[PlayerProfile.myindex].Item[index].ToString(), this.gui_style);
			this.gui_style.normal.textColor = Color.white;
			GUI.Label(rect, BotsController.Instance.Bots[PlayerProfile.myindex].Item[index].ToString(), this.gui_style);
			this.SetPadding(0, 0, 0, 0);
		}
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x00050FC4 File Offset: 0x0004F1C4
	private void DrawVehicle(int index, int _Active_Item_PodIndex)
	{
		Rect rect;
		rect..ctor((float)this.x_pos, (float)this.y_pos, 116f, 58f);
		Rect rect2 = new Rect((float)this.x_pos, (float)this.y_pos, 116f, 15f);
		if (this.SelectedItem[this.Active_Tab_Index][_Active_Item_PodIndex] == index || (rect.Contains(Event.current.mousePosition) && this.Active_Tab_Rect.Contains(this.mousePos)))
		{
			GUI.DrawTexture(rect, this.Item_Background_Hover);
		}
		else
		{
			GUI.DrawTexture(rect, this.Item_Background);
		}
		GUI.DrawTexture(rect2, this.Item_Name_Background);
		this.gui_style.fontSize = 10;
		this.gui_style.alignment = 0;
		this.gui_style.normal.textColor = Color.black;
		this.SetPadding(2, 0, 0, 2);
		this.gui_style.normal.textColor = Color.white;
		this.SetPadding(0, 0, 0, 0);
		if (GUI.Button(rect, "", this.gui_style))
		{
			this.SelectedItem[this.Active_Tab_Index][_Active_Item_PodIndex] = index;
			this.Active_Item_PodIndex = _Active_Item_PodIndex;
		}
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x000510F4 File Offset: 0x0004F2F4
	public void Draw_Active_Item()
	{
		if (ItemsDB.Items.Length == 0)
		{
			return;
		}
		if (!ItemsDB.CheckItem(this.SelectedItem[this.Active_Tab_Index][this.Active_Item_PodIndex]))
		{
			return;
		}
		if (this.SelectedItem[this.Active_Tab_Index][this.Active_Item_PodIndex] == 0)
		{
			return;
		}
		GUI.DrawTexture(this.Active_Item_Rect1, this.Item_Background);
		GUI.DrawTexture(new Rect(this.Active_Item_Rect1.x, this.Active_Item_Rect1.y, this.Active_Item_Rect1.width, 18f), this.Item_Name_Background);
		GUI.DrawTexture(new Rect(this.Active_Item_Rect1.x + 10f, this.Active_Item_Rect1.y, this.Active_Item_Rect1.height * 2f, this.Active_Item_Rect1.height), ItemsDB.Items[this.SelectedItem[this.Active_Tab_Index][this.Active_Item_PodIndex]].icon);
		if ((this.Active_Tab_Index == 1 || this.Active_Tab_Index == 2) && this.SelectedItem[this.Active_Tab_Index][this.Active_Item_PodIndex] != 174 && this.SelectedItem[this.Active_Tab_Index][this.Active_Item_PodIndex] != 304)
		{
			for (int i = 1; i < 6; i++)
			{
				this.DrawBar(i);
			}
		}
		this.gui_style.fontSize = 14;
		this.gui_style.alignment = 0;
		this.gui_style.normal.textColor = Color.black;
		this.SetPadding(2, 0, 0, 10);
		Rect rect = new Rect(this.Active_Item_Rect1.x + 1f, this.Active_Item_Rect1.y + 1f, this.Active_Item_Rect1.width, this.Active_Item_Rect1.height);
		ITEM item = (ITEM)this.SelectedItem[this.Active_Tab_Index][this.Active_Item_PodIndex];
		GUI.Label(rect, item.ToString(), this.gui_style);
		this.gui_style.normal.textColor = Color.white;
		Rect active_Item_Rect = this.Active_Item_Rect1;
		item = (ITEM)this.SelectedItem[this.Active_Tab_Index][this.Active_Item_PodIndex];
		GUI.Label(active_Item_Rect, item.ToString(), this.gui_style);
		this.SetPadding(0, 0, 0, 0);
		this.gui_style.fontSize = 14;
		this.gui_style.alignment = 1;
		this.gui_style.normal.textColor = Color.black;
		this.SetPadding(2, 0, 0, 0);
		GUI.Label(new Rect(this.Active_Item_Rect3.x + 1f, this.Active_Item_Rect3.y + 1f, this.Active_Item_Rect3.width, this.Active_Item_Rect3.height), Lang.GetLabel(435), this.gui_style);
		this.gui_style.normal.textColor = Color.white;
		GUI.Label(this.Active_Item_Rect3, Lang.GetLabel(435), this.gui_style);
		this.SetPadding(0, 0, 0, 0);
		this.gui_style.wordWrap = true;
		Rect viewzone;
		viewzone..ctor(this.Active_Item_Rect3.x, this.Active_Item_Rect3.y + 20f, this.Active_Item_Rect3.width, this.Active_Item_Rect3.height - 10f);
		this.gui_style.fontSize = 12;
		this.gui_style.alignment = 1;
		this.gui_style.normal.textColor = Color.black;
		this.SetPadding(5, 5, 5, 5);
		item = (ITEM)this.SelectedItem[this.Active_Tab_Index][this.Active_Item_PodIndex];
		GUIContent guicontent = new GUIContent(item.ToString());
		float num = this.gui_style.CalcHeight(guicontent, viewzone.width - 11f);
		Rect rect2;
		rect2..ctor(0f, 0f, viewzone.width - 11f, num);
		this.scrollViewVector2 = GUI3.BeginScrollView(viewzone, this.scrollViewVector2, rect2, false);
		Rect rect3 = new Rect(rect2.x + 1f, rect2.y + 1f, rect2.width, rect2.height);
		item = (ITEM)this.SelectedItem[this.Active_Tab_Index][this.Active_Item_PodIndex];
		GUI.Label(rect3, item.ToString(), this.gui_style);
		this.gui_style.normal.textColor = Color.white;
		Rect rect4 = rect2;
		item = (ITEM)this.SelectedItem[this.Active_Tab_Index][this.Active_Item_PodIndex];
		GUI.Label(rect4, item.ToString(), this.gui_style);
		this.SetPadding(0, 0, 0, 0);
		this.gui_style.wordWrap = false;
		GUI3.EndScrollView();
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x00002B75 File Offset: 0x00000D75
	public void Draw_Active_Vehicle()
	{
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x000515DC File Offset: 0x0004F7DC
	public void DrawBar(int i)
	{
		int num = (int)this.Active_Item_Rect2.x + 3;
		int num2 = (int)this.Active_Item_Rect2.y + 4 + 14 * (i - 1);
		int num3 = (int)this.Active_Item_Rect2.xMax - 1 - (num + 1 + 90 + 80);
		int num4 = ItemsDB.Items[this.SelectedItem[this.Active_Tab_Index][this.Active_Item_PodIndex]].Upgrades[i][0].Val * 71 / ItemsDB.Items[1000].Upgrades[i][0].Val;
		float num5 = (float)ItemsDB.Items[this.SelectedItem[this.Active_Tab_Index][this.Active_Item_PodIndex]].Upgrades[i][0].Val;
		this.gui_style.fontSize = 10;
		this.gui_style.normal.textColor = Color.black;
		this.SetPadding(0, 0, 0, 0);
		this.gui_style.alignment = 3;
		GUI.Label(new Rect((float)(num + 1), (float)(num2 + 1), 80f, 14f), this.WeaponBarsNames[i], this.gui_style);
		this.gui_style.alignment = 5;
		GUI.Label(new Rect((float)(num + 1 + 90 + 80), (float)(num2 + 1), (float)num3, 14f), num5.ToString(), this.gui_style);
		this.gui_style.normal.textColor = Color.white;
		this.gui_style.alignment = 3;
		GUI.Label(new Rect((float)num, (float)num2, 80f, 14f), this.WeaponBarsNames[i], this.gui_style);
		this.gui_style.alignment = 5;
		GUI.Label(new Rect((float)(num + 1 + 90 + 80), (float)num2, (float)num3, 14f), num5.ToString(), this.gui_style);
		this.SetPadding(0, 0, 0, 0);
		num += 90;
		if (num4 < 2)
		{
			num4 = 2;
		}
		if (i == 0)
		{
			GUI.DrawTexture(new Rect((float)num, (float)(num2 + 3), (float)num4, 8f), this.Item_Bar_Red);
		}
		else
		{
			GUI.DrawTexture(new Rect((float)num, (float)(num2 + 3), (float)num4, 8f), this.Item_Bar_Blue);
		}
		for (int j = 0; j < 10; j++)
		{
			GUI.DrawTexture(new Rect((float)num, (float)(num2 + 3), 8f, 8f), this.Item_Bar_Sharp);
			num += 7;
		}
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x0005184C File Offset: 0x0004FA4C
	public void DrawVehicleBar(int i)
	{
		int num = (int)this.Active_Item_Rect2.x + 3;
		int num2 = (int)this.Active_Item_Rect2.y + 4 + 14 * i;
		int num3 = (int)this.Active_Item_Rect2.xMax - 1 - (num + 1 + 90 + 80);
		int num4 = 0;
		float num5 = 0f;
		this.gui_style.fontSize = 10;
		this.gui_style.normal.textColor = Color.black;
		this.SetPadding(0, 0, 0, 0);
		this.gui_style.alignment = 3;
		GUI.Label(new Rect((float)(num + 1), (float)(num2 + 1), 80f, 14f), this.VehicleBarsNames[i], this.gui_style);
		this.gui_style.alignment = 5;
		GUI.Label(new Rect((float)(num + 1 + 90 + 80), (float)(num2 + 1), (float)num3, 14f), num5.ToString(), this.gui_style);
		this.gui_style.normal.textColor = Color.white;
		this.gui_style.alignment = 3;
		GUI.Label(new Rect((float)num, (float)num2, 80f, 14f), this.VehicleBarsNames[i], this.gui_style);
		this.gui_style.alignment = 5;
		GUI.Label(new Rect((float)(num + 1 + 90 + 80), (float)num2, (float)num3, 14f), num5.ToString(), this.gui_style);
		this.SetPadding(0, 0, 0, 0);
		num += 90;
		if (num4 < 2)
		{
			num4 = 2;
		}
		if (i == 0)
		{
			GUI.DrawTexture(new Rect((float)num, (float)(num2 + 3), (float)num4, 8f), this.Item_Bar_Red);
		}
		else
		{
			GUI.DrawTexture(new Rect((float)num, (float)(num2 + 3), (float)num4, 8f), this.Item_Bar_Blue);
		}
		for (int j = 0; j < 10; j++)
		{
			GUI.DrawTexture(new Rect((float)num, (float)(num2 + 3), 8f, 8f), this.Item_Bar_Sharp);
			num += 7;
		}
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x00051A40 File Offset: 0x0004FC40
	private void SetPadding(int _top, int _right, int _bottom, int _left)
	{
		this.gui_style.padding.top = _top;
		this.gui_style.padding.left = _left;
		this.gui_style.padding.bottom = _bottom;
		this.gui_style.padding.right = _right;
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x00051A92 File Offset: 0x0004FC92
	private int GetWeaponByID(int newID, int oldItemID)
	{
		if (!ItemsDB.CheckItem(newID))
		{
			return oldItemID;
		}
		return newID;
	}

	// Token: 0x04000840 RID: 2112
	private GUIStyle gui_style;

	// Token: 0x04000841 RID: 2113
	private Vector2 scrollViewVector;

	// Token: 0x04000842 RID: 2114
	private Vector2 scrollViewVector2;

	// Token: 0x04000843 RID: 2115
	public Texture Title_Background;

	// Token: 0x04000844 RID: 2116
	public Texture Tab_Background;

	// Token: 0x04000845 RID: 2117
	public Texture Active_Tab_Background;

	// Token: 0x04000846 RID: 2118
	public Texture Tab_Background_Hover;

	// Token: 0x04000847 RID: 2119
	public Texture Tab_Background_Active;

	// Token: 0x04000848 RID: 2120
	public Texture Item_Background;

	// Token: 0x04000849 RID: 2121
	public Texture Item_Name_Background;

	// Token: 0x0400084A RID: 2122
	public Texture Category_Background;

	// Token: 0x0400084B RID: 2123
	public Texture Item_Background_Hover;

	// Token: 0x0400084C RID: 2124
	public Texture Item_Bar_Red;

	// Token: 0x0400084D RID: 2125
	public Texture Item_Bar_Blue;

	// Token: 0x0400084E RID: 2126
	public Texture Item_Bar_Sharp;

	// Token: 0x0400084F RID: 2127
	public Texture Play_Button_Normal;

	// Token: 0x04000850 RID: 2128
	public Texture Play_Button_Hover;

	// Token: 0x04000851 RID: 2129
	public Texture2D Border;

	// Token: 0x04000852 RID: 2130
	public Texture[] Tabs;

	// Token: 0x04000853 RID: 2131
	public Rect Title_Background_Rect;

	// Token: 0x04000854 RID: 2132
	public Rect Play_Button_Rect;

	// Token: 0x04000855 RID: 2133
	public Rect Tabs_Rect;

	// Token: 0x04000856 RID: 2134
	public Rect Active_Item_Rect;

	// Token: 0x04000857 RID: 2135
	public Rect Inactive_Space_Rect;

	// Token: 0x04000858 RID: 2136
	public Rect Active_Tab_Rect;

	// Token: 0x04000859 RID: 2137
	public Rect Active_Item_Rect1;

	// Token: 0x0400085A RID: 2138
	public Rect Active_Item_Rect2;

	// Token: 0x0400085B RID: 2139
	public Rect Active_Item_Rect3;

	// Token: 0x0400085C RID: 2140
	public Rect[] Tabs_Rects;

	// Token: 0x0400085D RID: 2141
	public bool In_Play_Button_Rect;

	// Token: 0x0400085E RID: 2142
	public bool In_Tab_Rect;

	// Token: 0x0400085F RID: 2143
	public bool[] TabsActive;

	// Token: 0x04000860 RID: 2144
	public bool[] TabsHover;

	// Token: 0x04000861 RID: 2145
	public string[] TabsNames;

	// Token: 0x04000862 RID: 2146
	public string[] WeaponBarsNames;

	// Token: 0x04000863 RID: 2147
	public string[] VehicleBarsNames;

	// Token: 0x04000864 RID: 2148
	public string[] WeaponsCategoryNames;

	// Token: 0x04000865 RID: 2149
	private int x;

	// Token: 0x04000866 RID: 2150
	private int y;

	// Token: 0x04000867 RID: 2151
	private int koef;

	// Token: 0x04000868 RID: 2152
	private int Inactive_Tabs_Space_X;

	// Token: 0x04000869 RID: 2153
	public int Active_Tab_Index;

	// Token: 0x0400086A RID: 2154
	public Dictionary<int, int[]> SelectedItem = new Dictionary<int, int[]>();

	// Token: 0x0400086B RID: 2155
	public int Active_Item_PodIndex;

	// Token: 0x0400086C RID: 2156
	private int x_pos;

	// Token: 0x0400086D RID: 2157
	private int y_pos;

	// Token: 0x0400086E RID: 2158
	private int icount;

	// Token: 0x0400086F RID: 2159
	private float sh;

	// Token: 0x04000870 RID: 2160
	private Vector2 mousePos;

	// Token: 0x04000871 RID: 2161
	private Client cscl;

	// Token: 0x04000872 RID: 2162
	private WeaponSystem m_WeaponSystem;
}
