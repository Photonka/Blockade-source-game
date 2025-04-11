using System;
using System.Collections.Generic;
using System.Threading;
using BestHTTP.Extensions;
using BestHTTP.SocketIO.Events;
using BestHTTP.SocketIO.JsonEncoders;
using BestHTTP.SocketIO.Transports;

namespace BestHTTP.SocketIO
{
	// Token: 0x020001BE RID: 446
	public sealed class SocketManager : IHeartbeat, IManager
	{
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060010C8 RID: 4296 RVA: 0x000A22D2 File Offset: 0x000A04D2
		// (set) Token: 0x060010C9 RID: 4297 RVA: 0x000A22DA File Offset: 0x000A04DA
		public SocketManager.States State
		{
			get
			{
				return this.state;
			}
			private set
			{
				this.PreviousState = this.state;
				this.state = value;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060010CA RID: 4298 RVA: 0x000A22EF File Offset: 0x000A04EF
		// (set) Token: 0x060010CB RID: 4299 RVA: 0x000A22F7 File Offset: 0x000A04F7
		public SocketOptions Options { get; private set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x000A2300 File Offset: 0x000A0500
		// (set) Token: 0x060010CD RID: 4301 RVA: 0x000A2308 File Offset: 0x000A0508
		public Uri Uri { get; private set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x000A2311 File Offset: 0x000A0511
		// (set) Token: 0x060010CF RID: 4303 RVA: 0x000A2319 File Offset: 0x000A0519
		public HandshakeData Handshake { get; private set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060010D0 RID: 4304 RVA: 0x000A2322 File Offset: 0x000A0522
		// (set) Token: 0x060010D1 RID: 4305 RVA: 0x000A232A File Offset: 0x000A052A
		public ITransport Transport { get; private set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060010D2 RID: 4306 RVA: 0x000A2333 File Offset: 0x000A0533
		// (set) Token: 0x060010D3 RID: 4307 RVA: 0x000A233B File Offset: 0x000A053B
		public ulong RequestCounter { get; internal set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x000A2344 File Offset: 0x000A0544
		public Socket Socket
		{
			get
			{
				return this.GetSocket();
			}
		}

		// Token: 0x1700019D RID: 413
		public Socket this[string nsp]
		{
			get
			{
				return this.GetSocket(nsp);
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x000A2355 File Offset: 0x000A0555
		// (set) Token: 0x060010D7 RID: 4311 RVA: 0x000A235D File Offset: 0x000A055D
		public int ReconnectAttempts { get; private set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060010D8 RID: 4312 RVA: 0x000A2366 File Offset: 0x000A0566
		// (set) Token: 0x060010D9 RID: 4313 RVA: 0x000A236E File Offset: 0x000A056E
		public IJsonEncoder Encoder { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x000A2378 File Offset: 0x000A0578
		internal uint Timestamp
		{
			get
			{
				return (uint)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x000A23A7 File Offset: 0x000A05A7
		internal int NextAckId
		{
			get
			{
				return Interlocked.Increment(ref this.nextAckId);
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x000A23B4 File Offset: 0x000A05B4
		// (set) Token: 0x060010DD RID: 4317 RVA: 0x000A23BC File Offset: 0x000A05BC
		internal SocketManager.States PreviousState { get; private set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x000A23C5 File Offset: 0x000A05C5
		// (set) Token: 0x060010DF RID: 4319 RVA: 0x000A23CD File Offset: 0x000A05CD
		internal ITransport UpgradingTransport { get; set; }

		// Token: 0x060010E0 RID: 4320 RVA: 0x000A23D6 File Offset: 0x000A05D6
		public SocketManager(Uri uri) : this(uri, new SocketOptions())
		{
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x000A23E4 File Offset: 0x000A05E4
		public SocketManager(Uri uri, SocketOptions options)
		{
			this.Uri = uri;
			this.Options = options;
			this.State = SocketManager.States.Initial;
			this.PreviousState = SocketManager.States.Initial;
			this.Encoder = SocketManager.DefaultEncoder;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x000A243F File Offset: 0x000A063F
		public Socket GetSocket()
		{
			return this.GetSocket("/");
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x000A244C File Offset: 0x000A064C
		public Socket GetSocket(string nsp)
		{
			if (string.IsNullOrEmpty(nsp))
			{
				throw new ArgumentNullException("Namespace parameter is null or empty!");
			}
			Socket socket = null;
			if (!this.Namespaces.TryGetValue(nsp, out socket))
			{
				socket = new Socket(nsp, this);
				this.Namespaces.Add(nsp, socket);
				this.Sockets.Add(socket);
				((ISocket)socket).Open();
			}
			return socket;
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x000A24A6 File Offset: 0x000A06A6
		void IManager.Remove(Socket socket)
		{
			this.Namespaces.Remove(socket.Namespace);
			this.Sockets.Remove(socket);
			if (this.Sockets.Count == 0)
			{
				this.Close();
			}
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x000A24DC File Offset: 0x000A06DC
		public void Open()
		{
			if (this.State != SocketManager.States.Initial && this.State != SocketManager.States.Closed && this.State != SocketManager.States.Reconnecting)
			{
				return;
			}
			HTTPManager.Logger.Information("SocketManager", "Opening");
			this.ReconnectAt = DateTime.MinValue;
			TransportTypes connectWith = this.Options.ConnectWith;
			if (connectWith != TransportTypes.Polling)
			{
				if (connectWith == TransportTypes.WebSocket)
				{
					this.Transport = new WebSocketTransport(this);
				}
			}
			else
			{
				this.Transport = new PollingTransport(this);
			}
			this.Transport.Open();
			((IManager)this).EmitEvent("connecting", Array.Empty<object>());
			this.State = SocketManager.States.Opening;
			this.ConnectionStarted = DateTime.UtcNow;
			HTTPManager.Heartbeats.Subscribe(this);
			this.GetSocket("/");
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x000A2596 File Offset: 0x000A0796
		public void Close()
		{
			((IManager)this).Close(true);
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x000A25A0 File Offset: 0x000A07A0
		void IManager.Close(bool removeSockets)
		{
			if (this.State == SocketManager.States.Closed || this.closing)
			{
				return;
			}
			this.closing = true;
			HTTPManager.Logger.Information("SocketManager", "Closing");
			HTTPManager.Heartbeats.Unsubscribe(this);
			if (removeSockets)
			{
				while (this.Sockets.Count > 0)
				{
					((ISocket)this.Sockets[this.Sockets.Count - 1]).Disconnect(removeSockets);
				}
			}
			else
			{
				for (int i = 0; i < this.Sockets.Count; i++)
				{
					((ISocket)this.Sockets[i]).Disconnect(removeSockets);
				}
			}
			this.State = SocketManager.States.Closed;
			this.LastHeartbeat = DateTime.MinValue;
			if (this.OfflinePackets != null)
			{
				this.OfflinePackets.Clear();
			}
			if (removeSockets)
			{
				this.Namespaces.Clear();
			}
			this.Handshake = null;
			if (this.Transport != null)
			{
				this.Transport.Close();
			}
			this.Transport = null;
			this.closing = false;
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x000A269C File Offset: 0x000A089C
		void IManager.TryToReconnect()
		{
			if (this.State == SocketManager.States.Reconnecting || this.State == SocketManager.States.Closed)
			{
				return;
			}
			if (!this.Options.Reconnection || HTTPManager.IsQuitting)
			{
				this.Close();
				return;
			}
			int num = this.ReconnectAttempts + 1;
			this.ReconnectAttempts = num;
			if (num >= this.Options.ReconnectionAttempts)
			{
				((IManager)this).EmitEvent("reconnect_failed", Array.Empty<object>());
				this.Close();
				return;
			}
			Random random = new Random();
			int num2 = (int)this.Options.ReconnectionDelay.TotalMilliseconds * this.ReconnectAttempts;
			this.ReconnectAt = DateTime.UtcNow + TimeSpan.FromMilliseconds((double)Math.Min(random.Next((int)((float)num2 - (float)num2 * this.Options.RandomizationFactor), (int)((float)num2 + (float)num2 * this.Options.RandomizationFactor)), (int)this.Options.ReconnectionDelayMax.TotalMilliseconds));
			((IManager)this).Close(false);
			this.State = SocketManager.States.Reconnecting;
			for (int i = 0; i < this.Sockets.Count; i++)
			{
				((ISocket)this.Sockets[i]).Open();
			}
			HTTPManager.Heartbeats.Subscribe(this);
			HTTPManager.Logger.Information("SocketManager", "Reconnecting");
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x000A27E0 File Offset: 0x000A09E0
		bool IManager.OnTransportConnected(ITransport trans)
		{
			if (this.State != SocketManager.States.Opening)
			{
				return false;
			}
			if (this.PreviousState == SocketManager.States.Reconnecting)
			{
				((IManager)this).EmitEvent("reconnect", Array.Empty<object>());
			}
			this.State = SocketManager.States.Open;
			this.ReconnectAttempts = 0;
			this.SendOfflinePackets();
			HTTPManager.Logger.Information("SocketManager", "Open");
			if (this.Transport.Type != TransportTypes.WebSocket && this.Handshake.Upgrades.Contains("websocket"))
			{
				this.UpgradingTransport = new WebSocketTransport(this);
				this.UpgradingTransport.Open();
			}
			return true;
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x000A2876 File Offset: 0x000A0A76
		void IManager.OnTransportError(ITransport trans, string err)
		{
			((IManager)this).EmitError(SocketIOErrors.Internal, err);
			trans.Close();
			((IManager)this).TryToReconnect();
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x000A288C File Offset: 0x000A0A8C
		void IManager.OnTransportProbed(ITransport trans)
		{
			HTTPManager.Logger.Information("SocketManager", "\"probe\" packet received");
			this.Options.ConnectWith = trans.Type;
			this.State = SocketManager.States.Paused;
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x000A28BA File Offset: 0x000A0ABA
		private ITransport SelectTransport()
		{
			if (this.State != SocketManager.States.Open || this.Transport == null)
			{
				return null;
			}
			if (!this.Transport.IsRequestInProgress)
			{
				return this.Transport;
			}
			return null;
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x000A28E4 File Offset: 0x000A0AE4
		private void SendOfflinePackets()
		{
			ITransport transport = this.SelectTransport();
			if (this.OfflinePackets != null && this.OfflinePackets.Count > 0 && transport != null)
			{
				transport.Send(this.OfflinePackets);
				this.OfflinePackets.Clear();
			}
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x000A2928 File Offset: 0x000A0B28
		void IManager.SendPacket(Packet packet)
		{
			ITransport transport = this.SelectTransport();
			if (transport != null)
			{
				try
				{
					transport.Send(packet);
					return;
				}
				catch (Exception ex)
				{
					((IManager)this).EmitError(SocketIOErrors.Internal, ex.Message + " " + ex.StackTrace);
					return;
				}
			}
			if (this.OfflinePackets == null)
			{
				this.OfflinePackets = new List<Packet>();
			}
			this.OfflinePackets.Add(packet.Clone());
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x000A299C File Offset: 0x000A0B9C
		void IManager.OnPacket(Packet packet)
		{
			if (this.State == SocketManager.States.Closed)
			{
				return;
			}
			switch (packet.TransportEvent)
			{
			case TransportEventTypes.Open:
				if (this.Handshake == null)
				{
					this.Handshake = new HandshakeData();
					if (!this.Handshake.Parse(packet.Payload))
					{
						HTTPManager.Logger.Warning("SocketManager", "Expected handshake data, but wasn't able to pars. Payload: " + packet.Payload);
					}
					((IManager)this).OnTransportConnected(this.Transport);
					return;
				}
				break;
			case TransportEventTypes.Ping:
				((IManager)this).SendPacket(new Packet(TransportEventTypes.Pong, SocketIOEventTypes.Unknown, "/", string.Empty, 0, 0));
				break;
			case TransportEventTypes.Pong:
				this.IsWaitingPong = false;
				break;
			}
			Socket socket = null;
			if (this.Namespaces.TryGetValue(packet.Namespace, out socket))
			{
				((ISocket)socket).OnPacket(packet);
				return;
			}
			HTTPManager.Logger.Warning("SocketManager", "Namespace \"" + packet.Namespace + "\" not found!");
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x000A2A8C File Offset: 0x000A0C8C
		public void EmitAll(string eventName, params object[] args)
		{
			for (int i = 0; i < this.Sockets.Count; i++)
			{
				this.Sockets[i].Emit(eventName, args);
			}
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x000A2AC4 File Offset: 0x000A0CC4
		void IManager.EmitEvent(string eventName, params object[] args)
		{
			Socket socket = null;
			if (this.Namespaces.TryGetValue("/", out socket))
			{
				((ISocket)socket).EmitEvent(eventName, args);
			}
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x000A2AEF File Offset: 0x000A0CEF
		void IManager.EmitEvent(SocketIOEventTypes type, params object[] args)
		{
			((IManager)this).EmitEvent(EventNames.GetNameFor(type), args);
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x000A2AFE File Offset: 0x000A0CFE
		void IManager.EmitError(SocketIOErrors errCode, string msg)
		{
			((IManager)this).EmitEvent(SocketIOEventTypes.Error, new object[]
			{
				new Error(errCode, msg)
			});
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x000A2B18 File Offset: 0x000A0D18
		void IManager.EmitAll(string eventName, params object[] args)
		{
			for (int i = 0; i < this.Sockets.Count; i++)
			{
				((ISocket)this.Sockets[i]).EmitEvent(eventName, args);
			}
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x000A2B50 File Offset: 0x000A0D50
		void IHeartbeat.OnHeartbeatUpdate(TimeSpan dif)
		{
			switch (this.State)
			{
			case SocketManager.States.Opening:
				if (DateTime.UtcNow - this.ConnectionStarted >= this.Options.Timeout)
				{
					((IManager)this).EmitError(SocketIOErrors.Internal, "Connection timed out!");
					((IManager)this).EmitEvent("connect_error", Array.Empty<object>());
					((IManager)this).EmitEvent("connect_timeout", Array.Empty<object>());
					((IManager)this).TryToReconnect();
					return;
				}
				return;
			case SocketManager.States.Open:
				break;
			case SocketManager.States.Paused:
				if (this.Transport.IsRequestInProgress || this.Transport.IsPollingInProgress)
				{
					return;
				}
				this.State = SocketManager.States.Open;
				this.Transport.Close();
				this.Transport = this.UpgradingTransport;
				this.UpgradingTransport = null;
				this.Transport.Send(new Packet(TransportEventTypes.Upgrade, SocketIOEventTypes.Unknown, "/", string.Empty, 0, 0));
				break;
			case SocketManager.States.Reconnecting:
				if (this.ReconnectAt != DateTime.MinValue && DateTime.UtcNow >= this.ReconnectAt)
				{
					((IManager)this).EmitEvent("reconnect_attempt", Array.Empty<object>());
					((IManager)this).EmitEvent("reconnecting", Array.Empty<object>());
					this.Open();
					return;
				}
				return;
			default:
				return;
			}
			ITransport transport = null;
			if (this.Transport != null && this.Transport.State == TransportStates.Open)
			{
				transport = this.Transport;
			}
			if (transport == null || transport.State != TransportStates.Open)
			{
				return;
			}
			transport.Poll();
			this.SendOfflinePackets();
			if (this.LastHeartbeat == DateTime.MinValue)
			{
				this.LastHeartbeat = DateTime.UtcNow;
				return;
			}
			if (!this.IsWaitingPong && DateTime.UtcNow - this.LastHeartbeat > this.Handshake.PingInterval)
			{
				((IManager)this).SendPacket(new Packet(TransportEventTypes.Ping, SocketIOEventTypes.Unknown, "/", string.Empty, 0, 0));
				this.LastHeartbeat = DateTime.UtcNow;
				this.IsWaitingPong = true;
			}
			if (this.IsWaitingPong && DateTime.UtcNow - this.LastHeartbeat > this.Handshake.PingTimeout)
			{
				this.IsWaitingPong = false;
				((IManager)this).TryToReconnect();
			}
		}

		// Token: 0x04001379 RID: 4985
		public static IJsonEncoder DefaultEncoder = new DefaultJSonEncoder();

		// Token: 0x0400137A RID: 4986
		public const int MinProtocolVersion = 4;

		// Token: 0x0400137B RID: 4987
		private SocketManager.States state;

		// Token: 0x04001383 RID: 4995
		private int nextAckId;

		// Token: 0x04001386 RID: 4998
		private Dictionary<string, Socket> Namespaces = new Dictionary<string, Socket>();

		// Token: 0x04001387 RID: 4999
		private List<Socket> Sockets = new List<Socket>();

		// Token: 0x04001388 RID: 5000
		private List<Packet> OfflinePackets;

		// Token: 0x04001389 RID: 5001
		private DateTime LastHeartbeat = DateTime.MinValue;

		// Token: 0x0400138A RID: 5002
		private DateTime ReconnectAt;

		// Token: 0x0400138B RID: 5003
		private DateTime ConnectionStarted;

		// Token: 0x0400138C RID: 5004
		private bool closing;

		// Token: 0x0400138D RID: 5005
		private bool IsWaitingPong;

		// Token: 0x020008C8 RID: 2248
		public enum States
		{
			// Token: 0x0400335A RID: 13146
			Initial,
			// Token: 0x0400335B RID: 13147
			Closed,
			// Token: 0x0400335C RID: 13148
			Opening,
			// Token: 0x0400335D RID: 13149
			Open,
			// Token: 0x0400335E RID: 13150
			Paused,
			// Token: 0x0400335F RID: 13151
			Reconnecting
		}
	}
}
