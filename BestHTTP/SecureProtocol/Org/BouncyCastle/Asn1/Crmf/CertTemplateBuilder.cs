using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x02000754 RID: 1876
	public class CertTemplateBuilder
	{
		// Token: 0x060043C3 RID: 17347 RVA: 0x0018E9C7 File Offset: 0x0018CBC7
		public virtual CertTemplateBuilder SetVersion(int ver)
		{
			this.version = new DerInteger(ver);
			return this;
		}

		// Token: 0x060043C4 RID: 17348 RVA: 0x0018E9D6 File Offset: 0x0018CBD6
		public virtual CertTemplateBuilder SetSerialNumber(DerInteger ser)
		{
			this.serialNumber = ser;
			return this;
		}

		// Token: 0x060043C5 RID: 17349 RVA: 0x0018E9E0 File Offset: 0x0018CBE0
		public virtual CertTemplateBuilder SetSigningAlg(AlgorithmIdentifier aid)
		{
			this.signingAlg = aid;
			return this;
		}

		// Token: 0x060043C6 RID: 17350 RVA: 0x0018E9EA File Offset: 0x0018CBEA
		public virtual CertTemplateBuilder SetIssuer(X509Name name)
		{
			this.issuer = name;
			return this;
		}

		// Token: 0x060043C7 RID: 17351 RVA: 0x0018E9F4 File Offset: 0x0018CBF4
		public virtual CertTemplateBuilder SetValidity(OptionalValidity v)
		{
			this.validity = v;
			return this;
		}

		// Token: 0x060043C8 RID: 17352 RVA: 0x0018E9FE File Offset: 0x0018CBFE
		public virtual CertTemplateBuilder SetSubject(X509Name name)
		{
			this.subject = name;
			return this;
		}

		// Token: 0x060043C9 RID: 17353 RVA: 0x0018EA08 File Offset: 0x0018CC08
		public virtual CertTemplateBuilder SetPublicKey(SubjectPublicKeyInfo spki)
		{
			this.publicKey = spki;
			return this;
		}

		// Token: 0x060043CA RID: 17354 RVA: 0x0018EA12 File Offset: 0x0018CC12
		public virtual CertTemplateBuilder SetIssuerUID(DerBitString uid)
		{
			this.issuerUID = uid;
			return this;
		}

		// Token: 0x060043CB RID: 17355 RVA: 0x0018EA1C File Offset: 0x0018CC1C
		public virtual CertTemplateBuilder SetSubjectUID(DerBitString uid)
		{
			this.subjectUID = uid;
			return this;
		}

		// Token: 0x060043CC RID: 17356 RVA: 0x0018EA26 File Offset: 0x0018CC26
		public virtual CertTemplateBuilder SetExtensions(X509Extensions extens)
		{
			this.extensions = extens;
			return this;
		}

		// Token: 0x060043CD RID: 17357 RVA: 0x0018EA30 File Offset: 0x0018CC30
		public virtual CertTemplate Build()
		{
			Asn1EncodableVector v = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			this.AddOptional(v, 0, false, this.version);
			this.AddOptional(v, 1, false, this.serialNumber);
			this.AddOptional(v, 2, false, this.signingAlg);
			this.AddOptional(v, 3, true, this.issuer);
			this.AddOptional(v, 4, false, this.validity);
			this.AddOptional(v, 5, true, this.subject);
			this.AddOptional(v, 6, false, this.publicKey);
			this.AddOptional(v, 7, false, this.issuerUID);
			this.AddOptional(v, 8, false, this.subjectUID);
			this.AddOptional(v, 9, false, this.extensions);
			return CertTemplate.GetInstance(new DerSequence(v));
		}

		// Token: 0x060043CE RID: 17358 RVA: 0x0018EAEA File Offset: 0x0018CCEA
		private void AddOptional(Asn1EncodableVector v, int tagNo, bool isExplicit, Asn1Encodable obj)
		{
			if (obj != null)
			{
				v.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(isExplicit, tagNo, obj)
				});
			}
		}

		// Token: 0x04002B81 RID: 11137
		private DerInteger version;

		// Token: 0x04002B82 RID: 11138
		private DerInteger serialNumber;

		// Token: 0x04002B83 RID: 11139
		private AlgorithmIdentifier signingAlg;

		// Token: 0x04002B84 RID: 11140
		private X509Name issuer;

		// Token: 0x04002B85 RID: 11141
		private OptionalValidity validity;

		// Token: 0x04002B86 RID: 11142
		private X509Name subject;

		// Token: 0x04002B87 RID: 11143
		private SubjectPublicKeyInfo publicKey;

		// Token: 0x04002B88 RID: 11144
		private DerBitString issuerUID;

		// Token: 0x04002B89 RID: 11145
		private DerBitString subjectUID;

		// Token: 0x04002B8A RID: 11146
		private X509Extensions extensions;
	}
}
