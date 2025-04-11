using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000697 RID: 1687
	public class ReasonFlags : DerBitString
	{
		// Token: 0x06003EA0 RID: 16032 RVA: 0x0017105D File Offset: 0x0016F25D
		public ReasonFlags(int reasons) : base(reasons)
		{
		}

		// Token: 0x06003EA1 RID: 16033 RVA: 0x0017A21D File Offset: 0x0017841D
		public ReasonFlags(DerBitString reasons) : base(reasons.GetBytes(), reasons.PadBits)
		{
		}

		// Token: 0x040026CA RID: 9930
		public const int Unused = 128;

		// Token: 0x040026CB RID: 9931
		public const int KeyCompromise = 64;

		// Token: 0x040026CC RID: 9932
		public const int CACompromise = 32;

		// Token: 0x040026CD RID: 9933
		public const int AffiliationChanged = 16;

		// Token: 0x040026CE RID: 9934
		public const int Superseded = 8;

		// Token: 0x040026CF RID: 9935
		public const int CessationOfOperation = 4;

		// Token: 0x040026D0 RID: 9936
		public const int CertificateHold = 2;

		// Token: 0x040026D1 RID: 9937
		public const int PrivilegeWithdrawn = 1;

		// Token: 0x040026D2 RID: 9938
		public const int AACompromise = 32768;
	}
}
