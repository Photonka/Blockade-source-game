using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000424 RID: 1060
	public abstract class NameType
	{
		// Token: 0x06002A4A RID: 10826 RVA: 0x00114B7E File Offset: 0x00112D7E
		public static bool IsValid(byte nameType)
		{
			return nameType == 0;
		}

		// Token: 0x04001C5F RID: 7263
		public const byte host_name = 0;
	}
}
