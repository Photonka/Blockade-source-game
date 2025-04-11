using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Microsoft
{
	// Token: 0x0200070C RID: 1804
	public abstract class MicrosoftObjectIdentifiers
	{
		// Token: 0x04002A0A RID: 10762
		public static readonly DerObjectIdentifier Microsoft = new DerObjectIdentifier("1.3.6.1.4.1.311");

		// Token: 0x04002A0B RID: 10763
		public static readonly DerObjectIdentifier MicrosoftCertTemplateV1 = MicrosoftObjectIdentifiers.Microsoft.Branch("20.2");

		// Token: 0x04002A0C RID: 10764
		public static readonly DerObjectIdentifier MicrosoftCAVersion = MicrosoftObjectIdentifiers.Microsoft.Branch("21.1");

		// Token: 0x04002A0D RID: 10765
		public static readonly DerObjectIdentifier MicrosoftPrevCACertHash = MicrosoftObjectIdentifiers.Microsoft.Branch("21.2");

		// Token: 0x04002A0E RID: 10766
		public static readonly DerObjectIdentifier MicrosoftCrlNextPublish = MicrosoftObjectIdentifiers.Microsoft.Branch("21.4");

		// Token: 0x04002A0F RID: 10767
		public static readonly DerObjectIdentifier MicrosoftCertTemplateV2 = MicrosoftObjectIdentifiers.Microsoft.Branch("21.7");

		// Token: 0x04002A10 RID: 10768
		public static readonly DerObjectIdentifier MicrosoftAppPolicies = MicrosoftObjectIdentifiers.Microsoft.Branch("21.10");
	}
}
