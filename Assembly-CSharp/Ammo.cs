using System;
using UnityEngine;

// Token: 0x02000077 RID: 119
public class Ammo : MonoBehaviour
{
	// Token: 0x06000356 RID: 854 RVA: 0x0003C894 File Offset: 0x0003AA94
	private void Awake()
	{
		this.ammo_block = ContentLoader.LoadTexture("ammo_block");
		this.ammo_shotgun = ContentLoader.LoadTexture("ammo_m3");
		this.ammo_machinegun = ContentLoader.LoadTexture("ammo_mp5");
		this.ammo_rifle = ContentLoader.LoadTexture("ammo_m14");
		this.ammo_m61 = ContentLoader.LoadTexture("ammo_m61");
		this.ammo_shmel = ContentLoader.LoadTexture("ammo_shmel");
		this.ammo_medkit = ContentLoader.LoadTexture("ammo_medkit");
		this.ammo_tnt = ContentLoader.LoadTexture("ammo_tnt");
		this.ammo_gp = ContentLoader.LoadTexture("ammo_gp");
		this.ammo_rpg = ContentLoader.LoadTexture("ammo_shmel");
		this.ammo_zbk18m = ContentLoader.LoadTexture("ammo_shmel");
		this.ammo_zof26 = ContentLoader.LoadTexture("ammo_shmel");
		this.ammo_snaryad = ContentLoader.LoadTexture("ammo_shmel");
		this.ammo_repair_kit = ContentLoader.LoadTexture("ammo_shmel");
		this.ammo_arrows = ContentLoader.LoadTexture("ammo_arrows");
		this.ammo_flamethrower = ContentLoader.LoadTexture("fire");
		this.ammo_gp_hud = ContentLoader.LoadTexture("ammo_gp_hud");
		this.ammo_javelin_hud = ContentLoader.LoadTexture("ammo_javelin_hud");
		this.ammo_minefly_hud = ContentLoader.LoadTexture("ammo_minefly_hud");
		this.ammo_rpg_hud = ContentLoader.LoadTexture("ammo_rpg_hud");
		this.ammo_shmel_hud = ContentLoader.LoadTexture("ammo_shmel_hud");
		this.ammo_snowball_hud = ContentLoader.LoadTexture("ammo_snowball_hud");
		this.gui_style = new GUIStyle();
		this.gui_style.font = FontManager.font[0];
		this.gui_style.alignment = 2;
		this.gui_style.normal.textColor = new Color(255f, 255f, 255f, 255f);
		this.OnResize();
	}

	// Token: 0x06000357 RID: 855 RVA: 0x0003CA5C File Offset: 0x0003AC5C
	private void OnResize()
	{
		this.r_ammo_gun = new Rect((float)(Screen.width - 40), (float)(Screen.height - 40), 32f, 32f);
		this.r_ammo_m61 = new Rect((float)(Screen.width - 40), (float)(Screen.height - 100), 32f, 32f);
		this.r_ammo_shmel = new Rect((float)(Screen.width - 40), (float)(Screen.height - 140), 32f, 32f);
		this.r_ammo_tnt = new Rect((float)(Screen.width - 40), (float)(Screen.height - 180), 32f, 32f);
		this.r_ammo_gp = new Rect((float)(Screen.width - 120), (float)(Screen.height - 40), 32f, 32f);
		this.r_ammo_rpg = new Rect((float)(Screen.width - 40), (float)(Screen.height - 140), 32f, 32f);
		this.r_ammo_zbk18m = new Rect((float)(Screen.width - 40), (float)(Screen.height - 140), 32f, 32f);
		this.r_ammo_zof26 = new Rect((float)(Screen.width - 40), (float)(Screen.height - 140), 32f, 32f);
		this.r_ammo_snaryad = new Rect((float)(Screen.width - 40), (float)(Screen.height - 140), 32f, 32f);
		this.r_ammo_repair_kit = new Rect((float)(Screen.width - 40), (float)(Screen.height - 140), 32f, 32f);
		this.r_ammo_snowball = new Rect((float)(Screen.width - 40), (float)(Screen.height - 40), 32f, 32f);
	}

	// Token: 0x06000358 RID: 856 RVA: 0x0003CC2C File Offset: 0x0003AE2C
	private void OnGUI()
	{
		if (!this.draw)
		{
			return;
		}
		if (!ItemsDB.CheckItem(this.weaponid) && this.weaponid != 0)
		{
			return;
		}
		if (this.g1count > 0 || this.g2count > 0)
		{
			GUI.DrawTexture(this.r_ammo_m61, this.ammo_m61);
		}
		if (this.a1count > 0 || this.a3count > 0)
		{
			GUI.DrawTexture(this.r_ammo_shmel, this.ammo_shmel);
		}
		if (this.a2count > 0)
		{
			GUI.DrawTexture(this.r_ammo_tnt, this.ammo_tnt);
		}
		if (this.weaponid == 0)
		{
			GUI.DrawTexture(this.r_ammo_gun, this.ammo_block);
		}
		else if (ItemsDB.Items[this.weaponid].Category == 5)
		{
			GUI.DrawTexture(this.r_ammo_gun, this.ammo_rifle);
		}
		else if (ItemsDB.Items[this.weaponid].Category == 6)
		{
			GUI.DrawTexture(this.r_ammo_gun, this.ammo_shotgun);
		}
		else if (ItemsDB.Items[this.weaponid].Category == 19)
		{
			GUI.DrawTexture(this.r_ammo_gun, this.ammo_medkit);
		}
		else if (this.weaponid == 10 || this.weaponid == 185)
		{
			GUI.DrawTexture(this.r_ammo_gun, this.ammo_shmel_hud);
		}
		else if (this.weaponid == 62)
		{
			GUI.DrawTexture(this.r_ammo_gun, this.ammo_minefly_hud);
		}
		else if (this.weaponid == 77)
		{
			GUI.DrawTexture(this.r_ammo_gun, this.ammo_gp_hud);
		}
		else if (this.weaponid == 100)
		{
			GUI.DrawTexture(this.r_ammo_gun, this.ammo_rpg_hud);
		}
		else if (this.weaponid == 138)
		{
			GUI.DrawTexture(this.r_ammo_gun, this.ammo_javelin_hud);
		}
		else if (this.weaponid == 161)
		{
			GUI.DrawTexture(this.r_ammo_gun, this.ammo_arrows);
		}
		else if (this.weaponid == 315)
		{
			GUI.DrawTexture(this.r_ammo_gun, this.ammo_flamethrower);
		}
		else
		{
			if (ItemsDB.Items[this.weaponid].Type != 1)
			{
				return;
			}
			GUI.DrawTexture(this.r_ammo_gun, this.ammo_machinegun);
		}
		int num;
		if (this.weaponid == 10 || this.weaponid == 62 || this.weaponid == 185)
		{
			num = this.a1count;
		}
		else if (this.weaponid == 100 || this.weaponid == 138)
		{
			num = this.a3count;
		}
		else
		{
			num = this.clip;
		}
		this.gui_style.fontSize = 40;
		this.gui_style.normal.textColor = GUIManager.c[9];
		GUI.Label(new Rect((float)(Screen.width - 42 + 2), (float)(Screen.height - 34 + 2), 0f, 0f), num.ToString(), this.gui_style);
		this.gui_style.normal.textColor = GUIManager.c[8];
		GUI.Label(new Rect((float)(Screen.width - 42), (float)(Screen.height - 34), 0f, 0f), num.ToString(), this.gui_style);
		if (this.weaponid == 0 || this.weaponid == 38 || this.weaponid == 37 || this.weaponid == 36 || this.weaponid == 10 || this.weaponid == 62 || this.weaponid == 100 || this.weaponid == 138 || this.weaponid == 185 || this.weaponid == 315)
		{
			return;
		}
		this.gui_style.fontSize = 20;
		this.gui_style.normal.textColor = GUIManager.c[9];
		GUI.Label(new Rect((float)(Screen.width - 10 + 2), (float)(Screen.height - 50 + 2), 0f, 0f), this.backpack.ToString(), this.gui_style);
		this.gui_style.normal.textColor = GUIManager.c[8];
		GUI.Label(new Rect((float)(Screen.width - 10), (float)(Screen.height - 50), 0f, 0f), this.backpack.ToString(), this.gui_style);
		if (this.weaponid == 79 || this.weaponid == 80 || this.weaponid == 208)
		{
			GUI.DrawTexture(this.r_ammo_gp, this.ammo_gp);
			this.gui_style.fontSize = 40;
			this.gui_style.normal.textColor = GUIManager.c[9];
			GUI.Label(new Rect((float)(Screen.width - 112 + 2), (float)(Screen.height - 34 + 2), 0f, 0f), this.gp.ToString(), this.gui_style);
			this.gui_style.normal.textColor = GUIManager.c[8];
			GUI.Label(new Rect((float)(Screen.width - 112), (float)(Screen.height - 34), 0f, 0f), this.gp.ToString(), this.gui_style);
		}
	}

	// Token: 0x06000359 RID: 857 RVA: 0x0003D178 File Offset: 0x0003B378
	public void SetWeapon(int _weaponid, int _clip, int _backpack)
	{
		this.weaponid = _weaponid;
		if (this.weaponid != 201 && this.weaponid != 202 && this.weaponid != 203 && this.weaponid != 204)
		{
			this.clip = _clip;
			this.backpack = _backpack;
			if (this.clip > 999 && this.weaponid != 315)
			{
				this.clip = 999;
			}
		}
	}

	// Token: 0x0600035A RID: 858 RVA: 0x0003D1F3 File Offset: 0x0003B3F3
	public void SetPrimaryAmmo(int _ammo_clip)
	{
		this.clip = _ammo_clip;
	}

	// Token: 0x0600035B RID: 859 RVA: 0x0003D1FC File Offset: 0x0003B3FC
	public void SetAmmo(int _g1, int _g2, int _a1, int _a2, int _a3, int _gp, int _zbk18m, int _zof26, int _snaryad)
	{
		this.g1count = _g1;
		this.g2count = _g2;
		this.a1count = _a1;
		this.a2count = _a2;
		this.a3count = _a3;
		this.gp = _gp;
		this.zbk18m = _zbk18m;
		this.zof26 = _zof26;
		this.snaryad = _snaryad;
	}

	// Token: 0x04000602 RID: 1538
	private Texture ammo_block;

	// Token: 0x04000603 RID: 1539
	private Texture ammo_shotgun;

	// Token: 0x04000604 RID: 1540
	private Texture ammo_machinegun;

	// Token: 0x04000605 RID: 1541
	private Texture ammo_rifle;

	// Token: 0x04000606 RID: 1542
	private Texture ammo_m61;

	// Token: 0x04000607 RID: 1543
	private Texture ammo_shmel;

	// Token: 0x04000608 RID: 1544
	private Texture ammo_medkit;

	// Token: 0x04000609 RID: 1545
	private Texture ammo_tnt;

	// Token: 0x0400060A RID: 1546
	private Texture ammo_gp;

	// Token: 0x0400060B RID: 1547
	private Texture ammo_rpg;

	// Token: 0x0400060C RID: 1548
	private Texture ammo_zbk18m;

	// Token: 0x0400060D RID: 1549
	private Texture ammo_zof26;

	// Token: 0x0400060E RID: 1550
	private Texture ammo_snaryad;

	// Token: 0x0400060F RID: 1551
	private Texture ammo_repair_kit;

	// Token: 0x04000610 RID: 1552
	private Texture ammo_arrows;

	// Token: 0x04000611 RID: 1553
	private Texture ammo_flamethrower;

	// Token: 0x04000612 RID: 1554
	private Texture ammo_gp_hud;

	// Token: 0x04000613 RID: 1555
	private Texture ammo_javelin_hud;

	// Token: 0x04000614 RID: 1556
	private Texture ammo_minefly_hud;

	// Token: 0x04000615 RID: 1557
	private Texture ammo_rpg_hud;

	// Token: 0x04000616 RID: 1558
	private Texture ammo_shmel_hud;

	// Token: 0x04000617 RID: 1559
	private Texture ammo_snowball_hud;

	// Token: 0x04000618 RID: 1560
	private Rect r_ammo_gun;

	// Token: 0x04000619 RID: 1561
	private Rect r_ammo_m61;

	// Token: 0x0400061A RID: 1562
	private Rect r_ammo_shmel;

	// Token: 0x0400061B RID: 1563
	private Rect r_ammo_tnt;

	// Token: 0x0400061C RID: 1564
	private Rect r_ammo_gp;

	// Token: 0x0400061D RID: 1565
	private Rect r_ammo_rpg;

	// Token: 0x0400061E RID: 1566
	private Rect r_ammo_zbk18m;

	// Token: 0x0400061F RID: 1567
	private Rect r_ammo_zof26;

	// Token: 0x04000620 RID: 1568
	private Rect r_ammo_snaryad;

	// Token: 0x04000621 RID: 1569
	private Rect r_ammo_repair_kit;

	// Token: 0x04000622 RID: 1570
	private Rect r_ammo_snowball;

	// Token: 0x04000623 RID: 1571
	private int weaponid;

	// Token: 0x04000624 RID: 1572
	private int clip;

	// Token: 0x04000625 RID: 1573
	private int backpack;

	// Token: 0x04000626 RID: 1574
	private int g1count;

	// Token: 0x04000627 RID: 1575
	private int g2count;

	// Token: 0x04000628 RID: 1576
	private int a1count;

	// Token: 0x04000629 RID: 1577
	private int a2count;

	// Token: 0x0400062A RID: 1578
	private int a3count;

	// Token: 0x0400062B RID: 1579
	private int gp;

	// Token: 0x0400062C RID: 1580
	private int zbk18m;

	// Token: 0x0400062D RID: 1581
	private int zof26;

	// Token: 0x0400062E RID: 1582
	private int snaryad;

	// Token: 0x0400062F RID: 1583
	private int repair_kit;

	// Token: 0x04000630 RID: 1584
	private GUIStyle gui_style;

	// Token: 0x04000631 RID: 1585
	public bool draw = true;

	// Token: 0x04000632 RID: 1586
	private bool initialized;
}
