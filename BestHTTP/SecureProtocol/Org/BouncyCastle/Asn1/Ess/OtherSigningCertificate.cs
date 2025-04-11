using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ess
{
	// Token: 0x02000728 RID: 1832
	[Obsolete("Use version in Asn1.Esf instead")]
	public class OtherSigningCertificate : Asn1Encodable
	{
		// Token: 0x060042A3 RID: 17059 RVA: 0x0018A500 File Offset: 0x00188700
		public static OtherSigningCertificate GetInstance(object o)
		{
			if (o == null || o is OtherSigningCertificate)
			{
				return (OtherSigningCertificate)o;
			}
			if (o is Asn1Sequence)
			{
				return new OtherSigningCertificate((Asn1Sequence)o);
			}
			throw new ArgumentException("unknown object in 'OtherSigningCertificate' factory : " + Platform.GetTypeName(o) + ".");
		}

		// Token: 0x060042A4 RID: 17060 RVA: 0x0018A550 File Offset: 0x00188750
		public OtherSigningCertificate(Asn1Sequence seq)
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

		// Token: 0x060042A5 RID: 17061 RVA: 0x0018A5BD File Offset: 0x001887BD
		public OtherSigningCertificate(OtherCertID otherCertID)
		{
			this.certs = new DerSequence(otherCertID);
		}

		// Token: 0x060042A6 RID: 17062 RVA: 0x0018A5D4 File Offset: 0x001887D4
		public OtherCertID[] GetCerts()
		{
			OtherCertID[] array = new OtherCertID[this.certs.Count];
			for (int num = 0; num != this.certs.Count; num++)
			{
				array[num] = OtherCertID.GetInstance(this.certs[num]);
			}
			return array;
		}

		// Token: 0x060042A7 RID: 17063 RVA: 0x0018A620 File Offset: 0x00188820
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

		// Token: 0x060042A8 RID: 17064 RVA: 0x0018A674 File Offset: 0x00188874
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

		// Token: 0x04002AD9 RID: 10969
		private Asn1Sequence certs;

		// Token: 0x04002ADA RID: 10970
		private Asn1Sequence policies;
	}
}
