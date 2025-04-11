using System;

namespace LitJson
{
	// Token: 0x0200014B RID: 331
	public class JsonException : Exception
	{
		// Token: 0x06000B9E RID: 2974 RVA: 0x0008E219 File Offset: 0x0008C419
		public JsonException()
		{
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0008E221 File Offset: 0x0008C421
		internal JsonException(ParserToken token) : base(string.Format("Invalid token '{0}' in input string", token))
		{
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0008E239 File Offset: 0x0008C439
		internal JsonException(ParserToken token, Exception inner_exception) : base(string.Format("Invalid token '{0}' in input string", token), inner_exception)
		{
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0008E252 File Offset: 0x0008C452
		internal JsonException(int c) : base(string.Format("Invalid character '{0}' in input string", (char)c))
		{
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0008E26B File Offset: 0x0008C46B
		internal JsonException(int c, Exception inner_exception) : base(string.Format("Invalid character '{0}' in input string", (char)c), inner_exception)
		{
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0008E285 File Offset: 0x0008C485
		public JsonException(string message) : base(message)
		{
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0008E28E File Offset: 0x0008C48E
		public JsonException(string message, Exception inner_exception) : base(message, inner_exception)
		{
		}
	}
}
