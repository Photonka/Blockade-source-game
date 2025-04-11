using System;
using System.Collections.Generic;
using BestHTTP.Extensions;

namespace BestHTTP.ServerSentEvents
{
	// Token: 0x0200021F RID: 543
	public class EventSource : IHeartbeat
	{
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x000AAEE7 File Offset: 0x000A90E7
		// (set) Token: 0x060013CD RID: 5069 RVA: 0x000AAEEF File Offset: 0x000A90EF
		public Uri Uri { get; private set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x000AAEF8 File Offset: 0x000A90F8
		// (set) Token: 0x060013CF RID: 5071 RVA: 0x000AAF00 File Offset: 0x000A9100
		public States State
		{
			get
			{
				return this._state;
			}
			private set
			{
				States state = this._state;
				this._state = value;
				if (this.OnStateChanged != null)
				{
					try
					{
						this.OnStateChanged(this, state, this._state);
					}
					catch (Exception ex)
					{
						HTTPManager.Logger.Exception("EventSource", "OnStateChanged", ex);
					}
				}
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x000AAF60 File Offset: 0x000A9160
		// (set) Token: 0x060013D1 RID: 5073 RVA: 0x000AAF68 File Offset: 0x000A9168
		public TimeSpan ReconnectionTime { get; set; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x000AAF71 File Offset: 0x000A9171
		// (set) Token: 0x060013D3 RID: 5075 RVA: 0x000AAF79 File Offset: 0x000A9179
		public string LastEventId { get; private set; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x000AAF82 File Offset: 0x000A9182
		// (set) Token: 0x060013D5 RID: 5077 RVA: 0x000AAF8A File Offset: 0x000A918A
		public HTTPRequest InternalRequest { get; private set; }

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x060013D6 RID: 5078 RVA: 0x000AAF94 File Offset: 0x000A9194
		// (remove) Token: 0x060013D7 RID: 5079 RVA: 0x000AAFCC File Offset: 0x000A91CC
		public event OnGeneralEventDelegate OnOpen;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x060013D8 RID: 5080 RVA: 0x000AB004 File Offset: 0x000A9204
		// (remove) Token: 0x060013D9 RID: 5081 RVA: 0x000AB03C File Offset: 0x000A923C
		public event OnMessageDelegate OnMessage;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x060013DA RID: 5082 RVA: 0x000AB074 File Offset: 0x000A9274
		// (remove) Token: 0x060013DB RID: 5083 RVA: 0x000AB0AC File Offset: 0x000A92AC
		public event OnErrorDelegate OnError;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x060013DC RID: 5084 RVA: 0x000AB0E4 File Offset: 0x000A92E4
		// (remove) Token: 0x060013DD RID: 5085 RVA: 0x000AB11C File Offset: 0x000A931C
		public event OnRetryDelegate OnRetry;

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x060013DE RID: 5086 RVA: 0x000AB154 File Offset: 0x000A9354
		// (remove) Token: 0x060013DF RID: 5087 RVA: 0x000AB18C File Offset: 0x000A938C
		public event OnGeneralEventDelegate OnClosed;

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x060013E0 RID: 5088 RVA: 0x000AB1C4 File Offset: 0x000A93C4
		// (remove) Token: 0x060013E1 RID: 5089 RVA: 0x000AB1FC File Offset: 0x000A93FC
		public event OnStateChangedDelegate OnStateChanged;

		// Token: 0x060013E2 RID: 5090 RVA: 0x000AB234 File Offset: 0x000A9434
		public EventSource(Uri uri)
		{
			this.Uri = uri;
			this.ReconnectionTime = TimeSpan.FromMilliseconds(2000.0);
			this.InternalRequest = new HTTPRequest(this.Uri, HTTPMethods.Get, true, true, new OnRequestFinishedDelegate(this.OnRequestFinished));
			this.InternalRequest.SetHeader("Accept", "text/event-stream");
			this.InternalRequest.SetHeader("Cache-Control", "no-cache");
			this.InternalRequest.SetHeader("Accept-Encoding", "identity");
			this.InternalRequest.ProtocolHandler = SupportedProtocols.ServerSentEvents;
			this.InternalRequest.OnUpgraded = new OnRequestFinishedDelegate(this.OnUpgraded);
			this.InternalRequest.DisableRetry = true;
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x000AB2F0 File Offset: 0x000A94F0
		public void Open()
		{
			if (this.State != States.Initial && this.State != States.Retrying && this.State != States.Closed)
			{
				return;
			}
			this.State = States.Connecting;
			if (!string.IsNullOrEmpty(this.LastEventId))
			{
				this.InternalRequest.SetHeader("Last-Event-ID", this.LastEventId);
			}
			this.InternalRequest.Send();
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x000AB34E File Offset: 0x000A954E
		public void Close()
		{
			if (this.State == States.Closing || this.State == States.Closed)
			{
				return;
			}
			this.State = States.Closing;
			if (this.InternalRequest != null)
			{
				this.InternalRequest.Abort();
				return;
			}
			this.State = States.Closed;
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x000AB385 File Offset: 0x000A9585
		public void On(string eventName, OnEventDelegate action)
		{
			if (this.EventTable == null)
			{
				this.EventTable = new Dictionary<string, OnEventDelegate>();
			}
			this.EventTable[eventName] = action;
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x000AB3A7 File Offset: 0x000A95A7
		public void Off(string eventName)
		{
			if (eventName == null || this.EventTable == null)
			{
				return;
			}
			this.EventTable.Remove(eventName);
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x000AB3C4 File Offset: 0x000A95C4
		private void CallOnError(string error, string msg)
		{
			if (this.OnError != null)
			{
				try
				{
					this.OnError(this, error);
				}
				catch (Exception ex)
				{
					HTTPManager.Logger.Exception("EventSource", msg + " - OnError", ex);
				}
			}
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x000AB418 File Offset: 0x000A9618
		private bool CallOnRetry()
		{
			if (this.OnRetry != null)
			{
				try
				{
					return this.OnRetry(this);
				}
				catch (Exception ex)
				{
					HTTPManager.Logger.Exception("EventSource", "CallOnRetry", ex);
				}
				return true;
			}
			return true;
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x000AB468 File Offset: 0x000A9668
		private void SetClosed(string msg)
		{
			this.State = States.Closed;
			if (this.OnClosed != null)
			{
				try
				{
					this.OnClosed(this);
				}
				catch (Exception ex)
				{
					HTTPManager.Logger.Exception("EventSource", msg + " - OnClosed", ex);
				}
			}
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x000AB4C0 File Offset: 0x000A96C0
		private void Retry()
		{
			if (this.RetryCount > 0 || !this.CallOnRetry())
			{
				this.SetClosed("Retry");
				return;
			}
			this.RetryCount += 1;
			this.RetryCalled = DateTime.UtcNow;
			HTTPManager.Heartbeats.Subscribe(this);
			this.State = States.Retrying;
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x000AB518 File Offset: 0x000A9718
		private void OnUpgraded(HTTPRequest originalRequest, HTTPResponse response)
		{
			EventSourceResponse eventSourceResponse = response as EventSourceResponse;
			if (eventSourceResponse == null)
			{
				this.CallOnError("Not an EventSourceResponse!", "OnUpgraded");
				return;
			}
			if (this.OnOpen != null)
			{
				try
				{
					this.OnOpen(this);
				}
				catch (Exception ex)
				{
					HTTPManager.Logger.Exception("EventSource", "OnOpen", ex);
				}
			}
			EventSourceResponse eventSourceResponse2 = eventSourceResponse;
			eventSourceResponse2.OnMessage = (Action<EventSourceResponse, Message>)Delegate.Combine(eventSourceResponse2.OnMessage, new Action<EventSourceResponse, Message>(this.OnMessageReceived));
			eventSourceResponse.StartReceive();
			this.RetryCount = 0;
			this.State = States.Open;
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x000AB5B4 File Offset: 0x000A97B4
		private void OnRequestFinished(HTTPRequest req, HTTPResponse resp)
		{
			if (this.State == States.Closed)
			{
				return;
			}
			if (this.State == States.Closing || req.State == HTTPRequestStates.Aborted)
			{
				this.SetClosed("OnRequestFinished");
				return;
			}
			string text = string.Empty;
			bool flag = true;
			switch (req.State)
			{
			case HTTPRequestStates.Processing:
				flag = !resp.HasHeader("content-length");
				break;
			case HTTPRequestStates.Finished:
				if (resp.StatusCode == 200 && !resp.HasHeaderWithValue("content-type", "text/event-stream"))
				{
					text = "No Content-Type header with value 'text/event-stream' present.";
					flag = false;
				}
				if (flag && resp.StatusCode != 500 && resp.StatusCode != 502 && resp.StatusCode != 503 && resp.StatusCode != 504)
				{
					flag = false;
					text = string.Format("Request Finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}", resp.StatusCode, resp.Message, resp.DataAsText);
				}
				break;
			case HTTPRequestStates.Error:
				text = "Request Finished with Error! " + ((req.Exception != null) ? (req.Exception.Message + "\n" + req.Exception.StackTrace) : "No Exception");
				break;
			case HTTPRequestStates.Aborted:
				text = "OnRequestFinished - Aborted without request. EventSource's State: " + this.State;
				break;
			case HTTPRequestStates.ConnectionTimedOut:
				text = "Connection Timed Out!";
				break;
			case HTTPRequestStates.TimedOut:
				text = "Processing the request Timed Out!";
				break;
			}
			if (this.State >= States.Closing)
			{
				this.SetClosed("OnRequestFinished");
				return;
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.CallOnError(text, "OnRequestFinished");
			}
			if (flag)
			{
				this.Retry();
				return;
			}
			this.SetClosed("OnRequestFinished");
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x000AB764 File Offset: 0x000A9964
		private void OnMessageReceived(EventSourceResponse resp, Message message)
		{
			if (this.State >= States.Closing)
			{
				return;
			}
			if (message.Id != null)
			{
				this.LastEventId = message.Id;
			}
			if (message.Retry.TotalMilliseconds > 0.0)
			{
				this.ReconnectionTime = message.Retry;
			}
			if (string.IsNullOrEmpty(message.Data))
			{
				return;
			}
			if (this.OnMessage != null)
			{
				try
				{
					this.OnMessage(this, message);
				}
				catch (Exception ex)
				{
					HTTPManager.Logger.Exception("EventSource", "OnMessageReceived - OnMessage", ex);
				}
			}
			OnEventDelegate onEventDelegate;
			if (this.EventTable != null && !string.IsNullOrEmpty(message.Event) && this.EventTable.TryGetValue(message.Event, out onEventDelegate) && onEventDelegate != null)
			{
				try
				{
					onEventDelegate(this, message);
				}
				catch (Exception ex2)
				{
					HTTPManager.Logger.Exception("EventSource", "OnMessageReceived - action", ex2);
				}
			}
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x000AB85C File Offset: 0x000A9A5C
		void IHeartbeat.OnHeartbeatUpdate(TimeSpan dif)
		{
			if (this.State != States.Retrying)
			{
				HTTPManager.Heartbeats.Unsubscribe(this);
				return;
			}
			if (DateTime.UtcNow - this.RetryCalled >= this.ReconnectionTime)
			{
				this.Open();
				if (this.State != States.Connecting)
				{
					this.SetClosed("OnHeartbeatUpdate");
				}
				HTTPManager.Heartbeats.Unsubscribe(this);
			}
		}

		// Token: 0x040014C0 RID: 5312
		private States _state;

		// Token: 0x040014CA RID: 5322
		private Dictionary<string, OnEventDelegate> EventTable;

		// Token: 0x040014CB RID: 5323
		private byte RetryCount;

		// Token: 0x040014CC RID: 5324
		private DateTime RetryCalled;
	}
}
