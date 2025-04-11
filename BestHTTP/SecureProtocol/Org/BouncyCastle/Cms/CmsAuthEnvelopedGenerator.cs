using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Nist;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005D2 RID: 1490
	internal class CmsAuthEnvelopedGenerator
	{
		// Token: 0x04002493 RID: 9363
		public static readonly string Aes128Ccm = NistObjectIdentifiers.IdAes128Ccm.Id;

		// Token: 0x04002494 RID: 9364
		public static readonly string Aes192Ccm = NistObjectIdentifiers.IdAes192Ccm.Id;

		// Token: 0x04002495 RID: 9365
		public static readonly string Aes256Ccm = NistObjectIdentifiers.IdAes256Ccm.Id;

		// Token: 0x04002496 RID: 9366
		public static readonly string Aes128Gcm = NistObjectIdentifiers.IdAes128Gcm.Id;

		// Token: 0x04002497 RID: 9367
		public static readonly string Aes192Gcm = NistObjectIdentifiers.IdAes192Gcm.Id;

		// Token: 0x04002498 RID: 9368
		public static readonly string Aes256Gcm = NistObjectIdentifiers.IdAes256Gcm.Id;
	}
}
