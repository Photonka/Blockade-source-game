using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.BC
{
	// Token: 0x020007BC RID: 1980
	public abstract class BCObjectIdentifiers
	{
		// Token: 0x04002D41 RID: 11585
		public static readonly DerObjectIdentifier bc = new DerObjectIdentifier("1.3.6.1.4.1.22554");

		// Token: 0x04002D42 RID: 11586
		public static readonly DerObjectIdentifier bc_pbe = BCObjectIdentifiers.bc.Branch("1");

		// Token: 0x04002D43 RID: 11587
		public static readonly DerObjectIdentifier bc_pbe_sha1 = BCObjectIdentifiers.bc_pbe.Branch("1");

		// Token: 0x04002D44 RID: 11588
		public static readonly DerObjectIdentifier bc_pbe_sha256 = BCObjectIdentifiers.bc_pbe.Branch("2.1");

		// Token: 0x04002D45 RID: 11589
		public static readonly DerObjectIdentifier bc_pbe_sha384 = BCObjectIdentifiers.bc_pbe.Branch("2.2");

		// Token: 0x04002D46 RID: 11590
		public static readonly DerObjectIdentifier bc_pbe_sha512 = BCObjectIdentifiers.bc_pbe.Branch("2.3");

		// Token: 0x04002D47 RID: 11591
		public static readonly DerObjectIdentifier bc_pbe_sha224 = BCObjectIdentifiers.bc_pbe.Branch("2.4");

		// Token: 0x04002D48 RID: 11592
		public static readonly DerObjectIdentifier bc_pbe_sha1_pkcs5 = BCObjectIdentifiers.bc_pbe_sha1.Branch("1");

		// Token: 0x04002D49 RID: 11593
		public static readonly DerObjectIdentifier bc_pbe_sha1_pkcs12 = BCObjectIdentifiers.bc_pbe_sha1.Branch("2");

		// Token: 0x04002D4A RID: 11594
		public static readonly DerObjectIdentifier bc_pbe_sha256_pkcs5 = BCObjectIdentifiers.bc_pbe_sha256.Branch("1");

		// Token: 0x04002D4B RID: 11595
		public static readonly DerObjectIdentifier bc_pbe_sha256_pkcs12 = BCObjectIdentifiers.bc_pbe_sha256.Branch("2");

		// Token: 0x04002D4C RID: 11596
		public static readonly DerObjectIdentifier bc_pbe_sha1_pkcs12_aes128_cbc = BCObjectIdentifiers.bc_pbe_sha1_pkcs12.Branch("1.2");

		// Token: 0x04002D4D RID: 11597
		public static readonly DerObjectIdentifier bc_pbe_sha1_pkcs12_aes192_cbc = BCObjectIdentifiers.bc_pbe_sha1_pkcs12.Branch("1.22");

		// Token: 0x04002D4E RID: 11598
		public static readonly DerObjectIdentifier bc_pbe_sha1_pkcs12_aes256_cbc = BCObjectIdentifiers.bc_pbe_sha1_pkcs12.Branch("1.42");

		// Token: 0x04002D4F RID: 11599
		public static readonly DerObjectIdentifier bc_pbe_sha256_pkcs12_aes128_cbc = BCObjectIdentifiers.bc_pbe_sha256_pkcs12.Branch("1.2");

		// Token: 0x04002D50 RID: 11600
		public static readonly DerObjectIdentifier bc_pbe_sha256_pkcs12_aes192_cbc = BCObjectIdentifiers.bc_pbe_sha256_pkcs12.Branch("1.22");

		// Token: 0x04002D51 RID: 11601
		public static readonly DerObjectIdentifier bc_pbe_sha256_pkcs12_aes256_cbc = BCObjectIdentifiers.bc_pbe_sha256_pkcs12.Branch("1.42");

		// Token: 0x04002D52 RID: 11602
		public static readonly DerObjectIdentifier bc_sig = BCObjectIdentifiers.bc.Branch("2");

		// Token: 0x04002D53 RID: 11603
		public static readonly DerObjectIdentifier sphincs256 = BCObjectIdentifiers.bc_sig.Branch("1");

		// Token: 0x04002D54 RID: 11604
		public static readonly DerObjectIdentifier sphincs256_with_BLAKE512 = BCObjectIdentifiers.sphincs256.Branch("1");

		// Token: 0x04002D55 RID: 11605
		public static readonly DerObjectIdentifier sphincs256_with_SHA512 = BCObjectIdentifiers.sphincs256.Branch("2");

		// Token: 0x04002D56 RID: 11606
		public static readonly DerObjectIdentifier sphincs256_with_SHA3_512 = BCObjectIdentifiers.sphincs256.Branch("3");

		// Token: 0x04002D57 RID: 11607
		public static readonly DerObjectIdentifier xmss = BCObjectIdentifiers.bc_sig.Branch("2");

		// Token: 0x04002D58 RID: 11608
		public static readonly DerObjectIdentifier xmss_with_SHA256 = BCObjectIdentifiers.xmss.Branch("1");

		// Token: 0x04002D59 RID: 11609
		public static readonly DerObjectIdentifier xmss_with_SHA512 = BCObjectIdentifiers.xmss.Branch("2");

		// Token: 0x04002D5A RID: 11610
		public static readonly DerObjectIdentifier xmss_with_SHAKE128 = BCObjectIdentifiers.xmss.Branch("3");

		// Token: 0x04002D5B RID: 11611
		public static readonly DerObjectIdentifier xmss_with_SHAKE256 = BCObjectIdentifiers.xmss.Branch("4");

		// Token: 0x04002D5C RID: 11612
		public static readonly DerObjectIdentifier xmss_mt = BCObjectIdentifiers.bc_sig.Branch("3");

		// Token: 0x04002D5D RID: 11613
		public static readonly DerObjectIdentifier xmss_mt_with_SHA256 = BCObjectIdentifiers.xmss_mt.Branch("1");

		// Token: 0x04002D5E RID: 11614
		public static readonly DerObjectIdentifier xmss_mt_with_SHA512 = BCObjectIdentifiers.xmss_mt.Branch("2");

		// Token: 0x04002D5F RID: 11615
		public static readonly DerObjectIdentifier xmss_mt_with_SHAKE128 = BCObjectIdentifiers.xmss_mt.Branch("3");

		// Token: 0x04002D60 RID: 11616
		public static readonly DerObjectIdentifier xmss_mt_with_SHAKE256 = BCObjectIdentifiers.xmss_mt.Branch("4");

		// Token: 0x04002D61 RID: 11617
		public static readonly DerObjectIdentifier bc_exch = BCObjectIdentifiers.bc.Branch("3");

		// Token: 0x04002D62 RID: 11618
		public static readonly DerObjectIdentifier newHope = BCObjectIdentifiers.bc_exch.Branch("1");
	}
}
