using System;
using UnityEngine;

// Token: 0x02000018 RID: 24
public class MainMenu : MonoBehaviour
{
	// Token: 0x0600008B RID: 139 RVA: 0x00007A8C File Offset: 0x00005C8C
	private void myGlobalInit()
	{
		MainMenu.MainAS = base.GetComponent<AudioSource>();
		Cursor.visible = true;
		Cursor.lockState = 0;
		AudioListener.volume = 1f;
		this.menu[0] = true;
		for (int i = 0; i < 10; i++)
		{
			this.menu_hover[i] = false;
		}
		this.SetSound(Config.menuvolume > 0.1f);
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00007AEB File Offset: 0x00005CEB
	private void Start()
	{
		MainMenu.MainAS = base.GetComponent<AudioSource>();
	}

	// Token: 0x0600008D RID: 141 RVA: 0x00007AF8 File Offset: 0x00005CF8
	private void OnGUI()
	{
		if (GameController.STATE == GAME_STATES.BREAK || GameController.STATE == GAME_STATES.STEAMINITERROR)
		{
			GUI.DrawTexture(new Rect((float)(Screen.width / 2 - this.SteamError.width / 2), (float)(Screen.height / 2 - this.SteamError.height / 2), (float)this.SteamError.width, (float)this.SteamError.height), this.SteamError);
			return;
		}
		if (GameController.STATE == GAME_STATES.AUTH_ERROR)
		{
			GUI.DrawTexture(new Rect((float)(Screen.width / 2 - this.AuthError.width / 2), (float)(Screen.height / 2 - this.AuthError.height / 2), (float)this.AuthError.width, (float)this.AuthError.height), this.AuthError);
			return;
		}
		if (GameController.STATE < GAME_STATES.LOADING_BUNDLES || !GUIManager.IsReady)
		{
			if (Lang.current == 0)
			{
				GUI.DrawTexture(new Rect((float)(Screen.width - this.preLoaderRu.width), (float)(Screen.height - this.preLoaderRu.height), (float)this.preLoaderRu.width, (float)this.preLoaderRu.height), this.preLoaderRu);
				return;
			}
			GUI.DrawTexture(new Rect((float)(Screen.width - this.preLoaderEn.width), (float)(Screen.height - this.preLoaderEn.height), (float)this.preLoaderEn.width, (float)this.preLoaderEn.height), this.preLoaderEn);
			return;
		}
		else
		{
			if (GUIManager.logo == null)
			{
				return;
			}
			if (GM.currGUIState == GUIGS.RULES)
			{
				this.DrawRules();
				return;
			}
			if (GM.currGUIState == GUIGS.QUEST)
			{
				return;
			}
			Vector3 mousePosition = Input.mousePosition;
			int height = Screen.height;
			Vector3 mousePosition2 = Input.mousePosition;
			if (PlayerProfile.premium > 0)
			{
				GUI.DrawTexture(new Rect(0f, 60f, 128f, 32f), GUIManager.tex_icon_premium);
			}
			GUI.DrawTexture(new Rect(0f, 0f, GUIManager.XRES(1024f), 40f), GUIManager.tex_half_black);
			GUI.DrawTexture(new Rect((float)Screen.width / 2f - (float)((GUIManager.logo.width - 30) / 2), 24f, (float)(GUIManager.logo.width - 30), (float)(GUIManager.logo.height - 10)), GUIManager.logo);
			int num;
			if (this.button_screen_hover)
			{
				num = 2;
			}
			else
			{
				num = 0;
			}
			GUI.DrawTexture(new Rect(GUIManager.XRES(1024f) - 160f, (float)(4 - num), 32f, 32f), GUIManager.tex_fullscreen);
			if (GUI.Button(new Rect(GUIManager.XRES(1024f) - 160f, (float)(4 - num), 32f, 32f), "", GUIManager.gs_style3))
			{
				Config.ChangeMode();
			}
			if (this.button_exit_hover)
			{
				num = 2;
			}
			else
			{
				num = 0;
			}
			GUI.DrawTexture(new Rect(GUIManager.XRES(1024f) - 40f, (float)(4 - num), 32f, 32f), GUIManager.tex_exit);
			if (GUI.Button(new Rect(GUIManager.XRES(1024f) - 40f, (float)(4 - num), 32f, 32f), "", GUIManager.gs_style3))
			{
				Application.Quit();
			}
			if (this.button_help_hover)
			{
				num = 2;
			}
			else
			{
				num = 0;
			}
			GUI.DrawTexture(new Rect(GUIManager.XRES(1024f) - 120f, (float)(4 - num), 32f, 32f), GUIManager.tex_help);
			if (GUI.Button(new Rect(GUIManager.XRES(1024f) - 120f, (float)(4 - num), 32f, 32f), "", GUIManager.gs_style3))
			{
				this.last_state = GM.currGUIState;
				GM.currGUIState = GUIGS.RULES;
			}
			if (this.button_sound_hover)
			{
				num = 2;
			}
			else
			{
				num = 0;
			}
			if (this.sound)
			{
				GUI.DrawTexture(new Rect(GUIManager.XRES(1024f) - 80f, (float)(4 - num), 32f, 32f), GUIManager.tex_sound_on);
			}
			else
			{
				GUI.DrawTexture(new Rect(GUIManager.XRES(1024f) - 80f, (float)(4 - num), 32f, 32f), GUIManager.tex_sound_off);
			}
			if (GUI.Button(new Rect(GUIManager.XRES(1024f) - 80f, (float)(4 - num), 32f, 32f), "", GUIManager.gs_style3))
			{
				this.sound = !this.sound;
				this.SetSound(this.sound);
			}
			if (GameController.STATE != GAME_STATES.MAINMENU)
			{
				return;
			}
			if (PlayerProfile.get_player_stats)
			{
				GUI.DrawTexture(new Rect(4f, 4f, 32f, 32f), GUIManager.tex_face_icon);
				TextAnchor alignment;
				if (!this.editName)
				{
					alignment = GUIManager.gs_style3.alignment;
					GUIManager.gs_style3.alignment = 3;
					GUI.Label(new Rect(42f, 4f, 200f, 32f), PlayerProfile.PlayerName, GUIManager.gs_style3);
					Vector2 vector = GUIManager.gs_style3.CalcSize(new GUIContent(PlayerProfile.PlayerName));
					if (this.button_name_hover)
					{
						num = 2;
					}
					else
					{
						num = 0;
					}
					GUI.DrawTexture(new Rect(4f + vector.x + 40f, (float)(10 - num), 20f, 20f), GUIManager.tex_edit_icon);
					if (GUI.Button(new Rect(4f + vector.x + 40f, (float)(10 - num), 20f, 20f), "", GUIManager.gs_style3))
					{
						this.editName = true;
						MainMenu.MainAS.PlayOneShot(SoundManager.SoundClick, AudioListener.volume);
					}
					GUIManager.gs_style3.alignment = alignment;
				}
				else
				{
					PlayerProfile.PlayerName = GUI.TextField(new Rect(42f, 10f, 84f, 20f), PlayerProfile.PlayerName, 16);
					if (this.button_save_hover)
					{
						num = 2;
					}
					else
					{
						num = 0;
					}
					GUI.DrawTexture(new Rect(130f, (float)(10 - num), 32f, 32f), GUIManager.tex_save_icon);
					if (GUI.Button(new Rect(130f, (float)(10 - num), 20f, 20f), "", GUIManager.gs_style3))
					{
						this.editName = false;
						base.StartCoroutine(Handler.set_name());
						MainMenu.MainAS.PlayOneShot(SoundManager.SoundClick, AudioListener.volume);
					}
				}
				if (this.button_add_hover)
				{
					GUI.DrawTexture(new Rect(GUIManager.XRES(512f) - 256f, 0f, 512f, 64f), GUIManager.tex_top_plus2);
				}
				else
				{
					GUI.DrawTexture(new Rect(GUIManager.XRES(512f) - 256f, 0f, 512f, 64f), GUIManager.tex_top_plus);
				}
				if (GUI.Button(new Rect(GUIManager.XRES(512f) - 68f, 0f, 235f, 33f), "", GUIManager.gs_style3))
				{
					this.HideAll();
					base.gameObject.GetComponent<Gold>().Active = true;
					GM.currGUIState = GUIGS.GOLD;
				}
				GUIManager.gs_style3.fontSize = 20;
				alignment = GUIManager.gs_style3.alignment;
				GUIManager.gs_style3.alignment = 3;
				GUI.color = new Color(0f, 0f, 0f, 1f);
				GUI.Label(new Rect(GUIManager.XRES(512f) - 125f + 1f, 2f, 200f, 32f), PlayerProfile.money.ToString(), GUIManager.gs_style3);
				GUI.color = new Color(1f, 1f, 1f, 1f);
				GUI.Label(new Rect(GUIManager.XRES(512f) - 125f, 1f, 200f, 32f), PlayerProfile.money.ToString(), GUIManager.gs_style3);
				GUIManager.gs_style3.fontSize = 16;
				GUIManager.gs_style3.alignment = alignment;
			}
			else
			{
				GUI.DrawTexture(new Rect(4f, 4f, 32f, 32f), GUIManager.tex_icon_load);
				GUI.color = new Color(0f, 0f, 0f, 1f);
				GUI.Label(new Rect(43f, 5f, 200f, 32f), Lang.GetLabel(25), GUIManager.gs_style3);
				GUI.color = new Color(1f, 1f, 1f, 1f);
				GUI.Label(new Rect(42f, 4f, 200f, 32f), Lang.GetLabel(25), GUIManager.gs_style3);
			}
			GUI.depth = -1;
			this.DrawButtonBar();
			return;
		}
	}

	// Token: 0x0600008E RID: 142 RVA: 0x000083D0 File Offset: 0x000065D0
	private void DrawRules()
	{
		float x = Input.mousePosition.x;
		float num = (float)Screen.height - Input.mousePosition.y;
		GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), GUIManager.tex_black);
		TextAnchor alignment = GUIManager.gs_style3.alignment;
		int fontSize = GUIManager.gs_style3.fontSize;
		GUIManager.gs_style3.alignment = 4;
		GUIManager.gs_style3.fontSize = 30;
		GUI.Label(new Rect(0f, 0f, (float)Screen.width, 50f), Lang.GetLabel(531), GUIManager.gs_style3);
		GUIManager.gs_style3.wordWrap = false;
		GUIManager.gs_style3.alignment = alignment;
		GUIManager.gs_style3.fontSize = fontSize;
		this.scrollViewVector = GUIManager.BeginScrollView(new Rect(20f, 50f, (float)(Screen.width - 40), (float)(Screen.height - 100)), this.scrollViewVector, new Rect(0f, 0f, 0f, 1024f));
		alignment = GUIManager.gs_style3.alignment;
		fontSize = GUIManager.gs_style3.fontSize;
		GUIManager.gs_style3.alignment = 0;
		GUIManager.gs_style3.fontSize = 18;
		GUIManager.gs_style3.wordWrap = true;
		GUI.TextArea(new Rect(0f, 0f, (float)(Screen.width - 50), 1024f), Lang.GetLabel(530), GUIManager.gs_style3);
		GUIManager.gs_style3.wordWrap = false;
		GUIManager.gs_style3.alignment = alignment;
		GUIManager.gs_style3.fontSize = fontSize;
		GUIManager.EndScrollView();
		bool flag = false;
		Rect rect;
		rect..ctor((float)(Screen.width / 2 - 96), (float)(Screen.height - 40), 192f, 32f);
		if (rect.Contains(new Vector2(x, num)))
		{
			if (!flag)
			{
				flag = true;
			}
		}
		else if (flag)
		{
			flag = false;
		}
		if (flag)
		{
			GUI.DrawTexture(rect, GUIManager.tex_button_hover);
		}
		else
		{
			GUI.DrawTexture(rect, GUIManager.tex_button);
		}
		alignment = GUIManager.gs_style3.alignment;
		fontSize = GUIManager.gs_style3.fontSize;
		GUIManager.gs_style3.alignment = 4;
		GUIManager.gs_style3.fontSize = 22;
		if (GUI.Button(rect, Lang.GetLabel(532), GUIManager.gs_style3))
		{
			GM.currGUIState = this.last_state;
		}
		GUIManager.gs_style3.wordWrap = false;
		GUIManager.gs_style3.alignment = alignment;
		GUIManager.gs_style3.fontSize = fontSize;
	}

	// Token: 0x0600008F RID: 143 RVA: 0x0000864D File Offset: 0x0000684D
	public void OpenServerList()
	{
		this.HideAll();
		base.gameObject.GetComponent<ServerList>().Active = true;
		base.gameObject.GetComponent<ServerList>().refresh_servers();
		this.menu[0] = true;
		GM.currGUIState = GUIGS.SERVERLIST;
	}

	// Token: 0x06000090 RID: 144 RVA: 0x00008688 File Offset: 0x00006888
	private void DrawButtonBar()
	{
		Vector3 mousePosition = Input.mousePosition;
		int height = Screen.height;
		Vector3 mousePosition2 = Input.mousePosition;
		GUI.DrawTexture(new Rect(0f, 135f, GUIManager.XRES(1024f), 64f), GUIManager.tex_half_black);
		if (this.DrawButton(new Rect((float)Screen.width / 2f - 300f + 0f, 135f, 64f, 64f), GUIManager.tex_menu_start, this.menu[0], 0))
		{
			this.HideAll();
			base.gameObject.GetComponent<ServerList>().Active = true;
			base.gameObject.GetComponent<ServerList>().refresh_servers();
			this.menu[0] = true;
			GM.currGUIState = GUIGS.SERVERLIST;
		}
		if (this.DrawButton(new Rect((float)Screen.width / 2f - 300f + 70f, 135f, 64f, 64f), GUIManager.tex_menu_option, this.menu[1], 1))
		{
			this.HideAll();
			base.gameObject.GetComponent<Options>().onActive();
			base.gameObject.GetComponent<Options>().Active = true;
			this.menu[1] = true;
			GM.currGUIState = GUIGS.OPTIONS;
		}
		if (this.DrawButton(new Rect((float)Screen.width / 2f - 300f + 210f, 135f, 64f, 64f), GUIManager.tex_menu_shop, this.menu[3], 3))
		{
			this.HideAll();
			base.gameObject.GetComponent<Shop>().onActive();
			base.gameObject.GetComponent<Shop>().Active = true;
			this.menu[3] = true;
			GM.currGUIState = GUIGS.SHOP;
		}
		if (this.DrawButton(new Rect((float)Screen.width / 2f - 300f + 140f, 135f, 64f, 64f), GUIManager.tex_menu_profile, this.menu[2], 2))
		{
			this.HideAll();
			base.gameObject.GetComponent<Profile>().onActive();
			base.gameObject.GetComponent<Profile>().Active = true;
			this.menu[2] = true;
			GM.currGUIState = GUIGS.RANG;
		}
		if (this.DrawButton(new Rect((float)Screen.width / 2f - 300f + 280f, 135f, 64f, 64f), GUIManager.tex_menu_inv, this.menu[4], 4))
		{
			this.HideAll();
			base.gameObject.GetComponent<Inv>().onActive();
			base.gameObject.GetComponent<Inv>().Active = true;
			this.menu[4] = true;
			GM.currGUIState = GUIGS.INVENTORY;
		}
		if (this.DrawButton(new Rect((float)Screen.width / 2f - 300f + 350f, 135f, 64f, 64f), GUIManager.tex_menu_playerstats, this.menu[5], 5))
		{
			this.HideAll();
			base.gameObject.GetComponent<PlayerStats>().Active = true;
			base.gameObject.GetComponent<PlayerStats>().onActive();
			this.menu[5] = true;
			GM.currGUIState = GUIGS.RAITING;
		}
		if (this.DrawButton(new Rect((float)Screen.width / 2f - 300f + 420f, 135f, 64f, 64f), GUIManager.tex_menu_clan, this.menu[6], 6))
		{
			this.HideAll();
			base.gameObject.GetComponent<Clan>().onActive();
			base.gameObject.GetComponent<Clan>().Active = true;
			this.menu[6] = true;
			GM.currGUIState = GUIGS.CLAN;
		}
		if (this.DrawButton(new Rect((float)Screen.width / 2f - 300f + 490f, 135f, 64f, 64f), GUIManager.tex_menu_master, this.menu[7], 7))
		{
			this.HideAll();
			base.gameObject.GetComponent<Master>().OnActive();
			base.gameObject.GetComponent<Master>().Active = true;
			this.menu[7] = true;
			GM.currGUIState = GUIGS.MASTER;
		}
		if (this.quest)
		{
			if (GUI.Button(new Rect((float)Screen.width / 2f + 300f, 135f, 64f, 64f), GUIManager.tex_menu_social, GUIManager.gs_style3))
			{
				this.HideAll();
				base.gameObject.GetComponent<Social>().Active = true;
				GM.currGUIState = GUIGS.SOCIAL;
			}
			GUI.DrawTexture(new Rect((float)Screen.width / 2f + 300f, 135f, 64f, 64f), GUIManager.tex_menu_zadanie);
		}
	}

	// Token: 0x06000091 RID: 145 RVA: 0x00008B20 File Offset: 0x00006D20
	private bool DrawButton(Rect r, Texture2D tex, bool a, int id)
	{
		if (!a)
		{
			float x = Input.mousePosition.x;
			float num = (float)Screen.height - Input.mousePosition.y;
			if (r.Contains(new Vector2(x, num)))
			{
				if (!this.menu_hover[id])
				{
					this.menu_hover[id] = true;
					MainMenu.MainAS.PlayOneShot(SoundManager.SoundHover, AudioListener.volume);
				}
			}
			else if (this.menu_hover[id])
			{
				this.menu_hover[id] = false;
			}
			if (this.menu_hover[id])
			{
				r.y -= 2f;
			}
		}
		bool result = false;
		if (a)
		{
			GUI.DrawTexture(new Rect(r.x, r.y, 128f, 128f), GUIManager.tex_menu_back);
		}
		GUI.DrawTexture(r, tex);
		if (GUI.Button(r, "", GUIManager.gs_style3))
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06000092 RID: 146 RVA: 0x00008C08 File Offset: 0x00006E08
	private void FixedUpdate()
	{
		if (SoundManager.Soundtrack == null)
		{
			SoundManager.Soundtrack = ContentLoader.LoadSound("soundtrack");
			return;
		}
		if (MainMenu.MainAS.clip == null)
		{
			MainMenu.MainAS.clip = SoundManager.Soundtrack;
			return;
		}
		if (!MainMenu.MainAS.isPlaying)
		{
			MainMenu.MainAS.Play();
		}
	}

	// Token: 0x06000093 RID: 147 RVA: 0x00008C6C File Offset: 0x00006E6C
	private void Update()
	{
		if (GameController.STATE != GAME_STATES.MAINMENU)
		{
			return;
		}
		float x = Input.mousePosition.x;
		float num = (float)Screen.height - Input.mousePosition.y;
		if (!this.editName)
		{
			Vector2 vector = GUIManager.gs_style3.CalcSize(new GUIContent(PlayerProfile.PlayerName));
			Rect rect;
			rect..ctor(4f + vector.x + 40f, 10f, 20f, 20f);
			if (rect.Contains(new Vector2(x, num)))
			{
				if (!this.button_name_hover)
				{
					this.button_name_hover = true;
					MainMenu.MainAS.PlayOneShot(SoundManager.SoundHover, AudioListener.volume);
				}
			}
			else if (this.button_name_hover)
			{
				this.button_name_hover = false;
			}
		}
		else
		{
			Rect rect2;
			rect2..ctor(130f, 10f, 20f, 20f);
			if (rect2.Contains(new Vector2(x, num)))
			{
				if (!this.button_save_hover)
				{
					this.button_save_hover = true;
					MainMenu.MainAS.PlayOneShot(SoundManager.SoundHover, AudioListener.volume);
				}
			}
			else if (this.button_save_hover)
			{
				this.button_name_hover = false;
			}
		}
		Rect rect3;
		rect3..ctor(GUIManager.XRES(512f) - 68f, 0f, 235f, 33f);
		if (rect3.Contains(new Vector2(x, num)))
		{
			if (!this.button_add_hover)
			{
				this.button_add_hover = true;
				MainMenu.MainAS.PlayOneShot(SoundManager.SoundHover, AudioListener.volume);
			}
		}
		else if (this.button_add_hover)
		{
			this.button_add_hover = false;
		}
		Rect rect4;
		rect4..ctor(GUIManager.XRES(1024f) - 120f, 4f, 32f, 32f);
		if (rect4.Contains(new Vector2(x, num)))
		{
			if (!this.button_help_hover)
			{
				this.button_help_hover = true;
				MainMenu.MainAS.PlayOneShot(SoundManager.SoundHover, AudioListener.volume);
			}
		}
		else if (this.button_help_hover)
		{
			this.button_help_hover = false;
		}
		Rect rect5;
		rect5..ctor(GUIManager.XRES(1024f) - 80f, 4f, 32f, 32f);
		if (rect5.Contains(new Vector2(x, num)))
		{
			if (!this.button_sound_hover)
			{
				this.button_sound_hover = true;
				MainMenu.MainAS.PlayOneShot(SoundManager.SoundHover, AudioListener.volume);
			}
		}
		else if (this.button_sound_hover)
		{
			this.button_sound_hover = false;
		}
		Rect rect6;
		rect6..ctor(GUIManager.XRES(1024f) - 160f, 4f, 32f, 32f);
		if (rect6.Contains(new Vector2(x, num)))
		{
			if (!this.button_screen_hover)
			{
				this.button_screen_hover = true;
				MainMenu.MainAS.PlayOneShot(SoundManager.SoundHover, AudioListener.volume);
			}
		}
		else if (this.button_screen_hover)
		{
			this.button_screen_hover = false;
		}
		Rect rect7;
		rect7..ctor(GUIManager.XRES(1024f) - 40f, 4f, 32f, 32f);
		if (rect7.Contains(new Vector2(x, num)))
		{
			if (!this.button_exit_hover)
			{
				this.button_exit_hover = true;
				MainMenu.MainAS.PlayOneShot(SoundManager.SoundHover, AudioListener.volume);
				return;
			}
		}
		else if (this.button_exit_hover)
		{
			this.button_exit_hover = false;
		}
	}

	// Token: 0x06000094 RID: 148 RVA: 0x00008FA4 File Offset: 0x000071A4
	public void HideAll()
	{
		for (int i = 0; i < 10; i++)
		{
			this.menu[i] = false;
		}
		MainMenu.MainAS.PlayOneShot(SoundManager.SoundClick, AudioListener.volume);
		base.gameObject.GetComponent<ServerList>().Active = false;
		base.gameObject.GetComponent<Options>().Active = false;
		base.gameObject.GetComponent<Profile>().Active = false;
		base.gameObject.GetComponent<Shop>().Active = false;
		base.gameObject.GetComponent<Inv>().Active = false;
		base.gameObject.GetComponent<PlayerStats>().Active = false;
		base.gameObject.GetComponent<Clan>().Active = false;
		base.gameObject.GetComponent<Master>().Active = false;
		base.gameObject.GetComponent<Gold>().Active = false;
		base.gameObject.GetComponent<Social>().Active = false;
		foreach (ItemData itemData in ItemsDB.Items)
		{
			if (itemData != null && !itemData.preview)
			{
				ItemData itemData2 = itemData;
				string refgo = itemData.refgo;
				ITEM itemID = (ITEM)itemData.ItemID;
				itemData2.preview = GameObject.Find(refgo + itemID.ToString());
				if (itemData.preview)
				{
					itemData.preview.SetActive(false);
				}
				else
				{
					ItemData itemData3 = itemData;
					string refgoRoot = itemData.refgoRoot;
					itemID = (ITEM)itemData.ItemID;
					itemData3.preview = GameObject.Find(refgoRoot + itemID.ToString());
					if (itemData.preview)
					{
						itemData.preview.SetActive(false);
					}
				}
			}
		}
	}

	// Token: 0x06000095 RID: 149 RVA: 0x00009144 File Offset: 0x00007344
	public void ShowAll()
	{
		for (int i = 0; i < 10; i++)
		{
			this.menu[i] = true;
		}
		base.gameObject.GetComponent<ServerList>().Active = true;
		base.gameObject.GetComponent<Options>().Active = true;
		base.gameObject.GetComponent<Profile>().Active = true;
		base.gameObject.GetComponent<Shop>().Active = true;
		base.gameObject.GetComponent<Inv>().Active = true;
		base.gameObject.GetComponent<PlayerStats>().Active = true;
		base.gameObject.GetComponent<Clan>().Active = true;
		base.gameObject.GetComponent<Master>().Active = true;
		base.gameObject.GetComponent<Gold>().Active = true;
		base.gameObject.GetComponent<Social>().Active = true;
	}

	// Token: 0x06000096 RID: 150 RVA: 0x00009214 File Offset: 0x00007414
	public void SetSound(bool val)
	{
		if (val)
		{
			MainMenu.MainAS.volume = 0.6f;
		}
		else
		{
			MainMenu.MainAS.volume = 0f;
		}
		this.sound = val;
		if (val)
		{
			Config.menuvolume = MainMenu.MainAS.volume;
		}
		else
		{
			Config.menuvolume = MainMenu.MainAS.volume;
		}
		PlayerPrefs.SetFloat(CONST.MD5("MenuVolume"), MainMenu.MainAS.volume);
		PlayerPrefs.Save();
	}

	// Token: 0x0400007C RID: 124
	private TweenButton questButton;

	// Token: 0x0400007D RID: 125
	public static AudioSource MainAS;

	// Token: 0x0400007E RID: 126
	public Texture2D preLoaderRu;

	// Token: 0x0400007F RID: 127
	public Texture2D preLoaderEn;

	// Token: 0x04000080 RID: 128
	public Texture2D SteamError;

	// Token: 0x04000081 RID: 129
	public Texture2D NovalinkError;

	// Token: 0x04000082 RID: 130
	public Texture2D KartrigeError;

	// Token: 0x04000083 RID: 131
	public Texture2D AuthError;

	// Token: 0x04000084 RID: 132
	private GUIGS last_state;

	// Token: 0x04000085 RID: 133
	private Rect r;

	// Token: 0x04000086 RID: 134
	private bool button_start_hover;

	// Token: 0x04000087 RID: 135
	private bool button_settings_hover;

	// Token: 0x04000088 RID: 136
	private bool button_name_hover;

	// Token: 0x04000089 RID: 137
	private bool button_save_hover;

	// Token: 0x0400008A RID: 138
	private bool button_sound_hover;

	// Token: 0x0400008B RID: 139
	private bool button_help_hover;

	// Token: 0x0400008C RID: 140
	private bool button_screen_hover;

	// Token: 0x0400008D RID: 141
	private bool button_add_hover;

	// Token: 0x0400008E RID: 142
	private bool button_exit_hover;

	// Token: 0x0400008F RID: 143
	private bool editName;

	// Token: 0x04000090 RID: 144
	private bool sound = true;

	// Token: 0x04000091 RID: 145
	private bool[] menu = new bool[10];

	// Token: 0x04000092 RID: 146
	private bool[] menu_hover = new bool[10];

	// Token: 0x04000093 RID: 147
	public bool quest;

	// Token: 0x04000094 RID: 148
	private Vector2 scrollViewVector = Vector2.zero;
}
