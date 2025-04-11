using System;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Pkix
{
	// Token: 0x020002A5 RID: 677
	public class PkixCertPathValidatorResult
	{
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x000BE8CE File Offset: 0x000BCACE
		public PkixPolicyNode PolicyTree
		{
			get
			{
				return this.policyTree;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x000BE8D6 File Offset: 0x000BCAD6
		public TrustAnchor TrustAnchor
		{
			get
			{
				return this.trustAnchor;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x060018D1 RID: 6353 RVA: 0x000BE8DE File Offset: 0x000BCADE
		public AsymmetricKeyParameter SubjectPublicKey
		{
			get
			{
				return this.subjectPublicKey;
			}
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x000BE8E6 File Offset: 0x000BCAE6
		public PkixCertPathValidatorResult(TrustAnchor trustAnchor, PkixPolicyNode policyTree, AsymmetricKeyParameter subjectPublicKey)
		{
			if (subjectPublicKey == null)
			{
				throw new NullReferenceException("subjectPublicKey must be non-null");
			}
			if (trustAnchor == null)
			{
				throw new NullReferenceException("trustAnchor must be non-null");
			}
			this.trustAnchor = trustAnchor;
			this.policyTree = policyTree;
			this.subjectPublicKey = subjectPublicKey;
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x000BE91F File Offset: 0x000BCB1F
		public object Clone()
		{
			return new PkixCertPathValidatorResult(this.TrustAnchor, this.PolicyTree, this.SubjectPublicKey);
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x000BE938 File Offset: 0x000BCB38
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PKIXCertPathValidatorResult: [ \n");
			stringBuilder.Append("  Trust Anchor: ").Append(this.TrustAnchor).Append('\n');
			stringBuilder.Append("  Policy Tree: ").Append(this.PolicyTree).Append('\n');
			stringBuilder.Append("  Subject Public Key: ").Append(this.SubjectPublicKey).Append("\n]");
			return stringBuilder.ToString();
		}

		// Token: 0x0400173D RID: 5949
		private TrustAnchor trustAnchor;

		// Token: 0x0400173E RID: 5950
		private PkixPolicyNode policyTree;

		// Token: 0x0400173F RID: 5951
		private AsymmetricKeyParameter subjectPublicKey;
	}
}
