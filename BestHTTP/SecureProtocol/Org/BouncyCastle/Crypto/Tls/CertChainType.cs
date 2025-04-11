using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x020003EB RID: 1003
	public abstract class CertChainType
	{
		// Token: 0x06002904 RID: 10500 RVA: 0x0010FBFF File Offset: 0x0010DDFF
		public static bool IsValid(byte certChainType)
		{
			return certChainType >= 0 && certChainType <= 1;
		}

		// Token: 0x04001A29 RID: 6697
		public const byte individual_certs = 0;

		// Token: 0x04001A2A RID: 6698
		public const byte pkipath = 1;
	}
}
