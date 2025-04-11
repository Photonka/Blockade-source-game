using System;

// Token: 0x02000038 RID: 56
public class WeaponUpgrade
{
	// Token: 0x060001D2 RID: 466 RVA: 0x0002567F File Offset: 0x0002387F
	public WeaponUpgrade(int _Val, int _Cost)
	{
		this.Val = _Val;
		if (this.Val <= 0)
		{
			this.Val = 1;
		}
		this.Cost = _Cost;
		this.is_active = true;
	}

	// Token: 0x0400018D RID: 397
	public bool is_active;

	// Token: 0x0400018E RID: 398
	public int Val;

	// Token: 0x0400018F RID: 399
	public int Cost;
}
