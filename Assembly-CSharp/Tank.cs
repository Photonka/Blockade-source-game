using System;
using UnityEngine;

// Token: 0x0200004F RID: 79
public class Tank : MonoBehaviour
{
	// Token: 0x06000272 RID: 626 RVA: 0x00032A08 File Offset: 0x00030C08
	private void Awake()
	{
		this.entmanager = (EntManager)Object.FindObjectOfType(typeof(EntManager));
		this.obj = GameObject.Find("/" + base.gameObject.name);
		if (!this.tex)
		{
			this.tex = (Resources.Load("GUI/tank_health") as Texture);
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
	}

	// Token: 0x06000273 RID: 627 RVA: 0x00032AD4 File Offset: 0x00030CD4
	private void Update()
	{
		if (this.MG == null)
		{
			this.MG = GameObject.Find(base.gameObject.name + "/root/turret/barrel/module1/TANK MG").transform;
		}
		if (this.MGFlash == null)
		{
			this.MGFlash = GameObject.Find(base.gameObject.name + "/root/turret/barrel/module1/TANK MG/flash").transform;
		}
		if (Time.time > this.FlashTime && this.MGFlash != null)
		{
			this.MGFlash.gameObject.SetActive(false);
		}
		if (this.turret == null)
		{
			this.turret = GameObject.Find(base.gameObject.name + "/root/turret").transform;
		}
		if (this.turretBone == null)
		{
			this.turretBone = GameObject.Find(base.gameObject.name + "/root/turret").transform;
		}
		if (this.gun == null)
		{
			this.gun = GameObject.Find(base.gameObject.name + "/root/turret/barrel").transform;
		}
		if (this.FP == null)
		{
			this.FP = GameObject.Find(base.gameObject.name + "/root/turret/barrel/FirePoint").transform;
		}
		if (this.particles == null)
		{
			this.particles = base.gameObject.GetComponentInChildren<ParticleSystem>();
		}
		if (!this.client)
		{
			float num = Mathf.LerpAngle(this.MyTransform.eulerAngles.x, this.t_x, Time.deltaTime * 10f);
			float num2 = Mathf.LerpAngle(this.MyTransform.eulerAngles.z, this.t_z, Time.deltaTime * 10f);
			float num3 = Mathf.LerpAngle(this.turretBone.localEulerAngles.y, this.t_ry, Time.deltaTime);
			float num4 = Mathf.LerpAngle(this.gun.localEulerAngles.x, this.g_rx, Time.deltaTime);
			this.MyTransform.eulerAngles = new Vector3(num, this.MyTransform.eulerAngles.y, num2);
			this.turretBone.localEulerAngles = new Vector3(this.turretBone.localEulerAngles.x, num3, this.turretBone.localEulerAngles.z);
			this.gun.localEulerAngles = new Vector3(num4, this.gun.localEulerAngles.y, this.gun.localEulerAngles.z);
		}
		else
		{
			if (this.tc == null)
			{
				this.tc = (TankController)Object.FindObjectOfType(typeof(TankController));
			}
			if (this.tc.Turret == null)
			{
				this.tc.Turret = this.turret;
			}
			if (this.tc.Gun == null)
			{
				this.tc.Gun = this.gun;
			}
			if (this.tc.FP == null)
			{
				this.tc.FP = this.FP;
			}
			this.t_x = this.MyTransform.eulerAngles.x;
			this.t_z = this.MyTransform.eulerAngles.z;
			this.t_ry = this.tc.Turret.localRotation.eulerAngles.y;
			this.g_rx = this.tc.Gun.localRotation.eulerAngles.x;
		}
		if (this.turret.GetComponent<AudioSource>() == null)
		{
			this.TurretAs = this.turret.gameObject.AddComponent<AudioSource>();
		}
		if (this.TurretAs == null)
		{
			this.TurretAs = this.turret.gameObject.GetComponent<AudioSource>();
		}
		this.TurretAs.maxDistance = 30f;
		this.TurretAs.spatialBlend = 1f;
		if (this.health > 69)
		{
			if (this.particles.isPlaying)
			{
				this.particles.Stop();
			}
		}
		else if (this.health < 69 && this.health > 60)
		{
			if (!this.particles.isPlaying)
			{
				this.particles.Play();
			}
			this.particles.startColor = Color.white;
			this.particles.startSize = 0.2f;
		}
		else if (this.health < 60 && this.health > 30)
		{
			if (!this.particles.isPlaying)
			{
				this.particles.Play();
			}
			this.particles.startColor = Color.gray;
			this.particles.startSize = 1f;
		}
		else if (this.health < 30)
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
								this.s.PlaySound_TankStart(this.AS);
								this.lastState = 2;
							}
							else if (this.state == 2 && this.lastState == 2)
							{
								this.s.PlaySound_TankMove(this.AS);
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
								this.s.PlaySound_TankStop(this.AS);
								this.lastState = 1;
							}
							else if (this.state == 1 && this.lastState == 1)
							{
								this.s.PlaySound_TankStand(this.AS);
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
				Vector3 vector3 = this.turretState - this.turretBone.localRotation.eulerAngles;
				this.turretState = this.turretBone.localRotation.eulerAngles;
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

	// Token: 0x06000274 RID: 628 RVA: 0x00033394 File Offset: 0x00031594
	private void OnGUI()
	{
		if (this.tc != null)
		{
			this.activeTC = this.tc.enabled;
		}
		if (this.activeTC && base.gameObject.transform.parent != null && base.gameObject.transform.parent.gameObject.name == "Player")
		{
			this.gui_style.normal.textColor = GUIManager.c[9];
			GUI.Label(new Rect(GUIManager.XRES(512f) + 2f, GUIManager.YRES(768f) - 39f + 2f, 0f, 0f), this.health.ToString(), this.gui_style);
			GUI.Label(new Rect(GUIManager.XRES(512f) + 2f - 45f - 25f - 60f - 10f, GUIManager.YRES(768f) - 39f + 2f, 0f, 0f), this.armor.ToString(), this.gui_style);
			if (this.health < 30)
			{
				this.gui_style.normal.textColor = GUIManager.c[1];
			}
			else if (this.health >= 30 && this.health <= 60)
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

	// Token: 0x06000275 RID: 629 RVA: 0x0003366E File Offset: 0x0003186E
	public void KillSelf()
	{
		if (base.gameObject == null)
		{
			return;
		}
		EntManager.DeleteEnt(this.entid);
		Object.Destroy(base.gameObject);
	}

	// Token: 0x040003D4 RID: 980
	public int id;

	// Token: 0x040003D5 RID: 981
	public int uid;

	// Token: 0x040003D6 RID: 982
	public int entid;

	// Token: 0x040003D7 RID: 983
	public int classID;

	// Token: 0x040003D8 RID: 984
	public int tank_type;

	// Token: 0x040003D9 RID: 985
	public int health = 100;

	// Token: 0x040003DA RID: 986
	public int armor;

	// Token: 0x040003DB RID: 987
	public int speed = 100;

	// Token: 0x040003DC RID: 988
	public int reload = 100;

	// Token: 0x040003DD RID: 989
	public int turretRotation = 100;

	// Token: 0x040003DE RID: 990
	public int skin_id;

	// Token: 0x040003DF RID: 991
	public float dlina = 4.4f;

	// Token: 0x040003E0 RID: 992
	public float shirina = 3f;

	// Token: 0x040003E1 RID: 993
	public Transform MG;

	// Token: 0x040003E2 RID: 994
	public Transform MGFlash;

	// Token: 0x040003E3 RID: 995
	private Transform MyTransform;

	// Token: 0x040003E4 RID: 996
	public float FlashTime;

	// Token: 0x040003E5 RID: 997
	private Client cscl;

	// Token: 0x040003E6 RID: 998
	public bool client;

	// Token: 0x040003E7 RID: 999
	private EntManager entmanager;

	// Token: 0x040003E8 RID: 1000
	private GameObject obj;

	// Token: 0x040003E9 RID: 1001
	private GUIStyle gui_style;

	// Token: 0x040003EA RID: 1002
	public Texture tex;

	// Token: 0x040003EB RID: 1003
	public Texture armor_tex;

	// Token: 0x040003EC RID: 1004
	private TankController tc;

	// Token: 0x040003ED RID: 1005
	private bool activeTC;

	// Token: 0x040003EE RID: 1006
	private Vector3 lastPos;

	// Token: 0x040003EF RID: 1007
	private Vector3 lastRot;

	// Token: 0x040003F0 RID: 1008
	private int state;

	// Token: 0x040003F1 RID: 1009
	private int lastState;

	// Token: 0x040003F2 RID: 1010
	public Sound s;

	// Token: 0x040003F3 RID: 1011
	private AudioSource AS;

	// Token: 0x040003F4 RID: 1012
	private float lastTime;

	// Token: 0x040003F5 RID: 1013
	private float replayTime = 0.04f;

	// Token: 0x040003F6 RID: 1014
	private float lastTime2;

	// Token: 0x040003F7 RID: 1015
	private float replayTime2 = 0.1f;

	// Token: 0x040003F8 RID: 1016
	private int currTurretState = 1;

	// Token: 0x040003F9 RID: 1017
	private Vector3 turretState = Vector3.zero;

	// Token: 0x040003FA RID: 1018
	private AudioSource TurretAs;

	// Token: 0x040003FB RID: 1019
	private Transform turret;

	// Token: 0x040003FC RID: 1020
	private Transform turretBone;

	// Token: 0x040003FD RID: 1021
	private Transform gun;

	// Token: 0x040003FE RID: 1022
	private Transform FP;

	// Token: 0x040003FF RID: 1023
	public float t_x;

	// Token: 0x04000400 RID: 1024
	public float t_z;

	// Token: 0x04000401 RID: 1025
	public float t_ry;

	// Token: 0x04000402 RID: 1026
	public float g_rx;

	// Token: 0x04000403 RID: 1027
	private bool initialized;

	// Token: 0x04000404 RID: 1028
	private ParticleSystem particles;
}
