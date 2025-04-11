using System;
using System.IO;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509
{
	// Token: 0x02000226 RID: 550
	internal class PemParser
	{
		// Token: 0x06001437 RID: 5175 RVA: 0x000AC68C File Offset: 0x000AA88C
		internal PemParser(string type)
		{
			this._header1 = "-----BEGIN " + type + "-----";
			this._header2 = "-----BEGIN X509 " + type + "-----";
			this._footer1 = "-----END " + type + "-----";
			this._footer2 = "-----END X509 " + type + "-----";
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x000AC6F8 File Offset: 0x000AA8F8
		private string ReadLine(Stream inStream)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num;
			for (;;)
			{
				if ((num = inStream.ReadByte()) == 13 || num == 10 || num < 0)
				{
					if (num < 0 || stringBuilder.Length != 0)
					{
						break;
					}
				}
				else if (num != 13)
				{
					stringBuilder.Append((char)num);
				}
			}
			if (num < 0)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x000AC748 File Offset: 0x000AA948
		internal Asn1Sequence ReadPemObject(Stream inStream)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text;
			while ((text = this.ReadLine(inStream)) != null)
			{
				if (Platform.StartsWith(text, this._header1) || Platform.StartsWith(text, this._header2))
				{
					IL_55:
					while ((text = this.ReadLine(inStream)) != null && !Platform.StartsWith(text, this._footer1) && !Platform.StartsWith(text, this._footer2))
					{
						stringBuilder.Append(text);
					}
					if (stringBuilder.Length == 0)
					{
						return null;
					}
					Asn1Object asn1Object = Asn1Object.FromByteArray(Base64.Decode(stringBuilder.ToString()));
					if (!(asn1Object is Asn1Sequence))
					{
						throw new IOException("malformed PEM data encountered");
					}
					return (Asn1Sequence)asn1Object;
				}
			}
			goto IL_55;
		}

		// Token: 0x040014DB RID: 5339
		private readonly string _header1;

		// Token: 0x040014DC RID: 5340
		private readonly string _header2;

		// Token: 0x040014DD RID: 5341
		private readonly string _footer1;

		// Token: 0x040014DE RID: 5342
		private readonly string _footer2;
	}
}
