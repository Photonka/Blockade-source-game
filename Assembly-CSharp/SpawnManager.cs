using System;
using UnityEngine;

// Token: 0x02000045 RID: 69
public class SpawnManager : MonoBehaviour
{
	// Token: 0x0600021B RID: 539 RVA: 0x0002C18C File Offset: 0x0002A38C
	private void Awake()
	{
		this.goGUI = GameObject.Find("GUI");
		this.csb = (Batch)Object.FindObjectOfType(typeof(Batch));
		this.csr = this.goGUI.GetComponent<Radar>();
		this.cspc = (PlayerControl)Object.FindObjectOfType(typeof(PlayerControl));
		this.gui_style = new GUIStyle();
		this.gui_style.font = FontManager.font[0];
		this.gui_style.fontSize = 20;
		this.gui_style.normal.textColor = new Color(255f, 255f, 255f, 255f);
		this.gui_style.alignment = 4;
	}

	// Token: 0x0600021C RID: 540 RVA: 0x0002C24D File Offset: 0x0002A44D
	public void PreSpawn()
	{
		this.goGUI.GetComponent<Radar>().ForceUpdateRadar();
		this.goGUI.GetComponent<MainGUI>().OpenSelectTeam();
		this.goGUI.GetComponent<LoadScreen>().enabled = false;
		GM.currMainState = GAME_STATES.GAME;
	}

	// Token: 0x0600021D RID: 541 RVA: 0x0002C288 File Offset: 0x0002A488
	public void Spawn(float x, float y, float z)
	{
		if (this.LocalPlayer == null)
		{
			this.LocalPlayer = GameObject.Find("Player");
		}
		if (this.LocalPlayer.GetComponent<TankController>() != null || this.LocalPlayer.GetComponent<CarController>() != null)
		{
			this.LocalPlayer.GetComponent<TankController>().currTank = null;
			this.LocalPlayer.GetComponent<TankController>().enabled = false;
			this.LocalPlayer.GetComponent<CarController>().currCar = null;
			this.LocalPlayer.GetComponent<CarController>().enabled = false;
			this.LocalPlayer.GetComponentInChildren<OrbitCam>().enabled = false;
			this.LocalPlayer.GetComponent<TransportExit>().enabled = false;
			this.LocalPlayer.GetComponent<TransportExit>().vehicleType = CONST.VEHICLES.NONE;
		}
		this.LocalPlayer.GetComponent<vp_FPController>().enabled = true;
		this.LocalPlayer.GetComponent<vp_FPInput>().enabled = true;
		this.LocalPlayer.GetComponent<vp_FPWeaponHandler>().enabled = true;
		this.LocalPlayer.GetComponent<TransportDetect>().enabled = true;
		this.LocalPlayer.GetComponentInChildren<vp_FPCamera>().enabled = true;
		GameObject.Find("WeaponCamera").GetComponent<Camera>().enabled = true;
		((Crosshair)Object.FindObjectOfType(typeof(Crosshair))).SetActive(true);
		this.goCamera = null;
		this.DeathPos = Vector3.zero;
		this.msgshow = false;
		this.last_follow_player_index = -1;
		if (this.csws == null)
		{
			this.csws = (WeaponSystem)Object.FindObjectOfType(typeof(WeaponSystem));
		}
		this.csws.SetPrimaryWeapon(false);
		this.csb.Combine();
		if (this.m_Controller == null)
		{
			this.m_Controller = (vp_FPController)Object.FindObjectOfType(typeof(vp_FPController));
		}
		if (this.m_Controller)
		{
			this.m_Controller.ClearPos(x, y, z);
		}
	}

	// Token: 0x0600021E RID: 542 RVA: 0x0002C47C File Offset: 0x0002A67C
	public void SpawnCamera(GameObject head)
	{
		if (this.csws == null)
		{
			this.csws = (WeaponSystem)Object.FindObjectOfType(typeof(WeaponSystem));
		}
		this.csws.HideWeapons(true);
		this.goCamera = head;
		if (this.LocalPlayer == null)
		{
			this.LocalPlayer = GameObject.Find("Player");
		}
		if (this.LocalPlayer != null)
		{
			this.LocalPlayer.GetComponent<Sound>().PlaySound_Death();
		}
	}

	// Token: 0x0600021F RID: 543 RVA: 0x0002C500 File Offset: 0x0002A700
	private void LateUpdate()
	{
		if (this.last_follow_player_index > -1 && (!BotsController.Instance.Bots[this.last_follow_player_index].Active || BotsController.Instance.Bots[this.last_follow_player_index].Dead > 0))
		{
			this.SetRandomFollow(this._myindex, this.last_follow_player_index);
		}
		if (this.goCamera)
		{
			if (this.csws == null)
			{
				this.csws = (WeaponSystem)Object.FindObjectOfType(typeof(WeaponSystem));
			}
			if (this.csws == null)
			{
				return;
			}
			Camera.main.transform.position = this.goCamera.transform.position;
			Camera.main.transform.rotation = this.goCamera.transform.rotation;
			this.csws.HideWeapons(true);
			this.DeathPos = this.goCamera.transform.position;
			if (Input.GetKeyDown(32))
			{
				this.SetRandomFollow(this._myindex, this.last_follow_player_index);
			}
		}
		if (this.DeathPos != Vector3.zero)
		{
			if (this.csws == null)
			{
				this.csws = (WeaponSystem)Object.FindObjectOfType(typeof(WeaponSystem));
			}
			if (this.csws == null)
			{
				return;
			}
			Camera.main.transform.position = this.DeathPos;
			this.csws.HideWeapons(true);
		}
	}

	// Token: 0x06000220 RID: 544 RVA: 0x0002C688 File Offset: 0x0002A888
	private void Update()
	{
		if (this.waiting_for_respawn && Input.GetMouseButtonDown(0))
		{
			if (this.cl == null)
			{
				this.cl = Object.FindObjectOfType<Client>();
			}
			if (this.cl == null)
			{
				return;
			}
			this.cl.send_spawn_me();
			this.waiting_for_respawn = false;
		}
	}

	// Token: 0x06000221 RID: 545 RVA: 0x0002C6E0 File Offset: 0x0002A8E0
	private void OnGUI()
	{
		if (BotsController.Instance != null && this.last_follow_player_index >= 0)
		{
			string text = BotsController.Instance.Bots[this.last_follow_player_index].Name;
			if (BotsController.Instance.Bots[this.last_follow_player_index].ClanName.Length > 0)
			{
				text = string.Concat(new string[]
				{
					text,
					"\n",
					Lang.GetLabel(10),
					": ",
					BotsController.Instance.Bots[this.last_follow_player_index].ClanName
				});
			}
			this.gui_style.normal.textColor = GUIManager.c[9];
			GUI.Label(new Rect(2f, 22f, (float)(Screen.width - 2), 50f), text, this.gui_style);
			this.gui_style.normal.textColor = GUIManager.c[8];
			GUI.Label(new Rect(0f, 20f, (float)Screen.width, 50f), text, this.gui_style);
		}
		if (!this.msgshow)
		{
			return;
		}
		GUI.depth = -1;
		GUIManager.DrawText(this.rSpectatormsg, this.spectatormsg, 20, 4, 8);
	}

	// Token: 0x06000222 RID: 546 RVA: 0x0002C82C File Offset: 0x0002AA2C
	public void SetRandomFollow(int myindex, int _last_index)
	{
		this._myindex = myindex;
		if (BotsController.Instance == null)
		{
			return;
		}
		for (int i = 0; i < 32; i++)
		{
			if (BotsController.Instance.Bots[i].Active && BotsController.Instance.Bots[i].Dead <= 0 && (BotsController.Instance.Bots[i].Team == BotsController.Instance.Bots[myindex].Team || BotsController.Instance.Bots[myindex].Team == 255) && i > _last_index && i != myindex)
			{
				this.SetFollow(i);
				return;
			}
		}
		this.goCamera = GameObject.Find("CamPos");
		this.last_follow_player_index = -1;
	}

	// Token: 0x06000223 RID: 547 RVA: 0x0002C8E5 File Offset: 0x0002AAE5
	public void SetFollow(int index)
	{
		this.goCamera = BotsController.Instance.Bots[index].SpecView;
		this.last_follow_player_index = index;
	}

	// Token: 0x06000224 RID: 548 RVA: 0x0002C905 File Offset: 0x0002AB05
	public void SetSpectatorMsg(string msg)
	{
		this.spectatormsg = msg;
		this.rSpectatormsg = new Rect(0f, (float)Screen.height / 6f, (float)Screen.width, 32f);
		this.msgshow = true;
	}

	// Token: 0x040002B9 RID: 697
	private GameObject goGUI;

	// Token: 0x040002BA RID: 698
	private WeaponSystem csws;

	// Token: 0x040002BB RID: 699
	private Batch csb;

	// Token: 0x040002BC RID: 700
	private Radar csr;

	// Token: 0x040002BD RID: 701
	private Client cl;

	// Token: 0x040002BE RID: 702
	private PlayerControl cspc;

	// Token: 0x040002BF RID: 703
	private int last_follow_player_index = -1;

	// Token: 0x040002C0 RID: 704
	private int _myindex = -1;

	// Token: 0x040002C1 RID: 705
	private Vector3 DeathPos = Vector3.zero;

	// Token: 0x040002C2 RID: 706
	private GameObject goCamera;

	// Token: 0x040002C3 RID: 707
	private GameObject LocalPlayer;

	// Token: 0x040002C4 RID: 708
	private vp_FPController m_Controller;

	// Token: 0x040002C5 RID: 709
	private bool msgshow;

	// Token: 0x040002C6 RID: 710
	private string spectatormsg = "";

	// Token: 0x040002C7 RID: 711
	private Rect rSpectatormsg;

	// Token: 0x040002C8 RID: 712
	private GUIStyle gui_style;

	// Token: 0x040002C9 RID: 713
	public bool waiting_for_respawn;
}
