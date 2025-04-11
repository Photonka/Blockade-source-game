using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509.Qualified
{
	// Token: 0x020006B8 RID: 1720
	public abstract class EtsiQCObjectIdentifiers
	{
		// Token: 0x040027A6 RID: 10150
		public static readonly DerObjectIdentifier IdEtsiQcs = new DerObjectIdentifier("0.4.0.1862.1");

		// Token: 0x040027A7 RID: 10151
		public static readonly DerObjectIdentifier IdEtsiQcsQcCompliance = new DerObjectIdentifier(EtsiQCObjectIdentifiers.IdEtsiQcs + ".1");

		// Token: 0x040027A8 RID: 10152
		public static readonly DerObjectIdentifier IdEtsiQcsLimitValue = new DerObjectIdentifier(EtsiQCObjectIdentifiers.IdEtsiQcs + ".2");

		// Token: 0x040027A9 RID: 10153
		public static readonly DerObjectIdentifier IdEtsiQcsRetentionPeriod = new DerObjectIdentifier(EtsiQCObjectIdentifiers.IdEtsiQcs + ".3");

		// Token: 0x040027AA RID: 10154
		public static readonly DerObjectIdentifier IdEtsiQcsQcSscd = new DerObjectIdentifier(EtsiQCObjectIdentifiers.IdEtsiQcs + ".4");
	}
}
