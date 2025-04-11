using System;
using UnityEngine;

// Token: 0x020000D8 RID: 216
public class vp_ExampleSlomoPickup : vp_Pickup
{
	// Token: 0x060007EF RID: 2031 RVA: 0x0007A45C File Offset: 0x0007865C
	protected override void Update()
	{
		this.UpdateMotion();
		if (this.m_Depleted)
		{
			if (this.m_Player != null && this.m_Player.Dead.Active && !this.m_RespawnTimer.Active)
			{
				this.Respawn();
				return;
			}
			if (Time.timeScale > 0.2f && !vp_TimeUtility.Paused)
			{
				vp_TimeUtility.FadeTimeScale(0.2f, 0.1f);
				return;
			}
			if (!this.m_Audio.isPlaying)
			{
				this.Remove();
				return;
			}
		}
		else if (Time.timeScale < 1f && !vp_TimeUtility.Paused)
		{
			vp_TimeUtility.FadeTimeScale(1f, 0.05f);
		}
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x0007A504 File Offset: 0x00078704
	protected override bool TryGive(vp_FPPlayerEventHandler player)
	{
		this.m_Player = player;
		return !this.m_Depleted && Time.timeScale == 1f;
	}

	// Token: 0x04000D79 RID: 3449
	private vp_FPPlayerEventHandler m_Player;
}
