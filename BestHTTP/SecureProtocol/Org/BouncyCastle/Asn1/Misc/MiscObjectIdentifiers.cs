using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Misc
{
	// Token: 0x02000708 RID: 1800
	public abstract class MiscObjectIdentifiers
	{
		// Token: 0x040029DD RID: 10717
		public static readonly DerObjectIdentifier Netscape = new DerObjectIdentifier("2.16.840.1.113730.1");

		// Token: 0x040029DE RID: 10718
		public static readonly DerObjectIdentifier NetscapeCertType = MiscObjectIdentifiers.Netscape.Branch("1");

		// Token: 0x040029DF RID: 10719
		public static readonly DerObjectIdentifier NetscapeBaseUrl = MiscObjectIdentifiers.Netscape.Branch("2");

		// Token: 0x040029E0 RID: 10720
		public static readonly DerObjectIdentifier NetscapeRevocationUrl = MiscObjectIdentifiers.Netscape.Branch("3");

		// Token: 0x040029E1 RID: 10721
		public static readonly DerObjectIdentifier NetscapeCARevocationUrl = MiscObjectIdentifiers.Netscape.Branch("4");

		// Token: 0x040029E2 RID: 10722
		public static readonly DerObjectIdentifier NetscapeRenewalUrl = MiscObjectIdentifiers.Netscape.Branch("7");

		// Token: 0x040029E3 RID: 10723
		public static readonly DerObjectIdentifier NetscapeCAPolicyUrl = MiscObjectIdentifiers.Netscape.Branch("8");

		// Token: 0x040029E4 RID: 10724
		public static readonly DerObjectIdentifier NetscapeSslServerName = MiscObjectIdentifiers.Netscape.Branch("12");

		// Token: 0x040029E5 RID: 10725
		public static readonly DerObjectIdentifier NetscapeCertComment = MiscObjectIdentifiers.Netscape.Branch("13");

		// Token: 0x040029E6 RID: 10726
		public static readonly DerObjectIdentifier Verisign = new DerObjectIdentifier("2.16.840.1.113733.1");

		// Token: 0x040029E7 RID: 10727
		public static readonly DerObjectIdentifier VerisignCzagExtension = MiscObjectIdentifiers.Verisign.Branch("6.3");

		// Token: 0x040029E8 RID: 10728
		public static readonly DerObjectIdentifier VerisignPrivate_6_9 = MiscObjectIdentifiers.Verisign.Branch("6.9");

		// Token: 0x040029E9 RID: 10729
		public static readonly DerObjectIdentifier VerisignOnSiteJurisdictionHash = MiscObjectIdentifiers.Verisign.Branch("6.11");

		// Token: 0x040029EA RID: 10730
		public static readonly DerObjectIdentifier VerisignBitString_6_13 = MiscObjectIdentifiers.Verisign.Branch("6.13");

		// Token: 0x040029EB RID: 10731
		public static readonly DerObjectIdentifier VerisignDnbDunsNumber = MiscObjectIdentifiers.Verisign.Branch("6.15");

		// Token: 0x040029EC RID: 10732
		public static readonly DerObjectIdentifier VerisignIssStrongCrypto = MiscObjectIdentifiers.Verisign.Branch("8.1");

		// Token: 0x040029ED RID: 10733
		public static readonly string Novell = "2.16.840.1.113719";

		// Token: 0x040029EE RID: 10734
		public static readonly DerObjectIdentifier NovellSecurityAttribs = new DerObjectIdentifier(MiscObjectIdentifiers.Novell + ".1.9.4.1");

		// Token: 0x040029EF RID: 10735
		public static readonly string Entrust = "1.2.840.113533.7";

		// Token: 0x040029F0 RID: 10736
		public static readonly DerObjectIdentifier EntrustVersionExtension = new DerObjectIdentifier(MiscObjectIdentifiers.Entrust + ".65.0");

		// Token: 0x040029F1 RID: 10737
		public static readonly DerObjectIdentifier as_sys_sec_alg_ideaCBC = new DerObjectIdentifier("1.3.6.1.4.1.188.7.1.1.2");

		// Token: 0x040029F2 RID: 10738
		public static readonly DerObjectIdentifier cryptlib = new DerObjectIdentifier("1.3.6.1.4.1.3029");

		// Token: 0x040029F3 RID: 10739
		public static readonly DerObjectIdentifier cryptlib_algorithm = MiscObjectIdentifiers.cryptlib.Branch("1");

		// Token: 0x040029F4 RID: 10740
		public static readonly DerObjectIdentifier cryptlib_algorithm_blowfish_ECB = MiscObjectIdentifiers.cryptlib_algorithm.Branch("1.1");

		// Token: 0x040029F5 RID: 10741
		public static readonly DerObjectIdentifier cryptlib_algorithm_blowfish_CBC = MiscObjectIdentifiers.cryptlib_algorithm.Branch("1.2");

		// Token: 0x040029F6 RID: 10742
		public static readonly DerObjectIdentifier cryptlib_algorithm_blowfish_CFB = MiscObjectIdentifiers.cryptlib_algorithm.Branch("1.3");

		// Token: 0x040029F7 RID: 10743
		public static readonly DerObjectIdentifier cryptlib_algorithm_blowfish_OFB = MiscObjectIdentifiers.cryptlib_algorithm.Branch("1.4");

		// Token: 0x040029F8 RID: 10744
		public static readonly DerObjectIdentifier blake2 = new DerObjectIdentifier("1.3.6.1.4.1.1722.12.2");

		// Token: 0x040029F9 RID: 10745
		public static readonly DerObjectIdentifier id_blake2b160 = MiscObjectIdentifiers.blake2.Branch("1.5");

		// Token: 0x040029FA RID: 10746
		public static readonly DerObjectIdentifier id_blake2b256 = MiscObjectIdentifiers.blake2.Branch("1.8");

		// Token: 0x040029FB RID: 10747
		public static readonly DerObjectIdentifier id_blake2b384 = MiscObjectIdentifiers.blake2.Branch("1.12");

		// Token: 0x040029FC RID: 10748
		public static readonly DerObjectIdentifier id_blake2b512 = MiscObjectIdentifiers.blake2.Branch("1.16");

		// Token: 0x040029FD RID: 10749
		public static readonly DerObjectIdentifier id_blake2s128 = MiscObjectIdentifiers.blake2.Branch("2.4");

		// Token: 0x040029FE RID: 10750
		public static readonly DerObjectIdentifier id_blake2s160 = MiscObjectIdentifiers.blake2.Branch("2.5");

		// Token: 0x040029FF RID: 10751
		public static readonly DerObjectIdentifier id_blake2s224 = MiscObjectIdentifiers.blake2.Branch("2.7");

		// Token: 0x04002A00 RID: 10752
		public static readonly DerObjectIdentifier id_blake2s256 = MiscObjectIdentifiers.blake2.Branch("2.8");

		// Token: 0x04002A01 RID: 10753
		public static readonly DerObjectIdentifier id_scrypt = new DerObjectIdentifier("1.3.6.1.4.1.11591.4.11");
	}
}
