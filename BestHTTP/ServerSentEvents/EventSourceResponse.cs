using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using BestHTTP.Extensions;

namespace BestHTTP.ServerSentEvents
{
	// Token: 0x02000220 RID: 544
	public sealed class EventSourceResponse : HTTPResponse, IProtocol
	{
		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x000AB8C0 File Offset: 0x000A9AC0
		// (set) Token: 0x060013F0 RID: 5104 RVA: 0x000AB8C8 File Offset: 0x000A9AC8
		public bool IsClosed { get; private set; }

		// Token: 0x060013F1 RID: 5105 RVA: 0x000AB8D1 File Offset: 0x000A9AD1
		public EventSourceResponse(HTTPRequest request, Stream stream, bool isStreamed, bool isFromCache) : base(request, stream, isStreamed, isFromCache)
		{
			base.IsClosedManually = true;
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x000AB8FC File Offset: 0x000A9AFC
		public override bool Receive(int forceReadRawContentLength = -1, bool readPayloadData = true)
		{
			bool flag = base.Receive(forceReadRawContentLength, false);
			string firstHeaderValue = base.GetFirstHeaderValue("content-type");
			base.IsUpgraded = (flag && base.StatusCode == 200 && !string.IsNullOrEmpty(firstHeaderValue) && firstHeaderValue.ToLower().StartsWith("text/event-stream"));
			if (!base.IsUpgraded)
			{
				base.ReadPayload(forceReadRawContentLength);
			}
			return flag;
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x000AB960 File Offset: 0x000A9B60
		internal void StartReceive()
		{
			if (base.IsUpgraded)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.ReceiveThreadFunc));
			}
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x000AB97C File Offset: 0x000A9B7C
		private void ReceiveThreadFunc(object param)
		{
			try
			{
				if (base.HasHeaderWithValue("transfer-encoding", "chunked"))
				{
					this.ReadChunked(this.Stream);
				}
				else
				{
					this.ReadRaw(this.Stream, -1L);
				}
			}
			catch (ThreadAbortException)
			{
				this.baseRequest.State = HTTPRequestStates.Aborted;
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
			}
			finally
			{
				this.IsClosed = true;
			}
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x000ABA28 File Offset: 0x000A9C28
		private new void ReadChunked(Stream stream)
		{
			int num = base.ReadChunkLength(stream);
			byte[] array = VariableSizedBufferPool.Get((long)num, true);
			while (num != 0)
			{
				if (array.Length < num)
				{
					VariableSizedBufferPool.Resize(ref array, num, true);
				}
				int num2 = 0;
				do
				{
					int num3 = stream.Read(array, num2, num - num2);
					if (num3 == 0)
					{
						goto Block_2;
					}
					num2 += num3;
				}
				while (num2 < num);
				this.FeedData(array, num2);
				HTTPResponse.ReadTo(stream, 10);
				num = base.ReadChunkLength(stream);
				continue;
				Block_2:
				throw new Exception("The remote server closed the connection unexpectedly!");
			}
			VariableSizedBufferPool.Release(array);
			base.ReadHeaders(stream);
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x000ABAA8 File Offset: 0x000A9CA8
		private new void ReadRaw(Stream stream, long contentLength)
		{
			byte[] array = VariableSizedBufferPool.Get(1024L, true);
			int num;
			do
			{
				num = stream.Read(array, 0, array.Length);
				this.FeedData(array, num);
			}
			while (num > 0);
			VariableSizedBufferPool.Release(array);
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x000ABAE0 File Offset: 0x000A9CE0
		public void FeedData(byte[] buffer, int count)
		{
			if (count == -1)
			{
				count = buffer.Length;
			}
			if (count == 0)
			{
				return;
			}
			if (this.LineBuffer == null)
			{
				this.LineBuffer = VariableSizedBufferPool.Get(1024L, true);
			}
			int num = 0;
			for (;;)
			{
				int num2 = -1;
				int num3 = 1;
				int num4 = num;
				while (num4 < count && num2 == -1)
				{
					if (buffer[num4] == 13)
					{
						if (num4 + 1 < count && buffer[num4 + 1] == 10)
						{
							num3 = 2;
						}
						num2 = num4;
					}
					else if (buffer[num4] == 10)
					{
						num2 = num4;
					}
					num4++;
				}
				int num5 = (num2 == -1) ? count : num2;
				if (this.LineBuffer.Length < this.LineBufferPos + (num5 - num))
				{
					int newSize = this.LineBufferPos + (num5 - num);
					VariableSizedBufferPool.Resize(ref this.LineBuffer, newSize, true);
				}
				Array.Copy(buffer, num, this.LineBuffer, this.LineBufferPos, num5 - num);
				this.LineBufferPos += num5 - num;
				if (num2 == -1)
				{
					break;
				}
				this.ParseLine(this.LineBuffer, this.LineBufferPos);
				this.LineBufferPos = 0;
				num = num2 + num3;
				if (num2 == -1 || num >= count)
				{
					return;
				}
			}
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x000ABBE4 File Offset: 0x000A9DE4
		private void ParseLine(byte[] buffer, int count)
		{
			if (count == 0)
			{
				if (this.CurrentMessage != null)
				{
					object frameLock = this.FrameLock;
					lock (frameLock)
					{
						this.CompletedMessages.Add(this.CurrentMessage);
					}
					this.CurrentMessage = null;
				}
				return;
			}
			if (buffer[0] == 58)
			{
				return;
			}
			int num = -1;
			int num2 = 0;
			while (num2 < count && num == -1)
			{
				if (buffer[num2] == 58)
				{
					num = num2;
				}
				num2++;
			}
			string @string;
			string text;
			if (num != -1)
			{
				@string = Encoding.UTF8.GetString(buffer, 0, num);
				if (num + 1 < count && buffer[num + 1] == 32)
				{
					num++;
				}
				num++;
				if (num >= count)
				{
					return;
				}
				text = Encoding.UTF8.GetString(buffer, num, count - num);
			}
			else
			{
				@string = Encoding.UTF8.GetString(buffer, 0, count);
				text = string.Empty;
			}
			if (this.CurrentMessage == null)
			{
				this.CurrentMessage = new Message();
			}
			if (@string == "id")
			{
				this.CurrentMessage.Id = text;
				return;
			}
			if (@string == "event")
			{
				this.CurrentMessage.Event = text;
				return;
			}
			if (@string == "data")
			{
				if (this.CurrentMessage.Data != null)
				{
					Message currentMessage = this.CurrentMessage;
					currentMessage.Data += Environment.NewLine;
				}
				Message currentMessage2 = this.CurrentMessage;
				currentMessage2.Data += text;
				return;
			}
			if (!(@string == "retry"))
			{
				return;
			}
			int num3;
			if (int.TryParse(text, out num3))
			{
				this.CurrentMessage.Retry = TimeSpan.FromMilliseconds((double)num3);
			}
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x000ABD80 File Offset: 0x000A9F80
		void IProtocol.HandleEvents()
		{
			object frameLock = this.FrameLock;
			lock (frameLock)
			{
				if (this.CompletedMessages.Count > 0)
				{
					if (this.OnMessage != null)
					{
						for (int i = 0; i < this.CompletedMessages.Count; i++)
						{
							try
							{
								this.OnMessage(this, this.CompletedMessages[i]);
							}
							catch (Exception ex)
							{
								HTTPManager.Logger.Exception("EventSourceMessage", "HandleEvents - OnMessage", ex);
							}
						}
					}
					this.CompletedMessages.Clear();
				}
			}
			if (this.IsClosed)
			{
				this.CompletedMessages.Clear();
				if (this.OnClosed != null)
				{
					try
					{
						this.OnClosed(this);
					}
					catch (Exception ex2)
					{
						HTTPManager.Logger.Exception("EventSourceMessage", "HandleEvents - OnClosed", ex2);
					}
					finally
					{
						this.OnClosed = null;
					}
				}
			}
		}

		// Token: 0x040014CE RID: 5326
		public Action<EventSourceResponse, Message> OnMessage;

		// Token: 0x040014CF RID: 5327
		public Action<EventSourceResponse> OnClosed;

		// Token: 0x040014D0 RID: 5328
		private object FrameLock = new object();

		// Token: 0x040014D1 RID: 5329
		private byte[] LineBuffer;

		// Token: 0x040014D2 RID: 5330
		private int LineBufferPos;

		// Token: 0x040014D3 RID: 5331
		private Message CurrentMessage;

		// Token: 0x040014D4 RID: 5332
		private List<Message> CompletedMessages = new List<Message>();
	}
}
