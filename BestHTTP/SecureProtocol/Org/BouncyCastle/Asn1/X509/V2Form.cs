using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020006A7 RID: 1703
	public class V2Form : Asn1Encodable
	{
		// Token: 0x06003F26 RID: 16166 RVA: 0x0017C228 File Offset: 0x0017A428
		public static V2Form GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return V2Form.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06003F27 RID: 16167 RVA: 0x0017C236 File Offset: 0x0017A436
		public static V2Form GetInstance(object obj)
		{
			if (obj is V2Form)
			{
				return (V2Form)obj;
			}
			if (obj != null)
			{
				return new V2Form(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x06003F28 RID: 16168 RVA: 0x0017C257 File Offset: 0x0017A457
		public V2Form(GeneralNames issuerName) : this(issuerName, null, null)
		{
		}

		// Token: 0x06003F29 RID: 16169 RVA: 0x0017C262 File Offset: 0x0017A462
		public V2Form(GeneralNames issuerName, IssuerSerial baseCertificateID) : this(issuerName, baseCertificateID, null)
		{
		}

		// Token: 0x06003F2A RID: 16170 RVA: 0x0017C26D File Offset: 0x0017A46D
		public V2Form(GeneralNames issuerName, ObjectDigestInfo objectDigestInfo) : this(issuerName, null, objectDigestInfo)
		{
		}

		// Token: 0x06003F2B RID: 16171 RVA: 0x0017C278 File Offset: 0x0017A478
		public V2Form(GeneralNames issuerName, IssuerSerial baseCertificateID, ObjectDigestInfo objectDigestInfo)
		{
			this.issuerName = issuerName;
			this.baseCertificateID = baseCertificateID;
			this.objectDigestInfo = objectDigestInfo;
		}

		// Token: 0x06003F2C RID: 16172 RVA: 0x0017C298 File Offset: 0x0017A498
		private V2Form(Asn1Sequence seq)
		{
			if (seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			int num = 0;
			if (!(seq[0] is Asn1TaggedObject))
			{
				num++;
				this.issuerName = GeneralNames.GetInstance(seq[0]);
			}
			for (int num2 = num; num2 != seq.Count; num2++)
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[num2]);
				if (instance.TagNo == 0)
				{
					this.baseCertificateID = IssuerSerial.GetInstance(instance, false);
				}
				else
				{
					if (instance.TagNo != 1)
					{
						throw new ArgumentException("Bad tag number: " + instance.TagNo);
					}
					this.objectDigestInfo = ObjectDigestInfo.GetInstance(instance, false);
				}
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06003F2D RID: 16173 RVA: 0x0017C35D File Offset: 0x0017A55D
		public GeneralNames IssuerName
		{
			get
			{
				return this.issuerName;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06003F2E RID: 16174 RVA: 0x0017C365 File Offset: 0x0017A565
		public IssuerSerial BaseCertificateID
		{
			get
			{
				return this.baseCertificateID;
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06003F2F RID: 16175 RVA: 0x0017C36D File Offset: 0x0017A56D
		public ObjectDigestInfo ObjectDigestInfo
		{
			get
			{
				return this.objectDigestInfo;
			}
		}

		// Token: 0x06003F30 RID: 16176 RVA: 0x0017C378 File Offset: 0x0017A578
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.issuerName != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.issuerName
				});
			}
			if (this.baseCertificateID != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.baseCertificateID)
				});
			}
			if (this.objectDigestInfo != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.objectDigestInfo)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400270C RID: 9996
		internal GeneralNames issuerName;

		// Token: 0x0400270D RID: 9997
		internal IssuerSerial baseCertificateID;

		// Token: 0x0400270E RID: 9998
		internal ObjectDigestInfo objectDigestInfo;
	}
}
