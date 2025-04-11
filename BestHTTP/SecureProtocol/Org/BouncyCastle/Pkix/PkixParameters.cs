using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Date;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Pkix
{
	// Token: 0x020002AA RID: 682
	public class PkixParameters
	{
		// Token: 0x06001932 RID: 6450 RVA: 0x000C1AC8 File Offset: 0x000BFCC8
		public PkixParameters(ISet trustAnchors)
		{
			this.SetTrustAnchors(trustAnchors);
			this.initialPolicies = new HashSet();
			this.certPathCheckers = Platform.CreateArrayList();
			this.stores = Platform.CreateArrayList();
			this.additionalStores = Platform.CreateArrayList();
			this.trustedACIssuers = new HashSet();
			this.necessaryACAttributes = new HashSet();
			this.prohibitedACAttributes = new HashSet();
			this.attrCertCheckers = new HashSet();
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001933 RID: 6451 RVA: 0x000C1B48 File Offset: 0x000BFD48
		// (set) Token: 0x06001934 RID: 6452 RVA: 0x000C1B50 File Offset: 0x000BFD50
		public virtual bool IsRevocationEnabled
		{
			get
			{
				return this.revocationEnabled;
			}
			set
			{
				this.revocationEnabled = value;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001935 RID: 6453 RVA: 0x000C1B59 File Offset: 0x000BFD59
		// (set) Token: 0x06001936 RID: 6454 RVA: 0x000C1B61 File Offset: 0x000BFD61
		public virtual bool IsExplicitPolicyRequired
		{
			get
			{
				return this.explicitPolicyRequired;
			}
			set
			{
				this.explicitPolicyRequired = value;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06001937 RID: 6455 RVA: 0x000C1B6A File Offset: 0x000BFD6A
		// (set) Token: 0x06001938 RID: 6456 RVA: 0x000C1B72 File Offset: 0x000BFD72
		public virtual bool IsAnyPolicyInhibited
		{
			get
			{
				return this.anyPolicyInhibited;
			}
			set
			{
				this.anyPolicyInhibited = value;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06001939 RID: 6457 RVA: 0x000C1B7B File Offset: 0x000BFD7B
		// (set) Token: 0x0600193A RID: 6458 RVA: 0x000C1B83 File Offset: 0x000BFD83
		public virtual bool IsPolicyMappingInhibited
		{
			get
			{
				return this.policyMappingInhibited;
			}
			set
			{
				this.policyMappingInhibited = value;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x0600193B RID: 6459 RVA: 0x000C1B8C File Offset: 0x000BFD8C
		// (set) Token: 0x0600193C RID: 6460 RVA: 0x000C1B94 File Offset: 0x000BFD94
		public virtual bool IsPolicyQualifiersRejected
		{
			get
			{
				return this.policyQualifiersRejected;
			}
			set
			{
				this.policyQualifiersRejected = value;
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x0600193D RID: 6461 RVA: 0x000C1B9D File Offset: 0x000BFD9D
		// (set) Token: 0x0600193E RID: 6462 RVA: 0x000C1BA5 File Offset: 0x000BFDA5
		public virtual DateTimeObject Date
		{
			get
			{
				return this.date;
			}
			set
			{
				this.date = value;
			}
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x000C1BAE File Offset: 0x000BFDAE
		public virtual ISet GetTrustAnchors()
		{
			return new HashSet(this.trustAnchors);
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x000C1BBC File Offset: 0x000BFDBC
		public virtual void SetTrustAnchors(ISet tas)
		{
			if (tas == null)
			{
				throw new ArgumentNullException("value");
			}
			if (tas.IsEmpty)
			{
				throw new ArgumentException("non-empty set required", "value");
			}
			this.trustAnchors = new HashSet();
			foreach (object obj in tas)
			{
				TrustAnchor trustAnchor = (TrustAnchor)obj;
				if (trustAnchor != null)
				{
					this.trustAnchors.Add(trustAnchor);
				}
			}
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x000C1C4C File Offset: 0x000BFE4C
		public virtual X509CertStoreSelector GetTargetCertConstraints()
		{
			if (this.certSelector == null)
			{
				return null;
			}
			return (X509CertStoreSelector)this.certSelector.Clone();
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x000C1C68 File Offset: 0x000BFE68
		public virtual void SetTargetCertConstraints(IX509Selector selector)
		{
			if (selector == null)
			{
				this.certSelector = null;
				return;
			}
			this.certSelector = (IX509Selector)selector.Clone();
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x000C1C88 File Offset: 0x000BFE88
		public virtual ISet GetInitialPolicies()
		{
			ISet s = this.initialPolicies;
			if (this.initialPolicies == null)
			{
				s = new HashSet();
			}
			return new HashSet(s);
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x000C1CB0 File Offset: 0x000BFEB0
		public virtual void SetInitialPolicies(ISet initialPolicies)
		{
			this.initialPolicies = new HashSet();
			if (initialPolicies != null)
			{
				foreach (object obj in initialPolicies)
				{
					string text = (string)obj;
					if (text != null)
					{
						this.initialPolicies.Add(text);
					}
				}
			}
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x000C1D1C File Offset: 0x000BFF1C
		public virtual void SetCertPathCheckers(IList checkers)
		{
			this.certPathCheckers = Platform.CreateArrayList();
			if (checkers != null)
			{
				foreach (object obj in checkers)
				{
					PkixCertPathChecker pkixCertPathChecker = (PkixCertPathChecker)obj;
					this.certPathCheckers.Add(pkixCertPathChecker.Clone());
				}
			}
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x000C1D8C File Offset: 0x000BFF8C
		public virtual IList GetCertPathCheckers()
		{
			IList list = Platform.CreateArrayList();
			foreach (object obj in this.certPathCheckers)
			{
				PkixCertPathChecker pkixCertPathChecker = (PkixCertPathChecker)obj;
				list.Add(pkixCertPathChecker.Clone());
			}
			return list;
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x000C1DF4 File Offset: 0x000BFFF4
		public virtual void AddCertPathChecker(PkixCertPathChecker checker)
		{
			if (checker != null)
			{
				this.certPathCheckers.Add(checker.Clone());
			}
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x000C1E0B File Offset: 0x000C000B
		public virtual object Clone()
		{
			PkixParameters pkixParameters = new PkixParameters(this.GetTrustAnchors());
			pkixParameters.SetParams(this);
			return pkixParameters;
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x000C1E20 File Offset: 0x000C0020
		protected virtual void SetParams(PkixParameters parameters)
		{
			this.Date = parameters.Date;
			this.SetCertPathCheckers(parameters.GetCertPathCheckers());
			this.IsAnyPolicyInhibited = parameters.IsAnyPolicyInhibited;
			this.IsExplicitPolicyRequired = parameters.IsExplicitPolicyRequired;
			this.IsPolicyMappingInhibited = parameters.IsPolicyMappingInhibited;
			this.IsRevocationEnabled = parameters.IsRevocationEnabled;
			this.SetInitialPolicies(parameters.GetInitialPolicies());
			this.IsPolicyQualifiersRejected = parameters.IsPolicyQualifiersRejected;
			this.SetTargetCertConstraints(parameters.GetTargetCertConstraints());
			this.SetTrustAnchors(parameters.GetTrustAnchors());
			this.validityModel = parameters.validityModel;
			this.useDeltas = parameters.useDeltas;
			this.additionalLocationsEnabled = parameters.additionalLocationsEnabled;
			this.selector = ((parameters.selector == null) ? null : ((IX509Selector)parameters.selector.Clone()));
			this.stores = Platform.CreateArrayList(parameters.stores);
			this.additionalStores = Platform.CreateArrayList(parameters.additionalStores);
			this.trustedACIssuers = new HashSet(parameters.trustedACIssuers);
			this.prohibitedACAttributes = new HashSet(parameters.prohibitedACAttributes);
			this.necessaryACAttributes = new HashSet(parameters.necessaryACAttributes);
			this.attrCertCheckers = new HashSet(parameters.attrCertCheckers);
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x0600194A RID: 6474 RVA: 0x000C1F50 File Offset: 0x000C0150
		// (set) Token: 0x0600194B RID: 6475 RVA: 0x000C1F58 File Offset: 0x000C0158
		public virtual bool IsUseDeltasEnabled
		{
			get
			{
				return this.useDeltas;
			}
			set
			{
				this.useDeltas = value;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x0600194C RID: 6476 RVA: 0x000C1F61 File Offset: 0x000C0161
		// (set) Token: 0x0600194D RID: 6477 RVA: 0x000C1F69 File Offset: 0x000C0169
		public virtual int ValidityModel
		{
			get
			{
				return this.validityModel;
			}
			set
			{
				this.validityModel = value;
			}
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x000C1F74 File Offset: 0x000C0174
		public virtual void SetStores(IList stores)
		{
			if (stores == null)
			{
				this.stores = Platform.CreateArrayList();
				return;
			}
			using (IEnumerator enumerator = stores.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!(enumerator.Current is IX509Store))
					{
						throw new InvalidCastException("All elements of list must be of type " + typeof(IX509Store).FullName);
					}
				}
			}
			this.stores = Platform.CreateArrayList(stores);
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x000C1FFC File Offset: 0x000C01FC
		public virtual void AddStore(IX509Store store)
		{
			if (store != null)
			{
				this.stores.Add(store);
			}
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x000C200E File Offset: 0x000C020E
		public virtual void AddAdditionalStore(IX509Store store)
		{
			if (store != null)
			{
				this.additionalStores.Add(store);
			}
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x000C2020 File Offset: 0x000C0220
		public virtual IList GetAdditionalStores()
		{
			return Platform.CreateArrayList(this.additionalStores);
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x000C202D File Offset: 0x000C022D
		public virtual IList GetStores()
		{
			return Platform.CreateArrayList(this.stores);
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x000C203A File Offset: 0x000C023A
		public virtual bool IsAdditionalLocationsEnabled
		{
			get
			{
				return this.additionalLocationsEnabled;
			}
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x000C2042 File Offset: 0x000C0242
		public virtual void SetAdditionalLocationsEnabled(bool enabled)
		{
			this.additionalLocationsEnabled = enabled;
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x000C204B File Offset: 0x000C024B
		public virtual IX509Selector GetTargetConstraints()
		{
			if (this.selector != null)
			{
				return (IX509Selector)this.selector.Clone();
			}
			return null;
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x000C2067 File Offset: 0x000C0267
		public virtual void SetTargetConstraints(IX509Selector selector)
		{
			if (selector != null)
			{
				this.selector = (IX509Selector)selector.Clone();
				return;
			}
			this.selector = null;
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x000C2085 File Offset: 0x000C0285
		public virtual ISet GetTrustedACIssuers()
		{
			return new HashSet(this.trustedACIssuers);
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x000C2094 File Offset: 0x000C0294
		public virtual void SetTrustedACIssuers(ISet trustedACIssuers)
		{
			if (trustedACIssuers == null)
			{
				this.trustedACIssuers = new HashSet();
				return;
			}
			using (IEnumerator enumerator = trustedACIssuers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!(enumerator.Current is TrustAnchor))
					{
						throw new InvalidCastException("All elements of set must be of type " + typeof(TrustAnchor).FullName + ".");
					}
				}
			}
			this.trustedACIssuers = new HashSet(trustedACIssuers);
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x000C2124 File Offset: 0x000C0324
		public virtual ISet GetNecessaryACAttributes()
		{
			return new HashSet(this.necessaryACAttributes);
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x000C2134 File Offset: 0x000C0334
		public virtual void SetNecessaryACAttributes(ISet necessaryACAttributes)
		{
			if (necessaryACAttributes == null)
			{
				this.necessaryACAttributes = new HashSet();
				return;
			}
			using (IEnumerator enumerator = necessaryACAttributes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!(enumerator.Current is string))
					{
						throw new InvalidCastException("All elements of set must be of type string.");
					}
				}
			}
			this.necessaryACAttributes = new HashSet(necessaryACAttributes);
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x000C21A8 File Offset: 0x000C03A8
		public virtual ISet GetProhibitedACAttributes()
		{
			return new HashSet(this.prohibitedACAttributes);
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x000C21B8 File Offset: 0x000C03B8
		public virtual void SetProhibitedACAttributes(ISet prohibitedACAttributes)
		{
			if (prohibitedACAttributes == null)
			{
				this.prohibitedACAttributes = new HashSet();
				return;
			}
			using (IEnumerator enumerator = prohibitedACAttributes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!(enumerator.Current is string))
					{
						throw new InvalidCastException("All elements of set must be of type string.");
					}
				}
			}
			this.prohibitedACAttributes = new HashSet(prohibitedACAttributes);
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x000C222C File Offset: 0x000C042C
		public virtual ISet GetAttrCertCheckers()
		{
			return new HashSet(this.attrCertCheckers);
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x000C223C File Offset: 0x000C043C
		public virtual void SetAttrCertCheckers(ISet attrCertCheckers)
		{
			if (attrCertCheckers == null)
			{
				this.attrCertCheckers = new HashSet();
				return;
			}
			using (IEnumerator enumerator = attrCertCheckers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!(enumerator.Current is PkixAttrCertChecker))
					{
						throw new InvalidCastException("All elements of set must be of type " + typeof(PkixAttrCertChecker).FullName + ".");
					}
				}
			}
			this.attrCertCheckers = new HashSet(attrCertCheckers);
		}

		// Token: 0x04001750 RID: 5968
		public const int PkixValidityModel = 0;

		// Token: 0x04001751 RID: 5969
		public const int ChainValidityModel = 1;

		// Token: 0x04001752 RID: 5970
		private ISet trustAnchors;

		// Token: 0x04001753 RID: 5971
		private DateTimeObject date;

		// Token: 0x04001754 RID: 5972
		private IList certPathCheckers;

		// Token: 0x04001755 RID: 5973
		private bool revocationEnabled = true;

		// Token: 0x04001756 RID: 5974
		private ISet initialPolicies;

		// Token: 0x04001757 RID: 5975
		private bool explicitPolicyRequired;

		// Token: 0x04001758 RID: 5976
		private bool anyPolicyInhibited;

		// Token: 0x04001759 RID: 5977
		private bool policyMappingInhibited;

		// Token: 0x0400175A RID: 5978
		private bool policyQualifiersRejected = true;

		// Token: 0x0400175B RID: 5979
		private IX509Selector certSelector;

		// Token: 0x0400175C RID: 5980
		private IList stores;

		// Token: 0x0400175D RID: 5981
		private IX509Selector selector;

		// Token: 0x0400175E RID: 5982
		private bool additionalLocationsEnabled;

		// Token: 0x0400175F RID: 5983
		private IList additionalStores;

		// Token: 0x04001760 RID: 5984
		private ISet trustedACIssuers;

		// Token: 0x04001761 RID: 5985
		private ISet necessaryACAttributes;

		// Token: 0x04001762 RID: 5986
		private ISet prohibitedACAttributes;

		// Token: 0x04001763 RID: 5987
		private ISet attrCertCheckers;

		// Token: 0x04001764 RID: 5988
		private int validityModel;

		// Token: 0x04001765 RID: 5989
		private bool useDeltas;
	}
}
