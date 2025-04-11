using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Smime
{
	// Token: 0x020006CA RID: 1738
	public abstract class SmimeAttributes
	{
		// Token: 0x04002822 RID: 10274
		public static readonly DerObjectIdentifier SmimeCapabilities = PkcsObjectIdentifiers.Pkcs9AtSmimeCapabilities;

		// Token: 0x04002823 RID: 10275
		public static readonly DerObjectIdentifier EncrypKeyPref = PkcsObjectIdentifiers.IdAAEncrypKeyPref;
	}
}
