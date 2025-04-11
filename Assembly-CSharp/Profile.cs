using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200001E RID: 30
public class Profile : MonoBehaviour
{
	// Token: 0x060000D9 RID: 217 RVA: 0x0000F0C3 File Offset: 0x0000D2C3
	private void myGlobalInit()
	{
		this.Active = false;
		this.draw_profile = false;
		this.draw_hall = false;
	}

	// Token: 0x060000DA RID: 218 RVA: 0x0000F0DA File Offset: 0x0000D2DA
	public void onActive()
	{
		this.draw_profile = false;
		this.draw_hall = false;
		if (PlayerProfile.id == "0")
		{
			return;
		}
		base.StartCoroutine(this.get_stats());
		base.StartCoroutine(this.get_hall());
	}

	// Token: 0x060000DB RID: 219 RVA: 0x0000F118 File Offset: 0x0000D318
	private void OnGUI()
	{
		if (GameController.STATE != GAME_STATES.MAINMENU)
		{
			return;
		}
		if (GM.currGUIState != GUIGS.RANG)
		{
			return;
		}
		if (!this.Active)
		{
			return;
		}
		GUI.Window(902, new Rect((float)Screen.width / 2f - 300f, 199f, 600f, GUIManager.YRES(768f) - 180f), new GUI.WindowFunction(this.DoWindow), "", GUIManager.gs_style1);
	}

	// Token: 0x060000DC RID: 220 RVA: 0x0000F194 File Offset: 0x0000D394
	private void DoWindow(int windowID)
	{
		GUI.DrawTexture(new Rect(0f, 0f, 600f, 32f), GUIManager.tex_panel);
		GUI.DrawTexture(new Rect(0f, 32f, 600f, GUIManager.YRES(768f) - 180f), GUIManager.tex_half_black);
		this.DrawMode(Lang.GetLabel(541), 135, 0, 0, 160);
		this.DrawMode(Lang.GetLabel(542), 300, 0, 1, 160);
		if (this.type == 0)
		{
			this.DrawCategory0();
			return;
		}
		if (this.type == 1)
		{
			this.DrawCategory1();
		}
	}

	// Token: 0x060000DD RID: 221 RVA: 0x0000F248 File Offset: 0x0000D448
	private void DrawMode(string name, int x, int y, int id, int length)
	{
		float num = Input.mousePosition.x;
		float num2 = (float)Screen.height - Input.mousePosition.y;
		num -= (float)Screen.width / 2f - 300f;
		num2 -= 192f;
		Rect rect;
		rect..ctor((float)x, (float)y, (float)length, 32f);
		if (this.type != id)
		{
			if (rect.Contains(new Vector2(num, num2)))
			{
				if (!this.hovermode[id])
				{
					this.hovermode[id] = true;
					MainMenu.MainAS.PlayOneShot(SoundManager.SoundHover, AudioListener.volume);
				}
			}
			else if (this.hovermode[id])
			{
				this.hovermode[id] = false;
			}
		}
		if (this.type == id)
		{
			GUI.DrawTexture(new Rect((float)x, (float)y, (float)length, 32f), GUIManager.tex_select);
		}
		else if (this.hovermode[id])
		{
			GUI.DrawTexture(new Rect((float)x, (float)y, (float)length, 32f), GUIManager.tex_hover);
		}
		GUIManager.DrawText(rect, name, 17, 4, 8);
		if (GUI.Button(rect, "", GUIManager.gs_style1))
		{
			MainMenu.MainAS.PlayOneShot(SoundManager.SoundClick, AudioListener.volume);
			this.type = id;
		}
	}

	// Token: 0x060000DE RID: 222 RVA: 0x0000F384 File Offset: 0x0000D584
	private void DrawCategory0()
	{
		if (!this.draw_profile)
		{
			GUIManager.DrawLoading();
			return;
		}
		TextAnchor alignment = GUIManager.gs_style1.alignment;
		Color textColor = GUIManager.gs_style1.normal.textColor;
		GUIManager.gs_style1.alignment = 3;
		Rect rect = new Rect(244f, 50f, 256f, 32f);
		GUIManager.gs_style1.normal.textColor = new Color(1f, 1f, 1f, 1f);
		GUI.Label(rect, Lang.GetLabel(11) + ": " + this.level.ToString(), GUIManager.gs_style1);
		Rect rect2 = new Rect(244f, 82f, 256f, 32f);
		GUIManager.gs_style1.normal.textColor = new Color(1f, 1f, 1f, 1f);
		GUI.Label(rect2, Lang.GetLabel(38) + ": " + this.frags.ToString(), GUIManager.gs_style1);
		Rect rect3 = new Rect(244f, 114f, 256f, 32f);
		GUIManager.gs_style1.normal.textColor = new Color(1f, 1f, 1f, 1f);
		GUI.Label(rect3, Lang.GetLabel(39) + ": " + this.deaths.ToString(), GUIManager.gs_style1);
		Rect rect4 = new Rect(244f, 146f, 256f, 32f);
		GUIManager.gs_style1.normal.textColor = new Color(1f, 1f, 1f, 1f);
		GUI.Label(rect4, Lang.GetLabel(14) + ": " + this.exp.ToString(), GUIManager.gs_style1);
		Rect rect5 = new Rect(244f, 178f, 256f, 32f);
		GUIManager.gs_style1.normal.textColor = new Color(1f, 1f, 1f, 1f);
		GUI.Label(rect5, Lang.GetLabel(15) + ": " + this.progress.ToString() + " %", GUIManager.gs_style1);
		GUIManager.gs_style1.normal.textColor = textColor;
		GUIManager.gs_style1.alignment = alignment;
	}

	// Token: 0x060000DF RID: 223 RVA: 0x0000F5E8 File Offset: 0x0000D7E8
	private void DrawCategory1()
	{
		if (!this.draw_hall)
		{
			GUIManager.DrawLoading();
			return;
		}
		float num = GUIManager.YRES(768f) - 180f;
		this.scrollViewVector = GUIManager.BeginScrollView(new Rect(0f, 40f, 600f, num), this.scrollViewVector, new Rect(0f, 0f, 0f, 718f));
		if (!PlayerProfile.NY2017Q && !PlayerProfile.VD2017Q && !PlayerProfile.NY2018Q)
		{
			GUI.Label(new Rect(0f, 0f, 600f, 200f), Lang.GetLabel(547), GUIManager.gs_style1);
			return;
		}
		float num2 = 0f;
		if (PlayerProfile.NY2017Q)
		{
			GUI.DrawTexture(new Rect(10f, num2, 580f, 216f), GUIManager.tex_half_black);
			GUI.DrawTexture(new Rect(30f, num2, 113f, 216f), GUIManager.NY2017REWARD);
			TextAnchor alignment = GUIManager.gs_style3.alignment;
			GUIManager.gs_style3.alignment = 1;
			GUIManager.gs_style3.fontSize += 4;
			GUI.Label(new Rect(133f, num2 + 20f, 487f, 216f), Lang.GetLabel(548), GUIManager.gs_style3);
			GUIManager.gs_style3.fontSize -= 4;
			GUIManager.gs_style3.alignment = alignment;
			alignment = GUIManager.gs_style1.alignment;
			GUIManager.gs_style1.alignment = 4;
			GUI.Label(new Rect(133f, num2, 487f, 216f), Lang.GetLabel(549), GUIManager.gs_style1);
			GUIManager.gs_style1.alignment = alignment;
			if ((PlayerProfile.network == NETWORK.VK || PlayerProfile.network == NETWORK.FB) && GUIManager.DrawButton2(new Vector2(398f, 170f), Vector2.zero, Lang.GetLabel(229), 1))
			{
				if (Screen.fullScreen)
				{
					Config.ChangeMode();
				}
				if (PlayerProfile.network == NETWORK.VK)
				{
					Application.ExternalCall("SocialNY2017Reward", Array.Empty<object>());
				}
			}
		}
		num2 += 226f;
		if (PlayerProfile.VD2017Q)
		{
			GUI.DrawTexture(new Rect(10f, num2, 580f, 216f), GUIManager.tex_half_black);
			GUI.DrawTexture(new Rect(30f, num2, 113f, 216f), GUIManager.VD2017REWARD);
			TextAnchor alignment2 = GUIManager.gs_style3.alignment;
			GUIManager.gs_style3.alignment = 1;
			GUIManager.gs_style3.fontSize += 4;
			GUI.Label(new Rect(133f, num2 + 20f, 487f, 216f), Lang.GetLabel(667), GUIManager.gs_style3);
			GUIManager.gs_style3.fontSize -= 4;
			GUIManager.gs_style3.alignment = alignment2;
			alignment2 = GUIManager.gs_style1.alignment;
			GUIManager.gs_style1.alignment = 4;
			GUI.Label(new Rect(133f, num2, 487f, 216f), Lang.GetLabel(668), GUIManager.gs_style1);
			GUIManager.gs_style1.alignment = alignment2;
			if ((PlayerProfile.network == NETWORK.VK || PlayerProfile.network == NETWORK.FB) && GUIManager.DrawButton2(new Vector2(398f, 396f), Vector2.zero, Lang.GetLabel(229), 1))
			{
				if (Screen.fullScreen)
				{
					Config.ChangeMode();
				}
				if (PlayerProfile.network == NETWORK.VK)
				{
					Application.ExternalCall("SocialVD2017Reward", Array.Empty<object>());
				}
			}
		}
		num2 += 226f;
		if (PlayerProfile.NY2018Q)
		{
			GUI.DrawTexture(new Rect(10f, num2, 580f, 216f), GUIManager.tex_half_black);
			GUI.DrawTexture(new Rect(30f, num2, 113f, 216f), GUIManager.NY2018REWARD);
			TextAnchor alignment3 = GUIManager.gs_style3.alignment;
			GUIManager.gs_style3.alignment = 1;
			GUIManager.gs_style3.fontSize += 4;
			GUI.Label(new Rect(133f, num2 + 20f, 487f, 216f), Lang.GetLabel(574), GUIManager.gs_style3);
			GUIManager.gs_style3.fontSize -= 4;
			GUIManager.gs_style3.alignment = alignment3;
			alignment3 = GUIManager.gs_style1.alignment;
			GUIManager.gs_style1.alignment = 4;
			GUI.Label(new Rect(133f, num2, 487f, 216f), Lang.GetLabel(575), GUIManager.gs_style1);
			GUIManager.gs_style1.alignment = alignment3;
			if ((PlayerProfile.network == NETWORK.VK || PlayerProfile.network == NETWORK.FB) && GUIManager.DrawButton2(new Vector2(398f, 622f), Vector2.zero, Lang.GetLabel(229), 1))
			{
				if (Screen.fullScreen)
				{
					Config.ChangeMode();
				}
				if (PlayerProfile.network == NETWORK.VK)
				{
					Application.ExternalCall("SocialNY2018Reward", Array.Empty<object>());
				}
			}
		}
		GUIManager.EndScrollView();
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x0000FAD2 File Offset: 0x0000DCD2
	private IEnumerator get_stats()
	{
		string text = string.Concat(new object[]
		{
			CONST.HANDLER_SERVER,
			"0&id=",
			PlayerProfile.id.ToString(),
			"&key=",
			PlayerProfile.authkey,
			"&session=",
			PlayerProfile.session,
			"&time=",
			DateTime.Now.Second
		});
		WWW www = new WWW(text);
		yield return www;
		if (www.error == null)
		{
			string[] array = www.text.Split(new char[]
			{
				'|'
			});
			int.TryParse(array[0], out this.frags);
			int.TryParse(array[1], out this.deaths);
			int.TryParse(array[2], out this.exp);
			int num = 1;
			this.level = 1;
			while (this.exp >= (num * (num + 1) * (num + 2) + 15 * num) * 10)
			{
				num++;
				this.level++;
			}
			num = this.level - 1;
			this.currexp = (num * (num + 1) * (num + 2) + 15 * num) * 10;
			num = this.level;
			this.nextexp = (num * (num + 1) * (num + 2) + 15 * num) * 10;
			PlayerProfile.level = this.level;
			float num2 = (float)((this.exp - this.currexp) * 100 / (this.nextexp - this.currexp));
			this.progress = (int)num2;
			this.draw_profile = true;
			base.StartCoroutine(this.get_bonus_lvl());
			base.StartCoroutine(this.get_bonus_level());
		}
		yield break;
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x0000FAE1 File Offset: 0x0000DCE1
	private IEnumerator get_hall()
	{
		string text = string.Concat(new object[]
		{
			CONST.HANDLER_SERVER,
			"303&id=",
			PlayerProfile.id.ToString(),
			"&key=",
			PlayerProfile.authkey,
			"&session=",
			PlayerProfile.session,
			"&time=",
			DateTime.Now.Second
		});
		WWW www = new WWW(text);
		yield return www;
		if (www.error == null)
		{
			string[] array = www.text.Split(new char[]
			{
				'|'
			});
			if (array[0].Contains("OK"))
			{
				PlayerProfile.NY2017Q = true;
			}
			if (array[1].Contains("OK"))
			{
				PlayerProfile.VD2017Q = true;
			}
			if (array[2].Contains("OK"))
			{
				PlayerProfile.NY2018Q = true;
			}
			this.draw_hall = true;
		}
		yield break;
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x0000FAF0 File Offset: 0x0000DCF0
	private IEnumerator get_bonus_lvl()
	{
		string text = string.Concat(new object[]
		{
			CONST.HANDLER_SERVER,
			"102&id=",
			PlayerProfile.id,
			"&key=",
			PlayerProfile.authkey,
			"&session=",
			PlayerProfile.session,
			"&time=",
			DateTime.Now.Second
		});
		WWW www = new WWW(text);
		yield return www;
		if (www.error == null)
		{
			if (www.text.Split(new char[]
			{
				'^'
			})[0] == "OK")
			{
				PopUp.THIS.bonus_text = www.text;
				PopUp.ShowBonus(6, PlayerProfile.level);
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x060000E3 RID: 227 RVA: 0x0000FAF8 File Offset: 0x0000DCF8
	private IEnumerator get_bonus_level()
	{
		string text = string.Concat(new object[]
		{
			CONST.HANDLER_SERVER,
			"13&id=",
			PlayerProfile.id.ToString(),
			"&key=",
			PlayerProfile.authkey,
			"&session=",
			PlayerProfile.session,
			"&time=",
			DateTime.Now.Second
		});
		WWW www = new WWW(text);
		yield return www;
		if (www.error == null)
		{
			string[] array = www.text.Split(new char[]
			{
				'|'
			});
			if (array.Length != 2)
			{
				yield break;
			}
			int num = 0;
			int param = 0;
			if (!int.TryParse(array[0], out num))
			{
				yield break;
			}
			if (!int.TryParse(array[1], out param))
			{
				yield break;
			}
			PlayerProfile.money += num;
			PopUp.ShowBonus(5, param);
		}
		yield break;
	}

	// Token: 0x040000F0 RID: 240
	public bool Active;

	// Token: 0x040000F1 RID: 241
	private GameObject Store;

	// Token: 0x040000F2 RID: 242
	private bool stats_load;

	// Token: 0x040000F3 RID: 243
	private int level;

	// Token: 0x040000F4 RID: 244
	private int frags;

	// Token: 0x040000F5 RID: 245
	private int deaths;

	// Token: 0x040000F6 RID: 246
	private int exp;

	// Token: 0x040000F7 RID: 247
	private int currexp;

	// Token: 0x040000F8 RID: 248
	private int nextexp;

	// Token: 0x040000F9 RID: 249
	private int progress;

	// Token: 0x040000FA RID: 250
	private int type;

	// Token: 0x040000FB RID: 251
	private bool[] hovermode = new bool[2];

	// Token: 0x040000FC RID: 252
	private bool draw_profile;

	// Token: 0x040000FD RID: 253
	private bool draw_hall;

	// Token: 0x040000FE RID: 254
	private Vector2 scrollViewVector = Vector2.zero;
}
