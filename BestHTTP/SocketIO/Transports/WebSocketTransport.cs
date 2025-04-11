using System;
using System.Collections.Generic;
using BestHTTP.Extensions;
using BestHTTP.Logger;
using BestHTTP.WebSocket;

namespace BestHTTP.SocketIO.Transports
{
	// Token: 0x020001C4 RID: 452
	internal sealed class WebSocketTransport : ITransport
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x0006CF70 File Offset: 0x0006B170
		public TransportTypes Type
		{
			get
			{
				return TransportTypes.WebSocket;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x000A39B0 File Offset: 0x000A1BB0
		// (set) Token: 0x0600112B RID: 4395 RVA: 0x000A39B8 File Offset: 0x000A1BB8
		public TransportStates State { get; private set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x000A39C1 File Offset: 0x000A1BC1
		// (set) Token: 0x0600112D RID: 4397 RVA: 0x000A39C9 File Offset: 0x000A1BC9
		public SocketManager Manager { get; private set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public bool IsRequestInProgress
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public bool IsPollingInProgress
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x000A39D2 File Offset: 0x000A1BD2
		// (set) Token: 0x06001131 RID: 4401 RVA: 0x000A39DA File Offset: 0x000A1BDA
		public WebSocket Implementation { get; private set; }

		// Token: 0x06001132 RID: 4402 RVA: 0x000A39E3 File Offset: 0x000A1BE3
		public WebSocketTransport(SocketManager manager)
		{
			this.State = TransportStates.Closed;
			this.Manager = manager;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x000A39FC File Offset: 0x000A1BFC
		public void Open()
		{
			if (this.State != TransportStates.Closed)
			{
				return;
			}
			string text = new UriBuilder(HTTPProtocolFactory.IsSecureProtocol(this.Manager.Uri) ? "wss" : "ws", this.Manager.Uri.Host, this.Manager.Uri.Port, this.Manager.Uri.GetRequestPathAndQueryURL()).Uri.ToString();
			string text2 = "{0}?EIO={1}&transport=websocket{3}";
			if (this.Manager.Handshake != null)
			{
				text2 += "&sid={2}";
			}
			bool flag = !this.Manager.Options.QueryParamsOnlyForHandshake || (this.Manager.Options.QueryParamsOnlyForHandshake && this.Manager.Handshake == null);
			Uri uri = new Uri(string.Format(text2, new object[]
			{
				text,
				4,
				(this.Manager.Handshake != null) ? this.Manager.Handshake.Sid : string.Empty,
				flag ? this.Manager.Options.BuildQueryParams() : string.Empty
			}));
			this.Implementation = new WebSocket(uri);
			this.Implementation.OnOpen = new OnWebSocketOpenDelegate(this.OnOpen);
			this.Implementation.OnMessage = new OnWebSocketMessageDelegate(this.OnMessage);
			this.Implementation.OnBinary = new OnWebSocketBinaryDelegate(this.OnBinary);
			this.Implementation.OnError = new OnWebSocketErrorDelegate(this.OnError);
			this.Implementation.OnClosed = new OnWebSocketClosedDelegate(this.OnClosed);
			this.Implementation.Open();
			this.State = TransportStates.Connecting;
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x000A3BC0 File Offset: 0x000A1DC0
		public void Close()
		{
			if (this.State == TransportStates.Closed)
			{
				return;
			}
			this.State = TransportStates.Closed;
			if (this.Implementation != null)
			{
				this.Implementation.Close();
			}
			else
			{
				HTTPManager.Logger.Warning("WebSocketTransport", "Close - WebSocket Implementation already null!");
			}
			this.Implementation = null;
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00002B75 File Offset: 0x00000D75
		public void Poll()
		{
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x000A3C10 File Offset: 0x000A1E10
		private void OnOpen(WebSocket ws)
		{
			if (ws != this.Implementation)
			{
				return;
			}
			HTTPManager.Logger.Information("WebSocketTransport", "OnOpen");
			this.State = TransportStates.Opening;
			if (this.Manager.UpgradingTransport == this)
			{
				this.Send(new Packet(TransportEventTypes.Ping, SocketIOEventTypes.Unknown, "/", "probe", 0, 0));
			}
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x000A3C6C File Offset: 0x000A1E6C
		private void OnMessage(WebSocket ws, string message)
		{
			if (ws != this.Implementation)
			{
				return;
			}
			if (HTTPManager.Logger.Level <= Loglevels.All)
			{
				HTTPManager.Logger.Verbose("WebSocketTransport", "OnMessage: " + message);
			}
			Packet packet = null;
			try
			{
				packet = new Packet(message);
			}
			catch (Exception ex)
			{
				HTTPManager.Logger.Exception("WebSocketTransport", "OnMessage Packet parsing", ex);
			}
			if (packet == null)
			{
				HTTPManager.Logger.Error("WebSocketTransport", "Message parsing failed. Message: " + message);
				return;
			}
			try
			{
				if (packet.AttachmentCount == 0)
				{
					this.OnPacket(packet);
				}
				else
				{
					this.PacketWithAttachment = packet;
				}
			}
			catch (Exception ex2)
			{
				HTTPManager.Logger.Exception("WebSocketTransport", "OnMessage OnPacket", ex2);
			}
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x000A3D3C File Offset: 0x000A1F3C
		private void OnBinary(WebSocket ws, byte[] data)
		{
			if (ws != this.Implementation)
			{
				return;
			}
			if (HTTPManager.Logger.Level <= Loglevels.All)
			{
				HTTPManager.Logger.Verbose("WebSocketTransport", "OnBinary");
			}
			if (this.PacketWithAttachment != null)
			{
				this.PacketWithAttachment.AddAttachmentFromServer(data, false);
				if (this.PacketWithAttachment.HasAllAttachment)
				{
					try
					{
						this.OnPacket(this.PacketWithAttachment);
					}
					catch (Exception ex)
					{
						HTTPManager.Logger.Exception("WebSocketTransport", "OnBinary", ex);
					}
					finally
					{
						this.PacketWithAttachment = null;
					}
				}
			}
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x000A3DE0 File Offset: 0x000A1FE0
		private void OnError(WebSocket ws, Exception ex)
		{
			if (ws != this.Implementation)
			{
				return;
			}
			string err = string.Empty;
			if (ex != null)
			{
				err = ex.Message + " " + ex.StackTrace;
			}
			else
			{
				switch (ws.InternalRequest.State)
				{
				case HTTPRequestStates.Finished:
					if (ws.InternalRequest.Response.IsSuccess || ws.InternalRequest.Response.StatusCode == 101)
					{
						err = string.Format("Request finished. Status Code: {0} Message: {1}", ws.InternalRequest.Response.StatusCode.ToString(), ws.InternalRequest.Response.Message);
					}
					else
					{
						err = string.Format("Request Finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", ws.InternalRequest.Response.StatusCode, ws.InternalRequest.Response.Message, ws.InternalRequest.Response.DataAsText);
					}
					break;
				case HTTPRequestStates.Error:
					err = (("Request Finished with Error! : " + ws.InternalRequest.Exception != null) ? (ws.InternalRequest.Exception.Message + " " + ws.InternalRequest.Exception.StackTrace) : string.Empty);
					break;
				case HTTPRequestStates.Aborted:
					err = "Request Aborted!";
					break;
				case HTTPRequestStates.ConnectionTimedOut:
					err = "Connection Timed Out!";
					break;
				case HTTPRequestStates.TimedOut:
					err = "Processing the request Timed Out!";
					break;
				}
			}
			if (this.Manager.UpgradingTransport != this)
			{
				((IManager)this.Manager).OnTransportError(this, err);
				return;
			}
			this.Manager.UpgradingTransport = null;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x000A3F74 File Offset: 0x000A2174
		private void OnClosed(WebSocket ws, ushort code, string message)
		{
			if (ws != this.Implementation)
			{
				return;
			}
			HTTPManager.Logger.Information("WebSocketTransport", "OnClosed");
			this.Close();
			if (this.Manager.UpgradingTransport != this)
			{
				((IManager)this.Manager).TryToReconnect();
				return;
			}
			this.Manager.UpgradingTransport = null;
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x000A3FCC File Offset: 0x000A21CC
		public void Send(Packet packet)
		{
			if (this.State == TransportStates.Closed || this.State == TransportStates.Paused)
			{
				return;
			}
			string text = packet.Encode();
			if (HTTPManager.Logger.Level <= Loglevels.All)
			{
				HTTPManager.Logger.Verbose("WebSocketTransport", "Send: " + text);
			}
			if (packet.AttachmentCount != 0 || (packet.Attachments != null && packet.Attachments.Count != 0))
			{
				if (packet.Attachments == null)
				{
					throw new ArgumentException("packet.Attachments are null!");
				}
				if (packet.AttachmentCount != packet.Attachments.Count)
				{
					throw new ArgumentException("packet.AttachmentCount != packet.Attachments.Count. Use the packet.AddAttachment function to add data to a packet!");
				}
			}
			this.Implementation.Send(text);
			if (packet.AttachmentCount != 0)
			{
				int num = packet.Attachments[0].Length + 1;
				for (int i = 1; i < packet.Attachments.Count; i++)
				{
					if (packet.Attachments[i].Length + 1 > num)
					{
						num = packet.Attachments[i].Length + 1;
					}
				}
				if (this.Buffer == null || this.Buffer.Length < num)
				{
					Array.Resize<byte>(ref this.Buffer, num);
				}
				for (int j = 0; j < packet.AttachmentCount; j++)
				{
					this.Buffer[0] = 4;
					Array.Copy(packet.Attachments[j], 0, this.Buffer, 1, packet.Attachments[j].Length);
					this.Implementation.Send(this.Buffer, 0UL, (ulong)((long)packet.Attachments[j].Length + 1L));
				}
			}
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x000A4150 File Offset: 0x000A2350
		public void Send(List<Packet> packets)
		{
			for (int i = 0; i < packets.Count; i++)
			{
				this.Send(packets[i]);
			}
			packets.Clear();
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x000A4184 File Offset: 0x000A2384
		private void OnPacket(Packet packet)
		{
			TransportEventTypes transportEvent = packet.TransportEvent;
			if (transportEvent != TransportEventTypes.Open)
			{
				if (transportEvent == TransportEventTypes.Pong)
				{
					if (packet.Payload == "probe")
					{
						this.State = TransportStates.Open;
						((IManager)this.Manager).OnTransportProbed(this);
					}
				}
			}
			else if (this.State != TransportStates.Opening)
			{
				HTTPManager.Logger.Warning("WebSocketTransport", "Received 'Open' packet while state is '" + this.State.ToString() + "'");
			}
			else
			{
				this.State = TransportStates.Open;
			}
			if (this.Manager.UpgradingTransport != this)
			{
				((IManager)this.Manager).OnPacket(packet);
			}
		}

		// Token: 0x040013AB RID: 5035
		private Packet PacketWithAttachment;

		// Token: 0x040013AC RID: 5036
		private byte[] Buffer;
	}
}
