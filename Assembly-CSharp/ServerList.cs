using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x02000020 RID: 32
public class ServerList : MonoBehaviour
{
	// Token: 0x060000E8 RID: 232 RVA: 0x00013D8C File Offset: 0x00011F8C
	private void Awake()
	{
		ServerList.THIS = this;
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x00013D94 File Offset: 0x00011F94
	private void myGlobalInit()
	{
		for (int i = 0; i < 13; i++)
		{
			this.gamemode[i] = false;
		}
		this.gamemode[0] = true;
		this.gamemode[2] = true;
		this.gamemode[3] = true;
		this.gamemode[4] = true;
		this.gamemode[5] = true;
		this.gamemode[6] = true;
		this.gamemode[7] = true;
		this.filtercountry[0] = false;
		this.filtercountry[1] = false;
		this.filtercountry[2] = false;
		this.filtercountryBTN[0] = false;
		this.filtercountryBTN[1] = false;
		this.filtercountryBTN[2] = false;
		if (PlayerProfile.network == NETWORK.FB || PlayerProfile.network == NETWORK.KG)
		{
			this.filtercountryBTN[0] = true;
			this.filtercountryBTN[1] = true;
		}
		for (int j = 0; j < 13; j++)
		{
			this.hoverleftmenu[j] = false;
		}
		base.StartCoroutine(this.update_stats());
	}

	// Token: 0x060000EA RID: 234 RVA: 0x00013E71 File Offset: 0x00012071
	private IEnumerator update_stats()
	{
		yield break;
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00013E79 File Offset: 0x00012079
	public void refresh_servers()
	{
		this.lastupdate = Time.time;
		base.StartCoroutine(this.get_server_stats());
	}

	// Token: 0x060000EC RID: 236 RVA: 0x00013E93 File Offset: 0x00012093
	private IEnumerator get_server_stats()
	{
		string text = CONST.HANDLER_SERVER_LIST + "4&time" + DateTime.Now.Second;
		WWW www = new WWW(text, null, new Dictionary<string, string>
		{
			{
				"User-Agent",
				"Mozilla / 5.0(Windows NT 10.0; WOW64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 55.0.2883.87 Safari / 537.36"
			}
		});
		yield return www;
		if (www.error == null)
		{
			this.srvlist.Clear();
			this.modelist.Clear();
			string[] array = www.text.Split(new char[]
			{
				'^'
			});
			for (int i = 0; i < array.Length - 1; i++)
			{
				if (!(array[i] == ""))
				{
					string[] array2 = array[i].Split(new char[]
					{
						'|'
					});
					int num;
					int.TryParse(array2[0], out num);
					int num2;
					int.TryParse(array2[1], out num2);
					string ip = array2[2];
					int port;
					int.TryParse(array2[3], out port);
					int players;
					int.TryParse(array2[4], out players);
					int maxplayers;
					int.TryParse(array2[5], out maxplayers);
					string map = array2[6];
					ulong adminid;
					ulong.TryParse(array2[7], out adminid);
					int password;
					int.TryParse(array2[8], out password);
					int country_id;
					int.TryParse(array2[9], out country_id);
					if (!this.srvlist.ContainsKey(num2))
					{
						this.srvlist[num2] = new List<CServerData>();
						this.modelist.Add(num2);
					}
					this.srvlist[num2].Add(new CServerData(num, num2, players, maxplayers, map, adminid, ip, port, password, country_id));
				}
			}
			this.modelist.Sort();
			this.next_update = Time.time + (float)CONST.CFG.SERVER_UPDATE_TIMEOUT;
			yield return new WaitForSeconds((float)CONST.CFG.SERVER_UPDATE_TIMEOUT);
			base.StartCoroutine(this.get_server_stats());
		}
		else
		{
			Debug.Log(www.error);
			this.next_update = Time.time + (float)CONST.CFG.SERVER_UPDATE_TIMEOUT / 2f;
			yield return new WaitForSeconds((float)CONST.CFG.SERVER_UPDATE_TIMEOUT / 2f);
			base.StartCoroutine(this.get_server_stats());
		}
		if (this._get_stats)
		{
			yield break;
		}
		base.StartCoroutine(this.get_stats());
		yield break;
	}

	// Token: 0x060000ED RID: 237 RVA: 0x00013EA2 File Offset: 0x000120A2
	private IEnumerator get_stats()
	{
		if (this._get_stats)
		{
			yield break;
		}
		string text = string.Concat(new object[]
		{
			CONST.HANDLER_SERVER,
			"0&id=",
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
			int i;
			int.TryParse(www.text.Split(new char[]
			{
				'|'
			})[2], out i);
			int num = 1;
			int num2 = 1;
			while (i >= (num * (num + 1) * (num + 2) + 15 * num) * 10)
			{
				num++;
				num2++;
			}
			PlayerProfile.level = num2;
			this._get_stats = true;
			base.StartCoroutine(this.get_bonus_lvl());
		}
		yield break;
	}

	// Token: 0x060000EE RID: 238 RVA: 0x00013EB1 File Offset: 0x000120B1
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

	// Token: 0x060000EF RID: 239 RVA: 0x00013EBC File Offset: 0x000120BC
	private void OnGUI()
	{
		if (!this.Active)
		{
			return;
		}
		if (GameController.STATE != GAME_STATES.MAINMENU)
		{
			return;
		}
		if (GM.currGUIState != GUIGS.SERVERLIST)
		{
			return;
		}
		GUI.Window(900, new Rect((float)Screen.width / 2f - 300f, 199f, 600f, (float)(Screen.height - 180)), new GUI.WindowFunction(this.DoWindow), "", GUIManager.gs_empty);
		float num = this.next_update - Time.time;
		if (num < 0f)
		{
			num = 0f;
		}
		GUIManager.DrawText(new Rect(10f, (float)(Screen.height - 40), (float)(Screen.width - 20), 30f), Lang.GetLabel(443) + ((int)num).ToString() + "...", 12, 6, 8);
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x00013F98 File Offset: 0x00012198
	private void DoWindow(int windowID)
	{
		GUI.DrawTexture(new Rect(0f, 0f, 600f, 32f), GUIManager.tex_panel);
		GUI.DrawTexture(new Rect(0f, 32f, 600f, (float)(Screen.height - 180)), GUIManager.tex_half_black);
		if (PlayerProfile.network == NETWORK.VK)
		{
			this.DrawMode(Lang.GetLabel(44), 108, 0, 0);
			this.DrawMode(Lang.GetLabel(45), 236, 0, 1);
			this.DrawMode(Lang.GetLabel(486), 364, 0, 2);
		}
		else
		{
			this.DrawMode(Lang.GetLabel(44), 172, 0, 0);
			this.DrawMode(Lang.GetLabel(45), 300, 0, 1);
		}
		GUI.DrawTexture(new Rect(0f, 32f, 160f, 26f), GUIManager.tex_menubar);
		GUIManager.DrawText(new Rect(0f, 32f, 160f, 26f), Lang.GetLabel(46), 16, 4, 8);
		Rect r;
		r..ctor(0f, 58f, 160f, 32f);
		if (this.DrawButton(r, GUIManager.tex_button_mode, GUIManager.select_glow, Lang.GetLabel(47), null, 255 == this.drawmode, false, 0))
		{
			this.drawmode = 255;
		}
		r.y += 32f;
		if (this.gamemode[0])
		{
			if (this.DrawButton(r, GUIManager.tex_button_mode, GUIManager.select_glow, Lang.GetLabel(48), GUIManager.gm0, this.drawmode == 0, 1 == this.drawmode, 1))
			{
				this.drawmode = 0;
			}
			r.y += 32f;
		}
		if (this.gamemode[2])
		{
			if (this.DrawButton(r, GUIManager.tex_button_mode, GUIManager.select_glow, Lang.GetLabel(49), GUIManager.gm2, 2 == this.drawmode, this.drawmode == 0, 2))
			{
				this.drawmode = 2;
			}
			r.y += 32f;
		}
		if (this.gamemode[3])
		{
			if (this.DrawButton(r, GUIManager.tex_button_mode, GUIManager.select_glow, Lang.GetLabel(50), GUIManager.gm3, 3 == this.drawmode, 2 == this.drawmode, 3))
			{
				this.drawmode = 3;
			}
			r.y += 32f;
		}
		if (this.gamemode[4])
		{
			if (this.DrawButton(r, GUIManager.tex_button_mode, GUIManager.select_glow, Lang.GetLabel(51), GUIManager.gm4, 4 == this.drawmode, 3 == this.drawmode, 4))
			{
				this.drawmode = 4;
			}
			r.y += 32f;
		}
		if (this.gamemode[5])
		{
			if (this.DrawButton(r, GUIManager.tex_button_mode, GUIManager.select_glow, Lang.GetLabel(52), GUIManager.gm5, 5 == this.drawmode, 4 == this.drawmode, 5))
			{
				this.drawmode = 5;
			}
			r.y += 32f;
		}
		if (this.gamemode[6])
		{
			if (this.DrawButton(r, GUIManager.tex_button_mode, GUIManager.select_glow, Lang.GetLabel(53), GUIManager.gm6, 6 == this.drawmode, 5 == this.drawmode, 6))
			{
				this.drawmode = 6;
			}
			r.y += 32f;
		}
		if (this.gamemode[7])
		{
			if (this.DrawButton(r, GUIManager.tex_button_mode, GUIManager.select_glow, Lang.GetLabel(54), GUIManager.gm7, 7 == this.drawmode, 6 == this.drawmode, 7))
			{
				this.drawmode = 7;
			}
			r.y += 32f;
		}
		if (this.gamemode[8])
		{
			if (this.DrawButton(r, GUIManager.tex_button_mode, GUIManager.select_glow, Lang.GetLabel(206), GUIManager.gm9, 9 == this.drawmode, 7 == this.drawmode, 9))
			{
				this.drawmode = 9;
			}
			r.y += 32f;
		}
		if (this.gamemode[9])
		{
			if (this.DrawButton(r, GUIManager.tex_button_mode, GUIManager.select_glow, Lang.GetLabel(207), GUIManager.gm10, 10 == this.drawmode, 9 == this.drawmode, 10))
			{
				this.drawmode = 10;
			}
			r.y += 32f;
		}
		if (this.gamemode[11])
		{
			if (this.DrawButton(r, GUIManager.tex_button_mode, GUIManager.select_glow, Lang.GetLabel(208), GUIManager.gm11, 11 == this.drawmode, 10 == this.drawmode, 11))
			{
				this.drawmode = 11;
			}
			r.y += 32f;
		}
		GUI.DrawTexture(new Rect(r.x, r.y, r.width, 26f), GUIManager.tex_menubar);
		GUIManager.DrawText(new Rect(r.x, r.y, r.width, 26f), Lang.GetLabel(55), 16, 4, 8);
		r.y += 26f;
		if (this.DrawButton(r, GUIManager.tex_button_mode, GUIManager.select_glow, Lang.GetLabel(56), null, 255 == this.drawfilter, false, 5))
		{
			this.drawfilter = 255;
			this.drawcountryfilter = 0;
		}
		r.y += 32f;
		if (this.DrawButton(r, GUIManager.tex_button_mode, GUIManager.select_glow, Lang.GetLabel(57), null, this.drawfilter == 0, 255 == this.drawfilter, 6))
		{
			this.drawfilter = 0;
		}
		r.y += 32f;
		if (this.filtercountryBTN[0])
		{
			if (this.DrawButton(r, GUIManager.tex_button_mode, GUIManager.select_glow, Lang.GetLabel(342), GUIManager.tex_flags_filter[0], 1 == this.drawcountryfilter, this.drawfilter == 0, 12))
			{
				this.drawcountryfilter = 1;
			}
			r.y += 32f;
		}
		if (this.filtercountryBTN[1])
		{
			if (this.DrawButton(r, GUIManager.tex_button_mode, GUIManager.select_glow, Lang.GetLabel(343), GUIManager.tex_flags_filter[1], 2 == this.drawcountryfilter, 1 == this.drawcountryfilter, 13))
			{
				this.drawcountryfilter = 2;
			}
			r.y += 32f;
		}
		if (this.filtercountryBTN[2])
		{
			if (this.DrawButton(r, GUIManager.tex_button_mode, GUIManager.select_glow, Lang.GetLabel(344), GUIManager.tex_flags_filter[2], 3 == this.drawcountryfilter, 2 == this.drawcountryfilter, 14))
			{
				this.drawcountryfilter = 3;
			}
			r.y += 32f;
		}
		GUI.DrawTexture(new Rect(0f, r.y, 160f, (float)(Screen.height - 180 - 52 + 320)), GUIManager.tex_half_black);
		this.y_pos = 10;
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < this.modelist.Count; i++)
		{
			if (this.srvlist.ContainsKey(this.modelist[i]))
			{
				for (int j = 0; j < this.srvlist[this.modelist[i]].Count; j++)
				{
					if (this.srvlist[this.modelist[i]][j].type == this.type)
					{
						if (this.type == 1 && this.srvlist[this.modelist[i]][j].adminid == 0UL)
						{
							num2++;
						}
						else if ((this.drawfilter == 255 || this.drawfilter != 0 || this.srvlist[this.modelist[i]][j].players != this.srvlist[this.modelist[i]][j].maxplayers) && (this.drawmode == 255 || this.srvlist[this.modelist[i]][j].gamemode == this.drawmode) && (this.drawcountryfilter == 0 || this.srvlist[this.modelist[i]][j].country_id == this.drawcountryfilter))
						{
							num++;
						}
					}
				}
			}
		}
		float num3 = (float)(num * 36 + 16);
		if (this.type == 1)
		{
			num3 += 40f;
		}
		this.scrollViewVector = GUIManager.BeginScrollView(new Rect(0f, 32f, 600f, GUIManager.YRES(768f) - 180f - 32f - 32f), this.scrollViewVector, new Rect(0f, 0f, 0f, num3));
		if (PlayerProfile.loh > 0)
		{
			GUI.DrawTexture(new Rect(178f, 10f, 384f, 32f), GUIManager.tex_warning);
			GUIManager.DrawText(new Rect(210f, 10f, 352f, 32f), Lang.GetLabel(58), 16, 4, 8);
		}
		else
		{
			if (this.type == 1)
			{
				this.DrawCreateServer();
				this.y_pos += 40;
			}
			if (this.type == 2)
			{
				if (GameController.STATE == GAME_STATES.GETFRIENDSONLINECOMLITE)
				{
					for (int k = 1; k < PlayerProfile.friendsOnlineServers.Length; k++)
					{
						if (PlayerProfile.friendsOnlineServers[k] != "")
						{
							string[] array = PlayerProfile.friendsOnlineServers[k].Split(new char[]
							{
								'|'
							});
							int num4 = 0;
							int.TryParse(array[2], out num4);
							int key = 0;
							int.TryParse(array[3], out key);
							if (this.srvlist.ContainsKey(key))
							{
								for (int l = 0; l < this.srvlist[key].Count; l++)
								{
									if (this.srvlist[key][l].port == num4 && !(this.srvlist[key][l].ip != array[1]) && (this.drawfilter == 255 || this.drawfilter != 0 || this.srvlist[key][l].players != this.srvlist[key][l].maxplayers) && (this.drawmode == 255 || this.srvlist[key][l].gamemode == this.drawmode) && this.srvlist[key][l].map_size != 2 && this.srvlist[key][l].map_size != 1 && this.srvlist[key][l].map_size != 0)
									{
										this.DrawFriendServer(array, this.srvlist[key][l]);
										break;
									}
								}
							}
						}
					}
				}
				else
				{
					GUI.DrawTexture(new Rect(178f, 10f, 384f, 32f), GUIManager.tex_warning);
					GUIManager.DrawText(new Rect(210f, 10f, 352f, 32f), Lang.GetLabel(487), 16, 4, 8);
				}
			}
			else if (this.drawmode == 1 && PlayerProfile.skin != 59 && PlayerProfile.skin != 56 && PlayerProfile.skin != 57 && PlayerProfile.skin != 58 && PlayerProfile.skin != 148 && PlayerProfile.skin != 227 && PlayerProfile.skin != 228 && PlayerProfile.skin != 229 && PlayerProfile.skin != 149 && PlayerProfile.skin != 150 && PlayerProfile.skin != 253 && PlayerProfile.skin != 312)
			{
				GUI.DrawTexture(new Rect(178f, 10f, 384f, 54f), GUIManager.tex_warning2);
				GUIManager.DrawText(new Rect(210f, 10f, 352f, 54f), Lang.GetLabel(540), 16, 4, 8);
				this.y_pos += 72;
			}
			else
			{
				for (int m = 0; m < this.modelist.Count; m++)
				{
					if (this.srvlist.ContainsKey(this.modelist[m]))
					{
						for (int n = 0; n < this.srvlist[this.modelist[m]].Count; n++)
						{
							if (this.gamemode[this.srvlist[this.modelist[m]][n].gamemode] && this.srvlist[this.modelist[m]][n].type == this.type && (this.type != 1 || this.srvlist[this.modelist[m]][n].adminid != 0UL) && (this.drawfilter == 255 || this.drawfilter != 0 || this.srvlist[this.modelist[m]][n].players != this.srvlist[this.modelist[m]][n].maxplayers) && (this.drawmode == 255 || this.srvlist[this.modelist[m]][n].gamemode == this.drawmode) && (this.drawcountryfilter == 0 || this.srvlist[this.modelist[m]][n].country_id == this.drawcountryfilter))
							{
								this.DrawServer(this.srvlist[this.modelist[m]][n]);
							}
						}
					}
				}
			}
		}
		if (this.type == 0 && this.drawmode == 2)
		{
			GUI.DrawTexture(new Rect(178f, 10f, 384f, 32f), GUIManager.tex_warning);
			GUIManager.DrawText(new Rect(210f, 10f, 352f, 32f), Lang.GetLabel(59), 16, 4, 8);
		}
		else if (this.type == 0)
		{
			int num5 = this.drawmode;
		}
		GUIManager.EndScrollView();
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x00014F28 File Offset: 0x00013128
	private void DrawServer(CServerData server)
	{
		float num = Input.mousePosition.x;
		float num2 = (float)Screen.height - Input.mousePosition.y;
		num -= (float)Screen.width / 2f - 300f;
		num2 -= (float)(192 - (int)this.scrollViewVector.y + 32 + 10);
		Rect rect;
		rect..ctor(178f, (float)this.y_pos, 384f, 32f);
		Rect rect2;
		rect2..ctor(193f, (float)this.y_pos, 32f, 32f);
		Rect r;
		r..ctor(232f, (float)this.y_pos, 200f, 32f);
		Rect r2;
		r2..ctor(470f, (float)this.y_pos, 80f, 32f);
		if (!PopUp.Active)
		{
			if (rect.Contains(new Vector2(num, num2)))
			{
				if (!server.hover)
				{
					server.hover = true;
				}
			}
			else if (server.hover)
			{
				server.hover = false;
			}
			bool hover = server.hover;
			if (GUI.Button(rect, "", GUIManager.gs_empty))
			{
				MainMenu.MainAS.PlayOneShot(SoundManager.SoundClick, AudioListener.volume);
				if (server.password > 0)
				{
					base.gameObject.GetComponent<PasswordGUI>().Show(true, server);
				}
				else
				{
					ConnectionInfo.PRIVATE = (server.type == 1);
					ConnectionInfo.IP = server.ip;
					ConnectionInfo.PORT = server.port;
					ConnectionInfo.HOSTNAME = server.name;
					ConnectionInfo.mode = server.gamemode;
					GameController.STATE = GAME_STATES.GAME;
					GM.currExtState = GAME_STATES.NULL;
					SceneManager.LoadScene(1);
				}
			}
		}
		if (server.password > 0)
		{
			GUI.color = new Color(1f, 0.7f, 0.7f, 1f);
		}
		if (server.hover)
		{
			GUI.DrawTexture(rect, GUIManager.tex_server_hover);
		}
		else
		{
			GUI.DrawTexture(rect, GUIManager.tex_server);
		}
		GUI.color = new Color(1f, 1f, 1f, 1f);
		if (server.gamemode == 0)
		{
			GUI.DrawTexture(rect2, GUIManager.gm0);
		}
		else if (server.gamemode == 1)
		{
			GUI.DrawTexture(rect2, GUIManager.gm1);
		}
		else if (server.gamemode == 2)
		{
			GUI.DrawTexture(rect2, GUIManager.gm2);
		}
		else if (server.gamemode == 3)
		{
			GUI.DrawTexture(rect2, GUIManager.gm3);
		}
		else if (server.gamemode == 4)
		{
			GUI.DrawTexture(rect2, GUIManager.gm4);
		}
		else if (server.gamemode == 5)
		{
			GUI.DrawTexture(rect2, GUIManager.gm5);
		}
		else if (server.gamemode == 6)
		{
			GUI.DrawTexture(rect2, GUIManager.gm6);
		}
		else if (server.gamemode == 7)
		{
			GUI.DrawTexture(rect2, GUIManager.gm7);
		}
		else if (server.gamemode == 8)
		{
			GUI.DrawTexture(rect2, GUIManager.gm8);
		}
		else if (server.gamemode == 9)
		{
			GUI.DrawTexture(rect2, GUIManager.gm9);
		}
		else if (server.gamemode == 10)
		{
			GUI.DrawTexture(rect2, GUIManager.gm10);
		}
		else if (server.gamemode == 11)
		{
			GUI.DrawTexture(rect2, GUIManager.gm11);
		}
		if ((PlayerProfile.network == NETWORK.FB || PlayerProfile.network == NETWORK.KG) && server.type == 0)
		{
			GUI.DrawTexture(new Rect(r.x, r.y + 10f, 16f, 11f), GUIManager.tex_flags[server.country_id]);
			string[] array = server.name.Split(new char[]
			{
				'#'
			});
			if (Lang.current == 0)
			{
				if (array[0] == "BATTLE")
				{
					array[0] = "БИТВА";
				}
				else if (array[0] == "ZOMBIE")
				{
					array[0] = "ЗОМБИ";
				}
				else if (array[0] == "CAPTURE")
				{
					array[0] = "ЗАХВАТ";
				}
				else if (array[0] == "VERSUS")
				{
					array[0] = "КОНТРА";
				}
				else if (array[0] == "TANKS")
				{
					array[0] = "ТАНКИ";
				}
				else if (array[0] == "CARNAGE")
				{
					array[0] = "РЕЗНЯ";
				}
				else if (array[0] == "SURVIVAL")
				{
					array[0] = "ВЫЖИВАНИЕ";
				}
				else if (array[0] == "BREAK POINT")
				{
					array[0] = "ПРОРЫВ";
				}
				else if (array[0] == "SNOWBALLS")
				{
					array[0] = "СНЕЖКИ";
				}
			}
			GUIManager.DrawText(new Rect(r.x + 24f, r.y, r.width - 24f, r.height), array[0] + "#" + array[1], 16, 3, 8);
		}
		else
		{
			GUIManager.DrawText(r, server.name, 16, 3, 8);
		}
		GUI.color = new Color(0.8f, 0.8f, 0.8f, 1f);
		GUIManager.DrawText(r2, server.players + "/" + server.maxplayers, 20, 4, 8);
		GUI.color = new Color(1f, 1f, 1f, 1f);
		this.y_pos += 36;
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x0001548C File Offset: 0x0001368C
	private void DrawFriendServer(string[] _fInfo, CServerData server)
	{
		float num = Input.mousePosition.x;
		float num2 = (float)Screen.height - Input.mousePosition.y;
		num -= (float)Screen.width / 2f - 300f;
		num2 -= (float)(192 - (int)this.scrollViewVector.y + 32 + 10);
		Rect rect;
		rect..ctor(178f, (float)this.y_pos, 384f, 32f);
		Rect rect2;
		rect2..ctor(193f, (float)this.y_pos, 32f, 32f);
		Rect rect3;
		rect3..ctor(232f, (float)this.y_pos, 200f, 32f);
		Rect r;
		r..ctor(470f, (float)this.y_pos, 80f, 32f);
		if (!PopUp.Active)
		{
			if (rect.Contains(new Vector2(num, num2)))
			{
				if (!server.hover)
				{
					server.hover = true;
				}
			}
			else if (server.hover)
			{
				server.hover = false;
			}
			bool hover = server.hover;
			if (GUI.Button(rect, "", GUIManager.gs_empty))
			{
				MainMenu.MainAS.PlayOneShot(SoundManager.SoundClick, AudioListener.volume);
				if (server.password > 0)
				{
					base.gameObject.GetComponent<PasswordGUI>().Show(true, server);
				}
				else
				{
					ConnectionInfo.PRIVATE = (server.type == 1);
					ConnectionInfo.IP = server.ip;
					ConnectionInfo.PORT = server.port;
					ConnectionInfo.HOSTNAME = server.name;
					ConnectionInfo.mode = server.gamemode;
					GameController.STATE = GAME_STATES.GAME;
					GM.currExtState = GAME_STATES.NULL;
					SceneManager.LoadScene(1);
				}
			}
		}
		if (server.password > 0)
		{
			GUI.color = new Color(1f, 0.7f, 0.7f, 1f);
		}
		if (server.hover)
		{
			GUI.DrawTexture(rect, GUIManager.tex_server_hover);
		}
		else
		{
			GUI.DrawTexture(rect, GUIManager.tex_server);
		}
		GUI.color = new Color(1f, 1f, 1f, 1f);
		if (server.gamemode == 0)
		{
			GUI.DrawTexture(rect2, GUIManager.gm0);
		}
		else if (server.gamemode == 1)
		{
			GUI.DrawTexture(rect2, GUIManager.gm1);
		}
		else if (server.gamemode == 2)
		{
			GUI.DrawTexture(rect2, GUIManager.gm2);
		}
		else if (server.gamemode == 3)
		{
			GUI.DrawTexture(rect2, GUIManager.gm3);
		}
		else if (server.gamemode == 4)
		{
			GUI.DrawTexture(rect2, GUIManager.gm4);
		}
		else if (server.gamemode == 5)
		{
			GUI.DrawTexture(rect2, GUIManager.gm5);
		}
		else if (server.gamemode == 6)
		{
			GUI.DrawTexture(rect2, GUIManager.gm6);
		}
		else if (server.gamemode == 7)
		{
			GUI.DrawTexture(rect2, GUIManager.gm7);
		}
		else if (server.gamemode == 8)
		{
			GUI.DrawTexture(rect2, GUIManager.gm8);
		}
		else if (server.gamemode == 9)
		{
			GUI.DrawTexture(rect2, GUIManager.gm9);
		}
		else if (server.gamemode == 10)
		{
			GUI.DrawTexture(rect2, GUIManager.gm10);
		}
		else if (server.gamemode == 11)
		{
			GUI.DrawTexture(rect2, GUIManager.gm11);
		}
		if (PlayerProfile.network == NETWORK.FB || PlayerProfile.network == NETWORK.KG)
		{
			GUI.DrawTexture(new Rect(rect3.x, rect3.y + 10f, 16f, 11f), GUIManager.tex_flags[server.country_id]);
			string[] array = server.name.Split(new char[]
			{
				'#'
			});
			if (Lang.current == 0)
			{
				if (array[0] == "BATTLE")
				{
					array[0] = "БИТВА";
				}
				else if (array[0] == "ZOMBIE")
				{
					array[0] = "ЗОМБИ";
				}
				else if (array[0] == "CAPTURE")
				{
					array[0] = "ЗАХВАТ";
				}
				else if (array[0] == "VERSUS")
				{
					array[0] = "КОНТРА";
				}
				else if (array[0] == "TANKS")
				{
					array[0] = "ТАНКИ";
				}
				else if (array[0] == "CARNAGE")
				{
					array[0] = "РЕЗНЯ";
				}
				else if (array[0] == "SURVIVAL")
				{
					array[0] = "ВЫЖИВАНИЕ";
				}
				else if (array[0] == "SNOWBALLS")
				{
					array[0] = "СНЕЖКИ";
				}
			}
			GUIManager.DrawText(new Rect(rect3.x + 24f, rect3.y, rect3.width - 24f, rect3.height), array[0] + "#" + array[1], 16, 3, 8);
		}
		else
		{
			GUI.DrawTexture(new Rect(rect3.x, rect3.y + 10f, 16f, 11f), GUIManager.tex_flags[server.country_id]);
			string[] array2 = server.name.Split(new char[]
			{
				'#'
			});
			if (Lang.current == 0)
			{
				if (array2[0] == "BATTLE")
				{
					array2[0] = "БИТВА";
				}
				else if (array2[0] == "ZOMBIE")
				{
					array2[0] = "ЗОМБИ";
				}
				else if (array2[0] == "CAPTURE")
				{
					array2[0] = "ЗАХВАТ";
				}
				else if (array2[0] == "VERSUS")
				{
					array2[0] = "КОНТРА";
				}
				else if (array2[0] == "TANKS")
				{
					array2[0] = "ТАНКИ";
				}
				else if (array2[0] == "CARNAGE")
				{
					array2[0] = "РЕЗНЯ";
				}
				else if (array2[0] == "SURVIVAL")
				{
					array2[0] = "ВЫЖИВАНИЕ";
				}
			}
			GUIManager.DrawText(new Rect(rect3.x + 24f, rect3.y, rect3.width - 24f, rect3.height), array2[0] + "#" + array2[1], 16, 3, 8);
		}
		GUI.color = new Color(0.8f, 0.8f, 0.8f, 1f);
		GUIManager.DrawText(r, server.players + "/" + server.maxplayers, 20, 4, 8);
		GUI.color = new Color(1f, 1f, 1f, 1f);
		this.y_pos += 36;
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x00015B18 File Offset: 0x00013D18
	private void DrawMode(string name, int x, int y, int id)
	{
		float num = Input.mousePosition.x;
		float num2 = (float)Screen.height - Input.mousePosition.y;
		num -= (float)Screen.width / 2f - 300f;
		num2 -= 192f;
		Rect rect;
		rect..ctor((float)x, (float)y, 128f, 32f);
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
			GUI.DrawTexture(new Rect((float)x, (float)y, 128f, 32f), GUIManager.tex_select);
		}
		else if (this.hovermode[id])
		{
			GUI.DrawTexture(new Rect((float)x, (float)y, 128f, 32f), GUIManager.tex_hover);
		}
		GUIManager.DrawText(rect, name, 20, 4, 8);
		if (GUI.Button(rect, "", GUIManager.gs_empty))
		{
			MainMenu.MainAS.PlayOneShot(SoundManager.SoundClick, AudioListener.volume);
			this.type = id;
			this.drawmode = 255;
			int num3 = this.type;
			if (this.lastupdate + (float)CONST.CFG.SERVER_UPDATE_TIMEOUT < Time.time)
			{
				this.refresh_servers();
			}
		}
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x00015C88 File Offset: 0x00013E88
	private void DrawCreateServer()
	{
		float num = Input.mousePosition.x;
		float num2 = (float)Screen.height - Input.mousePosition.y;
		num -= (float)Screen.width / 2f - 300f;
		num2 -= (float)(192 - (int)this.scrollViewVector.y + 39);
		Rect rect;
		rect..ctor(280f, 4f, 192f, 32f);
		if (rect.Contains(new Vector2(num, num2)))
		{
			if (!this.bh)
			{
				this.bh = true;
				MainMenu.MainAS.PlayOneShot(SoundManager.SoundHover, AudioListener.volume);
			}
		}
		else if (this.bh)
		{
			this.bh = false;
		}
		if (this.bh)
		{
			rect.y -= 2f;
		}
		GUI.DrawTexture(rect, GUIManager.tex_button_hover);
		GUIManager.DrawText(rect, Lang.GetLabel(61), 16, 4, 8);
		if (Time.time - this.request < 5f)
		{
			return;
		}
		if (GUI.Button(rect, "", GUIManager.gs_empty))
		{
			this.request = Time.time;
			if (PlayerProfile.premium == 1)
			{
				base.StartCoroutine(this.create_server());
				return;
			}
			PopUp.ShowBonus(4, 0);
		}
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x00015DC4 File Offset: 0x00013FC4
	private IEnumerator create_server()
	{
		string text = string.Concat(new object[]
		{
			CONST.HANDLER_SERVER_LIST,
			"16&id=",
			PlayerProfile.id.ToString(),
			"&key=",
			PlayerProfile.authkey,
			"&session=",
			PlayerProfile.session,
			"&time=",
			DateTime.Now.Second
		});
		WWW www = new WWW(text, null, new Dictionary<string, string>
		{
			{
				"User-Agent",
				"Mozilla / 5.0(Windows NT 10.0; WOW64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 55.0.2883.87 Safari / 537.36"
			}
		});
		yield return www;
		if (www.error == null)
		{
			string[] array = www.text.Split(new char[]
			{
				'|'
			});
			if (array.Length <= 1)
			{
				yield break;
			}
			int port = 0;
			int mode = 0;
			int.TryParse(array[1], out port);
			int.TryParse(array[2], out mode);
			MainMenu.MainAS.PlayOneShot(SoundManager.SoundClick, AudioListener.volume);
			ConnectionInfo.PRIVATE = true;
			ConnectionInfo.IP = array[0];
			ConnectionInfo.PORT = port;
			ConnectionInfo.HOSTNAME = "";
			ConnectionInfo.mode = mode;
			GameController.STATE = GAME_STATES.GAME;
			GM.currExtState = GAME_STATES.NULL;
			SceneManager.LoadScene(1);
		}
		yield break;
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x00015DCC File Offset: 0x00013FCC
	private bool DrawButton(Rect r, Texture2D tex, Texture2D tex2, string text, Texture2D icon, bool sel, bool presel, int id)
	{
		bool result = false;
		if (GUI.Button(r, "", GUIManager.gs_style3))
		{
			result = true;
		}
		if (sel)
		{
			tex = tex2;
			GUI.DrawTexture(new Rect(r.x - 15f, r.y - 16f, 190f, 64f), tex);
		}
		else
		{
			GUI.DrawTexture(r, tex);
		}
		if (presel)
		{
			GUI.DrawTexture(new Rect(r.x - 15f, r.y, 190f, 16f), GUIManager.hover_part_glow);
		}
		if (!sel)
		{
			float num = Input.mousePosition.x;
			float num2 = (float)Screen.height - Input.mousePosition.y;
			num -= (float)Screen.width / 2f - 300f;
			num2 -= 199f;
			if (r.Contains(new Vector2(num, num2)))
			{
				if (!this.hoverleftmenu[id])
				{
					this.hoverleftmenu[id] = true;
				}
			}
			else if (this.hoverleftmenu[id])
			{
				this.hoverleftmenu[id] = false;
			}
			if (this.hoverleftmenu[id])
			{
				GUI.DrawTexture(new Rect(r.x - 28f, r.y - 16f, 64f, 64f), GUIManager.hover_glow);
				r.x += 8f;
			}
		}
		if (icon)
		{
			if (id == 8)
			{
				GUI.DrawTexture(new Rect(r.x, r.y, 160f, 32f), icon);
			}
			else
			{
				GUI.DrawTexture(new Rect(r.x + 4f, r.y, 32f, 32f), icon);
				GUIManager.DrawText(new Rect(r.x + 40f, r.y, r.width - 40f, r.height), text, 16, 3, 8);
			}
		}
		else
		{
			GUIManager.DrawText(new Rect(r.x + 12f, r.y, r.width - 12f, r.height), text, 16, 3, 8);
		}
		return result;
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x00016004 File Offset: 0x00014204
	public void ShuffleList<T>(List<T> list)
	{
		Random random = new Random();
		for (int i = 0; i < list.Count; i++)
		{
			T item = list[i];
			list.RemoveAt(i);
			list.Insert(random.Next(0, list.Count), item);
		}
	}

	// Token: 0x0400010C RID: 268
	public static ServerList THIS;

	// Token: 0x0400010D RID: 269
	public bool Active = true;

	// Token: 0x0400010E RID: 270
	private int drawmode = 255;

	// Token: 0x0400010F RID: 271
	private int drawfilter = 255;

	// Token: 0x04000110 RID: 272
	private int drawcountryfilter;

	// Token: 0x04000111 RID: 273
	private int type;

	// Token: 0x04000112 RID: 274
	private bool[] hovermode = new bool[3];

	// Token: 0x04000113 RID: 275
	private bool[] hoverleftmenu = new bool[15];

	// Token: 0x04000114 RID: 276
	private bool[] filtercountry = new bool[3];

	// Token: 0x04000115 RID: 277
	private bool[] filtercountryBTN = new bool[3];

	// Token: 0x04000116 RID: 278
	private Dictionary<int, List<CServerData>> srvlist = new Dictionary<int, List<CServerData>>();

	// Token: 0x04000117 RID: 279
	private List<int> modelist = new List<int>();

	// Token: 0x04000118 RID: 280
	private float lastupdate;

	// Token: 0x04000119 RID: 281
	private bool _get_stats;

	// Token: 0x0400011A RID: 282
	private float next_update;

	// Token: 0x0400011B RID: 283
	private bool[] gamemode = new bool[13];

	// Token: 0x0400011C RID: 284
	private int x_pos;

	// Token: 0x0400011D RID: 285
	private int y_pos;

	// Token: 0x0400011E RID: 286
	private Vector2 scrollViewVector = Vector2.zero;

	// Token: 0x0400011F RID: 287
	private bool bh;

	// Token: 0x04000120 RID: 288
	private float request = -5f;
}
