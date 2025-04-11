using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Bsi
{
	// Token: 0x020007BB RID: 1979
	public abstract class BsiObjectIdentifiers
	{
		// Token: 0x04002D2A RID: 11562
		public static readonly DerObjectIdentifier bsi_de = new DerObjectIdentifier("0.4.0.127.0.7");

		// Token: 0x04002D2B RID: 11563
		public static readonly DerObjectIdentifier id_ecc = BsiObjectIdentifiers.bsi_de.Branch("1.1");

		// Token: 0x04002D2C RID: 11564
		public static readonly DerObjectIdentifier ecdsa_plain_signatures = BsiObjectIdentifiers.id_ecc.Branch("4.1");

		// Token: 0x04002D2D RID: 11565
		public static readonly DerObjectIdentifier ecdsa_plain_SHA1 = BsiObjectIdentifiers.ecdsa_plain_signatures.Branch("1");

		// Token: 0x04002D2E RID: 11566
		public static readonly DerObjectIdentifier ecdsa_plain_SHA224 = BsiObjectIdentifiers.ecdsa_plain_signatures.Branch("2");

		// Token: 0x04002D2F RID: 11567
		public static readonly DerObjectIdentifier ecdsa_plain_SHA256 = BsiObjectIdentifiers.ecdsa_plain_signatures.Branch("3");

		// Token: 0x04002D30 RID: 11568
		public static readonly DerObjectIdentifier ecdsa_plain_SHA384 = BsiObjectIdentifiers.ecdsa_plain_signatures.Branch("4");

		// Token: 0x04002D31 RID: 11569
		public static readonly DerObjectIdentifier ecdsa_plain_SHA512 = BsiObjectIdentifiers.ecdsa_plain_signatures.Branch("5");

		// Token: 0x04002D32 RID: 11570
		public static readonly DerObjectIdentifier ecdsa_plain_RIPEMD160 = BsiObjectIdentifiers.ecdsa_plain_signatures.Branch("6");

		// Token: 0x04002D33 RID: 11571
		public static readonly DerObjectIdentifier algorithm = BsiObjectIdentifiers.bsi_de.Branch("1");

		// Token: 0x04002D34 RID: 11572
		public static readonly DerObjectIdentifier ecka_eg = BsiObjectIdentifiers.id_ecc.Branch("5.1");

		// Token: 0x04002D35 RID: 11573
		public static readonly DerObjectIdentifier ecka_eg_X963kdf = BsiObjectIdentifiers.ecka_eg.Branch("1");

		// Token: 0x04002D36 RID: 11574
		public static readonly DerObjectIdentifier ecka_eg_X963kdf_SHA1 = BsiObjectIdentifiers.ecka_eg_X963kdf.Branch("1");

		// Token: 0x04002D37 RID: 11575
		public static readonly DerObjectIdentifier ecka_eg_X963kdf_SHA224 = BsiObjectIdentifiers.ecka_eg_X963kdf.Branch("2");

		// Token: 0x04002D38 RID: 11576
		public static readonly DerObjectIdentifier ecka_eg_X963kdf_SHA256 = BsiObjectIdentifiers.ecka_eg_X963kdf.Branch("3");

		// Token: 0x04002D39 RID: 11577
		public static readonly DerObjectIdentifier ecka_eg_X963kdf_SHA384 = BsiObjectIdentifiers.ecka_eg_X963kdf.Branch("4");

		// Token: 0x04002D3A RID: 11578
		public static readonly DerObjectIdentifier ecka_eg_X963kdf_SHA512 = BsiObjectIdentifiers.ecka_eg_X963kdf.Branch("5");

		// Token: 0x04002D3B RID: 11579
		public static readonly DerObjectIdentifier ecka_eg_X963kdf_RIPEMD160 = BsiObjectIdentifiers.ecka_eg_X963kdf.Branch("6");

		// Token: 0x04002D3C RID: 11580
		public static readonly DerObjectIdentifier ecka_eg_SessionKDF = BsiObjectIdentifiers.ecka_eg.Branch("2");

		// Token: 0x04002D3D RID: 11581
		public static readonly DerObjectIdentifier ecka_eg_SessionKDF_3DES = BsiObjectIdentifiers.ecka_eg_SessionKDF.Branch("1");

		// Token: 0x04002D3E RID: 11582
		public static readonly DerObjectIdentifier ecka_eg_SessionKDF_AES128 = BsiObjectIdentifiers.ecka_eg_SessionKDF.Branch("2");

		// Token: 0x04002D3F RID: 11583
		public static readonly DerObjectIdentifier ecka_eg_SessionKDF_AES192 = BsiObjectIdentifiers.ecka_eg_SessionKDF.Branch("3");

		// Token: 0x04002D40 RID: 11584
		public static readonly DerObjectIdentifier ecka_eg_SessionKDF_AES256 = BsiObjectIdentifiers.ecka_eg_SessionKDF.Branch("4");
	}
}
