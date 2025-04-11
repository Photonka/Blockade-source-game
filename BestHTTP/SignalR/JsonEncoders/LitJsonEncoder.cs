using System;
using System.Collections.Generic;
using LitJson;

namespace BestHTTP.SignalR.JsonEncoders
{
	// Token: 0x0200020B RID: 523
	public sealed class LitJsonEncoder : IJsonEncoder
	{
		// Token: 0x06001352 RID: 4946 RVA: 0x000AA2BC File Offset: 0x000A84BC
		public string Encode(object obj)
		{
			JsonWriter jsonWriter = new JsonWriter();
			JsonMapper.ToJson(obj, jsonWriter);
			return jsonWriter.ToString();
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x000AA2DC File Offset: 0x000A84DC
		public IDictionary<string, object> DecodeMessage(string json)
		{
			return JsonMapper.ToObject<Dictionary<string, object>>(new JsonReader(json));
		}
	}
}
