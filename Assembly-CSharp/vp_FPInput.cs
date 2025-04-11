using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000CF RID: 207
public class vp_FPInput : MonoBehaviour
{
	// Token: 0x1700003B RID: 59
	// (get) Token: 0x0600074C RID: 1868 RVA: 0x000746ED File Offset: 0x000728ED
	// (set) Token: 0x0600074D RID: 1869 RVA: 0x000746F5 File Offset: 0x000728F5
	public bool AllowGameplayInput
	{
		get
		{
			return this.m_AllowGameplayInput;
		}
		set
		{
			this.m_AllowGameplayInput = value;
		}
	}

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x0600074E RID: 1870 RVA: 0x000746FE File Offset: 0x000728FE
	public Vector2 MousePos
	{
		get
		{
			return this.m_MousePos;
		}
	}

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x0600074F RID: 1871 RVA: 0x00074706 File Offset: 0x00072906
	// (set) Token: 0x06000750 RID: 1872 RVA: 0x00074713 File Offset: 0x00072913
	public int MouseSmoothSteps
	{
		get
		{
			return this.m_FPCamera.MouseSmoothSteps;
		}
		set
		{
			this.m_FPCamera.MouseSmoothSteps = (int)Mathf.Clamp((float)value, 1f, 10f);
		}
	}

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x06000751 RID: 1873 RVA: 0x00074732 File Offset: 0x00072932
	// (set) Token: 0x06000752 RID: 1874 RVA: 0x0007473F File Offset: 0x0007293F
	public float MouseSmoothWeight
	{
		get
		{
			return this.m_FPCamera.MouseSmoothWeight;
		}
		set
		{
			this.m_FPCamera.MouseSmoothWeight = Mathf.Clamp01(value);
		}
	}

	// Token: 0x1700003F RID: 63
	// (get) Token: 0x06000753 RID: 1875 RVA: 0x00074752 File Offset: 0x00072952
	// (set) Token: 0x06000754 RID: 1876 RVA: 0x0007475F File Offset: 0x0007295F
	public bool MouseAcceleration
	{
		get
		{
			return this.m_FPCamera.MouseAcceleration;
		}
		set
		{
			this.m_FPCamera.MouseAcceleration = value;
		}
	}

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x06000755 RID: 1877 RVA: 0x0007476D File Offset: 0x0007296D
	// (set) Token: 0x06000756 RID: 1878 RVA: 0x0007477A File Offset: 0x0007297A
	public float MouseAccelerationThreshold
	{
		get
		{
			return this.m_FPCamera.MouseAccelerationThreshold;
		}
		set
		{
			this.m_FPCamera.MouseAccelerationThreshold = value;
		}
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x00074788 File Offset: 0x00072988
	private void Start()
	{
		this.WeaponName[0] = "0weapon_block";
		this.WeaponName[33] = "1weapon_shovel";
		this.WeaponName[43] = "2weapon_m3";
		this.WeaponName[44] = "3weapon_m14";
		this.WeaponName[45] = "4weapon_mp5";
		this.WeaponName[2] = "5weapon_ak47";
		this.WeaponName[3] = "6weapon_svd";
		this.WeaponName[7] = "7weapon_m61";
		this.WeaponName[35] = "8weapon_zombie";
		this.WeaponName[46] = "9weapon_glock";
		this.WeaponName[9] = "10weapon_deagle";
		this.WeaponName[10] = "11weapon_shmel";
		this.WeaponName[12] = "12weapon_asval";
		this.WeaponName[13] = "13weapon_g36c";
		this.WeaponName[14] = "14weapon_kriss";
		this.WeaponName[15] = "15weapon_m4a1";
		this.WeaponName[19] = "16weapon_vsk94";
		this.WeaponName[16] = "17weapon_m249";
		this.WeaponName[17] = "18weapon_sps12";
		this.WeaponName[18] = "19weapon_vintorez";
		this.WeaponName[36] = "20weapon_medkit_w";
		this.WeaponName[37] = "21weapon_medkit_g";
		this.WeaponName[38] = "22weapon_medkit_o";
		this.WeaponName[40] = "23weapon_usp";
		this.WeaponName[47] = "24weapon_barrett";
		this.WeaponName[48] = "25weapon_tmp";
		this.WeaponName[50] = "26weapon_axe";
		this.WeaponName[51] = "27weapon_bat";
		this.WeaponName[52] = "28weapon_crowbar";
		this.WeaponName[49] = "29weapon_knife";
		this.WeaponName[53] = "30weapon_caramel";
		this.WeaponName[55] = "31weapon_tnt";
		this.WeaponName[62] = "32weapon_minefly";
		this.WeaponName[60] = "33weapon_auga3";
		this.WeaponName[61] = "34weapon_sg552";
		this.WeaponName[68] = "35weapon_m14ebr";
		this.WeaponName[69] = "36weapon_l96a1";
		this.WeaponName[70] = "37weapon_nova";
		this.WeaponName[71] = "38weapon_kord";
		this.WeaponName[72] = "39weapon_anaconda";
		this.WeaponName[73] = "40weapon_scar";
		this.WeaponName[74] = "41weapon_p90";
		this.WeaponName[78] = "42weapon_rpk";
		this.WeaponName[79] = "43weapon_hk416";
		this.WeaponName[80] = "44weapon_ak102";
		this.WeaponName[81] = "45weapon_sr25";
		this.WeaponName[82] = "46weapon_mk1";
		this.WeaponName[89] = "48weapon_mosin";
		this.WeaponName[90] = "49weapon_ppsh";
		this.WeaponName[91] = "50weapon_mp40";
		this.WeaponName[34] = "51weapon_l96a1mod";
		this.WeaponName[93] = "52weapon_kacpdw";
		this.WeaponName[94] = "53weapon_famas";
		this.WeaponName[95] = "54weapon_beretta";
		this.WeaponName[96] = "55weapon_machete";
		this.WeaponName[33] = "1weapon_shovel";
		this.WeaponName[100] = "57weapon_rpg7";
		this.WeaponName[101] = "58weapon_repair_tool";
		this.WeaponName[102] = "59weapon_aa12";
		this.WeaponName[103] = "60weapon_fn57";
		this.WeaponName[104] = "61weapon_fs2000";
		this.WeaponName[105] = "62weapon_l85";
		this.WeaponName[106] = "63weapon_mac10";
		this.WeaponName[107] = "64weapon_pkp";
		this.WeaponName[108] = "65weapon_pm";
		this.WeaponName[109] = "66weapon_tar21";
		this.WeaponName[110] = "67weapon_ump45";
		this.WeaponName[111] = "68weapon_ntw20";
		this.WeaponName[112] = "69weapon_vintorez_desert";
		this.WeaponName[137] = "70weapon_minigun";
		this.WeaponName[138] = "71weapon_javelin";
		this.WeaponName[139] = "72weapon_zaa12";
		this.WeaponName[140] = "73weapon_zasval";
		this.WeaponName[141] = "74weapon_zfn57";
		this.WeaponName[142] = "75weapon_zkord";
		this.WeaponName[143] = "76weapon_zm249";
		this.WeaponName[144] = "77weapon_zminigun";
		this.WeaponName[145] = "78weapon_zsps12";
		this.WeaponName[169] = "79weapon_sg";
		this.WeaponName[168] = "80weapon_hg";
		this.WeaponName[170] = "81weapon_rkg3";
		this.WeaponName[157] = "82weapon_tube";
		this.WeaponName[158] = "83weapon_bulava";
		this.WeaponName[159] = "84weapon_katana";
		this.WeaponName[160] = "85weapon_mauzer";
		this.WeaponName[161] = "86weapon_crossbow";
		this.WeaponName[162] = "87weapon_qbz95";
		this.WeaponName[171] = "88weapon_mine";
		this.WeaponName[172] = "89weapon_c4";
		this.WeaponName[173] = "90weapon_chopper";
		this.WeaponName[174] = "91weapon_shield";
		this.WeaponName[175] = "92weapon_aksu";
		this.WeaponName[176] = "93weapon_m700";
		this.WeaponName[177] = "94weapon_stechkin";
		this.WeaponName[183] = "95weapon_at_mine";
		this.WeaponName[184] = "96weapon_molotov";
		this.WeaponName[185] = "97weapon_m202";
		this.WeaponName[186] = "98weapon_gg";
		this.WeaponName[188] = "100weapon_dpm";
		this.WeaponName[189] = "101weapon_m1924";
		this.WeaponName[190] = "102weapon_mg42";
		this.WeaponName[191] = "103weapon_stenmk2";
		this.WeaponName[192] = "104weapon_m1a1";
		this.WeaponName[193] = "105weapon_type99";
		this.WeaponName[207] = "107weapon_bizon";
		this.WeaponName[208] = "108weapon_groza";
		this.WeaponName[209] = "109weapon_jackhammer";
		this.WeaponName[210] = "110weapon_pila";
		this.WeaponName[218] = "111weapon_psg_1";
		this.WeaponName[219] = "112weapon_krytac";
		this.WeaponName[220] = "113weapon_mp5sd";
		this.WeaponName[221] = "114weapon_colts";
		this.WeaponName[301] = "115weapon_jackhammer_lady";
		this.WeaponName[302] = "116weapon_m700_lady";
		this.WeaponName[303] = "117weapon_mg42_lady";
		this.WeaponName[304] = "118weapon_shield_lady";
		this.WeaponName[305] = "119weapon_magnum_lady";
		this.WeaponName[308] = "120weapon_scorpion";
		this.WeaponName[309] = "121weapon_g36c_veteran";
		this.WeaponName[313] = "123weapon_fmg9";
		this.WeaponName[314] = "124weapon_saiga";
		this.WeaponName[315] = "125weapon_flamethrower";
		this.WeaponName[329] = "126weapon_ak47_snow";
		this.WeaponName[330] = "127weapon_p90_snow";
		this.WeaponName[331] = "128weapon_saiga_snow";
		this.WeaponName[81] = "129weapon_sr25_snow";
		this.WeaponName[333] = "130weapon_usp_snow";
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x00074F30 File Offset: 0x00073130
	private int GetWeaponID(string name)
	{
		for (int i = 0; i < 350; i++)
		{
			if (this.WeaponName[i] == name)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x00074F60 File Offset: 0x00073160
	protected virtual void Update()
	{
		this.InputMove();
		this.InputWalk();
		this.InputJump();
		this.InputCrouch();
		this.InputAttack();
		this.InputZoom();
		this.InputReload();
		this.InputSetWeapon();
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x00074F94 File Offset: 0x00073194
	protected virtual void InputMove()
	{
		if (Time.time < this.stopmove)
		{
			this.Player.InputMoveVector.Set(Vector2.zero);
			return;
		}
		this.Player.InputMoveVector.Set(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x00074FF7 File Offset: 0x000731F7
	protected virtual void InputWalk()
	{
		if (Input.GetKey(304))
		{
			this.Player.Walk.TryStart();
			return;
		}
		this.Player.Walk.TryStop();
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x00075028 File Offset: 0x00073228
	protected virtual void InputJump()
	{
		if (Input.GetButton("Jump"))
		{
			this.Player.Jump.TryStart();
			return;
		}
		this.Player.Jump.Stop(0f);
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x0007505D File Offset: 0x0007325D
	protected virtual void InputCrouch()
	{
		if (Input.GetKey(306))
		{
			this.Player.Crouch.TryStart();
			return;
		}
		this.Player.Crouch.TryStop();
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x00075090 File Offset: 0x00073290
	protected virtual void InputZoom()
	{
		if (PlayerControl.GetGameMode() == CONST.CFG.SNOWBALLS_MODE)
		{
			return;
		}
		if (this.MouseBlockZoom > Time.time)
		{
			return;
		}
		if (this.Player.Reload.Active)
		{
			return;
		}
		if (this.GetWeaponID(this.Player.CurrentWeaponName.Get()) == 315)
		{
			return;
		}
		if (Input.GetKeyDown(324))
		{
			if (this.swid == 172)
			{
				this.m_WeaponSystem.DetonateMyC4();
			}
			if (this.m_FPCamera.GetComponent<Camera>().fieldOfView > 64f)
			{
				this.Player.Zoom.TryStart();
				return;
			}
			this.Player.Zoom.TryStop();
		}
	}

	// Token: 0x0600075F RID: 1887 RVA: 0x0007514C File Offset: 0x0007334C
	protected virtual void InputAttack()
	{
		if (Cursor.lockState.Equals(0))
		{
			return;
		}
		if (Cursor.visible)
		{
			Cursor.visible = false;
		}
		if (this.Player.Reload.Active)
		{
			return;
		}
		if (Input.GetKey(323))
		{
			this.Player.Attack.TryStart();
			return;
		}
		this.Player.Attack.TryStop();
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x000751C4 File Offset: 0x000733C4
	private IEnumerator AutoReload()
	{
		yield return new WaitForSeconds(0.5f);
		this.Player.Attack.TryStop();
		this.Player.Reload.TryStart();
		yield break;
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x000751D4 File Offset: 0x000733D4
	protected virtual void InputReload()
	{
		if (PlayerControl.GetGameMode() == CONST.CFG.SNOWBALLS_MODE)
		{
			return;
		}
		if (Input.GetKeyDown(114) && this.m_WeaponSystem.WeaponCanReload(this.GetWeaponID(this.Player.CurrentWeaponName.Get())))
		{
			this.Player.Attack.TryStop();
			this.Player.Reload.TryStart();
		}
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x00075244 File Offset: 0x00073444
	protected virtual void InputSetWeapon()
	{
		if (this.zombie)
		{
			return;
		}
		if (PlayerControl.GetGameMode() == CONST.CFG.SNOWBALLS_MODE)
		{
			return;
		}
		if (this.HideWeapons)
		{
			return;
		}
		if (this.Player.Reload.Active)
		{
			return;
		}
		if (this.m_WeaponSystem == null)
		{
			this.m_WeaponSystem = (WeaponSystem)Object.FindObjectOfType(typeof(WeaponSystem));
		}
		bool flag = false;
		if (Input.GetKeyDown(49))
		{
			this.cwid = 0;
			this.swid = 0;
			this.last_swid = 0;
			flag = true;
			this.lastweapon2 = 0;
		}
		if (Input.GetKeyDown(50))
		{
			this.cwid = 1;
			this.swid = 0;
			this.last_swid = 0;
			flag = true;
			this.lastweapon2 = 0;
		}
		if (Input.GetKeyDown(51))
		{
			this.cwid = 2;
			this.swid = 0;
			this.last_swid = 0;
			flag = true;
			this.lastweapon2 = 0;
		}
		if (Input.GetKeyDown(52))
		{
			this.cwid = 3;
			this.swid = 0;
			this.last_swid = 0;
			flag = true;
			this.lastweapon2 = 0;
		}
		if (Input.GetKeyDown(113))
		{
			int num;
			if (this.last_swid == 0)
			{
				num = 0;
			}
			else
			{
				num = this.lastweapon2 + 1;
			}
			if (num > 2)
			{
				num = 0;
			}
			int num2 = num;
			while (this.m_WeaponSystem.GetAmmo(num) < 1 && this.m_WeaponSystem.GetAmmoWid(num) != 172)
			{
				num++;
				if (num > 2)
				{
					num = 0;
				}
				if (num == num2)
				{
					break;
				}
			}
			this.swid = (byte)this.m_WeaponSystem.GetAmmoWid(num);
			this.last_swid = (int)this.swid;
			this.lastweapon2 = num;
			flag = true;
		}
		if (Input.GetKeyDown(103) && this.m_WeaponSystem.GetGAmmo() > 0)
		{
			this.Player.Attack.TryStop();
			this.swid = (byte)this.m_WeaponSystem.g1wid;
			flag = true;
			this.lastweapon2 = 0;
		}
		if (Input.GetKeyDown(104) && this.m_WeaponSystem.GetHAmmo() > 0)
		{
			this.Player.Attack.TryStop();
			this.swid = (byte)this.m_WeaponSystem.g2wid;
			flag = true;
			this.lastweapon2 = 0;
		}
		if (Input.GetKeyDown(53))
		{
			if (this.m_WeaponSystem.GetAmmoMedkit_o() > 0)
			{
				this.swid = 38;
			}
			else if (this.m_WeaponSystem.GetAmmoMedkit_g() > 0)
			{
				this.swid = 37;
			}
			else if (this.m_WeaponSystem.GetAmmoMedkit_w() > 0)
			{
				this.swid = 36;
			}
			E_Menu e_Menu = (E_Menu)Object.FindObjectOfType(typeof(E_Menu));
			if (e_Menu)
			{
				e_Menu.SelectedItem[3][0] = (int)this.swid;
			}
			flag = true;
			this.lastweapon2 = 0;
		}
		if (!flag)
		{
			return;
		}
		if (this.swid > 0)
		{
			this.Player.SetWeaponByName.Try(this.WeaponName[(int)this.swid]);
		}
		else if (this.cwid > 0 && this.awid[(int)this.cwid] > 0)
		{
			this.Player.SetWeaponByName.Try(this.WeaponName[this.awid[(int)this.cwid]]);
		}
		else if (this.cwid == 0)
		{
			this.Player.SetWeaponByName.Try(this.WeaponName[0]);
		}
		base.GetComponent<Sound>().PlaySound_Stop(GameObject.Find("Player/MainCamera/TMPAudio").GetComponent<AudioSource>());
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x00075594 File Offset: 0x00073794
	public void RestoreSetWeapon()
	{
		this.swid = 0;
		if (this.cwid > 0 && this.awid[(int)this.cwid] > 0)
		{
			this.Player.SetWeaponByName.Try(this.WeaponName[this.awid[(int)this.cwid]]);
			return;
		}
		if (this.cwid == 0)
		{
			this.Player.SetWeaponByName.Try(this.WeaponName[0]);
		}
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x00075614 File Offset: 0x00073814
	public void SetPrimaryWeapon(bool build_mode_and_block = false)
	{
		this.zombie = false;
		if (this.awid[2] > 0)
		{
			this.cwid = 2;
		}
		else if (this.awid[3] > 0)
		{
			this.cwid = 3;
		}
		else if (this.awid[1] > 0)
		{
			this.cwid = 1;
		}
		else
		{
			this.cwid = 0;
		}
		if (build_mode_and_block)
		{
			this.cwid = 0;
		}
		this.swid = 0;
		this.Player.SetWeaponByName.Try(this.WeaponName[this.awid[(int)this.cwid]]);
		this.HideWeapons = false;
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x000756AD File Offset: 0x000738AD
	public void SetZombieWeapon()
	{
		this.zombie = true;
		this.Player.SetWeaponByName.Try(this.WeaponName[35]);
		base.StartCoroutine(this.cr_SetZombieWeapon());
	}

	// Token: 0x06000766 RID: 1894 RVA: 0x000756E2 File Offset: 0x000738E2
	private IEnumerator cr_SetZombieWeapon()
	{
		yield return new WaitForSeconds(1f);
		if (this.zombie && this.Player.CurrentWeaponName.Get()[0] != '8')
		{
			this.SetZombieWeapon();
			base.StartCoroutine(this.cr_SetZombieWeapon());
		}
		yield break;
	}

	// Token: 0x06000767 RID: 1895 RVA: 0x00002B75 File Offset: 0x00000D75
	protected virtual void UpdatePause()
	{
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x00002B75 File Offset: 0x00000D75
	protected virtual void UpdateCursorLock()
	{
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x000756F1 File Offset: 0x000738F1
	protected virtual void Awake()
	{
		this.Player = (vp_FPPlayerEventHandler)base.transform.root.GetComponentInChildren(typeof(vp_FPPlayerEventHandler));
		this.m_FPCamera = base.GetComponentInChildren<vp_FPCamera>();
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x00075724 File Offset: 0x00073924
	protected virtual void OnEnable()
	{
		if (this.Player != null)
		{
			this.Player.Register(this);
		}
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x00075740 File Offset: 0x00073940
	protected virtual void OnDisable()
	{
		if (this.Player != null)
		{
			this.Player.Unregister(this);
		}
	}

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x0600076C RID: 1900 RVA: 0x000746ED File Offset: 0x000728ED
	// (set) Token: 0x0600076D RID: 1901 RVA: 0x000746F5 File Offset: 0x000728F5
	protected virtual bool OnValue_AllowGameplayInput
	{
		get
		{
			return this.m_AllowGameplayInput;
		}
		set
		{
			this.m_AllowGameplayInput = value;
		}
	}

	// Token: 0x17000042 RID: 66
	// (get) Token: 0x0600076E RID: 1902 RVA: 0x0007575C File Offset: 0x0007395C
	// (set) Token: 0x0600076F RID: 1903 RVA: 0x00075763 File Offset: 0x00073963
	protected virtual bool OnValue_Pause
	{
		get
		{
			return vp_TimeUtility.Paused;
		}
		set
		{
			vp_TimeUtility.Paused = value;
		}
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x0007576B File Offset: 0x0007396B
	public void SetHideWeapons(bool val)
	{
		this.HideWeapons = val;
		if (this.HideWeapons)
		{
			this.Player.SetWeapon.TryStart<int>(0);
		}
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x0007578E File Offset: 0x0007398E
	public void SetActiveWeapons(int mwid, int pwid, int swid)
	{
		this.awid[0] = 0;
		this.awid[1] = mwid;
		this.awid[2] = pwid;
		this.awid[3] = swid;
		if (pwid > 0)
		{
			this.cwid = 2;
		}
	}

	// Token: 0x04000C83 RID: 3203
	public vp_FPPlayerEventHandler Player;

	// Token: 0x04000C84 RID: 3204
	protected vp_FPCamera m_FPCamera;

	// Token: 0x04000C85 RID: 3205
	protected WeaponSystem m_WeaponSystem;

	// Token: 0x04000C86 RID: 3206
	public Rect[] MouseCursorZones;

	// Token: 0x04000C87 RID: 3207
	protected Vector2 m_MousePos = Vector2.zero;

	// Token: 0x04000C88 RID: 3208
	public float MouseBlockZoom;

	// Token: 0x04000C89 RID: 3209
	public int[] awid = new int[4];

	// Token: 0x04000C8A RID: 3210
	public byte cwid;

	// Token: 0x04000C8B RID: 3211
	public byte swid;

	// Token: 0x04000C8C RID: 3212
	public bool zombie;

	// Token: 0x04000C8D RID: 3213
	public byte lastcwid;

	// Token: 0x04000C8E RID: 3214
	public float stopmove;

	// Token: 0x04000C8F RID: 3215
	protected bool m_AllowGameplayInput = true;

	// Token: 0x04000C90 RID: 3216
	private bool HideWeapons;

	// Token: 0x04000C91 RID: 3217
	private string[] WeaponName = new string[350];

	// Token: 0x04000C92 RID: 3218
	public int lastweapon2;

	// Token: 0x04000C93 RID: 3219
	public int last_swid;
}
