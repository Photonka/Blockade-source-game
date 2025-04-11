using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x020000BB RID: 187
public class vp_Attempt<V> : vp_Attempt
{
	// Token: 0x06000645 RID: 1605 RVA: 0x0006CF70 File Offset: 0x0006B170
	protected static bool AlwaysOK<T>(T value)
	{
		return true;
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x0006D60F File Offset: 0x0006B80F
	public vp_Attempt(string name) : base(name)
	{
	}

	// Token: 0x06000647 RID: 1607 RVA: 0x0006D618 File Offset: 0x0006B818
	protected override void InitFields()
	{
		this.m_Fields = new FieldInfo[]
		{
			base.GetType().GetField("Try")
		};
		base.StoreInvokerFieldNames();
		this.m_DefaultMethods = new MethodInfo[]
		{
			base.GetStaticGenericMethod(base.GetType(), "AlwaysOK", this.m_ArgumentType, typeof(bool))
		};
		this.m_DelegateTypes = new Type[]
		{
			typeof(vp_Attempt<>.Tryer<>)
		};
		this.Prefixes = new Dictionary<string, int>
		{
			{
				"OnAttempt_",
				0
			}
		};
		base.SetFieldToLocalMethod(this.m_Fields[0], this.m_DefaultMethods[0], base.MakeGenericType(this.m_DelegateTypes[0]));
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x0006D6D0 File Offset: 0x0006B8D0
	public override void Register(object t, string m, int v)
	{
		if (((Delegate)this.m_Fields[v].GetValue(this)).Method.Name != this.m_DefaultMethods[v].Name)
		{
			Debug.LogWarning("Warning: Event '" + base.EventName + "' of type (vp_Attempt) targets multiple methods. Events of this type must reference a single method (only the last reference will be functional).");
		}
		base.SetFieldToExternalMethod(t, this.m_Fields[0], m, base.MakeGenericType(this.m_DelegateTypes[v]));
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x0006D746 File Offset: 0x0006B946
	public override void Unregister(object t)
	{
		base.SetFieldToLocalMethod(this.m_Fields[0], this.m_DefaultMethods[0], base.MakeGenericType(this.m_DelegateTypes[0]));
	}

	// Token: 0x04000BA4 RID: 2980
	public new vp_Attempt<V>.Tryer<V> Try;

	// Token: 0x02000887 RID: 2183
	// (Invoke) Token: 0x06004C3F RID: 19519
	public delegate bool Tryer<T>(T value);
}
