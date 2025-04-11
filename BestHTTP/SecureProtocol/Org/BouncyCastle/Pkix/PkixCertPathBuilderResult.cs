using System;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Pkix
{
	// Token: 0x020002A1 RID: 673
	public class PkixCertPathBuilderResult : PkixCertPathValidatorResult
	{
		// Token: 0x060018BC RID: 6332 RVA: 0x000BE0DF File Offset: 0x000BC2DF
		public PkixCertPathBuilderResult(PkixCertPath certPath, TrustAnchor trustAnchor, PkixPolicyNode policyTree, AsymmetricKeyParameter subjectPublicKey) : base(trustAnchor, policyTree, subjectPublicKey)
		{
			if (certPath == null)
			{
				throw new ArgumentNullException("certPath");
			}
			this.certPath = certPath;
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x060018BD RID: 6333 RVA: 0x000BE100 File Offset: 0x000BC300
		public PkixCertPath CertPath
		{
			get
			{
				return this.certPath;
			}
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x000BE108 File Offset: 0x000BC308
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SimplePKIXCertPathBuilderResult: [\n");
			stringBuilder.Append("  Certification Path: ").Append(this.CertPath).Append('\n');
			stringBuilder.Append("  Trust Anchor: ").Append(base.TrustAnchor.TrustedCert.IssuerDN.ToString()).Append('\n');
			stringBuilder.Append("  Subject Public Key: ").Append(base.SubjectPublicKey).Append("\n]");
			return stringBuilder.ToString();
		}

		// Token: 0x04001739 RID: 5945
		private PkixCertPath certPath;
	}
}
