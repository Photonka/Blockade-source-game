using System;
using System.Collections.Generic;
using BestHTTP.JSON;

namespace BestHTTP.SignalR
{
	// Token: 0x020001F8 RID: 504
	public sealed class NegotiationData
	{
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x000A852C File Offset: 0x000A672C
		// (set) Token: 0x060012A5 RID: 4773 RVA: 0x000A8534 File Offset: 0x000A6734
		public string Url { get; private set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x000A853D File Offset: 0x000A673D
		// (set) Token: 0x060012A7 RID: 4775 RVA: 0x000A8545 File Offset: 0x000A6745
		public string WebSocketServerUrl { get; private set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x000A854E File Offset: 0x000A674E
		// (set) Token: 0x060012A9 RID: 4777 RVA: 0x000A8556 File Offset: 0x000A6756
		public string ConnectionToken { get; private set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x000A855F File Offset: 0x000A675F
		// (set) Token: 0x060012AB RID: 4779 RVA: 0x000A8567 File Offset: 0x000A6767
		public string ConnectionId { get; private set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x000A8570 File Offset: 0x000A6770
		// (set) Token: 0x060012AD RID: 4781 RVA: 0x000A8578 File Offset: 0x000A6778
		public TimeSpan? KeepAliveTimeout { get; private set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x000A8581 File Offset: 0x000A6781
		// (set) Token: 0x060012AF RID: 4783 RVA: 0x000A8589 File Offset: 0x000A6789
		public TimeSpan DisconnectTimeout { get; private set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x000A8592 File Offset: 0x000A6792
		// (set) Token: 0x060012B1 RID: 4785 RVA: 0x000A859A File Offset: 0x000A679A
		public TimeSpan ConnectionTimeout { get; private set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x000A85A3 File Offset: 0x000A67A3
		// (set) Token: 0x060012B3 RID: 4787 RVA: 0x000A85AB File Offset: 0x000A67AB
		public bool TryWebSockets { get; private set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x000A85B4 File Offset: 0x000A67B4
		// (set) Token: 0x060012B5 RID: 4789 RVA: 0x000A85BC File Offset: 0x000A67BC
		public string ProtocolVersion { get; private set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x000A85C5 File Offset: 0x000A67C5
		// (set) Token: 0x060012B7 RID: 4791 RVA: 0x000A85CD File Offset: 0x000A67CD
		public TimeSpan TransportConnectTimeout { get; private set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x000A85D6 File Offset: 0x000A67D6
		// (set) Token: 0x060012B9 RID: 4793 RVA: 0x000A85DE File Offset: 0x000A67DE
		public TimeSpan LongPollDelay { get; private set; }

		// Token: 0x060012BA RID: 4794 RVA: 0x000A85E7 File Offset: 0x000A67E7
		public NegotiationData(Connection connection)
		{
			this.Connection = connection;
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x000A85F8 File Offset: 0x000A67F8
		public void Start()
		{
			this.NegotiationRequest = new HTTPRequest(this.Connection.BuildUri(RequestTypes.Negotiate), HTTPMethods.Get, true, true, new OnRequestFinishedDelegate(this.OnNegotiationRequestFinished));
			this.Connection.PrepareRequest(this.NegotiationRequest, RequestTypes.Negotiate);
			this.NegotiationRequest.Send();
			HTTPManager.Logger.Information("NegotiationData", "Negotiation request sent");
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x000A865E File Offset: 0x000A685E
		public void Abort()
		{
			if (this.NegotiationRequest != null)
			{
				this.OnReceived = null;
				this.OnError = null;
				this.NegotiationRequest.Abort();
			}
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x000A8684 File Offset: 0x000A6884
		private void OnNegotiationRequestFinished(HTTPRequest req, HTTPResponse resp)
		{
			this.NegotiationRequest = null;
			HTTPRequestStates state = req.State;
			if (state != HTTPRequestStates.Finished)
			{
				if (state == HTTPRequestStates.Error)
				{
					this.RaiseOnError((req.Exception != null) ? (req.Exception.Message + " " + req.Exception.StackTrace) : string.Empty);
					return;
				}
				this.RaiseOnError(req.State.ToString());
			}
			else
			{
				if (!resp.IsSuccess)
				{
					this.RaiseOnError(string.Format("Negotiation request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2} Uri: {3}", new object[]
					{
						resp.StatusCode,
						resp.Message,
						resp.DataAsText,
						req.CurrentUri
					}));
					return;
				}
				HTTPManager.Logger.Information("NegotiationData", "Negotiation data arrived: " + resp.DataAsText);
				int num = resp.DataAsText.IndexOf("{");
				if (num < 0)
				{
					this.RaiseOnError("Invalid negotiation text: " + resp.DataAsText);
					return;
				}
				if (this.Parse(resp.DataAsText.Substring(num)) == null)
				{
					this.RaiseOnError("Parsing Negotiation data failed: " + resp.DataAsText);
					return;
				}
				if (this.OnReceived != null)
				{
					this.OnReceived(this);
					this.OnReceived = null;
					return;
				}
			}
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x000A87DF File Offset: 0x000A69DF
		private void RaiseOnError(string err)
		{
			HTTPManager.Logger.Error("NegotiationData", "Negotiation request failed with error: " + err);
			if (this.OnError != null)
			{
				this.OnError(this, err);
				this.OnError = null;
			}
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x000A8818 File Offset: 0x000A6A18
		private NegotiationData Parse(string str)
		{
			bool flag = false;
			Dictionary<string, object> dictionary = Json.Decode(str, ref flag) as Dictionary<string, object>;
			if (!flag)
			{
				return null;
			}
			try
			{
				this.Url = NegotiationData.GetString(dictionary, "Url");
				if (dictionary.ContainsKey("webSocketServerUrl"))
				{
					this.WebSocketServerUrl = NegotiationData.GetString(dictionary, "webSocketServerUrl");
				}
				this.ConnectionToken = Uri.EscapeDataString(NegotiationData.GetString(dictionary, "ConnectionToken"));
				this.ConnectionId = NegotiationData.GetString(dictionary, "ConnectionId");
				if (dictionary.ContainsKey("KeepAliveTimeout"))
				{
					this.KeepAliveTimeout = new TimeSpan?(TimeSpan.FromSeconds(NegotiationData.GetDouble(dictionary, "KeepAliveTimeout")));
				}
				this.DisconnectTimeout = TimeSpan.FromSeconds(NegotiationData.GetDouble(dictionary, "DisconnectTimeout"));
				if (dictionary.ContainsKey("ConnectionTimeout"))
				{
					this.ConnectionTimeout = TimeSpan.FromSeconds(NegotiationData.GetDouble(dictionary, "ConnectionTimeout"));
				}
				else
				{
					this.ConnectionTimeout = TimeSpan.FromSeconds(120.0);
				}
				this.TryWebSockets = (bool)NegotiationData.Get(dictionary, "TryWebSockets");
				this.ProtocolVersion = NegotiationData.GetString(dictionary, "ProtocolVersion");
				this.TransportConnectTimeout = TimeSpan.FromSeconds(NegotiationData.GetDouble(dictionary, "TransportConnectTimeout"));
				if (dictionary.ContainsKey("LongPollDelay"))
				{
					this.LongPollDelay = TimeSpan.FromSeconds(NegotiationData.GetDouble(dictionary, "LongPollDelay"));
				}
			}
			catch (Exception ex)
			{
				HTTPManager.Logger.Exception("NegotiationData", "Parse", ex);
				return null;
			}
			return this;
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x000A89A4 File Offset: 0x000A6BA4
		private static object Get(Dictionary<string, object> from, string key)
		{
			object result;
			if (!from.TryGetValue(key, out result))
			{
				throw new Exception(string.Format("Can't get {0} from Negotiation data!", key));
			}
			return result;
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x000A89CE File Offset: 0x000A6BCE
		private static string GetString(Dictionary<string, object> from, string key)
		{
			return NegotiationData.Get(from, key) as string;
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x000A89DC File Offset: 0x000A6BDC
		private static List<string> GetStringList(Dictionary<string, object> from, string key)
		{
			List<object> list = NegotiationData.Get(from, key) as List<object>;
			List<string> list2 = new List<string>(list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				string text = list[i] as string;
				if (text != null)
				{
					list2.Add(text);
				}
			}
			return list2;
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x000A8A2B File Offset: 0x000A6C2B
		private static int GetInt(Dictionary<string, object> from, string key)
		{
			return (int)((double)NegotiationData.Get(from, key));
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x000A8A3A File Offset: 0x000A6C3A
		private static double GetDouble(Dictionary<string, object> from, string key)
		{
			return (double)NegotiationData.Get(from, key);
		}

		// Token: 0x04001477 RID: 5239
		public Action<NegotiationData> OnReceived;

		// Token: 0x04001478 RID: 5240
		public Action<NegotiationData, string> OnError;

		// Token: 0x04001479 RID: 5241
		private HTTPRequest NegotiationRequest;

		// Token: 0x0400147A RID: 5242
		private IConnection Connection;
	}
}
