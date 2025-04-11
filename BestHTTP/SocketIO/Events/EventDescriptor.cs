using System;
using System.Collections.Generic;

namespace BestHTTP.SocketIO.Events
{
	// Token: 0x020001CA RID: 458
	internal sealed class EventDescriptor
	{
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x000A4268 File Offset: 0x000A2468
		// (set) Token: 0x0600114F RID: 4431 RVA: 0x000A4270 File Offset: 0x000A2470
		public List<SocketIOCallback> Callbacks { get; private set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x000A4279 File Offset: 0x000A2479
		// (set) Token: 0x06001151 RID: 4433 RVA: 0x000A4281 File Offset: 0x000A2481
		public bool OnlyOnce { get; private set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x000A428A File Offset: 0x000A248A
		// (set) Token: 0x06001153 RID: 4435 RVA: 0x000A4292 File Offset: 0x000A2492
		public bool AutoDecodePayload { get; private set; }

		// Token: 0x06001154 RID: 4436 RVA: 0x000A429B File Offset: 0x000A249B
		public EventDescriptor(bool onlyOnce, bool autoDecodePayload, SocketIOCallback callback)
		{
			this.OnlyOnce = onlyOnce;
			this.AutoDecodePayload = autoDecodePayload;
			this.Callbacks = new List<SocketIOCallback>(1);
			if (callback != null)
			{
				this.Callbacks.Add(callback);
			}
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x000A42CC File Offset: 0x000A24CC
		public void Call(Socket socket, Packet packet, params object[] args)
		{
			int count = this.Callbacks.Count;
			if (this.CallbackArray == null || this.CallbackArray.Length < count)
			{
				Array.Resize<SocketIOCallback>(ref this.CallbackArray, count);
			}
			this.Callbacks.CopyTo(this.CallbackArray);
			for (int i = 0; i < count; i++)
			{
				try
				{
					SocketIOCallback socketIOCallback = this.CallbackArray[i];
					if (socketIOCallback != null)
					{
						socketIOCallback(socket, packet, args);
					}
				}
				catch (Exception ex)
				{
					if (args == null || args.Length == 0 || !(args[0] is Error))
					{
						((ISocket)socket).EmitError(SocketIOErrors.User, ex.Message + " " + ex.StackTrace);
					}
					HTTPManager.Logger.Exception("EventDescriptor", "Call", ex);
				}
				if (this.OnlyOnce)
				{
					this.Callbacks.Remove(this.CallbackArray[i]);
				}
				this.CallbackArray[i] = null;
			}
		}

		// Token: 0x040013B0 RID: 5040
		private SocketIOCallback[] CallbackArray;
	}
}
