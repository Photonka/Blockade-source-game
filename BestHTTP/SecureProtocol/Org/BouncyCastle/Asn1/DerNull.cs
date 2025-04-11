using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200063F RID: 1599
	public class DerNull : Asn1Null
	{
		// Token: 0x06003C33 RID: 15411 RVA: 0x0017375B File Offset: 0x0017195B
		[Obsolete("Use static Instance object")]
		public DerNull()
		{
		}

		// Token: 0x06003C34 RID: 15412 RVA: 0x0017375B File Offset: 0x0017195B
		protected internal DerNull(int dummy)
		{
		}

		// Token: 0x06003C35 RID: 15413 RVA: 0x0017376F File Offset: 0x0017196F
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(5, this.zeroBytes);
		}

		// Token: 0x06003C36 RID: 15414 RVA: 0x0017377E File Offset: 0x0017197E
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			return asn1Object is DerNull;
		}

		// Token: 0x06003C37 RID: 15415 RVA: 0x0008F871 File Offset: 0x0008DA71
		protected override int Asn1GetHashCode()
		{
			return -1;
		}

		// Token: 0x040025B5 RID: 9653
		public static readonly DerNull Instance = new DerNull(0);

		// Token: 0x040025B6 RID: 9654
		private byte[] zeroBytes = new byte[0];
	}
}
