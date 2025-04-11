using System;

// Token: 0x020000D6 RID: 214
public class vp_ExampleAmmoPickup : vp_Pickup
{
	// Token: 0x060007EA RID: 2026 RVA: 0x0007A342 File Offset: 0x00078542
	protected override bool TryGive(vp_FPPlayerEventHandler player)
	{
		if (player.Dead.Active)
		{
			return false;
		}
		if (base.TryGive(player))
		{
			this.TryReloadIfEmpty(player);
			return true;
		}
		if (this.TryReloadIfEmpty(player))
		{
			base.TryGive(player);
			return true;
		}
		return false;
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x0007A37C File Offset: 0x0007857C
	private bool TryReloadIfEmpty(vp_FPPlayerEventHandler player)
	{
		return player.CurrentWeaponAmmoCount.Get() <= 0 && !(player.CurrentWeaponClipType.Get() != this.InventoryName) && player.Reload.TryStart();
	}
}
