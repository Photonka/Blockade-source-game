using System;
using UnityEngine;

// Token: 0x0200004B RID: 75
public class Car : MonoBehaviour
{
	// Token: 0x06000246 RID: 582 RVA: 0x0002D0F8 File Offset: 0x0002B2F8
	private void Awake()
	{
		this.entmanager = (EntManager)Object.FindObjectOfType(typeof(EntManager));
		this.obj = GameObject.Find("/" + base.gameObject.name);
		if (!this.tex)
		{
			this.tex = (Resources.Load("GUI/jeep_health") as Texture);
		}
		if (!this.armor_tex)
		{
			this.armor_tex = (Resources.Load("GUI/tank_armor") as Texture);
		}
		this.gui_style = new GUIStyle();
		this.gui_style.font = FontManager.font[0];
		this.gui_style.fontSize = 52;
		this.lastPos = Vector3.zero;
		this.MyTransform = base.transform;
		this.slots[0] = CONST.VEHICLES.POSITION_NONE;
		this.slots[1] = CONST.VEHICLES.POSITION_NONE;
		this.slots[2] = CONST.VEHICLES.POSITION_NONE;
		this.UnactiveGunner();
	}

	// Token: 0x06000247 RID: 583 RVA: 0x0002D1F1 File Offset: 0x0002B3F1
	public void UnactiveGunner()
	{
		this.Gunner.SetActive(false);
		this.GunnerCap.SetActive(false);
		this.GunnerHelmet.SetActive(false);
		this.GunnerBudge.SetActive(false);
	}

	// Token: 0x06000248 RID: 584 RVA: 0x0002D223 File Offset: 0x0002B423
	public void ActiveGunner(bool _trooper, bool _helmet, bool _cap, bool _budge)
	{
		this.Gunner.SetActive(_trooper);
		this.GunnerCap.SetActive(_cap);
		this.GunnerHelmet.SetActive(_helmet);
		this.GunnerBudge.SetActive(_budge);
	}

	// Token: 0x06000249 RID: 585 RVA: 0x0002D258 File Offset: 0x0002B458
	private void Update()
	{
		if (this.cc == null)
		{
			this.cc = (CarController)Object.FindObjectOfType(typeof(CarController));
		}
		if (this.MGFlash == null)
		{
			this.MGFlash = GameObject.Find(base.gameObject.name + "/root/turret/flash").transform;
		}
		if (Time.time > this.FlashTime && this.MGFlash != null)
		{
			this.MGFlash.gameObject.SetActive(false);
		}
		if (this.turret == null)
		{
			this.turret = GameObject.Find(base.gameObject.name + "/root/turret").transform;
		}
		if (this.particles == null)
		{
			this.particles = base.gameObject.GetComponentInChildren<ParticleSystem>();
		}
		if (this.TurretAs == null)
		{
			this.TurretAs = this.turret.GetComponent<AudioSource>();
			if (this.TurretAs == null)
			{
				this.TurretAs = this.turret.gameObject.AddComponent<AudioSource>();
			}
			this.TurretAs.maxDistance = 30f;
			this.TurretAs.spatialBlend = 1f;
		}
		if (this.cc.myPosition == CONST.VEHICLES.POSITION_JEEP_GUNNER)
		{
			float num = Mathf.LerpAngle(this.MyTransform.eulerAngles.x, this.t_x, Time.deltaTime * 10f);
			float num2 = Mathf.LerpAngle(this.MyTransform.eulerAngles.z, this.t_z, Time.deltaTime * 10f);
			this.MyTransform.eulerAngles = new Vector3(num, this.MyTransform.eulerAngles.y, num2);
			if (this.cc.Turret == null)
			{
				this.cc.Turret = this.turret;
			}
			this.t_ry = this.cc.Turret.localRotation.eulerAngles.y;
		}
		else if (this.cc.myPosition == CONST.VEHICLES.POSITION_JEEP_PASS)
		{
			float num3 = Mathf.LerpAngle(this.MyTransform.eulerAngles.x, this.t_x, Time.deltaTime * 10f);
			float num4 = Mathf.LerpAngle(this.MyTransform.eulerAngles.z, this.t_z, Time.deltaTime * 10f);
			float num5 = Mathf.LerpAngle(this.turret.localEulerAngles.y, this.t_ry, Time.deltaTime);
			this.MyTransform.eulerAngles = new Vector3(num3, this.MyTransform.eulerAngles.y, num4);
			this.turret.localEulerAngles = new Vector3(this.turret.localEulerAngles.x, num5, this.turret.localEulerAngles.z);
		}
		else
		{
			this.t_x = this.MyTransform.eulerAngles.x;
			this.t_z = this.MyTransform.eulerAngles.z;
			float num6 = Mathf.LerpAngle(this.turret.localEulerAngles.y, this.t_ry, Time.deltaTime);
			this.turret.localEulerAngles = new Vector3(this.turret.localEulerAngles.x, num6, this.turret.localEulerAngles.z);
		}
		if (this.turret.GetComponent<AudioSource>() == null)
		{
			this.TurretAs = this.turret.gameObject.AddComponent<AudioSource>();
			this.TurretAs.maxDistance = 30f;
			this.TurretAs.spatialBlend = 1f;
		}
		if (this.TurretAs == null)
		{
			this.TurretAs = this.turret.gameObject.GetComponent<AudioSource>();
			this.TurretAs.maxDistance = 30f;
			this.TurretAs.spatialBlend = 1f;
		}
		if (this.health > 40)
		{
			if (this.particles.isPlaying)
			{
				this.particles.Stop();
			}
		}
		else if (this.health < 40 && this.health > 30)
		{
			if (!this.particles.isPlaying)
			{
				this.particles.Play();
			}
			this.particles.startColor = Color.white;
			this.particles.startSize = 0.2f;
		}
		else if (this.health < 30 && this.health > 20)
		{
			if (!this.particles.isPlaying)
			{
				this.particles.Play();
			}
			this.particles.startColor = Color.gray;
			this.particles.startSize = 1f;
		}
		else if (this.health < 20)
		{
			if (!this.particles.isPlaying)
			{
				this.particles.Play();
			}
			this.particles.startSize = 3f;
			this.particles.startColor = Color.black;
		}
		else if (this.health == 0)
		{
			this.KillSelf();
		}
		if (Time.time > this.lastTime)
		{
			if (this.MyTransform.parent != null)
			{
				Vector3 vector = this.lastPos - this.MyTransform.parent.position;
				Vector3 vector2 = this.lastRot - this.MyTransform.parent.rotation.eulerAngles;
				this.lastPos = this.MyTransform.parent.position;
				this.lastRot = this.MyTransform.parent.rotation.eulerAngles;
				if (this.MyTransform.parent.GetComponent<Client>() == null)
				{
					if (this.s == null)
					{
						this.s = this.MyTransform.parent.GetComponent<Sound>();
						this.AS = this.MyTransform.parent.GetComponent<AudioSource>();
					}
					if (this.s != null)
					{
						if (vector.magnitude > 0.05f || vector2.magnitude > 0.05f)
						{
							if (this.state == 0)
							{
								this.s.PlaySound_TankEnter(this.AS);
								this.state = 1;
							}
							else if ((this.state == 1 && this.lastState == 1) || (this.state == 1 && this.lastState == 2))
							{
								this.state = 2;
							}
							else if (this.state == 2 && this.lastState == 1)
							{
								this.s.PlaySound_JeepStart(this.AS);
								this.lastState = 2;
							}
							else if (this.state == 2 && this.lastState == 2)
							{
								this.s.PlaySound_JeepMove(this.AS);
							}
							this.lastRot = this.MyTransform.rotation.eulerAngles;
						}
						else
						{
							if (this.state == 2 && this.lastState == 2)
							{
								this.state = 1;
							}
							else if (this.state == 1 && this.lastState == 2)
							{
								this.s.PlaySound_JeepStop(this.AS);
								this.lastState = 1;
							}
							else if (this.state == 1 && this.lastState == 1)
							{
								this.s.PlaySound_JeepStand(this.AS);
							}
							if (!this.AS.isPlaying)
							{
								this.lastState = 1;
								this.state = 1;
							}
						}
					}
				}
			}
			this.lastTime = Time.time + this.replayTime;
		}
		if (Time.time > this.lastTime2)
		{
			if (this.MyTransform.parent != null && this.MyTransform.parent.GetComponent<Client>() == null)
			{
				if (this.s == null)
				{
					this.s = this.MyTransform.parent.GetComponent<Sound>();
					this.AS = this.MyTransform.parent.GetComponent<AudioSource>();
				}
				Vector3 vector3 = this.turretState - this.turret.localRotation.eulerAngles;
				this.turretState = this.turret.localRotation.eulerAngles;
				if (vector3.magnitude > 0.5f)
				{
					this.s.PlaySound_TurretMove(this.TurretAs);
				}
				else if (vector3.magnitude < 0.1f)
				{
					this.s.PlaySound_TurretStart(this.TurretAs);
				}
			}
			this.lastTime2 = Time.time + this.replayTime2;
		}
	}

	// Token: 0x0600024A RID: 586 RVA: 0x0002DAF4 File Offset: 0x0002BCF4
	private void OnGUI()
	{
		if (this.cc != null)
		{
			this.activeCC = this.cc.enabled;
		}
		if (this.activeCC)
		{
			this.gui_style.normal.textColor = GUIManager.c[9];
			GUI.Label(new Rect(GUIManager.XRES(512f) + 2f, GUIManager.YRES(768f) - 39f + 2f, 0f, 0f), this.health.ToString(), this.gui_style);
			GUI.Label(new Rect(GUIManager.XRES(512f) + 2f - 45f - 25f - 60f - 10f, GUIManager.YRES(768f) - 39f + 2f, 0f, 0f), this.armor.ToString(), this.gui_style);
			if (this.health < 20)
			{
				this.gui_style.normal.textColor = GUIManager.c[1];
			}
			else if (this.health >= 20 && this.health <= 35)
			{
				this.gui_style.normal.textColor = GUIManager.c[3];
			}
			else
			{
				this.gui_style.normal.textColor = GUIManager.c[8];
			}
			GUI.Label(new Rect(GUIManager.XRES(512f), GUIManager.YRES(768f) - 39f, 0f, 0f), this.health.ToString(), this.gui_style);
			GUI.Label(new Rect(GUIManager.XRES(512f) - 45f - 25f - 60f - 10f, GUIManager.YRES(768f) - 39f, 0f, 0f), this.armor.ToString(), this.gui_style);
			GUI.DrawTexture(new Rect(GUIManager.XRES(512f) - 40f, GUIManager.YRES(768f) - 41f, 32f, 32f), this.tex);
			GUI.DrawTexture(new Rect(GUIManager.XRES(512f) - 40f - 10f - 60f - 60f - 10f, GUIManager.YRES(768f) - 41f, 32f, 32f), this.armor_tex);
		}
	}

	// Token: 0x0600024B RID: 587 RVA: 0x0002DD8C File Offset: 0x0002BF8C
	public void CheckSlots(int plid)
	{
		if (this.slots[CONST.VEHICLES.POSITION_JEEP_DRIVER] == plid)
		{
			this.slots[CONST.VEHICLES.POSITION_JEEP_DRIVER] = CONST.VEHICLES.POSITION_NONE;
		}
		if (this.slots[CONST.VEHICLES.POSITION_JEEP_GUNNER] == plid)
		{
			this.slots[CONST.VEHICLES.POSITION_JEEP_GUNNER] = CONST.VEHICLES.POSITION_NONE;
		}
		if (this.slots[CONST.VEHICLES.POSITION_JEEP_PASS] == plid)
		{
			this.slots[CONST.VEHICLES.POSITION_JEEP_PASS] = CONST.VEHICLES.POSITION_NONE;
		}
	}

	// Token: 0x0600024C RID: 588 RVA: 0x0002DDF9 File Offset: 0x0002BFF9
	public void KillSelf()
	{
		if (base.gameObject == null)
		{
			return;
		}
		EntManager.DeleteEnt(this.entid);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x040002D6 RID: 726
	public int id;

	// Token: 0x040002D7 RID: 727
	public int uid;

	// Token: 0x040002D8 RID: 728
	public int entid;

	// Token: 0x040002D9 RID: 729
	public int classID;

	// Token: 0x040002DA RID: 730
	public int tank_type;

	// Token: 0x040002DB RID: 731
	public int health = 100;

	// Token: 0x040002DC RID: 732
	public int armor;

	// Token: 0x040002DD RID: 733
	public int speed = 100;

	// Token: 0x040002DE RID: 734
	public int reload = 100;

	// Token: 0x040002DF RID: 735
	public int turretRotation = 100;

	// Token: 0x040002E0 RID: 736
	public int skin_id;

	// Token: 0x040002E1 RID: 737
	public float dlina = 4f;

	// Token: 0x040002E2 RID: 738
	public float shirina = 3f;

	// Token: 0x040002E3 RID: 739
	public Transform MGFlash;

	// Token: 0x040002E4 RID: 740
	private Transform MyTransform;

	// Token: 0x040002E5 RID: 741
	public float FlashTime;

	// Token: 0x040002E6 RID: 742
	private Client cscl;

	// Token: 0x040002E7 RID: 743
	public bool gunner;

	// Token: 0x040002E8 RID: 744
	private EntManager entmanager;

	// Token: 0x040002E9 RID: 745
	private GameObject obj;

	// Token: 0x040002EA RID: 746
	private GUIStyle gui_style;

	// Token: 0x040002EB RID: 747
	public Texture tex;

	// Token: 0x040002EC RID: 748
	public Texture armor_tex;

	// Token: 0x040002ED RID: 749
	private CarController cc;

	// Token: 0x040002EE RID: 750
	private bool activeCC;

	// Token: 0x040002EF RID: 751
	private Vector3 lastPos;

	// Token: 0x040002F0 RID: 752
	private Vector3 lastRot;

	// Token: 0x040002F1 RID: 753
	private int state;

	// Token: 0x040002F2 RID: 754
	private int lastState;

	// Token: 0x040002F3 RID: 755
	public Sound s;

	// Token: 0x040002F4 RID: 756
	private AudioSource AS;

	// Token: 0x040002F5 RID: 757
	private float lastTime;

	// Token: 0x040002F6 RID: 758
	private float replayTime = 0.04f;

	// Token: 0x040002F7 RID: 759
	private float lastTime2;

	// Token: 0x040002F8 RID: 760
	private float replayTime2 = 0.1f;

	// Token: 0x040002F9 RID: 761
	private int currTurretState = 1;

	// Token: 0x040002FA RID: 762
	private Vector3 turretState = Vector3.zero;

	// Token: 0x040002FB RID: 763
	private AudioSource TurretAs;

	// Token: 0x040002FC RID: 764
	public Transform turret;

	// Token: 0x040002FD RID: 765
	public Transform JeepMesh;

	// Token: 0x040002FE RID: 766
	public Transform firePoint;

	// Token: 0x040002FF RID: 767
	public float t_x;

	// Token: 0x04000300 RID: 768
	public float t_z;

	// Token: 0x04000301 RID: 769
	public float t_ry;

	// Token: 0x04000302 RID: 770
	public float g_rx;

	// Token: 0x04000303 RID: 771
	public int team;

	// Token: 0x04000304 RID: 772
	private bool initialized;

	// Token: 0x04000305 RID: 773
	private ParticleSystem particles;

	// Token: 0x04000306 RID: 774
	public int[] slots = new int[3];

	// Token: 0x04000307 RID: 775
	public GameObject GunnerPos;

	// Token: 0x04000308 RID: 776
	public GameObject Gunner;

	// Token: 0x04000309 RID: 777
	public GameObject GunnerCap;

	// Token: 0x0400030A RID: 778
	public GameObject GunnerHelmet;

	// Token: 0x0400030B RID: 779
	public GameObject GunnerBudge;
}
