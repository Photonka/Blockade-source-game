using System;
using UnityEngine;

// Token: 0x020000D3 RID: 211
[RequireComponent(typeof(vp_FPWeapon))]
public class vp_FPWeaponReloader : MonoBehaviour
{
	// Token: 0x060007BF RID: 1983 RVA: 0x0007818F File Offset: 0x0007638F
	protected virtual void Awake()
	{
		this.m_Audio = base.GetComponent<AudioSource>();
		this.m_Player = (vp_FPPlayerEventHandler)base.transform.root.GetComponentInChildren(typeof(vp_FPPlayerEventHandler));
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x000781C2 File Offset: 0x000763C2
	protected virtual void Start()
	{
		this.m_Weapon = base.transform.GetComponent<vp_FPWeapon>();
		this.ReloadTex2 = GUIManager.tex_yellow;
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x000781E0 File Offset: 0x000763E0
	protected virtual void OnEnable()
	{
		if (this.m_Player != null)
		{
			this.m_Player.Register(this);
		}
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x000781FC File Offset: 0x000763FC
	protected virtual void OnDisable()
	{
		if (this.m_Player != null)
		{
			this.m_Player.Unregister(this);
		}
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x00078218 File Offset: 0x00076418
	protected virtual bool CanStart_Reload()
	{
		if (!this.m_Player.CurrentWeaponWielded.Get())
		{
			return false;
		}
		if (this.m_WeaponSystem == null)
		{
			this.m_WeaponSystem = (WeaponSystem)Object.FindObjectOfType(typeof(WeaponSystem));
		}
		if (this.m_PlayerControl == null)
		{
			this.m_PlayerControl = (PlayerControl)Object.FindObjectOfType(typeof(PlayerControl));
		}
		if (!this.m_WeaponSystem.OnWeaponReloadStart(this.m_Weapon))
		{
			return false;
		}
		this.ReloadStart = Time.time;
		int team = this.m_PlayerControl.GetTeam();
		if (team == 0)
		{
			this.ReloadTex = GUIManager.tex_blue;
		}
		else if (team == 1)
		{
			this.ReloadTex = GUIManager.tex_red;
		}
		else if (team == 2)
		{
			this.ReloadTex = GUIManager.tex_green;
		}
		else if (team == 3)
		{
			this.ReloadTex = GUIManager.tex_yellow;
		}
		this.m_Player.Zoom.TryStop();
		return true;
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x00078310 File Offset: 0x00076510
	protected virtual void OnStart_Reload()
	{
		if (this.sound == null)
		{
			this.sound = (Sound)Object.FindObjectOfType(typeof(Sound));
		}
		this.m_Player.Reload.AutoDuration = this.OnValue_CurrentWeaponReloadDuration;
		if (this.m_Player.Reload.AutoDuration == 0f && this.AnimationReload != null)
		{
			this.m_Player.Reload.AutoDuration = this.AnimationReload.length;
		}
		this.m_WeaponSystem.m_NextAllowedFireTimeOwerride = Time.time + 1.5f;
		if (this.AnimationReload != null)
		{
			this.m_Weapon.WeaponModel.GetComponent<Animation>().CrossFade(this.AnimationReload.name);
		}
		if (this.m_Audio != null)
		{
			if (this.SoundReload == null)
			{
				this.SoundReload = this.sound.GetReload();
			}
			this.m_Audio.pitch = Time.timeScale;
			this.m_Audio.PlayOneShot(this.SoundReload, AudioListener.volume);
		}
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x00078434 File Offset: 0x00076634
	protected virtual void OnStop_Reload()
	{
		this.m_Player.CurrentWeaponName.Get();
		if (this.m_WeaponSystem == null)
		{
			this.m_WeaponSystem = (WeaponSystem)Object.FindObjectOfType(typeof(WeaponSystem));
		}
		this.m_WeaponSystem.OnWeaponReloadEnd(this.m_Weapon);
	}

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x060007C6 RID: 1990 RVA: 0x00078490 File Offset: 0x00076690
	protected virtual float OnValue_CurrentWeaponReloadDuration
	{
		get
		{
			return this.ReloadDuration;
		}
	}

	// Token: 0x060007C7 RID: 1991 RVA: 0x00078498 File Offset: 0x00076698
	private void OnGUI()
	{
		if (this.m_Weapon.WeaponID == 122)
		{
			return;
		}
		this.GUIDrawReload();
		this.GUIDrawReload2();
	}

	// Token: 0x060007C8 RID: 1992 RVA: 0x000784B8 File Offset: 0x000766B8
	private void GUIDrawReload()
	{
		float num = this.ReloadStart + this.ReloadDuration - Time.time;
		if (num < 0f)
		{
			return;
		}
		float num2 = 1f - num / this.ReloadDuration;
		GUI.DrawTexture(new Rect((float)Screen.width / 2f - 66f, (float)Screen.height * 0.75f - 2f, 132f, 12f), GUIManager.tex_black);
		if (this.ReloadTex)
		{
			GUI.DrawTexture(new Rect((float)Screen.width / 2f - 64f, (float)Screen.height * 0.75f, 128f * num2, 8f), this.ReloadTex);
		}
	}

	// Token: 0x060007C9 RID: 1993 RVA: 0x00078578 File Offset: 0x00076778
	private void GUIDrawReload2()
	{
		if (this.ReloadStart2 == 0f)
		{
			return;
		}
		float num = this.ReloadStart2 + this.ReloadDuration2 - Time.time;
		if (num < 0f && this.ReloadStart2 > 0f)
		{
			this.m_Player.Zoom.NextAllowedStopTime = Time.time - 0.1f;
			this.m_Player.Zoom.TryStop();
			this.ReloadStart2 = 0f;
			return;
		}
		float num2 = 1f - num / this.ReloadDuration2;
		GUI.DrawTexture(new Rect((float)Screen.width / 2f - 66f, (float)Screen.height * 0.75f - 2f + 14f, 132f, 12f), GUIManager.tex_black);
		if (this.ReloadTex2)
		{
			GUI.DrawTexture(new Rect((float)Screen.width / 2f - 64f, (float)Screen.height * 0.75f + 14f, 128f * num2, 8f), this.ReloadTex2);
		}
	}

	// Token: 0x04000D32 RID: 3378
	protected WeaponSystem m_WeaponSystem;

	// Token: 0x04000D33 RID: 3379
	protected PlayerControl m_PlayerControl;

	// Token: 0x04000D34 RID: 3380
	protected vp_FPWeapon m_Weapon;

	// Token: 0x04000D35 RID: 3381
	protected vp_FPPlayerEventHandler m_Player;

	// Token: 0x04000D36 RID: 3382
	protected AudioSource m_Audio;

	// Token: 0x04000D37 RID: 3383
	public AudioClip SoundReload;

	// Token: 0x04000D38 RID: 3384
	public AnimationClip AnimationReload;

	// Token: 0x04000D39 RID: 3385
	public float ReloadDuration = 2f;

	// Token: 0x04000D3A RID: 3386
	private float ReloadStart;

	// Token: 0x04000D3B RID: 3387
	private Texture2D ReloadTex;

	// Token: 0x04000D3C RID: 3388
	public float ReloadDuration2 = 1f;

	// Token: 0x04000D3D RID: 3389
	public float ReloadStart2;

	// Token: 0x04000D3E RID: 3390
	private Texture2D ReloadTex2;

	// Token: 0x04000D3F RID: 3391
	private Sound sound;
}
