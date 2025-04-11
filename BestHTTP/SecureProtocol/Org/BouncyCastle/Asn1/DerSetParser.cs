using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200064B RID: 1611
	public class DerSetParser : Asn1SetParser, IAsn1Convertible
	{
		// Token: 0x06003C91 RID: 15505 RVA: 0x00174656 File Offset: 0x00172856
		internal DerSetParser(Asn1StreamParser parser)
		{
			this._parser = parser;
		}

		// Token: 0x06003C92 RID: 15506 RVA: 0x00174665 File Offset: 0x00172865
		public IAsn1Convertible ReadObject()
		{
			return this._parser.ReadObject();
		}

		// Token: 0x06003C93 RID: 15507 RVA: 0x00174672 File Offset: 0x00172872
		public Asn1Object ToAsn1Object()
		{
			return new DerSet(this._parser.ReadVector(), false);
		}

		// Token: 0x040025C3 RID: 9667
		private readonly Asn1StreamParser _parser;
	}
}
