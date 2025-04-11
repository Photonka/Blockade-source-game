using System;
using System.Collections;
using System.IO;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO.Pem
{
	// Token: 0x02000270 RID: 624
	public class PemReader
	{
		// Token: 0x0600173F RID: 5951 RVA: 0x000B9B34 File Offset: 0x000B7D34
		public PemReader(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.reader = reader;
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06001740 RID: 5952 RVA: 0x000B9B51 File Offset: 0x000B7D51
		public TextReader Reader
		{
			get
			{
				return this.reader;
			}
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x000B9B5C File Offset: 0x000B7D5C
		public PemObject ReadPemObject()
		{
			string text = this.reader.ReadLine();
			if (text != null && Platform.StartsWith(text, "-----BEGIN "))
			{
				text = text.Substring("-----BEGIN ".Length);
				int num = text.IndexOf('-');
				string type = text.Substring(0, num);
				if (num > 0)
				{
					return this.LoadObject(type);
				}
			}
			return null;
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x000B9BB8 File Offset: 0x000B7DB8
		private PemObject LoadObject(string type)
		{
			string text = "-----END " + type;
			IList list = Platform.CreateArrayList();
			StringBuilder stringBuilder = new StringBuilder();
			string text2;
			while ((text2 = this.reader.ReadLine()) != null && Platform.IndexOf(text2, text) == -1)
			{
				int num = text2.IndexOf(':');
				if (num == -1)
				{
					stringBuilder.Append(text2.Trim());
				}
				else
				{
					string text3 = text2.Substring(0, num).Trim();
					if (Platform.StartsWith(text3, "X-"))
					{
						text3 = text3.Substring(2);
					}
					string val = text2.Substring(num + 1).Trim();
					list.Add(new PemHeader(text3, val));
				}
			}
			if (text2 == null)
			{
				throw new IOException(text + " not found");
			}
			if (stringBuilder.Length % 4 != 0)
			{
				throw new IOException("base64 data appears to be truncated");
			}
			return new PemObject(type, list, Base64.Decode(stringBuilder.ToString()));
		}

		// Token: 0x040016D7 RID: 5847
		private const string BeginString = "-----BEGIN ";

		// Token: 0x040016D8 RID: 5848
		private const string EndString = "-----END ";

		// Token: 0x040016D9 RID: 5849
		private readonly TextReader reader;
	}
}
