using System;
using System.Collections.Generic;

namespace BestHTTP.Extensions
{
	// Token: 0x020007E1 RID: 2017
	public sealed class WWWAuthenticateHeaderParser : KeyValuePairList
	{
		// Token: 0x06004816 RID: 18454 RVA: 0x0019AFCA File Offset: 0x001991CA
		public WWWAuthenticateHeaderParser(string headerValue)
		{
			base.Values = this.ParseQuotedHeader(headerValue);
		}

		// Token: 0x06004817 RID: 18455 RVA: 0x0019AFE0 File Offset: 0x001991E0
		private List<HeaderValue> ParseQuotedHeader(string str)
		{
			List<HeaderValue> list = new List<HeaderValue>();
			if (str != null)
			{
				int i = 0;
				string key = str.Read(ref i, (char ch) => !char.IsWhiteSpace(ch) && !char.IsControl(ch), true).TrimAndLower();
				list.Add(new HeaderValue(key));
				while (i < str.Length)
				{
					HeaderValue headerValue = new HeaderValue(str.Read(ref i, '=', true).TrimAndLower());
					str.SkipWhiteSpace(ref i);
					headerValue.Value = str.ReadPossibleQuotedText(ref i);
					list.Add(headerValue);
				}
			}
			return list;
		}
	}
}
