using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020006B3 RID: 1715
	public abstract class X509ObjectIdentifiers
	{
		// Token: 0x0400277D RID: 10109
		internal const string ID = "2.5.4";

		// Token: 0x0400277E RID: 10110
		public static readonly DerObjectIdentifier CommonName = new DerObjectIdentifier("2.5.4.3");

		// Token: 0x0400277F RID: 10111
		public static readonly DerObjectIdentifier CountryName = new DerObjectIdentifier("2.5.4.6");

		// Token: 0x04002780 RID: 10112
		public static readonly DerObjectIdentifier LocalityName = new DerObjectIdentifier("2.5.4.7");

		// Token: 0x04002781 RID: 10113
		public static readonly DerObjectIdentifier StateOrProvinceName = new DerObjectIdentifier("2.5.4.8");

		// Token: 0x04002782 RID: 10114
		public static readonly DerObjectIdentifier Organization = new DerObjectIdentifier("2.5.4.10");

		// Token: 0x04002783 RID: 10115
		public static readonly DerObjectIdentifier OrganizationalUnitName = new DerObjectIdentifier("2.5.4.11");

		// Token: 0x04002784 RID: 10116
		public static readonly DerObjectIdentifier id_at_telephoneNumber = new DerObjectIdentifier("2.5.4.20");

		// Token: 0x04002785 RID: 10117
		public static readonly DerObjectIdentifier id_at_name = new DerObjectIdentifier("2.5.4.41");

		// Token: 0x04002786 RID: 10118
		public static readonly DerObjectIdentifier id_at_organizationIdentifier = new DerObjectIdentifier("2.5.4.97");

		// Token: 0x04002787 RID: 10119
		public static readonly DerObjectIdentifier IdSha1 = new DerObjectIdentifier("1.3.14.3.2.26");

		// Token: 0x04002788 RID: 10120
		public static readonly DerObjectIdentifier RipeMD160 = new DerObjectIdentifier("1.3.36.3.2.1");

		// Token: 0x04002789 RID: 10121
		public static readonly DerObjectIdentifier RipeMD160WithRsaEncryption = new DerObjectIdentifier("1.3.36.3.3.1.2");

		// Token: 0x0400278A RID: 10122
		public static readonly DerObjectIdentifier IdEARsa = new DerObjectIdentifier("2.5.8.1.1");

		// Token: 0x0400278B RID: 10123
		public static readonly DerObjectIdentifier IdPkix = new DerObjectIdentifier("1.3.6.1.5.5.7");

		// Token: 0x0400278C RID: 10124
		public static readonly DerObjectIdentifier IdPE = new DerObjectIdentifier(X509ObjectIdentifiers.IdPkix + ".1");

		// Token: 0x0400278D RID: 10125
		public static readonly DerObjectIdentifier IdAD = new DerObjectIdentifier(X509ObjectIdentifiers.IdPkix + ".48");

		// Token: 0x0400278E RID: 10126
		public static readonly DerObjectIdentifier IdADCAIssuers = new DerObjectIdentifier(X509ObjectIdentifiers.IdAD + ".2");

		// Token: 0x0400278F RID: 10127
		public static readonly DerObjectIdentifier IdADOcsp = new DerObjectIdentifier(X509ObjectIdentifiers.IdAD + ".1");

		// Token: 0x04002790 RID: 10128
		public static readonly DerObjectIdentifier OcspAccessMethod = X509ObjectIdentifiers.IdADOcsp;

		// Token: 0x04002791 RID: 10129
		public static readonly DerObjectIdentifier CrlAccessMethod = X509ObjectIdentifiers.IdADCAIssuers;
	}
}
