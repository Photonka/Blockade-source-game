using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020006A5 RID: 1701
	public class V1TbsCertificateGenerator
	{
		// Token: 0x06003F10 RID: 16144 RVA: 0x0017BF6A File Offset: 0x0017A16A
		public void SetSerialNumber(DerInteger serialNumber)
		{
			this.serialNumber = serialNumber;
		}

		// Token: 0x06003F11 RID: 16145 RVA: 0x0017BF73 File Offset: 0x0017A173
		public void SetSignature(AlgorithmIdentifier signature)
		{
			this.signature = signature;
		}

		// Token: 0x06003F12 RID: 16146 RVA: 0x0017BF7C File Offset: 0x0017A17C
		public void SetIssuer(X509Name issuer)
		{
			this.issuer = issuer;
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x0017BF85 File Offset: 0x0017A185
		public void SetStartDate(Time startDate)
		{
			this.startDate = startDate;
		}

		// Token: 0x06003F14 RID: 16148 RVA: 0x0017BF8E File Offset: 0x0017A18E
		public void SetStartDate(DerUtcTime startDate)
		{
			this.startDate = new Time(startDate);
		}

		// Token: 0x06003F15 RID: 16149 RVA: 0x0017BF9C File Offset: 0x0017A19C
		public void SetEndDate(Time endDate)
		{
			this.endDate = endDate;
		}

		// Token: 0x06003F16 RID: 16150 RVA: 0x0017BFA5 File Offset: 0x0017A1A5
		public void SetEndDate(DerUtcTime endDate)
		{
			this.endDate = new Time(endDate);
		}

		// Token: 0x06003F17 RID: 16151 RVA: 0x0017BFB3 File Offset: 0x0017A1B3
		public void SetSubject(X509Name subject)
		{
			this.subject = subject;
		}

		// Token: 0x06003F18 RID: 16152 RVA: 0x0017BFBC File Offset: 0x0017A1BC
		public void SetSubjectPublicKeyInfo(SubjectPublicKeyInfo pubKeyInfo)
		{
			this.subjectPublicKeyInfo = pubKeyInfo;
		}

		// Token: 0x06003F19 RID: 16153 RVA: 0x0017BFC8 File Offset: 0x0017A1C8
		public TbsCertificateStructure GenerateTbsCertificate()
		{
			if (this.serialNumber == null || this.signature == null || this.issuer == null || this.startDate == null || this.endDate == null || this.subject == null || this.subjectPublicKeyInfo == null)
			{
				throw new InvalidOperationException("not all mandatory fields set in V1 TBScertificate generator");
			}
			return new TbsCertificateStructure(new DerSequence(new Asn1Encodable[]
			{
				this.serialNumber,
				this.signature,
				this.issuer,
				new DerSequence(new Asn1Encodable[]
				{
					this.startDate,
					this.endDate
				}),
				this.subject,
				this.subjectPublicKeyInfo
			}));
		}

		// Token: 0x040026FA RID: 9978
		internal DerTaggedObject version = new DerTaggedObject(0, new DerInteger(0));

		// Token: 0x040026FB RID: 9979
		internal DerInteger serialNumber;

		// Token: 0x040026FC RID: 9980
		internal AlgorithmIdentifier signature;

		// Token: 0x040026FD RID: 9981
		internal X509Name issuer;

		// Token: 0x040026FE RID: 9982
		internal Time startDate;

		// Token: 0x040026FF RID: 9983
		internal Time endDate;

		// Token: 0x04002700 RID: 9984
		internal X509Name subject;

		// Token: 0x04002701 RID: 9985
		internal SubjectPublicKeyInfo subjectPublicKeyInfo;
	}
}
