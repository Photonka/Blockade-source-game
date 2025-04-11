using System;
using UnityEngine;

// Token: 0x020000C8 RID: 200
public sealed class vp_Layer
{
	// Token: 0x060006B4 RID: 1716 RVA: 0x0007034C File Offset: 0x0006E54C
	static vp_Layer()
	{
		Physics.IgnoreLayerCollision(30, 29);
		Physics.IgnoreLayerCollision(29, 29);
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x00023EF4 File Offset: 0x000220F4
	private vp_Layer()
	{
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x0007036C File Offset: 0x0006E56C
	public static void Set(GameObject obj, int layer, bool recursive = false)
	{
		if (layer < 0 || layer > 31)
		{
			Debug.LogError("vp_Layer: Attempted to set layer id out of range [0-31].");
			return;
		}
		obj.layer = layer;
		if (recursive)
		{
			foreach (object obj2 in obj.transform)
			{
				vp_Layer.Set(((Transform)obj2).gameObject, layer, true);
			}
		}
	}

	// Token: 0x04000BD6 RID: 3030
	public static readonly vp_Layer instance = new vp_Layer();

	// Token: 0x04000BD7 RID: 3031
	public const int Default = 0;

	// Token: 0x04000BD8 RID: 3032
	public const int TransparentFX = 1;

	// Token: 0x04000BD9 RID: 3033
	public const int IgnoreRaycast = 2;

	// Token: 0x04000BDA RID: 3034
	public const int Water = 4;

	// Token: 0x04000BDB RID: 3035
	public const int Enemy = 25;

	// Token: 0x04000BDC RID: 3036
	public const int Pickup = 26;

	// Token: 0x04000BDD RID: 3037
	public const int Trigger = 27;

	// Token: 0x04000BDE RID: 3038
	public const int MovableObject = 28;

	// Token: 0x04000BDF RID: 3039
	public const int Debris = 29;

	// Token: 0x04000BE0 RID: 3040
	public const int LocalPlayer = 30;

	// Token: 0x04000BE1 RID: 3041
	public const int Weapon = 31;

	// Token: 0x04000BE2 RID: 3042
	public const int Players = 10;

	// Token: 0x02000892 RID: 2194
	public static class Mask
	{
		// Token: 0x04003297 RID: 12951
		public const int BulletBlockers = -1811939349;

		// Token: 0x04003298 RID: 12952
		public const int ExternalBlockers = -1744831509;

		// Token: 0x04003299 RID: 12953
		public const int PhysicsBlockers = 1342177280;

		// Token: 0x0400329A RID: 12954
		public const int IgnoreWalkThru = -738197525;
	}
}
