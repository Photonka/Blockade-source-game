using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000736 RID: 1846
	public class OcspIdentifier : Asn1Encodable
	{
		// Token: 0x060042F7 RID: 17143 RVA: 0x0018B9E8 File Offset: 0x00189BE8
		public static OcspIdentifier GetInstance(object obj)
		{
			if (obj == null || obj is OcspIdentifier)
			{
				return (OcspIdentifier)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OcspIdentifier((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'OcspIdentifier' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060042F8 RID: 17144 RVA: 0x0018BA38 File Offset: 0x00189C38
		private OcspIdentifier(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.ocspResponderID = ResponderID.GetInstance(seq[0].ToAsn1Object());
			this.producedAt = (DerGeneralizedTime)seq[1].ToAsn1Object();
		}

		// Token: 0x060042F9 RID: 17145 RVA: 0x0018BAB0 File Offset: 0x00189CB0
		public OcspIdentifier(ResponderID ocspResponderID, DateTime producedAt)
		{
			if (ocspResponderID == null)
			{
				throw new ArgumentNullException();
			}
			this.ocspResponderID = ocspResponderID;
			this.producedAt = new DerGeneralizedTime(producedAt);
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x060042FA RID: 17146 RVA: 0x0018BAD4 File Offset: 0x00189CD4
		public ResponderID OcspResponderID
		{
			get
			{
				return this.ocspResponderID;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x060042FB RID: 17147 RVA: 0x0018BADC File Offset: 0x00189CDC
		public DateTime ProducedAt
		{
			get
			{
				return this.producedAt.ToDateTime();
			}
		}

		// Token: 0x060042FC RID: 17148 RVA: 0x0018BAE9 File Offset: 0x00189CE9
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.ocspResponderID,
				this.producedAt
			});
		}

		// Token: 0x04002B03 RID: 11011
		private readonly ResponderID ocspResponderID;

		// Token: 0x04002B04 RID: 11012
		private readonly DerGeneralizedTime producedAt;
	}
}
