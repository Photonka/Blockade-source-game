using System;
using System.Collections.Generic;
using System.Reflection;

// Token: 0x020000C3 RID: 195
public class vp_Value<V> : vp_Event
{
	// Token: 0x06000684 RID: 1668 RVA: 0x0006EF68 File Offset: 0x0006D168
	protected static T Empty<T>()
	{
		return default(T);
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x00002B75 File Offset: 0x00000D75
	protected static void Empty<T>(T value)
	{
	}

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x06000686 RID: 1670 RVA: 0x0006EF7E File Offset: 0x0006D17E
	private FieldInfo[] Fields
	{
		get
		{
			return this.m_Fields;
		}
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x0006D530 File Offset: 0x0006B730
	public vp_Value(string name) : base(name)
	{
		this.InitFields();
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x0006EF88 File Offset: 0x0006D188
	protected override void InitFields()
	{
		this.m_Fields = new FieldInfo[]
		{
			base.GetType().GetField("Get"),
			base.GetType().GetField("Set")
		};
		base.StoreInvokerFieldNames();
		this.m_DelegateTypes = new Type[]
		{
			typeof(vp_Value<>.Getter<>),
			typeof(vp_Value<>.Setter<>)
		};
		this.m_DefaultMethods = new MethodInfo[]
		{
			base.GetStaticGenericMethod(base.GetType(), "Empty", typeof(void), this.m_ArgumentType),
			base.GetStaticGenericMethod(base.GetType(), "Empty", this.m_ArgumentType, typeof(void))
		};
		this.Prefixes = new Dictionary<string, int>
		{
			{
				"get_OnValue_",
				0
			},
			{
				"set_OnValue_",
				1
			}
		};
		base.SetFieldToLocalMethod(this.m_Fields[0], this.m_DefaultMethods[0], base.MakeGenericType(this.m_DelegateTypes[0]));
		base.SetFieldToLocalMethod(this.m_Fields[1], this.m_DefaultMethods[1], base.MakeGenericType(this.m_DelegateTypes[1]));
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x0006F0B2 File Offset: 0x0006D2B2
	public override void Register(object t, string m, int v)
	{
		base.SetFieldToExternalMethod(t, this.m_Fields[v], m, base.MakeGenericType(this.m_DelegateTypes[v]));
		base.Refresh();
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x0006F0D8 File Offset: 0x0006D2D8
	public override void Unregister(object t)
	{
		base.SetFieldToLocalMethod(this.m_Fields[0], this.m_DefaultMethods[0], base.MakeGenericType(this.m_DelegateTypes[0]));
		base.SetFieldToLocalMethod(this.m_Fields[1], this.m_DefaultMethods[1], base.MakeGenericType(this.m_DelegateTypes[1]));
		base.Refresh();
	}

	// Token: 0x04000BB6 RID: 2998
	public vp_Value<V>.Getter<V> Get;

	// Token: 0x04000BB7 RID: 2999
	public vp_Value<V>.Setter<V> Set;

	// Token: 0x0200088E RID: 2190
	// (Invoke) Token: 0x06004C55 RID: 19541
	public delegate T Getter<T>();

	// Token: 0x0200088F RID: 2191
	// (Invoke) Token: 0x06004C59 RID: 19545
	public delegate void Setter<T>(T o);
}
