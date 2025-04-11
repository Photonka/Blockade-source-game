using System;
using UnityEngine;

// Token: 0x0200007F RID: 127
public class InventoryGUI : MonoBehaviour
{
	// Token: 0x06000390 RID: 912 RVA: 0x00043324 File Offset: 0x00041524
	private void Awake()
	{
		this.csr = (Radar)Object.FindObjectOfType(typeof(Radar));
		Map map = (Map)Object.FindObjectOfType(typeof(Map));
		this.cszl = (ZipLoader)Object.FindObjectOfType(typeof(ZipLoader));
		this.blockSet = map.GetBlockSet();
		this.blocksel = null;
		this.goMap = GameObject.Find("Map");
		this.gui_style = new GUIStyle();
		this.gui_style.font = FontManager.font[2];
		this.gui_style.fontSize = 14;
		this.gui_style.normal.textColor = new Color(1f, 1f, 1f, 1f);
		this.gui_style.alignment = 7;
		this.blackTexture = new Texture2D(1, 1);
		this.blackTexture.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.5f));
		this.blackTexture.Apply();
		this.yellowTexture = new Texture2D(1, 1);
		this.yellowTexture.SetPixel(0, 0, new Color(1f, 0.75f, 0.25f, 0.5f));
		this.yellowTexture.Apply();
		this.tex_shovel = (Resources.Load("GUI/shop/shovel") as Texture2D);
		this.tex_cube = (Resources.Load("GUI/shop/cube") as Texture2D);
		this.tex_medkit_w = (Resources.Load("GUI/shop/medkit_w") as Texture2D);
		this.tex_mp5 = (Resources.Load("GUI/shop/mp5") as Texture2D);
		this.tex_m3 = (Resources.Load("GUI/shop/m3") as Texture2D);
		this.tex_m14 = (Resources.Load("GUI/shop/m14") as Texture2D);
		this.tex_ak47 = (Resources.Load("GUI/shop/ak47") as Texture2D);
		this.tex_svd = (Resources.Load("GUI/shop/svd") as Texture2D);
		this.tex_glock = (Resources.Load("GUI/shop/glock") as Texture2D);
		this.tex_deagle = (Resources.Load("GUI/shop/deagle") as Texture2D);
		this.tex_asval = (Resources.Load("GUI/shop/asval") as Texture2D);
		this.tex_g36c = (Resources.Load("GUI/shop/g36c") as Texture2D);
		this.tex_kriss = (Resources.Load("GUI/shop/kriss") as Texture2D);
		this.tex_m4a1 = (Resources.Load("GUI/shop/m4a1") as Texture2D);
		this.tex_m249 = (Resources.Load("GUI/shop/m249") as Texture2D);
		this.tex_sps12 = (Resources.Load("GUI/shop/sps12") as Texture2D);
		this.tex_vintorez = (Resources.Load("GUI/shop/vintorez") as Texture2D);
		this.tex_vsk94 = (Resources.Load("GUI/shop/vsk94") as Texture2D);
		this.tex_usp = (Resources.Load("GUI/shop/usp") as Texture2D);
		this.tex_barrett = (Resources.Load("GUI/shop/barrett") as Texture2D);
		this.tex_tmp = (Resources.Load("GUI/shop/tmp") as Texture2D);
		this.tex_minigun = (Resources.Load("GUI/shop/minigun") as Texture2D);
		this.tex_knife = (Resources.Load("GUI/shop/knife") as Texture2D);
		this.tex_axe = (Resources.Load("GUI/shop/axe") as Texture2D);
		this.tex_bat = (Resources.Load("GUI/shop/bat") as Texture2D);
		this.tex_crowbar = (Resources.Load("GUI/shop/crowbar") as Texture2D);
		this.tex_caramel = (Resources.Load("GUI/shop/caramel") as Texture2D);
		this.tex_auga3 = (Resources.Load("GUI/shop/aug") as Texture2D);
		this.tex_sg552 = (Resources.Load("GUI/shop/sg552") as Texture2D);
		this.tex_m14ebr = (Resources.Load("GUI/shop/m14ebr") as Texture2D);
		this.tex_l96a1 = (Resources.Load("GUI/shop/l96a1") as Texture2D);
		this.tex_nova = (Resources.Load("GUI/shop/nova") as Texture2D);
		this.tex_kord = (Resources.Load("GUI/shop/kord") as Texture2D);
		this.tex_anaconda = (Resources.Load("GUI/shop/anaconda") as Texture2D);
		this.tex_scar = (Resources.Load("GUI/shop/scar") as Texture2D);
		this.tex_p90 = (Resources.Load("GUI/shop/p90") as Texture2D);
		this.tex_rpk = (Resources.Load("GUI/shop/rpk") as Texture2D);
		this.tex_hk416 = (Resources.Load("GUI/shop/hk416") as Texture2D);
		this.tex_ak102 = (Resources.Load("GUI/shop/ak102") as Texture2D);
		this.tex_sr25 = (Resources.Load("GUI/shop/sr25") as Texture2D);
		this.tex_mglmk1 = (Resources.Load("GUI/shop/mglmk1") as Texture2D);
		this.tex_mosin = (Resources.Load("GUI/shop/mosin") as Texture2D);
		this.tex_ppsh = (Resources.Load("GUI/shop/ppsh") as Texture2D);
		this.tex_mp40 = (Resources.Load("GUI/shop/mp40") as Texture2D);
		this.tex_l96a1mod = (Resources.Load("GUI/shop/l96a1mod") as Texture2D);
		this.tex_kacpdw = (Resources.Load("GUI/shop/kacpdw") as Texture2D);
		this.tex_famas = (Resources.Load("GUI/shop/famas") as Texture2D);
		this.tex_beretta = (Resources.Load("GUI/shop/beretta") as Texture2D);
		this.tex_machete = (Resources.Load("GUI/shop/machete") as Texture2D);
		this.tex_repair_tool = (Resources.Load("GUI/shop/repair tool") as Texture2D);
		this.tex_aa12 = (Resources.Load("GUI/shop/aa12") as Texture2D);
		this.tex_fn57 = (Resources.Load("GUI/shop/fn57") as Texture2D);
		this.tex_fs2000 = (Resources.Load("GUI/shop/fs2000") as Texture2D);
		this.tex_l85 = (Resources.Load("GUI/shop/l85") as Texture2D);
		this.tex_mac10 = (Resources.Load("GUI/shop/mac10") as Texture2D);
		this.tex_pkp = (Resources.Load("GUI/shop/pkp") as Texture2D);
		this.tex_pm = (Resources.Load("GUI/shop/pm") as Texture2D);
		this.tex_tar21 = (Resources.Load("GUI/shop/tar21") as Texture2D);
		this.tex_ump45 = (Resources.Load("GUI/shop/ump45") as Texture2D);
		this.tex_ntw20 = (Resources.Load("GUI/shop/ntw20") as Texture2D);
		this.tex_vintorez_desert = (Resources.Load("GUI/shop/vintorez_desert") as Texture2D);
		this.tex_tank_default = (Resources.Load("GUI/shop/tank_default") as Texture2D);
		this.tex_tank_light = (Resources.Load("GUI/shop/tank_light") as Texture2D);
		this.tex_tank_heavy = (Resources.Load("GUI/shop/tank_heavy") as Texture2D);
		this.tex_zaa12 = (Resources.Load("GUI/shop/zaa12") as Texture2D);
		this.tex_zasval = (Resources.Load("GUI/shop/zasval") as Texture2D);
		this.tex_zfn57 = (Resources.Load("GUI/shop/zfn57") as Texture2D);
		this.tex_zkord = (Resources.Load("GUI/shop/zkord") as Texture2D);
		this.tex_zm249 = (Resources.Load("GUI/shop/zm249") as Texture2D);
		this.tex_zminigun = (Resources.Load("GUI/shop/zminigun") as Texture2D);
		this.tex_zsps12 = (Resources.Load("GUI/shop/zsps12") as Texture2D);
		this.teamblock[0] = this.blockSet.GetBlock("Brick_blue");
		this.teamblock[1] = this.blockSet.GetBlock("Brick_red");
		this.teamblock[2] = this.blockSet.GetBlock("Brick_green");
		this.teamblock[3] = this.blockSet.GetBlock("Brick_yellow");
		this.tex_inv = (Resources.Load("GUI/inv") as Texture2D);
	}

	// Token: 0x06000391 RID: 913 RVA: 0x00043AC0 File Offset: 0x00041CC0
	private void Update()
	{
		if (this.tc == null)
		{
			this.tc = (TankController)Object.FindObjectOfType(typeof(TankController));
		}
		if (this.tc != null && this.tc.enabled)
		{
			this.g_canbuy = false;
		}
		if (Input.GetKeyDown(101) && this.g_canbuy)
		{
			this.show = !this.show;
			MainGUI.ForceCursor = this.show;
		}
		if (Time.time > this.g_buycheck)
		{
			if (this.cspc == null)
			{
				this.cspc = (PlayerControl)Object.FindObjectOfType(typeof(PlayerControl));
			}
			this.g_canbuy = false;
			this.g_buycheck += 0.5f;
			if (PlayerControl.GetGameMode() == 1 || PlayerControl.GetGameMode() == 2 || PlayerControl.GetGameMode() == 3 || PlayerControl.GetGameMode() == 7 || PlayerControl.GetGameMode() == 9 || PlayerControl.GetGameMode() == 10)
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
				if ((PlayerControl.GetGameMode() == 0 && this.cszl.rblock.Count == 32) || (PlayerControl.GetGameMode() == 8 || (this.cszl.mapversion > 0 && PlayerControl.GetGameMode() == 5)) || PlayerControl.GetGameMode() == 11)
				{
					for (int i = 0; i < this.cszl.rblock.Count; i++)
					{
						if (this.cszl.rblock[i].mode == PlayerControl.GetGameMode() && this.cszl.rblock[i].team == this.cspc.GetTeam() && Mathf.Abs(this.cszl.rblock[i].pos.x - this.goPlayer.transform.position.x) < 4f && Mathf.Abs(this.cszl.rblock[i].pos.z - this.goPlayer.transform.position.z) < 4f)
						{
							this.g_canbuy = true;
						}
					}
					return;
				}
				int num = 8;
				if (PlayerControl.GetGameMode() == 5)
				{
					num = 24;
				}
				if (Mathf.Abs(this.csr.team_pos[this.cspc.GetTeam()].x - this.goPlayer.transform.position.x) < (float)num && Mathf.Abs(this.csr.team_pos[this.cspc.GetTeam()].z - this.goPlayer.transform.position.z) < (float)num)
				{
					this.g_canbuy = true;
				}
			}
		}
	}

	// Token: 0x06000392 RID: 914 RVA: 0x00043DDC File Offset: 0x00041FDC
	private void OnGUI()
	{
		if (this.show)
		{
			if (!this.g_canbuy)
			{
				this.show = !this.show;
				MainGUI.ForceCursor = this.show;
				return;
			}
			if (PlayerControl.GetGameMode() == 0 || PlayerControl.GetGameMode() == 3 || PlayerControl.GetGameMode() == 4 || PlayerControl.GetGameMode() == 5 || PlayerControl.GetGameMode() == 7 || PlayerControl.GetGameMode() == 9 || PlayerControl.GetGameMode() == 10 || PlayerControl.GetGameMode() == 11)
			{
				this.InventoryBattle();
			}
			else if (PlayerControl.GetGameMode() == 6)
			{
				this.InventoryCarnage();
			}
			else if (PlayerControl.GetGameMode() == 8)
			{
				this.InventoryClassic();
			}
			else if (PlayerControl.GetGameMode() == 1 || PlayerControl.GetGameMode() == 2)
			{
				Rect rect;
				rect..ctor(0f, 0f, (float)Screen.width * 0.5f, (float)Screen.height * 0.6f);
				rect.center = new Vector2((float)Screen.width, (float)Screen.height) / 2f;
				GUILayout.Window(1, rect, new GUI.WindowFunction(this.DoInventoryWindow), "", this.gui_style, Array.Empty<GUILayoutOption>());
			}
		}
		if (this.g_canbuy)
		{
			GUI.DrawTexture(new Rect((float)(Screen.width - 40), (float)(Screen.height - 220), 32f, 32f), this.tex_inv);
		}
	}

	// Token: 0x06000393 RID: 915 RVA: 0x00043F40 File Offset: 0x00042140
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
				this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
			}
			if (num > 0)
			{
				this.cscl.send_selectblock((byte)num);
			}
		}
	}

	// Token: 0x06000394 RID: 916 RVA: 0x00044000 File Offset: 0x00042200
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
				if (block != null && block.GetName() != null && (block.GetName() == "Brick_blue" || block.GetName() == "Brick_red" || block.GetName() == "Brick_green" || block.GetName() == "Brick_yellow" || block.GetName() == "!Water" || block.GetName() == "TNT"))
				{
					j--;
				}
				else if (InventoryGUI.DrawBlock(block, block == selected && selected != null))
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

	// Token: 0x06000395 RID: 917 RVA: 0x00044108 File Offset: 0x00042308
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

	// Token: 0x06000396 RID: 918 RVA: 0x0004417C File Offset: 0x0004237C
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

	// Token: 0x06000397 RID: 919 RVA: 0x000442B0 File Offset: 0x000424B0
	private void InventoryBattle()
	{
		if (this.cscl == null)
		{
			this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
		}
		int myindex = this.cscl.myindex;
		int num = 0;
		GUIManager.DrawText(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height / 2f - 200f), Lang.GetLabel(128), 20, 7, 8);
		this.x_pos = 24f;
		this.y_pos = (float)Screen.height / 2f - 200f + 4f;
		GUI.DrawTexture(new Rect(0f, (float)Screen.height / 2f - 200f, (float)Screen.width, 72f), this.blackTexture);
		this.DrawItem(this.tex_m14, "M14", 2, 3, num);
		num++;
		this.x_pos = 24f;
		this.y_pos += 84f;
		GUI.DrawTexture(new Rect(0f, this.y_pos - 4f, (float)Screen.width, 72f), this.blackTexture);
		this.DrawItem(this.tex_mp5, "MP5", 0, 4, num);
		num++;
		this.x_pos = 24f;
		this.y_pos += 84f;
		GUI.DrawTexture(new Rect(0f, this.y_pos - 4f, (float)Screen.width, 72f), this.blackTexture);
		this.DrawItem(this.tex_m3, "M3", 1, 2, num);
		num++;
		if (BotsController.Instance.Bots[myindex].Item[8] == 1 || BotsController.Instance.Bots[myindex].Item[20] == 1 || BotsController.Instance.Bots[myindex].Item[34] == 1 || BotsController.Instance.Bots[myindex].Item[48] == 1 || BotsController.Instance.Bots[myindex].Item[52] == 1 || BotsController.Instance.Bots[myindex].Item[57] == 1)
		{
			this.x_pos = 24f;
			this.y_pos += 84f;
			GUI.DrawTexture(new Rect(0f, this.y_pos - 4f, (float)Screen.width, 72f), this.blackTexture);
			this.DrawItem(this.tex_glock, "GLOCK", 0, 9, num);
			num++;
		}
		if (BotsController.Instance.Bots[myindex].Item[23] == 1 || BotsController.Instance.Bots[myindex].Item[24] == 1 || BotsController.Instance.Bots[myindex].Item[25] == 1 || BotsController.Instance.Bots[myindex].Item[26] == 1 || BotsController.Instance.Bots[myindex].Item[27] == 1 || BotsController.Instance.Bots[myindex].Item[49] == 1 || BotsController.Instance.Bots[myindex].Item[50] == 1)
		{
			this.x_pos = 24f;
			this.y_pos += 84f;
			GUI.DrawTexture(new Rect(0f, this.y_pos - 4f, (float)Screen.width, 72f), this.blackTexture);
			this.DrawItem(this.tex_shovel, Lang.GetLabel(166), 0, 1, num);
			num++;
		}
		if ((BotsController.Instance.Bots[myindex].Item[62] == 1 || BotsController.Instance.Bots[myindex].Item[63] == 1 || BotsController.Instance.Bots[myindex].Item[64] == 1) && PlayerControl.GetGameMode() == 11)
		{
			this.x_pos = 24f;
			this.y_pos += 84f;
			GUI.DrawTexture(new Rect(0f, this.y_pos - 4f, (float)Screen.width, 72f), this.blackTexture);
		}
	}

	// Token: 0x06000398 RID: 920 RVA: 0x000446F0 File Offset: 0x000428F0
	private void InventoryCarnage()
	{
		if (this.cscl == null)
		{
			this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
		}
		int myindex = this.cscl.myindex;
		int num = 0;
		GUIManager.DrawText(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height / 2f - 120f), Lang.GetLabel(128), 20, 7, 8);
		float num2 = (float)Screen.height / 2f - 120f;
		GUI.DrawTexture(new Rect(0f, num2 + 4f, (float)Screen.width, 72f), this.blackTexture);
		this.x_pos = 24f;
		this.y_pos = num2 + 4f;
		this.DrawItem(this.tex_shovel, Lang.GetLabel(166), 0, 1, num);
		num++;
	}

	// Token: 0x06000399 RID: 921 RVA: 0x000447DC File Offset: 0x000429DC
	private void InventoryClassic()
	{
		if (this.cscl == null)
		{
			this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
		}
		int myindex = this.cscl.myindex;
		GUIManager.DrawText(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height / 2f - 120f), Lang.GetLabel(128), 20, 7, 8);
		float num = (float)Screen.height / 2f - 120f;
		GUI.DrawTexture(new Rect(0f, num + 4f, (float)Screen.width, 72f), this.blackTexture);
		this.x_pos = 24f;
		this.y_pos = num + 4f;
	}

	// Token: 0x0600039A RID: 922 RVA: 0x000448AC File Offset: 0x00042AAC
	private void DrawItem(Texture2D tex, string name, int cid, int wid, int id)
	{
		if (this.x_pos + 70f > (float)Screen.width)
		{
			this.x_pos = 24f;
			this.y_pos += 76f;
			GUI.DrawTexture(new Rect(0f, this.y_pos - 4f, (float)Screen.width, 72f), this.blackTexture);
		}
		float x = Input.mousePosition.x;
		float num = (float)Screen.height - Input.mousePosition.y;
		float num2 = this.x_pos;
		float num3 = this.y_pos;
		Rect rect;
		rect..ctor(num2, num3, 64f, 64f);
		Rect rect2;
		rect2..ctor(num2 + 1f, num3 + 1f, 64f, 64f);
		if (rect.Contains(new Vector2(x, num)))
		{
			if (!this.hover[id])
			{
				this.hover[id] = true;
			}
		}
		else if (this.hover[id])
		{
			this.hover[id] = false;
		}
		this.x_pos += 70f;
		if (this.hover[id])
		{
			rect..ctor(num2, num3 - 2f, 64f, 64f);
			rect2..ctor(num2 + 1f, num3 + 1f - 2f, 65f, 65f);
		}
		if (this.hover[id])
		{
			GUI.DrawTexture(rect, this.yellowTexture);
		}
		GUI.DrawTexture(rect, tex);
		this.gui_style.normal.textColor = new Color(0f, 0f, 0f, 1f);
		GUI.Label(rect2, name, this.gui_style);
		this.gui_style.normal.textColor = new Color(1f, 1f, 1f, 1f);
		GUI.Label(rect, name, this.gui_style);
		if (GUI.Button(rect, "", this.gui_style))
		{
			this.show = !this.show;
			MainGUI.ForceCursor = this.show;
			if (this.cscl == null)
			{
				this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
			}
			int myindex = this.cscl.myindex;
			byte team = BotsController.Instance.Bots[myindex].Team;
			if (wid < 100)
			{
				this.cscl.send_jointeamclass(team, (byte)cid);
				return;
			}
			this.cscl.send_spawn_my_vehicle(wid);
		}
	}

	// Token: 0x0400074E RID: 1870
	private BlockSet blockSet;

	// Token: 0x0400074F RID: 1871
	private Block[] teamblock = new Block[4];

	// Token: 0x04000750 RID: 1872
	private bool show;

	// Token: 0x04000751 RID: 1873
	private Vector2 scrollPosition = Vector3.zero;

	// Token: 0x04000752 RID: 1874
	private GameObject goMap;

	// Token: 0x04000753 RID: 1875
	private GameObject goPlayer;

	// Token: 0x04000754 RID: 1876
	private GameObject block_face;

	// Token: 0x04000755 RID: 1877
	private GameObject block_top;

	// Token: 0x04000756 RID: 1878
	private Client cscl;

	// Token: 0x04000757 RID: 1879
	private ZipLoader cszl;

	// Token: 0x04000758 RID: 1880
	private PlayerControl cspc;

	// Token: 0x04000759 RID: 1881
	private Radar csr;

	// Token: 0x0400075A RID: 1882
	private GUIStyle gui_style;

	// Token: 0x0400075B RID: 1883
	public Texture2D tex_cube;

	// Token: 0x0400075C RID: 1884
	public Texture2D tex_medkit_w;

	// Token: 0x0400075D RID: 1885
	public Texture2D tex_mp5;

	// Token: 0x0400075E RID: 1886
	public Texture2D tex_m3;

	// Token: 0x0400075F RID: 1887
	public Texture2D tex_m14;

	// Token: 0x04000760 RID: 1888
	public Texture2D tex_ak47;

	// Token: 0x04000761 RID: 1889
	public Texture2D tex_svd;

	// Token: 0x04000762 RID: 1890
	public Texture2D tex_glock;

	// Token: 0x04000763 RID: 1891
	public Texture2D tex_deagle;

	// Token: 0x04000764 RID: 1892
	public Texture2D tex_asval;

	// Token: 0x04000765 RID: 1893
	public Texture2D tex_g36c;

	// Token: 0x04000766 RID: 1894
	public Texture2D tex_kriss;

	// Token: 0x04000767 RID: 1895
	public Texture2D tex_m4a1;

	// Token: 0x04000768 RID: 1896
	public Texture2D tex_m249;

	// Token: 0x04000769 RID: 1897
	public Texture2D tex_sps12;

	// Token: 0x0400076A RID: 1898
	public Texture2D tex_vintorez;

	// Token: 0x0400076B RID: 1899
	public Texture2D tex_vsk94;

	// Token: 0x0400076C RID: 1900
	public Texture2D tex_usp;

	// Token: 0x0400076D RID: 1901
	public Texture2D tex_barrett;

	// Token: 0x0400076E RID: 1902
	public Texture2D tex_tmp;

	// Token: 0x0400076F RID: 1903
	public Texture2D tex_shovel;

	// Token: 0x04000770 RID: 1904
	public Texture2D tex_knife;

	// Token: 0x04000771 RID: 1905
	public Texture2D tex_axe;

	// Token: 0x04000772 RID: 1906
	public Texture2D tex_bat;

	// Token: 0x04000773 RID: 1907
	public Texture2D tex_crowbar;

	// Token: 0x04000774 RID: 1908
	public Texture2D tex_caramel;

	// Token: 0x04000775 RID: 1909
	public Texture2D tex_auga3;

	// Token: 0x04000776 RID: 1910
	public Texture2D tex_sg552;

	// Token: 0x04000777 RID: 1911
	public Texture2D tex_m14ebr;

	// Token: 0x04000778 RID: 1912
	public Texture2D tex_l96a1;

	// Token: 0x04000779 RID: 1913
	public Texture2D tex_kord;

	// Token: 0x0400077A RID: 1914
	public Texture2D tex_nova;

	// Token: 0x0400077B RID: 1915
	public Texture2D tex_p90;

	// Token: 0x0400077C RID: 1916
	public Texture2D tex_scar;

	// Token: 0x0400077D RID: 1917
	public Texture2D tex_anaconda;

	// Token: 0x0400077E RID: 1918
	public Texture2D tex_rpk;

	// Token: 0x0400077F RID: 1919
	public Texture2D tex_hk416;

	// Token: 0x04000780 RID: 1920
	public Texture2D tex_ak102;

	// Token: 0x04000781 RID: 1921
	public Texture2D tex_sr25;

	// Token: 0x04000782 RID: 1922
	public Texture2D tex_mglmk1;

	// Token: 0x04000783 RID: 1923
	public Texture2D tex_mosin;

	// Token: 0x04000784 RID: 1924
	public Texture2D tex_ppsh;

	// Token: 0x04000785 RID: 1925
	public Texture2D tex_mp40;

	// Token: 0x04000786 RID: 1926
	public Texture2D tex_l96a1mod;

	// Token: 0x04000787 RID: 1927
	public Texture2D tex_kacpdw;

	// Token: 0x04000788 RID: 1928
	public Texture2D tex_famas;

	// Token: 0x04000789 RID: 1929
	public Texture2D tex_beretta;

	// Token: 0x0400078A RID: 1930
	public Texture2D tex_machete;

	// Token: 0x0400078B RID: 1931
	public Texture2D tex_repair_tool;

	// Token: 0x0400078C RID: 1932
	public Texture2D tex_aa12;

	// Token: 0x0400078D RID: 1933
	public Texture2D tex_fn57;

	// Token: 0x0400078E RID: 1934
	public Texture2D tex_fs2000;

	// Token: 0x0400078F RID: 1935
	public Texture2D tex_l85;

	// Token: 0x04000790 RID: 1936
	public Texture2D tex_mac10;

	// Token: 0x04000791 RID: 1937
	public Texture2D tex_pkp;

	// Token: 0x04000792 RID: 1938
	public Texture2D tex_pm;

	// Token: 0x04000793 RID: 1939
	public Texture2D tex_tar21;

	// Token: 0x04000794 RID: 1940
	public Texture2D tex_ump45;

	// Token: 0x04000795 RID: 1941
	public Texture2D tex_ntw20;

	// Token: 0x04000796 RID: 1942
	public Texture2D tex_vintorez_desert;

	// Token: 0x04000797 RID: 1943
	public Texture2D tex_tank_default;

	// Token: 0x04000798 RID: 1944
	public Texture2D tex_tank_light;

	// Token: 0x04000799 RID: 1945
	public Texture2D tex_tank_heavy;

	// Token: 0x0400079A RID: 1946
	public Texture2D tex_minigun;

	// Token: 0x0400079B RID: 1947
	public Texture2D tex_zaa12;

	// Token: 0x0400079C RID: 1948
	public Texture2D tex_zasval;

	// Token: 0x0400079D RID: 1949
	public Texture2D tex_zfn57;

	// Token: 0x0400079E RID: 1950
	public Texture2D tex_zkord;

	// Token: 0x0400079F RID: 1951
	public Texture2D tex_zm249;

	// Token: 0x040007A0 RID: 1952
	public Texture2D tex_zminigun;

	// Token: 0x040007A1 RID: 1953
	public Texture2D tex_zsps12;

	// Token: 0x040007A2 RID: 1954
	private Texture2D blackTexture;

	// Token: 0x040007A3 RID: 1955
	private Texture2D yellowTexture;

	// Token: 0x040007A4 RID: 1956
	private Texture2D tex_inv;

	// Token: 0x040007A5 RID: 1957
	private float x_pos;

	// Token: 0x040007A6 RID: 1958
	private float y_pos;

	// Token: 0x040007A7 RID: 1959
	private bool[] hover = new bool[128];

	// Token: 0x040007A8 RID: 1960
	private Block selectedBlock;

	// Token: 0x040007A9 RID: 1961
	private Block blocksel;

	// Token: 0x040007AA RID: 1962
	private TankController tc;

	// Token: 0x040007AB RID: 1963
	private float g_buycheck;

	// Token: 0x040007AC RID: 1964
	private bool g_canbuy;
}
