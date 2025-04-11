using System;
using BestHTTP.ServerSentEvents;
using BestHTTP.SignalR.Messages;

namespace BestHTTP.SignalR.Transports
{
	// Token: 0x020001FB RID: 507
	public sealed class ServerSentEventsTransport : PostSendTransportBase
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x0006CF70 File Offset: 0x0006B170
		public override bool SupportsKeepAlive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x0006CF70 File Offset: 0x0006B170
		public override TransportTypes Type
		{
			get
			{
				return TransportTypes.ServerSentEvents;
			}
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x000A90C3 File Offset: 0x000A72C3
		public ServerSentEventsTransport(Connection con) : base("serverSentEvents", con)
		{
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x000A90D4 File Offset: 0x000A72D4
		public override void Connect()
		{
			if (this.EventSource != null)
			{
				HTTPManager.Logger.Warning("ServerSentEventsTransport", "Start - EventSource already created!");
				return;
			}
			if (base.State != TransportStates.Reconnecting)
			{
				base.State = TransportStates.Connecting;
			}
			RequestTypes type = (base.State == TransportStates.Reconnecting) ? RequestTypes.Reconnect : RequestTypes.Connect;
			Uri uri = base.Connection.BuildUri(type, this);
			this.EventSource = new EventSource(uri);
			this.EventSource.OnOpen += this.OnEventSourceOpen;
			this.EventSource.OnMessage += this.OnEventSourceMessage;
			this.EventSource.OnError += this.OnEventSourceError;
			this.EventSource.OnClosed += this.OnEventSourceClosed;
			this.EventSource.OnRetry += ((EventSource es) => false);
			this.EventSource.Open();
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x000A91C8 File Offset: 0x000A73C8
		public override void Stop()
		{
			this.EventSource.OnOpen -= this.OnEventSourceOpen;
			this.EventSource.OnMessage -= this.OnEventSourceMessage;
			this.EventSource.OnError -= this.OnEventSourceError;
			this.EventSource.OnClosed -= this.OnEventSourceClosed;
			this.EventSource.Close();
			this.EventSource = null;
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x00002B75 File Offset: 0x00000D75
		protected override void Started()
		{
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x000A9243 File Offset: 0x000A7443
		public override void Abort()
		{
			base.Abort();
			this.EventSource.Close();
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x000A9256 File Offset: 0x000A7456
		protected override void Aborted()
		{
			if (base.State == TransportStates.Closing)
			{
				base.State = TransportStates.Closed;
			}
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x000A9268 File Offset: 0x000A7468
		private void OnEventSourceOpen(EventSource eventSource)
		{
			HTTPManager.Logger.Information("Transport - " + base.Name, "OnEventSourceOpen");
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x000A928C File Offset: 0x000A748C
		private void OnEventSourceMessage(EventSource eventSource, Message message)
		{
			if (message.Data.Equals("initialized"))
			{
				base.OnConnected();
				return;
			}
			IServerMessage serverMessage = TransportBase.Parse(base.Connection.JsonEncoder, message.Data);
			if (serverMessage != null)
			{
				base.Connection.OnMessage(serverMessage);
			}
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x000A92D8 File Offset: 0x000A74D8
		private void OnEventSourceError(EventSource eventSource, string error)
		{
			HTTPManager.Logger.Information("Transport - " + base.Name, "OnEventSourceError");
			if (base.State == TransportStates.Reconnecting)
			{
				this.Connect();
				return;
			}
			if (base.State == TransportStates.Closed)
			{
				return;
			}
			if (base.State == TransportStates.Closing)
			{
				base.State = TransportStates.Closed;
				return;
			}
			base.Connection.Error(error);
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x000A933B File Offset: 0x000A753B
		private void OnEventSourceClosed(EventSource eventSource)
		{
			HTTPManager.Logger.Information("Transport - " + base.Name, "OnEventSourceClosed");
			this.OnEventSourceError(eventSource, "EventSource Closed!");
		}

		// Token: 0x04001480 RID: 5248
		private EventSource EventSource;
	}
}
