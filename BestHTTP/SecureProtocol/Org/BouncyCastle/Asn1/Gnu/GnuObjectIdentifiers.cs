using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Gnu
{
	// Token: 0x02000720 RID: 1824
	public abstract class GnuObjectIdentifiers
	{
		// Token: 0x04002A71 RID: 10865
		public static readonly DerObjectIdentifier Gnu = new DerObjectIdentifier("1.3.6.1.4.1.11591.1");

		// Token: 0x04002A72 RID: 10866
		public static readonly DerObjectIdentifier GnuPG = new DerObjectIdentifier("1.3.6.1.4.1.11591.2");

		// Token: 0x04002A73 RID: 10867
		public static readonly DerObjectIdentifier Notation = new DerObjectIdentifier("1.3.6.1.4.1.11591.2.1");

		// Token: 0x04002A74 RID: 10868
		public static readonly DerObjectIdentifier PkaAddress = new DerObjectIdentifier("1.3.6.1.4.1.11591.2.1.1");

		// Token: 0x04002A75 RID: 10869
		public static readonly DerObjectIdentifier GnuRadar = new DerObjectIdentifier("1.3.6.1.4.1.11591.3");

		// Token: 0x04002A76 RID: 10870
		public static readonly DerObjectIdentifier DigestAlgorithm = new DerObjectIdentifier("1.3.6.1.4.1.11591.12");

		// Token: 0x04002A77 RID: 10871
		public static readonly DerObjectIdentifier Tiger192 = new DerObjectIdentifier("1.3.6.1.4.1.11591.12.2");

		// Token: 0x04002A78 RID: 10872
		public static readonly DerObjectIdentifier EncryptionAlgorithm = new DerObjectIdentifier("1.3.6.1.4.1.11591.13");

		// Token: 0x04002A79 RID: 10873
		public static readonly DerObjectIdentifier Serpent = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2");

		// Token: 0x04002A7A RID: 10874
		public static readonly DerObjectIdentifier Serpent128Ecb = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2.1");

		// Token: 0x04002A7B RID: 10875
		public static readonly DerObjectIdentifier Serpent128Cbc = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2.2");

		// Token: 0x04002A7C RID: 10876
		public static readonly DerObjectIdentifier Serpent128Ofb = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2.3");

		// Token: 0x04002A7D RID: 10877
		public static readonly DerObjectIdentifier Serpent128Cfb = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2.4");

		// Token: 0x04002A7E RID: 10878
		public static readonly DerObjectIdentifier Serpent192Ecb = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2.21");

		// Token: 0x04002A7F RID: 10879
		public static readonly DerObjectIdentifier Serpent192Cbc = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2.22");

		// Token: 0x04002A80 RID: 10880
		public static readonly DerObjectIdentifier Serpent192Ofb = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2.23");

		// Token: 0x04002A81 RID: 10881
		public static readonly DerObjectIdentifier Serpent192Cfb = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2.24");

		// Token: 0x04002A82 RID: 10882
		public static readonly DerObjectIdentifier Serpent256Ecb = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2.41");

		// Token: 0x04002A83 RID: 10883
		public static readonly DerObjectIdentifier Serpent256Cbc = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2.42");

		// Token: 0x04002A84 RID: 10884
		public static readonly DerObjectIdentifier Serpent256Ofb = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2.43");

		// Token: 0x04002A85 RID: 10885
		public static readonly DerObjectIdentifier Serpent256Cfb = new DerObjectIdentifier("1.3.6.1.4.1.11591.13.2.44");

		// Token: 0x04002A86 RID: 10886
		public static readonly DerObjectIdentifier Crc = new DerObjectIdentifier("1.3.6.1.4.1.11591.14");

		// Token: 0x04002A87 RID: 10887
		public static readonly DerObjectIdentifier Crc32 = new DerObjectIdentifier("1.3.6.1.4.1.11591.14.1");

		// Token: 0x04002A88 RID: 10888
		public static readonly DerObjectIdentifier EllipticCurve = new DerObjectIdentifier("1.3.6.1.4.1.11591.15");

		// Token: 0x04002A89 RID: 10889
		public static readonly DerObjectIdentifier Ed25519 = GnuObjectIdentifiers.EllipticCurve.Branch("1");
	}
}
