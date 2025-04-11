using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007A8 RID: 1960
	public class PkiFailureInfo : DerBitString
	{
		// Token: 0x06004626 RID: 17958 RVA: 0x0017105D File Offset: 0x0016F25D
		public PkiFailureInfo(int info) : base(info)
		{
		}

		// Token: 0x06004627 RID: 17959 RVA: 0x0017A21D File Offset: 0x0017841D
		public PkiFailureInfo(DerBitString info) : base(info.GetBytes(), info.PadBits)
		{
		}

		// Token: 0x06004628 RID: 17960 RVA: 0x0019545C File Offset: 0x0019365C
		public override string ToString()
		{
			return "PkiFailureInfo: 0x" + this.IntValue.ToString("X");
		}

		// Token: 0x04002CC5 RID: 11461
		public const int BadAlg = 128;

		// Token: 0x04002CC6 RID: 11462
		public const int BadMessageCheck = 64;

		// Token: 0x04002CC7 RID: 11463
		public const int BadRequest = 32;

		// Token: 0x04002CC8 RID: 11464
		public const int BadTime = 16;

		// Token: 0x04002CC9 RID: 11465
		public const int BadCertId = 8;

		// Token: 0x04002CCA RID: 11466
		public const int BadDataFormat = 4;

		// Token: 0x04002CCB RID: 11467
		public const int WrongAuthority = 2;

		// Token: 0x04002CCC RID: 11468
		public const int IncorrectData = 1;

		// Token: 0x04002CCD RID: 11469
		public const int MissingTimeStamp = 32768;

		// Token: 0x04002CCE RID: 11470
		public const int BadPop = 16384;

		// Token: 0x04002CCF RID: 11471
		public const int CertRevoked = 8192;

		// Token: 0x04002CD0 RID: 11472
		public const int CertConfirmed = 4096;

		// Token: 0x04002CD1 RID: 11473
		public const int WrongIntegrity = 2048;

		// Token: 0x04002CD2 RID: 11474
		public const int BadRecipientNonce = 1024;

		// Token: 0x04002CD3 RID: 11475
		public const int TimeNotAvailable = 512;

		// Token: 0x04002CD4 RID: 11476
		public const int UnacceptedPolicy = 256;

		// Token: 0x04002CD5 RID: 11477
		public const int UnacceptedExtension = 8388608;

		// Token: 0x04002CD6 RID: 11478
		public const int AddInfoNotAvailable = 4194304;

		// Token: 0x04002CD7 RID: 11479
		public const int BadSenderNonce = 2097152;

		// Token: 0x04002CD8 RID: 11480
		public const int BadCertTemplate = 1048576;

		// Token: 0x04002CD9 RID: 11481
		public const int SignerNotTrusted = 524288;

		// Token: 0x04002CDA RID: 11482
		public const int TransactionIdInUse = 262144;

		// Token: 0x04002CDB RID: 11483
		public const int UnsupportedVersion = 131072;

		// Token: 0x04002CDC RID: 11484
		public const int NotAuthorized = 65536;

		// Token: 0x04002CDD RID: 11485
		public const int SystemUnavail = -2147483648;

		// Token: 0x04002CDE RID: 11486
		public const int SystemFailure = 1073741824;

		// Token: 0x04002CDF RID: 11487
		public const int DuplicateCertReq = 536870912;
	}
}
