using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200076F RID: 1903
	public class ContentInfo : Asn1Encodable
	{
		// Token: 0x06004489 RID: 17545 RVA: 0x00190D04 File Offset: 0x0018EF04
		public static ContentInfo GetInstance(object obj)
		{
			if (obj == null || obj is ContentInfo)
			{
				return (ContentInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ContentInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj));
		}

		// Token: 0x0600448A RID: 17546 RVA: 0x00190D41 File Offset: 0x0018EF41
		public static ContentInfo GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return ContentInfo.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x0600448B RID: 17547 RVA: 0x00190D50 File Offset: 0x0018EF50
		private ContentInfo(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.contentType = (DerObjectIdentifier)seq[0];
			if (seq.Count > 1)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[1];
				if (!asn1TaggedObject.IsExplicit() || asn1TaggedObject.TagNo != 0)
				{
					throw new ArgumentException("Bad tag for 'content'", "seq");
				}
				this.content = asn1TaggedObject.GetObject();
			}
		}

		// Token: 0x0600448C RID: 17548 RVA: 0x00190DE9 File Offset: 0x0018EFE9
		public ContentInfo(DerObjectIdentifier contentType, Asn1Encodable content)
		{
			this.contentType = contentType;
			this.content = content;
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x0600448D RID: 17549 RVA: 0x00190DFF File Offset: 0x0018EFFF
		public DerObjectIdentifier ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x0600448E RID: 17550 RVA: 0x00190E07 File Offset: 0x0018F007
		public Asn1Encodable Content
		{
			get
			{
				return this.content;
			}
		}

		// Token: 0x0600448F RID: 17551 RVA: 0x00190E10 File Offset: 0x0018F010
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

		// Token: 0x04002BF3 RID: 11251
		private readonly DerObjectIdentifier contentType;

		// Token: 0x04002BF4 RID: 11252
		private readonly Asn1Encodable content;
	}
}
