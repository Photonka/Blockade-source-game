using System;
using System.Collections.Generic;
using BestHTTP.JSON;

namespace BestHTTP.SocketIO.JsonEncoders
{
	// Token: 0x020001C5 RID: 453
	public sealed class DefaultJSonEncoder : IJsonEncoder
	{
		// Token: 0x0600113E RID: 4414 RVA: 0x000A4226 File Offset: 0x000A2426
		public List<object> Decode(string json)
		{
			return Json.Decode(json) as List<object>;
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x000A4233 File Offset: 0x000A2433
		public string Encode(List<object> obj)
		{
			return Json.Encode(obj);
		}
	}
}
