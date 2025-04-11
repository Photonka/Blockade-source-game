using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x020000B8 RID: 184
public class vp_Activity : vp_Event
{
	// Token: 0x06000628 RID: 1576 RVA: 0x00002B75 File Offset: 0x00000D75
	protected static void Empty()
	{
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x0006CF70 File Offset: 0x0006B170
	protected static bool AlwaysOK()
	{
		return true;
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x0006CF89 File Offset: 0x0006B189
	public vp_Activity(string name) : base(name)
	{
		this.InitFields();
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x0600062B RID: 1579 RVA: 0x0006CFAE File Offset: 0x0006B1AE
	// (set) Token: 0x0600062C RID: 1580 RVA: 0x0006CFB6 File Offset: 0x0006B1B6
	public float MinPause
	{
		get
		{
			return this.m_MinPause;
		}
		set
		{
			this.m_MinPause = Mathf.Max(0f, value);
		}
	}

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x0600062D RID: 1581 RVA: 0x0006CFC9 File Offset: 0x0006B1C9
	// (set) Token: 0x0600062E RID: 1582 RVA: 0x0006CFD4 File Offset: 0x0006B1D4
	public float MinDuration
	{
		get
		{
			return this.m_MinDuration;
		}
		set
		{
			this.m_MinDuration = Mathf.Max(0.001f, value);
			if (this.m_MaxDuration == -1f)
			{
				return;
			}
			if (this.m_MinDuration > this.m_MaxDuration)
			{
				this.m_MinDuration = this.m_MaxDuration;
				Debug.LogWarning("Warning: (vp_Activity) Tried to set MinDuration longer than MaxDuration for '" + base.EventName + "'. Capping at MaxDuration.");
			}
		}
	}

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x0600062F RID: 1583 RVA: 0x0006D034 File Offset: 0x0006B234
	// (set) Token: 0x06000630 RID: 1584 RVA: 0x0006D03C File Offset: 0x0006B23C
	public float AutoDuration
	{
		get
		{
			return this.m_MaxDuration;
		}
		set
		{
			if (value == -1f)
			{
				this.m_MaxDuration = value;
				return;
			}
			this.m_MaxDuration = Mathf.Max(0.001f, value);
			if (this.m_MaxDuration < this.m_MinDuration)
			{
				this.m_MaxDuration = this.m_MinDuration;
				Debug.LogWarning("Warning: (vp_Activity) Tried to set MaxDuration shorter than MinDuration for '" + base.EventName + "'. Capping at MinDuration.");
			}
		}
	}

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x06000631 RID: 1585 RVA: 0x0006D0A0 File Offset: 0x0006B2A0
	// (set) Token: 0x06000632 RID: 1586 RVA: 0x0006D0F8 File Offset: 0x0006B2F8
	public object Argument
	{
		get
		{
			if (this.m_ArgumentType == null)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Error: (",
					this,
					") Tried to fetch argument from '",
					base.EventName,
					"' but this activity takes no parameters."
				}));
				return null;
			}
			return this.m_Argument;
		}
		set
		{
			if (this.m_ArgumentType == null)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Error: (",
					this,
					") Tried to set argument for '",
					base.EventName,
					"' but this activity takes no parameters."
				}));
				return;
			}
			this.m_Argument = value;
		}
	}

	// Token: 0x06000633 RID: 1587 RVA: 0x0006D150 File Offset: 0x0006B350
	protected override void InitFields()
	{
		this.m_DelegateTypes = new Type[]
		{
			typeof(vp_Activity.Callback),
			typeof(vp_Activity.Callback),
			typeof(vp_Activity.Condition),
			typeof(vp_Activity.Condition)
		};
		this.m_Fields = new FieldInfo[]
		{
			base.GetType().GetField("StartCallbacks"),
			base.GetType().GetField("StopCallbacks"),
			base.GetType().GetField("StartConditions"),
			base.GetType().GetField("StopConditions")
		};
		base.StoreInvokerFieldNames();
		this.m_DefaultMethods = new MethodInfo[]
		{
			base.GetType().GetMethod("Empty"),
			base.GetType().GetMethod("Empty"),
			base.GetType().GetMethod("AlwaysOK"),
			base.GetType().GetMethod("AlwaysOK")
		};
		this.Prefixes = new Dictionary<string, int>
		{
			{
				"OnStart_",
				0
			},
			{
				"OnStop_",
				1
			},
			{
				"CanStart_",
				2
			},
			{
				"CanStop_",
				3
			}
		};
		this.StartCallbacks = new vp_Activity.Callback(vp_Activity.Empty);
		this.StopCallbacks = new vp_Activity.Callback(vp_Activity.Empty);
		this.StartConditions = new vp_Activity.Condition(vp_Activity.AlwaysOK);
		this.StopConditions = new vp_Activity.Condition(vp_Activity.AlwaysOK);
	}

	// Token: 0x06000634 RID: 1588 RVA: 0x0006D2D6 File Offset: 0x0006B4D6
	public override void Register(object t, string m, int v)
	{
		base.AddExternalMethodToField(t, this.m_Fields[v], m, this.m_DelegateTypes[v]);
		base.Refresh();
	}

	// Token: 0x06000635 RID: 1589 RVA: 0x0006D2F8 File Offset: 0x0006B4F8
	public override void Unregister(object t)
	{
		base.RemoveExternalMethodFromField(t, this.m_Fields[0]);
		base.RemoveExternalMethodFromField(t, this.m_Fields[1]);
		base.RemoveExternalMethodFromField(t, this.m_Fields[2]);
		base.RemoveExternalMethodFromField(t, this.m_Fields[3]);
		base.Refresh();
	}

	// Token: 0x06000636 RID: 1590 RVA: 0x0006D348 File Offset: 0x0006B548
	public bool TryStart()
	{
		if (this.m_Active)
		{
			return false;
		}
		if (Time.time < this.NextAllowedStartTime)
		{
			this.m_Argument = null;
			return false;
		}
		Delegate[] invocationList = this.StartConditions.GetInvocationList();
		for (int i = 0; i < invocationList.Length; i++)
		{
			if (!((vp_Activity.Condition)invocationList[i])())
			{
				this.m_Argument = null;
				return false;
			}
		}
		this.Active = true;
		return true;
	}

	// Token: 0x06000637 RID: 1591 RVA: 0x0006D3B0 File Offset: 0x0006B5B0
	public bool TryStop()
	{
		if (!this.m_Active)
		{
			return false;
		}
		if (Time.time < this.NextAllowedStopTime)
		{
			return false;
		}
		Delegate[] invocationList = this.StopConditions.GetInvocationList();
		for (int i = 0; i < invocationList.Length; i++)
		{
			if (!((vp_Activity.Condition)invocationList[i])())
			{
				return false;
			}
		}
		this.Active = false;
		return true;
	}

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x06000639 RID: 1593 RVA: 0x0006D4A9 File Offset: 0x0006B6A9
	// (set) Token: 0x06000638 RID: 1592 RVA: 0x0006D40C File Offset: 0x0006B60C
	public bool Active
	{
		get
		{
			return this.m_Active;
		}
		set
		{
			if (value && !this.m_Active)
			{
				this.m_Active = true;
				this.StartCallbacks();
				this.NextAllowedStopTime = Time.time + this.m_MinDuration;
				if (this.m_MaxDuration > 0f)
				{
					vp_Timer.In(this.m_MaxDuration, delegate()
					{
						this.Stop(0f);
					}, this.m_ForceStopTimer);
					return;
				}
			}
			else if (!value && this.m_Active)
			{
				this.m_Active = false;
				this.StopCallbacks();
				this.NextAllowedStartTime = Time.time + this.m_MinPause;
				this.m_Argument = null;
			}
		}
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x0006D4B1 File Offset: 0x0006B6B1
	public void Start(float forcedActiveDuration = 0f)
	{
		this.Active = true;
		if (forcedActiveDuration > 0f)
		{
			this.NextAllowedStopTime = Time.time + forcedActiveDuration;
		}
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x0006D4CF File Offset: 0x0006B6CF
	public void Stop(float forcedPauseDuration = 0f)
	{
		this.Active = false;
		if (forcedPauseDuration > 0f)
		{
			this.NextAllowedStartTime = Time.time + forcedPauseDuration;
		}
	}

	// Token: 0x0600063C RID: 1596 RVA: 0x0006D4ED File Offset: 0x0006B6ED
	public void Disallow(float duration)
	{
		this.NextAllowedStartTime = Time.time + duration;
	}

	// Token: 0x04000B97 RID: 2967
	public vp_Activity.Callback StartCallbacks;

	// Token: 0x04000B98 RID: 2968
	public vp_Activity.Callback StopCallbacks;

	// Token: 0x04000B99 RID: 2969
	public vp_Activity.Condition StartConditions;

	// Token: 0x04000B9A RID: 2970
	public vp_Activity.Condition StopConditions;

	// Token: 0x04000B9B RID: 2971
	protected vp_Timer.Handle m_ForceStopTimer = new vp_Timer.Handle();

	// Token: 0x04000B9C RID: 2972
	protected object m_Argument;

	// Token: 0x04000B9D RID: 2973
	protected bool m_Active;

	// Token: 0x04000B9E RID: 2974
	public float NextAllowedStartTime;

	// Token: 0x04000B9F RID: 2975
	public float NextAllowedStopTime;

	// Token: 0x04000BA0 RID: 2976
	private float m_MinPause;

	// Token: 0x04000BA1 RID: 2977
	private float m_MinDuration;

	// Token: 0x04000BA2 RID: 2978
	private float m_MaxDuration = -1f;

	// Token: 0x02000884 RID: 2180
	// (Invoke) Token: 0x06004C33 RID: 19507
	public delegate void Callback();

	// Token: 0x02000885 RID: 2181
	// (Invoke) Token: 0x06004C37 RID: 19511
	public delegate bool Condition();
}
