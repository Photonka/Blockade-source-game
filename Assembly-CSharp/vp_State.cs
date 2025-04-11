using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B6 RID: 182
[Serializable]
public class vp_State
{
	// Token: 0x06000617 RID: 1559 RVA: 0x0006CA67 File Offset: 0x0006AC67
	public vp_State(string typeName, string name = "Untitled", string path = null, TextAsset asset = null)
	{
		this.TypeName = typeName;
		this.Name = name;
		this.TextAsset = asset;
	}

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x06000618 RID: 1560 RVA: 0x0006CA85 File Offset: 0x0006AC85
	// (set) Token: 0x06000619 RID: 1561 RVA: 0x0006CA8D File Offset: 0x0006AC8D
	public bool Enabled
	{
		get
		{
			return this.m_Enabled;
		}
		set
		{
			this.m_Enabled = value;
			if (!Application.isPlaying)
			{
				return;
			}
			if (this.StateManager == null)
			{
				return;
			}
			if (this.m_Enabled)
			{
				this.StateManager.ImposeBlockingList(this);
				return;
			}
			this.StateManager.RelaxBlockingList(this);
		}
	}

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x0600061A RID: 1562 RVA: 0x0006CAC8 File Offset: 0x0006ACC8
	public bool Blocked
	{
		get
		{
			return this.CurrentlyBlockedBy.Count > 0;
		}
	}

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x0600061B RID: 1563 RVA: 0x0006CAD8 File Offset: 0x0006ACD8
	public int BlockCount
	{
		get
		{
			return this.CurrentlyBlockedBy.Count;
		}
	}

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x0600061C RID: 1564 RVA: 0x0006CAE5 File Offset: 0x0006ACE5
	protected List<vp_State> CurrentlyBlockedBy
	{
		get
		{
			if (this.m_CurrentlyBlockedBy == null)
			{
				this.m_CurrentlyBlockedBy = new List<vp_State>();
			}
			return this.m_CurrentlyBlockedBy;
		}
	}

	// Token: 0x0600061D RID: 1565 RVA: 0x0006CB00 File Offset: 0x0006AD00
	public void AddBlocker(vp_State blocker)
	{
		if (!this.CurrentlyBlockedBy.Contains(blocker))
		{
			this.CurrentlyBlockedBy.Add(blocker);
		}
	}

	// Token: 0x0600061E RID: 1566 RVA: 0x0006CB1C File Offset: 0x0006AD1C
	public void RemoveBlocker(vp_State blocker)
	{
		if (this.CurrentlyBlockedBy.Contains(blocker))
		{
			this.CurrentlyBlockedBy.Remove(blocker);
		}
	}

	// Token: 0x04000B88 RID: 2952
	public vp_StateManager StateManager;

	// Token: 0x04000B89 RID: 2953
	public string TypeName;

	// Token: 0x04000B8A RID: 2954
	public string Name;

	// Token: 0x04000B8B RID: 2955
	public TextAsset TextAsset;

	// Token: 0x04000B8C RID: 2956
	public vp_ComponentPreset Preset;

	// Token: 0x04000B8D RID: 2957
	public List<int> StatesToBlock;

	// Token: 0x04000B8E RID: 2958
	[NonSerialized]
	protected bool m_Enabled;

	// Token: 0x04000B8F RID: 2959
	[NonSerialized]
	protected List<vp_State> m_CurrentlyBlockedBy;
}
