using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000648 RID: 1608
	public class DerSequenceParser : Asn1SequenceParser, IAsn1Convertible
	{
		// Token: 0x06003C80 RID: 15488 RVA: 0x0017444E File Offset: 0x0017264E
		internal DerSequenceParser(Asn1StreamParser parser)
		{
			this._parser = parser;
		}

		// Token: 0x06003C81 RID: 15489 RVA: 0x0017445D File Offset: 0x0017265D
		public IAsn1Convertible ReadObject()
		{
			return this._parser.ReadObject();
		}

		// Token: 0x06003C82 RID: 15490 RVA: 0x0017446A File Offset: 0x0017266A
		public Asn1Object ToAsn1Object()
		{
			return new DerSequence(this._parser.ReadVector());
		}

		// Token: 0x040025C0 RID: 9664
		private readonly Asn1StreamParser _parser;
	}
}
