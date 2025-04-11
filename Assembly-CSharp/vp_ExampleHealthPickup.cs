using System;
using UnityEngine;

// Token: 0x020000D7 RID: 215
public class vp_ExampleHealthPickup : vp_Pickup
{
	// Token: 0x060007ED RID: 2029 RVA: 0x0007A3D8 File Offset: 0x000785D8
	protected override bool TryGive(vp_FPPlayerEventHandler player)
	{
		if (player.Health.Get() < 0f)
		{
			return false;
		}
		if (player.Health.Get() >= 1f)
		{
			return false;
		}
		player.Health.Set(Mathf.Min(1f, player.Health.Get() + this.Health));
		return true;
	}

	// Token: 0x04000D78 RID: 3448
	public float Health = 0.1f;
}
