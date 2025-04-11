using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200062D RID: 1581
	public class BerSetParser : Asn1SetParser, IAsn1Convertible
	{
		// Token: 0x06003B8C RID: 15244 RVA: 0x00171778 File Offset: 0x0016F978
		internal BerSetParser(Asn1StreamParser parser)
		{
			this._parser = parser;
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x00171787 File Offset: 0x0016F987
		public IAsn1Convertible ReadObject()
		{
			return this._parser.ReadObject();
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x00171794 File Offset: 0x0016F994
		public Asn1Object ToAsn1Object()
		{
			return new BerSet(this._parser.ReadVector(), false);
		}

		// Token: 0x04002590 RID: 9616
		private readonly Asn1StreamParser _parser;
	}
}
