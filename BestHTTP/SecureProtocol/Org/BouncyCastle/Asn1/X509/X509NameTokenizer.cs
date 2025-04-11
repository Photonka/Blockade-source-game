using System;
using System.Text;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020006B2 RID: 1714
	public class X509NameTokenizer
	{
		// Token: 0x06003FA6 RID: 16294 RVA: 0x0017EB51 File Offset: 0x0017CD51
		public X509NameTokenizer(string oid) : this(oid, ',')
		{
		}

		// Token: 0x06003FA7 RID: 16295 RVA: 0x0017EB5C File Offset: 0x0017CD5C
		public X509NameTokenizer(string oid, char separator)
		{
			this.value = oid;
			this.index = -1;
			this.separator = separator;
		}

		// Token: 0x06003FA8 RID: 16296 RVA: 0x0017EB84 File Offset: 0x0017CD84
		public bool HasMoreTokens()
		{
			return this.index != this.value.Length;
		}

		// Token: 0x06003FA9 RID: 16297 RVA: 0x0017EB9C File Offset: 0x0017CD9C
		public string NextToken()
		{
			if (this.index == this.value.Length)
			{
				return null;
			}
			int num = this.index + 1;
			bool flag = false;
			bool flag2 = false;
			this.buffer.Remove(0, this.buffer.Length);
			while (num != this.value.Length)
			{
				char c = this.value[num];
				if (c == '"')
				{
					if (!flag2)
					{
						flag = !flag;
					}
					else
					{
						this.buffer.Append(c);
						flag2 = false;
					}
				}
				else if (flag2 || flag)
				{
					if (c == '#' && this.buffer[this.buffer.Length - 1] == '=')
					{
						this.buffer.Append('\\');
					}
					else if (c == '+' && this.separator != '+')
					{
						this.buffer.Append('\\');
					}
					this.buffer.Append(c);
					flag2 = false;
				}
				else if (c == '\\')
				{
					flag2 = true;
				}
				else
				{
					if (c == this.separator)
					{
						break;
					}
					this.buffer.Append(c);
				}
				num++;
			}
			this.index = num;
			return this.buffer.ToString().Trim();
		}

		// Token: 0x04002779 RID: 10105
		private string value;

		// Token: 0x0400277A RID: 10106
		private int index;

		// Token: 0x0400277B RID: 10107
		private char separator;

		// Token: 0x0400277C RID: 10108
		private StringBuilder buffer = new StringBuilder();
	}
}
