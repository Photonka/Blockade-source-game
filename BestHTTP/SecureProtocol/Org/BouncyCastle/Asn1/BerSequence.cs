using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000628 RID: 1576
	public class BerSequence : DerSequence
	{
		// Token: 0x06003B76 RID: 15222 RVA: 0x00171538 File Offset: 0x0016F738
		public new static BerSequence FromVector(Asn1EncodableVector v)
		{
			if (v.Count >= 1)
			{
				return new BerSequence(v);
			}
			return BerSequence.Empty;
		}

		// Token: 0x06003B77 RID: 15223 RVA: 0x0017154F File Offset: 0x0016F74F
		public BerSequence()
		{
		}

		// Token: 0x06003B78 RID: 15224 RVA: 0x00171557 File Offset: 0x0016F757
		public BerSequence(Asn1Encodable obj) : base(obj)
		{
		}

		// Token: 0x06003B79 RID: 15225 RVA: 0x00171560 File Offset: 0x0016F760
		public BerSequence(params Asn1Encodable[] v) : base(v)
		{
		}

		// Token: 0x06003B7A RID: 15226 RVA: 0x00171569 File Offset: 0x0016F769
		public BerSequence(Asn1EncodableVector v) : base(v)
		{
		}

		// Token: 0x06003B7B RID: 15227 RVA: 0x00171574 File Offset: 0x0016F774
		internal override void Encode(DerOutputStream derOut)
		{
			if (derOut is Asn1OutputStream || derOut is BerOutputStream)
			{
				derOut.WriteByte(48);
				derOut.WriteByte(128);
				foreach (object obj in this)
				{
					Asn1Encodable obj2 = (Asn1Encodable)obj;
					derOut.WriteObject(obj2);
				}
				derOut.WriteByte(0);
				derOut.WriteByte(0);
				return;
			}
			base.Encode(derOut);
		}

		// Token: 0x0400258D RID: 9613
		public new static readonly BerSequence Empty = new BerSequence();
	}
}
