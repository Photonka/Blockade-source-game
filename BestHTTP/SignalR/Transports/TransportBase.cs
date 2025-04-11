using System;
using System.Collections.Generic;
using BestHTTP.SignalR.JsonEncoders;
using BestHTTP.SignalR.Messages;

namespace BestHTTP.SignalR.Transports
{
	// Token: 0x020001FD RID: 509
	public abstract class TransportBase
	{
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x000A9368 File Offset: 0x000A7568
		// (set) Token: 0x060012E4 RID: 4836 RVA: 0x000A9370 File Offset: 0x000A7570
		public string Name { get; protected set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060012E5 RID: 4837
		public abstract bool SupportsKeepAlive { get; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060012E6 RID: 4838
		public abstract TransportTypes Type { get; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x000A9379 File Offset: 0x000A7579
		// (set) Token: 0x060012E8 RID: 4840 RVA: 0x000A9381 File Offset: 0x000A7581
		public IConnection Connection { get; protected set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x000A938A File Offset: 0x000A758A
		// (set) Token: 0x060012EA RID: 4842 RVA: 0x000A9394 File Offset: 0x000A7594
		public TransportStates State
		{
			get
			{
				return this._state;
			}
			protected set
			{
				TransportStates state = this._state;
				this._state = value;
				if (this.OnStateChanged != null)
				{
					this.OnStateChanged(this, state, this._state);
				}
			}
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x060012EB RID: 4843 RVA: 0x000A93CC File Offset: 0x000A75CC
		// (remove) Token: 0x060012EC RID: 4844 RVA: 0x000A9404 File Offset: 0x000A7604
		public event OnTransportStateChangedDelegate OnStateChanged;

		// Token: 0x060012ED RID: 4845 RVA: 0x000A9439 File Offset: 0x000A7639
		public TransportBase(string name, Connection connection)
		{
			this.Name = name;
			this.Connection = connection;
			this.State = TransportStates.Initial;
		}

		// Token: 0x060012EE RID: 4846
		public abstract void Connect();

		// Token: 0x060012EF RID: 4847
		public abstract void Stop();

		// Token: 0x060012F0 RID: 4848
		protected abstract void SendImpl(string json);

		// Token: 0x060012F1 RID: 4849
		protected abstract void Started();

		// Token: 0x060012F2 RID: 4850
		protected abstract void Aborted();

		// Token: 0x060012F3 RID: 4851 RVA: 0x000A9456 File Offset: 0x000A7656
		protected void OnConnected()
		{
			if (this.State != TransportStates.Reconnecting)
			{
				this.Start();
				return;
			}
			this.Connection.TransportReconnected();
			this.Started();
			this.State = TransportStates.Started;
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x000A9480 File Offset: 0x000A7680
		protected void Start()
		{
			HTTPManager.Logger.Information("Transport - " + this.Name, "Sending Start Request");
			this.State = TransportStates.Starting;
			if (this.Connection.Protocol > ProtocolVersions.Protocol_2_0)
			{
				HTTPRequest httprequest = new HTTPRequest(this.Connection.BuildUri(RequestTypes.Start, this), HTTPMethods.Get, true, true, new OnRequestFinishedDelegate(this.OnStartRequestFinished));
				httprequest.Tag = 0;
				httprequest.DisableRetry = true;
				httprequest.Timeout = this.Connection.NegotiationResult.ConnectionTimeout + TimeSpan.FromSeconds(10.0);
				this.Connection.PrepareRequest(httprequest, RequestTypes.Start);
				httprequest.Send();
				return;
			}
			this.State = TransportStates.Started;
			this.Started();
			this.Connection.TransportStarted();
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x000A9550 File Offset: 0x000A7750
		private void OnStartRequestFinished(HTTPRequest req, HTTPResponse resp)
		{
			HTTPRequestStates state = req.State;
			if (state == HTTPRequestStates.Finished)
			{
				if (resp.IsSuccess)
				{
					HTTPManager.Logger.Information("Transport - " + this.Name, "Start - Returned: " + resp.DataAsText);
					string text = this.Connection.ParseResponse(resp.DataAsText);
					if (text != "started")
					{
						this.Connection.Error(string.Format("Expected 'started' response, but '{0}' found!", text));
						return;
					}
					this.State = TransportStates.Started;
					this.Started();
					this.Connection.TransportStarted();
					return;
				}
				else
				{
					HTTPManager.Logger.Warning("Transport - " + this.Name, string.Format("Start - request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2} Uri: {3}", new object[]
					{
						resp.StatusCode,
						resp.Message,
						resp.DataAsText,
						req.CurrentUri
					}));
				}
			}
			HTTPManager.Logger.Information("Transport - " + this.Name, "Start request state: " + req.State.ToString());
			int num = (int)req.Tag;
			if (num++ < 5)
			{
				req.Tag = num;
				req.Send();
				return;
			}
			this.Connection.Error("Failed to send Start request.");
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x000A96B0 File Offset: 0x000A78B0
		public virtual void Abort()
		{
			if (this.State != TransportStates.Started)
			{
				return;
			}
			this.State = TransportStates.Closing;
			HTTPRequest httprequest = new HTTPRequest(this.Connection.BuildUri(RequestTypes.Abort, this), HTTPMethods.Get, true, true, new OnRequestFinishedDelegate(this.OnAbortRequestFinished));
			httprequest.Tag = 0;
			httprequest.DisableRetry = true;
			this.Connection.PrepareRequest(httprequest, RequestTypes.Abort);
			httprequest.Send();
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x000A9718 File Offset: 0x000A7918
		protected void AbortFinished()
		{
			this.State = TransportStates.Closed;
			this.Connection.TransportAborted();
			this.Aborted();
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x000A9734 File Offset: 0x000A7934
		private void OnAbortRequestFinished(HTTPRequest req, HTTPResponse resp)
		{
			HTTPRequestStates state = req.State;
			if (state == HTTPRequestStates.Finished)
			{
				if (resp.IsSuccess)
				{
					HTTPManager.Logger.Information("Transport - " + this.Name, "Abort - Returned: " + resp.DataAsText);
					if (this.State == TransportStates.Closing)
					{
						this.AbortFinished();
						return;
					}
					return;
				}
				else
				{
					HTTPManager.Logger.Warning("Transport - " + this.Name, string.Format("Abort - Handshake request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2} Uri: {3}", new object[]
					{
						resp.StatusCode,
						resp.Message,
						resp.DataAsText,
						req.CurrentUri
					}));
				}
			}
			HTTPManager.Logger.Information("Transport - " + this.Name, "Abort request state: " + req.State.ToString());
			int num = (int)req.Tag;
			if (num++ < 5)
			{
				req.Tag = num;
				req.Send();
				return;
			}
			this.Connection.Error("Failed to send Abort request!");
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x000A9858 File Offset: 0x000A7A58
		public void Send(string jsonStr)
		{
			try
			{
				HTTPManager.Logger.Information("Transport - " + this.Name, "Sending: " + jsonStr);
				this.SendImpl(jsonStr);
			}
			catch (Exception ex)
			{
				HTTPManager.Logger.Exception("Transport - " + this.Name, "Send", ex);
			}
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x000A98C8 File Offset: 0x000A7AC8
		public void Reconnect()
		{
			HTTPManager.Logger.Information("Transport - " + this.Name, "Reconnecting");
			this.Stop();
			this.State = TransportStates.Reconnecting;
			this.Connect();
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x000A98FC File Offset: 0x000A7AFC
		public static IServerMessage Parse(IJsonEncoder encoder, string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				HTTPManager.Logger.Error("MessageFactory", "Parse - called with empty or null string!");
				return null;
			}
			if (json.Length == 2 && json == "{}")
			{
				return new KeepAliveMessage();
			}
			IDictionary<string, object> dictionary = null;
			try
			{
				dictionary = encoder.DecodeMessage(json);
			}
			catch (Exception ex)
			{
				HTTPManager.Logger.Exception("MessageFactory", "Parse - encoder.DecodeMessage", ex);
				return null;
			}
			if (dictionary == null)
			{
				HTTPManager.Logger.Error("MessageFactory", "Parse - Json Decode failed for json string: \"" + json + "\"");
				return null;
			}
			IServerMessage serverMessage = null;
			if (!dictionary.ContainsKey("C"))
			{
				if (!dictionary.ContainsKey("E"))
				{
					serverMessage = new ResultMessage();
				}
				else
				{
					serverMessage = new FailureMessage();
				}
			}
			else
			{
				serverMessage = new MultiMessage();
			}
			try
			{
				serverMessage.Parse(dictionary);
			}
			catch
			{
				HTTPManager.Logger.Error("MessageFactory", "Can't parse msg: " + json);
				throw;
			}
			return serverMessage;
		}

		// Token: 0x04001481 RID: 5249
		private const int MaxRetryCount = 5;

		// Token: 0x04001484 RID: 5252
		public TransportStates _state;
	}
}
