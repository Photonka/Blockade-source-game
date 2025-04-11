using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x020006F5 RID: 1781
	public abstract class OcspObjectIdentifiers
	{
		// Token: 0x04002971 RID: 10609
		internal const string PkixOcspId = "1.3.6.1.5.5.7.48.1";

		// Token: 0x04002972 RID: 10610
		public static readonly DerObjectIdentifier PkixOcsp = new DerObjectIdentifier("1.3.6.1.5.5.7.48.1");

		// Token: 0x04002973 RID: 10611
		public static readonly DerObjectIdentifier PkixOcspBasic = new DerObjectIdentifier("1.3.6.1.5.5.7.48.1.1");

		// Token: 0x04002974 RID: 10612
		public static readonly DerObjectIdentifier PkixOcspNonce = new DerObjectIdentifier(OcspObjectIdentifiers.PkixOcsp + ".2");

		// Token: 0x04002975 RID: 10613
		public static readonly DerObjectIdentifier PkixOcspCrl = new DerObjectIdentifier(OcspObjectIdentifiers.PkixOcsp + ".3");

		// Token: 0x04002976 RID: 10614
		public static readonly DerObjectIdentifier PkixOcspResponse = new DerObjectIdentifier(OcspObjectIdentifiers.PkixOcsp + ".4");

		// Token: 0x04002977 RID: 10615
		public static readonly DerObjectIdentifier PkixOcspNocheck = new DerObjectIdentifier(OcspObjectIdentifiers.PkixOcsp + ".5");

		// Token: 0x04002978 RID: 10616
		public static readonly DerObjectIdentifier PkixOcspArchiveCutoff = new DerObjectIdentifier(OcspObjectIdentifiers.PkixOcsp + ".6");

		// Token: 0x04002979 RID: 10617
		public static readonly DerObjectIdentifier PkixOcspServiceLocator = new DerObjectIdentifier(OcspObjectIdentifiers.PkixOcsp + ".7");
	}
}
