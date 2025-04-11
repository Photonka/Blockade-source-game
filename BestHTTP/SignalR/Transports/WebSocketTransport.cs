using System;
using BestHTTP.SignalR.Messages;
using BestHTTP.WebSocket;

namespace BestHTTP.SignalR.Transports
{
	// Token: 0x020001FE RID: 510
	public sealed class WebSocketTransport : TransportBase
	{
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x0006CF70 File Offset: 0x0006B170
		public override bool SupportsKeepAlive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override TransportTypes Type
		{
			get
			{
				return TransportTypes.WebSocket;
			}
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x000A9A08 File Offset: 0x000A7C08
		public WebSocketTransport(Connection connection) : base("webSockets", connection)
		{
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x000A9A18 File Offset: 0x000A7C18
		public override void Connect()
		{
			if (this.wSocket != null)
			{
				HTTPManager.Logger.Warning("WebSocketTransport", "Start - WebSocket already created!");
				return;
			}
			if (base.State != TransportStates.Reconnecting)
			{
				base.State = TransportStates.Connecting;
			}
			RequestTypes type = (base.State == TransportStates.Reconnecting) ? RequestTypes.Reconnect : RequestTypes.Connect;
			Uri uri = base.Connection.BuildUri(type, this);
			this.wSocket = new WebSocket(uri);
			WebSocket webSocket = this.wSocket;
			webSocket.OnOpen = (OnWebSocketOpenDelegate)Delegate.Combine(webSocket.OnOpen, new OnWebSocketOpenDelegate(this.WSocket_OnOpen));
			WebSocket webSocket2 = this.wSocket;
			webSocket2.OnMessage = (OnWebSocketMessageDelegate)Delegate.Combine(webSocket2.OnMessage, new OnWebSocketMessageDelegate(this.WSocket_OnMessage));
			WebSocket webSocket3 = this.wSocket;
			webSocket3.OnClosed = (OnWebSocketClosedDelegate)Delegate.Combine(webSocket3.OnClosed, new OnWebSocketClosedDelegate(this.WSocket_OnClosed));
			WebSocket webSocket4 = this.wSocket;
			webSocket4.OnErrorDesc = (OnWebSocketErrorDescriptionDelegate)Delegate.Combine(webSocket4.OnErrorDesc, new OnWebSocketErrorDescriptionDelegate(this.WSocket_OnError));
			base.Connection.PrepareRequest(this.wSocket.InternalRequest, type);
			this.wSocket.Open();
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x000A9B39 File Offset: 0x000A7D39
		protected override void SendImpl(string json)
		{
			if (this.wSocket != null && this.wSocket.IsOpen)
			{
				this.wSocket.Send(json);
			}
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x000A9B5C File Offset: 0x000A7D5C
		public override void Stop()
		{
			if (this.wSocket != null)
			{
				this.wSocket.OnOpen = null;
				this.wSocket.OnMessage = null;
				this.wSocket.OnClosed = null;
				this.wSocket.OnErrorDesc = null;
				this.wSocket.Close();
				this.wSocket = null;
			}
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00002B75 File Offset: 0x00000D75
		protected override void Started()
		{
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x000A9BB3 File Offset: 0x000A7DB3
		protected override void Aborted()
		{
			if (this.wSocket != null && this.wSocket.IsOpen)
			{
				this.wSocket.Close();
				this.wSocket = null;
			}
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x000A9BDC File Offset: 0x000A7DDC
		private void WSocket_OnOpen(WebSocket webSocket)
		{
			if (webSocket != this.wSocket)
			{
				return;
			}
			HTTPManager.Logger.Information("WebSocketTransport", "WSocket_OnOpen");
			base.OnConnected();
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x000A9C04 File Offset: 0x000A7E04
		private void WSocket_OnMessage(WebSocket webSocket, string message)
		{
			if (webSocket != this.wSocket)
			{
				return;
			}
			IServerMessage serverMessage = TransportBase.Parse(base.Connection.JsonEncoder, message);
			if (serverMessage != null)
			{
				base.Connection.OnMessage(serverMessage);
			}
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x000A9C3C File Offset: 0x000A7E3C
		private void WSocket_OnClosed(WebSocket webSocket, ushort code, string message)
		{
			if (webSocket != this.wSocket)
			{
				return;
			}
			string text = code.ToString() + " : " + message;
			HTTPManager.Logger.Information("WebSocketTransport", "WSocket_OnClosed " + text);
			if (base.State == TransportStates.Closing)
			{
				base.State = TransportStates.Closed;
				return;
			}
			base.Connection.Error(text);
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x000A9CA0 File Offset: 0x000A7EA0
		private void WSocket_OnError(WebSocket webSocket, string reason)
		{
			if (webSocket != this.wSocket)
			{
				return;
			}
			if (base.State == TransportStates.Closing || base.State == TransportStates.Closed)
			{
				base.AbortFinished();
				return;
			}
			HTTPManager.Logger.Error("WebSocketTransport", "WSocket_OnError " + reason);
			base.State = TransportStates.Closed;
			base.Connection.Error(reason);
		}

		// Token: 0x04001486 RID: 5254
		private WebSocket wSocket;
	}
}
