using System;
using System.Collections.Generic;
using BestHTTP.JSON;
using BestHTTP.SocketIO.Events;

namespace BestHTTP.SocketIO
{
	// Token: 0x020001BD RID: 445
	public sealed class Socket : ISocket
	{
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x000A1B22 File Offset: 0x0009FD22
		// (set) Token: 0x060010A6 RID: 4262 RVA: 0x000A1B2A File Offset: 0x0009FD2A
		public SocketManager Manager { get; private set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x000A1B33 File Offset: 0x0009FD33
		// (set) Token: 0x060010A8 RID: 4264 RVA: 0x000A1B3B File Offset: 0x0009FD3B
		public string Namespace { get; private set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x000A1B44 File Offset: 0x0009FD44
		// (set) Token: 0x060010AA RID: 4266 RVA: 0x000A1B4C File Offset: 0x0009FD4C
		public string Id { get; private set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060010AB RID: 4267 RVA: 0x000A1B55 File Offset: 0x0009FD55
		// (set) Token: 0x060010AC RID: 4268 RVA: 0x000A1B5D File Offset: 0x0009FD5D
		public bool IsOpen { get; private set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x000A1B66 File Offset: 0x0009FD66
		// (set) Token: 0x060010AE RID: 4270 RVA: 0x000A1B6E File Offset: 0x0009FD6E
		public bool AutoDecodePayload { get; set; }

		// Token: 0x060010AF RID: 4271 RVA: 0x000A1B77 File Offset: 0x0009FD77
		internal Socket(string nsp, SocketManager manager)
		{
			this.Namespace = nsp;
			this.Manager = manager;
			this.IsOpen = false;
			this.AutoDecodePayload = true;
			this.EventCallbacks = new EventTable(this);
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x000A1BB4 File Offset: 0x0009FDB4
		void ISocket.Open()
		{
			if (this.Manager.State == SocketManager.States.Open)
			{
				this.OnTransportOpen(this.Manager.Socket, null, Array.Empty<object>());
				return;
			}
			this.Manager.Socket.Off("connect", new SocketIOCallback(this.OnTransportOpen));
			this.Manager.Socket.On("connect", new SocketIOCallback(this.OnTransportOpen));
			if (this.Manager.Options.AutoConnect && this.Manager.State == SocketManager.States.Initial)
			{
				this.Manager.Open();
			}
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x000A1C53 File Offset: 0x0009FE53
		public void Disconnect()
		{
			((ISocket)this).Disconnect(true);
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x000A1C5C File Offset: 0x0009FE5C
		void ISocket.Disconnect(bool remove)
		{
			if (this.IsOpen)
			{
				Packet packet = new Packet(TransportEventTypes.Message, SocketIOEventTypes.Disconnect, this.Namespace, string.Empty, 0, 0);
				((IManager)this.Manager).SendPacket(packet);
				this.IsOpen = false;
				((ISocket)this).OnPacket(packet);
			}
			if (this.AckCallbacks != null)
			{
				this.AckCallbacks.Clear();
			}
			if (remove)
			{
				this.EventCallbacks.Clear();
				((IManager)this.Manager).Remove(this);
			}
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x000A1CCD File Offset: 0x0009FECD
		public Socket Emit(string eventName, params object[] args)
		{
			return this.Emit(eventName, null, args);
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x000A1CD8 File Offset: 0x0009FED8
		public Socket Emit(string eventName, SocketIOAckCallback callback, params object[] args)
		{
			if (EventNames.IsBlacklisted(eventName))
			{
				throw new ArgumentException("Blacklisted event: " + eventName);
			}
			this.arguments.Clear();
			this.arguments.Add(eventName);
			List<byte[]> list = null;
			if (args != null && args.Length != 0)
			{
				int num = 0;
				for (int i = 0; i < args.Length; i++)
				{
					byte[] array = args[i] as byte[];
					if (array != null)
					{
						if (list == null)
						{
							list = new List<byte[]>();
						}
						Dictionary<string, object> dictionary = new Dictionary<string, object>(2);
						dictionary.Add("_placeholder", true);
						dictionary.Add("num", num++);
						this.arguments.Add(dictionary);
						list.Add(array);
					}
					else
					{
						this.arguments.Add(args[i]);
					}
				}
			}
			string text = null;
			try
			{
				text = this.Manager.Encoder.Encode(this.arguments);
			}
			catch (Exception ex)
			{
				((ISocket)this).EmitError(SocketIOErrors.Internal, "Error while encoding payload: " + ex.Message + " " + ex.StackTrace);
				return this;
			}
			this.arguments.Clear();
			if (text == null)
			{
				throw new ArgumentException("Encoding the arguments to JSON failed!");
			}
			int num2 = 0;
			if (callback != null)
			{
				num2 = this.Manager.NextAckId;
				if (this.AckCallbacks == null)
				{
					this.AckCallbacks = new Dictionary<int, SocketIOAckCallback>();
				}
				this.AckCallbacks[num2] = callback;
			}
			Packet packet = new Packet(TransportEventTypes.Message, (list == null) ? SocketIOEventTypes.Event : SocketIOEventTypes.BinaryEvent, this.Namespace, text, 0, num2);
			if (list != null)
			{
				packet.Attachments = list;
			}
			((IManager)this.Manager).SendPacket(packet);
			return this;
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x000A1E7C File Offset: 0x000A007C
		public Socket EmitAck(Packet originalPacket, params object[] args)
		{
			if (originalPacket == null)
			{
				throw new ArgumentNullException("originalPacket == null!");
			}
			if (originalPacket.SocketIOEvent != SocketIOEventTypes.Event && originalPacket.SocketIOEvent != SocketIOEventTypes.BinaryEvent)
			{
				throw new ArgumentException("Wrong packet - you can't send an Ack for a packet with id == 0 and SocketIOEvent != Event or SocketIOEvent != BinaryEvent!");
			}
			this.arguments.Clear();
			if (args != null && args.Length != 0)
			{
				this.arguments.AddRange(args);
			}
			string text = null;
			try
			{
				text = this.Manager.Encoder.Encode(this.arguments);
			}
			catch (Exception ex)
			{
				((ISocket)this).EmitError(SocketIOErrors.Internal, "Error while encoding payload: " + ex.Message + " " + ex.StackTrace);
				return this;
			}
			if (text == null)
			{
				throw new ArgumentException("Encoding the arguments to JSON failed!");
			}
			Packet packet = new Packet(TransportEventTypes.Message, (originalPacket.SocketIOEvent == SocketIOEventTypes.Event) ? SocketIOEventTypes.Ack : SocketIOEventTypes.BinaryAck, this.Namespace, text, 0, originalPacket.Id);
			((IManager)this.Manager).SendPacket(packet);
			return this;
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x000A1F64 File Offset: 0x000A0164
		public void On(string eventName, SocketIOCallback callback)
		{
			this.EventCallbacks.Register(eventName, callback, false, this.AutoDecodePayload);
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x000A1F7C File Offset: 0x000A017C
		public void On(SocketIOEventTypes type, SocketIOCallback callback)
		{
			string nameFor = EventNames.GetNameFor(type);
			this.EventCallbacks.Register(nameFor, callback, false, this.AutoDecodePayload);
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x000A1FA4 File Offset: 0x000A01A4
		public void On(string eventName, SocketIOCallback callback, bool autoDecodePayload)
		{
			this.EventCallbacks.Register(eventName, callback, false, autoDecodePayload);
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x000A1FB8 File Offset: 0x000A01B8
		public void On(SocketIOEventTypes type, SocketIOCallback callback, bool autoDecodePayload)
		{
			string nameFor = EventNames.GetNameFor(type);
			this.EventCallbacks.Register(nameFor, callback, false, autoDecodePayload);
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x000A1FDB File Offset: 0x000A01DB
		public void Once(string eventName, SocketIOCallback callback)
		{
			this.EventCallbacks.Register(eventName, callback, true, this.AutoDecodePayload);
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x000A1FF1 File Offset: 0x000A01F1
		public void Once(SocketIOEventTypes type, SocketIOCallback callback)
		{
			this.EventCallbacks.Register(EventNames.GetNameFor(type), callback, true, this.AutoDecodePayload);
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x000A200C File Offset: 0x000A020C
		public void Once(string eventName, SocketIOCallback callback, bool autoDecodePayload)
		{
			this.EventCallbacks.Register(eventName, callback, true, autoDecodePayload);
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x000A201D File Offset: 0x000A021D
		public void Once(SocketIOEventTypes type, SocketIOCallback callback, bool autoDecodePayload)
		{
			this.EventCallbacks.Register(EventNames.GetNameFor(type), callback, true, autoDecodePayload);
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x000A2033 File Offset: 0x000A0233
		public void Off()
		{
			this.EventCallbacks.Clear();
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x000A2040 File Offset: 0x000A0240
		public void Off(string eventName)
		{
			this.EventCallbacks.Unregister(eventName);
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x000A204E File Offset: 0x000A024E
		public void Off(SocketIOEventTypes type)
		{
			this.Off(EventNames.GetNameFor(type));
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x000A205C File Offset: 0x000A025C
		public void Off(string eventName, SocketIOCallback callback)
		{
			this.EventCallbacks.Unregister(eventName, callback);
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x000A206B File Offset: 0x000A026B
		public void Off(SocketIOEventTypes type, SocketIOCallback callback)
		{
			this.EventCallbacks.Unregister(EventNames.GetNameFor(type), callback);
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x000A2080 File Offset: 0x000A0280
		void ISocket.OnPacket(Packet packet)
		{
			switch (packet.SocketIOEvent)
			{
			case SocketIOEventTypes.Connect:
				this.Id = ((this.Namespace != "/") ? (this.Namespace + "#" + this.Manager.Handshake.Sid) : this.Manager.Handshake.Sid);
				break;
			case SocketIOEventTypes.Disconnect:
				if (this.IsOpen)
				{
					this.IsOpen = false;
					this.EventCallbacks.Call(EventNames.GetNameFor(SocketIOEventTypes.Disconnect), packet, Array.Empty<object>());
					this.Disconnect();
				}
				break;
			case SocketIOEventTypes.Error:
			{
				bool flag = false;
				object obj = Json.Decode(packet.Payload, ref flag);
				if (flag)
				{
					Dictionary<string, object> dictionary = obj as Dictionary<string, object>;
					Error error;
					if (dictionary != null && dictionary.ContainsKey("code"))
					{
						error = new Error((SocketIOErrors)Convert.ToInt32(dictionary["code"]), dictionary["message"] as string);
					}
					else
					{
						error = new Error(SocketIOErrors.Custom, packet.Payload);
					}
					this.EventCallbacks.Call(EventNames.GetNameFor(SocketIOEventTypes.Error), packet, new object[]
					{
						error
					});
					return;
				}
				break;
			}
			}
			this.EventCallbacks.Call(packet);
			if ((packet.SocketIOEvent == SocketIOEventTypes.Ack || packet.SocketIOEvent == SocketIOEventTypes.BinaryAck) && this.AckCallbacks != null)
			{
				SocketIOAckCallback socketIOAckCallback = null;
				if (this.AckCallbacks.TryGetValue(packet.Id, out socketIOAckCallback) && socketIOAckCallback != null)
				{
					try
					{
						socketIOAckCallback(this, packet, this.AutoDecodePayload ? packet.Decode(this.Manager.Encoder) : null);
					}
					catch (Exception ex)
					{
						HTTPManager.Logger.Exception("Socket", "ackCallback", ex);
					}
				}
				this.AckCallbacks.Remove(packet.Id);
			}
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x000A2258 File Offset: 0x000A0458
		void ISocket.EmitEvent(SocketIOEventTypes type, params object[] args)
		{
			((ISocket)this).EmitEvent(EventNames.GetNameFor(type), args);
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x000A2267 File Offset: 0x000A0467
		void ISocket.EmitEvent(string eventName, params object[] args)
		{
			if (!string.IsNullOrEmpty(eventName))
			{
				this.EventCallbacks.Call(eventName, null, args);
			}
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x000A227F File Offset: 0x000A047F
		void ISocket.EmitError(SocketIOErrors errCode, string msg)
		{
			((ISocket)this).EmitEvent(SocketIOEventTypes.Error, new object[]
			{
				new Error(errCode, msg)
			});
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x000A2298 File Offset: 0x000A0498
		private void OnTransportOpen(Socket socket, Packet packet, params object[] args)
		{
			if (this.Namespace != "/")
			{
				((IManager)this.Manager).SendPacket(new Packet(TransportEventTypes.Message, SocketIOEventTypes.Connect, this.Namespace, string.Empty, 0, 0));
			}
			this.IsOpen = true;
		}

		// Token: 0x04001376 RID: 4982
		private Dictionary<int, SocketIOAckCallback> AckCallbacks;

		// Token: 0x04001377 RID: 4983
		private EventTable EventCallbacks;

		// Token: 0x04001378 RID: 4984
		private List<object> arguments = new List<object>();
	}
}
