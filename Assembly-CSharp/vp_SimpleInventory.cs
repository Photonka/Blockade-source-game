using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000EA RID: 234
public class vp_SimpleInventory : MonoBehaviour
{
	// Token: 0x0600087B RID: 2171 RVA: 0x0007E1F1 File Offset: 0x0007C3F1
	protected virtual void OnEnable()
	{
		if (this.m_Player != null)
		{
			this.m_Player.Register(this);
		}
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x0007E20D File Offset: 0x0007C40D
	protected virtual void OnDisable()
	{
		if (this.m_Player != null)
		{
			this.m_Player.Unregister(this);
		}
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x0007E229 File Offset: 0x0007C429
	private void Awake()
	{
		this.m_Player = (vp_FPPlayerEventHandler)base.transform.root.GetComponentInChildren(typeof(vp_FPPlayerEventHandler));
	}

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x0600087E RID: 2174 RVA: 0x0007E250 File Offset: 0x0007C450
	protected Dictionary<string, vp_SimpleInventory.InventoryItemStatus> ItemStatusDictionary
	{
		get
		{
			if (this.m_ItemStatusDictionary == null)
			{
				this.m_ItemStatusDictionary = new Dictionary<string, vp_SimpleInventory.InventoryItemStatus>();
				for (int i = this.m_ItemTypes.Count - 1; i > -1; i--)
				{
					if (!this.m_ItemStatusDictionary.ContainsKey(this.m_ItemTypes[i].Name))
					{
						this.m_ItemStatusDictionary.Add(this.m_ItemTypes[i].Name, this.m_ItemTypes[i]);
					}
					else
					{
						this.m_ItemTypes.Remove(this.m_ItemTypes[i]);
					}
				}
				for (int j = this.m_WeaponTypes.Count - 1; j > -1; j--)
				{
					if (!this.m_ItemStatusDictionary.ContainsKey(this.m_WeaponTypes[j].Name))
					{
						this.m_ItemStatusDictionary.Add(this.m_WeaponTypes[j].Name, this.m_WeaponTypes[j]);
					}
					else
					{
						this.m_WeaponTypes.Remove(this.m_WeaponTypes[j]);
					}
				}
			}
			return this.m_ItemStatusDictionary;
		}
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x0007E36C File Offset: 0x0007C56C
	public bool HaveItem(object name)
	{
		vp_SimpleInventory.InventoryItemStatus inventoryItemStatus;
		return this.ItemStatusDictionary.TryGetValue((string)name, out inventoryItemStatus) && inventoryItemStatus.Have >= 1;
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x0007E39C File Offset: 0x0007C59C
	private vp_SimpleInventory.InventoryItemStatus GetItemStatus(string name)
	{
		vp_SimpleInventory.InventoryItemStatus result;
		if (!this.ItemStatusDictionary.TryGetValue(name, out result))
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Error: (",
				this,
				"). Unknown item type: '",
				name,
				"'."
			}));
		}
		return result;
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x0007E3EC File Offset: 0x0007C5EC
	private vp_SimpleInventory.InventoryWeaponStatus GetWeaponStatus(string name)
	{
		if (name == null)
		{
			return null;
		}
		vp_SimpleInventory.InventoryItemStatus inventoryItemStatus;
		if (!this.ItemStatusDictionary.TryGetValue(name, out inventoryItemStatus))
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Error: (",
				this,
				"). Unknown item type: '",
				name,
				"'."
			}));
			return null;
		}
		if (inventoryItemStatus.GetType() != typeof(vp_SimpleInventory.InventoryWeaponStatus))
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Error: (",
				this,
				"). Item is not a weapon: '",
				name,
				"'."
			}));
			return null;
		}
		return (vp_SimpleInventory.InventoryWeaponStatus)inventoryItemStatus;
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x0007E490 File Offset: 0x0007C690
	protected void RefreshWeaponStatus()
	{
		if (!this.m_Player.CurrentWeaponWielded.Get() && this.m_RefreshWeaponStatusIterations < 50)
		{
			this.m_RefreshWeaponStatusIterations++;
			vp_Timer.In(0.1f, new vp_Timer.Callback(this.RefreshWeaponStatus), null);
			return;
		}
		this.m_RefreshWeaponStatusIterations = 0;
		string text = this.m_Player.CurrentWeaponName.Get();
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		this.m_CurrentWeaponStatus = this.GetWeaponStatus(text);
	}

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x06000883 RID: 2179 RVA: 0x0007E517 File Offset: 0x0007C717
	// (set) Token: 0x06000884 RID: 2180 RVA: 0x0007E52E File Offset: 0x0007C72E
	protected virtual int OnValue_CurrentWeaponAmmoCount
	{
		get
		{
			if (this.m_CurrentWeaponStatus == null)
			{
				return 0;
			}
			return this.m_CurrentWeaponStatus.LoadedAmmo;
		}
		set
		{
			if (this.m_CurrentWeaponStatus == null)
			{
				return;
			}
			this.m_CurrentWeaponStatus.LoadedAmmo = value;
		}
	}

	// Token: 0x1700005D RID: 93
	// (get) Token: 0x06000885 RID: 2181 RVA: 0x0007E548 File Offset: 0x0007C748
	protected virtual int OnValue_CurrentWeaponClipCount
	{
		get
		{
			if (this.m_CurrentWeaponStatus == null)
			{
				return 0;
			}
			vp_SimpleInventory.InventoryItemStatus inventoryItemStatus;
			if (!this.ItemStatusDictionary.TryGetValue(this.m_CurrentWeaponStatus.ClipType, out inventoryItemStatus))
			{
				return 0;
			}
			return inventoryItemStatus.Have;
		}
	}

	// Token: 0x1700005E RID: 94
	// (get) Token: 0x06000886 RID: 2182 RVA: 0x0007E581 File Offset: 0x0007C781
	protected virtual string OnValue_CurrentWeaponClipType
	{
		get
		{
			if (this.m_CurrentWeaponStatus == null)
			{
				return "";
			}
			return this.m_CurrentWeaponStatus.ClipType;
		}
	}

	// Token: 0x06000887 RID: 2183 RVA: 0x0007E59C File Offset: 0x0007C79C
	protected virtual int OnMessage_GetItemCount(string name)
	{
		vp_SimpleInventory.InventoryItemStatus inventoryItemStatus;
		if (!this.ItemStatusDictionary.TryGetValue(name, out inventoryItemStatus))
		{
			return 0;
		}
		return inventoryItemStatus.Have;
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x0007E5C1 File Offset: 0x0007C7C1
	protected virtual bool OnAttempt_DepleteAmmo()
	{
		if (this.m_CurrentWeaponStatus == null)
		{
			return false;
		}
		if (this.m_CurrentWeaponStatus.LoadedAmmo < 1)
		{
			return this.m_CurrentWeaponStatus.MaxAmmo == 0;
		}
		this.m_CurrentWeaponStatus.LoadedAmmo--;
		return true;
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x0007E600 File Offset: 0x0007C800
	protected virtual bool OnAttempt_AddAmmo(object arg)
	{
		object[] array = (object[])arg;
		string name = (string)array[0];
		int num = (array.Length == 2) ? ((int)array[1]) : -1;
		vp_SimpleInventory.InventoryWeaponStatus weaponStatus = this.GetWeaponStatus(name);
		if (weaponStatus == null)
		{
			return false;
		}
		if (num == -1)
		{
			weaponStatus.LoadedAmmo = weaponStatus.MaxAmmo;
		}
		else
		{
			weaponStatus.LoadedAmmo = Mathf.Min(num, weaponStatus.MaxAmmo);
		}
		return true;
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x0007E664 File Offset: 0x0007C864
	protected virtual bool OnAttempt_AddItem(object args)
	{
		object[] array = (object[])args;
		string name = (string)array[0];
		int num = (array.Length == 2) ? ((int)array[1]) : 1;
		vp_SimpleInventory.InventoryItemStatus itemStatus = this.GetItemStatus(name);
		if (itemStatus == null)
		{
			return false;
		}
		itemStatus.CanHave = Mathf.Max(1, itemStatus.CanHave);
		if (itemStatus.Have >= itemStatus.CanHave)
		{
			return false;
		}
		itemStatus.Have = Mathf.Min(itemStatus.Have + num, itemStatus.CanHave);
		return true;
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x0007E6DC File Offset: 0x0007C8DC
	protected virtual bool OnAttempt_RemoveItem(object args)
	{
		object[] array = (object[])args;
		string name = (string)array[0];
		int num = (array.Length == 2) ? ((int)array[1]) : 1;
		vp_SimpleInventory.InventoryItemStatus itemStatus = this.GetItemStatus(name);
		if (itemStatus == null)
		{
			return false;
		}
		if (itemStatus.Have <= 0)
		{
			return false;
		}
		itemStatus.Have = Mathf.Max(itemStatus.Have - num, 0);
		return true;
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x0007E738 File Offset: 0x0007C938
	protected virtual bool OnAttempt_RemoveClip()
	{
		return this.m_CurrentWeaponStatus != null && this.GetItemStatus(this.m_CurrentWeaponStatus.ClipType) != null && this.m_CurrentWeaponStatus.LoadedAmmo < this.m_CurrentWeaponStatus.MaxAmmo && this.m_Player.RemoveItem.Try(new object[]
		{
			this.m_CurrentWeaponStatus.ClipType
		});
	}

	// Token: 0x0600088D RID: 2189 RVA: 0x0007E7AC File Offset: 0x0007C9AC
	protected virtual bool CanStart_SetWeapon()
	{
		int num = (int)this.m_Player.SetWeapon.Argument;
		return num == 0 || (num >= 0 && num <= this.m_WeaponTypes.Count && this.HaveItem(this.m_WeaponTypes[num - 1].Name));
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x0007E801 File Offset: 0x0007CA01
	protected virtual void OnStop_SetWeapon()
	{
		this.RefreshWeaponStatus();
	}

	// Token: 0x0600088F RID: 2191 RVA: 0x0007E80C File Offset: 0x0007CA0C
	protected virtual void OnStart_Dead()
	{
		foreach (vp_SimpleInventory.InventoryItemStatus inventoryItemStatus in this.m_ItemStatusDictionary.Values)
		{
			if (inventoryItemStatus.ClearOnDeath)
			{
				inventoryItemStatus.Have = 0;
				if (inventoryItemStatus.GetType() == typeof(vp_SimpleInventory.InventoryWeaponStatus))
				{
					((vp_SimpleInventory.InventoryWeaponStatus)inventoryItemStatus).LoadedAmmo = 0;
				}
			}
		}
	}

	// Token: 0x04000E3D RID: 3645
	protected vp_FPPlayerEventHandler m_Player;

	// Token: 0x04000E3E RID: 3646
	[SerializeField]
	protected List<vp_SimpleInventory.InventoryItemStatus> m_ItemTypes;

	// Token: 0x04000E3F RID: 3647
	[SerializeField]
	protected List<vp_SimpleInventory.InventoryWeaponStatus> m_WeaponTypes;

	// Token: 0x04000E40 RID: 3648
	protected Dictionary<string, vp_SimpleInventory.InventoryItemStatus> m_ItemStatusDictionary;

	// Token: 0x04000E41 RID: 3649
	protected vp_SimpleInventory.InventoryWeaponStatus m_CurrentWeaponStatus;

	// Token: 0x04000E42 RID: 3650
	protected int m_RefreshWeaponStatusIterations;

	// Token: 0x020008A4 RID: 2212
	[Serializable]
	public class InventoryItemStatus
	{
		// Token: 0x040032CA RID: 13002
		public string Name = "Unnamed";

		// Token: 0x040032CB RID: 13003
		public int Have;

		// Token: 0x040032CC RID: 13004
		public int CanHave = 1;

		// Token: 0x040032CD RID: 13005
		public bool ClearOnDeath = true;
	}

	// Token: 0x020008A5 RID: 2213
	[Serializable]
	public class InventoryWeaponStatus : vp_SimpleInventory.InventoryItemStatus
	{
		// Token: 0x040032CE RID: 13006
		public string ClipType = "";

		// Token: 0x040032CF RID: 13007
		public int LoadedAmmo;

		// Token: 0x040032D0 RID: 13008
		public int MaxAmmo = 10;
	}
}
