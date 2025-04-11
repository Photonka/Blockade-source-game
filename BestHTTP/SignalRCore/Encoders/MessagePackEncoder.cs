using System;

namespace BestHTTP.SignalRCore.Encoders
{
	// Token: 0x020001DE RID: 478
	public sealed class MessagePackEncoder : IEncoder
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060011E8 RID: 4584 RVA: 0x000A5E5B File Offset: 0x000A405B
		public string Name
		{
			get
			{
				return "messagepack";
			}
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x00096B9B File Offset: 0x00094D9B
		public object ConvertTo(Type toType, object obj)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00096B9B File Offset: 0x00094D9B
		public T DecodeAs<T>(string text)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00096B9B File Offset: 0x00094D9B
		public T DecodeAs<T>(byte[] data)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00096B9B File Offset: 0x00094D9B
		public byte[] EncodeAsBinary<T>(T value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x00096B9B File Offset: 0x00094D9B
		public string EncodeAsText<T>(T value)
		{
			throw new NotImplementedException();
		}
	}
}
