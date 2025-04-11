using System;
using System.Text;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200067B RID: 1659
	public class CertificatePolicies : Asn1Encodable
	{
		// Token: 0x06003DD4 RID: 15828 RVA: 0x00177F54 File Offset: 0x00176154
		public static CertificatePolicies GetInstance(object obj)
		{
			if (obj == null || obj is CertificatePolicies)
			{
				return (CertificatePolicies)obj;
			}
			return new CertificatePolicies(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06003DD5 RID: 15829 RVA: 0x00177F73 File Offset: 0x00176173
		public static CertificatePolicies GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return CertificatePolicies.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06003DD6 RID: 15830 RVA: 0x00177F81 File Offset: 0x00176181
		public CertificatePolicies(PolicyInformation name)
		{
			this.policyInformation = new PolicyInformation[]
			{
				name
			};
		}

		// Token: 0x06003DD7 RID: 15831 RVA: 0x00177F99 File Offset: 0x00176199
		public CertificatePolicies(PolicyInformation[] policyInformation)
		{
			this.policyInformation = policyInformation;
		}

		// Token: 0x06003DD8 RID: 15832 RVA: 0x00177FA8 File Offset: 0x001761A8
		private CertificatePolicies(Asn1Sequence seq)
		{
			this.policyInformation = new PolicyInformation[seq.Count];
			for (int i = 0; i < seq.Count; i++)
			{
				this.policyInformation[i] = PolicyInformation.GetInstance(seq[i]);
			}
		}

		// Token: 0x06003DD9 RID: 15833 RVA: 0x00177FF1 File Offset: 0x001761F1
		public virtual PolicyInformation[] GetPolicyInformation()
		{
			return (PolicyInformation[])this.policyInformation.Clone();
		}

		// Token: 0x06003DDA RID: 15834 RVA: 0x00178004 File Offset: 0x00176204
		public override Asn1Object ToAsn1Object()
		{
			Asn1Encodable[] v = this.policyInformation;
			return new DerSequence(v);
		}

		// Token: 0x06003DDB RID: 15835 RVA: 0x00178020 File Offset: 0x00176220
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("CertificatePolicies:");
			if (this.policyInformation != null && this.policyInformation.Length != 0)
			{
				stringBuilder.Append(' ');
				stringBuilder.Append(this.policyInformation[0]);
				for (int i = 1; i < this.policyInformation.Length; i++)
				{
					stringBuilder.Append(", ");
					stringBuilder.Append(this.policyInformation[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400265A RID: 9818
		private readonly PolicyInformation[] policyInformation;
	}
}
