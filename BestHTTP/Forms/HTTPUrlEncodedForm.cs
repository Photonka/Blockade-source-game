using System;
using System.Text;

namespace BestHTTP.Forms
{
	// Token: 0x020007CB RID: 1995
	public sealed class HTTPUrlEncodedForm : HTTPFormBase
	{
		// Token: 0x06004758 RID: 18264 RVA: 0x001988A8 File Offset: 0x00196AA8
		public override void PrepareRequest(HTTPRequest request)
		{
			request.SetHeader("Content-Type", "application/x-www-form-urlencoded");
		}

		// Token: 0x06004759 RID: 18265 RVA: 0x001988BC File Offset: 0x00196ABC
		public override byte[] GetData()
		{
			if (this.CachedData != null && !base.IsChanged)
			{
				return this.CachedData;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < base.Fields.Count; i++)
			{
				HTTPFieldData httpfieldData = base.Fields[i];
				if (i > 0)
				{
					stringBuilder.Append("&");
				}
				stringBuilder.Append(HTTPUrlEncodedForm.EscapeString(httpfieldData.Name));
				stringBuilder.Append("=");
				if (!string.IsNullOrEmpty(httpfieldData.Text) || httpfieldData.Binary == null)
				{
					stringBuilder.Append(HTTPUrlEncodedForm.EscapeString(httpfieldData.Text));
				}
				else
				{
					stringBuilder.Append(HTTPUrlEncodedForm.EscapeString(Encoding.UTF8.GetString(httpfieldData.Binary, 0, httpfieldData.Binary.Length)));
				}
			}
			base.IsChanged = false;
			return this.CachedData = Encoding.UTF8.GetBytes(stringBuilder.ToString());
		}

		// Token: 0x0600475A RID: 18266 RVA: 0x001989AC File Offset: 0x00196BAC
		public static string EscapeString(string originalString)
		{
			if (originalString.Length < 256)
			{
				return Uri.EscapeDataString(originalString);
			}
			int num = originalString.Length / 256;
			StringBuilder stringBuilder = new StringBuilder(num);
			for (int i = 0; i <= num; i++)
			{
				stringBuilder.Append((i < num) ? Uri.EscapeDataString(originalString.Substring(256 * i, 256)) : Uri.EscapeDataString(originalString.Substring(256 * i)));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04002DA3 RID: 11683
		private const int EscapeTreshold = 256;

		// Token: 0x04002DA4 RID: 11684
		private byte[] CachedData;
	}
}
