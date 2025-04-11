using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x02000751 RID: 1873
	public class CertReqMsg : Asn1Encodable
	{
		// Token: 0x060043A6 RID: 17318 RVA: 0x0018E55C File Offset: 0x0018C75C
		private CertReqMsg(Asn1Sequence seq)
		{
			this.certReq = CertRequest.GetInstance(seq[0]);
			for (int i = 1; i < seq.Count; i++)
			{
				object obj = seq[i];
				if (obj is Asn1TaggedObject || obj is ProofOfPossession)
				{
					this.popo = ProofOfPossession.GetInstance(obj);
				}
				else
				{
					this.regInfo = Asn1Sequence.GetInstance(obj);
				}
			}
		}

		// Token: 0x060043A7 RID: 17319 RVA: 0x0018E5C4 File Offset: 0x0018C7C4
		public static CertReqMsg GetInstance(object obj)
		{
			if (obj is CertReqMsg)
			{
				return (CertReqMsg)obj;
			}
			if (obj != null)
			{
				return new CertReqMsg(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x060043A8 RID: 17320 RVA: 0x0018E5E5 File Offset: 0x0018C7E5
		public static CertReqMsg GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return CertReqMsg.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x060043A9 RID: 17321 RVA: 0x0018E5F4 File Offset: 0x0018C7F4
		public CertReqMsg(CertRequest certReq, ProofOfPossession popo, AttributeTypeAndValue[] regInfo)
		{
			if (certReq == null)
			{
				throw new ArgumentNullException("certReq");
			}
			this.certReq = certReq;
			this.popo = popo;
			if (regInfo != null)
			{
				this.regInfo = new DerSequence(regInfo);
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x060043AA RID: 17322 RVA: 0x0018E634 File Offset: 0x0018C834
		public virtual CertRequest CertReq
		{
			get
			{
				return this.certReq;
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x060043AB RID: 17323 RVA: 0x0018E63C File Offset: 0x0018C83C
		public virtual ProofOfPossession Popo
		{
			get
			{
				return this.popo;
			}
		}

		// Token: 0x060043AC RID: 17324 RVA: 0x0018E644 File Offset: 0x0018C844
		public virtual AttributeTypeAndValue[] GetRegInfo()
		{
			if (this.regInfo == null)
			{
				return null;
			}
			AttributeTypeAndValue[] array = new AttributeTypeAndValue[this.regInfo.Count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = AttributeTypeAndValue.GetInstance(this.regInfo[num]);
			}
			return array;
		}

		// Token: 0x060043AD RID: 17325 RVA: 0x0018E690 File Offset: 0x0018C890
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.certReq
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.popo,
				this.regInfo
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002B70 RID: 11120
		private readonly CertRequest certReq;

		// Token: 0x04002B71 RID: 11121
		private readonly ProofOfPossession popo;

		// Token: 0x04002B72 RID: 11122
		private readonly Asn1Sequence regInfo;
	}
}
