using System;

// Token: 0x020000B9 RID: 185
public class vp_Activity<V> : vp_Activity
{
	// Token: 0x0600063E RID: 1598 RVA: 0x0006D509 File Offset: 0x0006B709
	public vp_Activity(string name) : base(name)
	{
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x0006D512 File Offset: 0x0006B712
	public bool TryStart<T>(T argument)
	{
		if (this.m_Active)
		{
			return false;
		}
		this.m_Argument = argument;
		return base.TryStart();
	}
}
