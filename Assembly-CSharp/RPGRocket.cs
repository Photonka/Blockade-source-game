using System;
using UnityEngine;

// Token: 0x0200006D RID: 109
public class RPGRocket : MonoBehaviour
{
	// Token: 0x06000325 RID: 805 RVA: 0x0003B2FC File Offset: 0x000394FC
	private void Update()
	{
		if (this.m_WeaponSystem == null)
		{
			this.m_WeaponSystem = (WeaponSystem)Object.FindObjectOfType(typeof(WeaponSystem));
		}
		if (this.m_WeaponSystem.GetAmmoRPGClip() > 0)
		{
			this.Show();
			return;
		}
		this.Hide();
	}

	// Token: 0x06000326 RID: 806 RVA: 0x000391FD File Offset: 0x000373FD
	public void Hide()
	{
		base.GetComponent<Renderer>().enabled = false;
	}

	// Token: 0x06000327 RID: 807 RVA: 0x0003920B File Offset: 0x0003740B
	public void Show()
	{
		base.GetComponent<Renderer>().enabled = true;
	}

	// Token: 0x040005B7 RID: 1463
	private WeaponSystem m_WeaponSystem;
}
