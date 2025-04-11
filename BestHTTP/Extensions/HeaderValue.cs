using System;
using System.Collections.Generic;

namespace BestHTTP.Extensions
{
	// Token: 0x020007D8 RID: 2008
	public sealed class HeaderValue
	{
		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x060047D1 RID: 18385 RVA: 0x00199FC8 File Offset: 0x001981C8
		// (set) Token: 0x060047D2 RID: 18386 RVA: 0x00199FD0 File Offset: 0x001981D0
		public string Key { get; set; }

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x060047D3 RID: 18387 RVA: 0x00199FD9 File Offset: 0x001981D9
		// (set) Token: 0x060047D4 RID: 18388 RVA: 0x00199FE1 File Offset: 0x001981E1
		public string Value { get; set; }

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x060047D5 RID: 18389 RVA: 0x00199FEA File Offset: 0x001981EA
		// (set) Token: 0x060047D6 RID: 18390 RVA: 0x00199FF2 File Offset: 0x001981F2
		public List<HeaderValue> Options { get; set; }

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x060047D7 RID: 18391 RVA: 0x00199FFB File Offset: 0x001981FB
		public bool HasValue
		{
			get
			{
				return !string.IsNullOrEmpty(this.Value);
			}
		}

		// Token: 0x060047D8 RID: 18392 RVA: 0x00023EF4 File Offset: 0x000220F4
		public HeaderValue()
		{
		}

		// Token: 0x060047D9 RID: 18393 RVA: 0x0019A00B File Offset: 0x0019820B
		public HeaderValue(string key)
		{
			this.Key = key;
		}

		// Token: 0x060047DA RID: 18394 RVA: 0x0019A01A File Offset: 0x0019821A
		public void Parse(string headerStr, ref int pos)
		{
			this.ParseImplementation(headerStr, ref pos, true);
		}

		// Token: 0x060047DB RID: 18395 RVA: 0x0019A028 File Offset: 0x00198228
		public bool TryGetOption(string key, out HeaderValue option)
		{
			option = null;
			if (this.Options == null || this.Options.Count == 0)
			{
				return false;
			}
			for (int i = 0; i < this.Options.Count; i++)
			{
				if (string.Equals(this.Options[i].Key, key, StringComparison.OrdinalIgnoreCase))
				{
					option = this.Options[i];
					return true;
				}
			}
			return false;
		}

		// Token: 0x060047DC RID: 18396 RVA: 0x0019A090 File Offset: 0x00198290
		private void ParseImplementation(string headerStr, ref int pos, bool isOptionIsAnOption)
		{
			string key = headerStr.Read(ref pos, (char ch) => ch != ';' && ch != '=' && ch != ',', true);
			this.Key = key;
			char? c = headerStr.Peek(pos - 1);
			char? c2 = c;
			int? num = (c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null;
			int num2 = 61;
			bool flag = num.GetValueOrDefault() == num2 & num != null;
			bool flag2;
			if (isOptionIsAnOption)
			{
				c2 = c;
				num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
				num2 = 59;
				flag2 = (num.GetValueOrDefault() == num2 & num != null);
			}
			else
			{
				flag2 = false;
			}
			bool flag3 = flag2;
			while ((c != null && flag) || flag3)
			{
				if (flag)
				{
					string value = headerStr.ReadPossibleQuotedText(ref pos);
					this.Value = value;
				}
				else if (flag3)
				{
					HeaderValue headerValue = new HeaderValue();
					headerValue.ParseImplementation(headerStr, ref pos, false);
					if (this.Options == null)
					{
						this.Options = new List<HeaderValue>();
					}
					this.Options.Add(headerValue);
				}
				if (!isOptionIsAnOption)
				{
					return;
				}
				c = headerStr.Peek(pos - 1);
				c2 = c;
				num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
				num2 = 61;
				flag = (num.GetValueOrDefault() == num2 & num != null);
				bool flag4;
				if (isOptionIsAnOption)
				{
					c2 = c;
					num = ((c2 != null) ? new int?((int)c2.GetValueOrDefault()) : null);
					num2 = 59;
					flag4 = (num.GetValueOrDefault() == num2 & num != null);
				}
				else
				{
					flag4 = false;
				}
				flag3 = flag4;
			}
		}

		// Token: 0x060047DD RID: 18397 RVA: 0x0019A249 File Offset: 0x00198449
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.Value))
			{
				return this.Key + '=' + this.Value;
			}
			return this.Key;
		}
	}
}
