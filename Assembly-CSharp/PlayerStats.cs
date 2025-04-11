using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class PlayerStats : MonoBehaviour
{
	// Token: 0x060000C0 RID: 192 RVA: 0x0000C4FF File Offset: 0x0000A6FF
	private void myGlobalInit()
	{
		this.Active = false;
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x0000C508 File Offset: 0x0000A708
	private void OnGUI()
	{
		if (GameController.STATE != GAME_STATES.MAINMENU)
		{
			return;
		}
		if (GM.currGUIState != GUIGS.RAITING)
		{
			return;
		}
		GUI.Window(903, new Rect((float)Screen.width / 2f - 300f, 199f, 600f, GUIManager.YRES(768f) - 180f), new GUI.WindowFunction(this.DoWindow), "", GUIManager.gs_style1);
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x0000C57C File Offset: 0x0000A77C
	private void DoWindow(int windowID)
	{
		if (!this.Active)
		{
			return;
		}
		GUI.DrawTexture(new Rect(0f, 0f, 600f, 32f), GUIManager.tex_panel);
		GUI.DrawTexture(new Rect(0f, 32f, 600f, GUIManager.YRES(768f) - 180f), GUIManager.tex_half_black);
		TextAnchor alignment = GUIManager.gs_style1.alignment;
		int fontSize = GUIManager.gs_style1.fontSize;
		Color textColor = GUIManager.gs_style1.normal.textColor;
		GUIManager.gs_style1.alignment = 4;
		GUIManager.gs_style1.fontSize = 24;
		this.DrawMode(Lang.GetLabel(332), 170, 0, 0);
		this.DrawMode(Lang.GetLabel(333), 306, 0, 1);
		GUIManager.gs_style1.fontSize = 16;
		GUIManager.gs_style1.alignment = 0;
		if (this.mode == 0)
		{
			Rect viewzone;
			viewzone..ctor(0f, 32f, 600f, GUIManager.YRES(768f) - 180f - 32f - 19f);
			Rect scrollzone;
			scrollzone..ctor(0f, 0f, 0f, 2060f);
			this.scrollViewVector = GUIManager.BeginScrollView(viewzone, this.scrollViewVector, scrollzone);
			int num = 43;
			int num2 = 28;
			GUIManager.gs_style1.normal.textColor = new Color(1f, 1f, 0f, 1f);
			GUI.Label(new Rect((float)(num + 27), 8f, 100f, 20f), Lang.GetLabel(37), GUIManager.gs_style1);
			GUI.Label(new Rect((float)(num + 207), 8f, 100f, 20f), Lang.GetLabel(38), GUIManager.gs_style1);
			GUI.Label(new Rect((float)(num + 277), 8f, 100f, 20f), Lang.GetLabel(39), GUIManager.gs_style1);
			GUI.Label(new Rect((float)(num + 367), 8f, 100f, 20f), Lang.GetLabel(14), GUIManager.gs_style1);
			for (int i = 0; i < this.count; i++)
			{
				if (this.info_vkid[i] == this.myvkid)
				{
					GUI.DrawTexture(new Rect(0f, (float)(i * 20 + num2), 512f, 20f), GUIManager.tex_half_yellow);
				}
				GUIManager.gs_style1.normal.textColor = new Color(1f, 1f, 0f, 1f);
				GUI.Label(new Rect((float)num, (float)(i * 20 + num2), 100f, 20f), (i + 1).ToString(), GUIManager.gs_style1);
				GUIManager.gs_style1.normal.textColor = new Color(1f, 1f, 1f, 1f);
				GUI.Label(new Rect((float)(num + 27), (float)(i * 20 + num2), 100f, 20f), this.info_name[i], GUIManager.gs_style1);
				GUI.Label(new Rect((float)(num + 207), (float)(i * 20 + num2), 100f, 20f), this.info_frags[i], GUIManager.gs_style1);
				GUI.Label(new Rect((float)(num + 277), (float)(i * 20 + num2), 100f, 20f), this.info_deaths[i], GUIManager.gs_style1);
				GUI.Label(new Rect((float)(num + 367), (float)(i * 20 + num2), 100f, 20f), this.info_exp[i], GUIManager.gs_style1);
			}
			GUIManager.EndScrollView();
		}
		else if (this.mode == 1)
		{
			this.scrollViewVector = GUIManager.BeginScrollView(new Rect(0f, 32f, 600f, GUIManager.YRES(768f) - 180f - 32f - 19f), this.scrollViewVector, new Rect(0f, 0f, 0f, 2060f));
			int num3 = 43;
			int num4 = 28;
			GUIManager.gs_style1.normal.textColor = new Color(1f, 1f, 0f, 1f);
			GUI.Label(new Rect((float)(num3 + 27), 8f, 100f, 20f), Lang.GetLabel(37), GUIManager.gs_style1);
			GUI.Label(new Rect((float)(num3 + 157), 8f, 100f, 20f), Lang.GetLabel(12), GUIManager.gs_style1);
			GUI.Label(new Rect((float)(num3 + 277), 8f, 100f, 20f), Lang.GetLabel(11), GUIManager.gs_style1);
			GUI.Label(new Rect((float)(num3 + 367), 8f, 100f, 20f), Lang.GetLabel(14), GUIManager.gs_style1);
			for (int j = 0; j < this.clan_count; j++)
			{
				GUIManager.gs_style1.normal.textColor = new Color(1f, 1f, 0f, 1f);
				GUI.Label(new Rect((float)num3, (float)(j * 20 + num4), 100f, 20f), (j + 1).ToString(), GUIManager.gs_style1);
				GUIManager.gs_style1.normal.textColor = new Color(1f, 1f, 1f, 1f);
				GUI.Label(new Rect((float)(num3 + 27), (float)(j * 20 + num4), 100f, 20f), this.clan_name[j], GUIManager.gs_style1);
				GUI.Label(new Rect((float)(num3 + 157), (float)(j * 20 + num4), 100f, 20f), this.clan_creatorname[j], GUIManager.gs_style1);
				GUI.Label(new Rect((float)(num3 + 277), (float)(j * 20 + num4), 100f, 20f), this.clan_level[j], GUIManager.gs_style1);
				GUI.Label(new Rect((float)(num3 + 367), (float)(j * 20 + num4), 100f, 20f), this.clan_exp[j], GUIManager.gs_style1);
			}
			GUIManager.EndScrollView();
		}
		if (this.mode == 3)
		{
			Rect viewzone2;
			viewzone2..ctor(0f, 32f, 600f, GUIManager.YRES(768f) - 180f - 32f - 19f);
			int num5 = 43;
			int num6 = 28;
			GUIManager.gs_style1.normal.textColor = new Color(1f, 1f, 0f, 1f);
			Rect scrollzone2;
			scrollzone2..ctor(0f, 0f, 0f, (float)((PlayerProfile.friendsRating.Length - 2) * 20 + 60));
			this.scrollViewVector = GUIManager.BeginScrollView(viewzone2, this.scrollViewVector, scrollzone2);
			GUI.Label(new Rect((float)num5, 8f, 100f, 20f), Lang.GetLabel(481), GUIManager.gs_style1);
			GUI.Label(new Rect((float)(num5 + 77), 8f, 100f, 20f), Lang.GetLabel(482), GUIManager.gs_style1);
			GUI.Label(new Rect((float)(num5 + 367), 8f, 100f, 20f), Lang.GetLabel(483), GUIManager.gs_style1);
			for (int k = 1; k < PlayerProfile.friendsRating.Length; k++)
			{
				if (!(PlayerProfile.friendsRating[k] == ""))
				{
					string[] array = PlayerProfile.friendsRating[k].Split(new char[]
					{
						'|'
					});
					if (array[1] == PlayerProfile.id)
					{
						GUI.DrawTexture(new Rect(0f, (float)(k * 20 + num6), 512f, 20f), GUIManager.tex_half_yellow);
					}
					GUIManager.gs_style1.normal.textColor = new Color(1f, 1f, 0f, 1f);
					GUI.Label(new Rect((float)num5, (float)(k * 20 + num6), 100f, 20f), k.ToString(), GUIManager.gs_style1);
					GUIManager.gs_style1.normal.textColor = new Color(1f, 1f, 1f, 1f);
					GUI.Label(new Rect((float)(num5 + 77), (float)(k * 20 + num6), 100f, 20f), array[2], GUIManager.gs_style1);
					GUI.Label(new Rect((float)(num5 + 367), (float)(k * 20 + num6), 100f, 20f), array[0], GUIManager.gs_style1);
				}
			}
			GUIManager.EndScrollView();
		}
		GUIManager.gs_style1.normal.textColor = textColor;
		GUIManager.gs_style1.fontSize = fontSize;
		GUIManager.gs_style1.alignment = alignment;
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x0000CEDC File Offset: 0x0000B0DC
	public void onActive()
	{
		if (Time.time < this.lastupdate + 5f)
		{
			return;
		}
		this.lastupdate = Time.time;
		if (PlayerProfile.network == NETWORK.VK)
		{
			this.mode = 3;
			Application.ExternalCall("get_friends", new object[]
			{
				""
			});
			base.StartCoroutine(Handler.GetFriendsRating());
			return;
		}
		this.mode = 0;
		base.StartCoroutine(this.get_playerstats());
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x0000CF50 File Offset: 0x0000B150
	private IEnumerator get_playerstats()
	{
		this.myvkid = PlayerProfile.id;
		string text = string.Concat(new object[]
		{
			CONST.HANDLER_SERVER,
			"8&id=",
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
				'^'
			});
			this.count = array.Length - 1;
			for (int i = 0; i < this.count; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					'|'
				});
				this.info_name[i] = "[" + array2[6] + "]" + array2[1];
				this.info_frags[i] = array2[3];
				this.info_deaths[i] = array2[4];
				this.info_exp[i] = array2[5];
				this.info_vkid[i] = array2[2];
			}
		}
		yield break;
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x0000CF5F File Offset: 0x0000B15F
	private IEnumerator get_clanstats()
	{
		this.myvkid = PlayerProfile.id;
		string text = string.Concat(new object[]
		{
			CONST.HANDLER_SERVER,
			"14&id=",
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
				'^'
			});
			this.clan_count = array.Length - 1;
			for (int i = 0; i < this.clan_count; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					'|'
				});
				this.clan_name[i] = array2[1];
				this.clan_creatorname[i] = array2[2];
				this.clan_level[i] = array2[3];
				this.clan_exp[i] = array2[4];
			}
		}
		yield break;
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x0000CF70 File Offset: 0x0000B170
	private void DrawMode(string name, int x, int y, int id)
	{
		float num = Input.mousePosition.x;
		float num2 = (float)Screen.height - Input.mousePosition.y;
		num -= (float)Screen.width / 2f - 300f;
		num2 -= 192f;
		Rect rect;
		rect..ctor((float)x, (float)y, 128f, 32f);
		if (this.mode != id)
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
		if (this.mode == id)
		{
			GUI.DrawTexture(new Rect((float)x, (float)y, 128f, 32f), GUIManager.tex_select);
		}
		else if (this.hovermode[id])
		{
			GUI.DrawTexture(new Rect((float)x, (float)y, 128f, 32f), GUIManager.tex_hover);
		}
		GUIManager.DrawText(rect, name, 20, 4, 8);
		if (GUI.Button(rect, "", GUIManager.gs_style1))
		{
			MainMenu.MainAS.PlayOneShot(SoundManager.SoundClick, AudioListener.volume);
			this.mode = id;
			if (this.mode == 0 && this.count == 0)
			{
				base.StartCoroutine(this.get_playerstats());
				return;
			}
			if (this.mode == 1 && this.clan_count == 0)
			{
				base.StartCoroutine(this.get_clanstats());
				return;
			}
			if (this.mode == 3)
			{
				base.StartCoroutine(Handler.GetFriendsRating());
			}
		}
	}

	// Token: 0x040000B8 RID: 184
	public bool Active;

	// Token: 0x040000B9 RID: 185
	private bool[] hovermode = new bool[4];

	// Token: 0x040000BA RID: 186
	private int mode;

	// Token: 0x040000BB RID: 187
	private int x_pos;

	// Token: 0x040000BC RID: 188
	private int y_pos;

	// Token: 0x040000BD RID: 189
	private string[] info_name = new string[100];

	// Token: 0x040000BE RID: 190
	private string[] info_frags = new string[100];

	// Token: 0x040000BF RID: 191
	private string[] info_deaths = new string[100];

	// Token: 0x040000C0 RID: 192
	private string[] info_exp = new string[100];

	// Token: 0x040000C1 RID: 193
	private string[] info_vkid = new string[100];

	// Token: 0x040000C2 RID: 194
	private int count;

	// Token: 0x040000C3 RID: 195
	private float lastupdate = -5f;

	// Token: 0x040000C4 RID: 196
	private Vector2 scrollViewVector = Vector2.zero;

	// Token: 0x040000C5 RID: 197
	private string myvkid = "0";

	// Token: 0x040000C6 RID: 198
	private string[] clan_name = new string[100];

	// Token: 0x040000C7 RID: 199
	private string[] clan_creatorname = new string[100];

	// Token: 0x040000C8 RID: 200
	private string[] clan_level = new string[100];

	// Token: 0x040000C9 RID: 201
	private string[] clan_exp = new string[100];

	// Token: 0x040000CA RID: 202
	private int clan_count;
}
