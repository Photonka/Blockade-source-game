using System;
using System.Collections.Generic;
using BestHTTP.JSON;

namespace BestHTTP.SignalR.JsonEncoders
{
	// Token: 0x02000209 RID: 521
	public sealed class DefaultJsonEncoder : IJsonEncoder
	{
		// Token: 0x0600134D RID: 4941 RVA: 0x000A4233 File Offset: 0x000A2433
		public string Encode(object obj)
		{
			return Json.Encode(obj);
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x000AA298 File Offset: 0x000A8498
		public IDictionary<string, object> DecodeMessage(string json)
		{
			bool flag = false;
			IDictionary<string, object> result = Json.Decode(json, ref flag) as IDictionary<string, object>;
			if (!flag)
			{
				return null;
			}
			return result;
		}
	}
}
