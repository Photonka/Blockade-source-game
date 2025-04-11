using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000638 RID: 1592
	public class DerExternalParser : Asn1Encodable
	{
		// Token: 0x06003BF2 RID: 15346 RVA: 0x00172C52 File Offset: 0x00170E52
		public DerExternalParser(Asn1StreamParser parser)
		{
			this._parser = parser;
		}

		// Token: 0x06003BF3 RID: 15347 RVA: 0x00172C61 File Offset: 0x00170E61
		public IAsn1Convertible ReadObject()
		{
			return this._parser.ReadObject();
		}

		// Token: 0x06003BF4 RID: 15348 RVA: 0x00172C6E File Offset: 0x00170E6E
		public override Asn1Object ToAsn1Object()
		{
			return new DerExternal(this._parser.ReadVector());
		}

		// Token: 0x040025AB RID: 9643
		private readonly Asn1StreamParser _parser;
	}
}
