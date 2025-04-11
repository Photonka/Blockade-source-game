using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x020000BC RID: 188
public abstract class vp_Event
{
	// Token: 0x1700001D RID: 29
	// (get) Token: 0x0600064A RID: 1610 RVA: 0x0006D76C File Offset: 0x0006B96C
	public string EventName
	{
		get
		{
			return this.m_Name;
		}
	}

	// Token: 0x1700001E RID: 30
	// (get) Token: 0x0600064B RID: 1611 RVA: 0x0006D774 File Offset: 0x0006B974
	public Type ArgumentType
	{
		get
		{
			return this.m_ArgumentType;
		}
	}

	// Token: 0x1700001F RID: 31
	// (get) Token: 0x0600064C RID: 1612 RVA: 0x0006D77C File Offset: 0x0006B97C
	public Type ReturnType
	{
		get
		{
			return this.m_ReturnType;
		}
	}

	// Token: 0x0600064D RID: 1613
	public abstract void Register(object target, string method, int variant);

	// Token: 0x0600064E RID: 1614
	public abstract void Unregister(object target);

	// Token: 0x0600064F RID: 1615
	protected abstract void InitFields();

	// Token: 0x06000650 RID: 1616 RVA: 0x0006D784 File Offset: 0x0006B984
	public vp_Event(string name = "")
	{
		this.m_ArgumentType = this.GetArgumentType;
		this.m_ReturnType = this.GetGenericReturnType;
		this.m_Name = name;
	}

	// Token: 0x06000651 RID: 1617 RVA: 0x0006D7AC File Offset: 0x0006B9AC
	protected void StoreInvokerFieldNames()
	{
		this.InvokerFieldNames = new string[this.m_Fields.Length];
		for (int i = 0; i < this.m_Fields.Length; i++)
		{
			this.InvokerFieldNames[i] = this.m_Fields[i].Name;
		}
	}

	// Token: 0x06000652 RID: 1618 RVA: 0x0006D7F4 File Offset: 0x0006B9F4
	protected Type MakeGenericType(Type type)
	{
		if (this.m_ReturnType == typeof(void))
		{
			return type.MakeGenericType(new Type[]
			{
				this.m_ArgumentType,
				this.m_ArgumentType
			});
		}
		return type.MakeGenericType(new Type[]
		{
			this.m_ArgumentType,
			this.m_ReturnType,
			this.m_ArgumentType,
			this.m_ReturnType
		});
	}

	// Token: 0x06000653 RID: 1619 RVA: 0x0006D868 File Offset: 0x0006BA68
	protected void SetFieldToExternalMethod(object target, FieldInfo field, string method, Type type)
	{
		Delegate @delegate = Delegate.CreateDelegate(type, target, method, false, false);
		if (@delegate == null)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Error (",
				this,
				") Failed to bind: ",
				target,
				" -> ",
				method,
				"."
			}));
			return;
		}
		field.SetValue(this, @delegate);
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x0006D8CC File Offset: 0x0006BACC
	protected void AddExternalMethodToField(object target, FieldInfo field, string method, Type type)
	{
		Delegate @delegate = Delegate.Combine((Delegate)field.GetValue(this), Delegate.CreateDelegate(type, target, method, false, false));
		if (@delegate == null)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Error (",
				this,
				") Failed to bind: ",
				target,
				" -> ",
				method,
				"."
			}));
			return;
		}
		field.SetValue(this, @delegate);
	}

	// Token: 0x06000655 RID: 1621 RVA: 0x0006D940 File Offset: 0x0006BB40
	protected void SetFieldToLocalMethod(FieldInfo field, MethodInfo method, Type type)
	{
		Delegate @delegate = Delegate.CreateDelegate(type, method);
		if (@delegate == null)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Error (",
				this,
				") Failed to bind: ",
				method,
				"."
			}));
			return;
		}
		field.SetValue(this, @delegate);
	}

	// Token: 0x06000656 RID: 1622 RVA: 0x0006D994 File Offset: 0x0006BB94
	protected void RemoveExternalMethodFromField(object target, FieldInfo field)
	{
		List<Delegate> list = new List<Delegate>(((Delegate)field.GetValue(this)).GetInvocationList());
		if (list == null)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Error (",
				this,
				") Failed to remove: ",
				target,
				" -> ",
				field.Name,
				"."
			}));
			return;
		}
		for (int i = list.Count - 1; i > -1; i--)
		{
			if (list[i].Target == target)
			{
				list.Remove(list[i]);
			}
		}
		if (list != null)
		{
			field.SetValue(this, Delegate.Combine(list.ToArray()));
		}
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x0006DA44 File Offset: 0x0006BC44
	protected MethodInfo GetStaticGenericMethod(Type e, string name, Type parameterType, Type returnType)
	{
		foreach (MethodInfo methodInfo in e.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy))
		{
			if (!(methodInfo.Name != name))
			{
				MethodInfo methodInfo2;
				if (this.GetGenericReturnType == typeof(void))
				{
					methodInfo2 = methodInfo.MakeGenericMethod(new Type[]
					{
						this.m_ArgumentType
					});
				}
				else
				{
					methodInfo2 = methodInfo.MakeGenericMethod(new Type[]
					{
						this.m_ArgumentType,
						this.m_ReturnType
					});
				}
				if (methodInfo2.GetParameters().Length <= 1 && (methodInfo2.GetParameters().Length != 1 || !(parameterType == typeof(void))) && (methodInfo2.GetParameters().Length != 0 || !(parameterType != typeof(void))) && (methodInfo2.GetParameters().Length != 1 || !(methodInfo2.GetParameters()[0].ParameterType != parameterType)) && !(returnType != methodInfo2.ReturnType))
				{
					return methodInfo2;
				}
			}
		}
		return null;
	}

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x06000658 RID: 1624 RVA: 0x0006DB46 File Offset: 0x0006BD46
	private Type GetArgumentType
	{
		get
		{
			if (!base.GetType().IsGenericType)
			{
				return typeof(void);
			}
			return base.GetType().GetGenericArguments()[0];
		}
	}

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x06000659 RID: 1625 RVA: 0x0006DB70 File Offset: 0x0006BD70
	private Type GetGenericReturnType
	{
		get
		{
			if (!base.GetType().IsGenericType)
			{
				return typeof(void);
			}
			if (base.GetType().GetGenericArguments().Length != 2)
			{
				return typeof(void);
			}
			return base.GetType().GetGenericArguments()[1];
		}
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x0006DBC0 File Offset: 0x0006BDC0
	public Type GetParameterType(int index)
	{
		if (!base.GetType().IsGenericType)
		{
			return typeof(void);
		}
		if (index > this.m_Fields.Length - 1)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Error: (",
				this,
				") Event '",
				this.EventName,
				"' only supports ",
				this.m_Fields.Length,
				" indices. 'GetParameterType' referenced index ",
				index,
				"."
			}));
		}
		if (this.m_DelegateTypes[index].GetMethod("Invoke").GetParameters().Length == 0)
		{
			return typeof(void);
		}
		return this.m_ArgumentType;
	}

	// Token: 0x0600065B RID: 1627 RVA: 0x0006DC7C File Offset: 0x0006BE7C
	public Type GetReturnType(int index)
	{
		if (index > this.m_Fields.Length - 1)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Error: (",
				this,
				") Event '",
				this.EventName,
				"' only supports ",
				this.m_Fields.Length,
				" indices. 'GetReturnType' referenced index ",
				index,
				"."
			}));
			return null;
		}
		if (base.GetType().GetGenericArguments().Length > 1)
		{
			return this.GetGenericReturnType;
		}
		Type returnType = this.m_DelegateTypes[index].GetMethod("Invoke").ReturnType;
		if (returnType.IsGenericParameter)
		{
			return this.m_ArgumentType;
		}
		return returnType;
	}

	// Token: 0x0600065C RID: 1628 RVA: 0x00002B75 File Offset: 0x00000D75
	protected void Refresh()
	{
	}

	// Token: 0x04000BA5 RID: 2981
	protected string m_Name;

	// Token: 0x04000BA6 RID: 2982
	protected Type m_ArgumentType;

	// Token: 0x04000BA7 RID: 2983
	protected Type m_ReturnType;

	// Token: 0x04000BA8 RID: 2984
	protected FieldInfo[] m_Fields;

	// Token: 0x04000BA9 RID: 2985
	protected Type[] m_DelegateTypes;

	// Token: 0x04000BAA RID: 2986
	protected MethodInfo[] m_DefaultMethods;

	// Token: 0x04000BAB RID: 2987
	public string[] InvokerFieldNames;

	// Token: 0x04000BAC RID: 2988
	public Dictionary<string, int> Prefixes;
}
