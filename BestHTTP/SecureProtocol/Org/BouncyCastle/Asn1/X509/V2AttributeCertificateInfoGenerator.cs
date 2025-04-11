using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020006A6 RID: 1702
	public class V2AttributeCertificateInfoGenerator
	{
		// Token: 0x06003F1A RID: 16154 RVA: 0x0017C075 File Offset: 0x0017A275
		public V2AttributeCertificateInfoGenerator()
		{
			this.version = new DerInteger(1);
			this.attributes = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
		}

		// Token: 0x06003F1B RID: 16155 RVA: 0x0017C099 File Offset: 0x0017A299
		public void SetHolder(Holder holder)
		{
			this.holder = holder;
		}

		// Token: 0x06003F1C RID: 16156 RVA: 0x0017C0A2 File Offset: 0x0017A2A2
		public void AddAttribute(string oid, Asn1Encodable value)
		{
			this.attributes.Add(new Asn1Encodable[]
			{
				new AttributeX509(new DerObjectIdentifier(oid), new DerSet(value))
			});
		}

		// Token: 0x06003F1D RID: 16157 RVA: 0x0017C0C9 File Offset: 0x0017A2C9
		public void AddAttribute(AttributeX509 attribute)
		{
			this.attributes.Add(new Asn1Encodable[]
			{
				attribute
			});
		}

		// Token: 0x06003F1E RID: 16158 RVA: 0x0017C0E0 File Offset: 0x0017A2E0
		public void SetSerialNumber(DerInteger serialNumber)
		{
			this.serialNumber = serialNumber;
		}

		// Token: 0x06003F1F RID: 16159 RVA: 0x0017C0E9 File Offset: 0x0017A2E9
		public void SetSignature(AlgorithmIdentifier signature)
		{
			this.signature = signature;
		}

		// Token: 0x06003F20 RID: 16160 RVA: 0x0017C0F2 File Offset: 0x0017A2F2
		public void SetIssuer(AttCertIssuer issuer)
		{
			this.issuer = issuer;
		}

		// Token: 0x06003F21 RID: 16161 RVA: 0x0017C0FB File Offset: 0x0017A2FB
		public void SetStartDate(DerGeneralizedTime startDate)
		{
			this.startDate = startDate;
		}

		// Token: 0x06003F22 RID: 16162 RVA: 0x0017C104 File Offset: 0x0017A304
		public void SetEndDate(DerGeneralizedTime endDate)
		{
			this.endDate = endDate;
		}

		// Token: 0x06003F23 RID: 16163 RVA: 0x0017C10D File Offset: 0x0017A30D
		public void SetIssuerUniqueID(DerBitString issuerUniqueID)
		{
			this.issuerUniqueID = issuerUniqueID;
		}

		// Token: 0x06003F24 RID: 16164 RVA: 0x0017C116 File Offset: 0x0017A316
		public void SetExtensions(X509Extensions extensions)
		{
			this.extensions = extensions;
		}

		// Token: 0x06003F25 RID: 16165 RVA: 0x0017C120 File Offset: 0x0017A320
		public AttributeCertificateInfo GenerateAttributeCertificateInfo()
		{
			if (this.serialNumber == null || this.signature == null || this.issuer == null || this.startDate == null || this.endDate == null || this.holder == null || this.attributes == null)
			{
				throw new InvalidOperationException("not all mandatory fields set in V2 AttributeCertificateInfo generator");
			}
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.holder,
				this.issuer,
				this.signature,
				this.serialNumber
			});
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				new AttCertValidityPeriod(this.startDate, this.endDate)
			});
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				new DerSequence(this.attributes)
			});
			if (this.issuerUniqueID != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.issuerUniqueID
				});
			}
			if (this.extensions != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.extensions
				});
			}
			return AttributeCertificateInfo.GetInstance(new DerSequence(asn1EncodableVector));
		}

		// Token: 0x04002702 RID: 9986
		internal DerInteger version;

		// Token: 0x04002703 RID: 9987
		internal Holder holder;

		// Token: 0x04002704 RID: 9988
		internal AttCertIssuer issuer;

		// Token: 0x04002705 RID: 9989
		internal AlgorithmIdentifier signature;

		// Token: 0x04002706 RID: 9990
		internal DerInteger serialNumber;

		// Token: 0x04002707 RID: 9991
		internal Asn1EncodableVector attributes;

		// Token: 0x04002708 RID: 9992
		internal DerBitString issuerUniqueID;

		// Token: 0x04002709 RID: 9993
		internal X509Extensions extensions;

		// Token: 0x0400270A RID: 9994
		internal DerGeneralizedTime startDate;

		// Token: 0x0400270B RID: 9995
		internal DerGeneralizedTime endDate;
	}
}
