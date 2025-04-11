using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000694 RID: 1684
	public sealed class PolicyQualifierID : DerObjectIdentifier
	{
		// Token: 0x06003E92 RID: 16018 RVA: 0x00178095 File Offset: 0x00176295
		private PolicyQualifierID(string id) : base(id)
		{
		}

		// Token: 0x040026C3 RID: 9923
		private const string IdQt = "1.3.6.1.5.5.7.2";

		// Token: 0x040026C4 RID: 9924
		public static readonly PolicyQualifierID IdQtCps = new PolicyQualifierID("1.3.6.1.5.5.7.2.1");

		// Token: 0x040026C5 RID: 9925
		public static readonly PolicyQualifierID IdQtUnotice = new PolicyQualifierID("1.3.6.1.5.5.7.2.2");
	}
}
