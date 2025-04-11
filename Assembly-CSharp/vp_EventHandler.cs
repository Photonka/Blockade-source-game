using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x020000BE RID: 190
public abstract class vp_EventHandler : MonoBehaviour
{
	// Token: 0x06000664 RID: 1636 RVA: 0x0006E354 File Offset: 0x0006C554
	protected virtual void Awake()
	{
		this.StoreHandlerEvents();
		this.m_Initialized = true;
		for (int i = this.m_PendingRegistrants.Count - 1; i > -1; i--)
		{
			this.Register(this.m_PendingRegistrants[i]);
			this.m_PendingRegistrants.Remove(this.m_PendingRegistrants[i]);
		}
	}

	// Token: 0x06000665 RID: 1637 RVA: 0x0006E3B0 File Offset: 0x0006C5B0
	protected void StoreHandlerEvents()
	{
		foreach (FieldInfo fieldInfo in base.GetType().GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
		{
			object obj = Activator.CreateInstance(fieldInfo.FieldType, new object[]
			{
				fieldInfo.Name
			});
			if (obj != null)
			{
				fieldInfo.SetValue(this, obj);
				foreach (string str in ((vp_Event)obj).Prefixes.Keys)
				{
					this.m_HandlerEvents.Add(str + fieldInfo.Name, (vp_Event)obj);
				}
			}
		}
	}

	// Token: 0x06000666 RID: 1638 RVA: 0x0006E474 File Offset: 0x0006C674
	public void Register(object target)
	{
		if (target == null)
		{
			Debug.LogError("Error: (" + this + ") Target object was null.");
			return;
		}
		if (!this.m_Initialized)
		{
			this.m_PendingRegistrants.Add(target);
			return;
		}
		vp_EventHandler.ScriptMethods scriptMethods = this.GetScriptMethods(target);
		if (scriptMethods == null)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Error: (",
				this,
				") could not get script methods for '",
				target,
				"'."
			}));
			return;
		}
		foreach (MethodInfo methodInfo in scriptMethods.Events)
		{
			vp_Event vp_Event;
			if (!this.m_HandlerEvents.TryGetValue(methodInfo.Name, out vp_Event))
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					"Warning: (",
					methodInfo.DeclaringType,
					") Event handler can't register method '",
					methodInfo.Name,
					"' because '",
					base.GetType(),
					"' has not (successfully) registered any event named '",
					methodInfo.Name.Substring(methodInfo.Name.Substring(0, methodInfo.Name.IndexOf('_', 4) + 1).Length)
				}));
			}
			else
			{
				int num;
				vp_Event.Prefixes.TryGetValue(methodInfo.Name.Substring(0, methodInfo.Name.IndexOf('_', 4) + 1), out num);
				if (this.CompareMethodSignatures(methodInfo, vp_Event.GetParameterType(num), vp_Event.GetReturnType(num)))
				{
					vp_Event.Register(target, methodInfo.Name, num);
				}
			}
		}
	}

	// Token: 0x06000667 RID: 1639 RVA: 0x0006E614 File Offset: 0x0006C814
	public void Unregister(object target)
	{
		if (target == null)
		{
			Debug.LogError("Error: (" + this + ") Target object was null.");
			return;
		}
		foreach (vp_Event vp_Event in this.m_HandlerEvents.Values)
		{
			if (vp_Event != null)
			{
				foreach (string name in vp_Event.InvokerFieldNames)
				{
					FieldInfo field = vp_Event.GetType().GetField(name);
					if (!(field == null))
					{
						object value = field.GetValue(vp_Event);
						if (value != null)
						{
							Delegate @delegate = (Delegate)value;
							if (@delegate != null)
							{
								Delegate[] invocationList = @delegate.GetInvocationList();
								for (int j = 0; j < invocationList.Length; j++)
								{
									if (invocationList[j].Target == target)
									{
										vp_Event.Unregister(target);
									}
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000668 RID: 1640 RVA: 0x0006E710 File Offset: 0x0006C910
	protected bool CompareMethodSignatures(MethodInfo scriptMethod, Type handlerParameterType, Type handlerReturnType)
	{
		if (scriptMethod.ReturnType != handlerReturnType)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Error: (",
				scriptMethod.DeclaringType,
				") Return type (",
				vp_Utility.GetTypeAlias(scriptMethod.ReturnType),
				") is not valid for '",
				scriptMethod.Name,
				"'. Return type declared in event handler was: (",
				vp_Utility.GetTypeAlias(handlerReturnType),
				")."
			}));
			return false;
		}
		if (scriptMethod.GetParameters().Length == 1)
		{
			if (((ParameterInfo)scriptMethod.GetParameters().GetValue(0)).ParameterType != handlerParameterType)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Error: (",
					scriptMethod.DeclaringType,
					") Parameter type (",
					vp_Utility.GetTypeAlias(((ParameterInfo)scriptMethod.GetParameters().GetValue(0)).ParameterType),
					") is not valid for '",
					scriptMethod.Name,
					"'. Parameter type declared in event handler was: (",
					vp_Utility.GetTypeAlias(handlerParameterType),
					")."
				}));
				return false;
			}
		}
		else if (scriptMethod.GetParameters().Length == 0)
		{
			if (handlerParameterType != typeof(void))
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Error: (",
					scriptMethod.DeclaringType,
					") Can't register method '",
					scriptMethod.Name,
					"' with 0 parameters. Expected: 1 parameter of type (",
					vp_Utility.GetTypeAlias(handlerParameterType),
					")."
				}));
				return false;
			}
		}
		else if (scriptMethod.GetParameters().Length > 1)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Error: (",
				scriptMethod.DeclaringType,
				") Can't register method '",
				scriptMethod.Name,
				"' with ",
				scriptMethod.GetParameters().Length,
				" parameters. Max parameter count: 1 of type (",
				vp_Utility.GetTypeAlias(handlerParameterType),
				")."
			}));
			return false;
		}
		return true;
	}

	// Token: 0x06000669 RID: 1641 RVA: 0x0006E910 File Offset: 0x0006CB10
	protected vp_EventHandler.ScriptMethods GetScriptMethods(object target)
	{
		vp_EventHandler.ScriptMethods scriptMethods;
		if (!vp_EventHandler.m_StoredScriptTypes.TryGetValue(target.GetType(), out scriptMethods))
		{
			scriptMethods = new vp_EventHandler.ScriptMethods(target.GetType());
			vp_EventHandler.m_StoredScriptTypes.Add(target.GetType(), scriptMethods);
		}
		return scriptMethods;
	}

	// Token: 0x04000BAD RID: 2989
	protected bool m_Initialized;

	// Token: 0x04000BAE RID: 2990
	protected Dictionary<string, vp_Event> m_HandlerEvents = new Dictionary<string, vp_Event>();

	// Token: 0x04000BAF RID: 2991
	protected List<object> m_PendingRegistrants = new List<object>();

	// Token: 0x04000BB0 RID: 2992
	protected static Dictionary<Type, vp_EventHandler.ScriptMethods> m_StoredScriptTypes = new Dictionary<Type, vp_EventHandler.ScriptMethods>();

	// Token: 0x04000BB1 RID: 2993
	protected static string[] m_SupportedPrefixes = new string[]
	{
		"OnMessage_",
		"CanStart_",
		"CanStop_",
		"OnStart_",
		"OnStop_",
		"OnAttempt_",
		"get_OnValue_",
		"set_OnValue_"
	};

	// Token: 0x02000888 RID: 2184
	protected class ScriptMethods
	{
		// Token: 0x06004C42 RID: 19522 RVA: 0x001AE113 File Offset: 0x001AC313
		public ScriptMethods(Type type)
		{
			this.Events = vp_EventHandler.ScriptMethods.GetMethods(type);
		}

		// Token: 0x06004C43 RID: 19523 RVA: 0x001AE134 File Offset: 0x001AC334
		protected static List<MethodInfo> GetMethods(Type type)
		{
			List<MethodInfo> list = new List<MethodInfo>();
			List<string> list2 = new List<string>();
			while (type != null)
			{
				foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
				{
					if (!methodInfo.Name.Contains(">m__") && !list2.Contains(methodInfo.Name))
					{
						foreach (string value in vp_EventHandler.m_SupportedPrefixes)
						{
							if (methodInfo.Name.Contains(value))
							{
								list.Add(methodInfo);
								list2.Add(methodInfo.Name);
								break;
							}
						}
					}
				}
				type = type.BaseType;
			}
			return list;
		}

		// Token: 0x0400328B RID: 12939
		public List<MethodInfo> Events = new List<MethodInfo>();
	}
}
