using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000620 RID: 1568
	public class BerApplicationSpecificParser : IAsn1ApplicationSpecificParser, IAsn1Convertible
	{
		// Token: 0x06003B4C RID: 15180 RVA: 0x0017100F File Offset: 0x0016F20F
		internal BerApplicationSpecificParser(int tag, Asn1StreamParser parser)
		{
			this.tag = tag;
			this.parser = parser;
		}

		// Token: 0x06003B4D RID: 15181 RVA: 0x00171025 File Offset: 0x0016F225
		public IAsn1Convertible ReadObject()
		{
			return this.parser.ReadObject();
		}

		// Token: 0x06003B4E RID: 15182 RVA: 0x00171032 File Offset: 0x0016F232
		public Asn1Object ToAsn1Object()
		{
			return new BerApplicationSpecific(this.tag, this.parser.ReadVector());
		}

		// Token: 0x04002584 RID: 9604
		private readonly int tag;

		// Token: 0x04002585 RID: 9605
		private readonly Asn1StreamParser parser;
	}
}
