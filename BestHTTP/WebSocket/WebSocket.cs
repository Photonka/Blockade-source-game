using System;
using System.Text;
using BestHTTP.Decompression.Zlib;
using BestHTTP.Extensions;
using BestHTTP.WebSocket.Extensions;
using BestHTTP.WebSocket.Frames;

namespace BestHTTP.WebSocket
{
	// Token: 0x020001AA RID: 426
	public sealed class WebSocket
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x0009E6D8 File Offset: 0x0009C8D8
		// (set) Token: 0x06000FD8 RID: 4056 RVA: 0x0009E6E0 File Offset: 0x0009C8E0
		public WebSocketStates State { get; private set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x0009E6E9 File Offset: 0x0009C8E9
		public bool IsOpen
		{
			get
			{
				return this.webSocket != null && !this.webSocket.IsClosed;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x0009E703 File Offset: 0x0009C903
		public int BufferedAmount
		{
			get
			{
				return this.webSocket.BufferedAmount;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x0009E710 File Offset: 0x0009C910
		// (set) Token: 0x06000FDC RID: 4060 RVA: 0x0009E718 File Offset: 0x0009C918
		public bool StartPingThread { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x0009E721 File Offset: 0x0009C921
		// (set) Token: 0x06000FDE RID: 4062 RVA: 0x0009E729 File Offset: 0x0009C929
		public int PingFrequency { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x0009E732 File Offset: 0x0009C932
		// (set) Token: 0x06000FE0 RID: 4064 RVA: 0x0009E73A File Offset: 0x0009C93A
		public TimeSpan CloseAfterNoMesssage { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x0009E743 File Offset: 0x0009C943
		// (set) Token: 0x06000FE2 RID: 4066 RVA: 0x0009E74B File Offset: 0x0009C94B
		public HTTPRequest InternalRequest { get; private set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x0009E754 File Offset: 0x0009C954
		// (set) Token: 0x06000FE4 RID: 4068 RVA: 0x0009E75C File Offset: 0x0009C95C
		public IExtension[] Extensions { get; private set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x0009E765 File Offset: 0x0009C965
		public int Latency
		{
			get
			{
				return this.webSocket.Latency;
			}
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0009E774 File Offset: 0x0009C974
		public WebSocket(Uri uri) : this(uri, string.Empty, string.Empty, Array.Empty<IExtension>())
		{
			this.Extensions = new IExtension[]
			{
				new PerMessageCompression(CompressionLevel.Default, false, false, 15, 15, 256)
			};
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0009E7B8 File Offset: 0x0009C9B8
		public WebSocket(Uri uri, string origin, string protocol, params IExtension[] extensions)
		{
			string text = HTTPProtocolFactory.IsSecureProtocol(uri) ? "wss" : "ws";
			int num = (uri.Port != -1) ? uri.Port : (text.Equals("wss", StringComparison.OrdinalIgnoreCase) ? 443 : 80);
			uri = new Uri(string.Concat(new object[]
			{
				text,
				"://",
				uri.Host,
				":",
				num,
				uri.GetRequestPathAndQueryURL()
			}));
			this.PingFrequency = 1000;
			this.CloseAfterNoMesssage = TimeSpan.FromSeconds(10.0);
			this.InternalRequest = new HTTPRequest(uri, new OnRequestFinishedDelegate(this.OnInternalRequestCallback));
			this.InternalRequest.OnUpgraded = new OnRequestFinishedDelegate(this.OnInternalRequestUpgraded);
			if (!HTTPProtocolFactory.IsSecureProtocol(uri) && uri.Port != 80 && HTTPProtocolFactory.IsSecureProtocol(uri) && uri.Port != 443)
			{
				this.InternalRequest.SetHeader("Host", uri.Host + ":" + uri.Port);
			}
			else
			{
				this.InternalRequest.SetHeader("Host", uri.Host);
			}
			this.InternalRequest.SetHeader("Upgrade", "websocket");
			this.InternalRequest.SetHeader("Connection", "Upgrade");
			this.InternalRequest.SetHeader("Sec-WebSocket-Key", this.GetSecKey(new object[]
			{
				this,
				this.InternalRequest,
				uri,
				new object()
			}));
			if (!string.IsNullOrEmpty(origin))
			{
				this.InternalRequest.SetHeader("Origin", origin);
			}
			this.InternalRequest.SetHeader("Sec-WebSocket-Version", "13");
			if (!string.IsNullOrEmpty(protocol))
			{
				this.InternalRequest.SetHeader("Sec-WebSocket-Protocol", protocol);
			}
			this.InternalRequest.SetHeader("Cache-Control", "no-cache");
			this.InternalRequest.SetHeader("Pragma", "no-cache");
			this.Extensions = extensions;
			this.InternalRequest.DisableCache = true;
			this.InternalRequest.DisableRetry = true;
			this.InternalRequest.TryToMinimizeTCPLatency = true;
			HTTPProxy httpproxy = HTTPManager.Proxy as HTTPProxy;
			if (httpproxy != null)
			{
				this.InternalRequest.Proxy = new HTTPProxy(httpproxy.Address, httpproxy.Credentials, false, false, httpproxy.NonTransparentForHTTPS);
			}
			HTTPManager.Setup();
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0009EA38 File Offset: 0x0009CC38
		private void OnInternalRequestCallback(HTTPRequest req, HTTPResponse resp)
		{
			string text = string.Empty;
			switch (req.State)
			{
			case HTTPRequestStates.Finished:
				if (resp.IsSuccess || resp.StatusCode == 101)
				{
					HTTPManager.Logger.Information("WebSocket", string.Format("Request finished. Status Code: {0} Message: {1}", resp.StatusCode.ToString(), resp.Message));
					return;
				}
				text = string.Format("Request Finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", resp.StatusCode, resp.Message, resp.DataAsText);
				break;
			case HTTPRequestStates.Error:
				text = "Request Finished with Error! " + ((req.Exception != null) ? ("Exception: " + req.Exception.Message + req.Exception.StackTrace) : string.Empty);
				break;
			case HTTPRequestStates.Aborted:
				text = "Request Aborted!";
				break;
			case HTTPRequestStates.ConnectionTimedOut:
				text = "Connection Timed Out!";
				break;
			case HTTPRequestStates.TimedOut:
				text = "Processing the request Timed Out!";
				break;
			default:
				return;
			}
			if (this.State != WebSocketStates.Connecting || !string.IsNullOrEmpty(text))
			{
				if (this.OnError != null)
				{
					this.OnError(this, req.Exception);
				}
				if (this.OnErrorDesc != null)
				{
					this.OnErrorDesc(this, text);
				}
				if (this.OnError == null && this.OnErrorDesc == null)
				{
					HTTPManager.Logger.Error("WebSocket", text);
				}
			}
			else if (this.OnClosed != null)
			{
				this.OnClosed(this, 1000, "Closed while opening");
			}
			if (!req.IsKeepAlive && resp != null && resp is WebSocketResponse)
			{
				(resp as WebSocketResponse).CloseStream();
			}
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0009EBC4 File Offset: 0x0009CDC4
		private void OnInternalRequestUpgraded(HTTPRequest req, HTTPResponse resp)
		{
			this.webSocket = (resp as WebSocketResponse);
			if (this.webSocket == null)
			{
				if (this.OnError != null)
				{
					this.OnError(this, req.Exception);
				}
				if (this.OnErrorDesc != null)
				{
					string reason = string.Empty;
					if (req.Exception != null)
					{
						reason = req.Exception.Message + " " + req.Exception.StackTrace;
					}
					this.OnErrorDesc(this, reason);
				}
				this.State = WebSocketStates.Closed;
				return;
			}
			if (this.State == WebSocketStates.Closed)
			{
				this.webSocket.CloseStream();
				return;
			}
			this.webSocket.WebSocket = this;
			if (this.Extensions != null)
			{
				for (int i = 0; i < this.Extensions.Length; i++)
				{
					IExtension extension = this.Extensions[i];
					try
					{
						if (extension != null && !extension.ParseNegotiation(this.webSocket))
						{
							this.Extensions[i] = null;
						}
					}
					catch (Exception ex)
					{
						HTTPManager.Logger.Exception("WebSocket", "ParseNegotiation", ex);
						this.Extensions[i] = null;
					}
				}
			}
			this.State = WebSocketStates.Open;
			if (this.OnOpen != null)
			{
				try
				{
					this.OnOpen(this);
				}
				catch (Exception ex2)
				{
					HTTPManager.Logger.Exception("WebSocket", "OnOpen", ex2);
				}
			}
			this.webSocket.OnText = delegate(WebSocketResponse ws, string msg)
			{
				if (this.OnMessage != null)
				{
					this.OnMessage(this, msg);
				}
			};
			this.webSocket.OnBinary = delegate(WebSocketResponse ws, byte[] bin)
			{
				if (this.OnBinary != null)
				{
					this.OnBinary(this, bin);
				}
			};
			this.webSocket.OnClosed = delegate(WebSocketResponse ws, ushort code, string msg)
			{
				this.State = WebSocketStates.Closed;
				if (this.OnClosed != null)
				{
					this.OnClosed(this, code, msg);
				}
			};
			if (this.OnIncompleteFrame != null)
			{
				this.webSocket.OnIncompleteFrame = delegate(WebSocketResponse ws, WebSocketFrameReader frame)
				{
					if (this.OnIncompleteFrame != null)
					{
						this.OnIncompleteFrame(this, frame);
					}
				};
			}
			if (this.StartPingThread)
			{
				this.webSocket.StartPinging(Math.Max(this.PingFrequency, 100));
			}
			this.webSocket.StartReceive();
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0009EDB0 File Offset: 0x0009CFB0
		public void Open()
		{
			if (this.requestSent)
			{
				throw new InvalidOperationException("Open already called! You can't reuse this WebSocket instance!");
			}
			if (this.Extensions != null)
			{
				try
				{
					for (int i = 0; i < this.Extensions.Length; i++)
					{
						IExtension extension = this.Extensions[i];
						if (extension != null)
						{
							extension.AddNegotiation(this.InternalRequest);
						}
					}
				}
				catch (Exception ex)
				{
					HTTPManager.Logger.Exception("WebSocket", "Open", ex);
				}
			}
			this.InternalRequest.Send();
			this.requestSent = true;
			this.State = WebSocketStates.Connecting;
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0009EE48 File Offset: 0x0009D048
		public void Send(string message)
		{
			if (!this.IsOpen)
			{
				return;
			}
			this.webSocket.Send(message);
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0009EE5F File Offset: 0x0009D05F
		public void Send(byte[] buffer)
		{
			if (!this.IsOpen)
			{
				return;
			}
			this.webSocket.Send(buffer);
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0009EE76 File Offset: 0x0009D076
		public void Send(byte[] buffer, ulong offset, ulong count)
		{
			if (!this.IsOpen)
			{
				return;
			}
			this.webSocket.Send(buffer, offset, count);
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0009EE8F File Offset: 0x0009D08F
		public void Send(WebSocketFrame frame)
		{
			if (this.IsOpen)
			{
				this.webSocket.Send(frame);
			}
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0009EEA8 File Offset: 0x0009D0A8
		public void Close()
		{
			if (this.State >= WebSocketStates.Closing)
			{
				return;
			}
			if (this.State == WebSocketStates.Connecting)
			{
				this.State = WebSocketStates.Closed;
				if (this.OnClosed != null)
				{
					this.OnClosed(this, 1005, string.Empty);
					return;
				}
			}
			else
			{
				this.State = WebSocketStates.Closing;
				this.webSocket.Close();
			}
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0009EEFF File Offset: 0x0009D0FF
		public void Close(ushort code, string message)
		{
			if (!this.IsOpen)
			{
				return;
			}
			this.webSocket.Close(code, message);
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0009EF18 File Offset: 0x0009D118
		public static byte[] EncodeCloseData(ushort code, string message)
		{
			int byteCount = Encoding.UTF8.GetByteCount(message);
			byte[] result;
			using (BufferPoolMemoryStream bufferPoolMemoryStream = new BufferPoolMemoryStream(2 + byteCount))
			{
				byte[] bytes = BitConverter.GetBytes(code);
				if (BitConverter.IsLittleEndian)
				{
					Array.Reverse(bytes, 0, bytes.Length);
				}
				bufferPoolMemoryStream.Write(bytes, 0, bytes.Length);
				bytes = Encoding.UTF8.GetBytes(message);
				bufferPoolMemoryStream.Write(bytes, 0, bytes.Length);
				result = bufferPoolMemoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0009EF98 File Offset: 0x0009D198
		private string GetSecKey(object[] from)
		{
			byte[] array = new byte[16];
			int num = 0;
			for (int i = 0; i < from.Length; i++)
			{
				byte[] bytes = BitConverter.GetBytes(from[i].GetHashCode());
				int num2 = 0;
				while (num2 < bytes.Length && num < array.Length)
				{
					array[num++] = bytes[num2];
					num2++;
				}
			}
			return Convert.ToBase64String(array);
		}

		// Token: 0x040012E5 RID: 4837
		public OnWebSocketOpenDelegate OnOpen;

		// Token: 0x040012E6 RID: 4838
		public OnWebSocketMessageDelegate OnMessage;

		// Token: 0x040012E7 RID: 4839
		public OnWebSocketBinaryDelegate OnBinary;

		// Token: 0x040012E8 RID: 4840
		public OnWebSocketClosedDelegate OnClosed;

		// Token: 0x040012E9 RID: 4841
		public OnWebSocketErrorDelegate OnError;

		// Token: 0x040012EA RID: 4842
		public OnWebSocketErrorDescriptionDelegate OnErrorDesc;

		// Token: 0x040012EB RID: 4843
		public OnWebSocketIncompleteFrameDelegate OnIncompleteFrame;

		// Token: 0x040012EC RID: 4844
		private bool requestSent;

		// Token: 0x040012ED RID: 4845
		private WebSocketResponse webSocket;
	}
}
