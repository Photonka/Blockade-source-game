using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200009E RID: 158
public class WeaponSystem : MonoBehaviour
{
	// Token: 0x06000536 RID: 1334 RVA: 0x0006492C File Offset: 0x00062B2C
	private void Awake()
	{
		this.m_Input = base.transform.root.GetComponentInChildren<vp_FPInput>();
		this.m_FPCamera = base.transform.root.GetComponentInChildren<vp_FPCamera>();
		this.myDummy = base.transform.root.GetComponentInChildren<dummy>();
		this.cscr = (Crosshair)Object.FindObjectOfType(typeof(Crosshair));
		this.csmap = (Map)Object.FindObjectOfType(typeof(Map));
		this.cspm = (ParticleManager)Object.FindObjectOfType(typeof(ParticleManager));
		this.csig = (MainGUI)Object.FindObjectOfType(typeof(MainGUI));
		this.csam = (Ammo)Object.FindObjectOfType(typeof(Ammo));
		this.csrm = (RagDollManager)Object.FindObjectOfType(typeof(RagDollManager));
		this.cssw = (Switch)Object.FindObjectOfType(typeof(Switch));
		this.goBlockSetup = (GameObject)Object.Instantiate(Resources.Load("Prefab/Cursor"), new Vector3(-1000f, -1000f, -1000f), new Quaternion(0f, 0f, 0f, 0f));
		this.goBlockCrash = (GameObject)Object.Instantiate(Resources.Load("Prefab/CursorCrash"), new Vector3(-1000f, -1000f, -1000f), new Quaternion(0f, 0f, 0f, 0f));
		this.goBlockTNT = (GameObject)Object.Instantiate(Resources.Load("Prefab/CursorTNT"), new Vector3(-1000f, -1000f, -1000f), new Quaternion(0f, 0f, 0f, 0f));
		this.ammowid = new int[3];
		this.ammocount = new int[3];
		this.firstFireCount = 0;
		this.secondFireCount = 0;
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x00064B2C File Offset: 0x00062D2C
	public void StartEquip(int _mwid, int _pwid, int _swid, int _a1wid, int _a2wid, int _a3wid, int _g1wid, int _g2wid, int clipammo, int backpack, int clipammo2, int backpack2, int blockammo, int ammo1, int ammo2, int ammo3, int gren1, int gren2, int medkit_w, int medkit_g, int medkit_o, int zbk18m, int zof26, int snaryad, int repair_kit, int tank_light, int tank_medium, int tank_heavy, byte mg, int gp, int flash, int smoke)
	{
		this.mwid = _mwid;
		this.pwid = _pwid;
		this.swid = _swid;
		this.ammowid[0] = _a1wid;
		this.ammowid[1] = _a2wid;
		this.ammowid[2] = _a3wid;
		this.ammocount[0] = ammo1;
		this.ammocount[1] = ammo2;
		this.ammocount[2] = ammo3;
		this.g1wid = _g1wid;
		this.g2wid = _g2wid;
		this.gren1count = gren1;
		this.gren2count = gren2;
		this.m_Input.SetActiveWeapons(this.mwid, this.pwid, this.swid);
		this.ammo_clip = clipammo;
		this.ammo_fullclip = this.ammo_clip;
		this.ammo_backpack = backpack;
		this.ammo_clip2 = clipammo2;
		this.ammo_fullclip2 = this.ammo_clip2;
		this.ammo_backpack2 = backpack2;
		this.ammo_block = blockammo;
		this.ammo_medkit_w = medkit_w;
		this.ammo_medkit_g = medkit_g;
		this.ammo_medkit_o = medkit_o;
		E_Menu e_Menu = (E_Menu)Object.FindObjectOfType(typeof(E_Menu));
		if (e_Menu)
		{
			if (this.ammo_medkit_o > 0)
			{
				e_Menu.SelectedItem[3][0] = 38;
			}
			else if (this.ammo_medkit_g > 0)
			{
				e_Menu.SelectedItem[3][0] = 37;
			}
			else if (this.ammo_medkit_w > 0)
			{
				e_Menu.SelectedItem[3][0] = 36;
			}
		}
		((New_Slots)Object.FindObjectOfType(typeof(New_Slots))).Active[4] = true;
		this.ammo_gp = gp;
		this.ammo_clip_rockets = 1;
		this.ammo_zbk18m = zbk18m;
		this.ammo_zof26 = zof26;
		this.ammo_snaryad = snaryad;
		this.ammo_repair_kit = repair_kit;
		this.ammo_module_flash = flash;
		this.ammo_module_smoke = smoke;
		this.ammo_tank_light = tank_light;
		this.ammo_tank_heavy = tank_heavy;
		this.MGitem = mg;
		if (mg == 1)
		{
			this.MGammo = 200;
			this.MGammo_backpack = 100;
			this.MGammo_clip = 100;
		}
		this.JeepMGammo = 500;
		this.csam.SetAmmo(this.gren1count, this.gren2count, this.ammocount[0], this.ammocount[1], this.ammocount[2], this.ammo_gp, this.ammo_zbk18m, this.ammo_zof26, this.ammo_snaryad);
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x00064D70 File Offset: 0x00062F70
	public void FixedUpdate()
	{
		if (this.awid == 0)
		{
			Vector3i? cursor = this.GetCursor(false, 4);
			if (cursor == null)
			{
				this.goBlockSetup.transform.position = new Vector3(-1000f, -1000f, -1000f);
				return;
			}
			if (this.cscc == null)
			{
				this.cscc = base.gameObject.GetComponent<CharacterController>();
			}
			if (BlockCharacterCollision.GetContactBlockCharacter(cursor.Value, base.transform.position, this.cscc) != null)
			{
				this.goBlockSetup.transform.position = new Vector3(-1000f, -1000f, -1000f);
				return;
			}
			this.goBlockSetup.transform.position = cursor.Value;
			return;
		}
		else
		{
			if (this.awid != 33)
			{
				if (this.awid == 55)
				{
					Vector3i? cursor2 = this.GetCursor(false, 4);
					if (cursor2 != null)
					{
						if (this.cscc == null)
						{
							this.cscc = base.gameObject.GetComponent<CharacterController>();
						}
						if (BlockCharacterCollision.GetContactBlockCharacter(cursor2.Value, base.transform.position, this.cscc) != null)
						{
							this.goBlockTNT.transform.position = new Vector3(-1000f, -1000f, -1000f);
							return;
						}
						this.goBlockTNT.transform.position = cursor2.Value;
						return;
					}
					else
					{
						this.goBlockTNT.transform.position = new Vector3(-1000f, -1000f, -1000f);
					}
				}
				return;
			}
			Vector3i? cursor3 = this.GetCursor(true, 2);
			if (cursor3 != null)
			{
				this.goBlockCrash.transform.position = cursor3.Value;
				return;
			}
			this.goBlockCrash.transform.position = new Vector3(-1000f, -1000f, -1000f);
			return;
		}
	}

	// Token: 0x06000539 RID: 1337 RVA: 0x00064F88 File Offset: 0x00063188
	public void OnWeaponSelect(vp_FPWeapon weapon)
	{
		if (this.cscl == null)
		{
			this.cscl = Object.FindObjectOfType<Client>();
		}
		if (this.awid == 137 || this.awid == 144)
		{
			if (this.MG == null)
			{
				this.MG = (Minigun)Object.FindObjectOfType(typeof(Minigun));
			}
			if (this._S == null)
			{
				this._S = GameObject.Find("Player").GetComponent<Sound>();
			}
			if (this._AS == null)
			{
				this._AS = GameObject.Find("Player/MainCamera/TMPAudio").GetComponent<AudioSource>();
			}
			this._S.PlaySound_Stop(this._AS);
			if (this.MG != null)
			{
				this.MG.speedUp = false;
			}
		}
		if (weapon.WeaponID == 137 || weapon.WeaponID == 144)
		{
			if (this.MG == null)
			{
				this.MG = (Minigun)Object.FindObjectOfType(typeof(Minigun));
			}
			if (this._S == null)
			{
				this._S = GameObject.Find("Player").GetComponent<Sound>();
			}
			if (this._AS == null)
			{
				this._AS = GameObject.Find("Player/MainCamera/TMPAudio").GetComponent<AudioSource>();
			}
			this._S.PlaySound_Stop(this._AS);
			if (this.MG != null)
			{
				this.MG.speedUp = false;
				this.MG.speed = 0;
			}
		}
		this.awid = weapon.WeaponID;
		if (this.awid != 0)
		{
			this.goBlockSetup.transform.position = new Vector3(-1000f, -1000f, -1000f);
		}
		if (this.awid != 33)
		{
			this.goBlockCrash.transform.position = new Vector3(-1000f, -1000f, -1000f);
		}
		if (this.awid != 55)
		{
			this.goBlockTNT.transform.position = new Vector3(-1000f, -1000f, -1000f);
		}
		int myindex = this.cscl.myindex;
		int team = (int)BotsController.Instance.Bots[myindex].Team;
		int skin = BotsController.Instance.Bots[myindex].Skin;
		Texture skin2;
		if (weapon.WeaponID == 35)
		{
			skin2 = SkinManager.GetSkin(1, 0);
		}
		else
		{
			skin2 = SkinManager.GetSkin(team, skin);
		}
		if (weapon.m_LeftHandModel)
		{
			weapon.m_LeftHandModel.GetComponent<Renderer>().materials[0].mainTexture = skin2;
		}
		if (weapon.m_RightHandModel)
		{
			weapon.m_RightHandModel.GetComponent<Renderer>().materials[0].mainTexture = skin2;
		}
		if (skin == 311)
		{
			Color color = Color.white;
			if (team == 0)
			{
				color..ctor(0f, 0.45f, 1f);
			}
			else if (team == 1)
			{
				color = Color.red;
			}
			else if (team == 2)
			{
				color = Color.green;
			}
			else if (team == 3)
			{
				color = Color.yellow;
			}
			if (weapon.m_LeftHandModel != null)
			{
				weapon.m_LeftHandModel.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
			}
			weapon.m_RightHandModel.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);
		}
		else
		{
			if (weapon.m_LeftHandModel != null)
			{
				weapon.m_LeftHandModel.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
			}
			weapon.m_RightHandModel.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
		}
		if (weapon.WeaponID == 0)
		{
			int num = 0;
			if (BotsController.Instance.Bots[myindex].Item[198] > 0)
			{
				num = 4;
			}
			this.csig.SetBlockTextureTeam(weapon.m_Face, weapon.m_Top, team + num, true);
		}
		this.cscl.send_currentweapon(weapon.WeaponID);
		if (weapon.WeaponID == 0)
		{
			this.csam.SetWeapon(weapon.WeaponID, this.ammo_block, 0);
			this.cssw.ShowPanel(0);
			return;
		}
		if (weapon.WeaponID == 35)
		{
			this.csam.SetWeapon(weapon.WeaponID, this.ammo_medkit_w, 0);
			return;
		}
		if (weapon.WeaponID == 36)
		{
			this.csam.SetWeapon(weapon.WeaponID, this.ammo_medkit_w, 0);
			this.cssw.ShowPanel(4);
			return;
		}
		if (weapon.WeaponID == 37)
		{
			this.csam.SetWeapon(weapon.WeaponID, this.ammo_medkit_g, 0);
			this.cssw.ShowPanel(4);
			return;
		}
		if (weapon.WeaponID == 38)
		{
			this.csam.SetWeapon(weapon.WeaponID, this.ammo_medkit_o, 0);
			this.cssw.ShowPanel(4);
			return;
		}
		if (ItemsDB.Items[weapon.WeaponID].Category == 7)
		{
			this.csam.SetWeapon(weapon.WeaponID, 0, 0);
			this.cssw.ShowPanel(1);
			return;
		}
		if (weapon.WeaponID == 7 || weapon.WeaponID == 168 || weapon.WeaponID == 170)
		{
			this.csam.SetWeapon(weapon.WeaponID, 0, 0);
			this.cssw.ShowPanel(8);
			return;
		}
		if (weapon.WeaponID == 169 || weapon.WeaponID == 184 || weapon.WeaponID == 186)
		{
			this.csam.SetWeapon(weapon.WeaponID, 0, 0);
			this.cssw.ShowPanel(9);
			return;
		}
		if (weapon.WeaponID == 10 || weapon.WeaponID == 62 || weapon.WeaponID == 185)
		{
			this.csam.SetWeapon(weapon.WeaponID, 0, 0);
			this.cssw.ShowPanel2(0);
			return;
		}
		if (weapon.WeaponID == 55 || weapon.WeaponID == 171 || weapon.WeaponID == 172 || weapon.WeaponID == 183)
		{
			this.csam.SetWeapon(weapon.WeaponID, 0, 0);
			this.cssw.ShowPanel2(1);
			return;
		}
		if (weapon.WeaponID == 138 || weapon.WeaponID == 100)
		{
			this.csam.SetWeapon(weapon.WeaponID, 0, 0);
			this.cssw.ShowPanel2(2);
			return;
		}
		if (this.isPrimaryWeapon(weapon.WeaponID))
		{
			this.csam.SetWeapon(weapon.WeaponID, this.ammo_clip, this.ammo_backpack);
			this.cssw.ShowPanel(2);
			return;
		}
		this.csam.SetWeapon(weapon.WeaponID, this.ammo_clip2, this.ammo_backpack2);
		this.cssw.ShowPanel(3);
		if (weapon.WeaponID == 221 && this.ammo_clip2 == this.ammo_fullclip2)
		{
			this.firstFireCount = 0;
			this.secondFireCount = 0;
		}
	}

	// Token: 0x0600053A RID: 1338 RVA: 0x0006567C File Offset: 0x0006387C
	public bool OnWeaponAttack(vp_FPWeaponShooter weaponshooter)
	{
		if (weaponshooter.m_FPSWeapon.WeaponID == 10)
		{
			if (this.ammocount[0] == 0)
			{
				return false;
			}
			Vector3 pos = base.gameObject.transform.position + base.gameObject.transform.forward * 0.4f + base.gameObject.transform.up * 1.45f;
			Vector3 rot;
			rot..ctor(Camera.main.transform.eulerAngles.x, base.gameObject.transform.eulerAngles.y, 0f);
			Vector3 force = Camera.main.transform.forward * 5000f + Camera.main.transform.up * 100f;
			Vector3 torque;
			torque..ctor(0f, 0f, 0f);
			this.cscl.send_createent(pos, rot, force, torque, 2);
			this.ammocount[0]--;
			this.csam.SetAmmo(this.gren1count, this.gren2count, this.ammocount[0], this.ammocount[1], this.ammocount[2], this.ammo_gp, this.ammo_zbk18m, this.ammo_zof26, this.ammo_snaryad);
			if (this.ammocount[0] <= 0)
			{
				this.SetPrimaryWeapon(false);
			}
			return true;
		}
		else if (weaponshooter.m_FPSWeapon.WeaponID == 185)
		{
			if (this.ammocount[0] == 0)
			{
				return false;
			}
			Vector3 pos2 = base.gameObject.transform.position + base.gameObject.transform.forward * 0.4f + base.gameObject.transform.up * 1.45f;
			Vector3 rot2;
			rot2..ctor(Camera.main.transform.eulerAngles.x, base.gameObject.transform.eulerAngles.y, 0f);
			Vector3 force2 = Camera.main.transform.forward * 5000f + Camera.main.transform.up * 100f;
			Vector3 torque2;
			torque2..ctor(0f, 0f, 20f);
			this.cscl.send_createent(pos2, rot2, force2, torque2, 34);
			this.ammocount[0]--;
			this.csam.SetAmmo(this.gren1count, this.gren2count, this.ammocount[0], this.ammocount[1], this.ammocount[2], this.ammo_gp, this.ammo_zbk18m, this.ammo_zof26, this.ammo_snaryad);
			if (this.ammocount[0] <= 0)
			{
				this.SetPrimaryWeapon(false);
			}
			return true;
		}
		else if (weaponshooter.m_FPSWeapon.WeaponID == 62)
		{
			if (this.ammocount[0] == 0)
			{
				return false;
			}
			Vector3 pos3 = base.gameObject.transform.position + base.gameObject.transform.forward * 0.4f + base.gameObject.transform.up * 1.75f;
			Vector3 rot3;
			rot3..ctor(Camera.main.transform.eulerAngles.x, base.gameObject.transform.eulerAngles.y, 90f);
			Vector3 force3 = base.gameObject.transform.forward * (float)(600 + Random.Range(-100, 100)) + base.gameObject.transform.up * 1200f;
			Vector3 torque3;
			torque3..ctor(0f, 0f, 0f);
			this.cscl.send_createent(pos3, rot3, force3, torque3, 21);
			this.ammocount[0]--;
			this.csam.SetAmmo(this.gren1count, this.gren2count, this.ammocount[0], this.ammocount[1], this.ammocount[2], this.ammo_gp, this.ammo_zbk18m, this.ammo_zof26, this.ammo_snaryad);
			if (this.ammocount[0] <= 0)
			{
				this.SetPrimaryWeapon(false);
			}
			return true;
		}
		else if (weaponshooter.m_FPSWeapon.WeaponID == 138)
		{
			if (this.ammocount[2] == 0)
			{
				return false;
			}
			if (!this.targetLocked)
			{
				this.lastTarget = null;
				this.targetLock = false;
				return false;
			}
			int uid;
			if (this.lastTarget.transform.parent.gameObject.GetComponent<Tank>() != null)
			{
				uid = this.lastTarget.transform.parent.gameObject.GetComponent<Tank>().uid;
			}
			else
			{
				if (!(this.lastTarget.transform.parent.gameObject.GetComponent<Car>() != null))
				{
					this.lastTarget = null;
					this.targetLock = false;
					return false;
				}
				uid = this.lastTarget.transform.parent.gameObject.GetComponent<Car>().uid;
			}
			Vector3 pos4 = base.gameObject.transform.position + base.gameObject.transform.forward * 0.4f + base.gameObject.transform.up * 1.45f;
			Vector3 rot4;
			rot4..ctor(Camera.main.transform.eulerAngles.x, base.gameObject.transform.eulerAngles.y, 0f);
			Vector3 force4;
			force4..ctor(0f, 0f, 0f);
			Vector3 torque4;
			torque4..ctor(0f, 0f, (float)uid);
			this.cscl.send_createent(pos4, rot4, force4, torque4, CONST.ENTS.ENT_JAVELIN);
			this.ammocount[2]--;
			this.csam.SetAmmo(this.gren1count, this.gren2count, this.ammocount[0], this.ammocount[1], this.ammocount[2], this.ammo_gp, this.ammo_zbk18m, this.ammo_zof26, this.ammo_snaryad);
			if (this.ammocount[2] <= 0)
			{
				this.SetPrimaryWeapon(false);
			}
			return true;
		}
		else if (weaponshooter.m_FPSWeapon.WeaponID == 82)
		{
			if (this.ammo_gp == 0)
			{
				return false;
			}
			if (this.ammo_clip == 0)
			{
				return false;
			}
			Vector3 pos5 = base.gameObject.transform.position + base.gameObject.transform.forward * 0.4f + base.gameObject.transform.up * 1.35f + base.gameObject.transform.right * 0.2f;
			Vector3 rot5;
			rot5..ctor(90f, Camera.main.transform.eulerAngles.y, 0f);
			Vector3 force5 = Camera.main.transform.forward * 3000f + Camera.main.transform.up * 100f;
			Vector3 zero = Vector3.zero;
			this.cscl.send_createent(pos5, rot5, force5, zero, 4);
			this.ammo_gp--;
			this.ammo_clip--;
			this.csam.SetWeapon(weaponshooter.m_FPSWeapon.WeaponID, this.ammo_clip, this.ammo_backpack);
			this.csam.SetAmmo(this.gren1count, this.gren2count, this.ammocount[0], this.ammocount[1], this.ammocount[2], this.ammo_gp, this.ammo_zbk18m, this.ammo_zof26, this.ammo_snaryad);
			base.gameObject.GetComponent<Sound>().PlaySound_Weapon(82);
			return true;
		}
		else if (weaponshooter.m_FPSWeapon.WeaponID == 161)
		{
			if (this.ammo_clip == 0)
			{
				return false;
			}
			CrossbowArrow componentInChildren = weaponshooter.gameObject.GetComponentInChildren<CrossbowArrow>();
			Vector3 pos6;
			Vector3 eulerAngles;
			if (componentInChildren)
			{
				RaycastHit raycastHit;
				if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3((float)Screen.width / 2f, (float)Screen.height / 2f, 0f)), ref raycastHit))
				{
					componentInChildren.transform.LookAt(raycastHit.point, componentInChildren.transform.up);
				}
				pos6 = componentInChildren.gameObject.transform.position;
				eulerAngles = componentInChildren.transform.eulerAngles;
				componentInChildren.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
			}
			else
			{
				Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)Screen.width / 2f, (float)Screen.height / 2f, 0f));
				pos6 = ray.origin;
				eulerAngles = Quaternion.LookRotation(ray.direction).eulerAngles;
			}
			Vector3 force6;
			force6..ctor(0f, 0f, 0f);
			Vector3 torque5;
			torque5..ctor(0f, 0f, 0f);
			if (componentInChildren)
			{
				componentInChildren.Hide();
			}
			this.cscl.send_createent(pos6, eulerAngles, force6, torque5, 23);
			this.ammo_clip--;
			this.csam.SetPrimaryAmmo(this.ammo_clip);
			this.csam.SetAmmo(this.gren1count, this.gren2count, this.ammocount[0], this.ammocount[1], this.ammocount[2], this.ammo_gp, this.ammo_zbk18m, this.ammo_zof26, this.ammo_snaryad);
			return true;
		}
		else
		{
			if (weaponshooter.m_FPSWeapon.WeaponID != 100)
			{
				bool flag = this.isPrimaryWeapon(weaponshooter.m_FPSWeapon.WeaponID);
				if (flag)
				{
					if (this.ammo_clip == 0)
					{
						return false;
					}
				}
				else if (this.ammo_clip2 == 0)
				{
					return false;
				}
				if (flag)
				{
					this.ammo_clip--;
				}
				else
				{
					this.ammo_clip2--;
				}
				this.cscr.Shoot(weaponshooter.ProjectileTapFiringRate);
				int weaponID = weaponshooter.m_FPSWeapon.WeaponID;
				if (ItemsDB.CheckItem(weaponID))
				{
					this.weapon_raycast(weaponID, (float)ItemsDB.Items[weaponID].Upgrades[5][0].Val, (int)((float)ItemsDB.Items[weaponID].Upgrades[5][0].Val * 0.7f), this);
				}
				if (flag)
				{
					this.csam.SetWeapon(weaponshooter.m_FPSWeapon.WeaponID, this.ammo_clip, this.ammo_backpack);
				}
				else
				{
					this.csam.SetWeapon(weaponshooter.m_FPSWeapon.WeaponID, this.ammo_clip2, this.ammo_backpack2);
				}
				if (weaponID == 69 || weaponID == 89 || weaponID == 34 || weaponID == 176 || weaponID == 302)
				{
					this.m_Input.Player.Zoom.TryStop();
					this.m_Input.Player.Zoom.NextAllowedStartTime = Time.time + 1.01f;
				}
				return true;
			}
			if (this.ammocount[2] == 0)
			{
				return false;
			}
			if (this.ammo_clip_rockets == 0)
			{
				return false;
			}
			Vector3 pos7 = base.gameObject.transform.position + base.gameObject.transform.forward * 0.8f + base.gameObject.transform.up * 1.45f;
			Vector3 rot6;
			rot6..ctor(Camera.main.transform.eulerAngles.x, base.gameObject.transform.eulerAngles.y, 0f);
			Vector3 force7 = Camera.main.transform.forward * 5500f + Camera.main.transform.up * 100f;
			Vector3 torque6;
			torque6..ctor(0f, 0f, 0f);
			if (weaponshooter.gameObject.GetComponentInChildren<RPGRocket>() != null)
			{
				weaponshooter.gameObject.GetComponentInChildren<RPGRocket>().Hide();
			}
			this.cscl.send_createent(pos7, rot6, force7, torque6, 15);
			this.ammocount[2]--;
			this.ammo_clip_rockets--;
			this.csam.SetAmmo(this.gren1count, this.gren2count, this.ammocount[0], this.ammocount[1], this.ammocount[2], this.ammo_gp, this.ammo_zbk18m, this.ammo_zof26, this.ammo_snaryad);
			if (this.ammocount[2] <= 0)
			{
				this.SetPrimaryWeapon(false);
			}
			return true;
		}
	}

	// Token: 0x0600053B RID: 1339 RVA: 0x00066374 File Offset: 0x00064574
	public void UseVehicleModul(AudioSource AS, int module_index)
	{
		if (module_index == CONST.VEHICLES.VEHICLE_MODUL_REPAIR_KIT && this.ammo_repair_kit <= 0)
		{
			return;
		}
		if (module_index == CONST.VEHICLES.VEHICLE_MODUL_ANTI_MISSLE && this.ammo_module_flash <= 0)
		{
			return;
		}
		if (module_index == CONST.VEHICLES.VEHICLE_MODUL_SMOKE && this.ammo_module_smoke <= 0)
		{
			return;
		}
		if (this.cscl == null)
		{
			this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
		}
		this.cscl.send_use_module(module_index);
		if (module_index == CONST.VEHICLES.VEHICLE_MODUL_REPAIR_KIT)
		{
			this.ammo_repair_kit--;
		}
		if (module_index == CONST.VEHICLES.VEHICLE_MODUL_ANTI_MISSLE)
		{
			this.ammo_module_flash--;
		}
		if (module_index == CONST.VEHICLES.VEHICLE_MODUL_SMOKE)
		{
			this.ammo_module_smoke--;
		}
		base.GetComponent<Sound>().PlaySound_UseModul(AS, module_index);
	}

	// Token: 0x0600053C RID: 1340 RVA: 0x0006643C File Offset: 0x0006463C
	public bool TankFire(int sid, Transform FP)
	{
		Vector3 position = FP.position;
		Vector3 rot = FP.transform.eulerAngles - new Vector3(0f, -90f, 0f);
		Vector3 force = FP.transform.forward * 10000f;
		Vector3 torque;
		torque..ctor(0f, 0f, 0f);
		if (sid == 14)
		{
			if (this.ammo_snaryad <= 0)
			{
				return false;
			}
			this.ammo_snaryad--;
		}
		else if (sid == 19)
		{
			if (this.ammo_zbk18m <= 0)
			{
				return false;
			}
			this.ammo_zbk18m--;
		}
		else if (sid == 20)
		{
			if (this.ammo_zof26 <= 0)
			{
				return false;
			}
			this.ammo_zof26--;
		}
		if (this.cscl == null)
		{
			this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
		}
		this.cscl.send_createent(position, rot, force, torque, sid);
		return true;
	}

	// Token: 0x0600053D RID: 1341 RVA: 0x00066538 File Offset: 0x00064738
	public void TankMGFire()
	{
		this.MGammo_clip--;
		this.MGammo--;
		if (this.MGammo_clip < 0)
		{
			this.MGammo_clip = 0;
		}
		if (this.MGammo < 0)
		{
			this.MGammo = 0;
		}
	}

	// Token: 0x0600053E RID: 1342 RVA: 0x00066576 File Offset: 0x00064776
	public void JeepMGFire()
	{
		this.JeepMGammo--;
		if (this.JeepMGammo < 0)
		{
			this.JeepMGammo = 0;
		}
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x00066598 File Offset: 0x00064798
	public bool OnWeaponMeleeAttack(vp_FPWeapon weapon)
	{
		if (this.cscl == null)
		{
			this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
		}
		if (weapon.WeaponID == 0)
		{
			if (this.ammo_block == 0 && ConnectionInfo.mode != CONST.CFG.BUILD_MODE)
			{
				return false;
			}
			Vector3i? cursor = this.GetCursor(false, 4);
			if (cursor == null)
			{
				return false;
			}
			if (this.cscc == null)
			{
				this.cscc = base.gameObject.GetComponent<CharacterController>();
			}
			if (BlockCharacterCollision.GetContactBlockCharacter(cursor.Value, base.transform.position, this.cscc) != null)
			{
				return false;
			}
			this.ammo_block--;
			if (ConnectionInfo.mode == CONST.CFG.BUILD_MODE)
			{
				this.cscl.send_selectblock((byte)BotsController.Instance.Bots[PlayerProfile.myindex].blockFlag);
			}
			this.cscl.send_setblock(cursor.Value.x, cursor.Value.y, cursor.Value.z, Time.time);
			this.csam.SetWeapon(weapon.WeaponID, this.ammo_block, 0);
			base.gameObject.GetComponent<Sound>().PlaySound_Weapon(0);
			return true;
		}
		else
		{
			if (ItemsDB.Items[weapon.WeaponID].Category == 7)
			{
				if (weapon.WeaponID == 159)
				{
					this.weapon_raycast(weapon.WeaponID, 2.5f, 2, this);
				}
				else	
				{
					this.weapon_raycast(weapon.WeaponID, 2f, 2, this);
				}
				return true;
			}
			if (weapon.WeaponID == 7)
			{
				if (this.gren1count == 0)
				{
					return false;
				}
				Vector3 pos = base.gameObject.transform.position + base.gameObject.transform.forward * 0.4f + base.gameObject.transform.up * 1.45f;
				Vector3 rot;
				rot..ctor(0f, 0f, 0f);
				Vector3 force = Camera.main.transform.forward * 500f + Camera.main.transform.up * 100f;
				Vector3 torque = Camera.main.transform.forward * 10f + Camera.main.transform.right * 10f;
				this.cscl.send_createent(pos, rot, force, torque, 1);
				this.gren1count--;
				this.csam.SetAmmo(this.gren1count, this.gren2count, this.ammocount[0], this.ammocount[1], this.ammocount[2], this.ammo_gp, this.ammo_zbk18m, this.ammo_zof26, this.ammo_snaryad);
				return true;
			}
			else if (weapon.WeaponID == 169)
			{
				if (this.gren2count == 0)
				{
					return false;
				}
				Vector3 pos2 = base.gameObject.transform.position + base.gameObject.transform.forward * 0.4f + base.gameObject.transform.up * 1.45f;
				Vector3 rot2;
				rot2..ctor(0f, 0f, 0f);
				Vector3 force2 = Camera.main.transform.forward * 500f + Camera.main.transform.up * 100f;
				Vector3 torque2 = Camera.main.transform.forward * 10f + Camera.main.transform.right * 10f;
				this.cscl.send_createent(pos2, rot2, force2, torque2, 24);
				this.gren2count--;
				this.csam.SetAmmo(this.gren1count, this.gren2count, this.ammocount[0], this.ammocount[1], this.ammocount[2], this.ammo_gp, this.ammo_zbk18m, this.ammo_zof26, this.ammo_snaryad);
				return true;
			}
			else if (weapon.WeaponID == 168)
			{
				if (this.gren1count == 0)
				{
					return false;
				}
				Vector3 pos3 = base.gameObject.transform.position + base.gameObject.transform.forward * 0.4f + base.gameObject.transform.up * 1.45f;
				Vector3 rot3;
				rot3..ctor(0f, 0f, 0f);
				Vector3 force3 = Camera.main.transform.forward * 500f + Camera.main.transform.up * 100f;
				Vector3 torque3 = Camera.main.transform.forward * 10f + Camera.main.transform.right * 10f;
				this.cscl.send_createent(pos3, rot3, force3, torque3, 25);
				this.gren1count--;
				this.csam.SetAmmo(this.gren1count, this.gren2count, this.ammocount[0], this.ammocount[1], this.ammocount[2], this.ammo_gp, this.ammo_zbk18m, this.ammo_zof26, this.ammo_snaryad);
				return true;
			}
			else if (weapon.WeaponID == 170)
			{
				if (this.gren1count == 0)
				{
					return false;
				}
				Vector3 pos4 = base.gameObject.transform.position + base.gameObject.transform.forward * 0.4f + base.gameObject.transform.up * 1.45f;
				Vector3 rot4;
				rot4..ctor(0f, 0f, 0f);
				Vector3 force4 = Camera.main.transform.forward * 500f + Camera.main.transform.up * 100f;
				Vector3 torque4 = Camera.main.transform.forward * 10f + Camera.main.transform.right * 10f;
				this.cscl.send_createent(pos4, rot4, force4, torque4, 26);
				this.gren1count--;
				this.csam.SetAmmo(this.gren1count, this.gren2count, this.ammocount[0], this.ammocount[1], this.ammocount[2], this.ammo_gp, this.ammo_zbk18m, this.ammo_zof26, this.ammo_snaryad);
				return true;
			}
			else if (weapon.WeaponID == 184)
			{
				if (this.gren2count == 0)
				{
					return false;
				}
				Vector3 pos5 = base.gameObject.transform.position + base.gameObject.transform.forward * 0.5f + base.gameObject.transform.up * 1.45f;
				Vector3 rot5;
				rot5..ctor(0f, 0f, 0f);
				Vector3 force5 = Camera.main.transform.forward * 500f + Camera.main.transform.up * 100f;
				Vector3 torque5 = Camera.main.transform.forward * 10f + Camera.main.transform.right * 10f;
				this.cscl.send_createent(pos5, rot5, force5, torque5, 33);
				this.gren2count--;
				this.csam.SetAmmo(this.gren1count, this.gren2count, this.ammocount[0], this.ammocount[1], this.ammocount[2], this.ammo_gp, this.ammo_zbk18m, this.ammo_zof26, this.ammo_snaryad);
				return true;
			}
			else if (weapon.WeaponID == 186)
			{
				if (this.gren2count == 0)
				{
					return false;
				}
				Vector3 pos6 = base.gameObject.transform.position + base.gameObject.transform.forward * 0.4f + base.gameObject.transform.up * 1.45f;
				Vector3 rot6;
				rot6..ctor(0f, 0f, 0f);
				Vector3 force6 = Camera.main.transform.forward * 500f + Camera.main.transform.up * 100f;
				Vector3 torque6 = Camera.main.transform.forward * 10f + Camera.main.transform.right * 10f;
				this.cscl.send_createent(pos6, rot6, force6, torque6, 35);
				this.gren2count--;
				this.csam.SetAmmo(this.gren1count, this.gren2count, this.ammocount[0], this.ammocount[1], this.ammocount[2], this.ammo_gp, this.ammo_zbk18m, this.ammo_zof26, this.ammo_snaryad);
				return true;
			}
			else
			{
				if (weapon.WeaponID == 35)
				{
					this.weapon_raycast(weapon.WeaponID, 1.75f, 2, this);
					return true;
				}
				if (weapon.WeaponID == 36)
				{
					if (this.ammo_medkit_w <= 0)
					{
						return false;
					}
					if (this.m_Health == null)
					{
						this.m_Health = (Health)Object.FindObjectOfType(typeof(Health));
					}
					if (this.m_Health == null)
					{
						return false;
					}
					if (this.cscl == null)
					{
						return false;
					}
					if (this.m_Health.GetHealth() == 100)
					{
						return false;
					}
					this.cscl.send_weapon_medkit(0);
					this.ammo_medkit_w--;
					this.csam.SetWeapon(weapon.WeaponID, this.ammo_medkit_w, 0);
					return true;
				}
				else if (weapon.WeaponID == 37)
				{
					if (this.ammo_medkit_g <= 0)
					{
						return false;
					}
					if (this.m_Health == null)
					{
						this.m_Health = (Health)Object.FindObjectOfType(typeof(Health));
					}
					if (this.m_Health == null)
					{
						return false;
					}
					if (this.cscl == null)
					{
						return false;
					}
					if (this.m_Health.GetHealth() == 100)
					{
						return false;
					}
					this.cscl.send_weapon_medkit(1);
					this.ammo_medkit_g--;
					this.csam.SetWeapon(weapon.WeaponID, this.ammo_medkit_g, 0);
					return true;
				}
				else if (weapon.WeaponID == 38)
				{
					if (this.ammo_medkit_o <= 0)
					{
						return false;
					}
					if (this.m_Health == null)
					{
						this.m_Health = (Health)Object.FindObjectOfType(typeof(Health));
					}
					if (this.m_Health == null)
					{
						return false;
					}
					if (this.cscl == null)
					{
						return false;
					}
					if (this.m_Health.GetHealth() == 100)
					{
						return false;
					}
					this.cscl.send_weapon_medkit(2);
					this.ammo_medkit_o--;
					this.csam.SetWeapon(weapon.WeaponID, this.ammo_medkit_o, 0);
					return true;
				}
				else if (weapon.WeaponID == 55)
				{
					if (this.ammocount[1] <= 0)
					{
						return false;
					}
					Vector3i? cursor2 = this.GetCursor(false, 4);
					if (cursor2 == null)
					{
						return false;
					}
					if (this.cscc == null)
					{
						this.cscc = base.gameObject.GetComponent<CharacterController>();
					}
					if (BlockCharacterCollision.GetContactBlockCharacter(cursor2.Value, base.transform.position, this.cscc) != null)
					{
						return false;
					}
					this.ammocount[1]--;
					this.cscl.send_weapon_tnt(cursor2.Value.x, cursor2.Value.y, cursor2.Value.z, Time.time);
					this.csam.SetAmmo(this.gren1count, this.gren2count, this.ammocount[0], this.ammocount[1], this.ammocount[2], this.ammo_gp, this.ammo_zbk18m, this.ammo_zof26, this.ammo_snaryad);
					base.gameObject.GetComponent<Sound>().PlaySound_Weapon(0);
					if (this.ammocount[1] <= 0)
					{
						this.SetPrimaryWeapon(false);
					}
					return true;
				}
				else if (weapon.WeaponID == 171)
				{
					if (this.ammocount[1] <= 0)
					{
						return false;
					}
					Vector3i? cursor3 = this.GetCursor(false, 4);
					if (cursor3 != null)
					{
						this.ammocount[1]--;
						Vector3 pos7;
						pos7..ctor((float)cursor3.Value.x, (float)cursor3.Value.y - 0.5f, (float)cursor3.Value.z);
						Vector3 rot7;
						rot7..ctor(0f, 0f, 0f);
						Vector3 force7;
						force7..ctor(0f, 0f, 0f);
						Vector3 torque7;
						torque7..ctor(0f, 0f, 0f);
						this.cscl.send_createent(pos7, rot7, force7, torque7, 27);
						this.csam.SetAmmo(this.gren1count, this.gren2count, this.ammocount[0], this.ammocount[1], this.ammocount[2], this.ammo_gp, this.ammo_zbk18m, this.ammo_zof26, this.ammo_snaryad);
						base.gameObject.GetComponent<Sound>().PlaySound_MinePlace();
						if (this.ammocount[1] <= 0)
						{
							this.SetPrimaryWeapon(false);
						}
						return true;
					}
					return false;
				}
				else if (weapon.WeaponID == 183)
				{
					if (this.ammocount[1] <= 0)
					{
						return false;
					}
					Vector3i? cursor4 = this.GetCursor(false, 4);
					if (cursor4 != null)
					{
						this.ammocount[1]--;
						Vector3 pos8;
						pos8..ctor((float)cursor4.Value.x, (float)cursor4.Value.y - 0.5f, (float)cursor4.Value.z);
						Vector3 rot8;
						rot8..ctor(0f, 0f, 0f);
						Vector3 force8;
						force8..ctor(0f, 0f, 0f);
						Vector3 torque8;
						torque8..ctor(0f, 0f, 0f);
						this.cscl.send_createent(pos8, rot8, force8, torque8, 32);
						this.csam.SetAmmo(this.gren1count, this.gren2count, this.ammocount[0], this.ammocount[1], this.ammocount[2], this.ammo_gp, this.ammo_zbk18m, this.ammo_zof26, this.ammo_snaryad);
						base.gameObject.GetComponent<Sound>().PlaySound_MinePlace();
						if (this.ammocount[1] <= 0)
						{
							this.SetPrimaryWeapon(false);
						}
						return true;
					}
					return false;
				}
				else
				{
					if (weapon.WeaponID != 172)
					{
						return false;
					}
					if (this.ammocount[1] <= 0)
					{
						return false;
					}
					Vector3 pos9 = base.gameObject.transform.position + base.gameObject.transform.forward * 0.4f + base.gameObject.transform.up * 1.45f;
					Vector3 rot9;
					rot9..ctor(0f, Camera.main.transform.rotation.eulerAngles.y + 180f, 0f);
					Vector3 force9 = Camera.main.transform.forward * 300f + Camera.main.transform.up * 20f;
					Vector3 torque9;
					torque9..ctor(0f, 0f, 0f);
					this.cscl.send_createent(pos9, rot9, force9, torque9, 28);
					this.ammocount[1]--;
					this.csam.SetAmmo(this.gren1count, this.gren2count, this.ammocount[0], this.ammocount[1], this.ammocount[2], this.ammo_gp, this.ammo_zbk18m, this.ammo_zof26, this.ammo_snaryad);
					return true;
				}
			}
		}
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x00067680 File Offset: 0x00065880
	public bool OnWeaponReloadStart(vp_FPWeapon weapon)
	{
		if (weapon.WeaponID == 315)
		{
			return false;
		}
		if (!this.WeaponCanReload(weapon.WeaponID))
		{
			return false;
		}
		if (weapon.WeaponID == 100 && (this.ammocount[2] <= 0 || this.ammo_clip_rockets == 1))
		{
			return false;
		}
		if (this.cscl == null)
		{
			this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
		}
		if (this.isPrimaryWeapon(weapon.WeaponID))
		{
			if (this.ammo_backpack == 0)
			{
				return false;
			}
		}
		else if (this.ammo_backpack2 == 0)
		{
			return false;
		}
		if (ItemsDB.Items[weapon.WeaponID].Category == 1)
		{
			if (this.ammo_clip2 == this.ammo_fullclip2)
			{
				return false;
			}
		}
		else if (this.ammo_clip == this.ammo_fullclip)
		{
			return false;
		}
		this.cscl.send_prereload(weapon.WeaponID, Time.time);
		return true;
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x00067764 File Offset: 0x00065964
	public void OnWeaponReloadEnd(vp_FPWeapon weapon)
	{
		if (!this.WeaponCanReload(weapon.WeaponID))
		{
			return;
		}
		if (this.cscl == null)
		{
			this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
		}
		bool flag = this.isPrimaryWeapon(weapon.WeaponID);
		if (weapon.WeaponID != 100)
		{
			int num;
			if (flag)
			{
				num = this.ammo_fullclip;
			}
			else
			{
				num = this.ammo_fullclip2;
			}
			if (flag)
			{
				this.ammo_backpack += this.ammo_clip;
				if (this.ammo_backpack < num)
				{
					this.ammo_clip = this.ammo_backpack;
					this.ammo_backpack = 0;
				}
				else
				{
					this.ammo_backpack -= num;
					this.ammo_clip = num;
				}
			}
			else
			{
				this.ammo_backpack2 += this.ammo_clip2;
				if (this.ammo_backpack2 < num)
				{
					this.ammo_clip2 = this.ammo_backpack2;
					this.ammo_backpack2 = 0;
				}
				else
				{
					this.ammo_backpack2 -= num;
					this.ammo_clip2 = num;
				}
				if (weapon.WeaponID == 221)
				{
					this.firstFireCount = 0;
					this.secondFireCount = 0;
				}
			}
			if (flag)
			{
				this.csam.SetWeapon(weapon.WeaponID, this.ammo_clip, this.ammo_backpack);
			}
			else
			{
				this.csam.SetWeapon(weapon.WeaponID, this.ammo_clip2, this.ammo_backpack2);
			}
		}
		if (weapon.WeaponID == 100)
		{
			if (weapon.gameObject.GetComponentInChildren<RPGRocket>() != null)
			{
				weapon.gameObject.GetComponentInChildren<RPGRocket>().Show();
			}
			this.ammo_clip_rockets = 1;
		}
		if (weapon.WeaponID == 161 && weapon.gameObject.GetComponentInChildren<CrossbowArrow>() != null)
		{
			weapon.gameObject.GetComponentInChildren<CrossbowArrow>().Show();
		}
		this.cscl.send_reload(weapon.WeaponID, Time.time);
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x0006793C File Offset: 0x00065B3C
	public void OnWeaponZoomStart(vp_FPWeapon weapon)
	{
		if (weapon.WeaponID != 79 && weapon.WeaponID != 80 && weapon.WeaponID != 208)
		{
			if (this.HasScope(weapon.WeaponID))
			{
				base.gameObject.GetComponent<Sound>().PlaySound_Zoom();
			}
			this.cscr.SetActive(false);
			return;
		}
		if (this.ammo_gp == 0)
		{
			return;
		}
		this.clientreload2 = Time.time;
		Vector3 pos = base.gameObject.transform.position + base.gameObject.transform.forward * 0.4f + base.gameObject.transform.up * 1.35f + base.gameObject.transform.right * 0.2f;
		Vector3 rot;
		rot..ctor(90f, Camera.main.transform.eulerAngles.y, 0f);
		Vector3 force = Camera.main.transform.forward * 3000f + Camera.main.transform.up * 100f;
		Vector3 zero = Vector3.zero;
		this.cscl.send_createent(pos, rot, force, zero, 4);
		this.ammo_gp--;
		this.csam.SetAmmo(this.gren1count, this.gren2count, this.ammocount[0], this.ammocount[1], this.ammocount[2], this.ammo_gp, this.ammo_zbk18m, this.ammo_zof26, this.ammo_snaryad);
		base.gameObject.GetComponent<Sound>().PlaySound_GPLauncher();
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x00067AF4 File Offset: 0x00065CF4
	private bool HasScope(int WeaponID)
	{
		return WeaponID == 3 || WeaponID == 10 || WeaponID == 19 || WeaponID == 18 || WeaponID == 47 || WeaponID == 69 || WeaponID == 34 || WeaponID == 71 || WeaponID == 81 || WeaponID == 100 || WeaponID == 111 || WeaponID == 112 || WeaponID == 142 || WeaponID == 161 || WeaponID == 176 || WeaponID == 185 || WeaponID == 302 || WeaponID == 332;
	}

	// Token: 0x06000544 RID: 1348 RVA: 0x00067B6F File Offset: 0x00065D6F
	public void OnWeaponZoomEnd(vp_FPWeapon weapon)
	{
		if (this.HasScope(weapon.WeaponID))
		{
			base.gameObject.GetComponent<Sound>().PlaySound_Zoom();
		}
		this.cscr.SetActive(true);
	}

	// Token: 0x06000545 RID: 1349 RVA: 0x00067B9C File Offset: 0x00065D9C
	public bool WeaponCanReload(int wid)
	{
		return wid != 0 && wid != 62 && wid != 138 && wid != 55 && wid != 174 && wid != 304 && wid != 185 && wid != 315 && ItemsDB.Items[wid].Category != 7 && ItemsDB.Items[wid].Category != 19;
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x00067C00 File Offset: 0x00065E00
	public void HideWeapons(bool val)
	{
		this.m_Input.SetHideWeapons(val);
	}

	// Token: 0x06000547 RID: 1351 RVA: 0x00067C0E File Offset: 0x00065E0E
	public void SetPrimaryWeapon(bool build_mode_and_block = false)
	{
		this.m_Input.SetPrimaryWeapon(build_mode_and_block);
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x00067C1C File Offset: 0x00065E1C
	private void weapon_raycast(int wid, float dist, int blockdist, WeaponSystem WS)
	{
		if (WS != this)
		{
			return;
		}
		if (this.cscl == null)
		{
			this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
		}
		Ray ray = Camera.main.ScreenPointToRay(new Vector3((float)Screen.width / 2f, (float)Screen.height / 2f, 0f));
		if (wid == 137 || wid == 144)
		{
			ray.direction += new Vector3(Random.Range(-0.02f, 0.02f), Random.Range(-0.02f, 0.02f), Random.Range(-0.02f, 0.02f));
		}
		if (wid == 315)
		{
			ray.direction += new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
		}
		LayerMask layerMask = this.layerMask;
		if (wid == 35)
		{
			layerMask = this.ZlayerMask;
		}
		RaycastHit raycastHit;
		if (!Physics.Raycast(ray, ref raycastHit, dist, layerMask))
		{
			this.cscl.send_milkattack(wid, Time.time);
			return;
		}
		RaycastHit raycastHit2;
		if (Physics.Linecast(raycastHit.point, Camera.main.transform.position, ref raycastHit2, layerMask))
		{
			this.cscl.send_milkattack(wid, Time.time);
			return;
		}
		if (wid == 315)
		{
			this.cspm.CreateFlame(raycastHit.point, raycastHit.transform);
		}
		if (raycastHit.transform.name[0] == '(')
		{
			Vector3i? vector3i;
			if (wid != 137 && wid != 144 && wid != 315)
			{
				vector3i = this.GetCursor(true, blockdist);
			}
			else
			{
				vector3i = this.GetCursorUnderRay(true, blockdist, ray);
			}
			if (vector3i != null)
			{
				this.weapon_attack_block(wid, vector3i.Value, Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z, raycastHit.point.x, raycastHit.point.y, raycastHit.point.z);
			}
			return;
		}
		if ((raycastHit.collider.gameObject.name == "blockade_tank_default" || raycastHit.collider.gameObject.name == "blockade_jeep") && (wid == 101 || wid == 111))
		{
			if (raycastHit.collider.gameObject.name == "blockade_tank_default")
			{
				this.cscl.send_attack_ent(raycastHit.collider.transform.parent.GetComponent<Tank>().uid, wid);
			}
			else
			{
				this.cscl.send_attack_ent(raycastHit.collider.transform.parent.GetComponent<Car>().uid, wid);
			}
			this.cspm.CreateParticle(raycastHit.point.x, raycastHit.point.y, raycastHit.point.z, 1f, 1f, 0f, 1f);
			return;
		}
		if (raycastHit.collider.gameObject.name.Contains("MINE_"))
		{
			this.cscl.send_detonateent(raycastHit.collider.gameObject.GetComponent<Mine>().uid, raycastHit.collider.gameObject.transform.position);
			raycastHit.collider.gameObject.GetComponent<Mine>().KillSelf();
			return;
		}
		if (raycastHit.collider.gameObject.name.Contains("C4_"))
		{
			this.cscl.send_detonateent(raycastHit.collider.gameObject.GetComponent<C4>().uid, raycastHit.collider.gameObject.transform.position);
			raycastHit.collider.gameObject.GetComponent<C4>().KillSelf();
			return;
		}
		Data component = raycastHit.transform.gameObject.GetComponent<Data>();
		if (component == null)
		{
			this.cscl.send_milkattack(wid, Time.time);
			return;
		}
		if (this.GetCursor(true, (int)raycastHit.distance) != null)
		{
			return;
		}
		if (component.hitzone != 77 && wid != 315 && !raycastHit.transform.gameObject.GetComponent<Data>().isGost)
		{
			this.cspm.CreateHit(base.transform, component.hitzone, raycastHit.point.x, raycastHit.point.y, raycastHit.point.z);
			this.cspm.CreateParticle(raycastHit.point.x, raycastHit.point.y, raycastHit.point.z, 1f, 0f, 0f, 1f);
		}
		this.cscl.send_damage(wid, component.index, component.hitzone, Time.time, base.gameObject.transform.position.x, base.gameObject.transform.position.y, base.gameObject.transform.position.z, raycastHit.transform.position.x, raycastHit.transform.position.y, raycastHit.transform.position.z, Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z, raycastHit.point.x, raycastHit.point.y, raycastHit.point.z);
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x00068240 File Offset: 0x00066440
	private Vector3i? GetCursor(bool inside, int radius)
	{
		Ray ray;
		ray..ctor(Camera.main.transform.position, Camera.main.transform.forward);
		Vector3? vector = MapRayIntersection.Intersection(this.csmap, ray, (float)radius);
		if (vector != null)
		{
			Vector3 vector2 = vector.Value;
			if (inside)
			{
				vector2 += ray.direction * 0.01f;
			}
			if (!inside)
			{
				vector2 -= ray.direction * 0.01f;
			}
			int x = Mathf.RoundToInt(vector2.x);
			int y = Mathf.RoundToInt(vector2.y);
			int z = Mathf.RoundToInt(vector2.z);
			return new Vector3i?(new Vector3i(x, y, z));
		}
		return null;
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x00068304 File Offset: 0x00066504
	private Vector3i? GetCursorUnderRay(bool inside, int radius, Ray ray)
	{
		Vector3? vector = MapRayIntersection.Intersection(this.csmap, ray, (float)radius);
		if (vector != null)
		{
			Vector3 vector2 = vector.Value;
			if (inside)
			{
				vector2 += ray.direction * 0.01f;
			}
			if (!inside)
			{
				vector2 -= ray.direction * 0.01f;
			}
			int x = Mathf.RoundToInt(vector2.x);
			int y = Mathf.RoundToInt(vector2.y);
			int z = Mathf.RoundToInt(vector2.z);
			return new Vector3i?(new Vector3i(x, y, z));
		}
		return null;
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x000683A4 File Offset: 0x000665A4
	private void weapon_attack_block(int wid, Vector3i pointvalue, float x1, float y1, float z1, float x2, float y2, float z2)
	{
		if (this.cscl == null)
		{
			this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
		}
		BlockData block = this.csmap.GetBlock(pointvalue);
		if (block.block == null)
		{
			return;
		}
		if (block.block.GetName() == "Stoneend")
		{
			if (wid != 315)
			{
				base.gameObject.GetComponent<Sound>().PlaySound_Block("Stoneend");
			}
			this.cspm.CreateParticle((float)pointvalue.x, (float)pointvalue.y, (float)pointvalue.z, 0.75f, 0.75f, 0.75f, 255f);
			if (PlayerControl.GetGameMode() == CONST.CFG.BUILD_MODE && PlayerControl.isPrivateAdmin() == 1)
			{
				this.cscl.send_attackblock(pointvalue.x, pointvalue.y, pointvalue.z, wid, Time.time, x1, y1, z1, x2, y2, z2);
				return;
			}
		}
		else
		{
			this.cscl.send_attackblock(pointvalue.x, pointvalue.y, pointvalue.z, wid, Time.time, x1, y1, z1, x2, y2, z2);
			if (block.block != null)
			{
				if (block.block.GetName() == "Leaf")
				{
					if (wid != 315)
					{
						base.gameObject.GetComponent<Sound>().PlaySound_Block("Leaf");
					}
					this.cspm.CreateParticle((float)pointvalue.x, (float)pointvalue.y, (float)pointvalue.z, 0.2f, 1f, 0.2f, 255f);
					return;
				}
				if (block.block.GetName() == "Stone")
				{
					if (wid != 315)
					{
						base.gameObject.GetComponent<Sound>().PlaySound_Block("Stone");
					}
					this.cspm.CreateParticle((float)pointvalue.x, (float)pointvalue.y, (float)pointvalue.z, 0.5f, 0.5f, 0.5f, 255f);
					return;
				}
				if (block.block.GetName() == "Brick")
				{
					if (wid != 315)
					{
						base.gameObject.GetComponent<Sound>().PlaySound_Block("Brick");
					}
					this.cspm.CreateParticle((float)pointvalue.x, (float)pointvalue.y, (float)pointvalue.z, 0.75f, 0.3f, 0.2f, 255f);
					return;
				}
				if (block.block.GetName() == "Wood")
				{
					if (wid != 315)
					{
						base.gameObject.GetComponent<Sound>().PlaySound_Block("Wood");
					}
					this.cspm.CreateParticle((float)pointvalue.x, (float)pointvalue.y, (float)pointvalue.z, 0.75f, 0.3f, 0.2f, 255f);
					return;
				}
				if (wid != 315)
				{
					base.gameObject.GetComponent<Sound>().PlaySound_Weapon(33);
				}
				this.cspm.CreateParticle((float)pointvalue.x, (float)pointvalue.y, (float)pointvalue.z, 0.5f, 0.25f, 0f, 255f);
			}
		}
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x000686CE File Offset: 0x000668CE
	private bool isPrimaryWeapon(int wid)
	{
		return ItemsDB.CheckItem(wid) && ItemsDB.Items[wid].Category != 1;
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x000686EC File Offset: 0x000668EC
	public int GetAmmoWid(int i)
	{
		return this.ammowid[i];
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x000686F6 File Offset: 0x000668F6
	public int GetGAmmo()
	{
		return this.gren1count;
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x000686FE File Offset: 0x000668FE
	public int GetHAmmo()
	{
		return this.gren2count;
	}

	// Token: 0x06000550 RID: 1360 RVA: 0x00068706 File Offset: 0x00066906
	public int GetAmmo(int i)
	{
		if (PlayerControl.GetGameMode() == 6)
		{
			return 0;
		}
		return this.ammocount[i];
	}

	// Token: 0x06000551 RID: 1361 RVA: 0x0006871A File Offset: 0x0006691A
	public int GetPrimaryAmmo()
	{
		return this.ammo_clip;
	}

	// Token: 0x06000552 RID: 1362 RVA: 0x00068722 File Offset: 0x00066922
	public int GetAmmoShmel()
	{
		if (PlayerControl.GetGameMode() == 6)
		{
			return 0;
		}
		return this.ammocount[0];
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x00068736 File Offset: 0x00066936
	public int GetAmmoM61()
	{
		if (PlayerControl.GetGameMode() == 6)
		{
			return 0;
		}
		return this.gren1count;
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x00068748 File Offset: 0x00066948
	public int GetAmmoTNT()
	{
		if (PlayerControl.GetGameMode() == 6)
		{
			return 0;
		}
		return this.ammocount[1];
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x0006875C File Offset: 0x0006695C
	public int GetAmmoGP()
	{
		if (Time.time < this.clientreload2 + 1f)
		{
			return 0;
		}
		return this.ammo_gp;
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x00068779 File Offset: 0x00066979
	public int GetAmmoMedkit_w()
	{
		return this.ammo_medkit_w;
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x00068781 File Offset: 0x00066981
	public int GetAmmoMedkit_g()
	{
		return this.ammo_medkit_g;
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x00068789 File Offset: 0x00066989
	public int GetAmmoMedkit_o()
	{
		return this.ammo_medkit_o;
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x00068791 File Offset: 0x00066991
	public int GetModuleRepairKit()
	{
		return this.ammo_repair_kit;
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x00068799 File Offset: 0x00066999
	public int GetModuleFlash()
	{
		return this.ammo_module_flash;
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x000687A1 File Offset: 0x000669A1
	public int GetModuleSmoke()
	{
		return this.ammo_module_smoke;
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x000687A9 File Offset: 0x000669A9
	public int GetJeepMGAmmo()
	{
		return this.JeepMGammo;
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x000687B1 File Offset: 0x000669B1
	public int GetMGAmmo()
	{
		return this.MGammo;
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x000687B9 File Offset: 0x000669B9
	public int GetMGAmmoClip()
	{
		return this.MGammo_clip;
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x000687C1 File Offset: 0x000669C1
	public int GetMGAmmoBackpack()
	{
		return this.MGammo_backpack;
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x000687C9 File Offset: 0x000669C9
	public int GetZBK18M()
	{
		if (PlayerControl.GetGameMode() != 11)
		{
			return 0;
		}
		return this.ammo_zbk18m;
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x000687DC File Offset: 0x000669DC
	public int GetZOF26()
	{
		if (PlayerControl.GetGameMode() != 11)
		{
			return 0;
		}
		return this.ammo_zof26;
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x000687EF File Offset: 0x000669EF
	public int GetSnaryad()
	{
		if (PlayerControl.GetGameMode() != 11)
		{
			return 0;
		}
		return this.ammo_snaryad;
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x00068802 File Offset: 0x00066A02
	public void SetZombieWeapon()
	{
		this.pwid = 8;
		this.m_Input.SetZombieWeapon();
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x00068818 File Offset: 0x00066A18
	public bool GetBlackSkin()
	{
		return BotsController.Instance.Bots[this.cscl.myindex].Skin == 5 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 20 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 8 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 25 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 26 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 27 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 30 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 31 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 42 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 59 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 64 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 67 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 76 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 84 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 85 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 97 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 98 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 99 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 113 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 114 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 115 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 116 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 117 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 167 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 182 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 311 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 312 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 319 || BotsController.Instance.Bots[this.cscl.myindex].Skin == 322;
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x00068C08 File Offset: 0x00066E08
	public void Shake(Vector3 epos, bool force = false)
	{
		float num = Vector3.Distance(epos, base.transform.position);
		if (force)
		{
			num = 10f;
		}
		if (num > 12f)
		{
			return;
		}
		float num2 = 0.0025f;
		if (num < 10f)
		{
			num2 = 0.005f;
		}
		num = 12f - num;
		this.m_FPCamera.AddForce2(new Vector3(2f, -10f, 2f) * num * num2);
		if (Random.value > 0.5f)
		{
			num2 = -num2;
		}
		this.m_FPCamera.AddRollForce(num2 * 200f);
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x00068CA1 File Offset: 0x00066EA1
	public void SetMouseSensitivity(float val)
	{
		this.m_FPCamera.SetMouseSensitivity(val);
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x00068CAF File Offset: 0x00066EAF
	public void StartShake()
	{
		base.StartCoroutine(this.ShakeLift(10f));
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x00068CC4 File Offset: 0x00066EC4
	private void LiftMeUp()
	{
		float time = Time.time;
		float num = 0.0015f;
		this.m_FPCamera.AddForce2(new Vector3(2f, -10f, 2f) * num);
		if (Random.value > 0.5f)
		{
			num = -num;
		}
		this.m_FPCamera.AddRollForce(num * 10f);
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x00068D23 File Offset: 0x00066F23
	private IEnumerator ShakeLift(float timer)
	{
		if (timer < 0f)
		{
			yield break;
		}
		timer -= 0.03f;
		yield return new WaitForSeconds(0.03f);
		this.LiftMeUp();
		base.StartCoroutine(this.ShakeLift(timer));
		yield break;
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x00068D39 File Offset: 0x00066F39
	public int GetAmmoRPG7()
	{
		if (PlayerControl.GetGameMode() == 6)
		{
			return 0;
		}
		return this.ammocount[2];
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x00068722 File Offset: 0x00066922
	public int GetAmmoMinefly()
	{
		if (PlayerControl.GetGameMode() == 6)
		{
			return 0;
		}
		return this.ammocount[0];
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x00068D39 File Offset: 0x00066F39
	public int GetAmmoJavelin()
	{
		if (PlayerControl.GetGameMode() == 6)
		{
			return 0;
		}
		return this.ammocount[2];
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x00068D4D File Offset: 0x00066F4D
	public int GetAmmoRPGClip()
	{
		if (PlayerControl.GetGameMode() == 6)
		{
			return 0;
		}
		return this.ammo_clip_rockets;
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x00068D5F File Offset: 0x00066F5F
	public void TankMGReload()
	{
		if (this.GetMGAmmo() > 0)
		{
			this.MGammo_clip = 100;
			this.MGammo_backpack = 0;
		}
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x00068D79 File Offset: 0x00066F79
	public void DetonateMyC4()
	{
		this.cscl.send_detonate_my_c4();
		base.gameObject.GetComponent<Sound>().PlaySound_C4_Detonator();
		if (this.GetAmmo(1) == 0)
		{
			this.SetPrimaryWeapon(false);
		}
	}

	// Token: 0x0400095D RID: 2397
	public float m_NextAllowedFireTimeOwerride;

	// Token: 0x0400095E RID: 2398
	public const int MAX_WEAPONS = 350;

	// Token: 0x0400095F RID: 2399
	public const int MAX_ITEMS = 350;

	// Token: 0x04000960 RID: 2400
	protected vp_FPInput m_Input;

	// Token: 0x04000961 RID: 2401
	protected vp_FPCamera m_FPCamera;

	// Token: 0x04000962 RID: 2402
	protected dummy myDummy;

	// Token: 0x04000963 RID: 2403
	private Client cscl;

	// Token: 0x04000964 RID: 2404
	private Crosshair cscr;

	// Token: 0x04000965 RID: 2405
	private Map csmap;

	// Token: 0x04000966 RID: 2406
	private ParticleManager cspm;

	// Token: 0x04000967 RID: 2407
	private MainGUI csig;

	// Token: 0x04000968 RID: 2408
	private Ammo csam;

	// Token: 0x04000969 RID: 2409
	private RagDollManager csrm;

	// Token: 0x0400096A RID: 2410
	private PlayerControl cspc;

	// Token: 0x0400096B RID: 2411
	private CharacterController cscc;

	// Token: 0x0400096C RID: 2412
	private Health m_Health;

	// Token: 0x0400096D RID: 2413
	private Switch cssw;

	// Token: 0x0400096E RID: 2414
	public LayerMask LM;

	// Token: 0x0400096F RID: 2415
	private int mwid;

	// Token: 0x04000970 RID: 2416
	private int pwid;

	// Token: 0x04000971 RID: 2417
	private int swid;

	// Token: 0x04000972 RID: 2418
	private int[] ammowid;

	// Token: 0x04000973 RID: 2419
	private int[] ammocount;

	// Token: 0x04000974 RID: 2420
	public int g1wid;

	// Token: 0x04000975 RID: 2421
	public int g2wid;

	// Token: 0x04000976 RID: 2422
	private int gren1count;

	// Token: 0x04000977 RID: 2423
	private int gren2count;

	// Token: 0x04000978 RID: 2424
	private int ammo_clip;

	// Token: 0x04000979 RID: 2425
	private int ammo_fullclip;

	// Token: 0x0400097A RID: 2426
	private int ammo_backpack;

	// Token: 0x0400097B RID: 2427
	private int ammo_clip2;

	// Token: 0x0400097C RID: 2428
	public int ammo_fullclip2;

	// Token: 0x0400097D RID: 2429
	private int ammo_backpack2;

	// Token: 0x0400097E RID: 2430
	public int ammo_block;

	// Token: 0x0400097F RID: 2431
	private int ammo_medkit_w;

	// Token: 0x04000980 RID: 2432
	private int ammo_medkit_g;

	// Token: 0x04000981 RID: 2433
	private int ammo_medkit_o;

	// Token: 0x04000982 RID: 2434
	private int ammo_gp;

	// Token: 0x04000983 RID: 2435
	private int ammo_clip_rockets;

	// Token: 0x04000984 RID: 2436
	private int ammo_zbk18m;

	// Token: 0x04000985 RID: 2437
	private int ammo_zof26;

	// Token: 0x04000986 RID: 2438
	private int ammo_snaryad;

	// Token: 0x04000987 RID: 2439
	private int ammo_repair_kit;

	// Token: 0x04000988 RID: 2440
	private int ammo_module_flash;

	// Token: 0x04000989 RID: 2441
	private int ammo_module_smoke;

	// Token: 0x0400098A RID: 2442
	private int ammo_tank_light;

	// Token: 0x0400098B RID: 2443
	private int ammo_tank_heavy;

	// Token: 0x0400098C RID: 2444
	private int MGammo_clip = 100;

	// Token: 0x0400098D RID: 2445
	private int MGammo;

	// Token: 0x0400098E RID: 2446
	private int JeepMGammo;

	// Token: 0x0400098F RID: 2447
	private int MGammo_backpack;

	// Token: 0x04000990 RID: 2448
	public int firstFireCount;

	// Token: 0x04000991 RID: 2449
	public int secondFireCount;

	// Token: 0x04000992 RID: 2450
	public int awid = -1;

	// Token: 0x04000993 RID: 2451
	public byte MGitem;

	// Token: 0x04000994 RID: 2452
	public GameObject goBlockSetup;

	// Token: 0x04000995 RID: 2453
	public GameObject goBlockCrash;

	// Token: 0x04000996 RID: 2454
	public GameObject goBlockTNT;

	// Token: 0x04000997 RID: 2455
	private float clientreload2;

	// Token: 0x04000998 RID: 2456
	public bool targetLock;

	// Token: 0x04000999 RID: 2457
	public Transform target;

	// Token: 0x0400099A RID: 2458
	private Transform lastTarget;

	// Token: 0x0400099B RID: 2459
	public bool targetLocked;

	// Token: 0x0400099C RID: 2460
	private Sound _S;

	// Token: 0x0400099D RID: 2461
	private AudioSource _AS;

	// Token: 0x0400099E RID: 2462
	private Minigun MG;

	// Token: 0x0400099F RID: 2463
	private int layerMask = 1082625;

	// Token: 0x040009A0 RID: 2464
	private int ZlayerMask = 1049857;
}
