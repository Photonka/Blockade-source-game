using System;
using System.Collections.Generic;
using System.Text;
using BestHTTP.Extensions;
using BestHTTP.JSON;
using BestHTTP.Logger;
using BestHTTP.SignalR.Authentication;
using BestHTTP.SignalR.Hubs;
using BestHTTP.SignalR.JsonEncoders;
using BestHTTP.SignalR.Messages;
using BestHTTP.SignalR.Transports;
using PlatformSupport.Collections.ObjectModel;
using PlatformSupport.Collections.Specialized;

namespace BestHTTP.SignalR
{
	// Token: 0x020001F2 RID: 498
	public sealed class Connection : IHeartbeat, IConnection
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x000A6AF0 File Offset: 0x000A4CF0
		// (set) Token: 0x06001258 RID: 4696 RVA: 0x000A6AF8 File Offset: 0x000A4CF8
		public Uri Uri { get; private set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x000A6B01 File Offset: 0x000A4D01
		// (set) Token: 0x0600125A RID: 4698 RVA: 0x000A6B0C File Offset: 0x000A4D0C
		public ConnectionStates State
		{
			get
			{
				return this._state;
			}
			private set
			{
				ConnectionStates state = this._state;
				this._state = value;
				if (this.OnStateChanged != null)
				{
					this.OnStateChanged(this, state, this._state);
				}
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x000A6B42 File Offset: 0x000A4D42
		// (set) Token: 0x0600125C RID: 4700 RVA: 0x000A6B4A File Offset: 0x000A4D4A
		public NegotiationData NegotiationResult { get; private set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x000A6B53 File Offset: 0x000A4D53
		// (set) Token: 0x0600125E RID: 4702 RVA: 0x000A6B5B File Offset: 0x000A4D5B
		public Hub[] Hubs { get; private set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x000A6B64 File Offset: 0x000A4D64
		// (set) Token: 0x06001260 RID: 4704 RVA: 0x000A6B6C File Offset: 0x000A4D6C
		public TransportBase Transport { get; private set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x000A6B75 File Offset: 0x000A4D75
		// (set) Token: 0x06001262 RID: 4706 RVA: 0x000A6B7D File Offset: 0x000A4D7D
		public ProtocolVersions Protocol { get; private set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x000A6B86 File Offset: 0x000A4D86
		// (set) Token: 0x06001264 RID: 4708 RVA: 0x000A6B90 File Offset: 0x000A4D90
		public ObservableDictionary<string, string> AdditionalQueryParams
		{
			get
			{
				return this.additionalQueryParams;
			}
			set
			{
				if (this.additionalQueryParams != null)
				{
					this.additionalQueryParams.CollectionChanged -= this.AdditionalQueryParams_CollectionChanged;
				}
				this.additionalQueryParams = value;
				this.BuiltQueryParams = null;
				if (value != null)
				{
					value.CollectionChanged += this.AdditionalQueryParams_CollectionChanged;
				}
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x000A6BDF File Offset: 0x000A4DDF
		// (set) Token: 0x06001266 RID: 4710 RVA: 0x000A6BE7 File Offset: 0x000A4DE7
		public bool QueryParamsOnlyForHandshake { get; set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x000A6BF0 File Offset: 0x000A4DF0
		// (set) Token: 0x06001268 RID: 4712 RVA: 0x000A6BF8 File Offset: 0x000A4DF8
		public IJsonEncoder JsonEncoder { get; set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x000A6C01 File Offset: 0x000A4E01
		// (set) Token: 0x0600126A RID: 4714 RVA: 0x000A6C09 File Offset: 0x000A4E09
		public IAuthenticationProvider AuthenticationProvider { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x000A6C12 File Offset: 0x000A4E12
		// (set) Token: 0x0600126C RID: 4716 RVA: 0x000A6C1A File Offset: 0x000A4E1A
		public TimeSpan PingInterval { get; set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x000A6C23 File Offset: 0x000A4E23
		// (set) Token: 0x0600126E RID: 4718 RVA: 0x000A6C2B File Offset: 0x000A4E2B
		public TimeSpan ReconnectDelay { get; set; }

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x0600126F RID: 4719 RVA: 0x000A6C34 File Offset: 0x000A4E34
		// (remove) Token: 0x06001270 RID: 4720 RVA: 0x000A6C6C File Offset: 0x000A4E6C
		public event OnConnectedDelegate OnConnected;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06001271 RID: 4721 RVA: 0x000A6CA4 File Offset: 0x000A4EA4
		// (remove) Token: 0x06001272 RID: 4722 RVA: 0x000A6CDC File Offset: 0x000A4EDC
		public event OnClosedDelegate OnClosed;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06001273 RID: 4723 RVA: 0x000A6D14 File Offset: 0x000A4F14
		// (remove) Token: 0x06001274 RID: 4724 RVA: 0x000A6D4C File Offset: 0x000A4F4C
		public event OnErrorDelegate OnError;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06001275 RID: 4725 RVA: 0x000A6D84 File Offset: 0x000A4F84
		// (remove) Token: 0x06001276 RID: 4726 RVA: 0x000A6DBC File Offset: 0x000A4FBC
		public event OnConnectedDelegate OnReconnecting;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06001277 RID: 4727 RVA: 0x000A6DF4 File Offset: 0x000A4FF4
		// (remove) Token: 0x06001278 RID: 4728 RVA: 0x000A6E2C File Offset: 0x000A502C
		public event OnConnectedDelegate OnReconnected;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06001279 RID: 4729 RVA: 0x000A6E64 File Offset: 0x000A5064
		// (remove) Token: 0x0600127A RID: 4730 RVA: 0x000A6E9C File Offset: 0x000A509C
		public event OnStateChanged OnStateChanged;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x0600127B RID: 4731 RVA: 0x000A6ED4 File Offset: 0x000A50D4
		// (remove) Token: 0x0600127C RID: 4732 RVA: 0x000A6F0C File Offset: 0x000A510C
		public event OnNonHubMessageDelegate OnNonHubMessage;

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600127D RID: 4733 RVA: 0x000A6F41 File Offset: 0x000A5141
		// (set) Token: 0x0600127E RID: 4734 RVA: 0x000A6F49 File Offset: 0x000A5149
		public OnPrepareRequestDelegate RequestPreparator { get; set; }

		// Token: 0x170001FF RID: 511
		public Hub this[int idx]
		{
			get
			{
				return this.Hubs[idx];
			}
		}

		// Token: 0x17000200 RID: 512
		public Hub this[string hubName]
		{
			get
			{
				for (int i = 0; i < this.Hubs.Length; i++)
				{
					Hub hub = this.Hubs[i];
					if (hub.Name.Equals(hubName, StringComparison.OrdinalIgnoreCase))
					{
						return hub;
					}
				}
				return null;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x000A6F97 File Offset: 0x000A5197
		// (set) Token: 0x06001282 RID: 4738 RVA: 0x000A6F9F File Offset: 0x000A519F
		internal ulong ClientMessageCounter { get; set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x000A6FA8 File Offset: 0x000A51A8
		private uint Timestamp
		{
			get
			{
				return (uint)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).Ticks;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x000A6FD8 File Offset: 0x000A51D8
		private string ConnectionData
		{
			get
			{
				if (!string.IsNullOrEmpty(this.BuiltConnectionData))
				{
					return this.BuiltConnectionData;
				}
				StringBuilder stringBuilder = new StringBuilder("[", this.Hubs.Length * 4);
				if (this.Hubs != null)
				{
					for (int i = 0; i < this.Hubs.Length; i++)
					{
						stringBuilder.Append("{\"Name\":\"");
						stringBuilder.Append(this.Hubs[i].Name);
						stringBuilder.Append("\"}");
						if (i < this.Hubs.Length - 1)
						{
							stringBuilder.Append(",");
						}
					}
				}
				stringBuilder.Append("]");
				return this.BuiltConnectionData = Uri.EscapeUriString(stringBuilder.ToString());
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x000A7090 File Offset: 0x000A5290
		private string QueryParams
		{
			get
			{
				if (this.AdditionalQueryParams == null || this.AdditionalQueryParams.Count == 0)
				{
					return string.Empty;
				}
				if (!string.IsNullOrEmpty(this.BuiltQueryParams))
				{
					return this.BuiltQueryParams;
				}
				StringBuilder stringBuilder = new StringBuilder(this.AdditionalQueryParams.Count * 4);
				foreach (KeyValuePair<string, string> keyValuePair in this.AdditionalQueryParams)
				{
					stringBuilder.Append("&");
					stringBuilder.Append(keyValuePair.Key);
					if (!string.IsNullOrEmpty(keyValuePair.Value))
					{
						stringBuilder.Append("=");
						stringBuilder.Append(Uri.EscapeDataString(keyValuePair.Value));
					}
				}
				return this.BuiltQueryParams = stringBuilder.ToString();
			}
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x000A7170 File Offset: 0x000A5370
		public Connection(Uri uri, params string[] hubNames) : this(uri)
		{
			if (hubNames != null && hubNames.Length != 0)
			{
				this.Hubs = new Hub[hubNames.Length];
				for (int i = 0; i < hubNames.Length; i++)
				{
					this.Hubs[i] = new Hub(hubNames[i], this);
				}
			}
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x000A71B8 File Offset: 0x000A53B8
		public Connection(Uri uri, params Hub[] hubs) : this(uri)
		{
			this.Hubs = hubs;
			if (hubs != null)
			{
				for (int i = 0; i < hubs.Length; i++)
				{
					((IHub)hubs[i]).Connection = this;
				}
			}
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x000A71F0 File Offset: 0x000A53F0
		public Connection(Uri uri)
		{
			this.State = ConnectionStates.Initial;
			this.Uri = uri;
			this.JsonEncoder = Connection.DefaultEncoder;
			this.PingInterval = TimeSpan.FromMinutes(5.0);
			this.Protocol = ProtocolVersions.Protocol_2_2;
			this.ReconnectDelay = TimeSpan.FromSeconds(5.0);
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x000A7288 File Offset: 0x000A5488
		public void Open()
		{
			if (this.State != ConnectionStates.Initial && this.State != ConnectionStates.Closed)
			{
				return;
			}
			if (this.AuthenticationProvider != null && this.AuthenticationProvider.IsPreAuthRequired)
			{
				this.State = ConnectionStates.Authenticating;
				this.AuthenticationProvider.OnAuthenticationSucceded += this.OnAuthenticationSucceded;
				this.AuthenticationProvider.OnAuthenticationFailed += this.OnAuthenticationFailed;
				this.AuthenticationProvider.StartAuthentication();
				return;
			}
			this.StartImpl();
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x000A7303 File Offset: 0x000A5503
		private void OnAuthenticationSucceded(IAuthenticationProvider provider)
		{
			provider.OnAuthenticationSucceded -= this.OnAuthenticationSucceded;
			provider.OnAuthenticationFailed -= this.OnAuthenticationFailed;
			this.StartImpl();
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x000A732F File Offset: 0x000A552F
		private void OnAuthenticationFailed(IAuthenticationProvider provider, string reason)
		{
			provider.OnAuthenticationSucceded -= this.OnAuthenticationSucceded;
			provider.OnAuthenticationFailed -= this.OnAuthenticationFailed;
			((IConnection)this).Error(reason);
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x000A735C File Offset: 0x000A555C
		private void StartImpl()
		{
			this.State = ConnectionStates.Negotiating;
			this.NegotiationResult = new NegotiationData(this);
			this.NegotiationResult.OnReceived = new Action<NegotiationData>(this.OnNegotiationDataReceived);
			this.NegotiationResult.OnError = new Action<NegotiationData, string>(this.OnNegotiationError);
			this.NegotiationResult.Start();
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x000A73B8 File Offset: 0x000A55B8
		private void OnNegotiationDataReceived(NegotiationData data)
		{
			int num = -1;
			int num2 = 0;
			while (num2 < this.ClientProtocols.Length && num == -1)
			{
				if (data.ProtocolVersion == this.ClientProtocols[num2])
				{
					num = num2;
				}
				num2++;
			}
			if (num == -1)
			{
				num = 2;
				HTTPManager.Logger.Warning("SignalR Connection", "Unknown protocol version: " + data.ProtocolVersion);
			}
			this.Protocol = (ProtocolVersions)num;
			if (data.TryWebSockets)
			{
				this.Transport = new WebSocketTransport(this);
				this.NextProtocolToTry = SupportedProtocols.ServerSentEvents;
			}
			else
			{
				this.Transport = new ServerSentEventsTransport(this);
				this.NextProtocolToTry = SupportedProtocols.HTTP;
			}
			this.State = ConnectionStates.Connecting;
			this.TransportConnectionStartedAt = new DateTime?(DateTime.UtcNow);
			this.Transport.Connect();
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x000A7474 File Offset: 0x000A5674
		private void OnNegotiationError(NegotiationData data, string error)
		{
			((IConnection)this).Error(error);
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x000A7480 File Offset: 0x000A5680
		public void Close()
		{
			if (this.State == ConnectionStates.Closed)
			{
				return;
			}
			this.State = ConnectionStates.Closed;
			this.ReconnectStarted = false;
			this.TransportConnectionStartedAt = null;
			if (this.Transport != null)
			{
				this.Transport.Abort();
				this.Transport = null;
			}
			this.NegotiationResult = null;
			HTTPManager.Heartbeats.Unsubscribe(this);
			this.LastReceivedMessage = null;
			if (this.Hubs != null)
			{
				for (int i = 0; i < this.Hubs.Length; i++)
				{
					((IHub)this.Hubs[i]).Close();
				}
			}
			if (this.BufferedMessages != null)
			{
				this.BufferedMessages.Clear();
				this.BufferedMessages = null;
			}
			if (this.OnClosed != null)
			{
				try
				{
					this.OnClosed(this);
				}
				catch (Exception ex)
				{
					HTTPManager.Logger.Exception("SignalR Connection", "OnClosed", ex);
				}
			}
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x000A7564 File Offset: 0x000A5764
		public void Reconnect()
		{
			if (this.ReconnectStarted)
			{
				return;
			}
			this.ReconnectStarted = true;
			if (this.State != ConnectionStates.Reconnecting)
			{
				this.ReconnectStartedAt = DateTime.UtcNow;
			}
			this.State = ConnectionStates.Reconnecting;
			HTTPManager.Logger.Warning("SignalR Connection", "Reconnecting");
			this.Transport.Reconnect();
			if (this.PingRequest != null)
			{
				this.PingRequest.Abort();
			}
			if (this.OnReconnecting != null)
			{
				try
				{
					this.OnReconnecting(this);
				}
				catch (Exception ex)
				{
					HTTPManager.Logger.Exception("SignalR Connection", "OnReconnecting", ex);
				}
			}
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x000A760C File Offset: 0x000A580C
		public bool Send(object arg)
		{
			if (arg == null)
			{
				throw new ArgumentNullException("arg");
			}
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				if (this.State != ConnectionStates.Connected)
				{
					return false;
				}
				string text = this.JsonEncoder.Encode(arg);
				if (string.IsNullOrEmpty(text))
				{
					HTTPManager.Logger.Error("SignalR Connection", "Failed to JSon encode the given argument. Please try to use an advanced JSon encoder(check the documentation how you can do it).");
				}
				else
				{
					this.Transport.Send(text);
				}
			}
			return true;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x000A769C File Offset: 0x000A589C
		public bool SendJson(string json)
		{
			if (json == null)
			{
				throw new ArgumentNullException("json");
			}
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				if (this.State != ConnectionStates.Connected)
				{
					return false;
				}
				this.Transport.Send(json);
			}
			return true;
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x000A7700 File Offset: 0x000A5900
		void IConnection.OnMessage(IServerMessage msg)
		{
			if (this.State == ConnectionStates.Closed)
			{
				return;
			}
			if (this.State == ConnectionStates.Connecting)
			{
				if (this.BufferedMessages == null)
				{
					this.BufferedMessages = new List<IServerMessage>();
				}
				this.BufferedMessages.Add(msg);
				return;
			}
			this.LastMessageReceivedAt = DateTime.UtcNow;
			switch (msg.Type)
			{
			case MessageTypes.KeepAlive:
				break;
			case MessageTypes.Data:
				if (this.OnNonHubMessage != null)
				{
					this.OnNonHubMessage(this, (msg as DataMessage).Data);
					return;
				}
				break;
			case MessageTypes.Multiple:
				this.LastReceivedMessage = (msg as MultiMessage);
				if (this.LastReceivedMessage.IsInitialization)
				{
					HTTPManager.Logger.Information("SignalR Connection", "OnMessage - Init");
				}
				if (this.LastReceivedMessage.GroupsToken != null)
				{
					this.GroupsToken = this.LastReceivedMessage.GroupsToken;
				}
				if (this.LastReceivedMessage.ShouldReconnect)
				{
					HTTPManager.Logger.Information("SignalR Connection", "OnMessage - Should Reconnect");
					this.Reconnect();
				}
				if (this.LastReceivedMessage.Data != null)
				{
					for (int i = 0; i < this.LastReceivedMessage.Data.Count; i++)
					{
						((IConnection)this).OnMessage(this.LastReceivedMessage.Data[i]);
					}
					return;
				}
				break;
			case MessageTypes.Result:
			case MessageTypes.Failure:
			case MessageTypes.Progress:
			{
				ulong invocationId = (msg as IHubMessage).InvocationId;
				Hub hub = this.FindHub(invocationId);
				if (hub != null)
				{
					((IHub)hub).OnMessage(msg);
					return;
				}
				HTTPManager.Logger.Warning("SignalR Connection", string.Format("No Hub found for Progress message! Id: {0}", invocationId.ToString()));
				return;
			}
			case MessageTypes.MethodCall:
			{
				MethodCallMessage methodCallMessage = msg as MethodCallMessage;
				Hub hub = this[methodCallMessage.Hub];
				if (hub != null)
				{
					((IHub)hub).OnMethod(methodCallMessage);
					return;
				}
				HTTPManager.Logger.Warning("SignalR Connection", string.Format("Hub \"{0}\" not found!", methodCallMessage.Hub));
				return;
			}
			default:
				HTTPManager.Logger.Warning("SignalR Connection", "Unknown message type received: " + msg.Type.ToString());
				break;
			}
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x000A7900 File Offset: 0x000A5B00
		void IConnection.TransportStarted()
		{
			if (this.State != ConnectionStates.Connecting)
			{
				return;
			}
			this.InitOnStart();
			if (this.OnConnected != null)
			{
				try
				{
					this.OnConnected(this);
				}
				catch (Exception ex)
				{
					HTTPManager.Logger.Exception("SignalR Connection", "OnOpened", ex);
				}
			}
			if (this.BufferedMessages != null)
			{
				for (int i = 0; i < this.BufferedMessages.Count; i++)
				{
					((IConnection)this).OnMessage(this.BufferedMessages[i]);
				}
				this.BufferedMessages.Clear();
				this.BufferedMessages = null;
			}
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x000A79A0 File Offset: 0x000A5BA0
		void IConnection.TransportReconnected()
		{
			if (this.State != ConnectionStates.Reconnecting)
			{
				return;
			}
			HTTPManager.Logger.Information("SignalR Connection", "Transport Reconnected");
			this.InitOnStart();
			if (this.OnReconnected != null)
			{
				try
				{
					this.OnReconnected(this);
				}
				catch (Exception ex)
				{
					HTTPManager.Logger.Exception("SignalR Connection", "OnReconnected", ex);
				}
			}
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x000A7A10 File Offset: 0x000A5C10
		void IConnection.TransportAborted()
		{
			this.Close();
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x000A7A18 File Offset: 0x000A5C18
		void IConnection.Error(string reason)
		{
			if (this.State == ConnectionStates.Closed)
			{
				return;
			}
			if (HTTPManager.IsQuitting)
			{
				this.Close();
				return;
			}
			HTTPManager.Logger.Error("SignalR Connection", reason);
			this.ReconnectStarted = false;
			if (this.OnError != null)
			{
				this.OnError(this, reason);
			}
			if (this.State == ConnectionStates.Connected || this.State == ConnectionStates.Reconnecting)
			{
				this.ReconnectDelayStartedAt = DateTime.UtcNow;
				if (this.State != ConnectionStates.Reconnecting)
				{
					this.ReconnectStartedAt = DateTime.UtcNow;
					return;
				}
			}
			else if (this.State != ConnectionStates.Connecting || !this.TryFallbackTransport())
			{
				this.Close();
			}
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x000A7AB2 File Offset: 0x000A5CB2
		Uri IConnection.BuildUri(RequestTypes type)
		{
			return ((IConnection)this).BuildUri(type, null);
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x000A7ABC File Offset: 0x000A5CBC
		Uri IConnection.BuildUri(RequestTypes type, TransportBase transport)
		{
			object syncRoot = this.SyncRoot;
			Uri uri;
			lock (syncRoot)
			{
				this.queryBuilder.Length = 0;
				UriBuilder uriBuilder = new UriBuilder(this.Uri);
				if (!uriBuilder.Path.EndsWith("/"))
				{
					UriBuilder uriBuilder2 = uriBuilder;
					uriBuilder2.Path += "/";
				}
				this.RequestCounter %= ulong.MaxValue;
				ulong requestCounter;
				switch (type)
				{
				case RequestTypes.Negotiate:
				{
					UriBuilder uriBuilder3 = uriBuilder;
					uriBuilder3.Path += "negotiate";
					break;
				}
				case RequestTypes.Connect:
				{
					if (transport != null && transport.Type == TransportTypes.WebSocket)
					{
						uriBuilder.Scheme = (HTTPProtocolFactory.IsSecureProtocol(this.Uri) ? "wss" : "ws");
					}
					UriBuilder uriBuilder4 = uriBuilder;
					uriBuilder4.Path += "connect";
					break;
				}
				case RequestTypes.Start:
				{
					UriBuilder uriBuilder5 = uriBuilder;
					uriBuilder5.Path += "start";
					break;
				}
				case RequestTypes.Poll:
				{
					UriBuilder uriBuilder6 = uriBuilder;
					uriBuilder6.Path += "poll";
					if (this.LastReceivedMessage != null)
					{
						this.queryBuilder.Append("messageId=");
						this.queryBuilder.Append(this.LastReceivedMessage.MessageId);
					}
					if (!string.IsNullOrEmpty(this.GroupsToken))
					{
						if (this.queryBuilder.Length > 0)
						{
							this.queryBuilder.Append("&");
						}
						this.queryBuilder.Append("groupsToken=");
						this.queryBuilder.Append(this.GroupsToken);
					}
					break;
				}
				case RequestTypes.Send:
				{
					UriBuilder uriBuilder7 = uriBuilder;
					uriBuilder7.Path += "send";
					break;
				}
				case RequestTypes.Reconnect:
				{
					if (transport != null && transport.Type == TransportTypes.WebSocket)
					{
						uriBuilder.Scheme = (HTTPProtocolFactory.IsSecureProtocol(this.Uri) ? "wss" : "ws");
					}
					UriBuilder uriBuilder8 = uriBuilder;
					uriBuilder8.Path += "reconnect";
					if (this.LastReceivedMessage != null)
					{
						this.queryBuilder.Append("messageId=");
						this.queryBuilder.Append(this.LastReceivedMessage.MessageId);
					}
					if (!string.IsNullOrEmpty(this.GroupsToken))
					{
						if (this.queryBuilder.Length > 0)
						{
							this.queryBuilder.Append("&");
						}
						this.queryBuilder.Append("groupsToken=");
						this.queryBuilder.Append(this.GroupsToken);
					}
					break;
				}
				case RequestTypes.Abort:
				{
					UriBuilder uriBuilder9 = uriBuilder;
					uriBuilder9.Path += "abort";
					break;
				}
				case RequestTypes.Ping:
				{
					UriBuilder uriBuilder10 = uriBuilder;
					uriBuilder10.Path += "ping";
					this.queryBuilder.Append("&tid=");
					StringBuilder stringBuilder = this.queryBuilder;
					requestCounter = this.RequestCounter;
					this.RequestCounter = requestCounter + 1UL;
					stringBuilder.Append(requestCounter.ToString());
					this.queryBuilder.Append("&_=");
					this.queryBuilder.Append(this.Timestamp.ToString());
					goto IL_45F;
				}
				}
				if (this.queryBuilder.Length > 0)
				{
					this.queryBuilder.Append("&");
				}
				this.queryBuilder.Append("tid=");
				StringBuilder stringBuilder2 = this.queryBuilder;
				requestCounter = this.RequestCounter;
				this.RequestCounter = requestCounter + 1UL;
				stringBuilder2.Append(requestCounter.ToString());
				this.queryBuilder.Append("&_=");
				this.queryBuilder.Append(this.Timestamp.ToString());
				if (transport != null)
				{
					this.queryBuilder.Append("&transport=");
					this.queryBuilder.Append(transport.Name);
				}
				this.queryBuilder.Append("&clientProtocol=");
				this.queryBuilder.Append(this.ClientProtocols[(int)this.Protocol]);
				if (this.NegotiationResult != null && !string.IsNullOrEmpty(this.NegotiationResult.ConnectionToken))
				{
					this.queryBuilder.Append("&connectionToken=");
					this.queryBuilder.Append(this.NegotiationResult.ConnectionToken);
				}
				if (this.Hubs != null && this.Hubs.Length != 0)
				{
					this.queryBuilder.Append("&connectionData=");
					this.queryBuilder.Append(this.ConnectionData);
				}
				IL_45F:
				if (this.AdditionalQueryParams != null && this.AdditionalQueryParams.Count > 0)
				{
					this.queryBuilder.Append(this.QueryParams);
				}
				uriBuilder.Query = this.queryBuilder.ToString();
				this.queryBuilder.Length = 0;
				uri = uriBuilder.Uri;
			}
			return uri;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x000A7FA0 File Offset: 0x000A61A0
		HTTPRequest IConnection.PrepareRequest(HTTPRequest req, RequestTypes type)
		{
			if (req != null && this.AuthenticationProvider != null)
			{
				this.AuthenticationProvider.PrepareRequest(req, type);
			}
			if (this.RequestPreparator != null)
			{
				this.RequestPreparator(this, req, type);
			}
			return req;
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x000A7FD4 File Offset: 0x000A61D4
		string IConnection.ParseResponse(string responseStr)
		{
			Dictionary<string, object> dictionary = Json.Decode(responseStr) as Dictionary<string, object>;
			if (dictionary == null)
			{
				((IConnection)this).Error("Failed to parse Start response: " + responseStr);
				return string.Empty;
			}
			object obj;
			if (!dictionary.TryGetValue("Response", out obj) || obj == null)
			{
				((IConnection)this).Error("No 'Response' key found in response: " + responseStr);
				return string.Empty;
			}
			return obj.ToString();
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x000A8038 File Offset: 0x000A6238
		void IHeartbeat.OnHeartbeatUpdate(TimeSpan dif)
		{
			ConnectionStates state = this.State;
			if (state != ConnectionStates.Connected)
			{
				if (state != ConnectionStates.Reconnecting)
				{
					if (this.TransportConnectionStartedAt != null && DateTime.UtcNow - this.TransportConnectionStartedAt >= this.NegotiationResult.TransportConnectTimeout)
					{
						HTTPManager.Logger.Warning("SignalR Connection", "OnHeartbeatUpdate - Transport failed to connect in the given time!");
						((IConnection)this).Error("Transport failed to connect in the given time!");
					}
				}
				else
				{
					if (DateTime.UtcNow - this.ReconnectStartedAt >= this.NegotiationResult.DisconnectTimeout)
					{
						HTTPManager.Logger.Warning("SignalR Connection", "OnHeartbeatUpdate - Failed to reconnect in the given time!");
						this.Close();
						return;
					}
					if (DateTime.UtcNow - this.ReconnectDelayStartedAt >= this.ReconnectDelay)
					{
						if (HTTPManager.Logger.Level <= Loglevels.Warning)
						{
							HTTPManager.Logger.Warning("SignalR Connection", string.Concat(new string[]
							{
								this.ReconnectStarted.ToString(),
								" ",
								this.ReconnectStartedAt.ToString(),
								" ",
								this.NegotiationResult.DisconnectTimeout.ToString()
							}));
						}
						this.Reconnect();
						return;
					}
				}
			}
			else
			{
				if (this.Transport.SupportsKeepAlive && this.NegotiationResult.KeepAliveTimeout != null && DateTime.UtcNow - this.LastMessageReceivedAt >= this.NegotiationResult.KeepAliveTimeout)
				{
					this.Reconnect();
				}
				if (this.PingRequest == null && DateTime.UtcNow - this.LastPingSentAt >= this.PingInterval)
				{
					this.Ping();
					return;
				}
			}
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x000A824A File Offset: 0x000A644A
		private void InitOnStart()
		{
			this.State = ConnectionStates.Connected;
			this.ReconnectStarted = false;
			this.TransportConnectionStartedAt = null;
			this.LastPingSentAt = DateTime.UtcNow;
			this.LastMessageReceivedAt = DateTime.UtcNow;
			HTTPManager.Heartbeats.Subscribe(this);
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x000A8288 File Offset: 0x000A6488
		private Hub FindHub(ulong msgId)
		{
			if (this.Hubs != null)
			{
				for (int i = 0; i < this.Hubs.Length; i++)
				{
					if (((IHub)this.Hubs[i]).HasSentMessageId(msgId))
					{
						return this.Hubs[i];
					}
				}
			}
			return null;
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x000A82CC File Offset: 0x000A64CC
		private bool TryFallbackTransport()
		{
			if (this.State == ConnectionStates.Connecting)
			{
				if (this.BufferedMessages != null)
				{
					this.BufferedMessages.Clear();
				}
				this.Transport.Stop();
				this.Transport = null;
				switch (this.NextProtocolToTry)
				{
				case SupportedProtocols.Unknown:
					return false;
				case SupportedProtocols.HTTP:
					this.Transport = new PollingTransport(this);
					this.NextProtocolToTry = SupportedProtocols.Unknown;
					break;
				case SupportedProtocols.WebSocket:
					this.Transport = new WebSocketTransport(this);
					break;
				case SupportedProtocols.ServerSentEvents:
					this.Transport = new ServerSentEventsTransport(this);
					this.NextProtocolToTry = SupportedProtocols.HTTP;
					break;
				}
				this.TransportConnectionStartedAt = new DateTime?(DateTime.UtcNow);
				this.Transport.Connect();
				if (this.PingRequest != null)
				{
					this.PingRequest.Abort();
				}
				return true;
			}
			return false;
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x000A8394 File Offset: 0x000A6594
		private void AdditionalQueryParams_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			this.BuiltQueryParams = null;
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x000A83A0 File Offset: 0x000A65A0
		private void Ping()
		{
			HTTPManager.Logger.Information("SignalR Connection", "Sending Ping request.");
			this.PingRequest = new HTTPRequest(((IConnection)this).BuildUri(RequestTypes.Ping), new OnRequestFinishedDelegate(this.OnPingRequestFinished));
			this.PingRequest.ConnectTimeout = this.PingInterval;
			((IConnection)this).PrepareRequest(this.PingRequest, RequestTypes.Ping);
			this.PingRequest.Send();
			this.LastPingSentAt = DateTime.UtcNow;
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x000A8418 File Offset: 0x000A6618
		private void OnPingRequestFinished(HTTPRequest req, HTTPResponse resp)
		{
			this.PingRequest = null;
			string text = string.Empty;
			switch (req.State)
			{
			case HTTPRequestStates.Finished:
				if (resp.IsSuccess)
				{
					string text2 = ((IConnection)this).ParseResponse(resp.DataAsText);
					if (text2 != "pong")
					{
						text = "Wrong answer for ping request: " + text2;
					}
					else
					{
						HTTPManager.Logger.Information("SignalR Connection", "Pong received.");
					}
				}
				else
				{
					text = string.Format("Ping - Request Finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", resp.StatusCode, resp.Message, resp.DataAsText);
				}
				break;
			case HTTPRequestStates.Error:
				text = "Ping - Request Finished with Error! " + ((req.Exception != null) ? (req.Exception.Message + "\n" + req.Exception.StackTrace) : "No Exception");
				break;
			case HTTPRequestStates.ConnectionTimedOut:
				text = "Ping - Connection Timed Out!";
				break;
			case HTTPRequestStates.TimedOut:
				text = "Ping - Processing the request Timed Out!";
				break;
			}
			if (!string.IsNullOrEmpty(text))
			{
				((IConnection)this).Error(text);
			}
		}

		// Token: 0x04001420 RID: 5152
		public static IJsonEncoder DefaultEncoder = new DefaultJsonEncoder();

		// Token: 0x04001422 RID: 5154
		private ConnectionStates _state;

		// Token: 0x04001427 RID: 5159
		private ObservableDictionary<string, string> additionalQueryParams;

		// Token: 0x04001435 RID: 5173
		internal object SyncRoot = new object();

		// Token: 0x04001437 RID: 5175
		private readonly string[] ClientProtocols = new string[]
		{
			"1.3",
			"1.4",
			"1.5"
		};

		// Token: 0x04001438 RID: 5176
		private ulong RequestCounter;

		// Token: 0x04001439 RID: 5177
		private MultiMessage LastReceivedMessage;

		// Token: 0x0400143A RID: 5178
		private string GroupsToken;

		// Token: 0x0400143B RID: 5179
		private List<IServerMessage> BufferedMessages;

		// Token: 0x0400143C RID: 5180
		private DateTime LastMessageReceivedAt;

		// Token: 0x0400143D RID: 5181
		private DateTime ReconnectStartedAt;

		// Token: 0x0400143E RID: 5182
		private DateTime ReconnectDelayStartedAt;

		// Token: 0x0400143F RID: 5183
		private bool ReconnectStarted;

		// Token: 0x04001440 RID: 5184
		private DateTime LastPingSentAt;

		// Token: 0x04001441 RID: 5185
		private HTTPRequest PingRequest;

		// Token: 0x04001442 RID: 5186
		private DateTime? TransportConnectionStartedAt;

		// Token: 0x04001443 RID: 5187
		private StringBuilder queryBuilder = new StringBuilder();

		// Token: 0x04001444 RID: 5188
		private string BuiltConnectionData;

		// Token: 0x04001445 RID: 5189
		private string BuiltQueryParams;

		// Token: 0x04001446 RID: 5190
		private SupportedProtocols NextProtocolToTry;
	}
}
