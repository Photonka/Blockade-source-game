using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007B9 RID: 1977
	public class RevRepContentBuilder
	{
		// Token: 0x060046AC RID: 18092 RVA: 0x001967C4 File Offset: 0x001949C4
		public virtual RevRepContentBuilder Add(PkiStatusInfo status)
		{
			this.status.Add(new Asn1Encodable[]
			{
				status
			});
			return this;
		}

		// Token: 0x060046AD RID: 18093 RVA: 0x001967DC File Offset: 0x001949DC
		public virtual RevRepContentBuilder Add(PkiStatusInfo status, CertId certId)
		{
			if (this.status.Count != this.revCerts.Count)
			{
				throw new InvalidOperationException("status and revCerts sequence must be in common order");
			}
			this.status.Add(new Asn1Encodable[]
			{
				status
			});
			this.revCerts.Add(new Asn1Encodable[]
			{
				certId
			});
			return this;
		}

		// Token: 0x060046AE RID: 18094 RVA: 0x00196837 File Offset: 0x00194A37
		public virtual RevRepContentBuilder AddCrl(CertificateList crl)
		{
			this.crls.Add(new Asn1Encodable[]
			{
				crl
			});
			return this;
		}

		// Token: 0x060046AF RID: 18095 RVA: 0x00196850 File Offset: 0x00194A50
		public virtual RevRepContent Build()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				new DerSequence(this.status)
			});
			if (this.revCerts.Count != 0)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, new DerSequence(this.revCerts))
				});
			}
			if (this.crls.Count != 0)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, new DerSequence(this.crls))
				});
			}
			return RevRepContent.GetInstance(new DerSequence(asn1EncodableVector));
		}

		// Token: 0x04002D26 RID: 11558
		private readonly Asn1EncodableVector status = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());

		// Token: 0x04002D27 RID: 11559
		private readonly Asn1EncodableVector revCerts = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());

		// Token: 0x04002D28 RID: 11560
		private readonly Asn1EncodableVector crls = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
	}
}
