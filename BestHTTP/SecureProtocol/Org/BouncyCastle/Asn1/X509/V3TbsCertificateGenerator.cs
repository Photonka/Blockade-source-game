using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020006A9 RID: 1705
	public class V3TbsCertificateGenerator
	{
		// Token: 0x06003F40 RID: 16192 RVA: 0x0017C6D7 File Offset: 0x0017A8D7
		public void SetSerialNumber(DerInteger serialNumber)
		{
			this.serialNumber = serialNumber;
		}

		// Token: 0x06003F41 RID: 16193 RVA: 0x0017C6E0 File Offset: 0x0017A8E0
		public void SetSignature(AlgorithmIdentifier signature)
		{
			this.signature = signature;
		}

		// Token: 0x06003F42 RID: 16194 RVA: 0x0017C6E9 File Offset: 0x0017A8E9
		public void SetIssuer(X509Name issuer)
		{
			this.issuer = issuer;
		}

		// Token: 0x06003F43 RID: 16195 RVA: 0x0017C6F2 File Offset: 0x0017A8F2
		public void SetStartDate(DerUtcTime startDate)
		{
			this.startDate = new Time(startDate);
		}

		// Token: 0x06003F44 RID: 16196 RVA: 0x0017C700 File Offset: 0x0017A900
		public void SetStartDate(Time startDate)
		{
			this.startDate = startDate;
		}

		// Token: 0x06003F45 RID: 16197 RVA: 0x0017C709 File Offset: 0x0017A909
		public void SetEndDate(DerUtcTime endDate)
		{
			this.endDate = new Time(endDate);
		}

		// Token: 0x06003F46 RID: 16198 RVA: 0x0017C717 File Offset: 0x0017A917
		public void SetEndDate(Time endDate)
		{
			this.endDate = endDate;
		}

		// Token: 0x06003F47 RID: 16199 RVA: 0x0017C720 File Offset: 0x0017A920
		public void SetSubject(X509Name subject)
		{
			this.subject = subject;
		}

		// Token: 0x06003F48 RID: 16200 RVA: 0x0017C729 File Offset: 0x0017A929
		public void SetIssuerUniqueID(DerBitString uniqueID)
		{
			this.issuerUniqueID = uniqueID;
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x0017C732 File Offset: 0x0017A932
		public void SetSubjectUniqueID(DerBitString uniqueID)
		{
			this.subjectUniqueID = uniqueID;
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x0017C73B File Offset: 0x0017A93B
		public void SetSubjectPublicKeyInfo(SubjectPublicKeyInfo pubKeyInfo)
		{
			this.subjectPublicKeyInfo = pubKeyInfo;
		}

		// Token: 0x06003F4B RID: 16203 RVA: 0x0017C744 File Offset: 0x0017A944
		public void SetExtensions(X509Extensions extensions)
		{
			this.extensions = extensions;
			if (extensions != null)
			{
				X509Extension extension = extensions.GetExtension(X509Extensions.SubjectAlternativeName);
				if (extension != null && extension.IsCritical)
				{
					this.altNamePresentAndCritical = true;
				}
			}
		}

		// Token: 0x06003F4C RID: 16204 RVA: 0x0017C77C File Offset: 0x0017A97C
		public TbsCertificateStructure GenerateTbsCertificate()
		{
			if (this.serialNumber == null || this.signature == null || this.issuer == null || this.startDate == null || this.endDate == null || (this.subject == null && !this.altNamePresentAndCritical) || this.subjectPublicKeyInfo == null)
			{
				throw new InvalidOperationException("not all mandatory fields set in V3 TBScertificate generator");
			}
			DerSequence derSequence = new DerSequence(new Asn1Encodable[]
			{
				this.startDate,
				this.endDate
			});
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.serialNumber,
				this.signature,
				this.issuer,
				derSequence
			});
			if (this.subject != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.subject
				});
			}
			else
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					DerSequence.Empty
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.subjectPublicKeyInfo
			});
			if (this.issuerUniqueID != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.issuerUniqueID)
				});
			}
			if (this.subjectUniqueID != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 2, this.subjectUniqueID)
				});
			}
			if (this.extensions != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(3, this.extensions)
				});
			}
			return new TbsCertificateStructure(new DerSequence(asn1EncodableVector));
		}

		// Token: 0x04002716 RID: 10006
		internal DerTaggedObject version = new DerTaggedObject(0, new DerInteger(2));

		// Token: 0x04002717 RID: 10007
		internal DerInteger serialNumber;

		// Token: 0x04002718 RID: 10008
		internal AlgorithmIdentifier signature;

		// Token: 0x04002719 RID: 10009
		internal X509Name issuer;

		// Token: 0x0400271A RID: 10010
		internal Time startDate;

		// Token: 0x0400271B RID: 10011
		internal Time endDate;

		// Token: 0x0400271C RID: 10012
		internal X509Name subject;

		// Token: 0x0400271D RID: 10013
		internal SubjectPublicKeyInfo subjectPublicKeyInfo;

		// Token: 0x0400271E RID: 10014
		internal X509Extensions extensions;

		// Token: 0x0400271F RID: 10015
		private bool altNamePresentAndCritical;

		// Token: 0x04002720 RID: 10016
		private DerBitString issuerUniqueID;

		// Token: 0x04002721 RID: 10017
		private DerBitString subjectUniqueID;
	}
}
