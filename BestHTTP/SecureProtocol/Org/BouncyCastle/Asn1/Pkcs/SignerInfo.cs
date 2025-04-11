using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006EE RID: 1774
	public class SignerInfo : Asn1Encodable
	{
		// Token: 0x0600413A RID: 16698 RVA: 0x00185367 File Offset: 0x00183567
		public static SignerInfo GetInstance(object obj)
		{
			if (obj is SignerInfo)
			{
				return (SignerInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SignerInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600413B RID: 16699 RVA: 0x001853A6 File Offset: 0x001835A6
		public SignerInfo(DerInteger version, IssuerAndSerialNumber issuerAndSerialNumber, AlgorithmIdentifier digAlgorithm, Asn1Set authenticatedAttributes, AlgorithmIdentifier digEncryptionAlgorithm, Asn1OctetString encryptedDigest, Asn1Set unauthenticatedAttributes)
		{
			this.version = version;
			this.issuerAndSerialNumber = issuerAndSerialNumber;
			this.digAlgorithm = digAlgorithm;
			this.authenticatedAttributes = authenticatedAttributes;
			this.digEncryptionAlgorithm = digEncryptionAlgorithm;
			this.encryptedDigest = encryptedDigest;
			this.unauthenticatedAttributes = unauthenticatedAttributes;
		}

		// Token: 0x0600413C RID: 16700 RVA: 0x001853E4 File Offset: 0x001835E4
		public SignerInfo(Asn1Sequence seq)
		{
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.version = (DerInteger)enumerator.Current;
			enumerator.MoveNext();
			this.issuerAndSerialNumber = IssuerAndSerialNumber.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			this.digAlgorithm = AlgorithmIdentifier.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			object obj = enumerator.Current;
			if (obj is Asn1TaggedObject)
			{
				this.authenticatedAttributes = Asn1Set.GetInstance((Asn1TaggedObject)obj, false);
				enumerator.MoveNext();
				this.digEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(enumerator.Current);
			}
			else
			{
				this.authenticatedAttributes = null;
				this.digEncryptionAlgorithm = AlgorithmIdentifier.GetInstance(obj);
			}
			enumerator.MoveNext();
			this.encryptedDigest = Asn1OctetString.GetInstance(enumerator.Current);
			if (enumerator.MoveNext())
			{
				this.unauthenticatedAttributes = Asn1Set.GetInstance((Asn1TaggedObject)enumerator.Current, false);
				return;
			}
			this.unauthenticatedAttributes = null;
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x0600413D RID: 16701 RVA: 0x001854DA File Offset: 0x001836DA
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x0600413E RID: 16702 RVA: 0x001854E2 File Offset: 0x001836E2
		public IssuerAndSerialNumber IssuerAndSerialNumber
		{
			get
			{
				return this.issuerAndSerialNumber;
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x0600413F RID: 16703 RVA: 0x001854EA File Offset: 0x001836EA
		public Asn1Set AuthenticatedAttributes
		{
			get
			{
				return this.authenticatedAttributes;
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06004140 RID: 16704 RVA: 0x001854F2 File Offset: 0x001836F2
		public AlgorithmIdentifier DigestAlgorithm
		{
			get
			{
				return this.digAlgorithm;
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06004141 RID: 16705 RVA: 0x001854FA File Offset: 0x001836FA
		public Asn1OctetString EncryptedDigest
		{
			get
			{
				return this.encryptedDigest;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06004142 RID: 16706 RVA: 0x00185502 File Offset: 0x00183702
		public AlgorithmIdentifier DigestEncryptionAlgorithm
		{
			get
			{
				return this.digEncryptionAlgorithm;
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06004143 RID: 16707 RVA: 0x0018550A File Offset: 0x0018370A
		public Asn1Set UnauthenticatedAttributes
		{
			get
			{
				return this.unauthenticatedAttributes;
			}
		}

		// Token: 0x06004144 RID: 16708 RVA: 0x00185514 File Offset: 0x00183714
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.issuerAndSerialNumber,
				this.digAlgorithm
			});
			if (this.authenticatedAttributes != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.authenticatedAttributes)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.digEncryptionAlgorithm,
				this.encryptedDigest
			});
			if (this.unauthenticatedAttributes != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.unauthenticatedAttributes)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400294F RID: 10575
		private DerInteger version;

		// Token: 0x04002950 RID: 10576
		private IssuerAndSerialNumber issuerAndSerialNumber;

		// Token: 0x04002951 RID: 10577
		private AlgorithmIdentifier digAlgorithm;

		// Token: 0x04002952 RID: 10578
		private Asn1Set authenticatedAttributes;

		// Token: 0x04002953 RID: 10579
		private AlgorithmIdentifier digEncryptionAlgorithm;

		// Token: 0x04002954 RID: 10580
		private Asn1OctetString encryptedDigest;

		// Token: 0x04002955 RID: 10581
		private Asn1Set unauthenticatedAttributes;
	}
}
