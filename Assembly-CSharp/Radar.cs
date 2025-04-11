using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000086 RID: 134
public class Radar : MonoBehaviour
{
	// Token: 0x060003BC RID: 956 RVA: 0x00049458 File Offset: 0x00047658
	private void Awake()
	{
		this.RadarTexture = new Texture2D(256, 256);
		Color color;
		color..ctor(0f, 0.5f, 0f, 0.5f);
		for (int i = 0; i < 256; i++)
		{
			for (int j = 0; j < 256; j++)
			{
				this.RadarTexture.SetPixel(i, j, color);
			}
		}
		this.RadarTexture.Apply();
		this.RadarTexture.filterMode = 0;
		this.ZombiePosition = (Resources.Load("GUI/radar_zpos") as Texture2D);
		this.PlayerPosition = (Resources.Load("GUI/radar_pos") as Texture2D);
		this.TeamPosition = (Resources.Load("GUI/radar_team") as Texture2D);
		this.BasePosition = (Resources.Load("GUI/radar_base") as Texture2D);
		this.FlagPosition = new Texture2D[2];
		this.FlagPosition[0] = (Resources.Load("GUI/flag_blue") as Texture2D);
		this.FlagPosition[1] = (Resources.Load("GUI/flag_red") as Texture2D);
		this.map = (Map)Object.FindObjectOfType(typeof(Map));
		this.gui_style = new GUIStyle();
		this.gui_style.fontSize = 10;
		this.gui_style.alignment = 4;
		this.gui_style.normal.textColor = new Color(255f, 255f, 255f, 150f);
		this.tmpTex = new Texture2D(32, 32);
		this.blockset = this.map.GetBlockSet();
		this.blockTex = (this.blockset.GetAtlas(0).GetTexture() as Texture2D);
		for (int k = 0; k < 4; k++)
		{
			this._col[k] = Color.white;
			this.oldFlagScores[k] = 150;
			this.mig[k] = false;
		}
	}

	// Token: 0x060003BD RID: 957 RVA: 0x00049640 File Offset: 0x00047840
	private void OnGUI()
	{
		if (this.cscl == null)
		{
			this.cscl = Object.FindObjectOfType<Client>();
		}
		if (this.LocalPlayer == null)
		{
			this.LocalPlayer = GameObject.Find("Player");
		}
		if (this.cscl == null)
		{
			return;
		}
		GUI.DrawTexture(new Rect(GUIManager.XRES(4f), GUIManager.YRES(4f), GUIManager.YRES(128f), GUIManager.YRES(128f)), this.RadarTexture);
		if (this.RadarTexture2)
		{
			GUI.DrawTexture(new Rect(GUIManager.XRES(4f), GUIManager.YRES(4f), GUIManager.YRES(128f), GUIManager.YRES(128f)), this.RadarTexture2);
		}
		this.DrawBase(this.base_pos.x, this.base_pos.z);
		if (PlayerControl.GetGameMode() == CONST.CFG.BATTLE_MODE)
		{
			GUI.color = Color.blue;
			this.DrawTeam(this.team_pos[0].x, this.team_pos[0].z);
			GUI.color = Color.red;
			this.DrawTeam(this.team_pos[1].x, this.team_pos[1].z);
			GUI.color = Color.green;
			this.DrawTeam(this.team_pos[2].x, this.team_pos[2].z);
			GUI.color = Color.yellow;
			this.DrawTeam(this.team_pos[3].x, this.team_pos[3].z);
		}
		for (int i = 0; i < 32; i++)
		{
			if (i != this.cscl.myindex && BotsController.Instance.Bots[i].Active && BotsController.Instance.Bots[i].Team != 255 && BotsController.Instance.Bots[i].Dead != 1)
			{
				this.UpdPlayer(i);
			}
		}
		if (PlayerControl.GetGameMode() == CONST.CFG.PRORIV_MODE)
		{
			for (int j = 0; j < 4; j++)
			{
				if (!(this.map == null) && this.map.flags[j].inited)
				{
					int num = 1;
					if (this.map.flags[j].timer[0] > this.map.flags[j].timer[1])
					{
						num = 0;
					}
					if (this.oldFlagScores[j] != this.map.flags[j].timer[num])
					{
						this.mig[j] = true;
					}
					if (this.mig[j])
					{
						if (this.oldFlagScores[j] != this.map.flags[j].timer[num])
						{
							this._col[j] = new Color(this._col[j].r, this._col[j].g, this._col[j].b, this._col[j].a - 0.02f);
						}
						else
						{
							this._col[j] = new Color(this._col[j].r, this._col[j].g, this._col[j].b, this._col[j].a + 0.02f);
						}
						if (this._col[j].a <= 0.5f)
						{
							this.oldFlagScores[j] = this.map.flags[j].timer[num];
						}
						if (this._col[j].a >= 1f)
						{
							this._col[j].a = 1f;
							this.mig[j] = false;
						}
					}
					GUI.color = this._col[j];
					this.DrawFlag(num, (float)(this.map.flags[j].pos.x + 6), (float)(this.map.flags[j].pos.z + 16));
					GUI.color = Color.white;
				}
			}
		}
		Vector3 position = this.LocalPlayer.transform.position;
		Vector3 eulerAngles = this.LocalPlayer.transform.eulerAngles;
		this.DP(position.x, position.z, eulerAngles.y, true, false);
		if (PlayerControl.GetGameMode() == CONST.CFG.BUILD_MODE)
		{
			GUI.Label(new Rect(GUIManager.XRES(4f), GUIManager.YRES(132f), 200f, 20f), string.Concat(new string[]
			{
				"X: ",
				position.x.ToString("0"),
				" Y: ",
				position.y.ToString("0"),
				" Z: ",
				position.z.ToString("0")
			}));
		}
	}

	// Token: 0x060003BE RID: 958 RVA: 0x00049B80 File Offset: 0x00047D80
	private void DP(float px, float pz, float ry, bool self, bool zombie)
	{
		if (self)
		{
			GUI.color = Color.yellow;
		}
		else if (zombie)
		{
			GUI.color = Color.red;
		}
		else
		{
			GUI.color = Color.white;
		}
		Rect rect;
		rect..ctor(0f, 0f, GUIManager.YRES(16f), GUIManager.YRES(16f));
		rect.center = new Vector2(GUIManager.XRES(4f) + GUIManager.YRES(px / 2f), GUIManager.YRES(4f) + GUIManager.YRES(128f - pz / 2f));
		float num = ry - 90f;
		if (num > 360f)
		{
			num -= 360f;
		}
		if (num < 0f)
		{
			num += 360f;
		}
		Matrix4x4 matrix = GUI.matrix;
		GUIUtility.RotateAroundPivot(num, rect.center);
		if (zombie)
		{
			GUI.DrawTexture(rect, this.ZombiePosition);
		}
		else
		{
			GUI.DrawTexture(rect, this.PlayerPosition);
		}
		GUI.matrix = matrix;
	}

	// Token: 0x060003BF RID: 959 RVA: 0x00049C7C File Offset: 0x00047E7C
	public void UpdateRadarColor(byte[,] data)
	{
		if (this.RadarTexture2 == null)
		{
			this.RadarTexture2 = new Texture2D(256, 256);
		}
		this.team_dot[0] = 0;
		this.team_dot[1] = 0;
		this.team_dot[2] = 0;
		this.team_dot[3] = 0;
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				Color color;
				color..ctor(1f, 1f, 1f, 0.75f);
				if (data[i, j] == 255)
				{
					color..ctor(0f, 0f, 0f, 0f);
				}
				else if (data[i, j] == 0)
				{
					color..ctor(0f, 0f, 1f, 0.75f);
					this.team_dot[0]++;
				}
				else if (data[i, j] == 1)
				{
					color..ctor(1f, 0f, 0f, 0.75f);
					this.team_dot[1]++;
				}
				else if (data[i, j] == 2)
				{
					color..ctor(0f, 1f, 0f, 0.75f);
					this.team_dot[2]++;
				}
				else if (data[i, j] == 3)
				{
					color..ctor(1f, 1f, 0f, 0.75f);
					this.team_dot[3]++;
				}
				for (int k = i * 64; k < (i + 1) * 64; k++)
				{
					for (int l = j * 64; l < (j + 1) * 64; l++)
					{
						if (k == 0 || k == 63 || k == 127 || k == 191 || k == 255)
						{
							this.RadarTexture2.SetPixel(k, l, new Color(0f, 0f, 0f, 0f));
						}
						else if (l == 0 || l == 63 || l == 127 || l == 191 || l == 255)
						{
							this.RadarTexture2.SetPixel(k, l, new Color(0f, 0f, 0f, 0f));
						}
						else
						{
							this.RadarTexture2.SetPixel(k, l, color);
						}
					}
				}
			}
		}
		this.RadarTexture2.Apply();
		this.team_dot_all = this.team_dot[0] + this.team_dot[1] + this.team_dot[2] + this.team_dot[3];
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x00049F2E File Offset: 0x0004812E
	public void ForceUpdateRadar()
	{
		base.StartCoroutine(this.UpdateRadar());
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x00049F3D File Offset: 0x0004813D
	private IEnumerator UpdateRadar()
	{
		for (int i = 0; i < 256; i++)
		{
			for (int j = 0; j < 256; j++)
			{
				for (int k = 63; k > 0; k--)
				{
					BlockData block = this.map.GetBlock(i, k, j);
					if (!block.IsEmpty() && block.block.GetName() != null)
					{
						Color color = this.GetColor(block.block);
						this.RadarTexture.SetPixel(i, j, color);
						break;
					}
				}
			}
		}
		yield return null;
		this.RadarTexture.Apply();
		yield break;
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x00049F4C File Offset: 0x0004814C
	public Color GetColor(Block block)
	{
		for (int i = 0; i < this.colorlist.Count; i++)
		{
			if (this.colorlist[i].name == block.GetName())
			{
				return this.colorlist[i].color;
			}
		}
		Rect topFace = block.GetTopFace();
		if (Config.Tileset > 2)
		{
			this.tmpTex.SetPixels(this.blockTex.GetPixels((int)(topFace.x * 1024f), (int)(topFace.y * 1024f), 64, 64));
		}
		else
		{
			this.tmpTex.SetPixels(this.blockTex.GetPixels((int)(topFace.x * 512f), (int)(topFace.y * 512f), 32, 32));
		}
		this.tmpTex.filterMode = 0;
		this.tmpTex.Apply();
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		foreach (Color color in this.tmpTex.GetPixels())
		{
			num += color.r;
			num2 += color.g;
			num3 += color.b;
		}
		this.tmpColor = new Color(num / 1024f, num2 / 1024f, num3 / 1024f, 1f);
		this.colorlist.Add(new Radar.CBlockColor(block.GetName(), this.tmpColor));
		return this.tmpColor;
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x0004A0DC File Offset: 0x000482DC
	private void DrawBase(float px, float pz)
	{
		Rect rect;
		rect..ctor(0f, 0f, GUIManager.YRES(20f), GUIManager.YRES(20f));
		rect.center = new Vector2(GUIManager.XRES(4f) + GUIManager.YRES(px / 2f), GUIManager.YRES(4f) + GUIManager.YRES(128f - pz / 2f));
		GUI.DrawTexture(rect, this.BasePosition);
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x0004A15C File Offset: 0x0004835C
	private void DrawTeam(float px, float pz)
	{
		Rect rect;
		rect..ctor(0f, 0f, GUIManager.YRES(8f), GUIManager.YRES(8f));
		rect.center = new Vector2(GUIManager.XRES(4f) + GUIManager.YRES(px / 2f), GUIManager.YRES(4f) + GUIManager.YRES(128f - pz / 2f));
		GUI.DrawTexture(rect, this.TeamPosition);
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x0004A1DC File Offset: 0x000483DC
	private void DrawFlag(int _team, float px, float pz)
	{
		Rect rect;
		rect..ctor(0f, 0f, GUIManager.YRES(12f), GUIManager.YRES(12f));
		rect.center = new Vector2(GUIManager.XRES(6f) + GUIManager.YRES(px / 2f), GUIManager.YRES(6f) + GUIManager.YRES(128f - pz / 2f));
		GUI.DrawTexture(rect, this.FlagPosition[_team]);
	}

	// Token: 0x060003C6 RID: 966 RVA: 0x000432E8 File Offset: 0x000414E8
	public static float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
	{
		return Mathf.Atan2(Vector3.Dot(n, Vector3.Cross(v1, v2)), Vector3.Dot(v1, v2)) * 57.29578f;
	}

	// Token: 0x060003C7 RID: 967 RVA: 0x0004A25C File Offset: 0x0004845C
	public void GenerateSideTexture()
	{
		if (this.RadarSideTexture == null)
		{
			this.RadarSideTexture = new Texture2D(256, 64);
			this.RadarSideTexture.filterMode = 0;
		}
		Color color;
		color..ctor(0f, 0.5f, 0f, 0.5f);
		for (int i = 0; i < 256; i++)
		{
			for (int j = 0; j < 64; j++)
			{
				this.RadarSideTexture.SetPixel(i, j, color);
			}
		}
		for (int k = 0; k < 256; k++)
		{
			for (int l = 0; l < 64; l++)
			{
				for (int m = 0; m < 256; m++)
				{
					BlockData block = this.map.GetBlock(k, l, m);
					if (!block.IsEmpty() && block.block.GetName() != null)
					{
						color..ctor(0f, 0f, 0f, 0f);
						if (block.block.GetName() == "Stoneend")
						{
							color..ctor(0f, 0f, 0f, 1f);
						}
						else if (block.block.GetName() == "Dirt")
						{
							color..ctor(0.5f, 0.25f, 0f, 1f);
						}
						else if (block.block.GetName() == "Grass")
						{
							color..ctor(0f, 0.5f, 0f, 1f);
						}
						else if (block.block.GetName() == "Snow")
						{
							color..ctor(1f, 1f, 1f, 1f);
						}
						else if (block.block.GetName() == "Sand")
						{
							color..ctor(1f, 0.75f, 0.5f, 1f);
						}
						else if (block.block.GetName() == "Stone")
						{
							color..ctor(0.5f, 0.5f, 0.5f, 1f);
						}
						else if (block.block.GetName() == "!Water")
						{
							color..ctor(0f, 0.5f, 1f, 1f);
						}
						else if (block.block.GetName() == "Wood")
						{
							color..ctor(0.75f, 0.4f, 0.1f, 1f);
						}
						else if (block.block.GetName() == "Wood2")
						{
							color..ctor(0.5f, 0.3f, 0.1f, 1f);
						}
						else if (block.block.GetName() == "Leaf")
						{
							color..ctor(0f, 0.8f, 0f, 1f);
						}
						else if (block.block.GetName() == "Brick")
						{
							color..ctor(1f, 0.2f, 0.2f, 1f);
						}
						else if (block.block.GetName() == "Brick_blue")
						{
							color..ctor(0f, 0f, 0.5f, 1f);
						}
						else if (block.block.GetName() == "Brick_red")
						{
							color..ctor(0.5f, 0f, 0f, 1f);
						}
						else if (block.block.GetName() == "Brick_green")
						{
							color..ctor(0f, 0.5f, 0f, 1f);
						}
						else if (block.block.GetName() == "Brick_yellow")
						{
							color..ctor(0.5f, 0.5f, 0f, 1f);
						}
						else if (block.block.GetName() == "Window")
						{
							color..ctor(0.13f, 0.66f, 0.86f, 1f);
						}
						else if (block.block.GetName() == "Box")
						{
							color..ctor(0.6f, 0.46f, 0.6f, 1f);
						}
						else if (block.block.GetName() == "Brick2")
						{
							color..ctor(0.46f, 0.46f, 0.46f, 1f);
						}
						else if (block.block.GetName() == "Stone2")
						{
							color..ctor(0.46f, 0.53f, 0.46f, 1f);
						}
						else if (block.block.GetName() == "Stone3")
						{
							color..ctor(0.53f, 0.53f, 0.4f, 1f);
						}
						else if (block.block.GetName() == "Stone4")
						{
							color..ctor(0.46f, 0.2f, 0.06f, 1f);
						}
						else if (block.block.GetName() == "Tile")
						{
							color..ctor(0.6f, 0.33f, 0.13f, 1f);
						}
						else if (block.block.GetName() == "Stone5")
						{
							color..ctor(0.8f, 0.53f, 0.2f, 1f);
						}
						else if (block.block.GetName() == "Sand2")
						{
							color..ctor(1f, 0.73f, 0.46f, 1f);
						}
						else if (block.block.GetName() == "Stone6")
						{
							color..ctor(0.53f, 0.46f, 0.46f, 1f);
						}
						else if (block.block.GetName() == "Metall1")
						{
							color..ctor(0.8f, 0.4f, 0.33f, 1f);
						}
						else if (block.block.GetName() == "Metall2")
						{
							color..ctor(0.53f, 0.66f, 0.6f, 1f);
						}
						else if (block.block.GetName() == "Stone7")
						{
							color..ctor(0.8f, 0.8f, 0.8f, 1f);
						}
						else if (block.block.GetName() == "Stone8")
						{
							color..ctor(0.8f, 0.8f, 0.8f, 1f);
						}
						else if (block.block.GetName() == "R_b_blue")
						{
							color..ctor(0f, 0f, 0f, 1f);
						}
						else if (block.block.GetName() == "R_b_red")
						{
							color..ctor(0f, 0f, 0f, 1f);
						}
						else if (block.block.GetName() == "R_b_green")
						{
							color..ctor(0f, 0f, 0f, 1f);
						}
						else if (block.block.GetName() == "R_b_yellow")
						{
							color..ctor(0f, 0f, 0f, 1f);
						}
						else if (block.block.GetName() == "R_z")
						{
							color..ctor(0f, 0f, 0f, 1f);
						}
						else if (block.block.GetName() == "R_c_blue")
						{
							color..ctor(0f, 0f, 0f, 1f);
						}
						else if (block.block.GetName() == "R_c_red")
						{
							color..ctor(0f, 0f, 0f, 1f);
						}
						else if (block.block.GetName() == "R_center")
						{
							color..ctor(0f, 0f, 0f, 1f);
						}
						this.RadarSideTexture.SetPixel(k, l, color);
						break;
					}
				}
			}
		}
		this.RadarSideTexture.Apply();
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x0004AB98 File Offset: 0x00048D98
	private void UpdPlayer(int i)
	{
		if (!BotsController.Instance.Bots[this.cscl.myindex].zombie && BotsController.Instance.Bots[i].Team != BotsController.Instance.Bots[this.cscl.myindex].Team)
		{
			return;
		}
		Vector3 position = BotsController.Instance.BotsGmObj[i].transform.position;
		Vector3 eulerAngles = BotsController.Instance.BotsGmObj[i].transform.eulerAngles;
		if (BotsController.Instance.Bots[this.cscl.myindex].zombie && !BotsController.Instance.Bots[i].zombie)
		{
			this.DP(position.x, position.z, eulerAngles.y, false, true);
			return;
		}
		this.DP(position.x, position.z, eulerAngles.y, false, false);
		this.DTPNTmp(position, BotsController.Instance.Bots[i].Name);
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x0004AC9C File Offset: 0x00048E9C
	private void DTPNTmp(Vector3 p, string name)
	{
		p.y += 2.5f;
		Vector3 vector = Camera.main.WorldToScreenPoint(p);
		vector.y = (float)Screen.height - vector.y;
		if (Vector3.Angle(p - this.LocalPlayer.transform.position, this.LocalPlayer.transform.forward) < 90f && vector.z > 0f)
		{
			GUI.Label(new Rect(vector.x - 100f, vector.y, 200f, 20f), name, this.gui_style);
		}
	}

	// Token: 0x040007E5 RID: 2021
	public bool canupdate = true;

	// Token: 0x040007E6 RID: 2022
	public Texture2D RadarTexture;

	// Token: 0x040007E7 RID: 2023
	private Texture2D RadarTexture2;

	// Token: 0x040007E8 RID: 2024
	private Texture2D ZombiePosition;

	// Token: 0x040007E9 RID: 2025
	private Texture2D PlayerPosition;

	// Token: 0x040007EA RID: 2026
	private Texture2D TeamPosition;

	// Token: 0x040007EB RID: 2027
	private Texture2D BasePosition;

	// Token: 0x040007EC RID: 2028
	public Texture2D[] FlagPosition;

	// Token: 0x040007ED RID: 2029
	public Texture2D RadarSideTexture;

	// Token: 0x040007EE RID: 2030
	private GUIStyle gui_style;

	// Token: 0x040007EF RID: 2031
	private Map map;

	// Token: 0x040007F0 RID: 2032
	private Client cscl;

	// Token: 0x040007F1 RID: 2033
	private PlayerControl cspc;

	// Token: 0x040007F2 RID: 2034
	private GameObject LocalPlayer;

	// Token: 0x040007F3 RID: 2035
	public Vector3[] team_pos = new Vector3[4];

	// Token: 0x040007F4 RID: 2036
	public Vector3 base_pos;

	// Token: 0x040007F5 RID: 2037
	public int[] team_dot = new int[4];

	// Token: 0x040007F6 RID: 2038
	public int team_dot_all;

	// Token: 0x040007F7 RID: 2039
	private List<Radar.CBlockColor> colorlist = new List<Radar.CBlockColor>();

	// Token: 0x040007F8 RID: 2040
	private Texture2D tmpTex;

	// Token: 0x040007F9 RID: 2041
	private BlockSet blockset;

	// Token: 0x040007FA RID: 2042
	private Texture2D blockTex;

	// Token: 0x040007FB RID: 2043
	private Color tmpColor;

	// Token: 0x040007FC RID: 2044
	private Color[] _col = new Color[4];

	// Token: 0x040007FD RID: 2045
	private int[] oldFlagScores = new int[4];

	// Token: 0x040007FE RID: 2046
	private bool[] mig = new bool[4];

	// Token: 0x02000855 RID: 2133
	public class CBlockColor
	{
		// Token: 0x06004B5D RID: 19293 RVA: 0x001AB368 File Offset: 0x001A9568
		public CBlockColor(string name, Color color)
		{
			this.name = name;
			this.color = color;
		}

		// Token: 0x06004B5E RID: 19294 RVA: 0x001AB380 File Offset: 0x001A9580
		~CBlockColor()
		{
		}

		// Token: 0x040031C0 RID: 12736
		public string name;

		// Token: 0x040031C1 RID: 12737
		public Color color;
	}
}
