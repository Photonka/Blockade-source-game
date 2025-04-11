using System;
using BestHTTP.Extensions;
using BestHTTP.SignalR.Messages;

namespace BestHTTP.SignalR.Transports
{
	// Token: 0x020001F9 RID: 505
	public sealed class PollingTransport : PostSendTransportBase, IHeartbeat
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override bool SupportsKeepAlive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x000A8A48 File Offset: 0x000A6C48
		public override TransportTypes Type
		{
			get
			{
				return TransportTypes.LongPoll;
			}
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x000A8A4B File Offset: 0x000A6C4B
		public PollingTransport(Connection connection) : base("longPolling", connection)
		{
			this.LastPoll = DateTime.MinValue;
			this.PollTimeout = connection.NegotiationResult.ConnectionTimeout + TimeSpan.FromSeconds(10.0);
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x000A8A88 File Offset: 0x000A6C88
		public override void Connect()
		{
			HTTPManager.Logger.Information("Transport - " + base.Name, "Sending Open Request");
			if (base.State != TransportStates.Reconnecting)
			{
				base.State = TransportStates.Connecting;
			}
			RequestTypes type = (base.State == TransportStates.Reconnecting) ? RequestTypes.Reconnect : RequestTypes.Connect;
			HTTPRequest httprequest = new HTTPRequest(base.Connection.BuildUri(type, this), HTTPMethods.Get, true, true, new OnRequestFinishedDelegate(this.OnConnectRequestFinished));
			base.Connection.PrepareRequest(httprequest, type);
			httprequest.Send();
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x000A8B09 File Offset: 0x000A6D09
		public override void Stop()
		{
			HTTPManager.Heartbeats.Unsubscribe(this);
			if (this.pollRequest != null)
			{
				this.pollRequest.Abort();
				this.pollRequest = null;
			}
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x000A8B30 File Offset: 0x000A6D30
		protected override void Started()
		{
			this.LastPoll = DateTime.UtcNow;
			HTTPManager.Heartbeats.Subscribe(this);
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x000A8B48 File Offset: 0x000A6D48
		protected override void Aborted()
		{
			HTTPManager.Heartbeats.Unsubscribe(this);
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x000A8B58 File Offset: 0x000A6D58
		private void OnConnectRequestFinished(HTTPRequest req, HTTPResponse resp)
		{
			string text = string.Empty;
			switch (req.State)
			{
			case HTTPRequestStates.Finished:
				if (resp.IsSuccess)
				{
					HTTPManager.Logger.Information("Transport - " + base.Name, "Connect - Request Finished Successfully! " + resp.DataAsText);
					base.OnConnected();
					IServerMessage serverMessage = TransportBase.Parse(base.Connection.JsonEncoder, resp.DataAsText);
					if (serverMessage != null)
					{
						base.Connection.OnMessage(serverMessage);
						MultiMessage multiMessage = serverMessage as MultiMessage;
						if (multiMessage != null && multiMessage.PollDelay != null)
						{
							this.PollDelay = multiMessage.PollDelay.Value;
						}
					}
				}
				else
				{
					text = string.Format("Connect - Request Finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", resp.StatusCode, resp.Message, resp.DataAsText);
				}
				break;
			case HTTPRequestStates.Error:
				text = "Connect - Request Finished with Error! " + ((req.Exception != null) ? (req.Exception.Message + "\n" + req.Exception.StackTrace) : "No Exception");
				break;
			case HTTPRequestStates.Aborted:
				text = "Connect - Request Aborted!";
				break;
			case HTTPRequestStates.ConnectionTimedOut:
				text = "Connect - Connection Timed Out!";
				break;
			case HTTPRequestStates.TimedOut:
				text = "Connect - Processing the request Timed Out!";
				break;
			}
			if (!string.IsNullOrEmpty(text))
			{
				base.Connection.Error(text);
			}
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x000A8CBC File Offset: 0x000A6EBC
		private void OnPollRequestFinished(HTTPRequest req, HTTPResponse resp)
		{
			if (req.State == HTTPRequestStates.Aborted)
			{
				HTTPManager.Logger.Warning("Transport - " + base.Name, "Poll - Request Aborted!");
				return;
			}
			this.pollRequest = null;
			string text = string.Empty;
			switch (req.State)
			{
			case HTTPRequestStates.Finished:
				if (resp.IsSuccess)
				{
					HTTPManager.Logger.Information("Transport - " + base.Name, "Poll - Request Finished Successfully! " + resp.DataAsText);
					IServerMessage serverMessage = TransportBase.Parse(base.Connection.JsonEncoder, resp.DataAsText);
					if (serverMessage != null)
					{
						base.Connection.OnMessage(serverMessage);
						MultiMessage multiMessage = serverMessage as MultiMessage;
						if (multiMessage != null && multiMessage.PollDelay != null)
						{
							this.PollDelay = multiMessage.PollDelay.Value;
						}
						this.LastPoll = DateTime.UtcNow;
					}
				}
				else
				{
					text = string.Format("Poll - Request Finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", resp.StatusCode, resp.Message, resp.DataAsText);
				}
				break;
			case HTTPRequestStates.Error:
				text = "Poll - Request Finished with Error! " + ((req.Exception != null) ? (req.Exception.Message + "\n" + req.Exception.StackTrace) : "No Exception");
				break;
			case HTTPRequestStates.ConnectionTimedOut:
				text = "Poll - Connection Timed Out!";
				break;
			case HTTPRequestStates.TimedOut:
				text = "Poll - Processing the request Timed Out!";
				break;
			}
			if (!string.IsNullOrEmpty(text))
			{
				base.Connection.Error(text);
			}
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x000A8E44 File Offset: 0x000A7044
		private void Poll()
		{
			this.pollRequest = new HTTPRequest(base.Connection.BuildUri(RequestTypes.Poll, this), HTTPMethods.Get, true, true, new OnRequestFinishedDelegate(this.OnPollRequestFinished));
			base.Connection.PrepareRequest(this.pollRequest, RequestTypes.Poll);
			this.pollRequest.Timeout = this.PollTimeout;
			this.pollRequest.Send();
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x000A8EA8 File Offset: 0x000A70A8
		void IHeartbeat.OnHeartbeatUpdate(TimeSpan dif)
		{
			TransportStates state = base.State;
			if (state == TransportStates.Started && this.pollRequest == null && DateTime.UtcNow >= this.LastPoll + this.PollDelay + base.Connection.NegotiationResult.LongPollDelay)
			{
				this.Poll();
			}
		}

		// Token: 0x0400147B RID: 5243
		private DateTime LastPoll;

		// Token: 0x0400147C RID: 5244
		private TimeSpan PollDelay;

		// Token: 0x0400147D RID: 5245
		private TimeSpan PollTimeout;

		// Token: 0x0400147E RID: 5246
		private HTTPRequest pollRequest;
	}
}
