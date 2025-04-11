using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020006B1 RID: 1713
	public abstract class X509NameEntryConverter
	{
		// Token: 0x06003FA2 RID: 16290 RVA: 0x0017EB36 File Offset: 0x0017CD36
		protected Asn1Object ConvertHexEncoded(string hexString, int offset)
		{
			return Asn1Object.FromByteArray(Hex.Decode(hexString.Substring(offset)));
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x0017EB49 File Offset: 0x0017CD49
		protected bool CanBePrintable(string str)
		{
			return DerPrintableString.IsPrintableString(str);
		}

		// Token: 0x06003FA4 RID: 16292
		public abstract Asn1Object GetConvertedValue(DerObjectIdentifier oid, string value);
	}
}
