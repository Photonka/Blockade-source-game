using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200061D RID: 1565
	public interface Asn1TaggedObjectParser : IAsn1Convertible
	{
		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06003B48 RID: 15176
		int TagNo { get; }

		// Token: 0x06003B49 RID: 15177
		IAsn1Convertible GetObjectParser(int tag, bool isExplicit);
	}
}
