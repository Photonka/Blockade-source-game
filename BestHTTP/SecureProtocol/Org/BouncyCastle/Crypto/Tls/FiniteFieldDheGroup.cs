using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000419 RID: 1049
	public abstract class FiniteFieldDheGroup
	{
		// Token: 0x06002A30 RID: 10800 RVA: 0x001148DC File Offset: 0x00112ADC
		public static bool IsValid(byte group)
		{
			return group >= 0 && group <= 4;
		}

		// Token: 0x04001BF9 RID: 7161
		public const byte ffdhe2432 = 0;

		// Token: 0x04001BFA RID: 7162
		public const byte ffdhe3072 = 1;

		// Token: 0x04001BFB RID: 7163
		public const byte ffdhe4096 = 2;

		// Token: 0x04001BFC RID: 7164
		public const byte ffdhe6144 = 3;

		// Token: 0x04001BFD RID: 7165
		public const byte ffdhe8192 = 4;
	}
}
