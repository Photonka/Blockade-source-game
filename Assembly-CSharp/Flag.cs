using System;
using UnityEngine;

// Token: 0x02000112 RID: 274
public class Flag : MonoBehaviour
{
	// Token: 0x060009F1 RID: 2545 RVA: 0x00083834 File Offset: 0x00081A34
	public void SetFlag(Vector3i _pos, int _t1, int _t2)
	{
		this.map = Object.FindObjectOfType<Map>();
		if (this.map == null)
		{
			return;
		}
		this.pos = _pos;
		for (int i = this.pos.y; i <= this.pos.y + this.maxOffset; i++)
		{
			this.map.SetBlockAndRecompute(new BlockData(this.map.GetBlockSet().GetBlock(4)), new Vector3i(this.pos.x, i, this.pos.z));
		}
		this.map.SetBlockAndRecompute(new BlockData(this.map.GetBlockSet().GetBlock(4)), new Vector3i(this.pos.x + 1, this.pos.y, this.pos.z));
		this.map.SetBlockAndRecompute(new BlockData(this.map.GetBlockSet().GetBlock(4)), new Vector3i(this.pos.x - 1, this.pos.y, this.pos.z));
		this.map.SetBlockAndRecompute(new BlockData(this.map.GetBlockSet().GetBlock(4)), new Vector3i(this.pos.x, this.pos.y, this.pos.z + 1));
		this.map.SetBlockAndRecompute(new BlockData(this.map.GetBlockSet().GetBlock(4)), new Vector3i(this.pos.x, this.pos.y, this.pos.z - 1));
		for (int j = this.pos.x + 1; j <= this.pos.x + 4; j++)
		{
			this.map.SetBlockAndRecompute(new BlockData(this.map.GetBlockSet().GetBlock((_t2 > _t1) ? 37 : 38)), new Vector3i(j, this.pos.y + this.maxOffset, this.pos.z));
			this.map.SetBlockAndRecompute(new BlockData(this.map.GetBlockSet().GetBlock((_t2 > _t1) ? 37 : 38)), new Vector3i(j, this.pos.y + this.maxOffset - 1, this.pos.z));
		}
		if (_t1 < 150)
		{
			this.flagObject = HoloBase.Create(this.pos, 777);
			this.FP = this.flagObject.GetComponent<FlagPlase>();
			if (this.FP != null && _t1 > _t2)
			{
				this.FP.team = 0;
			}
		}
		this.timer[0] = _t1;
		this.timer[1] = _t2;
		this.inited = true;
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x00083B1C File Offset: 0x00081D1C
	public void UpdateFlag(int _t1, int _t2)
	{
		this.timer[0] = _t1;
		this.timer[1] = _t2;
		int num = 1;
		int num2 = this.timer[1];
		if (this.timer[0] >= num2)
		{
			num2 = this.timer[0];
			num = 0;
		}
		num2 = num2 * 10 / 150 + 10;
		if (num2 != this.maxOffset || num2 == 20)
		{
			for (int i = this.pos.x + 1; i <= this.pos.x + 4; i++)
			{
				this.map.SetBlockAndRecompute(new BlockData(null), new Vector3i(i, this.pos.y + this.maxOffset, this.pos.z));
				this.map.SetBlockAndRecompute(new BlockData(null), new Vector3i(i, this.pos.y + this.maxOffset - 1, this.pos.z));
				this.map.SetBlockAndRecompute(new BlockData(this.map.GetBlockSet().GetBlock((num == 1) ? 37 : 38)), new Vector3i(i, this.pos.y + num2, this.pos.z));
				this.map.SetBlockAndRecompute(new BlockData(this.map.GetBlockSet().GetBlock((num == 1) ? 37 : 38)), new Vector3i(i, this.pos.y + num2 - 1, this.pos.z));
			}
			this.maxOffset = num2;
		}
		if (this.FP != null)
		{
			this.FP.accepted = true;
			if (_t1 > _t2)
			{
				this.FP.team = 0;
			}
			else
			{
				this.FP.team = 1;
			}
		}
		if (this.timer[0] == 150)
		{
			Object.Destroy(this.flagObject);
		}
	}

	// Token: 0x04000EDE RID: 3806
	public int[] timer = new int[2];

	// Token: 0x04000EDF RID: 3807
	public Vector3i pos;

	// Token: 0x04000EE0 RID: 3808
	private Map map;

	// Token: 0x04000EE1 RID: 3809
	public bool inited;

	// Token: 0x04000EE2 RID: 3810
	public int maxOffset = 20;

	// Token: 0x04000EE3 RID: 3811
	public GameObject flagObject;

	// Token: 0x04000EE4 RID: 3812
	public FlagPlase FP;
}
