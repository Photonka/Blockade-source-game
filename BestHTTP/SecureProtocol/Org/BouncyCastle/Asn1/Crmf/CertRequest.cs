using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x02000752 RID: 1874
	public class CertRequest : Asn1Encodable
	{
		// Token: 0x060043AE RID: 17326 RVA: 0x0018E6D8 File Offset: 0x0018C8D8
		private CertRequest(Asn1Sequence seq)
		{
			this.certReqId = DerInteger.GetInstance(seq[0]);
			this.certTemplate = CertTemplate.GetInstance(seq[1]);
			if (seq.Count > 2)
			{
				this.controls = Controls.GetInstance(seq[2]);
			}
		}

		// Token: 0x060043AF RID: 17327 RVA: 0x0018E72A File Offset: 0x0018C92A
		public static CertRequest GetInstance(object obj)
		{
			if (obj is CertRequest)
			{
				return (CertRequest)obj;
			}
			if (obj != null)
			{
				return new CertRequest(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x060043B0 RID: 17328 RVA: 0x0018E74B File Offset: 0x0018C94B
		public CertRequest(int certReqId, CertTemplate certTemplate, Controls controls) : this(new DerInteger(certReqId), certTemplate, controls)
		{
		}

		// Token: 0x060043B1 RID: 17329 RVA: 0x0018E75B File Offset: 0x0018C95B
		public CertRequest(DerInteger certReqId, CertTemplate certTemplate, Controls controls)
		{
			this.certReqId = certReqId;
			this.certTemplate = certTemplate;
			this.controls = controls;
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x060043B2 RID: 17330 RVA: 0x0018E778 File Offset: 0x0018C978
		public virtual DerInteger CertReqID
		{
			get
			{
				return this.certReqId;
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x060043B3 RID: 17331 RVA: 0x0018E780 File Offset: 0x0018C980
		public virtual CertTemplate CertTemplate
		{
			get
			{
				return this.certTemplate;
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x060043B4 RID: 17332 RVA: 0x0018E788 File Offset: 0x0018C988
		public virtual Controls Controls
		{
			get
			{
				return this.controls;
			}
		}

		// Token: 0x060043B5 RID: 17333 RVA: 0x0018E790 File Offset: 0x0018C990
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.certReqId,
				this.certTemplate
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.controls
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002B73 RID: 11123
		private readonly DerInteger certReqId;

		// Token: 0x04002B74 RID: 11124
		private readonly CertTemplate certTemplate;

		// Token: 0x04002B75 RID: 11125
		private readonly Controls controls;
	}
}
