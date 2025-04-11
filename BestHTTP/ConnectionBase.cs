using System;
using System.Threading;

namespace BestHTTP
{
	// Token: 0x0200016A RID: 362
	internal abstract class ConnectionBase : IDisposable
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x0009273D File Offset: 0x0009093D
		// (set) Token: 0x06000CDE RID: 3294 RVA: 0x00092745 File Offset: 0x00090945
		public string ServerAddress { get; protected set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0009274E File Offset: 0x0009094E
		// (set) Token: 0x06000CE0 RID: 3296 RVA: 0x00092756 File Offset: 0x00090956
		public HTTPConnectionStates State { get; protected set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x0009275F File Offset: 0x0009095F
		public bool IsFree
		{
			get
			{
				return this.State == HTTPConnectionStates.Initial || this.State == HTTPConnectionStates.Free;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x00092774 File Offset: 0x00090974
		public bool IsActive
		{
			get
			{
				return this.State > HTTPConnectionStates.Initial && this.State < HTTPConnectionStates.Free;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x0009278A File Offset: 0x0009098A
		// (set) Token: 0x06000CE4 RID: 3300 RVA: 0x00092792 File Offset: 0x00090992
		public HTTPRequest CurrentRequest { get; protected set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x0009279B File Offset: 0x0009099B
		public virtual bool IsRemovable
		{
			get
			{
				return this.IsFree && DateTime.UtcNow - this.LastProcessTime > HTTPManager.MaxConnectionIdleTime;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x000927C1 File Offset: 0x000909C1
		// (set) Token: 0x06000CE7 RID: 3303 RVA: 0x000927C9 File Offset: 0x000909C9
		public DateTime StartTime { get; protected set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x000927D2 File Offset: 0x000909D2
		// (set) Token: 0x06000CE9 RID: 3305 RVA: 0x000927DA File Offset: 0x000909DA
		public DateTime TimedOutStart { get; protected set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x000927E3 File Offset: 0x000909E3
		public bool HasProxy
		{
			get
			{
				return this.CurrentRequest != null && this.CurrentRequest.Proxy != null;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x000927FD File Offset: 0x000909FD
		// (set) Token: 0x06000CEC RID: 3308 RVA: 0x00092805 File Offset: 0x00090A05
		public Uri LastProcessedUri { get; protected set; }

		// Token: 0x06000CED RID: 3309 RVA: 0x0009280E File Offset: 0x00090A0E
		public ConnectionBase(string serverAddress) : this(serverAddress, true)
		{
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00092818 File Offset: 0x00090A18
		public ConnectionBase(string serverAddress, bool threaded)
		{
			this.ServerAddress = serverAddress;
			this.State = HTTPConnectionStates.Initial;
			this.LastProcessTime = DateTime.UtcNow;
			this.IsThreaded = threaded;
		}

		// Token: 0x06000CEF RID: 3311
		internal abstract void Abort(HTTPConnectionStates hTTPConnectionStates);

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00092840 File Offset: 0x00090A40
		internal void Process(HTTPRequest request)
		{
			if (this.State == HTTPConnectionStates.Processing)
			{
				throw new Exception("Connection already processing a request!");
			}
			this.StartTime = DateTime.MaxValue;
			this.State = HTTPConnectionStates.Processing;
			this.CurrentRequest = request;
			if (this.IsThreaded)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.ThreadFunc));
				return;
			}
			this.ThreadFunc(null);
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00002B75 File Offset: 0x00000D75
		protected virtual void ThreadFunc(object param)
		{
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x000928A0 File Offset: 0x00090AA0
		internal void HandleProgressCallback()
		{
			if (this.CurrentRequest.OnProgress != null && this.CurrentRequest.DownloadProgressChanged)
			{
				try
				{
					this.CurrentRequest.OnProgress(this.CurrentRequest, this.CurrentRequest.Downloaded, this.CurrentRequest.DownloadLength);
				}
				catch (Exception ex)
				{
					HTTPManager.Logger.Exception("ConnectionBase", "HandleProgressCallback - OnProgress", ex);
				}
				this.CurrentRequest.DownloadProgressChanged = false;
			}
			if (this.CurrentRequest.OnUploadProgress != null && this.CurrentRequest.UploadProgressChanged)
			{
				try
				{
					this.CurrentRequest.OnUploadProgress(this.CurrentRequest, this.CurrentRequest.Uploaded, this.CurrentRequest.UploadLength);
				}
				catch (Exception ex2)
				{
					HTTPManager.Logger.Exception("ConnectionBase", "HandleProgressCallback - OnUploadProgress", ex2);
				}
				this.CurrentRequest.UploadProgressChanged = false;
			}
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x000929A4 File Offset: 0x00090BA4
		internal void HandleCallback()
		{
			try
			{
				this.HandleProgressCallback();
				if (this.State == HTTPConnectionStates.Upgraded)
				{
					if (this.CurrentRequest != null && this.CurrentRequest.Response != null && this.CurrentRequest.Response.IsUpgraded)
					{
						this.CurrentRequest.UpgradeCallback();
					}
					this.State = HTTPConnectionStates.WaitForProtocolShutdown;
				}
				else
				{
					this.CurrentRequest.CallCallback();
				}
			}
			catch (Exception ex)
			{
				HTTPManager.Logger.Exception("ConnectionBase", "HandleCallback", ex);
			}
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00092A30 File Offset: 0x00090C30
		internal void Recycle(HTTPConnectionRecycledDelegate onConnectionRecycled)
		{
			this.OnConnectionRecycled = onConnectionRecycled;
			if (this.State <= HTTPConnectionStates.Initial || this.State >= HTTPConnectionStates.WaitForProtocolShutdown || this.State == HTTPConnectionStates.Redirected)
			{
				this.RecycleNow();
			}
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00092A5C File Offset: 0x00090C5C
		protected void RecycleNow()
		{
			if (this.State == HTTPConnectionStates.TimedOut || this.State == HTTPConnectionStates.Closed)
			{
				this.LastProcessTime = DateTime.MinValue;
			}
			this.State = HTTPConnectionStates.Free;
			if (this.CurrentRequest != null)
			{
				this.CurrentRequest.Dispose();
			}
			this.CurrentRequest = null;
			if (this.OnConnectionRecycled != null)
			{
				this.OnConnectionRecycled(this);
				this.OnConnectionRecycled = null;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x00092AC3 File Offset: 0x00090CC3
		// (set) Token: 0x06000CF7 RID: 3319 RVA: 0x00092ACB File Offset: 0x00090CCB
		private protected bool IsDisposed { protected get; private set; }

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00092AD4 File Offset: 0x00090CD4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00092AE3 File Offset: 0x00090CE3
		protected virtual void Dispose(bool disposing)
		{
			this.IsDisposed = true;
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00092AEC File Offset: 0x00090CEC
		~ConnectionBase()
		{
			this.Dispose(false);
		}

		// Token: 0x0400117E RID: 4478
		protected DateTime LastProcessTime;

		// Token: 0x0400117F RID: 4479
		protected HTTPConnectionRecycledDelegate OnConnectionRecycled;

		// Token: 0x04001180 RID: 4480
		private bool IsThreaded;
	}
}
