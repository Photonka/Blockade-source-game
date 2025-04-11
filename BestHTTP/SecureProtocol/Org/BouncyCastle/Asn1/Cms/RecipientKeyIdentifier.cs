using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000788 RID: 1928
	public class RecipientKeyIdentifier : Asn1Encodable
	{
		// Token: 0x0600454A RID: 17738 RVA: 0x001929CC File Offset: 0x00190BCC
		public RecipientKeyIdentifier(Asn1OctetString subjectKeyIdentifier, DerGeneralizedTime date, OtherKeyAttribute other)
		{
			this.subjectKeyIdentifier = subjectKeyIdentifier;
			this.date = date;
			this.other = other;
		}

		// Token: 0x0600454B RID: 17739 RVA: 0x001929E9 File Offset: 0x00190BE9
		public RecipientKeyIdentifier(byte[] subjectKeyIdentifier) : this(subjectKeyIdentifier, null, null)
		{
		}

		// Token: 0x0600454C RID: 17740 RVA: 0x001929F4 File Offset: 0x00190BF4
		public RecipientKeyIdentifier(byte[] subjectKeyIdentifier, DerGeneralizedTime date, OtherKeyAttribute other)
		{
			this.subjectKeyIdentifier = new DerOctetString(subjectKeyIdentifier);
			this.date = date;
			this.other = other;
		}

		// Token: 0x0600454D RID: 17741 RVA: 0x00192A18 File Offset: 0x00190C18
		public RecipientKeyIdentifier(Asn1Sequence seq)
		{
			this.subjectKeyIdentifier = Asn1OctetString.GetInstance(seq[0]);
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
				throw new ArgumentException("Invalid RecipientKeyIdentifier");
			}
		}

		// Token: 0x0600454E RID: 17742 RVA: 0x00192ABE File Offset: 0x00190CBE
		public static RecipientKeyIdentifier GetInstance(Asn1TaggedObject ato, bool explicitly)
		{
			return RecipientKeyIdentifier.GetInstance(Asn1Sequence.GetInstance(ato, explicitly));
		}

		// Token: 0x0600454F RID: 17743 RVA: 0x00192ACC File Offset: 0x00190CCC
		public static RecipientKeyIdentifier GetInstance(object obj)
		{
			if (obj == null || obj is RecipientKeyIdentifier)
			{
				return (RecipientKeyIdentifier)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RecipientKeyIdentifier((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid RecipientKeyIdentifier: " + Platform.GetTypeName(obj));
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06004550 RID: 17744 RVA: 0x00192B09 File Offset: 0x00190D09
		public Asn1OctetString SubjectKeyIdentifier
		{
			get
			{
				return this.subjectKeyIdentifier;
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06004551 RID: 17745 RVA: 0x00192B11 File Offset: 0x00190D11
		public DerGeneralizedTime Date
		{
			get
			{
				return this.date;
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06004552 RID: 17746 RVA: 0x00192B19 File Offset: 0x00190D19
		public OtherKeyAttribute OtherKeyAttribute
		{
			get
			{
				return this.other;
			}
		}

		// Token: 0x06004553 RID: 17747 RVA: 0x00192B24 File Offset: 0x00190D24
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.subjectKeyIdentifier
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.date,
				this.other
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002C35 RID: 11317
		private Asn1OctetString subjectKeyIdentifier;

		// Token: 0x04002C36 RID: 11318
		private DerGeneralizedTime date;

		// Token: 0x04002C37 RID: 11319
		private OtherKeyAttribute other;
	}
}
