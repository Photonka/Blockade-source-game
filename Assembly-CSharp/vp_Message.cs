using System;
using System.Collections.Generic;
using System.Reflection;

// Token: 0x020000BF RID: 191
public class vp_Message : vp_Event
{
	// Token: 0x0600066C RID: 1644 RVA: 0x00002B75 File Offset: 0x00000D75
	protected static void Empty()
	{
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x0006D530 File Offset: 0x0006B730
	public vp_Message(string name) : base(name)
	{
		this.InitFields();
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x0006E9D4 File Offset: 0x0006CBD4
	protected override void InitFields()
	{
		this.m_Fields = new FieldInfo[]
		{
			base.GetType().GetField("Send")
		};
		base.StoreInvokerFieldNames();
		this.m_DefaultMethods = new MethodInfo[]
		{
			base.GetType().GetMethod("Empty")
		};
		this.m_DelegateTypes = new Type[]
		{
			typeof(vp_Message.Sender)
		};
		this.Prefixes = new Dictionary<string, int>
		{
			{
				"OnMessage_",
				0
			}
		};
		this.Send = new vp_Message.Sender(vp_Message.Empty);
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x0006EA67 File Offset: 0x0006CC67
	public override void Register(object t, string m, int v)
	{
		this.Send = (vp_Message.Sender)Delegate.Combine(this.Send, (vp_Message.Sender)Delegate.CreateDelegate(this.m_DelegateTypes[v], t, m));
		base.Refresh();
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x0006EA99 File Offset: 0x0006CC99
	public override void Unregister(object t)
	{
		base.RemoveExternalMethodFromField(t, this.m_Fields[0]);
		base.Refresh();
	}

	// Token: 0x04000BB2 RID: 2994
	public vp_Message.Sender Send;

	// Token: 0x02000889 RID: 2185
	// (Invoke) Token: 0x06004C45 RID: 19525
	public delegate void Sender();
}
