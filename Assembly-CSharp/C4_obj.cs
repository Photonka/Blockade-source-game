using System;
using UnityEngine;

// Token: 0x02000057 RID: 87
public class C4_obj : MonoBehaviour
{
	// Token: 0x060002AD RID: 685 RVA: 0x000391AC File Offset: 0x000373AC
	private void Update()
	{
		if (this.m_WeaponSystem == null)
		{
			this.m_WeaponSystem = (WeaponSystem)Object.FindObjectOfType(typeof(WeaponSystem));
		}
		if (this.m_WeaponSystem.GetAmmo(1) > 0)
		{
			this.Show();
			return;
		}
		this.Hide();
	}

	// Token: 0x060002AE RID: 686 RVA: 0x000391FD File Offset: 0x000373FD
	public void Hide()
	{
		base.GetComponent<Renderer>().enabled = false;
	}

	// Token: 0x060002AF RID: 687 RVA: 0x0003920B File Offset: 0x0003740B
	public void Show()
	{
		base.GetComponent<Renderer>().enabled = true;
	}

	// Token: 0x04000520 RID: 1312
	private WeaponSystem m_WeaponSystem;
}
