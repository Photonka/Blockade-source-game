using System;
using System.Collections.Generic;

namespace BestHTTP.Extensions
{
	// Token: 0x020007D7 RID: 2007
	public sealed class HeaderParser : KeyValuePairList
	{
		// Token: 0x060047CF RID: 18383 RVA: 0x00199F51 File Offset: 0x00198151
		public HeaderParser(string headerStr)
		{
			base.Values = this.Parse(headerStr);
		}

		// Token: 0x060047D0 RID: 18384 RVA: 0x00199F68 File Offset: 0x00198168
		private List<HeaderValue> Parse(string headerStr)
		{
			List<HeaderValue> list = new List<HeaderValue>();
			int i = 0;
			try
			{
				while (i < headerStr.Length)
				{
					HeaderValue headerValue = new HeaderValue();
					headerValue.Parse(headerStr, ref i);
					list.Add(headerValue);
				}
			}
			catch (Exception ex)
			{
				HTTPManager.Logger.Exception("HeaderParser - Parse", headerStr, ex);
			}
			return list;
		}
	}
}
