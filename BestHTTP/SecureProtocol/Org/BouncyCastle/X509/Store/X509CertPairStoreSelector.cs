using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store
{
	// Token: 0x02000240 RID: 576
	public class X509CertPairStoreSelector : IX509Selector, ICloneable
	{
		// Token: 0x0600154D RID: 5453 RVA: 0x000B07AC File Offset: 0x000AE9AC
		private static X509CertStoreSelector CloneSelector(X509CertStoreSelector s)
		{
			if (s != null)
			{
				return (X509CertStoreSelector)s.Clone();
			}
			return null;
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x00023EF4 File Offset: 0x000220F4
		public X509CertPairStoreSelector()
		{
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x000B07BE File Offset: 0x000AE9BE
		private X509CertPairStoreSelector(X509CertPairStoreSelector o)
		{
			this.certPair = o.CertPair;
			this.forwardSelector = o.ForwardSelector;
			this.reverseSelector = o.ReverseSelector;
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x000B07EA File Offset: 0x000AE9EA
		// (set) Token: 0x06001551 RID: 5457 RVA: 0x000B07F2 File Offset: 0x000AE9F2
		public X509CertificatePair CertPair
		{
			get
			{
				return this.certPair;
			}
			set
			{
				this.certPair = value;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x000B07FB File Offset: 0x000AE9FB
		// (set) Token: 0x06001553 RID: 5459 RVA: 0x000B0808 File Offset: 0x000AEA08
		public X509CertStoreSelector ForwardSelector
		{
			get
			{
				return X509CertPairStoreSelector.CloneSelector(this.forwardSelector);
			}
			set
			{
				this.forwardSelector = X509CertPairStoreSelector.CloneSelector(value);
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x000B0816 File Offset: 0x000AEA16
		// (set) Token: 0x06001555 RID: 5461 RVA: 0x000B0823 File Offset: 0x000AEA23
		public X509CertStoreSelector ReverseSelector
		{
			get
			{
				return X509CertPairStoreSelector.CloneSelector(this.reverseSelector);
			}
			set
			{
				this.reverseSelector = X509CertPairStoreSelector.CloneSelector(value);
			}
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x000B0834 File Offset: 0x000AEA34
		public bool Match(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			X509CertificatePair x509CertificatePair = obj as X509CertificatePair;
			return x509CertificatePair != null && (this.certPair == null || this.certPair.Equals(x509CertificatePair)) && (this.forwardSelector == null || this.forwardSelector.Match(x509CertificatePair.Forward)) && (this.reverseSelector == null || this.reverseSelector.Match(x509CertificatePair.Reverse));
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x000B08AE File Offset: 0x000AEAAE
		public object Clone()
		{
			return new X509CertPairStoreSelector(this);
		}

		// Token: 0x04001528 RID: 5416
		private X509CertificatePair certPair;

		// Token: 0x04001529 RID: 5417
		private X509CertStoreSelector forwardSelector;

		// Token: 0x0400152A RID: 5418
		private X509CertStoreSelector reverseSelector;
	}
}
