using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000642 RID: 1602
	public class DerOctetString : Asn1OctetString
	{
		// Token: 0x06003C58 RID: 15448 RVA: 0x00173E91 File Offset: 0x00172091
		public DerOctetString(byte[] str) : base(str)
		{
		}

		// Token: 0x06003C59 RID: 15449 RVA: 0x00173E9A File Offset: 0x0017209A
		public DerOctetString(Asn1Encodable obj) : base(obj)
		{
		}

		// Token: 0x06003C5A RID: 15450 RVA: 0x00173EA3 File Offset: 0x001720A3
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(4, this.str);
		}

		// Token: 0x06003C5B RID: 15451 RVA: 0x00173EB2 File Offset: 0x001720B2
		internal static void Encode(DerOutputStream derOut, byte[] bytes, int offset, int length)
		{
			derOut.WriteEncoded(4, bytes, offset, length);
		}
	}
}
