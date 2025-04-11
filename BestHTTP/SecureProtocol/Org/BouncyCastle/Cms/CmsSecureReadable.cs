using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005E5 RID: 1509
	internal interface CmsSecureReadable
	{
		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x0600399B RID: 14747
		AlgorithmIdentifier Algorithm { get; }

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x0600399C RID: 14748
		object CryptoObject { get; }

		// Token: 0x0600399D RID: 14749
		CmsReadable GetReadable(KeyParameter key);
	}
}
