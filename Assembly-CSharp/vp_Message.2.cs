using System;
using System.Collections.Generic;
using System.Reflection;

// Token: 0x020000C0 RID: 192
public class vp_Message<V> : vp_Message
{
	// Token: 0x06000671 RID: 1649 RVA: 0x00002B75 File Offset: 0x00000D75
	protected static void Empty<T>(T value)
	{
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x0006EAB0 File Offset: 0x0006CCB0
	public vp_Message(string name) : base(name)
	{
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x0006EABC File Offset: 0x0006CCBC
	protected override void InitFields()
	{
		this.m_Fields = new FieldInfo[]
		{
			base.GetType().GetField("Send")
		};
		base.StoreInvokerFieldNames();
		this.m_DefaultMethods = new MethodInfo[]
		{
			base.GetStaticGenericMethod(base.GetType(), "Empty", this.m_ArgumentType, typeof(void))
		};
		this.m_DelegateTypes = new Type[]
		{
			typeof(vp_Message<>.Sender<>)
		};
		this.Prefixes = new Dictionary<string, int>
		{
			{
				"OnMessage_",
				0
			}
		};
		this.Send = new vp_Message<V>.Sender<V>(vp_Message<V>.Empty<V>);
		base.SetFieldToLocalMethod(this.m_Fields[0], this.m_DefaultMethods[0], base.MakeGenericType(this.m_DelegateTypes[0]));
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x0006EB84 File Offset: 0x0006CD84
	public override void Register(object t, string m, int v)
	{
		base.AddExternalMethodToField(t, this.m_Fields[v], m, base.MakeGenericType(this.m_DelegateTypes[v]));
		base.Refresh();
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x0006EA99 File Offset: 0x0006CC99
	public override void Unregister(object t)
	{
		base.RemoveExternalMethodFromField(t, this.m_Fields[0]);
		base.Refresh();
	}

	// Token: 0x04000BB3 RID: 2995
	public new vp_Message<V>.Sender<V> Send;

	// Token: 0x0200088A RID: 2186
	// (Invoke) Token: 0x06004C49 RID: 19529
	public delegate void Sender<T>(T value);
}
