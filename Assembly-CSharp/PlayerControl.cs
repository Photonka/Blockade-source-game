using System;
using UnityEngine;

// Token: 0x02000129 RID: 297
public class PlayerControl : MonoBehaviour
{
	// Token: 0x06000A52 RID: 2642 RVA: 0x00087B34 File Offset: 0x00085D34
	private void Awake()
	{
		this.Map = GameObject.Find("Map");
		this.Gui = GameObject.Find("GUI");
		this.SetSpectator();
		this.cscl = (Client)Object.FindObjectOfType(typeof(Client));
		this.DeathSound = ContentLoader.LoadSound("death");
		this.HitSound = ContentLoader.LoadSound("hit");
		this.TraceHitSound = ContentLoader.LoadSound("tracehit");
		this.Headshot = (Resources.Load("Sound/Award/headshot") as AudioClip);
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x00087BC6 File Offset: 0x00085DC6
	private void Update()
	{
		if (this.freeze)
		{
			base.gameObject.transform.position = new Vector3(128f, 32f, 128f);
		}
	}

	// Token: 0x06000A54 RID: 2644 RVA: 0x00087BF4 File Offset: 0x00085DF4
	public void SetSpectator()
	{
		this.freeze = true;
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x00087BFD File Offset: 0x00085DFD
	public void UnSetSpectator()
	{
		this.freeze = false;
	}

	// Token: 0x06000A56 RID: 2646 RVA: 0x00087C06 File Offset: 0x00085E06
	public bool isSpectator()
	{
		return this.freeze;
	}

	// Token: 0x06000A57 RID: 2647 RVA: 0x00087C0E File Offset: 0x00085E0E
	public void Spawn(int x, int y, int z)
	{
		base.gameObject.transform.position = new Vector3((float)x, (float)y, (float)z);
		this.UnSetSpectator();
		this.Gui.GetComponent<MainGUI>().CloseAll();
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x00087C41 File Offset: 0x00085E41
	public int GetTeam()
	{
		return (int)BotsController.Instance.Bots[this.cscl.myindex].Team;
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x00087C5E File Offset: 0x00085E5E
	public void StartMap(string mapname)
	{
		MonoBehaviour.print("STARTMAP: " + mapname);
		PlayerControl.mapid = mapname;
		this.Map.GetComponent<ZipLoader>().WebLoadMap(mapname);
	}

	// Token: 0x06000A5A RID: 2650 RVA: 0x00087C87 File Offset: 0x00085E87
	public void SetHit()
	{
		base.GetComponent<WeaponSystem>().GetComponent<AudioSource>().PlayOneShot(this.HitSound, AudioListener.volume);
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x00087CA4 File Offset: 0x00085EA4
	public void SetTraceHit()
	{
		base.GetComponent<WeaponSystem>().GetComponent<AudioSource>().PlayOneShot(this.TraceHitSound, AudioListener.volume);
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x00087CC1 File Offset: 0x00085EC1
	public static void SetGameMode(int _value)
	{
		PlayerControl.gamemode = _value;
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x00087CC9 File Offset: 0x00085EC9
	public static int GetGameMode()
	{
		return PlayerControl.gamemode;
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x00087CD0 File Offset: 0x00085ED0
	public static string GetMapID()
	{
		return PlayerControl.mapid;
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x00087CD7 File Offset: 0x00085ED7
	public void SetSky(int _value, bool _prazd = false)
	{
		if (_value == 3 || _value == 7 || _value == 10)
		{
			this.Map.GetComponent<Sky>().SetSky(1, _prazd);
			return;
		}
		this.Map.GetComponent<Sky>().SetSky(0, _prazd);
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x00087D0B File Offset: 0x00085F0B
	public void AwardHeadshot()
	{
		base.GetComponent<WeaponSystem>().GetComponent<AudioSource>().PlayOneShot(this.Headshot, AudioListener.volume);
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x00087D28 File Offset: 0x00085F28
	public void SetPrivateServer(int flag)
	{
		PlayerControl.privateserver = 1;
		PlayerControl.privateadmin = flag;
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x00087D36 File Offset: 0x00085F36
	public int isPrivateServer()
	{
		return PlayerControl.privateserver;
	}

	// Token: 0x06000A63 RID: 2659 RVA: 0x00087D3D File Offset: 0x00085F3D
	public static int isPrivateAdmin()
	{
		return PlayerControl.privateadmin;
	}

	// Token: 0x04000FA5 RID: 4005
	private bool freeze;

	// Token: 0x04000FA6 RID: 4006
	private Client cscl;

	// Token: 0x04000FA7 RID: 4007
	private GameObject Gui;

	// Token: 0x04000FA8 RID: 4008
	private GameObject MainCamera;

	// Token: 0x04000FA9 RID: 4009
	private GameObject WeaponCamera;

	// Token: 0x04000FAA RID: 4010
	private GameObject Map;

	// Token: 0x04000FAB RID: 4011
	private static int gamemode = -1;

	// Token: 0x04000FAC RID: 4012
	private static string mapid;

	// Token: 0x04000FAD RID: 4013
	private static int privateserver = 0;

	// Token: 0x04000FAE RID: 4014
	private static int privateadmin = 0;

	// Token: 0x04000FAF RID: 4015
	private AudioClip DeathSound;

	// Token: 0x04000FB0 RID: 4016
	private AudioClip HitSound;

	// Token: 0x04000FB1 RID: 4017
	private AudioClip TraceHitSound;

	// Token: 0x04000FB2 RID: 4018
	private AudioClip Headshot;

	// Token: 0x04000FB3 RID: 4019
	private GameObject FollowCam;
}
