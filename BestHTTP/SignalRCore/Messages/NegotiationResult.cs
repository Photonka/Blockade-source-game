using System;
using System.Collections.Generic;
using BestHTTP.JSON;

namespace BestHTTP.SignalRCore.Messages
{
	// Token: 0x020001E7 RID: 487
	public sealed class NegotiationResult
	{
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x000A65A0 File Offset: 0x000A47A0
		// (set) Token: 0x06001217 RID: 4631 RVA: 0x000A65A8 File Offset: 0x000A47A8
		public string ConnectionId { get; private set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x000A65B1 File Offset: 0x000A47B1
		// (set) Token: 0x06001219 RID: 4633 RVA: 0x000A65B9 File Offset: 0x000A47B9
		public List<SupportedTransport> SupportedTransports { get; private set; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600121A RID: 4634 RVA: 0x000A65C2 File Offset: 0x000A47C2
		// (set) Token: 0x0600121B RID: 4635 RVA: 0x000A65CA File Offset: 0x000A47CA
		public Uri Url { get; private set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x000A65D3 File Offset: 0x000A47D3
		// (set) Token: 0x0600121D RID: 4637 RVA: 0x000A65DB File Offset: 0x000A47DB
		public string AccessToken { get; private set; }

		// Token: 0x0600121E RID: 4638 RVA: 0x000A65E4 File Offset: 0x000A47E4
		internal static NegotiationResult Parse(string json, out string error, HubConnection hub)
		{
			error = null;
			Dictionary<string, object> dictionary = Json.Decode(json) as Dictionary<string, object>;
			if (dictionary == null)
			{
				error = "Json decoding failed!";
				return null;
			}
			NegotiationResult result;
			try
			{
				NegotiationResult negotiationResult = new NegotiationResult();
				object obj;
				if (dictionary.TryGetValue("connectionId", out obj))
				{
					negotiationResult.ConnectionId = obj.ToString();
				}
				if (dictionary.TryGetValue("availableTransports", out obj))
				{
					List<object> list = obj as List<object>;
					if (list != null)
					{
						List<SupportedTransport> list2 = new List<SupportedTransport>(list.Count);
						foreach (object obj2 in list)
						{
							Dictionary<string, object> dictionary2 = (Dictionary<string, object>)obj2;
							string transportName = string.Empty;
							List<string> list3 = null;
							if (dictionary2.TryGetValue("transport", out obj))
							{
								transportName = obj.ToString();
							}
							if (dictionary2.TryGetValue("transferFormats", out obj))
							{
								List<object> list4 = obj as List<object>;
								if (list4 != null)
								{
									list3 = new List<string>(list4.Count);
									foreach (object obj3 in list4)
									{
										list3.Add(obj3.ToString());
									}
								}
							}
							list2.Add(new SupportedTransport(transportName, list3));
						}
						negotiationResult.SupportedTransports = list2;
					}
				}
				if (dictionary.TryGetValue("url", out obj))
				{
					string text = obj.ToString();
					Uri uri;
					if (!Uri.TryCreate(text, UriKind.RelativeOrAbsolute, out uri))
					{
						throw new Exception(string.Format("Couldn't parse url: '{0}'", text));
					}
					if (!uri.IsAbsoluteUri)
					{
						uri = new UriBuilder(hub.Uri)
						{
							Path = text
						}.Uri;
					}
					negotiationResult.Url = uri;
				}
				if (dictionary.TryGetValue("accessToken", out obj))
				{
					negotiationResult.AccessToken = obj.ToString();
				}
				else if (hub.NegotiationResult != null)
				{
					negotiationResult.AccessToken = hub.NegotiationResult.AccessToken;
				}
				result = negotiationResult;
			}
			catch (Exception ex)
			{
				error = "Error while parsing result: " + ex.Message + " StackTrace: " + ex.StackTrace;
				result = null;
			}
			return result;
		}
	}
}
