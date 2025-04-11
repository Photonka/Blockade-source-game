using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Iana
{
	// Token: 0x0200071F RID: 1823
	public abstract class IanaObjectIdentifiers
	{
		// Token: 0x04002A6C RID: 10860
		public static readonly DerObjectIdentifier IsakmpOakley = new DerObjectIdentifier("1.3.6.1.5.5.8.1");

		// Token: 0x04002A6D RID: 10861
		public static readonly DerObjectIdentifier HmacMD5 = new DerObjectIdentifier(IanaObjectIdentifiers.IsakmpOakley + ".1");

		// Token: 0x04002A6E RID: 10862
		public static readonly DerObjectIdentifier HmacSha1 = new DerObjectIdentifier(IanaObjectIdentifiers.IsakmpOakley + ".2");

		// Token: 0x04002A6F RID: 10863
		public static readonly DerObjectIdentifier HmacTiger = new DerObjectIdentifier(IanaObjectIdentifiers.IsakmpOakley + ".3");

		// Token: 0x04002A70 RID: 10864
		public static readonly DerObjectIdentifier HmacRipeMD160 = new DerObjectIdentifier(IanaObjectIdentifiers.IsakmpOakley + ".4");
	}
}
