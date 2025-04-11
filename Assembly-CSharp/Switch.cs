using System;
using UnityEngine;

// Token: 0x02000089 RID: 137
public class Switch : MonoBehaviour
{
	// Token: 0x060003E0 RID: 992 RVA: 0x0004DDDA File Offset: 0x0004BFDA
	private void Awake()
	{
		this.NS = base.gameObject.GetComponent<New_Slots>();
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x0004DDED File Offset: 0x0004BFED
	public void ShowPanel(int _slot)
	{
		this.showTime = Time.time + 1.25f;
		this.slot = 0;
		this.panel = _slot;
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x0004DE0E File Offset: 0x0004C00E
	public void ShowPanel2(int _slot)
	{
		this.showTime = Time.time + 1.25f;
		this.slot = _slot;
		this.panel = 5;
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x0004DE2F File Offset: 0x0004C02F
	private void OnGUI()
	{
		if (Time.time > this.showTime)
		{
			return;
		}
		this.NS.Draw_New_Slots(this.panel + this.slot);
	}

	// Token: 0x04000825 RID: 2085
	private float showTime;

	// Token: 0x04000826 RID: 2086
	private int slot;

	// Token: 0x04000827 RID: 2087
	private int panel;

	// Token: 0x04000828 RID: 2088
	private New_Slots NS;
}
