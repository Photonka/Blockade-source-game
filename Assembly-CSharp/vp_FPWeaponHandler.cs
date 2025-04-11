using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D2 RID: 210
public class vp_FPWeaponHandler : MonoBehaviour
{
	// Token: 0x1700004A RID: 74
	// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00077A0B File Offset: 0x00075C0B
	public vp_FPWeapon CurrentWeapon
	{
		get
		{
			return this.m_CurrentWeapon;
		}
	}

	// Token: 0x1700004B RID: 75
	// (get) Token: 0x060007A7 RID: 1959 RVA: 0x00077A13 File Offset: 0x00075C13
	public int CurrentWeaponID
	{
		get
		{
			return this.m_CurrentWeaponID;
		}
	}

	// Token: 0x060007A8 RID: 1960 RVA: 0x00077A1C File Offset: 0x00075C1C
	protected virtual void Awake()
	{
		if (base.GetComponent<vp_FPWeapon>())
		{
			Debug.LogError("Error: (" + this + ") Hierarchy error. This component should sit above any vp_FPWeapons in the gameobject hierarchy.");
			return;
		}
		this.m_Player = (vp_FPPlayerEventHandler)base.transform.root.GetComponentInChildren(typeof(vp_FPPlayerEventHandler));
		foreach (vp_FPWeapon item in base.GetComponentsInChildren<vp_FPWeapon>(true))
		{
			this.m_Weapons.Insert(this.m_Weapons.Count, item);
		}
		if (this.m_Weapons.Count == 0)
		{
			Debug.LogError("Error: (" + this + ") Hierarchy error. This component must be added to a gameobject with vp_FPWeapon components in child gameobjects.");
			return;
		}
		IComparer @object = new vp_FPWeaponHandler.WeaponComparer();
		this.m_Weapons.Sort(new Comparison<vp_FPWeapon>(@object.Compare));
		this.StartWeapon = Mathf.Clamp(this.StartWeapon, 0, this.m_Weapons.Count);
	}

	// Token: 0x060007A9 RID: 1961 RVA: 0x00077B00 File Offset: 0x00075D00
	protected virtual void OnEnable()
	{
		if (this.m_Player != null)
		{
			this.m_Player.Register(this);
		}
		vp_Timer.In(0f, delegate()
		{
			if (base.enabled)
			{
				this.SetWeapon(this.m_CurrentWeaponID);
			}
		}, null);
	}

	// Token: 0x060007AA RID: 1962 RVA: 0x00077B33 File Offset: 0x00075D33
	protected virtual void OnDisable()
	{
		if (this.m_Player != null)
		{
			this.m_Player.Unregister(this);
		}
	}

	// Token: 0x060007AB RID: 1963 RVA: 0x00077B4F File Offset: 0x00075D4F
	protected virtual void Update()
	{
		if (this.m_CurrentWeaponID == -1)
		{
			this.m_CurrentWeaponID = this.StartWeapon;
			this.SetWeapon(this.m_CurrentWeaponID);
		}
	}

	// Token: 0x060007AC RID: 1964 RVA: 0x00077B74 File Offset: 0x00075D74
	public virtual void SetWeapon(int i)
	{
		if (this.m_Weapons.Count < 1)
		{
			Debug.LogError("Error: (" + this + ") Tried to set weapon with an empty weapon list.");
			return;
		}
		if (i < 0 || i > this.m_Weapons.Count)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Error: (",
				this,
				") Weapon list does not have a weapon with index: ",
				i
			}));
			return;
		}
		if (this.m_CurrentWeapon != null)
		{
			this.m_CurrentWeapon.ResetState();
		}
		foreach (vp_FPWeapon vp_FPWeapon in this.m_Weapons)
		{
			vp_FPWeapon.ActivateGameObject(false);
		}
		this.m_CurrentWeaponID = i;
		this.m_CurrentWeapon = null;
		if (this.m_CurrentWeaponID > 0)
		{
			this.m_CurrentWeapon = this.m_Weapons[this.m_CurrentWeaponID - 1];
			if (this.m_CurrentWeapon != null)
			{
				this.m_CurrentWeapon.ActivateGameObject(true);
			}
		}
	}

	// Token: 0x060007AD RID: 1965 RVA: 0x00077C8C File Offset: 0x00075E8C
	public virtual void CancelTimers()
	{
		vp_Timer.CancelAll("EjectShell");
		this.m_DisableAttackStateTimer.Cancel();
		this.m_SetWeaponTimer.Cancel();
		this.m_SetWeaponRefreshTimer.Cancel();
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x00077CB9 File Offset: 0x00075EB9
	public virtual void SetWeaponLayer(int layer)
	{
		if (this.m_CurrentWeaponID < 1 || this.m_CurrentWeaponID > this.m_Weapons.Count)
		{
			return;
		}
		vp_Layer.Set(this.m_Weapons[this.m_CurrentWeaponID - 1].gameObject, layer, true);
	}

	// Token: 0x060007AF RID: 1967 RVA: 0x00077CF7 File Offset: 0x00075EF7
	protected virtual void OnStart_Reload()
	{
		this.m_Player.Attack.Stop(this.m_Player.CurrentWeaponReloadDuration.Get() + this.ReloadAttackSleepDuration);
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x00077D28 File Offset: 0x00075F28
	protected virtual void OnStart_SetWeapon()
	{
		this.CancelTimers();
		this.m_Player.Reload.Stop(this.SetWeaponDuration + this.SetWeaponReloadSleepDuration);
		this.m_Player.Zoom.Stop(this.SetWeaponDuration + this.SetWeaponZoomSleepDuration);
		this.m_Player.Attack.Stop(this.SetWeaponDuration + this.SetWeaponAttackSleepDuration);
		if (this.m_CurrentWeapon != null)
		{
			this.m_CurrentWeapon.Wield(false);
		}
		this.m_Player.SetWeapon.AutoDuration = this.SetWeaponDuration;
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x00077DC4 File Offset: 0x00075FC4
	protected virtual void OnStop_SetWeapon()
	{
		int weapon = (int)this.m_Player.SetWeapon.Argument;
		this.SetWeapon(weapon);
		if (this.m_CurrentWeapon != null)
		{
			this.m_CurrentWeapon.Wield(true);
		}
		vp_Timer.In(this.SetWeaponRefreshStatesDelay, delegate()
		{
			this.m_Player.RefreshActivityStates();
			if (this.m_CurrentWeapon != null && this.m_Player.CurrentWeaponAmmoCount.Get() == 0 && this.m_Player.CurrentWeaponClipCount.Get() > 0)
			{
				this.m_Player.Reload.TryStart();
			}
		}, this.m_SetWeaponRefreshTimer);
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x00077E28 File Offset: 0x00076028
	protected virtual bool CanStart_SetWeapon()
	{
		int num = (int)this.m_Player.SetWeapon.Argument;
		return num != this.m_CurrentWeaponID && num >= 0 && num <= this.m_Weapons.Count;
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x00077E6C File Offset: 0x0007606C
	protected virtual bool CanStart_Attack()
	{
		return !(this.m_CurrentWeapon == null) && !this.m_Player.Attack.Active && !this.m_Player.SetWeapon.Active && !this.m_Player.Reload.Active;
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x00077EC6 File Offset: 0x000760C6
	protected virtual void OnStop_Attack()
	{
		vp_Timer.In(this.AttackStateDisableDelay, delegate()
		{
			if (!this.m_Player.Attack.Active && this.m_CurrentWeapon != null)
			{
				this.m_CurrentWeapon.SetState("Attack", false, false, false);
			}
		}, this.m_DisableAttackStateTimer);
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x00077EE8 File Offset: 0x000760E8
	protected virtual bool OnAttempt_SetPrevWeapon()
	{
		int num = this.m_CurrentWeaponID - 1;
		if (num < 1)
		{
			num = this.m_Weapons.Count;
		}
		int num2 = 0;
		while (!this.m_Player.SetWeapon.TryStart<int>(num))
		{
			num--;
			if (num < 1)
			{
				num = this.m_Weapons.Count;
			}
			num2++;
			if (num2 > this.m_Weapons.Count)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x00077F50 File Offset: 0x00076150
	protected virtual bool OnAttempt_SetNextWeapon()
	{
		int num = this.m_CurrentWeaponID + 1;
		int num2 = 0;
		while (!this.m_Player.SetWeapon.TryStart<int>(num))
		{
			if (num > this.m_Weapons.Count + 1)
			{
				num = 0;
			}
			num++;
			num2++;
			if (num2 > this.m_Weapons.Count)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x00077FA8 File Offset: 0x000761A8
	protected virtual bool OnAttempt_SetWeaponByName(string name)
	{
		for (int i = 0; i < this.m_Weapons.Count; i++)
		{
			if (this.m_Weapons[i].name == name)
			{
				return this.m_Player.SetWeapon.TryStart<int>(i + 1);
			}
		}
		return false;
	}

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x060007B8 RID: 1976 RVA: 0x00077FF9 File Offset: 0x000761F9
	protected virtual bool OnValue_CurrentWeaponWielded
	{
		get
		{
			return !(this.m_CurrentWeapon == null) && this.m_CurrentWeapon.Wielded;
		}
	}

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x060007B9 RID: 1977 RVA: 0x00078016 File Offset: 0x00076216
	protected virtual string OnValue_CurrentWeaponName
	{
		get
		{
			if (this.m_CurrentWeapon == null || this.m_Weapons == null)
			{
				return "";
			}
			return this.m_CurrentWeapon.name;
		}
	}

	// Token: 0x1700004E RID: 78
	// (get) Token: 0x060007BA RID: 1978 RVA: 0x00077A13 File Offset: 0x00075C13
	protected virtual int OnValue_CurrentWeaponID
	{
		get
		{
			return this.m_CurrentWeaponID;
		}
	}

	// Token: 0x04000D22 RID: 3362
	public int StartWeapon;

	// Token: 0x04000D23 RID: 3363
	public float AttackStateDisableDelay = 0.5f;

	// Token: 0x04000D24 RID: 3364
	public float SetWeaponRefreshStatesDelay = 0.5f;

	// Token: 0x04000D25 RID: 3365
	public float SetWeaponDuration = 0.1f;

	// Token: 0x04000D26 RID: 3366
	public float SetWeaponReloadSleepDuration = 0.3f;

	// Token: 0x04000D27 RID: 3367
	public float SetWeaponZoomSleepDuration = 0.3f;

	// Token: 0x04000D28 RID: 3368
	public float SetWeaponAttackSleepDuration = 0.3f;

	// Token: 0x04000D29 RID: 3369
	public float ReloadAttackSleepDuration = 0.3f;

	// Token: 0x04000D2A RID: 3370
	protected vp_FPPlayerEventHandler m_Player;

	// Token: 0x04000D2B RID: 3371
	protected List<vp_FPWeapon> m_Weapons = new List<vp_FPWeapon>();

	// Token: 0x04000D2C RID: 3372
	protected int m_CurrentWeaponID = -1;

	// Token: 0x04000D2D RID: 3373
	protected vp_FPWeapon m_CurrentWeapon;

	// Token: 0x04000D2E RID: 3374
	protected vp_Timer.Handle m_SetWeaponTimer = new vp_Timer.Handle();

	// Token: 0x04000D2F RID: 3375
	protected vp_Timer.Handle m_SetWeaponRefreshTimer = new vp_Timer.Handle();

	// Token: 0x04000D30 RID: 3376
	protected vp_Timer.Handle m_DisableAttackStateTimer = new vp_Timer.Handle();

	// Token: 0x04000D31 RID: 3377
	protected vp_Timer.Handle m_DisableReloadStateTimer = new vp_Timer.Handle();

	// Token: 0x0200089C RID: 2204
	protected class WeaponComparer : IComparer
	{
		// Token: 0x06004C9A RID: 19610 RVA: 0x001AE9F2 File Offset: 0x001ACBF2
		int IComparer.Compare(object x, object y)
		{
			return new CaseInsensitiveComparer().Compare(((vp_FPWeapon)x).gameObject.name, ((vp_FPWeapon)y).gameObject.name);
		}
	}
}
