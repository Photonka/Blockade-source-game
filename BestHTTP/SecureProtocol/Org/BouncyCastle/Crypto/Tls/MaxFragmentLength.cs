using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000422 RID: 1058
	public abstract class MaxFragmentLength
	{
		// Token: 0x06002A45 RID: 10821 RVA: 0x00114B3E File Offset: 0x00112D3E
		public static bool IsValid(byte maxFragmentLength)
		{
			return maxFragmentLength >= 1 && maxFragmentLength <= 4;
		}

		// Token: 0x04001C3D RID: 7229
		public const byte pow2_9 = 1;

		// Token: 0x04001C3E RID: 7230
		public const byte pow2_10 = 2;

		// Token: 0x04001C3F RID: 7231
		public const byte pow2_11 = 3;

		// Token: 0x04001C40 RID: 7232
		public const byte pow2_12 = 4;
	}
}
