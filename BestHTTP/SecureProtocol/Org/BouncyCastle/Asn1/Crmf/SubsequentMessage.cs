using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x02000763 RID: 1891
	public class SubsequentMessage : DerInteger
	{
		// Token: 0x0600442E RID: 17454 RVA: 0x0018FA21 File Offset: 0x0018DC21
		private SubsequentMessage(int value) : base(value)
		{
		}

		// Token: 0x0600442F RID: 17455 RVA: 0x0018FA2A File Offset: 0x0018DC2A
		public static SubsequentMessage ValueOf(int value)
		{
			if (value == 0)
			{
				return SubsequentMessage.encrCert;
			}
			if (value == 1)
			{
				return SubsequentMessage.challengeResp;
			}
			throw new ArgumentException("unknown value: " + value, "value");
		}

		// Token: 0x04002BBD RID: 11197
		public static readonly SubsequentMessage encrCert = new SubsequentMessage(0);

		// Token: 0x04002BBE RID: 11198
		public static readonly SubsequentMessage challengeResp = new SubsequentMessage(1);
	}
}
