using System;
using System.Collections.Generic;

namespace BestHTTP.SocketIO.JsonEncoders
{
	// Token: 0x020001C6 RID: 454
	public interface IJsonEncoder
	{
		// Token: 0x06001141 RID: 4417
		List<object> Decode(string json);

		// Token: 0x06001142 RID: 4418
		string Encode(List<object> obj);
	}
}
