using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200067F RID: 1663
	public class CrlReason : DerEnumerated
	{
		// Token: 0x06003DE7 RID: 15847 RVA: 0x00178209 File Offset: 0x00176409
		public CrlReason(int reason) : base(reason)
		{
		}

		// Token: 0x06003DE8 RID: 15848 RVA: 0x00178212 File Offset: 0x00176412
		public CrlReason(DerEnumerated reason) : base(reason.Value.IntValue)
		{
		}

		// Token: 0x06003DE9 RID: 15849 RVA: 0x00178228 File Offset: 0x00176428
		public override string ToString()
		{
			int intValue = base.Value.IntValue;
			string str = (intValue < 0 || intValue > 10) ? "Invalid" : CrlReason.ReasonString[intValue];
			return "CrlReason: " + str;
		}

		// Token: 0x0400265C RID: 9820
		public const int Unspecified = 0;

		// Token: 0x0400265D RID: 9821
		public const int KeyCompromise = 1;

		// Token: 0x0400265E RID: 9822
		public const int CACompromise = 2;

		// Token: 0x0400265F RID: 9823
		public const int AffiliationChanged = 3;

		// Token: 0x04002660 RID: 9824
		public const int Superseded = 4;

		// Token: 0x04002661 RID: 9825
		public const int CessationOfOperation = 5;

		// Token: 0x04002662 RID: 9826
		public const int CertificateHold = 6;

		// Token: 0x04002663 RID: 9827
		public const int RemoveFromCrl = 8;

		// Token: 0x04002664 RID: 9828
		public const int PrivilegeWithdrawn = 9;

		// Token: 0x04002665 RID: 9829
		public const int AACompromise = 10;

		// Token: 0x04002666 RID: 9830
		private static readonly string[] ReasonString = new string[]
		{
			"Unspecified",
			"KeyCompromise",
			"CACompromise",
			"AffiliationChanged",
			"Superseded",
			"CessationOfOperation",
			"CertificateHold",
			"Unknown",
			"RemoveFromCrl",
			"PrivilegeWithdrawn",
			"AACompromise"
		};
	}
}
