using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200064E RID: 1614
	public class DerTaggedObject : Asn1TaggedObject
	{
		// Token: 0x06003CA0 RID: 15520 RVA: 0x00174776 File Offset: 0x00172976
		public DerTaggedObject(int tagNo, Asn1Encodable obj) : base(tagNo, obj)
		{
		}

		// Token: 0x06003CA1 RID: 15521 RVA: 0x00174780 File Offset: 0x00172980
		public DerTaggedObject(bool explicitly, int tagNo, Asn1Encodable obj) : base(explicitly, tagNo, obj)
		{
		}

		// Token: 0x06003CA2 RID: 15522 RVA: 0x0017478B File Offset: 0x0017298B
		public DerTaggedObject(int tagNo) : base(false, tagNo, DerSequence.Empty)
		{
		}

		// Token: 0x06003CA3 RID: 15523 RVA: 0x0017479C File Offset: 0x0017299C
		internal override void Encode(DerOutputStream derOut)
		{
			if (base.IsEmpty())
			{
				derOut.WriteEncoded(160, this.tagNo, new byte[0]);
				return;
			}
			byte[] derEncoded = this.obj.GetDerEncoded();
			if (this.explicitly)
			{
				derOut.WriteEncoded(160, this.tagNo, derEncoded);
				return;
			}
			int flags = (int)((derEncoded[0] & 32) | 128);
			derOut.WriteTag(flags, this.tagNo);
			derOut.Write(derEncoded, 1, derEncoded.Length - 1);
		}
	}
}
