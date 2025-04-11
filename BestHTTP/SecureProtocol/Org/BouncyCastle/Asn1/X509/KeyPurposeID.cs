using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200068D RID: 1677
	public sealed class KeyPurposeID : DerObjectIdentifier
	{
		// Token: 0x06003E67 RID: 15975 RVA: 0x00178095 File Offset: 0x00176295
		private KeyPurposeID(string id) : base(id)
		{
		}

		// Token: 0x0400269F RID: 9887
		private const string IdKP = "1.3.6.1.5.5.7.3";

		// Token: 0x040026A0 RID: 9888
		public static readonly KeyPurposeID AnyExtendedKeyUsage = new KeyPurposeID(X509Extensions.ExtendedKeyUsage.Id + ".0");

		// Token: 0x040026A1 RID: 9889
		public static readonly KeyPurposeID IdKPServerAuth = new KeyPurposeID("1.3.6.1.5.5.7.3.1");

		// Token: 0x040026A2 RID: 9890
		public static readonly KeyPurposeID IdKPClientAuth = new KeyPurposeID("1.3.6.1.5.5.7.3.2");

		// Token: 0x040026A3 RID: 9891
		public static readonly KeyPurposeID IdKPCodeSigning = new KeyPurposeID("1.3.6.1.5.5.7.3.3");

		// Token: 0x040026A4 RID: 9892
		public static readonly KeyPurposeID IdKPEmailProtection = new KeyPurposeID("1.3.6.1.5.5.7.3.4");

		// Token: 0x040026A5 RID: 9893
		public static readonly KeyPurposeID IdKPIpsecEndSystem = new KeyPurposeID("1.3.6.1.5.5.7.3.5");

		// Token: 0x040026A6 RID: 9894
		public static readonly KeyPurposeID IdKPIpsecTunnel = new KeyPurposeID("1.3.6.1.5.5.7.3.6");

		// Token: 0x040026A7 RID: 9895
		public static readonly KeyPurposeID IdKPIpsecUser = new KeyPurposeID("1.3.6.1.5.5.7.3.7");

		// Token: 0x040026A8 RID: 9896
		public static readonly KeyPurposeID IdKPTimeStamping = new KeyPurposeID("1.3.6.1.5.5.7.3.8");

		// Token: 0x040026A9 RID: 9897
		public static readonly KeyPurposeID IdKPOcspSigning = new KeyPurposeID("1.3.6.1.5.5.7.3.9");

		// Token: 0x040026AA RID: 9898
		public static readonly KeyPurposeID IdKPSmartCardLogon = new KeyPurposeID("1.3.6.1.4.1.311.20.2.2");

		// Token: 0x040026AB RID: 9899
		public static readonly KeyPurposeID IdKPMacAddress = new KeyPurposeID("1.3.6.1.1.1.1.22");
	}
}
