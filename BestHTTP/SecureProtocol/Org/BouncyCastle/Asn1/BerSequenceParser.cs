using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200062A RID: 1578
	public class BerSequenceParser : Asn1SequenceParser, IAsn1Convertible
	{
		// Token: 0x06003B7F RID: 15231 RVA: 0x00171634 File Offset: 0x0016F834
		internal BerSequenceParser(Asn1StreamParser parser)
		{
			this._parser = parser;
		}

		// Token: 0x06003B80 RID: 15232 RVA: 0x00171643 File Offset: 0x0016F843
		public IAsn1Convertible ReadObject()
		{
			return this._parser.ReadObject();
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x00171650 File Offset: 0x0016F850
		public Asn1Object ToAsn1Object()
		{
			return new BerSequence(this._parser.ReadVector());
		}

		// Token: 0x0400258E RID: 9614
		private readonly Asn1StreamParser _parser;
	}
}
