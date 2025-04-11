using System;
using UnityEngine;

// Token: 0x020000E4 RID: 228
[RequireComponent(typeof(vp_FPPlayerEventHandler))]
public class vp_PlayerDamageHandler : vp_DamageHandler
{
	// Token: 0x06000849 RID: 2121 RVA: 0x0007CDFE File Offset: 0x0007AFFE
	protected override void Awake()
	{
		base.Awake();
		this.m_Player = base.transform.GetComponent<vp_FPPlayerEventHandler>();
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x0007CE17 File Offset: 0x0007B017
	protected virtual void OnEnable()
	{
		if (this.m_Player != null)
		{
			this.m_Player.Register(this);
		}
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x0007CE33 File Offset: 0x0007B033
	protected virtual void OnDisable()
	{
		if (this.m_Player != null)
		{
			this.m_Player.Unregister(this);
		}
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x0007CE4F File Offset: 0x0007B04F
	public override void Damage(float damage)
	{
		if (!base.enabled)
		{
			return;
		}
		if (!vp_Utility.IsActive(base.gameObject))
		{
			return;
		}
		base.Damage(damage);
		this.m_Player.HUDDamageFlash.Send(damage);
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x0007CE88 File Offset: 0x0007B088
	public override void Die()
	{
		if (!base.enabled || !vp_Utility.IsActive(base.gameObject))
		{
			return;
		}
		if (this.DeathEffect != null)
		{
			Object.Instantiate<GameObject>(this.DeathEffect, base.transform.position, base.transform.rotation);
		}
		this.m_Player.SetWeapon.Argument = 0;
		this.m_Player.SetWeapon.Start(0f);
		this.m_Player.Dead.Start(0f);
		this.m_Player.AllowGameplayInput.Set(false);
		if (this.Respawns)
		{
			vp_Timer.In(Random.Range(this.MinRespawnTime, this.MaxRespawnTime), new vp_Timer.Callback(this.Respawn), null);
		}
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x0007CF60 File Offset: 0x0007B160
	protected override void Respawn()
	{
		if (this == null)
		{
			return;
		}
		if (Physics.CheckSphere(this.m_StartPosition + Vector3.up * this.m_RespawnOffset, this.RespawnCheckRadius, 1342177280))
		{
			this.m_RespawnOffset += 1f;
			this.Respawn();
			return;
		}
		this.m_RespawnOffset = 0f;
		this.Reactivate();
		this.Reset();
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x0007CFD4 File Offset: 0x0007B1D4
	protected override void Reset()
	{
		this.m_CurrentHealth = this.MaxHealth;
		this.m_Player.Position.Set(this.m_StartPosition);
		this.m_Player.Rotation.Set(this.m_StartRotation.eulerAngles);
		this.m_Player.Stop.Send();
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x0007D044 File Offset: 0x0007B244
	protected override void Reactivate()
	{
		this.m_Player.Dead.Stop(0f);
		this.m_Player.AllowGameplayInput.Set(true);
		this.m_Player.HUDDamageFlash.Send(0f);
		if (this.m_Audio != null)
		{
			this.m_Audio.pitch = Time.timeScale;
			this.m_Audio.PlayOneShot(this.RespawnSound, AudioListener.volume);
		}
	}

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x06000851 RID: 2129 RVA: 0x0007D0CA File Offset: 0x0007B2CA
	// (set) Token: 0x06000852 RID: 2130 RVA: 0x0007D0D2 File Offset: 0x0007B2D2
	protected virtual float OnValue_Health
	{
		get
		{
			return this.m_CurrentHealth;
		}
		set
		{
			this.m_CurrentHealth = value;
		}
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x0007D0DB File Offset: 0x0007B2DB
	private void Update()
	{
		if (this.m_Player.Dead.Active && Time.timeScale < 1f)
		{
			vp_TimeUtility.FadeTimeScale(1f, 0.05f);
		}
	}

	// Token: 0x04000DFD RID: 3581
	private vp_FPPlayerEventHandler m_Player;

	// Token: 0x04000DFE RID: 3582
	protected float m_RespawnOffset;
}
