using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Rosstandart
{
	// Token: 0x020006D3 RID: 1747
	public abstract class RosstandartObjectIdentifiers
	{
		// Token: 0x0400285F RID: 10335
		public static readonly DerObjectIdentifier rosstandart = new DerObjectIdentifier("1.2.643.7");

		// Token: 0x04002860 RID: 10336
		public static readonly DerObjectIdentifier id_tc26 = RosstandartObjectIdentifiers.rosstandart.Branch("1");

		// Token: 0x04002861 RID: 10337
		public static readonly DerObjectIdentifier id_tc26_gost_3411_12_256 = RosstandartObjectIdentifiers.id_tc26.Branch("1.2.2");

		// Token: 0x04002862 RID: 10338
		public static readonly DerObjectIdentifier id_tc26_gost_3411_12_512 = RosstandartObjectIdentifiers.id_tc26.Branch("1.2.3");

		// Token: 0x04002863 RID: 10339
		public static readonly DerObjectIdentifier id_tc26_hmac_gost_3411_12_256 = RosstandartObjectIdentifiers.id_tc26.Branch("1.4.1");

		// Token: 0x04002864 RID: 10340
		public static readonly DerObjectIdentifier id_tc26_hmac_gost_3411_12_512 = RosstandartObjectIdentifiers.id_tc26.Branch("1.4.2");

		// Token: 0x04002865 RID: 10341
		public static readonly DerObjectIdentifier id_tc26_gost_3410_12_256 = RosstandartObjectIdentifiers.id_tc26.Branch("1.1.1");

		// Token: 0x04002866 RID: 10342
		public static readonly DerObjectIdentifier id_tc26_gost_3410_12_512 = RosstandartObjectIdentifiers.id_tc26.Branch("1.1.2");

		// Token: 0x04002867 RID: 10343
		public static readonly DerObjectIdentifier id_tc26_signwithdigest_gost_3410_12_256 = RosstandartObjectIdentifiers.id_tc26.Branch("1.3.2");

		// Token: 0x04002868 RID: 10344
		public static readonly DerObjectIdentifier id_tc26_signwithdigest_gost_3410_12_512 = RosstandartObjectIdentifiers.id_tc26.Branch("1.3.3");

		// Token: 0x04002869 RID: 10345
		public static readonly DerObjectIdentifier id_tc26_agreement = RosstandartObjectIdentifiers.id_tc26.Branch("1.6");

		// Token: 0x0400286A RID: 10346
		public static readonly DerObjectIdentifier id_tc26_agreement_gost_3410_12_256 = RosstandartObjectIdentifiers.id_tc26_agreement.Branch("1");

		// Token: 0x0400286B RID: 10347
		public static readonly DerObjectIdentifier id_tc26_agreement_gost_3410_12_512 = RosstandartObjectIdentifiers.id_tc26_agreement.Branch("2");

		// Token: 0x0400286C RID: 10348
		public static readonly DerObjectIdentifier id_tc26_gost_3410_12_256_paramSet = RosstandartObjectIdentifiers.id_tc26.Branch("2.1.1");

		// Token: 0x0400286D RID: 10349
		public static readonly DerObjectIdentifier id_tc26_gost_3410_12_256_paramSetA = RosstandartObjectIdentifiers.id_tc26_gost_3410_12_256_paramSet.Branch("1");

		// Token: 0x0400286E RID: 10350
		public static readonly DerObjectIdentifier id_tc26_gost_3410_12_512_paramSet = RosstandartObjectIdentifiers.id_tc26.Branch("2.1.2");

		// Token: 0x0400286F RID: 10351
		public static readonly DerObjectIdentifier id_tc26_gost_3410_12_512_paramSetA = RosstandartObjectIdentifiers.id_tc26_gost_3410_12_512_paramSet.Branch("1");

		// Token: 0x04002870 RID: 10352
		public static readonly DerObjectIdentifier id_tc26_gost_3410_12_512_paramSetB = RosstandartObjectIdentifiers.id_tc26_gost_3410_12_512_paramSet.Branch("2");

		// Token: 0x04002871 RID: 10353
		public static readonly DerObjectIdentifier id_tc26_gost_3410_12_512_paramSetC = RosstandartObjectIdentifiers.id_tc26_gost_3410_12_512_paramSet.Branch("3");

		// Token: 0x04002872 RID: 10354
		public static readonly DerObjectIdentifier id_tc26_gost_28147_param_Z = RosstandartObjectIdentifiers.id_tc26.Branch("2.5.1.1");
	}
}
