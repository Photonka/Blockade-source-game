using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200041B RID: 1051
	public abstract class HashAlgorithm
	{
		// Token: 0x06002A33 RID: 10803 RVA: 0x001148EC File Offset: 0x00112AEC
		public static string GetName(byte hashAlgorithm)
		{
			switch (hashAlgorithm)
			{
			case 0:
				return "none";
			case 1:
				return "md5";
			case 2:
				return "sha1";
			case 3:
				return "sha224";
			case 4:
				return "sha256";
			case 5:
				return "sha384";
			case 6:
				return "sha512";
			default:
				return "UNKNOWN";
			}
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x0011494C File Offset: 0x00112B4C
		public static string GetText(byte hashAlgorithm)
		{
			return string.Concat(new object[]
			{
				HashAlgorithm.GetName(hashAlgorithm),
				"(",
				hashAlgorithm,
				")"
			});
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x0011497B File Offset: 0x00112B7B
		public static bool IsPrivate(byte hashAlgorithm)
		{
			return 224 <= hashAlgorithm && hashAlgorithm <= byte.MaxValue;
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x00114992 File Offset: 0x00112B92
		public static bool IsRecognized(byte hashAlgorithm)
		{
			return hashAlgorithm - 1 <= 5;
		}

		// Token: 0x04001C0D RID: 7181
		public const byte none = 0;

		// Token: 0x04001C0E RID: 7182
		public const byte md5 = 1;

		// Token: 0x04001C0F RID: 7183
		public const byte sha1 = 2;

		// Token: 0x04001C10 RID: 7184
		public const byte sha224 = 3;

		// Token: 0x04001C11 RID: 7185
		public const byte sha256 = 4;

		// Token: 0x04001C12 RID: 7186
		public const byte sha384 = 5;

		// Token: 0x04001C13 RID: 7187
		public const byte sha512 = 6;
	}
}
