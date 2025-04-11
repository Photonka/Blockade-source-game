using System;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Pkix
{
	// Token: 0x0200029D RID: 669
	public class PkixBuilderParameters : PkixParameters
	{
		// Token: 0x060018A0 RID: 6304 RVA: 0x000BD610 File Offset: 0x000BB810
		public static PkixBuilderParameters GetInstance(PkixParameters pkixParams)
		{
			PkixBuilderParameters pkixBuilderParameters = new PkixBuilderParameters(pkixParams.GetTrustAnchors(), new X509CertStoreSelector(pkixParams.GetTargetCertConstraints()));
			pkixBuilderParameters.SetParams(pkixParams);
			return pkixBuilderParameters;
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x000BD62F File Offset: 0x000BB82F
		public PkixBuilderParameters(ISet trustAnchors, IX509Selector targetConstraints) : base(trustAnchors)
		{
			this.SetTargetCertConstraints(targetConstraints);
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x060018A2 RID: 6306 RVA: 0x000BD651 File Offset: 0x000BB851
		// (set) Token: 0x060018A3 RID: 6307 RVA: 0x000BD659 File Offset: 0x000BB859
		public virtual int MaxPathLength
		{
			get
			{
				return this.maxPathLength;
			}
			set
			{
				if (value < -1)
				{
					throw new InvalidParameterException("The maximum path length parameter can not be less than -1.");
				}
				this.maxPathLength = value;
			}
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x000BD671 File Offset: 0x000BB871
		public virtual ISet GetExcludedCerts()
		{
			return new HashSet(this.excludedCerts);
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x000BD67E File Offset: 0x000BB87E
		public virtual void SetExcludedCerts(ISet excludedCerts)
		{
			if (excludedCerts == null)
			{
				excludedCerts = new HashSet();
				return;
			}
			this.excludedCerts = new HashSet(excludedCerts);
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x000BD698 File Offset: 0x000BB898
		protected override void SetParams(PkixParameters parameters)
		{
			base.SetParams(parameters);
			if (parameters is PkixBuilderParameters)
			{
				PkixBuilderParameters pkixBuilderParameters = (PkixBuilderParameters)parameters;
				this.maxPathLength = pkixBuilderParameters.maxPathLength;
				this.excludedCerts = new HashSet(pkixBuilderParameters.excludedCerts);
			}
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x000BD6D8 File Offset: 0x000BB8D8
		public override object Clone()
		{
			PkixBuilderParameters pkixBuilderParameters = new PkixBuilderParameters(this.GetTrustAnchors(), this.GetTargetCertConstraints());
			pkixBuilderParameters.SetParams(this);
			return pkixBuilderParameters;
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x000BD6F4 File Offset: 0x000BB8F4
		public override string ToString()
		{
			string newLine = Platform.NewLine;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("PkixBuilderParameters [" + newLine);
			stringBuilder.Append(base.ToString());
			stringBuilder.Append("  Maximum Path Length: ");
			stringBuilder.Append(this.MaxPathLength);
			stringBuilder.Append(newLine + "]" + newLine);
			return stringBuilder.ToString();
		}

		// Token: 0x04001734 RID: 5940
		private int maxPathLength = 5;

		// Token: 0x04001735 RID: 5941
		private ISet excludedCerts = new HashSet();
	}
}
