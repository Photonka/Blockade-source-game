using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x0200078F RID: 1935
	public class TimeStampAndCrl : Asn1Encodable
	{
		// Token: 0x0600458A RID: 17802 RVA: 0x00193844 File Offset: 0x00191A44
		public TimeStampAndCrl(ContentInfo timeStamp)
		{
			this.timeStamp = timeStamp;
		}

		// Token: 0x0600458B RID: 17803 RVA: 0x00193853 File Offset: 0x00191A53
		private TimeStampAndCrl(Asn1Sequence seq)
		{
			this.timeStamp = ContentInfo.GetInstance(seq[0]);
			if (seq.Count == 2)
			{
				this.crl = CertificateList.GetInstance(seq[1]);
			}
		}

		// Token: 0x0600458C RID: 17804 RVA: 0x00193888 File Offset: 0x00191A88
		public static TimeStampAndCrl GetInstance(object obj)
		{
			if (obj is TimeStampAndCrl)
			{
				return (TimeStampAndCrl)obj;
			}
			if (obj != null)
			{
				return new TimeStampAndCrl(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x0600458D RID: 17805 RVA: 0x001938A9 File Offset: 0x00191AA9
		public virtual ContentInfo TimeStampToken
		{
			get
			{
				return this.timeStamp;
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x0600458E RID: 17806 RVA: 0x001938B1 File Offset: 0x00191AB1
		public virtual CertificateList Crl
		{
			get
			{
				return this.crl;
			}
		}

		// Token: 0x0600458F RID: 17807 RVA: 0x001938BC File Offset: 0x00191ABC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.timeStamp
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.crl
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002C54 RID: 11348
		private ContentInfo timeStamp;

		// Token: 0x04002C55 RID: 11349
		private CertificateList crl;
	}
}
