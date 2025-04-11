using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x02000700 RID: 1792
	public class SingleResponse : Asn1Encodable
	{
		// Token: 0x060041BD RID: 16829 RVA: 0x00186879 File Offset: 0x00184A79
		public SingleResponse(CertID certID, CertStatus certStatus, DerGeneralizedTime thisUpdate, DerGeneralizedTime nextUpdate, X509Extensions singleExtensions)
		{
			this.certID = certID;
			this.certStatus = certStatus;
			this.thisUpdate = thisUpdate;
			this.nextUpdate = nextUpdate;
			this.singleExtensions = singleExtensions;
		}

		// Token: 0x060041BE RID: 16830 RVA: 0x001868A8 File Offset: 0x00184AA8
		public SingleResponse(Asn1Sequence seq)
		{
			this.certID = CertID.GetInstance(seq[0]);
			this.certStatus = CertStatus.GetInstance(seq[1]);
			this.thisUpdate = (DerGeneralizedTime)seq[2];
			if (seq.Count > 4)
			{
				this.nextUpdate = DerGeneralizedTime.GetInstance((Asn1TaggedObject)seq[3], true);
				this.singleExtensions = X509Extensions.GetInstance((Asn1TaggedObject)seq[4], true);
				return;
			}
			if (seq.Count > 3)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[3];
				if (asn1TaggedObject.TagNo == 0)
				{
					this.nextUpdate = DerGeneralizedTime.GetInstance(asn1TaggedObject, true);
					return;
				}
				this.singleExtensions = X509Extensions.GetInstance(asn1TaggedObject, true);
			}
		}

		// Token: 0x060041BF RID: 16831 RVA: 0x00186964 File Offset: 0x00184B64
		public static SingleResponse GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return SingleResponse.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060041C0 RID: 16832 RVA: 0x00186974 File Offset: 0x00184B74
		public static SingleResponse GetInstance(object obj)
		{
			if (obj == null || obj is SingleResponse)
			{
				return (SingleResponse)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SingleResponse((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x060041C1 RID: 16833 RVA: 0x001869C1 File Offset: 0x00184BC1
		public CertID CertId
		{
			get
			{
				return this.certID;
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x060041C2 RID: 16834 RVA: 0x001869C9 File Offset: 0x00184BC9
		public CertStatus CertStatus
		{
			get
			{
				return this.certStatus;
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x060041C3 RID: 16835 RVA: 0x001869D1 File Offset: 0x00184BD1
		public DerGeneralizedTime ThisUpdate
		{
			get
			{
				return this.thisUpdate;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x060041C4 RID: 16836 RVA: 0x001869D9 File Offset: 0x00184BD9
		public DerGeneralizedTime NextUpdate
		{
			get
			{
				return this.nextUpdate;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x060041C5 RID: 16837 RVA: 0x001869E1 File Offset: 0x00184BE1
		public X509Extensions SingleExtensions
		{
			get
			{
				return this.singleExtensions;
			}
		}

		// Token: 0x060041C6 RID: 16838 RVA: 0x001869EC File Offset: 0x00184BEC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.certID,
				this.certStatus,
				this.thisUpdate
			});
			if (this.nextUpdate != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.nextUpdate)
				});
			}
			if (this.singleExtensions != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, this.singleExtensions)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002997 RID: 10647
		private readonly CertID certID;

		// Token: 0x04002998 RID: 10648
		private readonly CertStatus certStatus;

		// Token: 0x04002999 RID: 10649
		private readonly DerGeneralizedTime thisUpdate;

		// Token: 0x0400299A RID: 10650
		private readonly DerGeneralizedTime nextUpdate;

		// Token: 0x0400299B RID: 10651
		private readonly X509Extensions singleExtensions;
	}
}
