using System;
using UnityEngine;

// Token: 0x0200009D RID: 157
public class vp_WeaponAnimator : vp_Component
{
	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000530 RID: 1328 RVA: 0x000646DE File Offset: 0x000628DE
	private vp_FPPlayerEventHandler Player
	{
		get
		{
			if (this.m_Player == null && this.EventHandler != null)
			{
				this.m_Player = (vp_FPPlayerEventHandler)this.EventHandler;
			}
			return this.m_Player;
		}
	}

	// Token: 0x06000531 RID: 1329 RVA: 0x00064713 File Offset: 0x00062913
	protected override void Start()
	{
		base.Start();
		this.m_Weapon = (vp_FPWeapon)base.Transform.GetComponent(typeof(vp_FPWeapon));
		this.m_Input = base.transform.root.GetComponentInChildren<vp_FPInput>();
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x00064751 File Offset: 0x00062951
	protected override void Update()
	{
		base.Update();
		this.UpdateAttack();
	}

	// Token: 0x06000533 RID: 1331 RVA: 0x00064760 File Offset: 0x00062960
	public override void Activate()
	{
		base.Activate();
		this.m_ForceAttack = false;
		this.m_ForceExit = false;
		if (!ItemsDB.CheckItem(this.m_Weapon.WeaponID))
		{
			return;
		}
		if (ItemsDB.Items[this.m_Weapon.WeaponID].Category == 20)
		{
			this.m_ForceAttack = true;
		}
	}

	// Token: 0x06000534 RID: 1332 RVA: 0x000647B8 File Offset: 0x000629B8
	private void UpdateAttack()
	{
		if (this.m_ForceExit && Time.time > this.m_NextAllowedTime)
		{
			this.m_ForceExit = false;
			this.m_Input.RestoreSetWeapon();
		}
		if (this.m_ForceAttack)
		{
			this.Player.Attack.TryStart();
		}
		if (!this.Player.Attack.Active)
		{
			return;
		}
		if (this.Player.SetWeapon.Active)
		{
			return;
		}
		if (this.m_Weapon == null)
		{
			return;
		}
		if (!this.m_Weapon.Wielded)
		{
			return;
		}
		if (Time.time < this.m_NextAllowedTime)
		{
			return;
		}
		if (this.m_ForceAttack)
		{
			this.m_ForceAttack = false;
			this.m_ForceExit = true;
		}
		if (this.m_WeaponSystem == null)
		{
			this.m_WeaponSystem = (WeaponSystem)Object.FindObjectOfType(typeof(WeaponSystem));
		}
		if (!this.m_WeaponSystem.OnWeaponMeleeAttack(this.m_Weapon))
		{
			return;
		}
		this.m_NextAllowedTime = Time.time + this.nextattack;
		this.m_Weapon.WeaponModel.GetComponent<Animation>().Stop();
		this.m_Weapon.WeaponModel.GetComponent<Animation>().Play(this.animation_name);
		if (this.soundattack)
		{
			base.GetComponent<AudioSource>().PlayOneShot(this.soundattack, AudioListener.volume);
		}
	}

	// Token: 0x04000953 RID: 2387
	protected WeaponSystem m_WeaponSystem;

	// Token: 0x04000954 RID: 2388
	protected vp_FPInput m_Input;

	// Token: 0x04000955 RID: 2389
	public string animation_name = "";

	// Token: 0x04000956 RID: 2390
	public float nextattack = 0.2f;

	// Token: 0x04000957 RID: 2391
	public AudioClip soundattack;

	// Token: 0x04000958 RID: 2392
	private vp_FPWeapon m_Weapon;

	// Token: 0x04000959 RID: 2393
	private float m_NextAllowedTime;

	// Token: 0x0400095A RID: 2394
	private bool m_ForceAttack;

	// Token: 0x0400095B RID: 2395
	private bool m_ForceExit;

	// Token: 0x0400095C RID: 2396
	private vp_FPPlayerEventHandler m_Player;
}
