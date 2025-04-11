using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.EdEC
{
	// Token: 0x02000745 RID: 1861
	public abstract class EdECObjectIdentifiers
	{
		// Token: 0x04002B22 RID: 11042
		public static readonly DerObjectIdentifier id_edwards_curve_algs = new DerObjectIdentifier("1.3.101");

		// Token: 0x04002B23 RID: 11043
		public static readonly DerObjectIdentifier id_X25519 = EdECObjectIdentifiers.id_edwards_curve_algs.Branch("110");

		// Token: 0x04002B24 RID: 11044
		public static readonly DerObjectIdentifier id_X448 = EdECObjectIdentifiers.id_edwards_curve_algs.Branch("111");

		// Token: 0x04002B25 RID: 11045
		public static readonly DerObjectIdentifier id_Ed25519 = EdECObjectIdentifiers.id_edwards_curve_algs.Branch("112");

		// Token: 0x04002B26 RID: 11046
		public static readonly DerObjectIdentifier id_Ed448 = EdECObjectIdentifiers.id_edwards_curve_algs.Branch("113");
	}
}
