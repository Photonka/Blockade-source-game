using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.TeleTrust
{
	// Token: 0x020006C9 RID: 1737
	public sealed class TeleTrusTObjectIdentifiers
	{
		// Token: 0x06004047 RID: 16455 RVA: 0x00023EF4 File Offset: 0x000220F4
		private TeleTrusTObjectIdentifiers()
		{
		}

		// Token: 0x04002806 RID: 10246
		public static readonly DerObjectIdentifier TeleTrusTAlgorithm = new DerObjectIdentifier("1.3.36.3");

		// Token: 0x04002807 RID: 10247
		public static readonly DerObjectIdentifier RipeMD160 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTAlgorithm + ".2.1");

		// Token: 0x04002808 RID: 10248
		public static readonly DerObjectIdentifier RipeMD128 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTAlgorithm + ".2.2");

		// Token: 0x04002809 RID: 10249
		public static readonly DerObjectIdentifier RipeMD256 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTAlgorithm + ".2.3");

		// Token: 0x0400280A RID: 10250
		public static readonly DerObjectIdentifier TeleTrusTRsaSignatureAlgorithm = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTAlgorithm + ".3.1");

		// Token: 0x0400280B RID: 10251
		public static readonly DerObjectIdentifier RsaSignatureWithRipeMD160 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTRsaSignatureAlgorithm + ".2");

		// Token: 0x0400280C RID: 10252
		public static readonly DerObjectIdentifier RsaSignatureWithRipeMD128 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTRsaSignatureAlgorithm + ".3");

		// Token: 0x0400280D RID: 10253
		public static readonly DerObjectIdentifier RsaSignatureWithRipeMD256 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTRsaSignatureAlgorithm + ".4");

		// Token: 0x0400280E RID: 10254
		public static readonly DerObjectIdentifier ECSign = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTAlgorithm + ".3.2");

		// Token: 0x0400280F RID: 10255
		public static readonly DerObjectIdentifier ECSignWithSha1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.ECSign + ".1");

		// Token: 0x04002810 RID: 10256
		public static readonly DerObjectIdentifier ECSignWithRipeMD160 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.ECSign + ".2");

		// Token: 0x04002811 RID: 10257
		public static readonly DerObjectIdentifier EccBrainpool = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.TeleTrusTAlgorithm + ".3.2.8");

		// Token: 0x04002812 RID: 10258
		public static readonly DerObjectIdentifier EllipticCurve = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.EccBrainpool + ".1");

		// Token: 0x04002813 RID: 10259
		public static readonly DerObjectIdentifier VersionOne = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.EllipticCurve + ".1");

		// Token: 0x04002814 RID: 10260
		public static readonly DerObjectIdentifier BrainpoolP160R1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".1");

		// Token: 0x04002815 RID: 10261
		public static readonly DerObjectIdentifier BrainpoolP160T1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".2");

		// Token: 0x04002816 RID: 10262
		public static readonly DerObjectIdentifier BrainpoolP192R1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".3");

		// Token: 0x04002817 RID: 10263
		public static readonly DerObjectIdentifier BrainpoolP192T1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".4");

		// Token: 0x04002818 RID: 10264
		public static readonly DerObjectIdentifier BrainpoolP224R1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".5");

		// Token: 0x04002819 RID: 10265
		public static readonly DerObjectIdentifier BrainpoolP224T1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".6");

		// Token: 0x0400281A RID: 10266
		public static readonly DerObjectIdentifier BrainpoolP256R1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".7");

		// Token: 0x0400281B RID: 10267
		public static readonly DerObjectIdentifier BrainpoolP256T1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".8");

		// Token: 0x0400281C RID: 10268
		public static readonly DerObjectIdentifier BrainpoolP320R1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".9");

		// Token: 0x0400281D RID: 10269
		public static readonly DerObjectIdentifier BrainpoolP320T1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".10");

		// Token: 0x0400281E RID: 10270
		public static readonly DerObjectIdentifier BrainpoolP384R1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".11");

		// Token: 0x0400281F RID: 10271
		public static readonly DerObjectIdentifier BrainpoolP384T1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".12");

		// Token: 0x04002820 RID: 10272
		public static readonly DerObjectIdentifier BrainpoolP512R1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".13");

		// Token: 0x04002821 RID: 10273
		public static readonly DerObjectIdentifier BrainpoolP512T1 = new DerObjectIdentifier(TeleTrusTObjectIdentifiers.VersionOne + ".14");
	}
}
