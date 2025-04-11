using System;
using System.Collections.Generic;
using System.Reflection;

// Token: 0x020000BA RID: 186
public class vp_Attempt : vp_Event
{
	// Token: 0x06000640 RID: 1600 RVA: 0x0006CF70 File Offset: 0x0006B170
	protected static bool AlwaysOK()
	{
		return true;
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x0006D530 File Offset: 0x0006B730
	public vp_Attempt(string name) : base(name)
	{
		this.InitFields();
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x0006D540 File Offset: 0x0006B740
	protected override void InitFields()
	{
		this.m_Fields = new FieldInfo[]
		{
			base.GetType().GetField("Try")
		};
		base.StoreInvokerFieldNames();
		this.m_DefaultMethods = new MethodInfo[]
		{
			base.GetType().GetMethod("AlwaysOK")
		};
		this.m_DelegateTypes = new Type[]
		{
			typeof(vp_Attempt.Tryer)
		};
		this.Prefixes = new Dictionary<string, int>
		{
			{
				"OnAttempt_",
				0
			}
		};
		this.Try = new vp_Attempt.Tryer(vp_Attempt.AlwaysOK);
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x0006D5D3 File Offset: 0x0006B7D3
	public override void Register(object t, string m, int v)
	{
		this.Try = (vp_Attempt.Tryer)Delegate.CreateDelegate(this.m_DelegateTypes[v], t, m);
		base.Refresh();
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x0006D5F5 File Offset: 0x0006B7F5
	public override void Unregister(object t)
	{
		this.Try = new vp_Attempt.Tryer(vp_Attempt.AlwaysOK);
		base.Refresh();
	}

	// Token: 0x04000BA3 RID: 2979
	public vp_Attempt.Tryer Try;

	// Token: 0x02000886 RID: 2182
	// (Invoke) Token: 0x06004C3B RID: 19515
	public delegate bool Tryer();
}
