using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200077F RID: 1919
	public class OriginatorInfo : Asn1Encodable
	{
		// Token: 0x06004504 RID: 17668 RVA: 0x0019201E File Offset: 0x0019021E
		public OriginatorInfo(Asn1Set certs, Asn1Set crls)
		{
			this.certs = certs;
			this.crls = crls;
		}

		// Token: 0x06004505 RID: 17669 RVA: 0x00192034 File Offset: 0x00190234
		public OriginatorInfo(Asn1Sequence seq)
		{
			switch (seq.Count)
			{
			case 0:
				return;
			case 1:
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[0];
				int tagNo = asn1TaggedObject.TagNo;
				if (tagNo == 0)
				{
					this.certs = Asn1Set.GetInstance(asn1TaggedObject, false);
					return;
				}
				if (tagNo != 1)
				{
					throw new ArgumentException("Bad tag in OriginatorInfo: " + asn1TaggedObject.TagNo);
				}
				this.crls = Asn1Set.GetInstance(asn1TaggedObject, false);
				return;
			}
			case 2:
				this.certs = Asn1Set.GetInstance((Asn1TaggedObject)seq[0], false);
				this.crls = Asn1Set.GetInstance((Asn1TaggedObject)seq[1], false);
				return;
			default:
				throw new ArgumentException("OriginatorInfo too big");
			}
		}

		// Token: 0x06004506 RID: 17670 RVA: 0x001920F5 File Offset: 0x001902F5
		public static OriginatorInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return OriginatorInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06004507 RID: 17671 RVA: 0x00192103 File Offset: 0x00190303
		public static OriginatorInfo GetInstance(object obj)
		{
			if (obj == null || obj is OriginatorInfo)
			{
				return (OriginatorInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OriginatorInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid OriginatorInfo: " + Platform.GetTypeName(obj));
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06004508 RID: 17672 RVA: 0x00192140 File Offset: 0x00190340
		public Asn1Set Certificates
		{
			get
			{
				return this.certs;
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06004509 RID: 17673 RVA: 0x00192148 File Offset: 0x00190348
		public Asn1Set Crls
		{
			get
			{
				return this.crls;
			}
		}

		// Token: 0x0600450A RID: 17674 RVA: 0x00192150 File Offset: 0x00190350
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.certs != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.certs)
				});
			}
			if (this.crls != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.crls)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002C23 RID: 11299
		private Asn1Set certs;

		// Token: 0x04002C24 RID: 11300
		private Asn1Set crls;
	}
}
