using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B7 RID: 183
public class vp_StateManager
{
	// Token: 0x0600061F RID: 1567 RVA: 0x0006CB3C File Offset: 0x0006AD3C
	public vp_StateManager(vp_Component component, List<vp_State> states)
	{
		this.m_States = states;
		this.m_Component = component;
		this.m_Component.RefreshDefaultState();
		this.m_StateIds = new Dictionary<string, int>(StringComparer.CurrentCulture);
		foreach (vp_State vp_State in this.m_States)
		{
			vp_State.StateManager = this;
			if (!this.m_StateIds.ContainsKey(vp_State.Name))
			{
				this.m_StateIds.Add(vp_State.Name, this.m_States.IndexOf(vp_State));
			}
			else
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					"Warning: ",
					this.m_Component.GetType(),
					" on '",
					this.m_Component.name,
					"' has more than one state named: '",
					vp_State.Name,
					"'. Only the topmost one will be used."
				}));
				this.m_States[this.m_DefaultId].StatesToBlock.Add(this.m_States.IndexOf(vp_State));
			}
			if (vp_State.Preset == null)
			{
				vp_State.Preset = new vp_ComponentPreset();
			}
			if (vp_State.TextAsset != null)
			{
				vp_State.Preset.LoadFromTextAsset(vp_State.TextAsset);
			}
		}
		this.m_DefaultId = this.m_States.Count - 1;
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x0006CCC4 File Offset: 0x0006AEC4
	public void ImposeBlockingList(vp_State blocker)
	{
		foreach (int index in blocker.StatesToBlock)
		{
			this.m_States[index].AddBlocker(blocker);
		}
	}

	// Token: 0x06000621 RID: 1569 RVA: 0x0006CD24 File Offset: 0x0006AF24
	public void RelaxBlockingList(vp_State blocker)
	{
		foreach (int index in blocker.StatesToBlock)
		{
			this.m_States[index].RemoveBlocker(blocker);
		}
	}

	// Token: 0x06000622 RID: 1570 RVA: 0x0006CD84 File Offset: 0x0006AF84
	public void SetState(string state, bool setEnabled = true)
	{
		if (!vp_StateManager.AppPlaying())
		{
			return;
		}
		if (!this.m_StateIds.TryGetValue(state, out this.m_TargetId))
		{
			return;
		}
		if (this.m_TargetId == this.m_DefaultId && !setEnabled)
		{
			Debug.LogWarning(vp_StateManager.m_DefaultStateNoDisableMessage);
			return;
		}
		this.m_States[this.m_TargetId].Enabled = setEnabled;
		this.CombineStates();
		this.m_Component.Refresh();
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x0006CDF4 File Offset: 0x0006AFF4
	public void Reset()
	{
		if (!vp_StateManager.AppPlaying())
		{
			return;
		}
		foreach (vp_State vp_State in this.m_States)
		{
			vp_State.Enabled = false;
		}
		this.m_States[this.m_DefaultId].Enabled = true;
		this.m_TargetId = this.m_DefaultId;
		this.CombineStates();
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x0006CE78 File Offset: 0x0006B078
	public void CombineStates()
	{
		for (int i = this.m_States.Count - 1; i > -1; i--)
		{
			if ((i == this.m_DefaultId || (this.m_States[i].Enabled && !this.m_States[i].Blocked && !(this.m_States[i].TextAsset == null))) && this.m_States[i].Preset != null && !(this.m_States[i].Preset.ComponentType == null))
			{
				vp_ComponentPreset.Apply(this.m_Component, this.m_States[i].Preset);
			}
		}
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x0006CF39 File Offset: 0x0006B139
	public bool IsEnabled(string state)
	{
		return vp_StateManager.AppPlaying() && this.m_StateIds.TryGetValue(state, out this.m_TargetId) && this.m_States[this.m_TargetId].Enabled;
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x0006CF70 File Offset: 0x0006B170
	private static bool AppPlaying()
	{
		return true;
	}

	// Token: 0x04000B90 RID: 2960
	private vp_Component m_Component;

	// Token: 0x04000B91 RID: 2961
	[NonSerialized]
	private List<vp_State> m_States;

	// Token: 0x04000B92 RID: 2962
	private Dictionary<string, int> m_StateIds;

	// Token: 0x04000B93 RID: 2963
	private static string m_AppNotPlayingMessage = "Error: StateManager can only be accessed while application is playing.";

	// Token: 0x04000B94 RID: 2964
	private static string m_DefaultStateNoDisableMessage = "Warning: The 'Default' state cannot be disabled.";

	// Token: 0x04000B95 RID: 2965
	private int m_DefaultId;

	// Token: 0x04000B96 RID: 2966
	private int m_TargetId;
}
