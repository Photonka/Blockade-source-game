using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006D9 RID: 1753
	public class ContentInfo : Asn1Encodable
	{
		// Token: 0x0600409F RID: 16543 RVA: 0x00183038 File Offset: 0x00181238
		public static ContentInfo GetInstance(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			ContentInfo contentInfo = obj as ContentInfo;
			if (contentInfo != null)
			{
				return contentInfo;
			}
			return new ContentInfo(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x060040A0 RID: 16544 RVA: 0x00183061 File Offset: 0x00181261
		private ContentInfo(Asn1Sequence seq)
		{
			this.contentType = (DerObjectIdentifier)seq[0];
			if (seq.Count > 1)
			{
				this.content = ((Asn1TaggedObject)seq[1]).GetObject();
			}
		}

		// Token: 0x060040A1 RID: 16545 RVA: 0x0018309B File Offset: 0x0018129B
		public ContentInfo(DerObjectIdentifier contentType, Asn1Encodable content)
		{
			this.contentType = contentType;
			this.content = content;
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x060040A2 RID: 16546 RVA: 0x001830B1 File Offset: 0x001812B1
		public DerObjectIdentifier ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x060040A3 RID: 16547 RVA: 0x001830B9 File Offset: 0x001812B9
		public Asn1Encodable Content
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x060040A4 RID: 16548 RVA: 0x001830C4 File Offset: 0x001812C4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.contentType
			});
			if (this.content != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new BerTaggedObject(0, this.content)
				});
			}
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x0400287F RID: 10367
		private readonly DerObjectIdentifier contentType;

		// Token: 0x04002880 RID: 10368
		private readonly Asn1Encodable content;
	}
}
