using System;

namespace BestHTTP.SignalRCore
{
	// Token: 0x020001D2 RID: 466
	public interface IEncoder
	{
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600116E RID: 4462
		string Name { get; }

		// Token: 0x0600116F RID: 4463
		string EncodeAsText<T>(T value);

		// Token: 0x06001170 RID: 4464
		T DecodeAs<T>(string text);

		// Token: 0x06001171 RID: 4465
		byte[] EncodeAsBinary<T>(T value);

		// Token: 0x06001172 RID: 4466
		T DecodeAs<T>(byte[] data);

		// Token: 0x06001173 RID: 4467
		object ConvertTo(Type toType, object obj);
	}
}
