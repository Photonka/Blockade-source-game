using System;
using LitJson;

namespace BestHTTP.SignalRCore.Encoders
{
	// Token: 0x020001DD RID: 477
	public sealed class LitJsonEncoder : IEncoder
	{
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x000A5D39 File Offset: 0x000A3F39
		public string Name
		{
			get
			{
				return "json";
			}
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x000A5D40 File Offset: 0x000A3F40
		public LitJsonEncoder()
		{
			JsonMapper.RegisterImporter<int, long>((int input) => (long)input);
			JsonMapper.RegisterImporter<long, int>((long input) => (int)input);
			JsonMapper.RegisterImporter<double, int>((double input) => (int)(input + 0.5));
			JsonMapper.RegisterImporter<string, DateTime>((string input) => Convert.ToDateTime(input).ToUniversalTime());
			JsonMapper.RegisterImporter<double, float>((double input) => (float)input);
			JsonMapper.RegisterImporter<string, byte[]>((string input) => Convert.FromBase64String(input));
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x000A5E2B File Offset: 0x000A402B
		public T DecodeAs<T>(string text)
		{
			return JsonMapper.ToObject<T>(text);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00096B9B File Offset: 0x00094D9B
		public T DecodeAs<T>(byte[] data)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00096B9B File Offset: 0x00094D9B
		public byte[] EncodeAsBinary<T>(T value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x000A5E33 File Offset: 0x000A4033
		public string EncodeAsText<T>(T value)
		{
			return JsonMapper.ToJson(value);
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x000A5E40 File Offset: 0x000A4040
		public object ConvertTo(Type toType, object obj)
		{
			string json = JsonMapper.ToJson(obj);
			return JsonMapper.ToObject(toType, json);
		}
	}
}
