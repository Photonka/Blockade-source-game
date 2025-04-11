using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Date;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Extension;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store
{
	// Token: 0x02000244 RID: 580
	public class X509CrlStoreSelector : IX509Selector, ICloneable
	{
		// Token: 0x06001586 RID: 5510 RVA: 0x00023EF4 File Offset: 0x000220F4
		public X509CrlStoreSelector()
		{
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x000B0F84 File Offset: 0x000AF184
		public X509CrlStoreSelector(X509CrlStoreSelector o)
		{
			this.certificateChecking = o.CertificateChecking;
			this.dateAndTime = o.DateAndTime;
			this.issuers = o.Issuers;
			this.maxCrlNumber = o.MaxCrlNumber;
			this.minCrlNumber = o.MinCrlNumber;
			this.deltaCrlIndicatorEnabled = o.DeltaCrlIndicatorEnabled;
			this.completeCrlEnabled = o.CompleteCrlEnabled;
			this.maxBaseCrlNumber = o.MaxBaseCrlNumber;
			this.attrCertChecking = o.AttrCertChecking;
			this.issuingDistributionPointEnabled = o.IssuingDistributionPointEnabled;
			this.issuingDistributionPoint = o.IssuingDistributionPoint;
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x000B101B File Offset: 0x000AF21B
		public virtual object Clone()
		{
			return new X509CrlStoreSelector(this);
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06001589 RID: 5513 RVA: 0x000B1023 File Offset: 0x000AF223
		// (set) Token: 0x0600158A RID: 5514 RVA: 0x000B102B File Offset: 0x000AF22B
		public X509Certificate CertificateChecking
		{
			get
			{
				return this.certificateChecking;
			}
			set
			{
				this.certificateChecking = value;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x000B1034 File Offset: 0x000AF234
		// (set) Token: 0x0600158C RID: 5516 RVA: 0x000B103C File Offset: 0x000AF23C
		public DateTimeObject DateAndTime
		{
			get
			{
				return this.dateAndTime;
			}
			set
			{
				this.dateAndTime = value;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x0600158D RID: 5517 RVA: 0x000B1045 File Offset: 0x000AF245
		// (set) Token: 0x0600158E RID: 5518 RVA: 0x000B1052 File Offset: 0x000AF252
		public ICollection Issuers
		{
			get
			{
				return Platform.CreateArrayList(this.issuers);
			}
			set
			{
				this.issuers = Platform.CreateArrayList(value);
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x000B1060 File Offset: 0x000AF260
		// (set) Token: 0x06001590 RID: 5520 RVA: 0x000B1068 File Offset: 0x000AF268
		public BigInteger MaxCrlNumber
		{
			get
			{
				return this.maxCrlNumber;
			}
			set
			{
				this.maxCrlNumber = value;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x000B1071 File Offset: 0x000AF271
		// (set) Token: 0x06001592 RID: 5522 RVA: 0x000B1079 File Offset: 0x000AF279
		public BigInteger MinCrlNumber
		{
			get
			{
				return this.minCrlNumber;
			}
			set
			{
				this.minCrlNumber = value;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06001593 RID: 5523 RVA: 0x000B1082 File Offset: 0x000AF282
		// (set) Token: 0x06001594 RID: 5524 RVA: 0x000B108A File Offset: 0x000AF28A
		public IX509AttributeCertificate AttrCertChecking
		{
			get
			{
				return this.attrCertChecking;
			}
			set
			{
				this.attrCertChecking = value;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06001595 RID: 5525 RVA: 0x000B1093 File Offset: 0x000AF293
		// (set) Token: 0x06001596 RID: 5526 RVA: 0x000B109B File Offset: 0x000AF29B
		public bool CompleteCrlEnabled
		{
			get
			{
				return this.completeCrlEnabled;
			}
			set
			{
				this.completeCrlEnabled = value;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06001597 RID: 5527 RVA: 0x000B10A4 File Offset: 0x000AF2A4
		// (set) Token: 0x06001598 RID: 5528 RVA: 0x000B10AC File Offset: 0x000AF2AC
		public bool DeltaCrlIndicatorEnabled
		{
			get
			{
				return this.deltaCrlIndicatorEnabled;
			}
			set
			{
				this.deltaCrlIndicatorEnabled = value;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06001599 RID: 5529 RVA: 0x000B10B5 File Offset: 0x000AF2B5
		// (set) Token: 0x0600159A RID: 5530 RVA: 0x000B10C2 File Offset: 0x000AF2C2
		public byte[] IssuingDistributionPoint
		{
			get
			{
				return Arrays.Clone(this.issuingDistributionPoint);
			}
			set
			{
				this.issuingDistributionPoint = Arrays.Clone(value);
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x000B10D0 File Offset: 0x000AF2D0
		// (set) Token: 0x0600159C RID: 5532 RVA: 0x000B10D8 File Offset: 0x000AF2D8
		public bool IssuingDistributionPointEnabled
		{
			get
			{
				return this.issuingDistributionPointEnabled;
			}
			set
			{
				this.issuingDistributionPointEnabled = value;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x000B10E1 File Offset: 0x000AF2E1
		// (set) Token: 0x0600159E RID: 5534 RVA: 0x000B10E9 File Offset: 0x000AF2E9
		public BigInteger MaxBaseCrlNumber
		{
			get
			{
				return this.maxBaseCrlNumber;
			}
			set
			{
				this.maxBaseCrlNumber = value;
			}
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x000B10F4 File Offset: 0x000AF2F4
		public virtual bool Match(object obj)
		{
			X509Crl x509Crl = obj as X509Crl;
			if (x509Crl == null)
			{
				return false;
			}
			if (this.dateAndTime != null)
			{
				DateTime value = this.dateAndTime.Value;
				DateTime thisUpdate = x509Crl.ThisUpdate;
				DateTimeObject nextUpdate = x509Crl.NextUpdate;
				if (value.CompareTo(thisUpdate) < 0 || nextUpdate == null || value.CompareTo(nextUpdate.Value) >= 0)
				{
					return false;
				}
			}
			if (this.issuers != null)
			{
				X509Name issuerDN = x509Crl.IssuerDN;
				bool flag = false;
				using (IEnumerator enumerator = this.issuers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((X509Name)enumerator.Current).Equivalent(issuerDN, true))
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			if (this.maxCrlNumber != null || this.minCrlNumber != null)
			{
				Asn1OctetString extensionValue = x509Crl.GetExtensionValue(X509Extensions.CrlNumber);
				if (extensionValue == null)
				{
					return false;
				}
				BigInteger positiveValue = DerInteger.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue)).PositiveValue;
				if (this.maxCrlNumber != null && positiveValue.CompareTo(this.maxCrlNumber) > 0)
				{
					return false;
				}
				if (this.minCrlNumber != null && positiveValue.CompareTo(this.minCrlNumber) < 0)
				{
					return false;
				}
			}
			DerInteger derInteger = null;
			try
			{
				Asn1OctetString extensionValue2 = x509Crl.GetExtensionValue(X509Extensions.DeltaCrlIndicator);
				if (extensionValue2 != null)
				{
					derInteger = DerInteger.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue2));
				}
			}
			catch (Exception)
			{
				return false;
			}
			if (derInteger == null)
			{
				if (this.DeltaCrlIndicatorEnabled)
				{
					return false;
				}
			}
			else
			{
				if (this.CompleteCrlEnabled)
				{
					return false;
				}
				if (this.maxBaseCrlNumber != null && derInteger.PositiveValue.CompareTo(this.maxBaseCrlNumber) > 0)
				{
					return false;
				}
			}
			if (this.issuingDistributionPointEnabled)
			{
				Asn1OctetString extensionValue3 = x509Crl.GetExtensionValue(X509Extensions.IssuingDistributionPoint);
				if (this.issuingDistributionPoint == null)
				{
					if (extensionValue3 != null)
					{
						return false;
					}
				}
				else if (!Arrays.AreEqual(extensionValue3.GetOctets(), this.issuingDistributionPoint))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400153C RID: 5436
		private X509Certificate certificateChecking;

		// Token: 0x0400153D RID: 5437
		private DateTimeObject dateAndTime;

		// Token: 0x0400153E RID: 5438
		private ICollection issuers;

		// Token: 0x0400153F RID: 5439
		private BigInteger maxCrlNumber;

		// Token: 0x04001540 RID: 5440
		private BigInteger minCrlNumber;

		// Token: 0x04001541 RID: 5441
		private IX509AttributeCertificate attrCertChecking;

		// Token: 0x04001542 RID: 5442
		private bool completeCrlEnabled;

		// Token: 0x04001543 RID: 5443
		private bool deltaCrlIndicatorEnabled;

		// Token: 0x04001544 RID: 5444
		private byte[] issuingDistributionPoint;

		// Token: 0x04001545 RID: 5445
		private bool issuingDistributionPointEnabled;

		// Token: 0x04001546 RID: 5446
		private BigInteger maxBaseCrlNumber;
	}
}
