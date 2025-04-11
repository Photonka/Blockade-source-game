using System;
using System.Collections.Generic;
using System.Text;
using BestHTTP.JSON;
using BestHTTP.SignalRCore.Messages;
using BestHTTP.WebSocket;

namespace BestHTTP.SignalRCore.Transports
{
	// Token: 0x020001E0 RID: 480
	internal sealed class WebSocketTransport : ITransport
	{
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public TransportTypes TransportType
		{
			get
			{
				return TransportTypes.WebSocket;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public TransferModes TransferMode
		{
			get
			{
				return TransferModes.Binary;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x000A5E97 File Offset: 0x000A4097
		// (set) Token: 0x060011FD RID: 4605 RVA: 0x000A5EA0 File Offset: 0x000A40A0
		public TransportStates State
		{
			get
			{
				return this._state;
			}
			private set
			{
				if (this._state != value)
				{
					TransportStates state = this._state;
					this._state = value;
					if (this.OnStateChanged != null)
					{
						this.OnStateChanged(state, this._state);
					}
				}
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x000A5EDE File Offset: 0x000A40DE
		// (set) Token: 0x060011FF RID: 4607 RVA: 0x000A5EE6 File Offset: 0x000A40E6
		public string ErrorReason { get; private set; }

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06001200 RID: 4608 RVA: 0x000A5EF0 File Offset: 0x000A40F0
		// (remove) Token: 0x06001201 RID: 4609 RVA: 0x000A5F28 File Offset: 0x000A4128
		public event Action<TransportStates, TransportStates> OnStateChanged;

		// Token: 0x06001202 RID: 4610 RVA: 0x000A5F5D File Offset: 0x000A415D
		public WebSocketTransport(HubConnection con)
		{
			this.connection = con;
			this.State = TransportStates.Initial;
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x000A5F80 File Offset: 0x000A4180
		void ITransport.StartConnect()
		{
			HTTPManager.Logger.Verbose("WebSocketTransport", "StartConnect");
			if (this.webSocket == null)
			{
				Uri uri = this.BuildUri(this.connection.Uri);
				if (this.connection.AuthenticationProvider != null)
				{
					uri = (this.connection.AuthenticationProvider.PrepareUri(uri) ?? uri);
				}
				HTTPManager.Logger.Verbose("WebSocketTransport", "StartConnect connecting to Uri: " + uri.ToString());
				this.webSocket = new WebSocket(uri);
			}
			if (this.connection.AuthenticationProvider != null)
			{
				this.connection.AuthenticationProvider.PrepareRequest(this.webSocket.InternalRequest);
			}
			WebSocket webSocket = this.webSocket;
			webSocket.OnOpen = (OnWebSocketOpenDelegate)Delegate.Combine(webSocket.OnOpen, new OnWebSocketOpenDelegate(this.OnOpen));
			WebSocket webSocket2 = this.webSocket;
			webSocket2.OnMessage = (OnWebSocketMessageDelegate)Delegate.Combine(webSocket2.OnMessage, new OnWebSocketMessageDelegate(this.OnMessage));
			WebSocket webSocket3 = this.webSocket;
			webSocket3.OnBinary = (OnWebSocketBinaryDelegate)Delegate.Combine(webSocket3.OnBinary, new OnWebSocketBinaryDelegate(this.OnBinary));
			WebSocket webSocket4 = this.webSocket;
			webSocket4.OnErrorDesc = (OnWebSocketErrorDescriptionDelegate)Delegate.Combine(webSocket4.OnErrorDesc, new OnWebSocketErrorDescriptionDelegate(this.OnError));
			WebSocket webSocket5 = this.webSocket;
			webSocket5.OnClosed = (OnWebSocketClosedDelegate)Delegate.Combine(webSocket5.OnClosed, new OnWebSocketClosedDelegate(this.OnClosed));
			this.webSocket.Open();
			this.State = TransportStates.Connecting;
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x000A6107 File Offset: 0x000A4307
		void ITransport.Send(byte[] msg)
		{
			this.webSocket.Send(msg);
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x000A6118 File Offset: 0x000A4318
		private void OnOpen(WebSocket webSocket)
		{
			HTTPManager.Logger.Verbose("WebSocketTransport", "OnOpen");
			byte[] msg = JsonProtocol.WithSeparator(string.Format("{{'protocol':'{0}', 'version': 1}}", this.connection.Protocol.Encoder.Name));
			((ITransport)this).Send(msg);
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x000A6168 File Offset: 0x000A4368
		private string ParseHandshakeResponse(string data)
		{
			Dictionary<string, object> dictionary = Json.Decode(data) as Dictionary<string, object>;
			if (dictionary == null)
			{
				return "Couldn't parse json data: " + data;
			}
			object obj;
			if (dictionary.TryGetValue("error", out obj))
			{
				return obj.ToString();
			}
			return null;
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x000A61A7 File Offset: 0x000A43A7
		private void HandleHandshakeResponse(string data)
		{
			this.ErrorReason = this.ParseHandshakeResponse(data);
			this.State = (string.IsNullOrEmpty(this.ErrorReason) ? TransportStates.Connected : TransportStates.Failed);
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x000A61D0 File Offset: 0x000A43D0
		private void OnMessage(WebSocket webSocket, string data)
		{
			if (this.State == TransportStates.Connecting)
			{
				this.HandleHandshakeResponse(data);
				return;
			}
			this.messages.Clear();
			try
			{
				this.connection.Protocol.ParseMessages(data, ref this.messages);
				this.connection.OnMessages(this.messages);
			}
			catch (Exception ex)
			{
				HTTPManager.Logger.Exception("WebSocketTransport", "OnMessage(string)", ex);
			}
			finally
			{
				this.messages.Clear();
			}
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x000A6264 File Offset: 0x000A4464
		private void OnBinary(WebSocket webSocket, byte[] data)
		{
			if (this.State == TransportStates.Connecting)
			{
				this.HandleHandshakeResponse(Encoding.UTF8.GetString(data, 0, data.Length));
				return;
			}
			this.messages.Clear();
			try
			{
				this.connection.Protocol.ParseMessages(data, ref this.messages);
				this.connection.OnMessages(this.messages);
			}
			catch (Exception ex)
			{
				HTTPManager.Logger.Exception("WebSocketTransport", "OnMessage(byte[])", ex);
			}
			finally
			{
				this.messages.Clear();
			}
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x000A6308 File Offset: 0x000A4508
		private void OnError(WebSocket webSocket, string reason)
		{
			HTTPManager.Logger.Verbose("WebSocketTransport", "OnError: " + reason);
			this.ErrorReason = reason;
			this.State = TransportStates.Failed;
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x000A6334 File Offset: 0x000A4534
		private void OnClosed(WebSocket webSocket, ushort code, string message)
		{
			HTTPManager.Logger.Verbose("WebSocketTransport", string.Concat(new object[]
			{
				"OnClosed: ",
				code,
				" ",
				message
			}));
			this.webSocket = null;
			this.State = TransportStates.Closed;
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x000A6386 File Offset: 0x000A4586
		void ITransport.StartClose()
		{
			HTTPManager.Logger.Verbose("WebSocketTransport", "StartClose");
			if (this.webSocket != null)
			{
				this.State = TransportStates.Closing;
				this.webSocket.Close();
				return;
			}
			this.State = TransportStates.Closed;
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x000A63C0 File Offset: 0x000A45C0
		private Uri BuildUri(Uri baseUri)
		{
			if (this.connection.NegotiationResult == null)
			{
				return baseUri;
			}
			UriBuilder uriBuilder = new UriBuilder(baseUri);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(baseUri.Query);
			if (!string.IsNullOrEmpty(this.connection.NegotiationResult.ConnectionId))
			{
				stringBuilder.Append("&id=").Append(this.connection.NegotiationResult.ConnectionId);
			}
			uriBuilder.Query = stringBuilder.ToString();
			return uriBuilder.Uri;
		}

		// Token: 0x040013F3 RID: 5107
		private TransportStates _state;

		// Token: 0x040013F6 RID: 5110
		private WebSocket webSocket;

		// Token: 0x040013F7 RID: 5111
		private HubConnection connection;

		// Token: 0x040013F8 RID: 5112
		private List<Message> messages = new List<Message>();
	}
}
