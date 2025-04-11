using System;
using System.Collections.Generic;
using System.Text;
using BestHTTP.Logger;

namespace BestHTTP.SocketIO.Transports
{
	// Token: 0x020001C3 RID: 451
	internal sealed class PollingTransport : ITransport
	{
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public TransportTypes Type
		{
			get
			{
				return TransportTypes.Polling;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x000A2FE9 File Offset: 0x000A11E9
		// (set) Token: 0x0600111A RID: 4378 RVA: 0x000A2FF1 File Offset: 0x000A11F1
		public TransportStates State { get; private set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x000A2FFA File Offset: 0x000A11FA
		// (set) Token: 0x0600111C RID: 4380 RVA: 0x000A3002 File Offset: 0x000A1202
		public SocketManager Manager { get; private set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x000A300B File Offset: 0x000A120B
		public bool IsRequestInProgress
		{
			get
			{
				return this.LastRequest != null;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x000A3016 File Offset: 0x000A1216
		public bool IsPollingInProgress
		{
			get
			{
				return this.PollRequest != null;
			}
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x000A3021 File Offset: 0x000A1221
		public PollingTransport(SocketManager manager)
		{
			this.Manager = manager;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x000A303C File Offset: 0x000A123C
		public void Open()
		{
			string text = "{0}?EIO={1}&transport=polling&t={2}-{3}{5}";
			if (this.Manager.Handshake != null)
			{
				text += "&sid={4}";
			}
			bool flag = !this.Manager.Options.QueryParamsOnlyForHandshake || (this.Manager.Options.QueryParamsOnlyForHandshake && this.Manager.Handshake == null);
			string format = text;
			object[] array = new object[6];
			array[0] = this.Manager.Uri.ToString();
			array[1] = 4;
			array[2] = this.Manager.Timestamp.ToString();
			int num = 3;
			SocketManager manager = this.Manager;
			ulong requestCounter = manager.RequestCounter;
			manager.RequestCounter = requestCounter + 1UL;
			array[num] = requestCounter.ToString();
			array[4] = ((this.Manager.Handshake != null) ? this.Manager.Handshake.Sid : string.Empty);
			array[5] = (flag ? this.Manager.Options.BuildQueryParams() : string.Empty);
			new HTTPRequest(new Uri(string.Format(format, array)), new OnRequestFinishedDelegate(this.OnRequestFinished))
			{
				DisableCache = true,
				DisableRetry = true
			}.Send();
			this.State = TransportStates.Opening;
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x000A3174 File Offset: 0x000A1374
		public void Close()
		{
			if (this.State == TransportStates.Closed)
			{
				return;
			}
			this.State = TransportStates.Closed;
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x000A3188 File Offset: 0x000A1388
		public void Send(Packet packet)
		{
			try
			{
				this.lonelyPacketList.Add(packet);
				this.Send(this.lonelyPacketList);
			}
			finally
			{
				this.lonelyPacketList.Clear();
			}
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x000A31CC File Offset: 0x000A13CC
		public void Send(List<Packet> packets)
		{
			if (this.State != TransportStates.Opening && this.State != TransportStates.Open)
			{
				return;
			}
			if (this.IsRequestInProgress)
			{
				throw new Exception("Sending packets are still in progress!");
			}
			byte[] array = null;
			try
			{
				array = packets[0].EncodeBinary();
				for (int i = 1; i < packets.Count; i++)
				{
					byte[] array2 = packets[i].EncodeBinary();
					Array.Resize<byte>(ref array, array.Length + array2.Length);
					Array.Copy(array2, 0, array, array.Length - array2.Length, array2.Length);
				}
				packets.Clear();
			}
			catch (Exception ex)
			{
				((IManager)this.Manager).EmitError(SocketIOErrors.Internal, ex.Message + " " + ex.StackTrace);
				return;
			}
			string format = "{0}?EIO={1}&transport=polling&t={2}-{3}&sid={4}{5}";
			object[] array3 = new object[6];
			array3[0] = this.Manager.Uri.ToString();
			array3[1] = 4;
			array3[2] = this.Manager.Timestamp.ToString();
			int num = 3;
			SocketManager manager = this.Manager;
			ulong requestCounter = manager.RequestCounter;
			manager.RequestCounter = requestCounter + 1UL;
			array3[num] = requestCounter.ToString();
			array3[4] = this.Manager.Handshake.Sid;
			array3[5] = ((!this.Manager.Options.QueryParamsOnlyForHandshake) ? this.Manager.Options.BuildQueryParams() : string.Empty);
			this.LastRequest = new HTTPRequest(new Uri(string.Format(format, array3)), HTTPMethods.Post, new OnRequestFinishedDelegate(this.OnRequestFinished));
			this.LastRequest.DisableCache = true;
			this.LastRequest.SetHeader("Content-Type", "application/octet-stream");
			this.LastRequest.RawData = array;
			this.LastRequest.Send();
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x000A3388 File Offset: 0x000A1588
		private void OnRequestFinished(HTTPRequest req, HTTPResponse resp)
		{
			this.LastRequest = null;
			if (this.State == TransportStates.Closed)
			{
				return;
			}
			string text = null;
			switch (req.State)
			{
			case HTTPRequestStates.Finished:
				if (HTTPManager.Logger.Level <= Loglevels.All)
				{
					HTTPManager.Logger.Verbose("PollingTransport", "OnRequestFinished: " + resp.DataAsText);
				}
				if (resp.IsSuccess)
				{
					if (req.MethodType != HTTPMethods.Post)
					{
						this.ParseResponse(resp);
					}
				}
				else
				{
					text = string.Format("Polling - Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2} Uri: {3}", new object[]
					{
						resp.StatusCode,
						resp.Message,
						resp.DataAsText,
						req.CurrentUri
					});
				}
				break;
			case HTTPRequestStates.Error:
				text = ((req.Exception != null) ? (req.Exception.Message + "\n" + req.Exception.StackTrace) : "No Exception");
				break;
			case HTTPRequestStates.Aborted:
				text = string.Format("Polling - Request({0}) Aborted!", req.CurrentUri);
				break;
			case HTTPRequestStates.ConnectionTimedOut:
				text = string.Format("Polling - Connection Timed Out! Uri: {0}", req.CurrentUri);
				break;
			case HTTPRequestStates.TimedOut:
				text = string.Format("Polling - Processing the request({0}) Timed Out!", req.CurrentUri);
				break;
			}
			if (!string.IsNullOrEmpty(text))
			{
				((IManager)this.Manager).OnTransportError(this, text);
			}
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x000A34D8 File Offset: 0x000A16D8
		public void Poll()
		{
			if (this.PollRequest != null || this.State == TransportStates.Paused)
			{
				return;
			}
			string format = "{0}?EIO={1}&transport=polling&t={2}-{3}&sid={4}{5}";
			object[] array = new object[6];
			array[0] = this.Manager.Uri.ToString();
			array[1] = 4;
			array[2] = this.Manager.Timestamp.ToString();
			int num = 3;
			SocketManager manager = this.Manager;
			ulong requestCounter = manager.RequestCounter;
			manager.RequestCounter = requestCounter + 1UL;
			array[num] = requestCounter.ToString();
			array[4] = this.Manager.Handshake.Sid;
			array[5] = ((!this.Manager.Options.QueryParamsOnlyForHandshake) ? this.Manager.Options.BuildQueryParams() : string.Empty);
			this.PollRequest = new HTTPRequest(new Uri(string.Format(format, array)), HTTPMethods.Get, new OnRequestFinishedDelegate(this.OnPollRequestFinished));
			this.PollRequest.DisableCache = true;
			this.PollRequest.DisableRetry = true;
			this.PollRequest.Send();
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x000A35DC File Offset: 0x000A17DC
		private void OnPollRequestFinished(HTTPRequest req, HTTPResponse resp)
		{
			this.PollRequest = null;
			if (this.State == TransportStates.Closed)
			{
				return;
			}
			string text = null;
			switch (req.State)
			{
			case HTTPRequestStates.Finished:
				if (HTTPManager.Logger.Level <= Loglevels.All)
				{
					HTTPManager.Logger.Verbose("PollingTransport", "OnPollRequestFinished: " + resp.DataAsText);
				}
				if (resp.IsSuccess)
				{
					this.ParseResponse(resp);
				}
				else
				{
					text = string.Format("Polling - Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2} Uri: {3}", new object[]
					{
						resp.StatusCode,
						resp.Message,
						resp.DataAsText,
						req.CurrentUri
					});
				}
				break;
			case HTTPRequestStates.Error:
				text = ((req.Exception != null) ? (req.Exception.Message + "\n" + req.Exception.StackTrace) : "No Exception");
				break;
			case HTTPRequestStates.Aborted:
				text = string.Format("Polling - Request({0}) Aborted!", req.CurrentUri);
				break;
			case HTTPRequestStates.ConnectionTimedOut:
				text = string.Format("Polling - Connection Timed Out! Uri: {0}", req.CurrentUri);
				break;
			case HTTPRequestStates.TimedOut:
				text = string.Format("Polling - Processing the request({0}) Timed Out!", req.CurrentUri);
				break;
			}
			if (!string.IsNullOrEmpty(text))
			{
				((IManager)this.Manager).OnTransportError(this, text);
			}
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x000A3720 File Offset: 0x000A1920
		private void OnPacket(Packet packet)
		{
			if (packet.AttachmentCount != 0 && !packet.HasAllAttachment)
			{
				this.PacketWithAttachment = packet;
				return;
			}
			TransportEventTypes transportEvent = packet.TransportEvent;
			if (transportEvent != TransportEventTypes.Open)
			{
				if (transportEvent == TransportEventTypes.Message)
				{
					if (packet.SocketIOEvent == SocketIOEventTypes.Connect)
					{
						this.State = TransportStates.Open;
					}
				}
			}
			else if (this.State != TransportStates.Opening)
			{
				HTTPManager.Logger.Warning("PollingTransport", "Received 'Open' packet while state is '" + this.State.ToString() + "'");
			}
			else
			{
				this.State = TransportStates.Open;
			}
			((IManager)this.Manager).OnPacket(packet);
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x000A37B8 File Offset: 0x000A19B8
		private void ParseResponse(HTTPResponse resp)
		{
			try
			{
				if (resp != null && resp.Data != null && resp.Data.Length >= 1)
				{
					int num;
					for (int i = 0; i < resp.Data.Length; i += num)
					{
						PollingTransport.PayloadTypes payloadTypes = PollingTransport.PayloadTypes.Text;
						num = 0;
						if (resp.Data[i] < 48)
						{
							payloadTypes = (PollingTransport.PayloadTypes)resp.Data[i++];
							for (byte b = resp.Data[i++]; b != 255; b = resp.Data[i++])
							{
								num = num * 10 + (int)b;
							}
						}
						else
						{
							for (byte b2 = resp.Data[i++]; b2 != 58; b2 = resp.Data[i++])
							{
								num = num * 10 + (int)(b2 - 48);
							}
						}
						Packet packet = null;
						if (payloadTypes != PollingTransport.PayloadTypes.Text)
						{
							if (payloadTypes == PollingTransport.PayloadTypes.Binary)
							{
								if (this.PacketWithAttachment != null)
								{
									i++;
									num--;
									byte[] array = new byte[num];
									Array.Copy(resp.Data, i, array, 0, num);
									this.PacketWithAttachment.AddAttachmentFromServer(array, true);
									if (this.PacketWithAttachment.HasAllAttachment)
									{
										packet = this.PacketWithAttachment;
										this.PacketWithAttachment = null;
									}
								}
							}
						}
						else
						{
							packet = new Packet(Encoding.UTF8.GetString(resp.Data, i, num));
						}
						if (packet != null)
						{
							try
							{
								this.OnPacket(packet);
							}
							catch (Exception ex)
							{
								HTTPManager.Logger.Exception("PollingTransport", "ParseResponse - OnPacket", ex);
								((IManager)this.Manager).EmitError(SocketIOErrors.Internal, ex.Message + " " + ex.StackTrace);
							}
						}
					}
				}
			}
			catch (Exception ex2)
			{
				((IManager)this.Manager).EmitError(SocketIOErrors.Internal, ex2.Message + " " + ex2.StackTrace);
				HTTPManager.Logger.Exception("PollingTransport", "ParseResponse", ex2);
			}
		}

		// Token: 0x040013A4 RID: 5028
		private HTTPRequest LastRequest;

		// Token: 0x040013A5 RID: 5029
		private HTTPRequest PollRequest;

		// Token: 0x040013A6 RID: 5030
		private Packet PacketWithAttachment;

		// Token: 0x040013A7 RID: 5031
		private List<Packet> lonelyPacketList = new List<Packet>(1);

		// Token: 0x020008C9 RID: 2249
		private enum PayloadTypes : byte
		{
			// Token: 0x04003361 RID: 13153
			Text,
			// Token: 0x04003362 RID: 13154
			Binary
		}
	}
}
