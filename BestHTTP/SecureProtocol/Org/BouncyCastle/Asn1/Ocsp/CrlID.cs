using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x020006F4 RID: 1780
	public class CrlID : Asn1Encodable
	{
		// Token: 0x0600416B RID: 16747 RVA: 0x00185B0C File Offset: 0x00183D0C
		public CrlID(Asn1Sequence seq)
		{
			foreach (object obj in seq)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
					this.crlUrl = DerIA5String.GetInstance(asn1TaggedObject, true);
					break;
				case 1:
					this.crlNum = DerInteger.GetInstance(asn1TaggedObject, true);
					break;
				case 2:
					this.crlTime = DerGeneralizedTime.GetInstance(asn1TaggedObject, true);
					break;
				default:
					throw new ArgumentException("unknown tag number: " + asn1TaggedObject.TagNo);
				}
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x0600416C RID: 16748 RVA: 0x00185BC4 File Offset: 0x00183DC4
		public DerIA5String CrlUrl
		{
			get
			{
				return this.crlUrl;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x0600416D RID: 16749 RVA: 0x00185BCC File Offset: 0x00183DCC
		public DerInteger CrlNum
		{
			get
			{
				return this.crlNum;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x0600416E RID: 16750 RVA: 0x00185BD4 File Offset: 0x00183DD4
		public DerGeneralizedTime CrlTime
		{
			get
			{
				return this.crlTime;
			}
		}

		// Token: 0x0600416F RID: 16751 RVA: 0x00185BDC File Offset: 0x00183DDC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.crlUrl != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.crlUrl)
				});
			}
			if (this.crlNum != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, this.crlNum)
				});
			}
			if (this.crlTime != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 2, this.crlTime)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400296E RID: 10606
		private readonly DerIA5String crlUrl;

		// Token: 0x0400296F RID: 10607
		private readonly DerInteger crlNum;

		// Token: 0x04002970 RID: 10608
		private readonly DerGeneralizedTime crlTime;
	}
}
