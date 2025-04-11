using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000423 RID: 1059
	public abstract class NamedCurve
	{
		// Token: 0x06002A47 RID: 10823 RVA: 0x00114B4D File Offset: 0x00112D4D
		public static bool IsValid(int namedCurve)
		{
			return (namedCurve >= 1 && namedCurve <= 28) || (namedCurve >= 65281 && namedCurve <= 65282);
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x00114B6F File Offset: 0x00112D6F
		public static bool RefersToASpecificNamedCurve(int namedCurve)
		{
			return namedCurve - 65281 > 1;
		}

		// Token: 0x04001C41 RID: 7233
		public const int sect163k1 = 1;

		// Token: 0x04001C42 RID: 7234
		public const int sect163r1 = 2;

		// Token: 0x04001C43 RID: 7235
		public const int sect163r2 = 3;

		// Token: 0x04001C44 RID: 7236
		public const int sect193r1 = 4;

		// Token: 0x04001C45 RID: 7237
		public const int sect193r2 = 5;

		// Token: 0x04001C46 RID: 7238
		public const int sect233k1 = 6;

		// Token: 0x04001C47 RID: 7239
		public const int sect233r1 = 7;

		// Token: 0x04001C48 RID: 7240
		public const int sect239k1 = 8;

		// Token: 0x04001C49 RID: 7241
		public const int sect283k1 = 9;

		// Token: 0x04001C4A RID: 7242
		public const int sect283r1 = 10;

		// Token: 0x04001C4B RID: 7243
		public const int sect409k1 = 11;

		// Token: 0x04001C4C RID: 7244
		public const int sect409r1 = 12;

		// Token: 0x04001C4D RID: 7245
		public const int sect571k1 = 13;

		// Token: 0x04001C4E RID: 7246
		public const int sect571r1 = 14;

		// Token: 0x04001C4F RID: 7247
		public const int secp160k1 = 15;

		// Token: 0x04001C50 RID: 7248
		public const int secp160r1 = 16;

		// Token: 0x04001C51 RID: 7249
		public const int secp160r2 = 17;

		// Token: 0x04001C52 RID: 7250
		public const int secp192k1 = 18;

		// Token: 0x04001C53 RID: 7251
		public const int secp192r1 = 19;

		// Token: 0x04001C54 RID: 7252
		public const int secp224k1 = 20;

		// Token: 0x04001C55 RID: 7253
		public const int secp224r1 = 21;

		// Token: 0x04001C56 RID: 7254
		public const int secp256k1 = 22;

		// Token: 0x04001C57 RID: 7255
		public const int secp256r1 = 23;

		// Token: 0x04001C58 RID: 7256
		public const int secp384r1 = 24;

		// Token: 0x04001C59 RID: 7257
		public const int secp521r1 = 25;

		// Token: 0x04001C5A RID: 7258
		public const int brainpoolP256r1 = 26;

		// Token: 0x04001C5B RID: 7259
		public const int brainpoolP384r1 = 27;

		// Token: 0x04001C5C RID: 7260
		public const int brainpoolP512r1 = 28;

		// Token: 0x04001C5D RID: 7261
		public const int arbitrary_explicit_prime_curves = 65281;

		// Token: 0x04001C5E RID: 7262
		public const int arbitrary_explicit_char2_curves = 65282;
	}
}
