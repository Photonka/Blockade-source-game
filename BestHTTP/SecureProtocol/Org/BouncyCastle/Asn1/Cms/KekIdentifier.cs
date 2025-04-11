using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000778 RID: 1912
	public class KekIdentifier : Asn1Encodable
	{
		// Token: 0x060044C4 RID: 17604 RVA: 0x001916D1 File Offset: 0x0018F8D1
		public KekIdentifier(byte[] keyIdentifier, DerGeneralizedTime date, OtherKeyAttribute other)
		{
			this.keyIdentifier = new DerOctetString(keyIdentifier);
			this.date = date;
			this.other = other;
		}

		// Token: 0x060044C5 RID: 17605 RVA: 0x001916F4 File Offset: 0x0018F8F4
		public KekIdentifier(Asn1Sequence seq)
		{
			this.keyIdentifier = (Asn1OctetString)seq[0];
			switch (seq.Count)
			{
			case 1:
				return;
			case 2:
				if (seq[1] is DerGeneralizedTime)
				{
					this.date = (DerGeneralizedTime)seq[1];
					return;
				}
				this.other = OtherKeyAttribute.GetInstance(seq[2]);
				return;
			case 3:
				this.date = (DerGeneralizedTime)seq[1];
				this.other = OtherKeyAttribute.GetInstance(seq[2]);
				return;
			default:
				throw new ArgumentException("Invalid KekIdentifier");
			}
		}

		// Token: 0x060044C6 RID: 17606 RVA: 0x0019179A File Offset: 0x0018F99A
		public static KekIdentifier GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return KekIdentifier.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060044C7 RID: 17607 RVA: 0x001917A8 File Offset: 0x0018F9A8
		public static KekIdentifier GetInstance(object obj)
		{
			if (obj == null || obj is KekIdentifier)
			{
				return (KekIdentifier)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new KekIdentifier((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid KekIdentifier: " + Platform.GetTypeName(obj));
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x060044C8 RID: 17608 RVA: 0x001917E5 File Offset: 0x0018F9E5
		public Asn1OctetString KeyIdentifier
		{
			get
			{
				return this.keyIdentifier;
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x060044C9 RID: 17609 RVA: 0x001917ED File Offset: 0x0018F9ED
		public DerGeneralizedTime Date
		{
			get
			{
				return this.date;
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x060044CA RID: 17610 RVA: 0x001917F5 File Offset: 0x0018F9F5
		public OtherKeyAttribute Other
		{
			get
			{
				return this.other;
			}
		}

		// Token: 0x060044CB RID: 17611 RVA: 0x00191800 File Offset: 0x0018FA00
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.keyIdentifier
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.date,
				this.other
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002C0C RID: 11276
		private Asn1OctetString keyIdentifier;

		// Token: 0x04002C0D RID: 11277
		private DerGeneralizedTime date;

		// Token: 0x04002C0E RID: 11278
		private OtherKeyAttribute other;
	}
}
