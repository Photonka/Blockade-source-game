using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Date;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Extension;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store
{
	// Token: 0x02000241 RID: 577
	public class X509CertStoreSelector : IX509Selector, ICloneable
	{
		// Token: 0x06001558 RID: 5464 RVA: 0x000B08B6 File Offset: 0x000AEAB6
		public X509CertStoreSelector()
		{
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x000B08C8 File Offset: 0x000AEAC8
		public X509CertStoreSelector(X509CertStoreSelector o)
		{
			this.authorityKeyIdentifier = o.AuthorityKeyIdentifier;
			this.basicConstraints = o.BasicConstraints;
			this.certificate = o.Certificate;
			this.certificateValid = o.CertificateValid;
			this.extendedKeyUsage = o.ExtendedKeyUsage;
			this.ignoreX509NameOrdering = o.IgnoreX509NameOrdering;
			this.issuer = o.Issuer;
			this.keyUsage = o.KeyUsage;
			this.policy = o.Policy;
			this.privateKeyValid = o.PrivateKeyValid;
			this.serialNumber = o.SerialNumber;
			this.subject = o.Subject;
			this.subjectKeyIdentifier = o.SubjectKeyIdentifier;
			this.subjectPublicKey = o.SubjectPublicKey;
			this.subjectPublicKeyAlgID = o.SubjectPublicKeyAlgID;
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x000B0996 File Offset: 0x000AEB96
		public virtual object Clone()
		{
			return new X509CertStoreSelector(this);
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x000B099E File Offset: 0x000AEB9E
		// (set) Token: 0x0600155C RID: 5468 RVA: 0x000B09AB File Offset: 0x000AEBAB
		public byte[] AuthorityKeyIdentifier
		{
			get
			{
				return Arrays.Clone(this.authorityKeyIdentifier);
			}
			set
			{
				this.authorityKeyIdentifier = Arrays.Clone(value);
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x000B09B9 File Offset: 0x000AEBB9
		// (set) Token: 0x0600155E RID: 5470 RVA: 0x000B09C1 File Offset: 0x000AEBC1
		public int BasicConstraints
		{
			get
			{
				return this.basicConstraints;
			}
			set
			{
				if (value < -2)
				{
					throw new ArgumentException("value can't be less than -2", "value");
				}
				this.basicConstraints = value;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x000B09DF File Offset: 0x000AEBDF
		// (set) Token: 0x06001560 RID: 5472 RVA: 0x000B09E7 File Offset: 0x000AEBE7
		public X509Certificate Certificate
		{
			get
			{
				return this.certificate;
			}
			set
			{
				this.certificate = value;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x000B09F0 File Offset: 0x000AEBF0
		// (set) Token: 0x06001562 RID: 5474 RVA: 0x000B09F8 File Offset: 0x000AEBF8
		public DateTimeObject CertificateValid
		{
			get
			{
				return this.certificateValid;
			}
			set
			{
				this.certificateValid = value;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x000B0A01 File Offset: 0x000AEC01
		// (set) Token: 0x06001564 RID: 5476 RVA: 0x000B0A0E File Offset: 0x000AEC0E
		public ISet ExtendedKeyUsage
		{
			get
			{
				return X509CertStoreSelector.CopySet(this.extendedKeyUsage);
			}
			set
			{
				this.extendedKeyUsage = X509CertStoreSelector.CopySet(value);
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x000B0A1C File Offset: 0x000AEC1C
		// (set) Token: 0x06001566 RID: 5478 RVA: 0x000B0A24 File Offset: 0x000AEC24
		public bool IgnoreX509NameOrdering
		{
			get
			{
				return this.ignoreX509NameOrdering;
			}
			set
			{
				this.ignoreX509NameOrdering = value;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x000B0A2D File Offset: 0x000AEC2D
		// (set) Token: 0x06001568 RID: 5480 RVA: 0x000B0A35 File Offset: 0x000AEC35
		public X509Name Issuer
		{
			get
			{
				return this.issuer;
			}
			set
			{
				this.issuer = value;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x000B0A3E File Offset: 0x000AEC3E
		[Obsolete("Avoid working with X509Name objects in string form")]
		public string IssuerAsString
		{
			get
			{
				if (this.issuer == null)
				{
					return null;
				}
				return this.issuer.ToString();
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600156A RID: 5482 RVA: 0x000B0A55 File Offset: 0x000AEC55
		// (set) Token: 0x0600156B RID: 5483 RVA: 0x000B0A62 File Offset: 0x000AEC62
		public bool[] KeyUsage
		{
			get
			{
				return X509CertStoreSelector.CopyBoolArray(this.keyUsage);
			}
			set
			{
				this.keyUsage = X509CertStoreSelector.CopyBoolArray(value);
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x0600156C RID: 5484 RVA: 0x000B0A70 File Offset: 0x000AEC70
		// (set) Token: 0x0600156D RID: 5485 RVA: 0x000B0A7D File Offset: 0x000AEC7D
		public ISet Policy
		{
			get
			{
				return X509CertStoreSelector.CopySet(this.policy);
			}
			set
			{
				this.policy = X509CertStoreSelector.CopySet(value);
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x000B0A8B File Offset: 0x000AEC8B
		// (set) Token: 0x0600156F RID: 5487 RVA: 0x000B0A93 File Offset: 0x000AEC93
		public DateTimeObject PrivateKeyValid
		{
			get
			{
				return this.privateKeyValid;
			}
			set
			{
				this.privateKeyValid = value;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x000B0A9C File Offset: 0x000AEC9C
		// (set) Token: 0x06001571 RID: 5489 RVA: 0x000B0AA4 File Offset: 0x000AECA4
		public BigInteger SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
			set
			{
				this.serialNumber = value;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x000B0AAD File Offset: 0x000AECAD
		// (set) Token: 0x06001573 RID: 5491 RVA: 0x000B0AB5 File Offset: 0x000AECB5
		public X509Name Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				this.subject = value;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x000B0ABE File Offset: 0x000AECBE
		[Obsolete("Avoid working with X509Name objects in string form")]
		public string SubjectAsString
		{
			get
			{
				if (this.subject == null)
				{
					return null;
				}
				return this.subject.ToString();
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x000B0AD5 File Offset: 0x000AECD5
		// (set) Token: 0x06001576 RID: 5494 RVA: 0x000B0AE2 File Offset: 0x000AECE2
		public byte[] SubjectKeyIdentifier
		{
			get
			{
				return Arrays.Clone(this.subjectKeyIdentifier);
			}
			set
			{
				this.subjectKeyIdentifier = Arrays.Clone(value);
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x000B0AF0 File Offset: 0x000AECF0
		// (set) Token: 0x06001578 RID: 5496 RVA: 0x000B0AF8 File Offset: 0x000AECF8
		public SubjectPublicKeyInfo SubjectPublicKey
		{
			get
			{
				return this.subjectPublicKey;
			}
			set
			{
				this.subjectPublicKey = value;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06001579 RID: 5497 RVA: 0x000B0B01 File Offset: 0x000AED01
		// (set) Token: 0x0600157A RID: 5498 RVA: 0x000B0B09 File Offset: 0x000AED09
		public DerObjectIdentifier SubjectPublicKeyAlgID
		{
			get
			{
				return this.subjectPublicKeyAlgID;
			}
			set
			{
				this.subjectPublicKeyAlgID = value;
			}
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x000B0B14 File Offset: 0x000AED14
		public virtual bool Match(object obj)
		{
			X509Certificate x509Certificate = obj as X509Certificate;
			if (x509Certificate == null)
			{
				return false;
			}
			if (!X509CertStoreSelector.MatchExtension(this.authorityKeyIdentifier, x509Certificate, X509Extensions.AuthorityKeyIdentifier))
			{
				return false;
			}
			if (this.basicConstraints != -1)
			{
				int num = x509Certificate.GetBasicConstraints();
				if (this.basicConstraints == -2)
				{
					if (num != -1)
					{
						return false;
					}
				}
				else if (num < this.basicConstraints)
				{
					return false;
				}
			}
			if (this.certificate != null && !this.certificate.Equals(x509Certificate))
			{
				return false;
			}
			if (this.certificateValid != null && !x509Certificate.IsValid(this.certificateValid.Value))
			{
				return false;
			}
			if (this.extendedKeyUsage != null)
			{
				IList list = x509Certificate.GetExtendedKeyUsage();
				if (list != null)
				{
					foreach (object obj2 in this.extendedKeyUsage)
					{
						DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)obj2;
						if (!list.Contains(derObjectIdentifier.Id))
						{
							return false;
						}
					}
				}
			}
			if (this.issuer != null && !this.issuer.Equivalent(x509Certificate.IssuerDN, !this.ignoreX509NameOrdering))
			{
				return false;
			}
			if (this.keyUsage != null)
			{
				bool[] array = x509Certificate.GetKeyUsage();
				if (array != null)
				{
					for (int i = 0; i < 9; i++)
					{
						if (this.keyUsage[i] && !array[i])
						{
							return false;
						}
					}
				}
			}
			if (this.policy != null)
			{
				Asn1OctetString extensionValue = x509Certificate.GetExtensionValue(X509Extensions.CertificatePolicies);
				if (extensionValue == null)
				{
					return false;
				}
				Asn1Sequence instance = Asn1Sequence.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue));
				if (this.policy.Count < 1 && instance.Count < 1)
				{
					return false;
				}
				bool flag = false;
				foreach (object obj3 in instance)
				{
					PolicyInformation policyInformation = (PolicyInformation)obj3;
					if (this.policy.Contains(policyInformation.PolicyIdentifier))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			if (this.privateKeyValid != null)
			{
				Asn1OctetString extensionValue2 = x509Certificate.GetExtensionValue(X509Extensions.PrivateKeyUsagePeriod);
				if (extensionValue2 == null)
				{
					return false;
				}
				PrivateKeyUsagePeriod instance2 = PrivateKeyUsagePeriod.GetInstance(X509ExtensionUtilities.FromExtensionValue(extensionValue2));
				DateTime value = this.privateKeyValid.Value;
				DateTime value2 = instance2.NotAfter.ToDateTime();
				DateTime value3 = instance2.NotBefore.ToDateTime();
				if (value.CompareTo(value2) > 0 || value.CompareTo(value3) < 0)
				{
					return false;
				}
			}
			return (this.serialNumber == null || this.serialNumber.Equals(x509Certificate.SerialNumber)) && (this.subject == null || this.subject.Equivalent(x509Certificate.SubjectDN, !this.ignoreX509NameOrdering)) && X509CertStoreSelector.MatchExtension(this.subjectKeyIdentifier, x509Certificate, X509Extensions.SubjectKeyIdentifier) && (this.subjectPublicKey == null || this.subjectPublicKey.Equals(X509CertStoreSelector.GetSubjectPublicKey(x509Certificate))) && (this.subjectPublicKeyAlgID == null || this.subjectPublicKeyAlgID.Equals(X509CertStoreSelector.GetSubjectPublicKey(x509Certificate).AlgorithmID));
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x000B0E20 File Offset: 0x000AF020
		internal static bool IssuersMatch(X509Name a, X509Name b)
		{
			if (a != null)
			{
				return a.Equivalent(b, true);
			}
			return b == null;
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x000B0E32 File Offset: 0x000AF032
		private static bool[] CopyBoolArray(bool[] b)
		{
			if (b != null)
			{
				return (bool[])b.Clone();
			}
			return null;
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x000B0E44 File Offset: 0x000AF044
		private static ISet CopySet(ISet s)
		{
			if (s != null)
			{
				return new HashSet(s);
			}
			return null;
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x000B0E51 File Offset: 0x000AF051
		private static SubjectPublicKeyInfo GetSubjectPublicKey(X509Certificate c)
		{
			return SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(c.GetPublicKey());
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x000B0E60 File Offset: 0x000AF060
		private static bool MatchExtension(byte[] b, X509Certificate c, DerObjectIdentifier oid)
		{
			if (b == null)
			{
				return true;
			}
			Asn1OctetString extensionValue = c.GetExtensionValue(oid);
			return extensionValue != null && Arrays.AreEqual(b, extensionValue.GetOctets());
		}

		// Token: 0x0400152B RID: 5419
		private byte[] authorityKeyIdentifier;

		// Token: 0x0400152C RID: 5420
		private int basicConstraints = -1;

		// Token: 0x0400152D RID: 5421
		private X509Certificate certificate;

		// Token: 0x0400152E RID: 5422
		private DateTimeObject certificateValid;

		// Token: 0x0400152F RID: 5423
		private ISet extendedKeyUsage;

		// Token: 0x04001530 RID: 5424
		private bool ignoreX509NameOrdering;

		// Token: 0x04001531 RID: 5425
		private X509Name issuer;

		// Token: 0x04001532 RID: 5426
		private bool[] keyUsage;

		// Token: 0x04001533 RID: 5427
		private ISet policy;

		// Token: 0x04001534 RID: 5428
		private DateTimeObject privateKeyValid;

		// Token: 0x04001535 RID: 5429
		private BigInteger serialNumber;

		// Token: 0x04001536 RID: 5430
		private X509Name subject;

		// Token: 0x04001537 RID: 5431
		private byte[] subjectKeyIdentifier;

		// Token: 0x04001538 RID: 5432
		private SubjectPublicKeyInfo subjectPublicKey;

		// Token: 0x04001539 RID: 5433
		private DerObjectIdentifier subjectPublicKeyAlgID;
	}
}
