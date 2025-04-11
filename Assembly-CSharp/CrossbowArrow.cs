using System;
using UnityEngine;

// Token: 0x02000058 RID: 88
public class CrossbowArrow : MonoBehaviour
{
	// Token: 0x060002B1 RID: 689 RVA: 0x0003921C File Offset: 0x0003741C
	private void Update()
	{
		if (this.m_WeaponSystem == null)
		{
			this.m_WeaponSystem = (WeaponSystem)Object.FindObjectOfType(typeof(WeaponSystem));
		}
		if (this.m_WeaponSystem.GetPrimaryAmmo() > 0)
		{
			this.Show();
			return;
		}
		this.Hide();
	}

	// Token: 0x060002B2 RID: 690 RVA: 0x000391FD File Offset: 0x000373FD
	public void Hide()
	{
		base.GetComponent<Renderer>().enabled = false;
	}

	// Token: 0x060002B3 RID: 691 RVA: 0x0003920B File Offset: 0x0003740B
	public void Show()
	{
		base.GetComponent<Renderer>().enabled = true;
	}

	// Token: 0x04000521 RID: 1313
	private WeaponSystem m_WeaponSystem;
}
