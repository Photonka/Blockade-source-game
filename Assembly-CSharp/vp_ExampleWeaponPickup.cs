using System;

// Token: 0x020000DA RID: 218
public class vp_ExampleWeaponPickup : vp_Pickup
{
	// Token: 0x060007F5 RID: 2037 RVA: 0x0007A5B8 File Offset: 0x000787B8
	protected override bool TryGive(vp_FPPlayerEventHandler player)
	{
		if (player.Dead.Active)
		{
			return false;
		}
		if (!base.TryGive(player))
		{
			return false;
		}
		player.SetWeaponByName.Try(this.InventoryName);
		if (this.AmmoIncluded > 0)
		{
			player.AddAmmo.Try(new object[]
			{
				this.InventoryName,
				this.AmmoIncluded
			});
		}
		return true;
	}

	// Token: 0x04000D7B RID: 3451
	public int AmmoIncluded;
}
