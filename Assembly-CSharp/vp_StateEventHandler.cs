using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C2 RID: 194
public abstract class vp_StateEventHandler : vp_EventHandler
{
	// Token: 0x0600067B RID: 1659 RVA: 0x0006EC9C File Offset: 0x0006CE9C
	protected override void Awake()
	{
		base.Awake();
		foreach (vp_Component item in base.transform.root.GetComponents<vp_Component>())
		{
			this.m_StateTargets.Add(item);
		}
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x0006ECDE File Offset: 0x0006CEDE
	protected void BindStateToActivity(vp_Activity a)
	{
		this.BindStateToActivityOnStart(a);
		this.BindStateToActivityOnStop(a);
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x0006ECF0 File Offset: 0x0006CEF0
	protected void BindStateToActivityOnStart(vp_Activity a)
	{
		if (!this.ActivityInitialized(a))
		{
			return;
		}
		string s = a.EventName;
		a.StartCallbacks = (vp_Activity.Callback)Delegate.Combine(a.StartCallbacks, new vp_Activity.Callback(delegate()
		{
			foreach (vp_Component vp_Component in this.m_StateTargets)
			{
				vp_Component.SetState(s, true, true, false);
			}
		}));
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x0006ED44 File Offset: 0x0006CF44
	protected void BindStateToActivityOnStop(vp_Activity a)
	{
		if (!this.ActivityInitialized(a))
		{
			return;
		}
		string s = a.EventName;
		a.StopCallbacks = (vp_Activity.Callback)Delegate.Combine(a.StopCallbacks, new vp_Activity.Callback(delegate()
		{
			foreach (vp_Component vp_Component in this.m_StateTargets)
			{
				vp_Component.SetState(s, false, true, false);
			}
		}));
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x0006ED98 File Offset: 0x0006CF98
	public void RefreshActivityStates()
	{
		foreach (vp_Event vp_Event in this.m_HandlerEvents.Values)
		{
			if (vp_Event is vp_Activity || vp_Event.GetType().BaseType == typeof(vp_Activity))
			{
				foreach (vp_Component vp_Component in this.m_StateTargets)
				{
					vp_Component.SetState(vp_Event.EventName, ((vp_Activity)vp_Event).Active, true, false);
				}
			}
		}
	}

	// Token: 0x06000680 RID: 1664 RVA: 0x0006EE60 File Offset: 0x0006D060
	public void ResetActivityStates()
	{
		foreach (vp_Component vp_Component in this.m_StateTargets)
		{
			vp_Component.ResetState();
		}
	}

	// Token: 0x06000681 RID: 1665 RVA: 0x0006EEB0 File Offset: 0x0006D0B0
	public void SetState(string state, bool setActive = true, bool recursive = true, bool includeDisabled = false)
	{
		foreach (vp_Component vp_Component in this.m_StateTargets)
		{
			vp_Component.SetState(state, setActive, recursive, includeDisabled);
		}
	}

	// Token: 0x06000682 RID: 1666 RVA: 0x0006EF08 File Offset: 0x0006D108
	private bool ActivityInitialized(vp_Activity a)
	{
		if (a == null)
		{
			Debug.LogError("Error: (" + this + ") Activity is null.");
			return false;
		}
		if (string.IsNullOrEmpty(a.EventName))
		{
			Debug.LogError("Error: (" + this + ") Activity not initialized. Make sure the event handler has run its Awake call before binding layers.");
			return false;
		}
		return true;
	}

	// Token: 0x04000BB5 RID: 2997
	private List<vp_Component> m_StateTargets = new List<vp_Component>();
}
