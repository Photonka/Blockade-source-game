using System;
using UnityEngine;

// Token: 0x0200008D RID: 141
public class MainGUI : MonoBehaviour
{
	// Token: 0x06000417 RID: 1047 RVA: 0x00051EF8 File Offset: 0x000500F8
	private void Start()
	{
		this.EM = base.GetComponent<E_Menu>();
		this.NS = base.GetComponent<New_Slots>();
		this.NST = base.GetComponent<New_Select_Team>();
		MainGUI.sel_team = false;
		MainGUI.e_menu = false;
		Map map = (Map)Object.FindObjectOfType(typeof(Map));
		this.csr = (Radar)Object.FindObjectOfType(typeof(Radar));
		this.cszl = (ZipLoader)Object.FindObjectOfType(typeof(ZipLoader));
		this.blockSet = map.GetBlockSet();
		this.blocksel = null;
		this.SetTeamBlocks();
		this.tex_inv = (Resources.Load("GUI/inv") as Texture2D);
		this.gui_style = new GUIStyle();
		this.gui_style.font = FontManager.font[2];
		this.gui_style.fontSize = 14;
		this.gui_style.normal.textColor = new Color(1f, 1f, 1f, 1f);
		this.gui_style.alignment = 7;
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x0005200C File Offset: 0x0005020C
	public void SetTeamBlocks()
	{
		this.teamblock[0] = this.blockSet.GetBlock("Brick_blue");
		this.teamblock[1] = this.blockSet.GetBlock("Brick_red");
		this.teamblock[2] = this.blockSet.GetBlock("Brick_green");
		this.teamblock[3] = this.blockSet.GetBlock("Brick_yellow");
		this.teamblock[4] = this.blockSet.GetBlock("ArmoredBrickBlue");
		this.teamblock[5] = this.blockSet.GetBlock("ArmoredBrickRed");
		this.teamblock[6] = this.blockSet.GetBlock("ArmoredBrickGreen");
		this.teamblock[7] = this.blockSet.GetBlock("ArmoredBrickYellow");
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x000520DC File Offset: 0x000502DC
	private void Update()
	{
		MainGUI.UpdateCursorLock();
		if (ConnectionInfo.mode == CONST.CFG.BUILD_MODE)
		{
			if (this.blocksel == null)
			{
				if (this.blockSet != null)
				{
					this.blocksel = this.blockSet.GetBlock(1);
					this.selectedBlock = this.blockSet.GetBlock(1);
					this.SetBlockTexture(this.blocksel);
					if (this.cscl == null)
					{
						this.cscl = Object.FindObjectOfType<Client>();
					}
					int block = ZipLoader.GetBlock(this.blocksel.GetName());
					this.cscl.send_selectblock((byte)block);
					BotsController.Instance.Bots[PlayerProfile.myindex].blockFlag = block;
				}
			}
			else if ((this.blocksel.GetName() == "Brick_blue" || this.blocksel.GetName() == "Brick_red" || this.blocksel.GetName() == "Brick_green" || this.blocksel.GetName() == "Brick_yellow" || this.blocksel.GetName() == "!Water" || this.blocksel.GetName() == "TNT" || this.blocksel.GetName() == "ArmoredBrickBlue" || this.blocksel.GetName() == "ArmoredBrickRed" || this.blocksel.GetName() == "ArmoredBrickGreen" || this.blocksel.GetName() == "ArmoredBrickYellow") && this.blockSet != null)
			{
				this.blocksel = this.blockSet.GetBlock(1);
				this.selectedBlock = this.blockSet.GetBlock(1);
				this.SetBlockTexture(this.blocksel);
				if (this.cscl == null)
				{
					this.cscl = Object.FindObjectOfType<Client>();
				}
				int block2 = ZipLoader.GetBlock(this.blocksel.GetName());
				this.cscl.send_selectblock((byte)block2);
				BotsController.Instance.Bots[PlayerProfile.myindex].blockFlag = block2;
			}
		}
		if (PlayerControl.GetGameMode() == CONST.CFG.ZOMBIE_MODE && BotsController.Instance.Bots[PlayerProfile.myindex].zombie)
		{
			this.g_canbuy = false;
			if (MainGUI.e_menu)
			{
				this.CloseEMenu(false);
			}
			if (MainGUI.sel_team)
			{
				this.CloseSelectTeam();
			}
			return;
		}
		if (BotsController.Instance.Bots[PlayerProfile.myindex].Active && BotsController.Instance.Bots[PlayerProfile.myindex].Dead == 1 && MainGUI.sel_team)
		{
			this.CloseSelectTeam();
			return;
		}
		if ((PlayerControl.GetGameMode() == CONST.CFG.SURVIVAL_MODE || PlayerControl.GetGameMode() == CONST.CFG.CLEAR_MODE) && MainGUI.sel_team)
		{
			this.CloseSelectTeam();
			return;
		}
		if (this.tc == null)
		{
			this.tc = (TankController)Object.FindObjectOfType(typeof(TankController));
		}
		if (this.cc == null)
		{
			this.cc = (CarController)Object.FindObjectOfType(typeof(CarController));
		}
		if (this.tc != null && this.tc.enabled)
		{
			this.CloseSelectTeam();
			this.CloseEMenu(true);
			this.g_canbuy = false;
			return;
		}
		if (this.cc != null && this.cc.enabled)
		{
			this.CloseSelectTeam();
			this.CloseEMenu(true);
			this.g_canbuy = false;
			return;
		}
		if (ConnectionInfo.mode != CONST.CFG.SNOWBALLS_MODE && Input.GetKeyDown(101) && this.g_canbuy)
		{
			if (!BotsController.Instance.Bots[PlayerProfile.myindex].Active || BotsController.Instance.Bots[PlayerProfile.myindex].Dead == 1)
			{
				this.g_canbuy = false;
				return;
			}
			if (MainGUI.sel_team)
			{
				return;
			}
			MainGUI.e_menu = !MainGUI.e_menu;
			if (MainGUI.e_menu)
			{
				this.OpenEMenu();
			}
			else
			{
				this.CloseEMenu(false);
			}
		}
		if (Input.GetKeyDown(109))
		{
			if (PlayerControl.GetGameMode() == CONST.CFG.BUILD_MODE || PlayerControl.GetGameMode() == CONST.CFG.ZOMBIE_MODE || PlayerControl.GetGameMode() == CONST.CFG.SURVIVAL_MODE || PlayerControl.GetGameMode() == CONST.CFG.CLEAR_MODE || !BotsController.Instance.Bots[PlayerProfile.myindex].Active || BotsController.Instance.Bots[PlayerProfile.myindex].Dead == 1)
			{
				return;
			}
			if (MainGUI.e_menu)
			{
				return;
			}
			MainGUI.sel_team = !MainGUI.sel_team;
			if (MainGUI.sel_team)
			{
				this.OpenSelectTeam();
			}
			else
			{
				this.CloseSelectTeam();
			}
		}
		if (Time.time > this.g_buycheck)
		{
			if (!BotsController.Instance.Bots[PlayerProfile.myindex].Active || BotsController.Instance.Bots[PlayerProfile.myindex].Dead == 1)
			{
				this.g_canbuy = false;
			}
			if (this.cspc == null)
			{
				this.cspc = (PlayerControl)Object.FindObjectOfType(typeof(PlayerControl));
			}
			this.g_canbuy = false;
			this.g_buycheck += 0.5f;
			if (PlayerControl.GetGameMode() == CONST.CFG.BUILD_MODE || PlayerControl.GetGameMode() == CONST.CFG.ZOMBIE_MODE || PlayerControl.GetGameMode() == CONST.CFG.SURVIVAL_MODE || PlayerControl.GetGameMode() == CONST.CFG.CLEAR_MODE)
			{
				this.g_canbuy = true;
				return;
			}
			if (this.goPlayer == null)
			{
				this.goPlayer = GameObject.Find("Player");
			}
			if (this.cspc && this.goPlayer && this.cspc.GetTeam() <= 3)
			{
				if ((PlayerControl.GetGameMode() == CONST.CFG.BATTLE_MODE && this.cszl.rblock.Count == 32) || (PlayerControl.GetGameMode() == CONST.CFG.CLASSIC_MODE || (this.cszl.mapversion > 0 && PlayerControl.GetGameMode() == CONST.CFG.CONTRA_MODE)) || PlayerControl.GetGameMode() == CONST.CFG.TANK_MODE || PlayerControl.GetGameMode() == CONST.CFG.PRORIV_MODE)
				{
					for (int i = 0; i < this.cszl.rblock.Count; i++)
					{
						if ((this.cszl.rblock[i].mode == PlayerControl.GetGameMode() || PlayerControl.GetGameMode() == CONST.CFG.PRORIV_MODE) && (this.cszl.rblock[i].team == this.cspc.GetTeam() || PlayerControl.GetGameMode() == CONST.CFG.PRORIV_MODE) && Mathf.Abs(this.cszl.rblock[i].pos.x - this.goPlayer.transform.position.x) < 4f && Mathf.Abs(this.cszl.rblock[i].pos.z - this.goPlayer.transform.position.z) < 4f)
						{
							this.g_canbuy = true;
						}
					}
					return;
				}
				int num = 8;
				if (PlayerControl.GetGameMode() == CONST.CFG.CONTRA_MODE)
				{
					num = 24;
				}
				if (Mathf.Abs(this.csr.team_pos[this.cspc.GetTeam()].x - this.goPlayer.transform.position.x) < (float)num && Mathf.Abs(this.csr.team_pos[this.cspc.GetTeam()].z - this.goPlayer.transform.position.z) < (float)num)
				{
					this.g_canbuy = true;
					return;
				}
				this.g_canbuy = false;
			}
		}
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x00052898 File Offset: 0x00050A98
	public void CloseAll()
	{
		GM.currGUIState = GUIGS.NULL;
		MainGUI.e_menu = false;
		MainGUI.sel_team = false;
		if (this.csws == null)
		{
			this.csws = Object.FindObjectOfType<WeaponSystem>();
		}
		if (!BotsController.Instance.Bots[PlayerProfile.myindex].zombie && BotsController.Instance.Bots[PlayerProfile.myindex].Active && BotsController.Instance.Bots[PlayerProfile.myindex].Dead == 0 && PlayerControl.GetGameMode() == CONST.CFG.ZOMBIE_MODE)
		{
			this.csws.SetPrimaryWeapon(BotsController.Instance.Bots[PlayerProfile.myindex].WeaponID == 0 && ConnectionInfo.mode == CONST.CFG.BUILD_MODE);
			this.NS.Active[4] = true;
		}
		if (PlayerProfile.myteam > -1)
		{
			MainGUI.ForceCursor = false;
			if (!BotsController.Instance.Bots[PlayerProfile.myindex].zombie && BotsController.Instance.Bots[PlayerProfile.myindex].Active && BotsController.Instance.Bots[PlayerProfile.myindex].Dead == 0)
			{
				this.csws.SetPrimaryWeapon(BotsController.Instance.Bots[PlayerProfile.myindex].WeaponID == 0 && ConnectionInfo.mode == CONST.CFG.BUILD_MODE);
			}
			this.NS.Active[4] = true;
		}
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x000529F8 File Offset: 0x00050BF8
	public void CloseSelectTeam()
	{
		GM.currGUIState = GUIGS.NULL;
		MainGUI.sel_team = false;
		MainGUI.ForceCursor = false;
		if (!MainGUI.e_menu)
		{
			if (this.csws == null)
			{
				this.csws = (WeaponSystem)Object.FindObjectOfType(typeof(WeaponSystem));
			}
			if (PlayerProfile.myteam > -1)
			{
				if (!BotsController.Instance.Bots[PlayerProfile.myindex].zombie && BotsController.Instance.Bots[PlayerProfile.myindex].Active && BotsController.Instance.Bots[PlayerProfile.myindex].Dead == 0)
				{
					this.csws.SetPrimaryWeapon(BotsController.Instance.Bots[PlayerProfile.myindex].WeaponID == 0 && ConnectionInfo.mode == CONST.CFG.BUILD_MODE);
				}
				this.NS.Active[4] = true;
			}
		}
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x00052ADC File Offset: 0x00050CDC
	public void CloseEMenu(bool skipSetWeapon = false)
	{
		GM.currGUIState = GUIGS.NULL;
		MainGUI.e_menu = false;
		MainGUI.ForceCursor = false;
		if (this.csws == null)
		{
			this.csws = (WeaponSystem)Object.FindObjectOfType(typeof(WeaponSystem));
		}
		if (!BotsController.Instance.Bots[PlayerProfile.myindex].zombie && BotsController.Instance.Bots[PlayerProfile.myindex].Active && BotsController.Instance.Bots[PlayerProfile.myindex].Dead == 0 && PlayerControl.GetGameMode() == CONST.CFG.ZOMBIE_MODE)
		{
			this.csws.SetPrimaryWeapon(false);
			this.NS.Active[4] = true;
		}
		if (PlayerProfile.myteam > -1 && !skipSetWeapon)
		{
			if (!BotsController.Instance.Bots[PlayerProfile.myindex].zombie && BotsController.Instance.Bots[PlayerProfile.myindex].Active && BotsController.Instance.Bots[PlayerProfile.myindex].Dead == 0)
			{
				this.csws.SetPrimaryWeapon(BotsController.Instance.Bots[PlayerProfile.myindex].WeaponID == 0 && ConnectionInfo.mode == CONST.CFG.BUILD_MODE);
			}
			this.NS.Active[4] = true;
		}
	}

	// Token: 0x0600041D RID: 1053 RVA: 0x00052C24 File Offset: 0x00050E24
	public void OpenSelectTeam()
	{
		this.NS.Active[4] = false;
		MainGUI.e_menu = false;
		MainGUI.sel_team = true;
		if (this.csws == null)
		{
			this.csws = (WeaponSystem)Object.FindObjectOfType(typeof(WeaponSystem));
		}
		MainGUI.ForceCursor = true;
		this.csws.HideWeapons(true);
		GM.currGUIState = GUIGS.TEAMSELECT;
	}

	// Token: 0x0600041E RID: 1054 RVA: 0x00052C8C File Offset: 0x00050E8C
	public void OpenEMenu()
	{
		this.NS.Active[4] = false;
		this.EM.Init();
		MainGUI.e_menu = true;
		MainGUI.sel_team = false;
		if (this.csws == null)
		{
			this.csws = (WeaponSystem)Object.FindObjectOfType(typeof(WeaponSystem));
		}
		MainGUI.ForceCursor = true;
		this.csws.HideWeapons(true);
		GM.currGUIState = GUIGS.WEAPONSELECT;
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x00052D00 File Offset: 0x00050F00
	private void OnGUI()
	{
		if (MainGUI.e_menu)
		{
			if (PlayerControl.GetGameMode() == 2)
			{
				Rect rect;
				rect..ctor(0f, 0f, (float)Screen.width * 0.5f, (float)Screen.height * 0.6f);
				rect.center = new Vector2((float)Screen.width, (float)Screen.height) / 2f;
				GUILayout.Window(1, rect, new GUI.WindowFunction(this.DoInventoryWindow), "", this.gui_style, Array.Empty<GUILayoutOption>());
			}
			else
			{
				if (PlayerControl.GetGameMode() == 1)
				{
					return;
				}
				this.EM.Draw_E_Menu();
				if (this.EM.Active_Tab_Index < 4)
				{
					this.NS.Draw_New_Slots(this.EM.Active_Tab_Index + this.EM.Active_Item_PodIndex + 1);
				}
				else
				{
					this.NS.Draw_New_Slots(-1);
				}
			}
		}
		if (MainGUI.sel_team)
		{
			this.NST.Draw_New_Select_Team();
		}
		if (this.g_canbuy)
		{
			GUI.DrawTexture(new Rect((float)(Screen.width - 40), (float)(Screen.height - 220), 32f, 32f), this.tex_inv);
		}
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x00052E30 File Offset: 0x00051030
	public void SetBlockTextureTeam(GameObject face, GameObject top, int team, bool self = false)
	{
		this.block_face = face;
		this.block_top = top;
		if (team > 7)
		{
			team = 0;
		}
		if (!self || PlayerControl.GetGameMode() != CONST.CFG.BUILD_MODE)
		{
			this.SetBlockTexture(this.teamblock[team]);
			return;
		}
		if (this.blocksel != null)
		{
			this.SetBlockTexture(this.blocksel);
			return;
		}
		this.SetBlockTexture(this.teamblock[team]);
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x00052E94 File Offset: 0x00051094
	public void SetBlockTextureForBuild(GameObject face, GameObject top, int flag)
	{
		ZipLoader zipLoader = Object.FindObjectOfType<ZipLoader>();
		Block block;
		if (zipLoader != null)
		{
			block = zipLoader.GetBlock(flag);
		}
		else
		{
			if (this.blockSet == null)
			{
				Map map = Object.FindObjectOfType<Map>();
				this.blockSet = map.GetBlockSet();
			}
			block = this.blockSet.GetBlock(flag);
		}
		if (block == null)
		{
			return;
		}
		if (block.GetTexture() == null)
		{
			return;
		}
		if (face == null || top == null)
		{
			return;
		}
		face.GetComponent<Renderer>().materials[0].mainTexture = block.GetTexture();
		Rect previewFace = block.GetPreviewFace();
		face.GetComponent<Renderer>().materials[0].mainTextureOffset = new Vector2(previewFace.x, previewFace.y + 0.0625f);
		face.GetComponent<Renderer>().materials[0].mainTextureScale = new Vector2(previewFace.width, previewFace.height);
		top.GetComponent<Renderer>().materials[0].mainTexture = block.GetTexture();
		Rect topFace = block.GetTopFace();
		top.GetComponent<Renderer>().materials[0].mainTextureOffset = new Vector2(topFace.x, topFace.y);
		top.GetComponent<Renderer>().materials[0].mainTextureScale = new Vector2(topFace.width, topFace.height);
	}

	// Token: 0x06000422 RID: 1058 RVA: 0x00052FE7 File Offset: 0x000511E7
	public Block GetBlockTextureTeam(int team)
	{
		if (team > 7)
		{
			return this.teamblock[0];
		}
		return this.teamblock[team];
	}

	// Token: 0x06000423 RID: 1059 RVA: 0x00053000 File Offset: 0x00051200
	public void SetBlockTexture(Block block)
	{
		if (block == null)
		{
			return;
		}
		if (block.GetTexture() == null)
		{
			return;
		}
		if (this.block_face == null || this.block_top == null)
		{
			return;
		}
		this.block_face.GetComponent<Renderer>().materials[0].mainTexture = block.GetTexture();
		Rect previewFace = block.GetPreviewFace();
		this.block_face.GetComponent<Renderer>().materials[0].mainTextureOffset = new Vector2(previewFace.x, previewFace.y + 0.0625f);
		this.block_face.GetComponent<Renderer>().materials[0].mainTextureScale = new Vector2(previewFace.width, previewFace.height);
		this.block_top.GetComponent<Renderer>().materials[0].mainTexture = block.GetTexture();
		Rect topFace = block.GetTopFace();
		this.block_top.GetComponent<Renderer>().materials[0].mainTextureOffset = new Vector2(topFace.x, topFace.y);
		this.block_top.GetComponent<Renderer>().materials[0].mainTextureScale = new Vector2(topFace.width, topFace.height);
	}

	// Token: 0x06000424 RID: 1060 RVA: 0x00053134 File Offset: 0x00051334
	private void DoInventoryWindow(int windowID)
	{
		this.selectedBlock = this.DrawInventory(this.blockSet, ref this.scrollPosition, this.selectedBlock);
		if (this.selectedBlock != null && this.selectedBlock != this.blocksel)
		{
			this.blocksel = this.selectedBlock;
			this.SetBlockTexture(this.blocksel);
			int num = ZipLoader.GetBlock(this.blocksel.GetName());
			if (this.blocksel.GetName() == "Water")
			{
				num = 7;
			}
			if (this.cscl == null)
			{
				this.cscl = Object.FindObjectOfType<Client>();
			}
			if (this.csws == null)
			{
				this.csws = Object.FindObjectOfType<WeaponSystem>();
			}
			if (num > 0)
			{
				this.cscl.send_selectblock((byte)num);
				BotsController.Instance.Bots[PlayerProfile.myindex].blockFlag = num;
				BotsController.Instance.SetCurrentWeapon(PlayerProfile.myindex, 0);
				BotsController.Instance.Bots[PlayerProfile.myindex].WeaponID = 0;
			}
		}
	}

	// Token: 0x06000425 RID: 1061 RVA: 0x0005323C File Offset: 0x0005143C
	private Block DrawInventory(BlockSet blockSet, ref Vector2 scrollPosition, Block selected)
	{
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, Array.Empty<GUILayoutOption>());
		int i = 0;
		int num = 0;
		while (i < blockSet.GetBlockCount())
		{
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			int j = 0;
			while (j < 8)
			{
				Block block = blockSet.GetBlock(i);
				PlayerControl.isPrivateAdmin();
				if (block != null && block.GetName() != null && (block.GetName() == "Brick_blue" || block.GetName() == "Brick_red" || block.GetName() == "Brick_green" || block.GetName() == "Brick_yellow" || block.GetName() == "!Water" || block.GetName() == "TNT" || block.GetName() == "ArmoredBrickBlue" || block.GetName() == "ArmoredBrickRed" || block.GetName() == "ArmoredBrickGreen" || block.GetName() == "ArmoredBrickYellow"))
				{
					j--;
				}
				else if (MainGUI.DrawBlock(block, block == selected && selected != null))
				{
					selected = block;
				}
				j++;
				i++;
			}
			GUILayout.EndHorizontal();
			num++;
		}
		GUILayout.EndScrollView();
		return selected;
	}

	// Token: 0x06000426 RID: 1062 RVA: 0x00053398 File Offset: 0x00051598
	private static bool DrawBlock(Block block, bool selected)
	{
		Rect aspectRect = GUILayoutUtility.GetAspectRect(1f);
		if (selected)
		{
			GUI.Box(aspectRect, GUIContent.none);
		}
		Vector3 vector = aspectRect.center;
		aspectRect.width -= 8f;
		aspectRect.height -= 8f;
		aspectRect.center = vector;
		return block != null && block.DrawPreview(aspectRect);
	}

	// Token: 0x06000427 RID: 1063 RVA: 0x0005340C File Offset: 0x0005160C
	public static void UpdateCursorLock()
	{
		if (MainGUI.ForceCursor)
		{
			Cursor.visible = true;
			if (Cursor.lockState.Equals(0))
			{
				return;
			}
			Cursor.lockState = 0;
			return;
		}
		else
		{
			Cursor.visible = false;
			if (Cursor.lockState.Equals(1))
			{
				return;
			}
			Cursor.lockState = 1;
			return;
		}
	}

	// Token: 0x04000877 RID: 2167
	private Vector2 scrollPosition = Vector3.zero;

	// Token: 0x04000878 RID: 2168
	private E_Menu EM;

	// Token: 0x04000879 RID: 2169
	private New_Slots NS;

	// Token: 0x0400087A RID: 2170
	private New_Select_Team NST;

	// Token: 0x0400087B RID: 2171
	public static bool sel_team;

	// Token: 0x0400087C RID: 2172
	public static bool e_menu;

	// Token: 0x0400087D RID: 2173
	private WeaponSystem csws;

	// Token: 0x0400087E RID: 2174
	private GameObject block_face;

	// Token: 0x0400087F RID: 2175
	private GameObject block_top;

	// Token: 0x04000880 RID: 2176
	private GameObject goMap;

	// Token: 0x04000881 RID: 2177
	private GameObject goPlayer;

	// Token: 0x04000882 RID: 2178
	private Client cscl;

	// Token: 0x04000883 RID: 2179
	private ZipLoader cszl;

	// Token: 0x04000884 RID: 2180
	private PlayerControl cspc;

	// Token: 0x04000885 RID: 2181
	private Radar csr;

	// Token: 0x04000886 RID: 2182
	private Block[] teamblock = new Block[8];

	// Token: 0x04000887 RID: 2183
	private BlockSet blockSet;

	// Token: 0x04000888 RID: 2184
	private Texture tex_inv;

	// Token: 0x04000889 RID: 2185
	private int last_block;

	// Token: 0x0400088A RID: 2186
	private Block selectedBlock;

	// Token: 0x0400088B RID: 2187
	private Block blocksel;

	// Token: 0x0400088C RID: 2188
	private GUIStyle gui_style;

	// Token: 0x0400088D RID: 2189
	public static bool ForceCursor;

	// Token: 0x0400088E RID: 2190
	private float g_buycheck;

	// Token: 0x0400088F RID: 2191
	private bool g_canbuy = true;

	// Token: 0x04000890 RID: 2192
	private TankController tc;

	// Token: 0x04000891 RID: 2193
	private CarController cc;
}
