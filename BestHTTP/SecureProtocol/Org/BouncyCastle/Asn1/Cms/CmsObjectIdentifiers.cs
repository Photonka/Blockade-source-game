using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200076C RID: 1900
	public abstract class CmsObjectIdentifiers
	{
		// Token: 0x04002BE0 RID: 11232
		public static readonly DerObjectIdentifier Data = PkcsObjectIdentifiers.Data;

		// Token: 0x04002BE1 RID: 11233
		public static readonly DerObjectIdentifier SignedData = PkcsObjectIdentifiers.SignedData;

		// Token: 0x04002BE2 RID: 11234
		public static readonly DerObjectIdentifier EnvelopedData = PkcsObjectIdentifiers.EnvelopedData;

		// Token: 0x04002BE3 RID: 11235
		public static readonly DerObjectIdentifier SignedAndEnvelopedData = PkcsObjectIdentifiers.SignedAndEnvelopedData;

		// Token: 0x04002BE4 RID: 11236
		public static readonly DerObjectIdentifier DigestedData = PkcsObjectIdentifiers.DigestedData;

		// Token: 0x04002BE5 RID: 11237
		public static readonly DerObjectIdentifier EncryptedData = PkcsObjectIdentifiers.EncryptedData;

		// Token: 0x04002BE6 RID: 11238
		public static readonly DerObjectIdentifier AuthenticatedData = PkcsObjectIdentifiers.IdCTAuthData;

		// Token: 0x04002BE7 RID: 11239
		public static readonly DerObjectIdentifier CompressedData = PkcsObjectIdentifiers.IdCTCompressedData;

		// Token: 0x04002BE8 RID: 11240
		public static readonly DerObjectIdentifier AuthEnvelopedData = PkcsObjectIdentifiers.IdCTAuthEnvelopedData;

		// Token: 0x04002BE9 RID: 11241
		public static readonly DerObjectIdentifier timestampedData = PkcsObjectIdentifiers.IdCTTimestampedData;

		// Token: 0x04002BEA RID: 11242
		public static readonly DerObjectIdentifier id_ri = new DerObjectIdentifier("1.3.6.1.5.5.7.16");

		// Token: 0x04002BEB RID: 11243
		public static readonly DerObjectIdentifier id_ri_ocsp_response = CmsObjectIdentifiers.id_ri.Branch("2");

		// Token: 0x04002BEC RID: 11244
		public static readonly DerObjectIdentifier id_ri_scvp = CmsObjectIdentifiers.id_ri.Branch("4");
	}
}
