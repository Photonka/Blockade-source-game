using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000690 RID: 1680
	public class NoticeReference : Asn1Encodable
	{
		// Token: 0x06003E75 RID: 15989 RVA: 0x0017A45C File Offset: 0x0017865C
		private static Asn1EncodableVector ConvertVector(IList numbers)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			foreach (object obj in numbers)
			{
				DerInteger derInteger;
				if (obj is BigInteger)
				{
					derInteger = new DerInteger((BigInteger)obj);
				}
				else
				{
					if (!(obj is int))
					{
						throw new ArgumentException();
					}
					derInteger = new DerInteger((int)obj);
				}
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					derInteger
				});
			}
			return asn1EncodableVector;
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x0017A4F8 File Offset: 0x001786F8
		public NoticeReference(string organization, IList numbers) : this(organization, NoticeReference.ConvertVector(numbers))
		{
		}

		// Token: 0x06003E77 RID: 15991 RVA: 0x0017A507 File Offset: 0x00178707
		public NoticeReference(string organization, Asn1EncodableVector noticeNumbers) : this(new DisplayText(organization), noticeNumbers)
		{
		}

		// Token: 0x06003E78 RID: 15992 RVA: 0x0017A516 File Offset: 0x00178716
		public NoticeReference(DisplayText organization, Asn1EncodableVector noticeNumbers)
		{
			this.organization = organization;
			this.noticeNumbers = new DerSequence(noticeNumbers);
		}

		// Token: 0x06003E79 RID: 15993 RVA: 0x0017A534 File Offset: 0x00178734
		private NoticeReference(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.organization = DisplayText.GetInstance(seq[0]);
			this.noticeNumbers = Asn1Sequence.GetInstance(seq[1]);
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x0017A594 File Offset: 0x00178794
		public static NoticeReference GetInstance(object obj)
		{
			if (obj is NoticeReference)
			{
				return (NoticeReference)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new NoticeReference(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06003E7B RID: 15995 RVA: 0x0017A5B5 File Offset: 0x001787B5
		public virtual DisplayText Organization
		{
			get
			{
				return this.organization;
			}
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x0017A5C0 File Offset: 0x001787C0
		public virtual DerInteger[] GetNoticeNumbers()
		{
			DerInteger[] array = new DerInteger[this.noticeNumbers.Count];
			for (int num = 0; num != this.noticeNumbers.Count; num++)
			{
				array[num] = DerInteger.GetInstance(this.noticeNumbers[num]);
			}
			return array;
		}

		// Token: 0x06003E7D RID: 15997 RVA: 0x0017A609 File Offset: 0x00178809
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.organization,
				this.noticeNumbers
			});
		}

		// Token: 0x040026B7 RID: 9911
		private readonly DisplayText organization;

		// Token: 0x040026B8 RID: 9912
		private readonly Asn1Sequence noticeNumbers;
	}
}
