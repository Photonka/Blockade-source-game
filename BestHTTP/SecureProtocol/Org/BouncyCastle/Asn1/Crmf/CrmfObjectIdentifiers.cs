using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x02000756 RID: 1878
	public abstract class CrmfObjectIdentifiers
	{
		// Token: 0x04002B8C RID: 11148
		public static readonly DerObjectIdentifier id_pkix = new DerObjectIdentifier("1.3.6.1.5.5.7");

		// Token: 0x04002B8D RID: 11149
		public static readonly DerObjectIdentifier id_pkip = CrmfObjectIdentifiers.id_pkix.Branch("5");

		// Token: 0x04002B8E RID: 11150
		public static readonly DerObjectIdentifier id_regCtrl = CrmfObjectIdentifiers.id_pkip.Branch("1");

		// Token: 0x04002B8F RID: 11151
		public static readonly DerObjectIdentifier id_regCtrl_regToken = CrmfObjectIdentifiers.id_regCtrl.Branch("1");

		// Token: 0x04002B90 RID: 11152
		public static readonly DerObjectIdentifier id_regCtrl_authenticator = CrmfObjectIdentifiers.id_regCtrl.Branch("2");

		// Token: 0x04002B91 RID: 11153
		public static readonly DerObjectIdentifier id_regCtrl_pkiPublicationInfo = CrmfObjectIdentifiers.id_regCtrl.Branch("3");

		// Token: 0x04002B92 RID: 11154
		public static readonly DerObjectIdentifier id_regCtrl_pkiArchiveOptions = CrmfObjectIdentifiers.id_regCtrl.Branch("4");

		// Token: 0x04002B93 RID: 11155
		public static readonly DerObjectIdentifier id_ct_encKeyWithID = new DerObjectIdentifier("1.2.840.113549.1.9.16.1.21");
	}
}
