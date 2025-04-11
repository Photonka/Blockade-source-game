using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ess
{
	// Token: 0x0200072A RID: 1834
	public class SigningCertificateV2 : Asn1Encodable
	{
		// Token: 0x060042AF RID: 17071 RVA: 0x0018A878 File Offset: 0x00188A78
		public static SigningCertificateV2 GetInstance(object o)
		{
			if (o == null || o is SigningCertificateV2)
			{
				return (SigningCertificateV2)o;
			}
			if (o is Asn1Sequence)
			{
				return new SigningCertificateV2((Asn1Sequence)o);
			}
			throw new ArgumentException("unknown object in 'SigningCertificateV2' factory : " + Platform.GetTypeName(o) + ".");
		}

		// Token: 0x060042B0 RID: 17072 RVA: 0x0018A8C8 File Offset: 0x00188AC8
		private SigningCertificateV2(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.certs = Asn1Sequence.GetInstance(seq[0].ToAsn1Object());
			if (seq.Count > 1)
			{
				this.policies = Asn1Sequence.GetInstance(seq[1].ToAsn1Object());
			}
		}

		// Token: 0x060042B1 RID: 17073 RVA: 0x0018A944 File Offset: 0x00188B44
		public SigningCertificateV2(EssCertIDv2 cert)
		{
			this.certs = new DerSequence(cert);
		}

		// Token: 0x060042B2 RID: 17074 RVA: 0x0018A958 File Offset: 0x00188B58
		public SigningCertificateV2(EssCertIDv2[] certs)
		{
			this.certs = new DerSequence(certs);
		}

		// Token: 0x060042B3 RID: 17075 RVA: 0x0018A97C File Offset: 0x00188B7C
		public SigningCertificateV2(EssCertIDv2[] certs, PolicyInformation[] policies)
		{
			this.certs = new DerSequence(certs);
			if (policies != null)
			{
				this.policies = new DerSequence(policies);
			}
		}

		// Token: 0x060042B4 RID: 17076 RVA: 0x0018A9B0 File Offset: 0x00188BB0
		public EssCertIDv2[] GetCerts()
		{
			EssCertIDv2[] array = new EssCertIDv2[this.certs.Count];
			for (int num = 0; num != this.certs.Count; num++)
			{
				array[num] = EssCertIDv2.GetInstance(this.certs[num]);
			}
			return array;
		}

		// Token: 0x060042B5 RID: 17077 RVA: 0x0018A9FC File Offset: 0x00188BFC
		public PolicyInformation[] GetPolicies()
		{
			if (this.policies == null)
			{
				return null;
			}
			PolicyInformation[] array = new PolicyInformation[this.policies.Count];
			for (int num = 0; num != this.policies.Count; num++)
			{
				array[num] = PolicyInformation.GetInstance(this.policies[num]);
			}
			return array;
		}

		// Token: 0x060042B6 RID: 17078 RVA: 0x0018AA50 File Offset: 0x00188C50
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.certs
			});
			if (this.policies != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.policies
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002ADD RID: 10973
		private readonly Asn1Sequence certs;

		// Token: 0x04002ADE RID: 10974
		private readonly Asn1Sequence policies;
	}
}
