using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x0200079D RID: 1949
	public abstract class CmpObjectIdentifiers
	{
		// Token: 0x04002C7B RID: 11387
		public static readonly DerObjectIdentifier passwordBasedMac = new DerObjectIdentifier("1.2.840.113533.7.66.13");

		// Token: 0x04002C7C RID: 11388
		public static readonly DerObjectIdentifier dhBasedMac = new DerObjectIdentifier("1.2.840.113533.7.66.30");

		// Token: 0x04002C7D RID: 11389
		public static readonly DerObjectIdentifier it_caProtEncCert = new DerObjectIdentifier("1.3.6.1.5.5.7.4.1");

		// Token: 0x04002C7E RID: 11390
		public static readonly DerObjectIdentifier it_signKeyPairTypes = new DerObjectIdentifier("1.3.6.1.5.5.7.4.2");

		// Token: 0x04002C7F RID: 11391
		public static readonly DerObjectIdentifier it_encKeyPairTypes = new DerObjectIdentifier("1.3.6.1.5.5.7.4.3");

		// Token: 0x04002C80 RID: 11392
		public static readonly DerObjectIdentifier it_preferredSymAlg = new DerObjectIdentifier("1.3.6.1.5.5.7.4.4");

		// Token: 0x04002C81 RID: 11393
		public static readonly DerObjectIdentifier it_caKeyUpdateInfo = new DerObjectIdentifier("1.3.6.1.5.5.7.4.5");

		// Token: 0x04002C82 RID: 11394
		public static readonly DerObjectIdentifier it_currentCRL = new DerObjectIdentifier("1.3.6.1.5.5.7.4.6");

		// Token: 0x04002C83 RID: 11395
		public static readonly DerObjectIdentifier it_unsupportedOIDs = new DerObjectIdentifier("1.3.6.1.5.5.7.4.7");

		// Token: 0x04002C84 RID: 11396
		public static readonly DerObjectIdentifier it_keyPairParamReq = new DerObjectIdentifier("1.3.6.1.5.5.7.4.10");

		// Token: 0x04002C85 RID: 11397
		public static readonly DerObjectIdentifier it_keyPairParamRep = new DerObjectIdentifier("1.3.6.1.5.5.7.4.11");

		// Token: 0x04002C86 RID: 11398
		public static readonly DerObjectIdentifier it_revPassphrase = new DerObjectIdentifier("1.3.6.1.5.5.7.4.12");

		// Token: 0x04002C87 RID: 11399
		public static readonly DerObjectIdentifier it_implicitConfirm = new DerObjectIdentifier("1.3.6.1.5.5.7.4.13");

		// Token: 0x04002C88 RID: 11400
		public static readonly DerObjectIdentifier it_confirmWaitTime = new DerObjectIdentifier("1.3.6.1.5.5.7.4.14");

		// Token: 0x04002C89 RID: 11401
		public static readonly DerObjectIdentifier it_origPKIMessage = new DerObjectIdentifier("1.3.6.1.5.5.7.4.15");

		// Token: 0x04002C8A RID: 11402
		public static readonly DerObjectIdentifier it_suppLangTags = new DerObjectIdentifier("1.3.6.1.5.5.7.4.16");

		// Token: 0x04002C8B RID: 11403
		public static readonly DerObjectIdentifier regCtrl_regToken = new DerObjectIdentifier("1.3.6.1.5.5.7.5.1.1");

		// Token: 0x04002C8C RID: 11404
		public static readonly DerObjectIdentifier regCtrl_authenticator = new DerObjectIdentifier("1.3.6.1.5.5.7.5.1.2");

		// Token: 0x04002C8D RID: 11405
		public static readonly DerObjectIdentifier regCtrl_pkiPublicationInfo = new DerObjectIdentifier("1.3.6.1.5.5.7.5.1.3");

		// Token: 0x04002C8E RID: 11406
		public static readonly DerObjectIdentifier regCtrl_pkiArchiveOptions = new DerObjectIdentifier("1.3.6.1.5.5.7.5.1.4");

		// Token: 0x04002C8F RID: 11407
		public static readonly DerObjectIdentifier regCtrl_oldCertID = new DerObjectIdentifier("1.3.6.1.5.5.7.5.1.5");

		// Token: 0x04002C90 RID: 11408
		public static readonly DerObjectIdentifier regCtrl_protocolEncrKey = new DerObjectIdentifier("1.3.6.1.5.5.7.5.1.6");

		// Token: 0x04002C91 RID: 11409
		public static readonly DerObjectIdentifier regCtrl_altCertTemplate = new DerObjectIdentifier("1.3.6.1.5.5.7.5.1.7");

		// Token: 0x04002C92 RID: 11410
		public static readonly DerObjectIdentifier regInfo_utf8Pairs = new DerObjectIdentifier("1.3.6.1.5.5.7.5.2.1");

		// Token: 0x04002C93 RID: 11411
		public static readonly DerObjectIdentifier regInfo_certReq = new DerObjectIdentifier("1.3.6.1.5.5.7.5.2.2");

		// Token: 0x04002C94 RID: 11412
		public static readonly DerObjectIdentifier ct_encKeyWithID = new DerObjectIdentifier("1.2.840.113549.1.9.16.1.21");
	}
}
