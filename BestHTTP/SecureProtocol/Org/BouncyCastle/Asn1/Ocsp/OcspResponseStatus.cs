using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x020006F8 RID: 1784
	public class OcspResponseStatus : DerEnumerated
	{
		// Token: 0x06004180 RID: 16768 RVA: 0x00178209 File Offset: 0x00176409
		public OcspResponseStatus(int value) : base(value)
		{
		}

		// Token: 0x06004181 RID: 16769 RVA: 0x00178212 File Offset: 0x00176412
		public OcspResponseStatus(DerEnumerated value) : base(value.Value.IntValue)
		{
		}

		// Token: 0x0400297E RID: 10622
		public const int Successful = 0;

		// Token: 0x0400297F RID: 10623
		public const int MalformedRequest = 1;

		// Token: 0x04002980 RID: 10624
		public const int InternalError = 2;

		// Token: 0x04002981 RID: 10625
		public const int TryLater = 3;

		// Token: 0x04002982 RID: 10626
		public const int SignatureRequired = 5;

		// Token: 0x04002983 RID: 10627
		public const int Unauthorized = 6;
	}
}
