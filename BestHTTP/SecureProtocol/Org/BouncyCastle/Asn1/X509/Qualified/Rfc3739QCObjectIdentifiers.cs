using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509.Qualified
{
	// Token: 0x020006BC RID: 1724
	public sealed class Rfc3739QCObjectIdentifiers
	{
		// Token: 0x06003FE1 RID: 16353 RVA: 0x00023EF4 File Offset: 0x000220F4
		private Rfc3739QCObjectIdentifiers()
		{
		}

		// Token: 0x040027B4 RID: 10164
		public static readonly DerObjectIdentifier IdQcs = new DerObjectIdentifier("1.3.6.1.5.5.7.11");

		// Token: 0x040027B5 RID: 10165
		public static readonly DerObjectIdentifier IdQcsPkixQCSyntaxV1 = new DerObjectIdentifier(Rfc3739QCObjectIdentifiers.IdQcs + ".1");

		// Token: 0x040027B6 RID: 10166
		public static readonly DerObjectIdentifier IdQcsPkixQCSyntaxV2 = new DerObjectIdentifier(Rfc3739QCObjectIdentifiers.IdQcs + ".2");
	}
}
