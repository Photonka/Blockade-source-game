using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using BestHTTP.Extensions;
using BestHTTP.WebSocket.Frames;

namespace BestHTTP.WebSocket
{
	// Token: 0x020001AB RID: 427
	public sealed class WebSocketResponse : HTTPResponse, IHeartbeat, IProtocol
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x0009F058 File Offset: 0x0009D258
		// (set) Token: 0x06000FF8 RID: 4088 RVA: 0x0009F060 File Offset: 0x0009D260
		public WebSocket WebSocket { get; internal set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x0009F069 File Offset: 0x0009D269
		public bool IsClosed
		{
			get
			{
				return this.closed;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000FFA RID: 4090 RVA: 0x0009F073 File Offset: 0x0009D273
		// (set) Token: 0x06000FFB RID: 4091 RVA: 0x0009F07B File Offset: 0x0009D27B
		public TimeSpan PingFrequnecy { get; private set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000FFC RID: 4092 RVA: 0x0009F084 File Offset: 0x0009D284
		// (set) Token: 0x06000FFD RID: 4093 RVA: 0x0009F08C File Offset: 0x0009D28C
		public ushort MaxFragmentSize { get; private set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x0009F095 File Offset: 0x0009D295
		public int BufferedAmount
		{
			get
			{
				return this._bufferedAmount;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x0009F09D File Offset: 0x0009D29D
		// (set) Token: 0x06001000 RID: 4096 RVA: 0x0009F0A5 File Offset: 0x0009D2A5
		public int Latency { get; private set; }

		// Token: 0x06001001 RID: 4097 RVA: 0x0009F0B0 File Offset: 0x0009D2B0
		internal WebSocketResponse(HTTPRequest request, Stream stream, bool isStreamed, bool isFromCache) : base(request, stream, isStreamed, isFromCache)
		{
			base.IsClosedManually = true;
			this.closed = false;
			this.MaxFragmentSize = 32767;
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0009F159 File Offset: 0x0009D359
		internal void StartReceive()
		{
			if (base.IsUpgraded)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.ReceiveThreadFunc));
			}
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0009F178 File Offset: 0x0009D378
		internal void CloseStream()
		{
			ConnectionBase connectionWith = HTTPManager.GetConnectionWith(this.baseRequest);
			if (connectionWith != null)
			{
				connectionWith.Abort(HTTPConnectionStates.Closed);
			}
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0009F19C File Offset: 0x0009D39C
		public void Send(string message)
		{
			if (message == null)
			{
				throw new ArgumentNullException("message must not be null!");
			}
			int byteCount = Encoding.UTF8.GetByteCount(message);
			byte[] array = VariableSizedBufferPool.Get((long)byteCount, true);
			Encoding.UTF8.GetBytes(message, 0, message.Length, array, 0);
			WebSocketFrame webSocketFrame = new WebSocketFrame(this.WebSocket, WebSocketFrameTypes.Text, array, 0UL, (ulong)((long)byteCount), true, true);
			if (webSocketFrame.Data != null && webSocketFrame.Data.Length > (int)this.MaxFragmentSize)
			{
				WebSocketFrame[] array2 = webSocketFrame.Fragment(this.MaxFragmentSize);
				object sendLock = this.SendLock;
				lock (sendLock)
				{
					this.Send(webSocketFrame);
					if (array2 != null)
					{
						for (int i = 0; i < array2.Length; i++)
						{
							this.Send(array2[i]);
						}
					}
					goto IL_C0;
				}
			}
			this.Send(webSocketFrame);
			IL_C0:
			VariableSizedBufferPool.Release(array);
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0009F280 File Offset: 0x0009D480
		public void Send(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data must not be null!");
			}
			WebSocketFrame webSocketFrame = new WebSocketFrame(this.WebSocket, WebSocketFrameTypes.Binary, data);
			if (webSocketFrame.Data != null && webSocketFrame.Data.Length > (int)this.MaxFragmentSize)
			{
				WebSocketFrame[] array = webSocketFrame.Fragment(this.MaxFragmentSize);
				object sendLock = this.SendLock;
				lock (sendLock)
				{
					this.Send(webSocketFrame);
					if (array != null)
					{
						for (int i = 0; i < array.Length; i++)
						{
							this.Send(array[i]);
						}
					}
					return;
				}
			}
			this.Send(webSocketFrame);
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0009F328 File Offset: 0x0009D528
		public void Send(byte[] data, ulong offset, ulong count)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data must not be null!");
			}
			if (offset + count > (ulong)((long)data.Length))
			{
				throw new ArgumentOutOfRangeException("offset + count >= data.Length");
			}
			WebSocketFrame webSocketFrame = new WebSocketFrame(this.WebSocket, WebSocketFrameTypes.Binary, data, offset, count, true, true);
			if (webSocketFrame.Data != null && webSocketFrame.Data.Length > (int)this.MaxFragmentSize)
			{
				WebSocketFrame[] array = webSocketFrame.Fragment(this.MaxFragmentSize);
				object sendLock = this.SendLock;
				lock (sendLock)
				{
					this.Send(webSocketFrame);
					if (array != null)
					{
						for (int i = 0; i < array.Length; i++)
						{
							this.Send(array[i]);
						}
					}
					return;
				}
			}
			this.Send(webSocketFrame);
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x0009F3E8 File Offset: 0x0009D5E8
		public void Send(WebSocketFrame frame)
		{
			if (frame == null)
			{
				throw new ArgumentNullException("frame is null!");
			}
			if (this.closed || this.closeSent)
			{
				return;
			}
			object sendLock = this.SendLock;
			lock (sendLock)
			{
				this.unsentFrames.Add(frame);
				if (!this.sendThreadCreated)
				{
					HTTPManager.Logger.Information("WebSocketResponse", "Send - Creating thread");
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.SendThreadFunc));
					this.sendThreadCreated = true;
				}
			}
			Interlocked.Add(ref this._bufferedAmount, (frame.Data != null) ? frame.DataLength : 0);
			this.newFrameSignal.Set();
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x0009F4B8 File Offset: 0x0009D6B8
		public void Insert(WebSocketFrame frame)
		{
			if (frame == null)
			{
				throw new ArgumentNullException("frame is null!");
			}
			if (this.closed || this.closeSent)
			{
				return;
			}
			object sendLock = this.SendLock;
			lock (sendLock)
			{
				this.unsentFrames.Insert(0, frame);
				if (!this.sendThreadCreated)
				{
					HTTPManager.Logger.Information("WebSocketResponse", "Insert - Creating thread");
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.SendThreadFunc));
					this.sendThreadCreated = true;
				}
			}
			Interlocked.Add(ref this._bufferedAmount, (frame.Data != null) ? frame.DataLength : 0);
			this.newFrameSignal.Set();
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0009F588 File Offset: 0x0009D788
		public void SendNow(WebSocketFrame frame)
		{
			if (frame == null)
			{
				throw new ArgumentNullException("frame is null!");
			}
			if (this.closed || this.closeSent)
			{
				return;
			}
			using (RawFrameData rawFrameData = frame.Get())
			{
				this.Stream.Write(rawFrameData.Data, 0, rawFrameData.Length);
				this.Stream.Flush();
			}
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0009F604 File Offset: 0x0009D804
		public void Close()
		{
			this.Close(1000, "Bye!");
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0009F618 File Offset: 0x0009D818
		public void Close(ushort code, string msg)
		{
			if (this.closed)
			{
				return;
			}
			object sendLock = this.SendLock;
			lock (sendLock)
			{
				this.unsentFrames.Clear();
			}
			Interlocked.Exchange(ref this._bufferedAmount, 0);
			this.Send(new WebSocketFrame(this.WebSocket, WebSocketFrameTypes.ConnectionClose, WebSocket.EncodeCloseData(code, msg)));
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0009F690 File Offset: 0x0009D890
		public void StartPinging(int frequency)
		{
			if (frequency < 100)
			{
				throw new ArgumentException("frequency must be at least 100 milliseconds!");
			}
			this.PingFrequnecy = TimeSpan.FromMilliseconds((double)frequency);
			this.lastMessage = DateTime.UtcNow;
			this.SendPing();
			HTTPManager.Heartbeats.Subscribe(this);
			HTTPUpdateDelegator.OnApplicationForegroundStateChanged = (Action<bool>)Delegate.Combine(HTTPUpdateDelegator.OnApplicationForegroundStateChanged, new Action<bool>(this.OnApplicationForegroundStateChanged));
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0009F6F8 File Offset: 0x0009D8F8
		private void SendThreadFunc(object param)
		{
			List<WebSocketFrame> list = new List<WebSocketFrame>();
			try
			{
				while (!this.closed && !this.closeSent)
				{
					this.newFrameSignal.WaitOne();
					try
					{
						object sendLock = this.SendLock;
						lock (sendLock)
						{
							for (int i = this.unsentFrames.Count - 1; i >= 0; i--)
							{
								list.Add(this.unsentFrames[i]);
							}
							this.unsentFrames.Clear();
							goto IL_FC;
						}
						IL_6E:
						WebSocketFrame webSocketFrame = list[list.Count - 1];
						list.RemoveAt(list.Count - 1);
						if (!this.closeSent)
						{
							using (RawFrameData rawFrameData = webSocketFrame.Get())
							{
								this.Stream.Write(rawFrameData.Data, 0, rawFrameData.Length);
							}
							VariableSizedBufferPool.Release(webSocketFrame.Data);
							if (webSocketFrame.Type == WebSocketFrameTypes.ConnectionClose)
							{
								this.closeSent = true;
							}
						}
						Interlocked.Add(ref this._bufferedAmount, -webSocketFrame.DataLength);
						IL_FC:
						if (list.Count > 0)
						{
							goto IL_6E;
						}
						this.Stream.Flush();
					}
					catch (Exception exception)
					{
						if (HTTPUpdateDelegator.IsCreated)
						{
							this.baseRequest.Exception = exception;
							this.baseRequest.State = HTTPRequestStates.Error;
						}
						else
						{
							this.baseRequest.State = HTTPRequestStates.Aborted;
						}
						this.closed = true;
					}
				}
			}
			finally
			{
				this.sendThreadCreated = false;
				((IDisposable)this.newFrameSignal).Dispose();
				this.newFrameSignal = null;
				HTTPManager.Logger.Information("WebSocketResponse", "SendThread - Closed!");
			}
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0009F908 File Offset: 0x0009DB08
		private void ReceiveThreadFunc(object param)
		{
			try
			{
				using (ReadOnlyBufferedStream readOnlyBufferedStream = new ReadOnlyBufferedStream(this.Stream))
				{
					while (!this.closed)
					{
						try
						{
							WebSocketFrameReader webSocketFrameReader = default(WebSocketFrameReader);
							webSocketFrameReader.Read(readOnlyBufferedStream);
							this.lastMessage = DateTime.UtcNow;
							if (webSocketFrameReader.HasMask)
							{
								this.Close(1002, "Protocol Error: masked frame received from server!");
							}
							else if (!webSocketFrameReader.IsFinal)
							{
								if (this.OnIncompleteFrame == null)
								{
									this.IncompleteFrames.Add(webSocketFrameReader);
								}
								else
								{
									object frameLock = this.FrameLock;
									lock (frameLock)
									{
										this.CompletedFrames.Add(webSocketFrameReader);
									}
								}
							}
							else
							{
								object frameLock;
								switch (webSocketFrameReader.Type)
								{
								case WebSocketFrameTypes.Continuation:
									if (this.OnIncompleteFrame == null)
									{
										webSocketFrameReader.Assemble(this.IncompleteFrames);
										this.IncompleteFrames.Clear();
									}
									else
									{
										frameLock = this.FrameLock;
										lock (frameLock)
										{
											this.CompletedFrames.Add(webSocketFrameReader);
											continue;
										}
									}
									break;
								case WebSocketFrameTypes.Text:
								case WebSocketFrameTypes.Binary:
									break;
								case (WebSocketFrameTypes)3:
								case (WebSocketFrameTypes)4:
								case (WebSocketFrameTypes)5:
								case (WebSocketFrameTypes)6:
								case (WebSocketFrameTypes)7:
									continue;
								case WebSocketFrameTypes.ConnectionClose:
									goto IL_1DF;
								case WebSocketFrameTypes.Ping:
									goto IL_160;
								case WebSocketFrameTypes.Pong:
									try
									{
										long num = BitConverter.ToInt64(webSocketFrameReader.Data, 0);
										TimeSpan timeSpan = TimeSpan.FromTicks(this.lastMessage.Ticks - num);
										this.rtts.Add((int)timeSpan.TotalMilliseconds);
										this.Latency = this.CalculateLatency();
										continue;
									}
									catch
									{
										continue;
									}
									goto IL_1DF;
								default:
									continue;
								}
								webSocketFrameReader.DecodeWithExtensions(this.WebSocket);
								frameLock = this.FrameLock;
								lock (frameLock)
								{
									this.CompletedFrames.Add(webSocketFrameReader);
									continue;
								}
								IL_160:
								if (!this.closeSent && !this.closed)
								{
									this.Send(new WebSocketFrame(this.WebSocket, WebSocketFrameTypes.Pong, webSocketFrameReader.Data));
									continue;
								}
								continue;
								IL_1DF:
								this.CloseFrame = webSocketFrameReader;
								if (!this.closeSent)
								{
									this.Send(new WebSocketFrame(this.WebSocket, WebSocketFrameTypes.ConnectionClose, null));
								}
								this.closed = true;
							}
						}
						catch (ThreadAbortException)
						{
							this.IncompleteFrames.Clear();
							this.baseRequest.State = HTTPRequestStates.Aborted;
							this.closed = true;
							this.newFrameSignal.Set();
						}
						catch (Exception exception)
						{
							if (HTTPUpdateDelegator.IsCreated)
							{
								this.baseRequest.Exception = exception;
								this.baseRequest.State = HTTPRequestStates.Error;
							}
							else
							{
								this.baseRequest.State = HTTPRequestStates.Aborted;
							}
							this.closed = true;
							this.newFrameSignal.Set();
						}
					}
				}
			}
			finally
			{
				HTTPManager.Heartbeats.Unsubscribe(this);
				HTTPUpdateDelegator.OnApplicationForegroundStateChanged = (Action<bool>)Delegate.Remove(HTTPUpdateDelegator.OnApplicationForegroundStateChanged, new Action<bool>(this.OnApplicationForegroundStateChanged));
				HTTPManager.Logger.Information("WebSocketResponse", "ReceiveThread - Closed!");
			}
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x0009FCBC File Offset: 0x0009DEBC
		void IProtocol.HandleEvents()
		{
			this.frameCache.Clear();
			object frameLock = this.FrameLock;
			lock (frameLock)
			{
				this.frameCache.AddRange(this.CompletedFrames);
				this.CompletedFrames.Clear();
			}
			for (int i = 0; i < this.frameCache.Count; i++)
			{
				WebSocketFrameReader arg = this.frameCache[i];
				try
				{
					switch (arg.Type)
					{
					case WebSocketFrameTypes.Continuation:
						break;
					case WebSocketFrameTypes.Text:
						if (arg.IsFinal)
						{
							if (this.OnText != null)
							{
								this.OnText(this, arg.DataAsText);
								goto IL_D7;
							}
							goto IL_D7;
						}
						break;
					case WebSocketFrameTypes.Binary:
						if (arg.IsFinal)
						{
							if (this.OnBinary != null)
							{
								this.OnBinary(this, arg.Data);
								goto IL_D7;
							}
							goto IL_D7;
						}
						break;
					default:
						goto IL_D7;
					}
					if (this.OnIncompleteFrame != null)
					{
						this.OnIncompleteFrame(this, arg);
					}
					IL_D7:;
				}
				catch (Exception ex)
				{
					HTTPManager.Logger.Exception("WebSocketResponse", "HandleEvents", ex);
				}
			}
			this.frameCache.Clear();
			if (this.IsClosed && this.OnClosed != null && this.baseRequest.State == HTTPRequestStates.Processing)
			{
				try
				{
					ushort arg2 = 0;
					string arg3 = string.Empty;
					if (this.CloseFrame.Data != null && this.CloseFrame.Data.Length >= 2)
					{
						if (BitConverter.IsLittleEndian)
						{
							Array.Reverse(this.CloseFrame.Data, 0, 2);
						}
						arg2 = BitConverter.ToUInt16(this.CloseFrame.Data, 0);
						if (this.CloseFrame.Data.Length > 2)
						{
							arg3 = Encoding.UTF8.GetString(this.CloseFrame.Data, 2, this.CloseFrame.Data.Length - 2);
						}
						VariableSizedBufferPool.Release(this.CloseFrame.Data);
					}
					this.OnClosed(this, arg2, arg3);
				}
				catch (Exception ex2)
				{
					HTTPManager.Logger.Exception("WebSocketResponse", "HandleEvents - OnClosed", ex2);
				}
			}
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0009FEF4 File Offset: 0x0009E0F4
		void IHeartbeat.OnHeartbeatUpdate(TimeSpan dif)
		{
			DateTime utcNow = DateTime.UtcNow;
			if (utcNow - this.lastPing >= this.PingFrequnecy)
			{
				this.SendPing();
			}
			if (utcNow - (this.lastMessage + this.PingFrequnecy) > this.WebSocket.CloseAfterNoMesssage)
			{
				HTTPManager.Logger.Warning("WebSocketResponse", string.Format("No message received in the given time! Closing WebSocket. LastMessage: {0}, PingFrequency: {1}, Close After: {2}, Now: {3}", new object[]
				{
					this.lastMessage,
					this.PingFrequnecy,
					this.WebSocket.CloseAfterNoMesssage,
					utcNow
				}));
				this.CloseWithError("No message received in the given time!");
			}
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0009FFB1 File Offset: 0x0009E1B1
		private void OnApplicationForegroundStateChanged(bool isPaused)
		{
			if (!isPaused)
			{
				this.lastMessage = DateTime.UtcNow;
			}
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0009FFC4 File Offset: 0x0009E1C4
		private void SendPing()
		{
			this.lastPing = DateTime.UtcNow;
			try
			{
				byte[] bytes = BitConverter.GetBytes(DateTime.UtcNow.Ticks);
				WebSocketFrame frame = new WebSocketFrame(this.WebSocket, WebSocketFrameTypes.Ping, bytes);
				this.Insert(frame);
			}
			catch
			{
				HTTPManager.Logger.Information("WebSocketResponse", "Error while sending PING message! Closing WebSocket.");
				this.CloseWithError("Error while sending PING message!");
			}
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x000A003C File Offset: 0x0009E23C
		private void CloseWithError(string message)
		{
			this.baseRequest.Exception = new Exception(message);
			this.baseRequest.State = HTTPRequestStates.Error;
			this.closed = true;
			HTTPManager.Heartbeats.Unsubscribe(this);
			HTTPUpdateDelegator.OnApplicationForegroundStateChanged = (Action<bool>)Delegate.Remove(HTTPUpdateDelegator.OnApplicationForegroundStateChanged, new Action<bool>(this.OnApplicationForegroundStateChanged));
			this.newFrameSignal.Set();
			this.CloseStream();
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x000A00B0 File Offset: 0x0009E2B0
		private int CalculateLatency()
		{
			if (this.rtts.Count == 0)
			{
				return 0;
			}
			int num = 0;
			for (int i = 0; i < this.rtts.Count; i++)
			{
				num += this.rtts[i];
			}
			return num / this.rtts.Count;
		}

		// Token: 0x040012EE RID: 4846
		public static int RTTBufferCapacity = 5;

		// Token: 0x040012F0 RID: 4848
		public Action<WebSocketResponse, string> OnText;

		// Token: 0x040012F1 RID: 4849
		public Action<WebSocketResponse, byte[]> OnBinary;

		// Token: 0x040012F2 RID: 4850
		public Action<WebSocketResponse, WebSocketFrameReader> OnIncompleteFrame;

		// Token: 0x040012F3 RID: 4851
		public Action<WebSocketResponse, ushort, string> OnClosed;

		// Token: 0x040012F6 RID: 4854
		private int _bufferedAmount;

		// Token: 0x040012F8 RID: 4856
		private List<WebSocketFrameReader> IncompleteFrames = new List<WebSocketFrameReader>();

		// Token: 0x040012F9 RID: 4857
		private List<WebSocketFrameReader> CompletedFrames = new List<WebSocketFrameReader>();

		// Token: 0x040012FA RID: 4858
		private List<WebSocketFrameReader> frameCache = new List<WebSocketFrameReader>();

		// Token: 0x040012FB RID: 4859
		private WebSocketFrameReader CloseFrame;

		// Token: 0x040012FC RID: 4860
		private object FrameLock = new object();

		// Token: 0x040012FD RID: 4861
		private object SendLock = new object();

		// Token: 0x040012FE RID: 4862
		private List<WebSocketFrame> unsentFrames = new List<WebSocketFrame>();

		// Token: 0x040012FF RID: 4863
		private volatile AutoResetEvent newFrameSignal = new AutoResetEvent(false);

		// Token: 0x04001300 RID: 4864
		private volatile bool sendThreadCreated;

		// Token: 0x04001301 RID: 4865
		private volatile bool closeSent;

		// Token: 0x04001302 RID: 4866
		private volatile bool closed;

		// Token: 0x04001303 RID: 4867
		private DateTime lastPing = DateTime.MinValue;

		// Token: 0x04001304 RID: 4868
		private DateTime lastMessage = DateTime.MinValue;

		// Token: 0x04001305 RID: 4869
		private CircularBuffer<int> rtts = new CircularBuffer<int>(WebSocketResponse.RTTBufferCapacity);
	}
}
