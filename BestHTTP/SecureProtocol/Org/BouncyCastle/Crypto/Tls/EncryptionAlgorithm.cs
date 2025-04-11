using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000416 RID: 1046
	public abstract class EncryptionAlgorithm
	{
		// Token: 0x04001BBA RID: 7098
		public const int NULL = 0;

		// Token: 0x04001BBB RID: 7099
		public const int RC4_40 = 1;

		// Token: 0x04001BBC RID: 7100
		public const int RC4_128 = 2;

		// Token: 0x04001BBD RID: 7101
		public const int RC2_CBC_40 = 3;

		// Token: 0x04001BBE RID: 7102
		public const int IDEA_CBC = 4;

		// Token: 0x04001BBF RID: 7103
		public const int DES40_CBC = 5;

		// Token: 0x04001BC0 RID: 7104
		public const int DES_CBC = 6;

		// Token: 0x04001BC1 RID: 7105
		public const int cls_3DES_EDE_CBC = 7;

		// Token: 0x04001BC2 RID: 7106
		public const int AES_128_CBC = 8;

		// Token: 0x04001BC3 RID: 7107
		public const int AES_256_CBC = 9;

		// Token: 0x04001BC4 RID: 7108
		public const int AES_128_GCM = 10;

		// Token: 0x04001BC5 RID: 7109
		public const int AES_256_GCM = 11;

		// Token: 0x04001BC6 RID: 7110
		public const int CAMELLIA_128_CBC = 12;

		// Token: 0x04001BC7 RID: 7111
		public const int CAMELLIA_256_CBC = 13;

		// Token: 0x04001BC8 RID: 7112
		public const int SEED_CBC = 14;

		// Token: 0x04001BC9 RID: 7113
		public const int AES_128_CCM = 15;

		// Token: 0x04001BCA RID: 7114
		public const int AES_128_CCM_8 = 16;

		// Token: 0x04001BCB RID: 7115
		public const int AES_256_CCM = 17;

		// Token: 0x04001BCC RID: 7116
		public const int AES_256_CCM_8 = 18;

		// Token: 0x04001BCD RID: 7117
		public const int CAMELLIA_128_GCM = 19;

		// Token: 0x04001BCE RID: 7118
		public const int CAMELLIA_256_GCM = 20;

		// Token: 0x04001BCF RID: 7119
		public const int CHACHA20_POLY1305 = 21;

		// Token: 0x04001BD0 RID: 7120
		public const int AES_128_OCB_TAGLEN96 = 103;

		// Token: 0x04001BD1 RID: 7121
		public const int AES_256_OCB_TAGLEN96 = 104;
	}
}
