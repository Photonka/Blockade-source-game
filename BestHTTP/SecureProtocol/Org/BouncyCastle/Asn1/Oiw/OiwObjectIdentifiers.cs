using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Oiw
{
	// Token: 0x020006F0 RID: 1776
	public abstract class OiwObjectIdentifiers
	{
		// Token: 0x04002958 RID: 10584
		public static readonly DerObjectIdentifier MD4WithRsa = new DerObjectIdentifier("1.3.14.3.2.2");

		// Token: 0x04002959 RID: 10585
		public static readonly DerObjectIdentifier MD5WithRsa = new DerObjectIdentifier("1.3.14.3.2.3");

		// Token: 0x0400295A RID: 10586
		public static readonly DerObjectIdentifier MD4WithRsaEncryption = new DerObjectIdentifier("1.3.14.3.2.4");

		// Token: 0x0400295B RID: 10587
		public static readonly DerObjectIdentifier DesEcb = new DerObjectIdentifier("1.3.14.3.2.6");

		// Token: 0x0400295C RID: 10588
		public static readonly DerObjectIdentifier DesCbc = new DerObjectIdentifier("1.3.14.3.2.7");

		// Token: 0x0400295D RID: 10589
		public static readonly DerObjectIdentifier DesOfb = new DerObjectIdentifier("1.3.14.3.2.8");

		// Token: 0x0400295E RID: 10590
		public static readonly DerObjectIdentifier DesCfb = new DerObjectIdentifier("1.3.14.3.2.9");

		// Token: 0x0400295F RID: 10591
		public static readonly DerObjectIdentifier DesEde = new DerObjectIdentifier("1.3.14.3.2.17");

		// Token: 0x04002960 RID: 10592
		public static readonly DerObjectIdentifier IdSha1 = new DerObjectIdentifier("1.3.14.3.2.26");

		// Token: 0x04002961 RID: 10593
		public static readonly DerObjectIdentifier DsaWithSha1 = new DerObjectIdentifier("1.3.14.3.2.27");

		// Token: 0x04002962 RID: 10594
		public static readonly DerObjectIdentifier Sha1WithRsa = new DerObjectIdentifier("1.3.14.3.2.29");

		// Token: 0x04002963 RID: 10595
		public static readonly DerObjectIdentifier ElGamalAlgorithm = new DerObjectIdentifier("1.3.14.7.2.1.1");
	}
}
