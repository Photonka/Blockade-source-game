using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020006AE RID: 1710
	public class X509Extensions : Asn1Encodable
	{
		// Token: 0x06003F69 RID: 16233 RVA: 0x0017CC74 File Offset: 0x0017AE74
		public static X509Extensions GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return X509Extensions.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06003F6A RID: 16234 RVA: 0x0017CC84 File Offset: 0x0017AE84
		public static X509Extensions GetInstance(object obj)
		{
			if (obj == null || obj is X509Extensions)
			{
				return (X509Extensions)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new X509Extensions((Asn1Sequence)obj);
			}
			if (obj is Asn1TaggedObject)
			{
				return X509Extensions.GetInstance(((Asn1TaggedObject)obj).GetObject());
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003F6B RID: 16235 RVA: 0x0017CCEC File Offset: 0x0017AEEC
		private X509Extensions(Asn1Sequence seq)
		{
			this.ordering = Platform.CreateArrayList();
			foreach (object obj in seq)
			{
				Asn1Sequence instance = Asn1Sequence.GetInstance(((Asn1Encodable)obj).ToAsn1Object());
				if (instance.Count < 2 || instance.Count > 3)
				{
					throw new ArgumentException("Bad sequence size: " + instance.Count);
				}
				DerObjectIdentifier instance2 = DerObjectIdentifier.GetInstance(instance[0].ToAsn1Object());
				bool critical = instance.Count == 3 && DerBoolean.GetInstance(instance[1].ToAsn1Object()).IsTrue;
				Asn1OctetString instance3 = Asn1OctetString.GetInstance(instance[instance.Count - 1].ToAsn1Object());
				if (this.extensions.Contains(instance2))
				{
					throw new ArgumentException("repeated extension found: " + instance2);
				}
				this.extensions.Add(instance2, new X509Extension(critical, instance3));
				this.ordering.Add(instance2);
			}
		}

		// Token: 0x06003F6C RID: 16236 RVA: 0x0017CE24 File Offset: 0x0017B024
		public X509Extensions(IDictionary extensions) : this(null, extensions)
		{
		}

		// Token: 0x06003F6D RID: 16237 RVA: 0x0017CE30 File Offset: 0x0017B030
		public X509Extensions(IList ordering, IDictionary extensions)
		{
			if (ordering == null)
			{
				this.ordering = Platform.CreateArrayList(extensions.Keys);
			}
			else
			{
				this.ordering = Platform.CreateArrayList(ordering);
			}
			foreach (object obj in this.ordering)
			{
				DerObjectIdentifier key = (DerObjectIdentifier)obj;
				this.extensions.Add(key, (X509Extension)extensions[key]);
			}
		}

		// Token: 0x06003F6E RID: 16238 RVA: 0x0017CED0 File Offset: 0x0017B0D0
		public X509Extensions(IList oids, IList values)
		{
			this.ordering = Platform.CreateArrayList(oids);
			int num = 0;
			foreach (object obj in this.ordering)
			{
				DerObjectIdentifier key = (DerObjectIdentifier)obj;
				this.extensions.Add(key, (X509Extension)values[num++]);
			}
		}

		// Token: 0x06003F6F RID: 16239 RVA: 0x0017CF60 File Offset: 0x0017B160
		[Obsolete]
		public X509Extensions(Hashtable extensions) : this(null, extensions)
		{
		}

		// Token: 0x06003F70 RID: 16240 RVA: 0x0017CF6C File Offset: 0x0017B16C
		[Obsolete]
		public X509Extensions(ArrayList ordering, Hashtable extensions)
		{
			if (ordering == null)
			{
				this.ordering = Platform.CreateArrayList(extensions.Keys);
			}
			else
			{
				this.ordering = Platform.CreateArrayList(ordering);
			}
			foreach (object obj in this.ordering)
			{
				DerObjectIdentifier key = (DerObjectIdentifier)obj;
				this.extensions.Add(key, (X509Extension)extensions[key]);
			}
		}

		// Token: 0x06003F71 RID: 16241 RVA: 0x0017D00C File Offset: 0x0017B20C
		[Obsolete]
		public X509Extensions(ArrayList oids, ArrayList values)
		{
			this.ordering = Platform.CreateArrayList(oids);
			int num = 0;
			foreach (object obj in this.ordering)
			{
				DerObjectIdentifier key = (DerObjectIdentifier)obj;
				this.extensions.Add(key, (X509Extension)values[num++]);
			}
		}

		// Token: 0x06003F72 RID: 16242 RVA: 0x0017D09C File Offset: 0x0017B29C
		[Obsolete("Use ExtensionOids IEnumerable property")]
		public IEnumerator Oids()
		{
			return this.ExtensionOids.GetEnumerator();
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06003F73 RID: 16243 RVA: 0x0017D0A9 File Offset: 0x0017B2A9
		public IEnumerable ExtensionOids
		{
			get
			{
				return new EnumerableProxy(this.ordering);
			}
		}

		// Token: 0x06003F74 RID: 16244 RVA: 0x0017D0B6 File Offset: 0x0017B2B6
		public X509Extension GetExtension(DerObjectIdentifier oid)
		{
			return (X509Extension)this.extensions[oid];
		}

		// Token: 0x06003F75 RID: 16245 RVA: 0x0017D0CC File Offset: 0x0017B2CC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			foreach (object obj in this.ordering)
			{
				DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)obj;
				X509Extension x509Extension = (X509Extension)this.extensions[derObjectIdentifier];
				Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector(new Asn1Encodable[]
				{
					derObjectIdentifier
				});
				if (x509Extension.IsCritical)
				{
					asn1EncodableVector2.Add(new Asn1Encodable[]
					{
						DerBoolean.True
					});
				}
				asn1EncodableVector2.Add(new Asn1Encodable[]
				{
					x509Extension.Value
				});
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerSequence(asn1EncodableVector2)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x06003F76 RID: 16246 RVA: 0x0017D1A0 File Offset: 0x0017B3A0
		public bool Equivalent(X509Extensions other)
		{
			if (this.extensions.Count != other.extensions.Count)
			{
				return false;
			}
			foreach (object obj in this.extensions.Keys)
			{
				DerObjectIdentifier key = (DerObjectIdentifier)obj;
				if (!this.extensions[key].Equals(other.extensions[key]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003F77 RID: 16247 RVA: 0x0017D238 File Offset: 0x0017B438
		public DerObjectIdentifier[] GetExtensionOids()
		{
			return X509Extensions.ToOidArray(this.ordering);
		}

		// Token: 0x06003F78 RID: 16248 RVA: 0x0017D245 File Offset: 0x0017B445
		public DerObjectIdentifier[] GetNonCriticalExtensionOids()
		{
			return this.GetExtensionOids(false);
		}

		// Token: 0x06003F79 RID: 16249 RVA: 0x0017D24E File Offset: 0x0017B44E
		public DerObjectIdentifier[] GetCriticalExtensionOids()
		{
			return this.GetExtensionOids(true);
		}

		// Token: 0x06003F7A RID: 16250 RVA: 0x0017D258 File Offset: 0x0017B458
		private DerObjectIdentifier[] GetExtensionOids(bool isCritical)
		{
			IList list = Platform.CreateArrayList();
			foreach (object obj in this.ordering)
			{
				DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)obj;
				if (((X509Extension)this.extensions[derObjectIdentifier]).IsCritical == isCritical)
				{
					list.Add(derObjectIdentifier);
				}
			}
			return X509Extensions.ToOidArray(list);
		}

		// Token: 0x06003F7B RID: 16251 RVA: 0x0017D2D8 File Offset: 0x0017B4D8
		private static DerObjectIdentifier[] ToOidArray(IList oids)
		{
			DerObjectIdentifier[] array = new DerObjectIdentifier[oids.Count];
			oids.CopyTo(array, 0);
			return array;
		}

		// Token: 0x04002728 RID: 10024
		public static readonly DerObjectIdentifier SubjectDirectoryAttributes = new DerObjectIdentifier("2.5.29.9");

		// Token: 0x04002729 RID: 10025
		public static readonly DerObjectIdentifier SubjectKeyIdentifier = new DerObjectIdentifier("2.5.29.14");

		// Token: 0x0400272A RID: 10026
		public static readonly DerObjectIdentifier KeyUsage = new DerObjectIdentifier("2.5.29.15");

		// Token: 0x0400272B RID: 10027
		public static readonly DerObjectIdentifier PrivateKeyUsagePeriod = new DerObjectIdentifier("2.5.29.16");

		// Token: 0x0400272C RID: 10028
		public static readonly DerObjectIdentifier SubjectAlternativeName = new DerObjectIdentifier("2.5.29.17");

		// Token: 0x0400272D RID: 10029
		public static readonly DerObjectIdentifier IssuerAlternativeName = new DerObjectIdentifier("2.5.29.18");

		// Token: 0x0400272E RID: 10030
		public static readonly DerObjectIdentifier BasicConstraints = new DerObjectIdentifier("2.5.29.19");

		// Token: 0x0400272F RID: 10031
		public static readonly DerObjectIdentifier CrlNumber = new DerObjectIdentifier("2.5.29.20");

		// Token: 0x04002730 RID: 10032
		public static readonly DerObjectIdentifier ReasonCode = new DerObjectIdentifier("2.5.29.21");

		// Token: 0x04002731 RID: 10033
		public static readonly DerObjectIdentifier InstructionCode = new DerObjectIdentifier("2.5.29.23");

		// Token: 0x04002732 RID: 10034
		public static readonly DerObjectIdentifier InvalidityDate = new DerObjectIdentifier("2.5.29.24");

		// Token: 0x04002733 RID: 10035
		public static readonly DerObjectIdentifier DeltaCrlIndicator = new DerObjectIdentifier("2.5.29.27");

		// Token: 0x04002734 RID: 10036
		public static readonly DerObjectIdentifier IssuingDistributionPoint = new DerObjectIdentifier("2.5.29.28");

		// Token: 0x04002735 RID: 10037
		public static readonly DerObjectIdentifier CertificateIssuer = new DerObjectIdentifier("2.5.29.29");

		// Token: 0x04002736 RID: 10038
		public static readonly DerObjectIdentifier NameConstraints = new DerObjectIdentifier("2.5.29.30");

		// Token: 0x04002737 RID: 10039
		public static readonly DerObjectIdentifier CrlDistributionPoints = new DerObjectIdentifier("2.5.29.31");

		// Token: 0x04002738 RID: 10040
		public static readonly DerObjectIdentifier CertificatePolicies = new DerObjectIdentifier("2.5.29.32");

		// Token: 0x04002739 RID: 10041
		public static readonly DerObjectIdentifier PolicyMappings = new DerObjectIdentifier("2.5.29.33");

		// Token: 0x0400273A RID: 10042
		public static readonly DerObjectIdentifier AuthorityKeyIdentifier = new DerObjectIdentifier("2.5.29.35");

		// Token: 0x0400273B RID: 10043
		public static readonly DerObjectIdentifier PolicyConstraints = new DerObjectIdentifier("2.5.29.36");

		// Token: 0x0400273C RID: 10044
		public static readonly DerObjectIdentifier ExtendedKeyUsage = new DerObjectIdentifier("2.5.29.37");

		// Token: 0x0400273D RID: 10045
		public static readonly DerObjectIdentifier FreshestCrl = new DerObjectIdentifier("2.5.29.46");

		// Token: 0x0400273E RID: 10046
		public static readonly DerObjectIdentifier InhibitAnyPolicy = new DerObjectIdentifier("2.5.29.54");

		// Token: 0x0400273F RID: 10047
		public static readonly DerObjectIdentifier AuthorityInfoAccess = new DerObjectIdentifier("1.3.6.1.5.5.7.1.1");

		// Token: 0x04002740 RID: 10048
		public static readonly DerObjectIdentifier SubjectInfoAccess = new DerObjectIdentifier("1.3.6.1.5.5.7.1.11");

		// Token: 0x04002741 RID: 10049
		public static readonly DerObjectIdentifier LogoType = new DerObjectIdentifier("1.3.6.1.5.5.7.1.12");

		// Token: 0x04002742 RID: 10050
		public static readonly DerObjectIdentifier BiometricInfo = new DerObjectIdentifier("1.3.6.1.5.5.7.1.2");

		// Token: 0x04002743 RID: 10051
		public static readonly DerObjectIdentifier QCStatements = new DerObjectIdentifier("1.3.6.1.5.5.7.1.3");

		// Token: 0x04002744 RID: 10052
		public static readonly DerObjectIdentifier AuditIdentity = new DerObjectIdentifier("1.3.6.1.5.5.7.1.4");

		// Token: 0x04002745 RID: 10053
		public static readonly DerObjectIdentifier NoRevAvail = new DerObjectIdentifier("2.5.29.56");

		// Token: 0x04002746 RID: 10054
		public static readonly DerObjectIdentifier TargetInformation = new DerObjectIdentifier("2.5.29.55");

		// Token: 0x04002747 RID: 10055
		public static readonly DerObjectIdentifier ExpiredCertsOnCrl = new DerObjectIdentifier("2.5.29.60");

		// Token: 0x04002748 RID: 10056
		private readonly IDictionary extensions = Platform.CreateHashtable();

		// Token: 0x04002749 RID: 10057
		private readonly IList ordering;
	}
}
