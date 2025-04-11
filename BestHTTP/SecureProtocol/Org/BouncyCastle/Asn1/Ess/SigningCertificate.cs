using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ess
{
	// Token: 0x02000729 RID: 1833
	public class SigningCertificate : Asn1Encodable
	{
		// Token: 0x060042A9 RID: 17065 RVA: 0x0018A6BC File Offset: 0x001888BC
		public static SigningCertificate GetInstance(object o)
		{
			if (o == null || o is SigningCertificate)
			{
				return (SigningCertificate)o;
			}
			if (o is Asn1Sequence)
			{
				return new SigningCertificate((Asn1Sequence)o);
			}
			throw new ArgumentException("unknown object in 'SigningCertificate' factory : " + Platform.GetTypeName(o) + ".");
		}

		// Token: 0x060042AA RID: 17066 RVA: 0x0018A70C File Offset: 0x0018890C
		public SigningCertificate(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.certs = Asn1Sequence.GetInstance(seq[0]);
			if (seq.Count > 1)
			{
				this.policies = Asn1Sequence.GetInstance(seq[1]);
			}
		}

		// Token: 0x060042AB RID: 17067 RVA: 0x0018A779 File Offset: 0x00188979
		public SigningCertificate(EssCertID essCertID)
		{
			this.certs = new DerSequence(essCertID);
		}

		// Token: 0x060042AC RID: 17068 RVA: 0x0018A790 File Offset: 0x00188990
		public EssCertID[] GetCerts()
		{
			EssCertID[] array = new EssCertID[this.certs.Count];
			for (int num = 0; num != this.certs.Count; num++)
			{
				array[num] = EssCertID.GetInstance(this.certs[num]);
			}
			return array;
		}

		// Token: 0x060042AD RID: 17069 RVA: 0x0018A7DC File Offset: 0x001889DC
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

		// Token: 0x060042AE RID: 17070 RVA: 0x0018A830 File Offset: 0x00188A30
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

		// Token: 0x04002ADB RID: 10971
		private Asn1Sequence certs;

		// Token: 0x04002ADC RID: 10972
		private Asn1Sequence policies;
	}
}
