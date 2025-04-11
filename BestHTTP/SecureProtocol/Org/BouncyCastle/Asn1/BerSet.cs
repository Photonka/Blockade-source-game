using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200062B RID: 1579
	public class BerSet : DerSet
	{
		// Token: 0x06003B82 RID: 15234 RVA: 0x00171662 File Offset: 0x0016F862
		public new static BerSet FromVector(Asn1EncodableVector v)
		{
			if (v.Count >= 1)
			{
				return new BerSet(v);
			}
			return BerSet.Empty;
		}

		// Token: 0x06003B83 RID: 15235 RVA: 0x00171679 File Offset: 0x0016F879
		internal new static BerSet FromVector(Asn1EncodableVector v, bool needsSorting)
		{
			if (v.Count >= 1)
			{
				return new BerSet(v, needsSorting);
			}
			return BerSet.Empty;
		}

		// Token: 0x06003B84 RID: 15236 RVA: 0x00171691 File Offset: 0x0016F891
		public BerSet()
		{
		}

		// Token: 0x06003B85 RID: 15237 RVA: 0x00171699 File Offset: 0x0016F899
		public BerSet(Asn1Encodable obj) : base(obj)
		{
		}

		// Token: 0x06003B86 RID: 15238 RVA: 0x001716A2 File Offset: 0x0016F8A2
		public BerSet(Asn1EncodableVector v) : base(v, false)
		{
		}

		// Token: 0x06003B87 RID: 15239 RVA: 0x001716AC File Offset: 0x0016F8AC
		internal BerSet(Asn1EncodableVector v, bool needsSorting) : base(v, needsSorting)
		{
		}

		// Token: 0x06003B88 RID: 15240 RVA: 0x001716B8 File Offset: 0x0016F8B8
		internal override void Encode(DerOutputStream derOut)
		{
			if (derOut is Asn1OutputStream || derOut is BerOutputStream)
			{
				derOut.WriteByte(49);
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

		// Token: 0x0400258F RID: 9615
		public new static readonly BerSet Empty = new BerSet();
	}
}
