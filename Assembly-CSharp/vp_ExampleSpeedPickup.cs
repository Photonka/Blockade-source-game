using System;

// Token: 0x020000D9 RID: 217
public class vp_ExampleSpeedPickup : vp_Pickup
{
	// Token: 0x060007F2 RID: 2034 RVA: 0x0007A524 File Offset: 0x00078724
	protected override void Update()
	{
		this.UpdateMotion();
		if (this.m_Depleted && !this.m_Audio.isPlaying)
		{
			this.Remove();
		}
	}

	// Token: 0x060007F3 RID: 2035 RVA: 0x0007A548 File Offset: 0x00078748
	protected override bool TryGive(vp_FPPlayerEventHandler player)
	{
		if (this.m_Timer.Active)
		{
			return false;
		}
		player.SetState("MegaSpeed", true, true, false);
		vp_Timer.In(this.RespawnDuration, delegate()
		{
			player.SetState("MegaSpeed", false, true, false);
		}, this.m_Timer);
		return true;
	}

	// Token: 0x04000D7A RID: 3450
	protected vp_Timer.Handle m_Timer = new vp_Timer.Handle();
}
