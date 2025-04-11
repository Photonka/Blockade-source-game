using System;
using System.Collections.Generic;

namespace BestHTTP.Extensions
{
	// Token: 0x020007DA RID: 2010
	public sealed class HeartbeatManager
	{
		// Token: 0x060047DF RID: 18399 RVA: 0x0019A278 File Offset: 0x00198478
		public void Subscribe(IHeartbeat heartbeat)
		{
			List<IHeartbeat> heartbeats = this.Heartbeats;
			lock (heartbeats)
			{
				if (!this.Heartbeats.Contains(heartbeat))
				{
					this.Heartbeats.Add(heartbeat);
				}
			}
		}

		// Token: 0x060047E0 RID: 18400 RVA: 0x0019A2CC File Offset: 0x001984CC
		public void Unsubscribe(IHeartbeat heartbeat)
		{
			List<IHeartbeat> heartbeats = this.Heartbeats;
			lock (heartbeats)
			{
				this.Heartbeats.Remove(heartbeat);
			}
		}

		// Token: 0x060047E1 RID: 18401 RVA: 0x0019A314 File Offset: 0x00198514
		public void Update()
		{
			if (this.LastUpdate == DateTime.MinValue)
			{
				this.LastUpdate = DateTime.UtcNow;
				return;
			}
			TimeSpan dif = DateTime.UtcNow - this.LastUpdate;
			this.LastUpdate = DateTime.UtcNow;
			int num = 0;
			List<IHeartbeat> heartbeats = this.Heartbeats;
			lock (heartbeats)
			{
				if (this.UpdateArray == null || this.UpdateArray.Length < this.Heartbeats.Count)
				{
					Array.Resize<IHeartbeat>(ref this.UpdateArray, this.Heartbeats.Count);
				}
				this.Heartbeats.CopyTo(0, this.UpdateArray, 0, this.Heartbeats.Count);
				num = this.Heartbeats.Count;
			}
			for (int i = 0; i < num; i++)
			{
				try
				{
					this.UpdateArray[i].OnHeartbeatUpdate(dif);
				}
				catch
				{
				}
			}
		}

		// Token: 0x04002DC6 RID: 11718
		private List<IHeartbeat> Heartbeats = new List<IHeartbeat>();

		// Token: 0x04002DC7 RID: 11719
		private IHeartbeat[] UpdateArray;

		// Token: 0x04002DC8 RID: 11720
		private DateTime LastUpdate = DateTime.MinValue;
	}
}
