using System;
using System.Collections.Generic;
using System.Reflection;

// Token: 0x020000C1 RID: 193
public class vp_Message<V, VResult> : vp_Message
{
	// Token: 0x06000676 RID: 1654 RVA: 0x0006EBAC File Offset: 0x0006CDAC
	protected static TResult Empty<T, TResult>(T value)
	{
		return default(TResult);
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x0006EAB0 File Offset: 0x0006CCB0
	public vp_Message(string name) : base(name)
	{
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x0006EBC4 File Offset: 0x0006CDC4
	protected override void InitFields()
	{
		this.m_Fields = new FieldInfo[]
		{
			base.GetType().GetField("Send")
		};
		base.StoreInvokerFieldNames();
		this.m_DefaultMethods = new MethodInfo[]
		{
			base.GetStaticGenericMethod(base.GetType(), "Empty", this.m_ArgumentType, this.m_ReturnType)
		};
		this.m_DelegateTypes = new Type[]
		{
			typeof(vp_Message<, >.Sender<, >)
		};
		this.Prefixes = new Dictionary<string, int>
		{
			{
				"OnMessage_",
				0
			}
		};
		base.SetFieldToLocalMethod(this.m_Fields[0], this.m_DefaultMethods[0], base.MakeGenericType(this.m_DelegateTypes[0]));
	}

	// Token: 0x06000679 RID: 1657 RVA: 0x0006EC76 File Offset: 0x0006CE76
	public override void Register(object t, string m, int v)
	{
		base.AddExternalMethodToField(t, this.m_Fields[0], m, base.MakeGenericType(this.m_DelegateTypes[0]));
		base.Refresh();
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x0006EA99 File Offset: 0x0006CC99
	public override void Unregister(object t)
	{
		base.RemoveExternalMethodFromField(t, this.m_Fields[0]);
		base.Refresh();
	}

	// Token: 0x04000BB4 RID: 2996
	public new vp_Message<V, VResult>.Sender<V, VResult> Send;

	// Token: 0x0200088B RID: 2187
	// (Invoke) Token: 0x06004C4D RID: 19533
	public delegate TResult Sender<T, TResult>(T value);
}
