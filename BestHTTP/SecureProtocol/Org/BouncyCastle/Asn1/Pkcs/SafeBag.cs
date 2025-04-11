using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006EC RID: 1772
	public class SafeBag : Asn1Encodable
	{
		// Token: 0x06004129 RID: 16681 RVA: 0x00185064 File Offset: 0x00183264
		public SafeBag(DerObjectIdentifier oid, Asn1Object obj)
		{
			this.bagID = oid;
			this.bagValue = obj;
			this.bagAttributes = null;
		}

		// Token: 0x0600412A RID: 16682 RVA: 0x00185081 File Offset: 0x00183281
		public SafeBag(DerObjectIdentifier oid, Asn1Object obj, Asn1Set bagAttributes)
		{
			this.bagID = oid;
			this.bagValue = obj;
			this.bagAttributes = bagAttributes;
		}

		// Token: 0x0600412B RID: 16683 RVA: 0x001850A0 File Offset: 0x001832A0
		public SafeBag(Asn1Sequence seq)
		{
			this.bagID = (DerObjectIdentifier)seq[0];
			this.bagValue = ((DerTaggedObject)seq[1]).GetObject();
			if (seq.Count == 3)
			{
				this.bagAttributes = (Asn1Set)seq[2];
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x0600412C RID: 16684 RVA: 0x001850F7 File Offset: 0x001832F7
		public DerObjectIdentifier BagID
		{
			get
			{
				return this.bagID;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x0600412D RID: 16685 RVA: 0x001850FF File Offset: 0x001832FF
		public Asn1Object BagValue
		{
			get
			{
				return this.bagValue;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x0600412E RID: 16686 RVA: 0x00185107 File Offset: 0x00183307
		public Asn1Set BagAttributes
		{
			get
			{
				return this.bagAttributes;
			}
		}

		// Token: 0x0600412F RID: 16687 RVA: 0x00185110 File Offset: 0x00183310
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.bagID,
				new DerTaggedObject(0, this.bagValue)
			});
			if (this.bagAttributes != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.bagAttributes
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002946 RID: 10566
		private readonly DerObjectIdentifier bagID;

		// Token: 0x04002947 RID: 10567
		private readonly Asn1Object bagValue;

		// Token: 0x04002948 RID: 10568
		private readonly Asn1Set bagAttributes;
	}
}
