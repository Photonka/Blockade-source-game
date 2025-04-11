using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000695 RID: 1685
	public class PolicyQualifierInfo : Asn1Encodable
	{
		// Token: 0x06003E94 RID: 16020 RVA: 0x0017A9D4 File Offset: 0x00178BD4
		public PolicyQualifierInfo(DerObjectIdentifier policyQualifierId, Asn1Encodable qualifier)
		{
			this.policyQualifierId = policyQualifierId;
			this.qualifier = qualifier;
		}

		// Token: 0x06003E95 RID: 16021 RVA: 0x0017A9EA File Offset: 0x00178BEA
		public PolicyQualifierInfo(string cps)
		{
			this.policyQualifierId = PolicyQualifierID.IdQtCps;
			this.qualifier = new DerIA5String(cps);
		}

		// Token: 0x06003E96 RID: 16022 RVA: 0x0017AA0C File Offset: 0x00178C0C
		private PolicyQualifierInfo(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.policyQualifierId = DerObjectIdentifier.GetInstance(seq[0]);
			this.qualifier = seq[1];
		}

		// Token: 0x06003E97 RID: 16023 RVA: 0x0017AA67 File Offset: 0x00178C67
		public static PolicyQualifierInfo GetInstance(object obj)
		{
			if (obj is PolicyQualifierInfo)
			{
				return (PolicyQualifierInfo)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new PolicyQualifierInfo(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06003E98 RID: 16024 RVA: 0x0017AA88 File Offset: 0x00178C88
		public virtual DerObjectIdentifier PolicyQualifierId
		{
			get
			{
				return this.policyQualifierId;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06003E99 RID: 16025 RVA: 0x0017AA90 File Offset: 0x00178C90
		public virtual Asn1Encodable Qualifier
		{
			get
			{
				return this.qualifier;
			}
		}

		// Token: 0x06003E9A RID: 16026 RVA: 0x0017AA98 File Offset: 0x00178C98
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.policyQualifierId,
				this.qualifier
			});
		}

		// Token: 0x040026C6 RID: 9926
		private readonly DerObjectIdentifier policyQualifierId;

		// Token: 0x040026C7 RID: 9927
		private readonly Asn1Encodable qualifier;
	}
}
