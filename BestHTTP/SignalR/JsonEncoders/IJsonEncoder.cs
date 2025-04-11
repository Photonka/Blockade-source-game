using System;
using System.Collections.Generic;

namespace BestHTTP.SignalR.JsonEncoders
{
	// Token: 0x0200020A RID: 522
	public interface IJsonEncoder
	{
		// Token: 0x06001350 RID: 4944
		string Encode(object obj);

		// Token: 0x06001351 RID: 4945
		IDictionary<string, object> DecodeMessage(string json);
	}
}
