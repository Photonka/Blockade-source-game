using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000692 RID: 1682
	public class PolicyInformation : Asn1Encodable
	{
		// Token: 0x06003E87 RID: 16007 RVA: 0x0017A7DC File Offset: 0x001789DC
		private PolicyInformation(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.policyIdentifier = DerObjectIdentifier.GetInstance(seq[0]);
			if (seq.Count > 1)
			{
				this.policyQualifiers = Asn1Sequence.GetInstance(seq[1]);
			}
		}

		// Token: 0x06003E88 RID: 16008 RVA: 0x0017A849 File Offset: 0x00178A49
		public PolicyInformation(DerObjectIdentifier policyIdentifier)
		{
			this.policyIdentifier = policyIdentifier;
		}

		// Token: 0x06003E89 RID: 16009 RVA: 0x0017A858 File Offset: 0x00178A58
		public PolicyInformation(DerObjectIdentifier policyIdentifier, Asn1Sequence policyQualifiers)
		{
			this.policyIdentifier = policyIdentifier;
			this.policyQualifiers = policyQualifiers;
		}

		// Token: 0x06003E8A RID: 16010 RVA: 0x0017A86E File Offset: 0x00178A6E
		public static PolicyInformation GetInstance(object obj)
		{
			if (obj == null || obj is PolicyInformation)
			{
				return (PolicyInformation)obj;
			}
			return new PolicyInformation(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06003E8B RID: 16011 RVA: 0x0017A88D File Offset: 0x00178A8D
		public DerObjectIdentifier PolicyIdentifier
		{
			get
			{
				return this.policyIdentifier;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06003E8C RID: 16012 RVA: 0x0017A895 File Offset: 0x00178A95
		public Asn1Sequence PolicyQualifiers
		{
			get
			{
				return this.policyQualifiers;
			}
		}

		// Token: 0x06003E8D RID: 16013 RVA: 0x0017A8A0 File Offset: 0x00178AA0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.policyIdentifier
			});
			if (this.policyQualifiers != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.policyQualifiers
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040026C0 RID: 9920
		private readonly DerObjectIdentifier policyIdentifier;

		// Token: 0x040026C1 RID: 9921
		private readonly Asn1Sequence policyQualifiers;
	}
}
