using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200001D RID: 29
public class PopUp : MonoBehaviour
{
	// Token: 0x060000C8 RID: 200 RVA: 0x0000D1BC File Offset: 0x0000B3BC
	private void Awake()
	{
		PopUp.THIS = this;
		this.backTexture = new Texture2D(1, 1);
		this.backTexture.SetPixel(0, 0, new Color(0.18f, 0.18f, 0.18f, 1f));
		this.backTexture.Apply();
		this.blackTexture = new Texture2D(1, 1);
		this.blackTexture.SetPixel(0, 0, new Color(0f, 0f, 0f, 1f));
		this.blackTexture.Apply();
		this.blackTexture2 = new Texture2D(1, 1);
		this.blackTexture2.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.9f));
		this.blackTexture2.Apply();
		this.start_back = (Resources.Load("GUI/bonus/start_bonus" + Lang.current.ToString()) as Texture2D);
		this.day_back = (Resources.Load("GUI/bonus/day_bonus" + Lang.current.ToString()) as Texture2D);
		this.week_back = (Resources.Load("GUI/bonus/week_bonus" + Lang.current.ToString()) as Texture2D);
		this.week_1 = (Resources.Load("GUI/bonus/week_1") as Texture2D);
		this.week_2 = (Resources.Load("GUI/bonus/week_2") as Texture2D);
		this.week_3 = (Resources.Load("GUI/bonus/week_3") as Texture2D);
		this.level_back = (Resources.Load("GUI/bonus/level_bonus" + Lang.current.ToString()) as Texture2D);
		this.info_back = (Resources.Load("GUI/bonus/info_bonus") as Texture2D);
		this.level_back_steam = (Resources.Load("GUI/bonus/level_bonus_steam") as Texture2D);
		this.item_back = (Resources.Load("GUI/bonus/item_back") as Texture2D);
		this.info_bonus3 = (Resources.Load("GUI/bonus/info_bonus3") as Texture2D);
		this.info_bonus4 = (Resources.Load("GUI/bonus/info_bonus4") as Texture2D);
		this.info_bonus5 = (Resources.Load("GUI/bonus/info_bonus5") as Texture2D);
		this.info_bonus6 = (Resources.Load("GUI/bonus/info_bonus6") as Texture2D);
		this.info_bonus7 = (Resources.Load("GUI/bonus/info_bonus7") as Texture2D);
		this.info_error = (Resources.Load("GUI/bonus/info_error") as Texture2D);
		this.tex_letter = Resources.Load<Texture2D>("NYQuest/Letter");
		this.info_updates = (Resources.Load("GUI/update") as Texture2D);
		this.info_ban = (Resources.Load("GUI/ban") as Texture2D);
		this.BonusItemIcons.Add(50, Resources.Load("GUI/shop/axe") as Texture2D);
		this.BonusItemIcons.Add(52, Resources.Load("GUI/shop/crowbar") as Texture2D);
		this.BonusItemIcons.Add(96, Resources.Load("GUI/shop/machete") as Texture2D);
		this.BonusItemIcons.Add(51, Resources.Load("GUI/shop/bat") as Texture2D);
		this.BonusItemIcons.Add(103, Resources.Load("GUI/shop/fn57") as Texture2D);
		this.BonusItemIcons.Add(40, Resources.Load("GUI/shop/usp") as Texture2D);
		this.BonusItemIcons.Add(95, Resources.Load("GUI/shop/beretta") as Texture2D);
		this.BonusItemIcons.Add(72, Resources.Load("GUI/shop/anaconda") as Texture2D);
		this.BonusItemIcons.Add(48, Resources.Load("GUI/shop/tmp") as Texture2D);
		this.BonusItemIcons.Add(74, Resources.Load("GUI/shop/p90") as Texture2D);
		this.BonusItemIcons.Add(13, Resources.Load("GUI/shop/g36c") as Texture2D);
		this.BonusItemIcons.Add(93, Resources.Load("GUI/shop/kacpdw") as Texture2D);
		this.BonusItemIcons.Add(14, Resources.Load("GUI/shop/kriss") as Texture2D);
		this.BonusItemIcons.Add(60, Resources.Load("GUI/shop/aug") as Texture2D);
		this.BonusItemIcons.Add(15, Resources.Load("GUI/shop/m4a1") as Texture2D);
		this.BonusItemIcons.Add(2, Resources.Load("GUI/shop/ak47") as Texture2D);
		this.BonusItemIcons.Add(105, Resources.Load("GUI/shop/l85") as Texture2D);
		this.BonusItemIcons.Add(78, Resources.Load("GUI/shop/rpk") as Texture2D);
		this.BonusItemIcons.Add(107, Resources.Load("GUI/shop/pkp") as Texture2D);
		this.BonusItemIcons.Add(3, Resources.Load("GUI/shop/svd") as Texture2D);
		this.BonusItemIcons.Add(19, Resources.Load("GUI/shop/vsk94") as Texture2D);
		this.BonusItemIcons.Add(81, Resources.Load("GUI/shop/sr25") as Texture2D);
		this.BonusItemIcons.Add(7, Resources.Load("GUI/shop/m61") as Texture2D);
		this.BonusItemIcons.Add(10, Resources.Load("GUI/shop/shmel") as Texture2D);
		this.BonusItemIcons.Add(36, Resources.Load("GUI/shop/medkit_w") as Texture2D);
		this.BonusItemIcons.Add(37, Resources.Load("GUI/shop/medkit_g") as Texture2D);
		this.BonusItemIcons.Add(38, Resources.Load("GUI/shop/medkit_o") as Texture2D);
		this.BonusItemIcons.Add(55, Resources.Load("GUI/shop/tnt") as Texture2D);
		this.BonusItemIcons.Add(77, Resources.Load("GUI/shop/gp") as Texture2D);
		this.BonusItemIcons.Add(138, Resources.Load("GUI/shop/javelin") as Texture2D);
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x0000D797 File Offset: 0x0000B997
	public static void ShowBonus(int _msgid, int _param)
	{
		if (PopUp.popups.ContainsKey(_msgid))
		{
			return;
		}
		PopUp.popups.Add(_msgid, _param);
		PopUp.Active = true;
	}

	// Token: 0x060000CA RID: 202 RVA: 0x0000D7B9 File Offset: 0x0000B9B9
	private void RemoveCurrentMSG()
	{
		PopUp.popups.Remove(this.current_msgid);
		this.current_msgid = 0;
		this.start = 0f;
		if (PopUp.popups.Count == 0)
		{
			PopUp.Active = false;
		}
	}

	// Token: 0x060000CB RID: 203 RVA: 0x0000D7F0 File Offset: 0x0000B9F0
	private void OnGUI()
	{
		if (!PopUp.Active)
		{
			return;
		}
		if (this.start == 0f)
		{
			this.start = Time.time;
			this.xofs = 0f;
		}
		if (Time.time > this.start + 10000f)
		{
			this.RemoveCurrentMSG();
			return;
		}
		GUI.depth = -99;
		GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), this.blackTexture2);
		if (this.current_msgid == 0)
		{
			using (Dictionary<int, int>.KeyCollection.Enumerator enumerator = PopUp.popups.Keys.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					int num = enumerator.Current;
					this.current_msgid = num;
				}
			}
		}
		if (this.current_msgid == 1)
		{
			this.DrawStartBonus();
			return;
		}
		if (this.current_msgid == 2)
		{
			this.DrawDayBonus();
			return;
		}
		if (this.current_msgid == 3)
		{
			this.DrawWeekBonus();
			return;
		}
		if (this.current_msgid == 4)
		{
			this.DrawInfoBonus();
			return;
		}
		if (this.current_msgid == 5)
		{
			this.DrawLevelBonus();
			return;
		}
		if (this.current_msgid == 6)
		{
			this.DrawLevelSteamBonus();
			return;
		}
		if (this.current_msgid == 7)
		{
			this.DrawUpdates();
			return;
		}
		if (this.current_msgid == 8)
		{
			this.DrawBan();
			return;
		}
		if (this.current_msgid == 9)
		{
			this.DrawAuthError();
			return;
		}
		if (this.current_msgid == 2017)
		{
			if (PopUp.popups[this.current_msgid] == 1)
			{
				this.DrawNYQuest();
				return;
			}
			this.DrawNYReward();
		}
	}

	// Token: 0x060000CC RID: 204 RVA: 0x0000D984 File Offset: 0x0000BB84
	private void DrawStartBonus()
	{
		this.xofs += Time.deltaTime * 2000f;
		if (this.xofs > 1000f)
		{
			this.xofs = 1000f;
		}
		Rect rect;
		rect..ctor(0f, 0f, 600f, 300f);
		rect.center = new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f);
		rect.x = rect.x + this.xofs - 1000f;
		GUI.DrawTexture(rect, this.start_back);
		if (PlayerProfile.network == NETWORK.VK || PlayerProfile.network == NETWORK.MM)
		{
			if (GUIManager.DrawButton2(new Vector2(rect.x + 358f, rect.y + 196f), Vector2.zero, Lang.GetLabel(229), 1))
			{
				if (Screen.fullScreen)
				{
					Config.ChangeMode();
				}
				Application.ExternalCall("wallpost1", Array.Empty<object>());
				this.RemoveCurrentMSG();
			}
		}
		else if (PlayerProfile.network == NETWORK.FB && GUIManager.DrawButton2(new Vector2(rect.x + 358f, rect.y + 196f), Vector2.zero, Lang.GetLabel(229), 1))
		{
			if (Screen.fullScreen)
			{
				Config.ChangeMode();
			}
			this.RemoveCurrentMSG();
		}
		if (GUIManager.DrawButton2(new Vector2(rect.x + 358f, rect.y + 228f), Vector2.zero, Lang.GetLabel(228), 0))
		{
			this.RemoveCurrentMSG();
		}
	}

	// Token: 0x060000CD RID: 205 RVA: 0x0000DB1C File Offset: 0x0000BD1C
	private void DrawDayBonus()
	{
		this.xofs += Time.deltaTime * 2000f;
		if (this.xofs > 1000f)
		{
			this.xofs = 1000f;
		}
		Rect rect;
		rect..ctor(0f, 0f, 600f, 300f);
		rect.center = new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f);
		rect.x = rect.x + this.xofs - 1000f;
		GUI.DrawTexture(rect, this.day_back);
		if (PlayerProfile.network == NETWORK.VK || PlayerProfile.network == NETWORK.MM)
		{
			if (GUIManager.DrawButton2(new Vector2(rect.x + 358f, rect.y + 196f), Vector2.zero, Lang.GetLabel(229), 1))
			{
				if (Screen.fullScreen)
				{
					Config.ChangeMode();
				}
				Application.ExternalCall("wallpost1", Array.Empty<object>());
				this.RemoveCurrentMSG();
			}
		}
		else if (PlayerProfile.network == NETWORK.FB && GUIManager.DrawButton2(new Vector2(rect.x + 358f, rect.y + 196f), Vector2.zero, Lang.GetLabel(229), 1))
		{
			if (Screen.fullScreen)
			{
				Config.ChangeMode();
			}
			this.RemoveCurrentMSG();
		}
		if (GUIManager.DrawButton2(new Vector2(rect.x + 358f, rect.y + 228f), Vector2.zero, Lang.GetLabel(228), 0))
		{
			this.RemoveCurrentMSG();
		}
	}

	// Token: 0x060000CE RID: 206 RVA: 0x0000DCB4 File Offset: 0x0000BEB4
	private void DrawWeekBonus()
	{
		this.xofs += Time.deltaTime * 2000f;
		if (this.xofs > 1000f)
		{
			this.xofs = 1000f;
		}
		Rect rect;
		rect..ctor(0f, 0f, 600f, 300f);
		rect.center = new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f);
		rect.x = rect.x + this.xofs - 1000f;
		GUI.DrawTexture(rect, this.week_back);
		GUI.DrawTexture(new Rect(rect.x + 315f, rect.y + 160f, 255f, 40f), this.week_1);
		GUI.DrawTexture(new Rect(rect.x + 315f + (float)(37 * (PopUp.popups[this.current_msgid] - 1)), rect.y + 160f + 7f, 33f, 33f), this.week_2);
		GUI.DrawTexture(new Rect(rect.x + 315f, rect.y + 160f, 255f, 40f), this.week_3);
		if (GUIManager.DrawButton2(new Vector2(rect.x + 380f, rect.y + 228f), Vector2.zero, Lang.GetLabel(228), 0))
		{
			this.RemoveCurrentMSG();
		}
	}

	// Token: 0x060000CF RID: 207 RVA: 0x0000DE4C File Offset: 0x0000C04C
	private void DrawNYQuest()
	{
		int depth = GUI.depth;
		GUI.depth = -100;
		this.xofs += Time.deltaTime * 2000f;
		if (this.xofs > 1000f)
		{
			this.xofs = 1000f;
		}
		Rect rect;
		rect..ctor(0f, 0f, 800f, 600f);
		rect.center = new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f);
		rect.x = rect.x + this.xofs - 1000f;
		GUI.DrawTexture(rect, this.tex_letter);
		TextAnchor alignment = GUIManager.gs_style2.alignment;
		TextAnchor alignment2 = GUIManager.gs_style3.alignment;
		GUIManager.gs_style2.richText = true;
		GUI.color = new Color(0f, 0f, 0f, 0.8f);
		GUIManager.gs_style2.alignment = 4;
		GUIManager.gs_style3.alignment = 1;
		GUIManager.gs_style3.fontSize += 10;
		GUI.Label(new Rect(rect.x, rect.y + 120f, rect.width, rect.height), Lang.GetLabel(662), GUIManager.gs_style3);
		GUIManager.gs_style3.fontSize -= 10;
		GUIManager.gs_style2.fontSize -= 2;
		GUI.Label(new Rect(rect.x, rect.y + 20f, rect.width, rect.height), Lang.GetLabel(661), GUIManager.gs_style2);
		GUIManager.gs_style2.fontSize += 2;
		GUI.color = Color.white;
		GUIManager.gs_style2.richText = false;
		GUIManager.gs_style2.alignment = alignment;
		GUIManager.gs_style3.alignment = alignment2;
		if (GUIManager.DrawButton2(new Vector2(rect.x + 500f, rect.y + 520f), Vector2.zero, Lang.GetLabel(228), 0))
		{
			this.RemoveCurrentMSG();
			PlayerPrefs.SetInt(PlayerProfile.id.ToString() + "NYQuest2018", 1);
			PlayerPrefs.Save();
		}
		GUI.depth = depth;
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x0000E09C File Offset: 0x0000C29C
	private void DrawNYReward()
	{
		this.xofs += Time.deltaTime * 2000f;
		if (this.xofs > 1000f)
		{
			this.xofs = 1000f;
		}
		Rect rect;
		rect..ctor(0f, 0f, 600f, 300f);
		rect.center = new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f);
		rect.x = rect.x + this.xofs - 1000f;
		GUI.DrawTexture(rect, this.info_bonus3);
		GUIManager.DrawText(new Rect(rect.x + 266f, rect.y + 66f, 280f, 26f), Lang.GetLabel(545), 26, 5, 8);
		GUIManager.DrawText(new Rect(rect.x + 246f, rect.y + 106f, 300f, 200f), Lang.GetLabel(546), 16, 2, 8);
		if (GUIManager.DrawButton2(new Vector2(rect.x + 358f, rect.y + 206f), Vector2.zero, Lang.GetLabel(228), 0))
		{
			this.RemoveCurrentMSG();
		}
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x0000E1F4 File Offset: 0x0000C3F4
	private void DrawInfoBonus()
	{
		this.xofs += Time.deltaTime * 2000f;
		if (this.xofs > 1000f)
		{
			this.xofs = 1000f;
		}
		Rect rect;
		rect..ctor(0f, 0f, 600f, 300f);
		rect.center = new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f);
		rect.x = rect.x + this.xofs - 1000f;
		if (PopUp.popups[this.current_msgid] > 1)
		{
			if (PopUp.popups[this.current_msgid] == 4)
			{
				GUI.DrawTexture(rect, this.info_bonus4);
			}
			else if (PopUp.popups[this.current_msgid] == 5)
			{
				GUI.DrawTexture(rect, this.info_bonus5);
			}
			else if (PopUp.popups[this.current_msgid] == 6)
			{
				GUI.DrawTexture(rect, this.info_bonus6);
			}
			else
			{
				GUI.DrawTexture(rect, this.info_bonus3);
			}
		}
		else
		{
			GUI.DrawTexture(rect, this.info_back);
		}
		if (PopUp.popups[this.current_msgid] == 0)
		{
			GUIManager.DrawText(new Rect(rect.x + 246f, rect.y + 106f, 300f, 200f), Lang.GetLabel(62), 22, 2, 8);
		}
		else if (PopUp.popups[this.current_msgid] == 1)
		{
			GUIManager.DrawText(new Rect(rect.x + 246f, rect.y + 106f, 300f, 200f), Lang.GetLabel(115), 22, 2, 8);
		}
		else if (PopUp.popups[this.current_msgid] == 2)
		{
			GUIManager.DrawText(new Rect(rect.x + 266f, rect.y + 66f, 280f, 26f), Lang.GetLabel(380), 26, 5, 8);
			GUIManager.DrawText(new Rect(rect.x + 246f, rect.y + 106f, 300f, 200f), Lang.GetLabel(509), 22, 2, 8);
		}
		else if (PopUp.popups[this.current_msgid] == 3)
		{
			GUIManager.DrawText(new Rect(rect.x + 266f, rect.y + 66f, 280f, 26f), Lang.GetLabel(380), 26, 5, 8);
			GUIManager.DrawText(new Rect(rect.x + 246f, rect.y + 106f, 300f, 200f), Lang.GetLabel(509), 22, 2, 8);
		}
		else if (PopUp.popups[this.current_msgid] == 4)
		{
			string text = Lang.GetLabel(509);
			GUIManager.DrawText(new Rect(rect.x + 266f, rect.y + 66f, 280f, 26f), Lang.GetLabel(510), 26, 5, 8);
			GUIManager.DrawText(new Rect(rect.x + 246f, rect.y + 106f, 300f, 200f), text, 22, 2, 8);
		}
		else if (PopUp.popups[this.current_msgid] == 5)
		{
			GUIManager.DrawText(new Rect(rect.x + 266f, rect.y + 66f, 280f, 26f), Lang.GetLabel(528), 26, 5, 8);
			GUIManager.DrawText(new Rect(rect.x + 246f, rect.y + 106f, 300f, 200f), Lang.GetLabel(509), 22, 2, 8);
		}
		else if (PopUp.popups[this.current_msgid] == 6)
		{
			GUIManager.DrawText(new Rect(rect.x + 266f, rect.y + 66f, 280f, 26f), Lang.GetLabel(534), 26, 5, 8);
			GUIManager.DrawText(new Rect(rect.x + 246f, rect.y + 106f, 300f, 200f), Lang.GetLabel(535), 22, 2, 8);
		}
		else if (PopUp.popups[this.current_msgid] == 7)
		{
			GUIManager.DrawText(new Rect(rect.x + 266f, rect.y + 66f, 280f, 26f), Lang.GetLabel(510), 26, 5, 8);
			GUIManager.DrawText(new Rect(rect.x + 246f, rect.y + 106f, 300f, 200f), Lang.GetLabel(509), 22, 2, 8);
		}
		if (GUIManager.DrawButton2(new Vector2(rect.x + 358f, rect.y + 206f), Vector2.zero, Lang.GetLabel(228), 0))
		{
			this.RemoveCurrentMSG();
		}
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x0000E758 File Offset: 0x0000C958
	private void DrawLevelBonus()
	{
		this.xofs += Time.deltaTime * 2000f;
		if (this.xofs > 1000f)
		{
			this.xofs = 1000f;
		}
		Rect rect;
		rect..ctor(0f, 0f, 600f, 300f);
		rect.center = new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f);
		rect.x = rect.x + this.xofs - 1000f;
		GUI.DrawTexture(rect, this.level_back);
		GUIManager.DrawText(new Rect(rect.x + 406f, rect.y + 106f, 60f, 22f), PopUp.popups[this.current_msgid].ToString(), 22, 4, 8);
		if (PlayerProfile.network == NETWORK.FB && GUIManager.DrawButton2(new Vector2(rect.x + 358f, rect.y + 196f), Vector2.zero, Lang.GetLabel(229), 1))
		{
			if (Screen.fullScreen)
			{
				Config.ChangeMode();
			}
			this.RemoveCurrentMSG();
		}
		if (GUIManager.DrawButton2(new Vector2(rect.x + 358f, rect.y + 228f), Vector2.zero, Lang.GetLabel(228), 0))
		{
			this.RemoveCurrentMSG();
		}
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x0000E8D4 File Offset: 0x0000CAD4
	private void DrawUpdates()
	{
		this.xofs += Time.deltaTime * 2000f;
		if (this.xofs > 1000f)
		{
			this.xofs = 1000f;
		}
		Rect rect;
		rect..ctor(0f, 0f, 600f, 300f);
		rect.center = new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f);
		rect.x = rect.x + this.xofs - 1000f;
		GUI.DrawTexture(rect, this.info_updates);
		GUIManager.DrawText(new Rect(rect.x + 266f, rect.y + 106f, 280f, 200f), Lang.GetLabel(386), 22, 2, 8);
		GUIManager.DrawText(new Rect(rect.x + 266f, rect.y + 66f, 280f, 26f), Lang.GetLabel(385), 26, 5, 8);
		if (GUIManager.DrawButton2(new Vector2(rect.x + 358f, rect.y + 226f), Vector2.zero, Lang.GetLabel(389), 0))
		{
			Application.Quit();
		}
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x0000EA2C File Offset: 0x0000CC2C
	private void DrawAuthError()
	{
		this.xofs += Time.deltaTime * 2000f;
		if (this.xofs > 1000f)
		{
			this.xofs = 1000f;
		}
		Rect rect;
		rect..ctor(0f, 0f, 600f, 300f);
		rect.center = new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f);
		rect.x = rect.x + this.xofs - 1000f;
		GUI.DrawTexture(rect, this.info_error);
		GUIManager.DrawText(new Rect(rect.x + 266f, rect.y + 106f, 280f, 200f), Lang.GetLabel(391), 22, 2, 8);
		GUIManager.DrawText(new Rect(rect.x + 266f, rect.y + 66f, 280f, 26f), Lang.GetLabel(390), 26, 5, 8);
		if (GUIManager.DrawButton2(new Vector2(rect.x + 358f, rect.y + 226f), Vector2.zero, Lang.GetLabel(389), 0))
		{
			Application.Quit();
		}
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x0000EB84 File Offset: 0x0000CD84
	private void DrawBan()
	{
		this.xofs += Time.deltaTime * 2000f;
		if (this.xofs > 1000f)
		{
			this.xofs = 1000f;
		}
		Rect rect;
		rect..ctor(0f, 0f, 600f, 300f);
		rect.center = new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f);
		rect.x = rect.x + this.xofs - 1000f;
		GUI.DrawTexture(rect, this.info_ban);
		GUIManager.DrawText(new Rect(rect.x + 266f, rect.y + 106f, 280f, 200f), Lang.GetLabel(388), 22, 2, 8);
		GUIManager.DrawText(new Rect(rect.x + 266f, rect.y + 66f, 280f, 26f), Lang.GetLabel(387), 26, 5, 8);
		if (PlayerProfile.network == NETWORK.FB)
		{
			GUIManager.DrawButton2(new Vector2(rect.x + 358f, rect.y + 195f), Vector2.zero, Lang.GetLabel(445) + "6", 1);
		}
		if (GUIManager.DrawButton2(new Vector2(rect.x + 358f, rect.y + 195f), Vector2.zero, Lang.GetLabel(445) + PopUp.popups[this.current_msgid], 1))
		{
			Shop shop = (Shop)Object.FindObjectOfType(typeof(Shop));
		}
		if (GUIManager.DrawButton2(new Vector2(rect.x + 358f, rect.y + 226f), Vector2.zero, Lang.GetLabel(389), 0))
		{
			Application.Quit();
		}
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x0000ED88 File Offset: 0x0000CF88
	private void DrawLevelSteamBonus()
	{
		this.xofs += Time.deltaTime * 2000f;
		if (this.xofs > 1000f)
		{
			this.xofs = 1000f;
		}
		Rect rect;
		rect..ctor(0f, 0f, 600f, 300f);
		rect.center = new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f);
		rect.x = rect.x + this.xofs - 1000f;
		GUI.DrawTexture(rect, this.level_back_steam);
		GUIManager.DrawText(new Rect(rect.x + 266f, rect.y + 106f, 280f, 200f), Lang.GetLabel(345) + PopUp.popups[this.current_msgid].ToString() + Lang.GetLabel(346), 22, 2, 8);
		GUIManager.DrawText(new Rect(rect.x + 266f, rect.y + 66f, 280f, 26f), Lang.GetLabel(347), 26, 5, 8);
		if (this.bonus_text != "")
		{
			int num = 266;
			int num2 = 166;
			int num3 = 0;
			string[] array = this.bonus_text.Split(new char[]
			{
				'^'
			});
			if (array.Length - 2 > 7)
			{
				num3 = (array.Length - 9) * 3;
			}
			int num4 = (int)(140f - (float)(array.Length - 2) / 2f * (float)(42 - num3));
			for (int i = 1; i < array.Length; i++)
			{
				if (!(array[i] == ""))
				{
					int key = 0;
					int num5 = 0;
					string[] array2 = array[i].Split(new char[]
					{
						'|'
					});
					int.TryParse(array2[0], out key);
					int.TryParse(array2[1], out num5);
					GUI.DrawTexture(new Rect(rect.x + (float)num + (float)num4, rect.y + (float)num2, (float)(38 - num3), (float)(38 - num3)), this.item_back);
					GUI.DrawTexture(new Rect(rect.x + (float)num + (float)num4, rect.y + (float)num2, (float)(38 - num3), (float)(38 - num3)), this.BonusItemIcons[key]);
					GUIManager.DrawText(new Rect(rect.x + (float)num + (float)num4, rect.y + (float)num2 + 42f - (float)num3, (float)(38 - num3), 16f), num5.ToString(), 16, 1, 8);
					num += 42 - num3;
				}
			}
		}
		if (GUIManager.DrawButton2(new Vector2(rect.x + 358f, rect.y + 226f), Vector2.zero, Lang.GetLabel(228), 0))
		{
			this.RemoveCurrentMSG();
		}
	}

	// Token: 0x040000CB RID: 203
	public static PopUp THIS;

	// Token: 0x040000CC RID: 204
	private Texture2D blackTexture;

	// Token: 0x040000CD RID: 205
	private Texture2D blackTexture2;

	// Token: 0x040000CE RID: 206
	private Texture2D backTexture;

	// Token: 0x040000CF RID: 207
	private Texture2D tex_letter;

	// Token: 0x040000D0 RID: 208
	private float start;

	// Token: 0x040000D1 RID: 209
	private string msg;

	// Token: 0x040000D2 RID: 210
	private string label;

	// Token: 0x040000D3 RID: 211
	private int callback;

	// Token: 0x040000D4 RID: 212
	private int social;

	// Token: 0x040000D5 RID: 213
	public static bool Active = false;

	// Token: 0x040000D6 RID: 214
	private float xofs;

	// Token: 0x040000D7 RID: 215
	public static int mySkin = 0;

	// Token: 0x040000D8 RID: 216
	private Texture2D start_back;

	// Token: 0x040000D9 RID: 217
	private Texture2D day_back;

	// Token: 0x040000DA RID: 218
	private Texture2D week_back;

	// Token: 0x040000DB RID: 219
	private Texture2D week_1;

	// Token: 0x040000DC RID: 220
	private Texture2D week_2;

	// Token: 0x040000DD RID: 221
	private Texture2D week_3;

	// Token: 0x040000DE RID: 222
	private Texture2D level_back;

	// Token: 0x040000DF RID: 223
	private Texture2D info_back;

	// Token: 0x040000E0 RID: 224
	private Texture2D level_back_steam;

	// Token: 0x040000E1 RID: 225
	private Texture2D item_back;

	// Token: 0x040000E2 RID: 226
	private Texture2D info_bonus3;

	// Token: 0x040000E3 RID: 227
	private Texture2D info_bonus4;

	// Token: 0x040000E4 RID: 228
	private Texture2D info_bonus5;

	// Token: 0x040000E5 RID: 229
	private Texture2D info_bonus6;

	// Token: 0x040000E6 RID: 230
	private Texture2D info_bonus7;

	// Token: 0x040000E7 RID: 231
	private Texture2D info_error;

	// Token: 0x040000E8 RID: 232
	private Texture2D info_updates;

	// Token: 0x040000E9 RID: 233
	private Texture2D info_ban;

	// Token: 0x040000EA RID: 234
	private static Dictionary<int, int> popups = new Dictionary<int, int>();

	// Token: 0x040000EB RID: 235
	private int current_msgid;

	// Token: 0x040000EC RID: 236
	public Dictionary<int, Texture2D> BonusItemIcons = new Dictionary<int, Texture2D>();

	// Token: 0x040000ED RID: 237
	public string bonus_text = "";

	// Token: 0x040000EE RID: 238
	public Color[] c;

	// Token: 0x040000EF RID: 239
	private GUIStyle gs_style;
}
