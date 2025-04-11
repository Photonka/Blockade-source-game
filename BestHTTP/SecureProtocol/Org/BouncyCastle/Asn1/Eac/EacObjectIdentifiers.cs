using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Eac
{
	// Token: 0x02000746 RID: 1862
	public abstract class EacObjectIdentifiers
	{
		// Token: 0x04002B27 RID: 11047
		public static readonly DerObjectIdentifier bsi_de = new DerObjectIdentifier("0.4.0.127.0.7");

		// Token: 0x04002B28 RID: 11048
		public static readonly DerObjectIdentifier id_PK = new DerObjectIdentifier(EacObjectIdentifiers.bsi_de + ".2.2.1");

		// Token: 0x04002B29 RID: 11049
		public static readonly DerObjectIdentifier id_PK_DH = new DerObjectIdentifier(EacObjectIdentifiers.id_PK + ".1");

		// Token: 0x04002B2A RID: 11050
		public static readonly DerObjectIdentifier id_PK_ECDH = new DerObjectIdentifier(EacObjectIdentifiers.id_PK + ".2");

		// Token: 0x04002B2B RID: 11051
		public static readonly DerObjectIdentifier id_CA = new DerObjectIdentifier(EacObjectIdentifiers.bsi_de + ".2.2.3");

		// Token: 0x04002B2C RID: 11052
		public static readonly DerObjectIdentifier id_CA_DH = new DerObjectIdentifier(EacObjectIdentifiers.id_CA + ".1");

		// Token: 0x04002B2D RID: 11053
		public static readonly DerObjectIdentifier id_CA_DH_3DES_CBC_CBC = new DerObjectIdentifier(EacObjectIdentifiers.id_CA_DH + ".1");

		// Token: 0x04002B2E RID: 11054
		public static readonly DerObjectIdentifier id_CA_ECDH = new DerObjectIdentifier(EacObjectIdentifiers.id_CA + ".2");

		// Token: 0x04002B2F RID: 11055
		public static readonly DerObjectIdentifier id_CA_ECDH_3DES_CBC_CBC = new DerObjectIdentifier(EacObjectIdentifiers.id_CA_ECDH + ".1");

		// Token: 0x04002B30 RID: 11056
		public static readonly DerObjectIdentifier id_TA = new DerObjectIdentifier(EacObjectIdentifiers.bsi_de + ".2.2.2");

		// Token: 0x04002B31 RID: 11057
		public static readonly DerObjectIdentifier id_TA_RSA = new DerObjectIdentifier(EacObjectIdentifiers.id_TA + ".1");

		// Token: 0x04002B32 RID: 11058
		public static readonly DerObjectIdentifier id_TA_RSA_v1_5_SHA_1 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_RSA + ".1");

		// Token: 0x04002B33 RID: 11059
		public static readonly DerObjectIdentifier id_TA_RSA_v1_5_SHA_256 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_RSA + ".2");

		// Token: 0x04002B34 RID: 11060
		public static readonly DerObjectIdentifier id_TA_RSA_PSS_SHA_1 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_RSA + ".3");

		// Token: 0x04002B35 RID: 11061
		public static readonly DerObjectIdentifier id_TA_RSA_PSS_SHA_256 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_RSA + ".4");

		// Token: 0x04002B36 RID: 11062
		public static readonly DerObjectIdentifier id_TA_ECDSA = new DerObjectIdentifier(EacObjectIdentifiers.id_TA + ".2");

		// Token: 0x04002B37 RID: 11063
		public static readonly DerObjectIdentifier id_TA_ECDSA_SHA_1 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_ECDSA + ".1");

		// Token: 0x04002B38 RID: 11064
		public static readonly DerObjectIdentifier id_TA_ECDSA_SHA_224 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_ECDSA + ".2");

		// Token: 0x04002B39 RID: 11065
		public static readonly DerObjectIdentifier id_TA_ECDSA_SHA_256 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_ECDSA + ".3");

		// Token: 0x04002B3A RID: 11066
		public static readonly DerObjectIdentifier id_TA_ECDSA_SHA_384 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_ECDSA + ".4");

		// Token: 0x04002B3B RID: 11067
		public static readonly DerObjectIdentifier id_TA_ECDSA_SHA_512 = new DerObjectIdentifier(EacObjectIdentifiers.id_TA_ECDSA + ".5");
	}
}
