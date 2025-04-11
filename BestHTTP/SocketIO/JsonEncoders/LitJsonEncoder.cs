using System;
using System.Collections.Generic;
using LitJson;

namespace BestHTTP.SocketIO.JsonEncoders
{
	// Token: 0x020001C7 RID: 455
	public sealed class LitJsonEncoder : IJsonEncoder
	{
		// Token: 0x06001143 RID: 4419 RVA: 0x000A423B File Offset: 0x000A243B
		public List<object> Decode(string json)
		{
			return JsonMapper.ToObject<List<object>>(new JsonReader(json));
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x000A4248 File Offset: 0x000A2448
		public string Encode(List<object> obj)
		{
			JsonWriter jsonWriter = new JsonWriter();
			JsonMapper.ToJson(obj, jsonWriter);
			return jsonWriter.ToString();
		}
	}
}
