using System;
using System.Threading;

// Token: 0x02000049 RID: 73
public class ThreadedJob
{
	// Token: 0x17000008 RID: 8
	// (get) Token: 0x06000238 RID: 568 RVA: 0x0002CDEC File Offset: 0x0002AFEC
	// (set) Token: 0x06000239 RID: 569 RVA: 0x0002CE30 File Offset: 0x0002B030
	public bool IsDone
	{
		get
		{
			object handle = this.m_Handle;
			bool isDone;
			lock (handle)
			{
				isDone = this.m_IsDone;
			}
			return isDone;
		}
		set
		{
			object handle = this.m_Handle;
			lock (handle)
			{
				this.m_IsDone = value;
			}
		}
	}

	// Token: 0x0600023A RID: 570 RVA: 0x0002CE74 File Offset: 0x0002B074
	public virtual void Start()
	{
		this.m_Thread = new Thread(new ThreadStart(this.Run));
		this.m_Thread.Start();
	}

	// Token: 0x0600023B RID: 571 RVA: 0x0002CE98 File Offset: 0x0002B098
	public virtual void Abort()
	{
		this.m_Thread.Abort();
	}

	// Token: 0x0600023C RID: 572 RVA: 0x00002B75 File Offset: 0x00000D75
	public virtual void Restart()
	{
	}

	// Token: 0x0600023D RID: 573 RVA: 0x00002B75 File Offset: 0x00000D75
	protected virtual void ThreadFunction()
	{
	}

	// Token: 0x0600023E RID: 574 RVA: 0x00002B75 File Offset: 0x00000D75
	protected virtual void OnFinished()
	{
	}

	// Token: 0x0600023F RID: 575 RVA: 0x0002CEA5 File Offset: 0x0002B0A5
	public virtual bool Update()
	{
		if (this.IsDone)
		{
			this.OnFinished();
			this.IsDone = false;
			return true;
		}
		return false;
	}

	// Token: 0x06000240 RID: 576 RVA: 0x0002CEBF File Offset: 0x0002B0BF
	private void Run()
	{
		this.ThreadFunction();
		this.IsDone = true;
	}

	// Token: 0x040002CF RID: 719
	private bool m_IsDone;

	// Token: 0x040002D0 RID: 720
	private object m_Handle = new object();

	// Token: 0x040002D1 RID: 721
	private Thread m_Thread;
}
