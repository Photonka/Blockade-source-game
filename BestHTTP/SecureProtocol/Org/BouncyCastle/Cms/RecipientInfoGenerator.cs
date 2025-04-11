using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x02000602 RID: 1538
	internal interface RecipientInfoGenerator
	{
		// Token: 0x06003A89 RID: 14985
		RecipientInfo Generate(KeyParameter contentEncryptionKey, SecureRandom random);
	}
}
